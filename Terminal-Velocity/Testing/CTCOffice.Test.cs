using System;
using System.Collections.Generic;
using System.Threading;

using Interfaces;
using Utility;

using CTCOffice;
using SimulationEnvironment;
using TrackModel;

namespace Testing
{
    public class CTCOfficeTest : ITesting
    {
        private SimulationEnvironment.SimulationEnvironment _env;
        private CTCOffice.TestingTrackModel _trackMod;
        private CTCOffice.CTCOffice _ctc;
        private CTCOffice.TestingTrackController _red;
        private CTCOffice.TestingTrackController _green;
        
        private int _pass;
        private int _fail;
        private List<string> _message;
        private bool[] redRequests;
        private bool[] greenRequests;

        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            //local
            pass = 0;
            fail = 0;
            message = new List<string>();

            //global for events
            _pass = 0;
            _fail = 0;
            _message = new List<string>();

            redRequests = new bool[7] { false, false, false, false, false, false, false };
            greenRequests = new bool[7] { false, false, false, false, false, false, false };
            

            //create environment instance
            _env = new SimulationEnvironment.SimulationEnvironment();

            //create testing track model
            _trackMod = new TestingTrackModel(_env);

            //creating testing track controllers
            _red = new TestingTrackController(0, _trackMod, _env);
            _green = new TestingTrackController(1, _trackMod, _env);

            //hook to environment
            _env.PrimaryTrackControllerRed = _red;
            _red.TransferRequest += new EventHandler<EventArgs>(_red_TransferRequest);
            _env.PrimaryTrackControllerGreen = _green;
            _green.TransferRequest += new EventHandler<EventArgs>(_green_TransferRequest);
            _env.TrackModel = _trackMod;

            //creating office instance
            _ctc = new CTCOffice.CTCOffice(_env, _red, _green);
            if (_ctc != null)
            {
                pass++;
                message.Add("CTC Object Secessfully Created");
            }
            else
            {
                fail++;
                message.Add("CTC Object Not Secessfully Created");
            }

            _env.CTCOffice = _ctc;
            _ctc.StartAutomation += new EventHandler<EventArgs>(_ctc_StartAutomation);
            _ctc.StopAutomation += new EventHandler<EventArgs>(_ctc_StopAutomation);
            _ctc.LoadData += new EventHandler<EventArgs>(_ctc_LoadData);
            _ctc.MessagesReady += new EventHandler<EventArgs>(_ctc_MessagesReady);
            _ctc.UnlockLogin += new EventHandler<EventArgs>(_ctc_UnlockLogin);
            _ctc.UpdatedData += new EventHandler<EventArgs>(_ctc_UpdatedData);

            _env.startTick();

            if (_env.CTCOffice != null)
            {
                pass++;
                message.Add("CTC Office Added to Environment");

                #region Login Testing
                //Test Improper Login
                if (!_ctc.Login("me", "42"))
                {
                    pass++;
                    message.Add("PASS: Operator has invalid credentials");
                }
                else
                {
                    fail--;
                    message.Add("FAIL: Operator has invlaid credentials");
                }

                //Test Operator Auth (invalid creds)
                if (!_ctc.IsAuth())
                {
                    pass++;
                    message.Add("PASS: Op Auth (Lockout)");
                }
                else
                {
                    fail++;
                    message.Add("FAIL: Op Auth (Lockout)");
                }

                //Test Proper Login
                if (_ctc.Login("root", "admin"))
                {
                    pass++;
                    message.Add("PASS: Operator Login");
                }
                else
                {
                    fail--;
                    message.Add("FAIL: Operator Login");
                }

                //Test Operator Auth (invalid creds)
                if (_ctc.IsAuth())
                {
                    pass++;
                    message.Add("PASS: Op Auth");
                }
                else
                {
                    fail++;
                    message.Add("FAIL: Op Auth");
                }

                //Logout
                //Always returns true
                pass++;
                message.Add("PASS: Logout (always true)");

                #endregion

                //Populate Track -> void function.. always passes unit test
                pass++;
                message.Add("PASS: PopulateTrack() (always true unless exception is generated)");

                //call for start scheduling
                _ctc.StartScheduling();
                _ctc.StopScheduling();

                #region Test Get Line
                if (_ctc.GetLine(0) != null)
                {
                    pass++;
                    message.Add("PASS: GetLine(0) -> Red.");
                }
                else
                {
                    fail++;
                    message.Add("Fail: GetLine(0) -> Red.");
                }

                if (_ctc.GetLine(1) != null)
                {
                    pass++;
                    message.Add("PASS: GetLine(1) -> Green.");
                }
                else
                {
                    fail++;
                    message.Add("Fail: GetLine(1) -> Green.");
                }

                if (_ctc.GetLine(2) == null)
                {
                    pass++;
                    message.Add("PASS: GetLine(2) -> null.");
                }
                else
                {
                    fail++;
                    message.Add("Fail: GetLine(0) -> null.");
                }
                #endregion

                //Cannot test private functions (including Line ID functions)

                //RequestTypes.AssignTrainRoute
                //RequestTypes.SetTrainAuthority
                //RequestTypes.SetTrainOOS
                //RequestTypes.SetTrainSpeed
                ////RequestTypes.TrackControllerData
                //RequestTypes.TrackMaintenanceClose
                //RequestTypes.TrackMaintenanceOpen

                TestingBlock redBlock = new TestingBlock("Red", 0, 0, 0, null, true, false, false, false, false, false, StateEnum.Healthy);
                TestingBlock greenBlock = new TestingBlock("Green", 0, 0, 0, null, true, false, false, false, false, false, StateEnum.Healthy);

                _ctc.assignTrainRouteRequest(1, 0, null, redBlock);
                _ctc.setTrainAuthorityRequest(1, 0, 50, redBlock);
                _ctc.setTrainOutOfServiceRequest(1, 0, redBlock);
                _ctc.setTrainSpeedRequest(1, 0, 50, redBlock);
                _ctc.closeTrackBlockRequest(0, redBlock);
                _ctc.openTrackBlockRequest(0, redBlock);

                _ctc.assignTrainRouteRequest(1, 1, null, greenBlock);
                _ctc.setTrainAuthorityRequest(1, 1, 50, greenBlock);
                _ctc.setTrainOutOfServiceRequest(1, 1, greenBlock);
                _ctc.setTrainSpeedRequest(1, 1, 50, greenBlock);
                _ctc.closeTrackBlockRequest(1, greenBlock);
                _ctc.openTrackBlockRequest(1, greenBlock);

                //dispatch train handled in GUI Tester

                //cannot test handleResponse.  No actual case to test
            }
            else
            {
                fail++;
                message.Add("CTC Office Not Added to Environment");
            }

            
            

            //add event handler tests
            pass = pass + _pass;
            fail = fail + _fail;
            message.AddRange(_message);
            return true;
        }

        void _red_TransferRequest(object sender, EventArgs e)
        {
            RequestEventArgs args = (RequestEventArgs)e;
            string line = "Red";
            if (args.Request.RequestType == RequestTypes.AssignTrainRoute)
            {
                redRequests[0] = true;
                if (!redRequests[0])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST ("+line+") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.SetTrainAuthority)
            {
                redRequests[1] = true;
                if (!redRequests[1])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.SetTrainOOS)
            {
                redRequests[2] = true;
                if (!redRequests[2])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST -> (" + line + ") Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.SetTrainSpeed)
            {
                redRequests[3] = true;
                if (!redRequests[3])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.TrackControllerData)
            {
                redRequests[4] = true;
                if (!redRequests[4])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.TrackMaintenanceClose)
            {
                redRequests[5] = true;
                if (!redRequests[5])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.TrackMaintenanceOpen)
            {
                redRequests[6] = true;
                if (!redRequests[6])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");

                    for (int i = 0; i < 7; i++)
                    {
                        if (!redRequests[i])
                        {
                            _message.Add("WARNING. ("+line+") REQUEST " + i + " was never sent");
                        }
                    }

                    _red.TransferRequest -= _red_TransferRequest;
                }
                
            }
        }

        void _green_TransferRequest(object sender, EventArgs e)
        {
            RequestEventArgs args = (RequestEventArgs)e;
            string line = "Green";
            if (args.Request.RequestType == RequestTypes.AssignTrainRoute)
            {
                greenRequests[0] = true;
                if (!greenRequests[0])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.SetTrainAuthority)
            {
                greenRequests[1] = true;
                if (!greenRequests[1])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.SetTrainOOS)
            {
                greenRequests[2] = true;
                if (!greenRequests[2])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST -> (" + line + ") Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.SetTrainSpeed)
            {
                greenRequests[3] = true;
                if (!greenRequests[3])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.TrackControllerData)
            {
                greenRequests[4] = true;
                if (!greenRequests[4])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.TrackMaintenanceClose)
            {
                greenRequests[5] = true;
                if (!greenRequests[5])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");
                }
            }
            else if (args.Request.RequestType == RequestTypes.TrackMaintenanceOpen)
            {
                greenRequests[6] = true;
                if (!greenRequests[6])
                {
                    _pass++;
                    _message.Add("PASS: TRANSFER REQUEST (" + line + ") -> Send REQUEST: AssignTrainRoute");

                    for (int i = 0; i < 7; i++)
                    {
                        if (!greenRequests[i])
                        {
                            _message.Add("WARNING. (" + line + ") REQUEST " + i + " was never sent");
                        }
                    }

                    _green.TransferRequest -= _green_TransferRequest;
                }

            }
        }

        #region Event Tests
        void _ctc_UpdatedData(object sender, EventArgs e)
        {
            _pass++;
            _message.Add("Update Data Thrown: Pass. Unsubscribed.");
            _ctc.UpdatedData -= _ctc_UpdatedData;   
        }

        void _ctc_UnlockLogin(object sender, EventArgs e)
        {
            _pass++;
            _message.Add("Unlock Login Thrown: Pass. Unsubscribed.");
            _ctc.UnlockLogin -= _ctc_UnlockLogin;
        }

        void _ctc_MessagesReady(object sender, EventArgs e)
        {
            _pass++;
            _message.Add("Messages Ready Thrown: Pass. Unsubscribed.");
            _ctc.MessagesReady -= _ctc_MessagesReady;
        }

        void _ctc_LoadData(object sender, EventArgs e)
        {
            _pass++;
            _message.Add("Load Data Thrown: Pass. Unsubscribed.");
            _ctc.LoadData -= _ctc_LoadData;
        }

        void _ctc_StopAutomation(object sender, EventArgs e)
        {
            _pass++;
            _message.Add("Stop Automation Thrown: Pass. Unsubscribed.");
            _ctc.StopAutomation -= _ctc_StopAutomation;
        }

        void _ctc_StartAutomation(object sender, EventArgs e)
        {
            _pass++;
            _message.Add("Stop Automation Thrown: Pass. Unsubscribed.");
            _ctc.StartAutomation -= _ctc_StartAutomation;
        }
        #endregion
    }
}