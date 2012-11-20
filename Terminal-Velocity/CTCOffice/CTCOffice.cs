﻿using System;
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
        private ISimulationEnvironment _env;
        private ITrackController _primaryTrackControllerRed;
        private LineData _redLineData;
        private ITrackController _primaryTrackControllerGreen;
        private LineData _greenLineData;
        private Operator _op;

        private Queue<IRequest> _requestsOut;
        private Queue<IRequest> _requestsIn;
        private event EventHandler<EventArgs> RequestQueueOut;
        private event EventHandler<EventArgs> RequestQueueIn;
        private bool _processingOutRequests;
        private bool _processingInRequests;
        #endregion

        #region Constructor
        public CTCOffice(ISimulationEnvironment env, ITrackController redTC, ITrackController greenTC)
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

            if (_env.TrackModel != null)
            {
                _redLineData = new LineData(_env.TrackModel.requestTrackGrid(0),0);
                _greenLineData = new LineData(_env.TrackModel.requestTrackGrid(1),1);
            }
            else
            {
                _env.sendLogEntry("CTCOffice: NULL Referenct to TrackModel");
            }

            //create queues
            _requestsOut = new Queue<IRequest>();
            _requestsIn = new Queue<IRequest>();
            
            //create queue events
            RequestQueueIn += new EventHandler<EventArgs>(CTCOffice_RequestQueueIn);
            RequestQueueOut += new EventHandler<EventArgs>(CTCOffice_RequestQueueOut);

            //create queue processing flags / mutex
            _processingOutRequests = false;
            _processingInRequests = false;

            //get status from red and green prrimary track controllers (default)
            _requestsOut.Enqueue(new Request(RequestTypes.TrackControllerData,_primaryTrackControllerRed.ID,-1,-1,-1,null,null));
            RequestQueueOut(this, EventArgs.Empty);
            _requestsOut.Enqueue(new Request(RequestTypes.TrackControllerData, _primaryTrackControllerGreen.ID, -1, -1,-1, null, null));
            RequestQueueOut(this, EventArgs.Empty);
        }

        /// <summary>
        /// Function to handle queue out
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
                    sendRequest(_requestsOut.Dequeue());
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
        /// Function to handle Queue In
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
                    if ((_requestsIn.Peek()).Info != null)
                    {
                        //if valid return data
                        internalRequest(_requestsIn.Dequeue());
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
                    RequestQueueOut(this, EventArgs.Empty);
                }
            }
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
            bool status = false;
            _op.login(username, password);
            status = _op.isAuth();
            if (status)
            {
                _env.sendLogEntry((string)"CTCOffice: User Logged in with username->" + username + ".");
            }
            else
            {
                _env.sendLogEntry("CTCOffice: User Logged out.");
            }
            return status;
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
        /// Function to determine if Operator is authorized to use CTC
        /// </summary>
        /// <returns></returns>
        public bool isAuth()
        {
            return _op.isAuth();
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
        /// Function to handle return request
        /// </summary>
        /// <param name="request"></param>
        private void internalRequest(IRequest request)
        {
            //handle request property (Status)Info here
        }

        /// <summary>
        /// Do Processing on Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _env_Tick(object sender, TickEventArgs e)
        {
            addAutomaticUpdate();
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
        /// Function to return line data to gui
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public LineData getLine(int line)
        {
            if (line == 0)
            {
                return _redLineData;
            }
            else if (line ==1)
            {
                return _greenLineData;
            }

            return null;
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
                RequestQueueOut(this, EventArgs.Empty);
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
                //throws event to actually process queue
                RequestQueueIn(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
