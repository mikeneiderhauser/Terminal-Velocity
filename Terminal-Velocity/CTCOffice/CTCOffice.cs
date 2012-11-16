using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

/*
    See passRequest for clarification
*/

namespace CTCOffice
{
    public class CTCOffice : ICTCOffice
    {
        #region Private Variables
        private IEnvironment _env;
        private ITrackController _primaryTrackControllerRed;
        private LineData _redLineData;
        private ITrackController _primaryTrackControllerGreen;
        private LineData _greenLineData;
        private Queue<IRequest> _requestsOut;
        private Queue<IRequest> _requestsIn;
        private Operator _op;
        #endregion

        #region Constructor
        public CTCOffice(IEnvironment env, ITrackController redTC, ITrackController greenTC)
        {
            _env = env;
            _primaryTrackControllerGreen = greenTC;
            _primaryTrackControllerRed = redTC;

            //subscribe to Environment Tick
            _env.Tick += new EventHandler<TickEventArgs>(_env_Tick);

            //create new operator object
            _op = new Operator();
            //set credentials
            _op.setAuth("root", "admin");

            _redLineData = new LineData();
            _greenLineData = new LineData();

            _redLineData.Layout = _env.TrackModel.requestTrackGrid(0);
            //add 2D blocks to LineData (red)
            //add blocks to Line Data objects (red)

            _greenLineData.Layout = _env.TrackModel.requestTrackGrid(1);
            //add 2D blocks to LineData (green)
            //add blocks to Line Data objects (green)

            //create queues
            _requestsOut = new Queue<IRequest>();
            _requestsIn = new Queue<IRequest>();

            //get status from red and green prrimary track controllers (default)
            _requestsOut.Enqueue(new Request(RequestTypes.TrackControllerData,_primaryTrackControllerRed.ID,-1,-1,null,null));
            _requestsOut.Enqueue(new Request(RequestTypes.TrackControllerData, _primaryTrackControllerGreen.ID, -1, -1, null, null));
        }
        #endregion

        #region Functions

        /// <summary>
        /// Function to login the operator
        /// </summary>
        /// <param name="username">Passed in Operator Entered Username</param>
        /// <param name="password">Passed in Operator Entered Password</param>
        /// <returns>True if operator is logged in. Else False</returns>
        public bool Login(string username, string password)
        {
            _op.login(username, password);
            return _op.isAuth();
        }

        /// <summary>
        /// Function to log the operator out
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
        /// Function to throw the event to the System Scheduler to start automated scheduling
        /// </summary>
        public void StartScheduling()
        {
            if (StartAutomation != null)
            {
                StartAutomation(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Function to throw the event to the System Scheduler to start automated scheduling
        /// </summary>
        public void StopScheduling()
        {
            if (StopAutomation != null)
            {
                StopAutomation(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Sends a request to the primary track controller. Has logic to determine with primary track controller
        /// </summary>
        /// <param name="request">Request to send to track controller</param>
        public void sendRequest(IRequest request)
        {
            int line = determineLine(request);
            //figure out a way to determine line from block ID -- Waiting on Track Model

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
        /// Do Processing on Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _env_Tick(object sender, TickEventArgs e)
        {
            addAutomaticUpdate();
            //handle queues here
            processOut();
            processIn();
        }

        /// <summary>
        /// Calculates if the CTC should ask the track controllers for data.
        /// </summary>
        private void addAutomaticUpdate()
        {
            //cannot implement until environment implementation is expanded
        }

        /// <summary>
        /// logic to determine which track line
        /// </summary>
        /// <param name="request">Request to process</param>
        /// <returns></returns>
        private int determineLine(IRequest request)
        {
            //cannot implement without Track Model Interface
            //red=0....green=1
            return 0;
        }

        /// <summary>
        /// Processes the _requestIn Queue (handles 1 request at a time (1 request per tick))
        /// </summary>
        private void processIn()
        {
            int line = 0;
            IRequest removedRequest;
            if (_requestsIn.Count != 0)
            {
                //handle passing to SS if needed. (requuest is per track controller)
                removedRequest = _requestsIn.Dequeue();
                line = determineLine(removedRequest);

                if (line == 0)
                {
                    //red
                }
                else if (line == 1)
                {
                    //green
                }
            }
        }
        
        /// <summary>
        /// Processes the _requestOut Queue (handles 1 request at a time (1request per tick))
        /// </summary>
        private void processOut()
        {
            if (_requestsOut.Count != 0)
            {
                sendRequest(_requestsOut.Dequeue());
            }
        }
        
        #endregion

        #region Public Interface
        public event EventHandler<EventArgs> StartAutomation;

        public event EventHandler<EventArgs> StopAutomation;

        /// <summary>
        /// Pass request from System Scheduler to Track Controller via send request
        /// </summary>
        /// <param name="request"></param>
        public void passRequest(IRequest request)
        {
            //add request to queue to send to Track Controller
            //while the scheduler is on, the CTCOffice is (blocked/unblocked?)
            if (request != null)
            {
                _requestsOut.Enqueue(request);
            }
        }

        /// <summary>
        /// Sends Request to CTC for processing.  Will only process if request.Info is not null
        /// </summary>
        /// <param name="request">request to process</param>
        public void handleResponse(IRequest request)
        {
            //add request to queue for CTC to Process. CTC will only process if Info is not null
            if (request.Info != null)
            {
                _requestsIn.Enqueue(request);
            }
        }
        #endregion
    }
}
