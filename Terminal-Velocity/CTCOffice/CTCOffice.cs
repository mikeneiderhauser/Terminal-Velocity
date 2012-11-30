using System;
using System.Collections.Generic;
using Interfaces;
using Utility;
using Utility.Properties;

namespace CTCOffice
{
    public class CTCOffice : ICTCOffice
    {
        #region Private Variables

        private readonly ISimulationEnvironment _env;
        private readonly Operator _op;
        private readonly ITrackController _primaryTrackControllerGreen;
        private readonly ITrackController _primaryTrackControllerRed;

        /// <summary>
        ///     Number of Ticks Elapsed to update
        /// </summary>
        private readonly double _rate;

        private readonly Queue<IRequest> _requestsIn;
        private readonly Queue<IRequest> _requestsOut;
        private bool _automation;
        private LineData _greenLineData;
        private LineData _greenLineDataBackup;
        private bool _populationBlock;
        private bool _processingInRequests;
        private bool _processingOutRequests;
        private LineData _redLineData;
        private LineData _redLineDataBackup;

        private double _tickCount;
        private List<ITrainModel> _trains;
        private event EventHandler<EventArgs> RequestQueueOut;
        private event EventHandler<EventArgs> RequestQueueIn;

        #endregion

        #region Constructor

        public CTCOffice(ISimulationEnvironment env, ITrackController redTC, ITrackController greenTC)
        {
            _automation = false;
            _rate = 100; //num of ticks
            _tickCount = 0;
            _rate = env.getInterval();
            _env = env;
            _primaryTrackControllerGreen = greenTC;
            _primaryTrackControllerRed = redTC;

            _trains = new List<ITrainModel>();
            //subscribe to Environment Tick
            _env.Tick += _env_Tick;

            //create new operator object
            _op = new Operator();
            //set credentials
            _op.setAuth("root", "admin");

            if (_env.TrackModel != null)
            {
                _redLineData = new LineData(_env.TrackModel.requestTrackGrid(0), _env);
                _redLineDataBackup = new LineData(_env.TrackModel.requestTrackGrid(0), _env);
                _greenLineData = new LineData(_env.TrackModel.requestTrackGrid(1), _env);
                _greenLineDataBackup = new LineData(_env.TrackModel.requestTrackGrid(1), _env);
            }
            else
            {
                _env.sendLogEntry("CTCOffice: NULL Referenct to TrackModel");
            }

            //create queues
            _requestsOut = new Queue<IRequest>();
            _requestsIn = new Queue<IRequest>();

            //create queue events
            RequestQueueIn += CTCOffice_RequestQueueIn;
            RequestQueueOut += CTCOffice_RequestQueueOut;

            //create queue processing flags / mutex
            _processingOutRequests = false;
            _processingInRequests = false;

            _populationBlock = false;
        }

        #endregion

        #region Functions

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
                    sendRequest(temp);
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
                        internalRequest(r);
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

        /// <summary>
        ///     Function to login the operator
        /// </summary>
        /// <param name="username">Passed in Operator Entered Username</param>
        /// <param name="password">Passed in Operator Entered Password</param>
        /// <returns>True if operator is logged in. Else False</returns>
        public bool Login(string username, string password)
        {
            bool status = false;
            _op.login(username, password);
            status = _op.isAuth();
            if (status)
            {
                _env.sendLogEntry("CTCOffice: User Logged in with username->" + username + ".");
            }
            else
            {
                _env.sendLogEntry("CTCOffice: User Logged out.");
            }
            return status;
        }

        /// <summary>
        ///     Function to log the operator out
        /// </summary>
        /// <returns>Always True</returns>
        public bool Logout()
        {
            if (_op.isAuth())
            {
                _op.logout();
            }
            return true;
        }

        /// <summary>
        ///     Function to determine if Operator is authorized to use CTC
        /// </summary>
        /// <returns></returns>
        public bool isAuth()
        {
            return _op.isAuth();
        }

        /// <summary>
        ///     Function to throw the event to the System Scheduler to start automated scheduling
        /// </summary>
        public void StartScheduling()
        {
            if (StartAutomation != null)
            {
                StartAutomation(this, EventArgs.Empty);
            }
            _automation = true;
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
            _automation = false;
        }

        /// <summary>
        ///     Sends a request to the primary track controller. Has logic to determine with primary track controller
        /// </summary>
        /// <param name="request">Request to send to track controller</param>
        public void sendRequest(IRequest request)
        {
            int line = determineLine(request.Block);

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
        }

        /// <summary>
        ///     Function to handle return request
        /// </summary>
        /// <param name="request"></param>
        private void internalRequest(IRequest request)
        {
            //TODO
            IStatus s = request.Info;
        }

        /// <summary>
        ///     Do Processing on Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _env_Tick(object sender, TickEventArgs e)
        {
            _tickCount++;
            if (_tickCount == _rate)
            {
                //TODO
                //addAutomaticUpdate();
            }
        }

        /// <summary>
        ///     Calculates if the CTC should ask the track controllers for data.
        /// </summary>
        private void addAutomaticUpdate()
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
        }

        public void PopulateTrack()
        {
            if (!_populationBlock)
            {
                _populationBlock = true;
                _redLineDataBackup = _redLineData;
                _greenLineDataBackup = _greenLineData;

                _redLineData = new LineData(_env.TrackModel.requestTrackGrid(0), _env);
                _greenLineData = new LineData(_env.TrackModel.requestTrackGrid(1), _env);

                AddTrainsToTrack();

                _populationBlock = false;
            }
        }

        public void AddTrainsToTrack()
        {
            var trains = new List<ITrainModel>(_env.AllTrains);

            foreach (LayoutCellDataContainer c in _redLineData.Layout)
            {
                if (c.Block != null)
                {
                    foreach (ITrainModel t in trains)
                    {
                        if (t.CurrentBlock.BlockID == c.Block.BlockID)
                        {
                            c.Tile = Resources.RedTrack_Train;
                            c.Train = t;
                        }
                    }
                }
            }

            foreach (LayoutCellDataContainer c in _greenLineData.Layout)
            {
                if (c.Block != null)
                {
                    foreach (ITrainModel t in trains)
                    {
                        if (t.CurrentBlock.BlockID == c.Block.BlockID)
                        {
                            c.Tile = Resources.GreenTrack_Train;
                            c.Train = t;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     logic to determine which track line
        /// </summary>
        /// <param name="request">Request to process</param>
        /// <returns></returns>
        private int determineLine(IRequest request)
        {
            //red=0....green=1
            if (request.Block != null)
            {
                return determineLine(request.Block);
            }

            return determineLine(request.TrainRoute);
        }

        private int determineLine(IRoute route)
        {
            if (route == null)
            {
                return -1;
            }
            return route.RouteID;
        }

        private int determineLine(IBlock block)
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

        /// <summary>
        ///     Function to return line data to gui
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public LineData getLine(int line)
        {
            if (line == 0)
            {
                if (_populationBlock)
                {
                    return _greenLineDataBackup;
                }
                else
                {
                    return _redLineData;
                }
            }
            else if (line == 1)
            {
                if (_populationBlock)
                {
                    return _greenLineDataBackup;
                }
                else
                {
                    return _greenLineData;
                }
            }

            return null;
        }

        #region Request Abstractions

        //TODO verify BLOCKs
        public void dispatchTrainRequest(IRoute route)
        {
            int line = determineLine(route);
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
                _env.sendLogEntry("CTCOffice: INVALID ROUTE IN DISPATCH TRAIN REQUEST");
            }

            //change block from null to yard
            IRequest r = new Request(RequestTypes.DispatchTrain, id, 0, 0, 0, route, route.EndBlock);

            _requestsOut.Enqueue(r);
            RequestQueueOut(this, EventArgs.Empty);
        }

        public void setTrainOutOfServiceRequest(int trainID, int trackControllerID, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.SetTrainOOS, trackControllerID, trainID, 0, 0, null, block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        public void assignTrainRouteRequest(int trainID, int trackControllerID, IRoute route, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.AssignTrainRoute, trackControllerID, trainID, 0, 0, route,
                                             block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        public void setTrainAuthorityRequest(int trainID, int trackControllerID, int authority, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.SetTrainAuthority, trackControllerID, trainID, authority, 0,
                                             null, block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        public void closeTrackBlockRequest(int trackControllerID, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.TrackMaintenanceClose, trackControllerID, 0, 0, 0, null, block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        public void openTrackBlockRequest(int trackControllerID, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.TrackMaintenanceOpen, trackControllerID, 0, 0, 0, null, block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        public void setTrainSpeedRequest(int trainID, int trackControllerID, double speed, IBlock block)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.SetTrainSpeed, trackControllerID, trainID, 0, speed, null,
                                             block));
            RequestQueueOut(this, EventArgs.Empty);
        }

        public void trackControllerDataRequest(int trackControllerID)
        {
            _requestsOut.Enqueue(new Request(RequestTypes.TrackControllerData, trackControllerID, 0, 0, 0, null, null));
            RequestQueueOut(this, EventArgs.Empty);
        }

        #endregion

        #endregion

        #region Public Interface

        public event EventHandler<EventArgs> StartAutomation;

        public event EventHandler<EventArgs> StopAutomation;

        /// <summary>
        ///     Pass request from System Scheduler to Track Controller via send request
        /// </summary>
        /// <param name="request"></param>
        public void passRequest(IRequest request)
        {
            //add request to queue to send to Track Controller
            //while the scheduler is on, the CTCOffice is (blocked/unblocked?)
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

        //confirm merge
    }
}