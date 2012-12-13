using System;
using System.Collections.Generic;
using System.Threading;
using Interfaces;
using Utility;
using Utility.Properties;

namespace CTCOffice
{
    public class CTCOffice : ICTCOffice
    {
        #region Private Variables
        //System Hooks
        private ISimulationEnvironment _env;
        private Operator _op;
        private ITrackController _primaryTrackControllerGreen;
        private ITrackController _primaryTrackControllerRed;
        private ResourceWrapper _res;

        //env timer variables
        private double _tickCount;
        private double _rate;

        //Request Queues
        private Queue<IRequest> _requestsIn;
        private Queue<IRequest> _requestsOut;
        private bool _processingInRequests;
        private bool _processingOutRequests;
        private event EventHandler<EventArgs> RequestQueueOut;
        private event EventHandler<EventArgs> RequestQueueIn;

        //Track Items
        private LineData _redLineData;
        private LineData _greenLineData;
        private bool _redLoaded;
        private bool _greenLoaded;
        private List<TrainAndBlock> _containedTrainAndBlock;

        //messages for gui
        private List<string> _messages;

        //Mutex for updating
        private Mutex _populateTrackMutex;
        private Mutex _updateTrackMutex;
        private Mutex _loadTrackMutex;
        #endregion

        #region Constructor and Environment Tick Handler
        public CTCOffice(ISimulationEnvironment env, ITrackController redTC, ITrackController greenTC)
        {
            _populateTrackMutex = new Mutex(false);
            _updateTrackMutex = new Mutex(false);
            _loadTrackMutex = new Mutex(false);
            _rate = 100; //num of ticks
            _tickCount = 0;
            _rate = env.GetInterval();
            _env = env;
            _primaryTrackControllerGreen = greenTC;
            _primaryTrackControllerRed = redTC;

            _env.TrackModel.TrackChangedEvent += new EventHandler<EventArgs>(TrackModel_TrackChangedEvent);

            _messages = new List<string>();

            //subscribe to Environment Tick
            _env.Tick += _env_Tick;

            //create new resource wrapper
            _res = new ResourceWrapper();

            //create new operator object
            _op = new Operator();
            //set credentials
            _op.SetAuth("root", "admin");

            //create queues
            _requestsOut = new Queue<IRequest>();
            _requestsIn = new Queue<IRequest>();

            //create queue events
            RequestQueueIn += CTCOffice_RequestQueueIn;
            RequestQueueOut += CTCOffice_RequestQueueOut;

            //create queue processing flags / mutex
            _processingOutRequests = false;
            _processingInRequests = false;

            _redLoaded = false;
            _greenLoaded = false;

            _containedTrainAndBlock = new List<TrainAndBlock>();

            if (_env.TrackModel == null)
            {
                _env.SendLogEntry("CTCOffice: NULL Reference to TrackModel");
            }

        }//Constructor

        /// <summary>
        ///     Do Processing on Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _env_Tick(object sender, TickEventArgs e)
        {
            //check for track if both tracks are not up.. if tracks are up.. dont check
            if (!(_redLoaded && _greenLoaded))
            {
                IsTrackUp();
            }

            if (_primaryTrackControllerRed == null)
            {
                _primaryTrackControllerRed = _env.PrimaryTrackControllerRed;
            }

            if (_primaryTrackControllerGreen == null)
            {
                _primaryTrackControllerGreen = _env.PrimaryTrackControllerGreen;
            }

            _tickCount++;
            if (_tickCount >= _rate)
            {
                //AddAutomaticUpdate();//no longer need this information as of Track Model Update
                _tickCount = 0;
                PopulateTrack();//add trains to track
                
            }
        }

        #endregion

        #region Automated Track Update
        /// <summary>
        /// Handles changed event by track model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackModel_TrackChangedEvent(object sender, EventArgs e)
        {
            IsTrackUp();
            bool messageFlag = false;
            //parse track here
            if (_env.TrackModel.ChangeFlag == TrackChanged.Both)
            {
                UpdateRed();
                UpdateGreen();
                messageFlag = !messageFlag;
            }
            else if (_env.TrackModel.ChangeFlag == TrackChanged.Red)
            {
                UpdateRed();
                messageFlag = !messageFlag;
            }
            else if (_env.TrackModel.ChangeFlag == TrackChanged.Green)
            {
                UpdateGreen();
                messageFlag = !messageFlag;
            }

            PopulateTrack();

            if (messageFlag)
            {
                if (_messages.Count > 0)
                {
                    if (MessagesReady != null)
                    {
                        MessagesReady(this, EventArgs.Empty);
                    }
                }
            }//end if message flag
        }//end track changed

        /// <summary>
        /// Updates Red Line
        /// </summary>
        private void UpdateRed()
        {
            _updateTrackMutex.WaitOne();
            //request blocks in red line
            IRouteInfo rtnfo = _env.TrackModel.requestRouteInfo(0);
            foreach (IBlock b in rtnfo.BlockList)
            {
                //find block in layout and change image
                //speed limit of 500 is unique to block id 0 (yard) on both red and green line
                if (b.SpeedLimit != 500)
                {
                    LayoutCellDataContainer c = _redLineData.TriangulateContainer(b);
                    if (c != null)
                    {
                        c.Tile = _redLineData.GetBlockType(b);
                        if (c.Panel != null)
                        {
                            string msg = "Red Line: Block ID: " + c.Block.BlockID + " is now " + c.Block.State.ToString();
                            _messages.Add(msg);
                            _env.SendLogEntry("CTC Office: " + msg);
                            c.Panel.ReDrawMe();
                        }
                    }
                }
            }
            rtnfo = null;
            _updateTrackMutex.ReleaseMutex();
        }

        /// <summary>
        /// Updates Green Line
        /// </summary>
        private void UpdateGreen()
        {
            _updateTrackMutex.WaitOne();
            //request blocks in green line
            IRouteInfo rtnfo = _env.TrackModel.requestRouteInfo(1);
            foreach (IBlock b in rtnfo.BlockList)
            {
                //find block in layout and change image
                LayoutCellDataContainer c = _greenLineData.TriangulateContainer(b);
                if (c != null)
                {
                    c.Tile = _greenLineData.GetBlockType(b);
                    if (c.Panel != null)
                    {
                        string msg = "Red Line: Block ID: " + c.Block.BlockID + " is now " + c.Block.State.ToString();
                        _messages.Add(msg);
                        _env.SendLogEntry("CTC Office: " + msg);
                        c.Panel.ReDrawMe();
                    }
                }
            }
            rtnfo = null;
            _updateTrackMutex.ReleaseMutex();
        }

        #endregion

        #region Request Queues
        /// <summary>
        ///     Function to handle queue out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CTCOffice_RequestQueueOut(object sender, EventArgs e)
        {
            //handle sequential sending of the queue to the track controller
            if (!_processingOutRequests)
            {
                //if office is not already processing queue
                _processingOutRequests = true;
                while (_requestsOut.Count > 0)
                {
                    IRequest temp = _requestsOut.Dequeue();
                    SendRequest(temp);
                }
                _processingOutRequests = false;

                //failsafe - check to see if there is a new request while processing... (should nevert hit)
                if (_requestsOut.Count != 0)
                {
                    RequestQueueOut(this, EventArgs.Empty);
                }
            }
            //else already processing
        }

        /// <summary>
        ///     Function to handle Queue In
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CTCOffice_RequestQueueIn(object sender, EventArgs e)
        {
            //handle sequential recieving of requests from track controller
            if (!_processingInRequests)
            {
                _processingInRequests = true;
                while (_requestsIn.Count > 0)
                {
                    //check if return data is not null then handle request
                    if ((_requestsIn.Peek()).Info != null)
                    {
                        //if valid return data
                        IRequest r = _requestsIn.Dequeue();
                        /* Feature not needed at this time
                        if (_automation)
                        {
                            //send request back to system scheduler
                        }
                         */
                        InternalRequest(r);
                    }
                    else
                    {
                        //invalid return data
                        _requestsIn.Dequeue();
                    }
                }
                _processingInRequests = false;

                //failsafe - check to see if there is a new request while processing... (should nevert hit)
                if (_requestsIn.Count != 0)
                {
                    RequestQueueIn(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Operator Login / Logut Functions
        /// <summary>
        ///     Function to login the operator
        /// </summary>
        /// <param name="username">Passed in Operator Entered Username</param>
        /// <param name="password">Passed in Operator Entered Password</param>
        /// <returns>True if operator is logged in. Else False</returns>
        public bool Login(string username, string password)
        {
            bool status = false;
            _op.Login(username, password);
            status = _op.IsAuth();
            if (status)
            {
                _env.SendLogEntry("CTCOffice: User Logged in with username->" + username + ".");
                if (LoadData != null)
                {
                    LoadData(this, EventArgs.Empty);
                }
            }
            else
            {
                _env.SendLogEntry("CTCOffice: User Logged out.");
            }
            return status;
        }

        /// <summary>
        ///     Function to log the operator out
        /// </summary>
        /// <returns>Always True</returns>
        public bool Logout()
        {
            if (_op.IsAuth())
            {
                _op.Logout();
            }
            return true;
        }

        /// <summary>
        ///     Function to determine if Operator is authorized to use CTC
        /// </summary>
        /// <returns></returns>
        public bool IsAuth()
        {
            return _op.IsAuth();
        }

        #endregion

        #region Public Functions
        /// <summary>
        /// Manages Trains on Track
        /// </summary>
        public void PopulateTrack()
        {
            _populateTrackMutex.WaitOne();
            //_env.stopTick();
            //_env.Tick -= _env_Tick;
            //clear current trains
            //foreach(IBlock b in _containedBlocks)
            for (int i = 0; i < _containedTrainAndBlock.Count; i++)
            {
                IBlock b = _containedTrainAndBlock[i].Block;
                if (b.Line.CompareTo("Red") == 0)
                {
                    if (!(b.SpeedLimit == 500 || b.SpeedLimit == -1))
                    {
                        LayoutCellDataContainer c = _redLineData.TriangulateContainer(b);
                        c.Tile = _redLineData.GetBlockType(b);
                        c.Train = null;
                        if (c.Panel != null)
                        {
                            c.Panel.ReDrawMe();
                        }
                    }
                }//end if
                else
                {
                    if (!(b.SpeedLimit == 500 || b.SpeedLimit == -1))
                    {
                        LayoutCellDataContainer c = _greenLineData.TriangulateContainer(b);
                        c.Tile = _greenLineData.GetBlockType(b);
                        c.Train = null;
                        if (c.Panel != null)
                        {
                            c.Panel.ReDrawMe();
                        }
                    }
                }//end if
            }//end foreach

            //make list of new trains
            _containedTrainAndBlock.Clear();
            //foreach (ITrainModel t in _env.AllTrains)
            for (int i = 0; i < _env.AllTrains.Count; i++)
            {
                ITrainModel t = _env.AllTrains[i];
                TrainAndBlock tb = new TrainAndBlock();
                tb.Train = t;
                tb.Block = t.CurrentBlock;
                _containedTrainAndBlock.Add(tb);
            }//end foreach

            //update graphics
            //foreach (IBlock b in _containedBlocks)
            for (int i = 0; i < _containedTrainAndBlock.Count; i++)
            {
                TrainAndBlock tb = _containedTrainAndBlock[i];
                if (tb.Block.Line.CompareTo("Red") == 0)
                {
                    if (!(tb.Block.SpeedLimit == 500 || tb.Block.SpeedLimit == -1))
                    {
                        LayoutCellDataContainer c = _redLineData.TriangulateContainer(tb.Block);
                        c.Tile = _res.Train;
                        c.Train = tb.Train;

                        if (c.Panel != null)
                        {
                            string msg = "Red Line: Train ID: " + c.Train.TrainID + " is now on Block: " + c.Block.BlockID + ".";
                            _messages.Add(msg);
                            _env.SendLogEntry("CTC Office: " + msg);
                            c.Panel.ReDrawMe();
                        }
                    }

                }//end if
                else
                {
                    if (!(tb.Block.SpeedLimit == 500 || tb.Block.SpeedLimit == -1))
                    {
                        LayoutCellDataContainer c = _greenLineData.TriangulateContainer(tb.Block);
                        c.Tile = _res.Train;
                        c.Train = tb.Train;
                        if (c.Panel != null)
                        {
                            string msg = "Green Line: Train ID: " + c.Train.TrainID + " is now on Block: " + c.Block.BlockID + ".";
                            _messages.Add(msg);
                            _env.SendLogEntry("CTC Office: " + msg);
                            c.Panel.ReDrawMe();
                        }
                    }
                }//end if
            }//end for each

            _populateTrackMutex.ReleaseMutex();
            //_env.Tick += _env_Tick;
            //_env.startTick();
        }//End Populate Track

        /// <summary>
        ///     Function to throw the event to the System Scheduler to start automated scheduling
        /// </summary>
        public void StartScheduling()
        {
            if (StartAutomation != null)
            {
                StartAutomation(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Function to throw the event to the System Scheduler to start automated scheduling
        /// </summary>
        public void StopScheduling()
        {
            if (StopAutomation != null)
            {
                StopAutomation(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Function to return line data to gui
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public LineData GetLine(int line)
        {
            if (line == 0)
            {
                return _redLineData;
            }
            else if (line == 1)
            {
                return _greenLineData;
            }

            return null;
        }
        #endregion

        #region Private Functions

        private void IsTrackUp()
        {
            _loadTrackMutex.WaitOne();
            //Checks the environment to see if a Track Models exists
            if (_env.TrackModel != null)
            {
                //if both are not loaded
                if (!(_redLoaded && _greenLoaded))
                {
                    if (!_redLoaded)
                    {
                        _redLoaded = true;
                        //Checks if the red line has been loaded yet
                        if (_env.TrackModel.RedLoaded)
                        {
                            _redLineData = new LineData(_env.TrackModel.requestTrackGrid(0), _env, _res);
                            _redLoaded = true;
                            if (UnlockLogin != null)
                            {
                                UnlockLogin(this, EventArgs.Empty);
                            }
                        }
                        else
                        {
                            _redLoaded = false;
                            //_env.sendLogEntry("The Red Line has not been loaded yet.");
                        }
                    }


                    if (!_greenLoaded)
                    {
                        _greenLoaded = true;
                        //Checks if the green line has been loaded yet
                        if (_env.TrackModel.GreenLoaded)
                        {
                            _greenLineData = new LineData(_env.TrackModel.requestTrackGrid(1), _env, _res);
                            _greenLoaded = true;
                            if (UnlockLogin != null)
                            {
                                UnlockLogin(this, EventArgs.Empty);
                            }
                        }
                        else
                        {
                            _greenLoaded = false;
                            //_env.sendLogEntry("The Green Line has not be loaded yet.");
                        }
                    }
                }
            }
            else
            {
                _env.SendLogEntry("CTCOffice: NULL Reference to TrackModel");
            }
            _loadTrackMutex.ReleaseMutex();
        }

        /// <summary>
        ///     Function to handle return request
        /// </summary>
        /// <param name="request"></param>
        private void InternalRequest(IRequest request)
        {
            /* Implemented Else Where
            IStatus s = request.Info;
            if (_env.TrackModel.ChangeFlag != TrackChanged.None)
            {
                foreach (IBlock b in s.Blocks)
                {
                    _messages.Add("Block " + b.BlockID + ": is now in state " + b.State.ToString());
                    if (b.Line.CompareTo("Red") != 0)
                    {
                        //red line
                        LayoutCellDataContainer c = _redLineData.TriangulateContainer(b);
                        c.Tile = _redLineData.GetBlockType(b);
                    }
                    else
                    {
                        //green line
                        LayoutCellDataContainer c = _greenLineData.TriangulateContainer(b);
                        c.Tile = _greenLineData.GetBlockType(b);
                    }
                }
            }//end process blocks
            */
        }


        /// <summary>
        ///  Requests Data from each Track Controller
        /// </summary>
        private void AddAutomaticUpdate()
        {
            ITrackModel tm = _env.TrackModel;
            if (tm != null)
            {
                IRouteInfo redInfo = tm.requestRouteInfo(0);
                foreach (IBlock b in redInfo.BlockList)
                {
                    trackControllerDataRequest(b.TrackCirID);
                }

                IRouteInfo greenInfo = tm.requestRouteInfo(1);
                foreach (IBlock b in greenInfo.BlockList)
                {
                    trackControllerDataRequest(b.TrackCirID);
                }
            }
        }//End AddAutomaticUpdate

        #endregion

        #region Line ID Functions
        /// <summary>
        ///     logic to determine which track line
        /// </summary>
        /// <param name="request">Request to process</param>
        /// <returns>Integer based Line ID</returns>
        private int DetermineLine(IRequest request)
        {
            //red=0....green=1
            if (request.Block != null)
            {
                return DetermineLine(request.Block);
            }

            return DetermineLine(request.TrainRoute);
        }

        /// <summary>
        /// Determins Line ID via route object
        /// </summary>
        /// <param name="route">Route in question</param>
        /// <returns>Integer based Line ID</returns>
        private int DetermineLine(IRoute route)
        {
            if (route == null)
            {
                return -1;
            }
            return route.RouteID;
        }

        /// <summary>
        /// Determins Line ID via block object
        /// </summary>
        /// <param name="block">Block In Question</param>
        /// <returns>Integer based Line ID</returns>
        private int DetermineLine(IBlock block)
        {
            if (block != null)
            {
                if (block.Line.CompareTo("Red") == 0 || block.Line.CompareTo("red") == 0 ||
                    block.Line.CompareTo("R") == 0 || block.Line.CompareTo("r") == 0)
                {
                    return 0;
                }
                else if (block.Line.CompareTo("Green") == 0 || block.Line.CompareTo("green") == 0 ||
                         block.Line.CompareTo("G") == 0 || block.Line.CompareTo("g") == 0)
                {
                    return 1;
                }
            }
            return -1;
        }

        #endregion

        #region Request Abstraction

        /// <summary>
        ///     Sends a request to the primary track controller. Has logic to determine with primary track controller
        /// </summary>
        /// <param name="request">Request to send to track controller</param>
        public void SendRequest(IRequest request)
        {
            int line = DetermineLine(request.Block);

            if (line == 0)
            {
                //red line
                _primaryTrackControllerRed.Request = request;
            }
            else if (line == 1)
            {
                //greenline
                _primaryTrackControllerGreen.Request = request;
            }

            if (line == 0 || line == 1)
            {
                string msg = "Request Sent: " + request.RequestType.ToString();
                _messages.Add(msg);
                _env.SendLogEntry("CTC Office: " + msg);
            }
        }

        //TODO verify BLOCKs
        /// <summary>
        /// Request abstraction for dispatching a train
        /// </summary>
        /// <param name="route">Determins which Line to dispatch train</param>
        public void dispatchTrainRequest(IRoute route)
        {
            int line = DetermineLine(route);
            int id = -1;
            if (line == 0)
            {
                id = _primaryTrackControllerRed.ID;
            }
            else if (line == 1)
            {
                id = _primaryTrackControllerGreen.ID;
            }
            else
            {
                _env.SendLogEntry("CTCOffice: INVALID ROUTE IN DISPATCH TRAIN REQUEST");
            }

            //change block from null to yard
            IRequest r = new Request(RequestTypes.DispatchTrain, id, 0, 0, 0, route, route.EndBlock);

            _requestsOut.Enqueue(r);
            _env.Dispatch(r);

            if (UpdatedData != null)
            {
                UpdatedData(this, EventArgs.Empty);
            }

            //RequestQueueOut(this, EventArgs.Empty);
        }

        /// <summary>
        /// Request Abstraction To send train back to yard
        /// </summary>
        /// <param name="trainID">Selected Train ID</param>
        /// <param name="trackControllerID">Current Track Controller ID</param>
        /// <param name="block">Current Block</param>
        public void setTrainOutOfServiceRequest(int trainID, int trackControllerID, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.SetTrainOOS, trackControllerID, trainID, 0, 0, null, block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        /// <summary>
        /// Request Abstraction to Route a train
        /// </summary>
        /// <param name="trainID">Selected Train ID</param>
        /// <param name="trackControllerID">Current Track Contrller ID</param>
        /// <param name="route">New Routing Class</param>
        /// <param name="block">Current Block</param>
        public void assignTrainRouteRequest(int trainID, int trackControllerID, IRoute route, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.AssignTrainRoute, trackControllerID, trainID, 0, 0, route,
                                             block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        /// <summary>
        /// Request Abstraction to Set Train Authority
        /// </summary>
        /// <param name="trainID">Selected Train Id</param>
        /// <param name="trackControllerID">Current Track Controller ID</param>
        /// <param name="authority">New Authority Limit</param>
        /// <param name="block">Current Block</param>
        public void setTrainAuthorityRequest(int trainID, int trackControllerID, int authority, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.SetTrainAuthority, trackControllerID, trainID, authority, 0,
                                             null, block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        /// <summary>
        /// Request Abstraction to close block
        /// </summary>
        /// <param name="trackControllerID">Track Controller that contains block</param>
        /// <param name="block">Block To Close</param>
        public void closeTrackBlockRequest(int trackControllerID, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.TrackMaintenanceClose, trackControllerID, 0, 0, 0, null, block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        /// <summary>
        /// Request Abstraction to open Block
        /// </summary>
        /// <param name="trackControllerID">Track Controller ID that contains the block</param>
        /// <param name="block">Block to open</param>
        public void openTrackBlockRequest(int trackControllerID, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.TrackMaintenanceOpen, trackControllerID, 0, 0, 0, null, block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        /// <summary>
        /// Request Abstraction to Set new train speed
        /// </summary>
        /// <param name="trainID">selected trian id</param>
        /// <param name="trackControllerID">Current Track Controller</param>
        /// <param name="speed">New Speed</param>
        /// <param name="block">Current Block</param>
        public void setTrainSpeedRequest(int trainID, int trackControllerID, double speed, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.SetTrainSpeed, trackControllerID, trainID, 0, speed, null,
                                             block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        /// <summary>
        /// Request Abstraction to get data from track contrller
        /// </summary>
        /// <param name="trackControllerID">track controller to get data from</param>
        public void trackControllerDataRequest(int trackControllerID)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.TrackControllerData, trackControllerID, 0, 0, 0, null, null));
            RequestQueueOut(this, EventArgs.Empty);
        }

        #endregion

        #region Public Properties / Methods (Interface)

        public event EventHandler<EventArgs> StartAutomation;

        public event EventHandler<EventArgs> StopAutomation;

        /// <summary>
        /// Method to allow any other module refresh the Trains in view of CTC
        /// </summary>
        public void ExternalRefresh()
        {
            PopulateTrack();
        }

        /// <summary>
        ///     Pass request from System Scheduler to Track Controller via send request
        /// </summary>
        /// <param name="request"></param>
        public void passRequest(IRequest request)
        {
            //add request to queue to send to Track Controller
            if (request != null)
            {
                _requestsOut.Enqueue(request);
                RequestQueueOut(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Sends Request to CTC for processing.  Will only process if request.Info is not null
        /// </summary>
        /// <param name="request">request to process</param>
        public void handleResponse(IRequest request)
        {
            //add request to queue for CTC to Process. CTC will only process if Info is not null
            if (request.Info != null)
            {
                _requestsIn.Enqueue(request);
                //throws event to actually process queue
                RequestQueueIn(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Public Properties (Non-Interface)
        /// <summary>
        /// Hook to resource wrapper - Made to save memory
        /// </summary>
        public ResourceWrapper Resource
        {
            get { return _res; }
        }

        /// <summary>
        /// Property to access System Messages
        /// </summary>
        public List<string> SystemMessages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        #endregion

        #region Public Events (Non-Interface)
        public event EventHandler<EventArgs> LoadData;
        public event EventHandler<EventArgs> UnlockLogin;
        public event EventHandler<EventArgs> MessagesReady;
        public event EventHandler<EventArgs> UpdatedData;
        #endregion
    }
}