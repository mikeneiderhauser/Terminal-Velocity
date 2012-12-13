using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;

namespace TrainController
{
    public class TrainController : ITrainController
    {
        #region Global variables
        private TrainControllerUI _tcGUI;
        private ISimulationEnvironment _environment;
        private ITrainModel _train;
        private IBlock _currentBlock;
        private Dictionary<int, string> _announcements;
        //private String[] _announcements = { "Approaching station A", "Station A", "Approaching station B", "Station B" };
        private int _authorityLimit;
        private double _speedLimit;
        private double _speedInput;
        private int _announcement;
        private bool _passengersFlag;
        private int _distanceToStation;
        private int _temperature;
        private string _log;
        private double integral = 0;
        #endregion


        #region Constant values
        private const double MaxValue = 110000;
        #endregion

        public TrainController(ISimulationEnvironment env, ITrainModel tm)
        {
            _environment = env;
            _environment.Tick += _environment_Tick;
            _tcGUI = null;
            Train = tm;
            _announcements = new Dictionary<int, string>();
            _announcements.Add(8, "Arrived at Shadyside Station\r\n");
            _announcements.Add(16, "Arrived at Herron Station\r\n");
            _announcements.Add(21, "Arrived at Swissville Station\r\n");
            _announcements.Add(25, "Arrived at Penn Station\r\n");
            _announcements.Add(35, "Arrived at Steel Plaza Station\r\n");
            _announcements.Add(45, "Arrived at First Avenue Station\r\n");
            _announcements.Add(60, "Arrived at South Hills Junction\r\n");
            _announcements.Add(2, "Arrived at Pioneer Station\r\n");
            _announcements.Add(9, "Arrived at Edgebrook Station\r\n");
            _announcements.Add(22, "Arrived at Whited Station\r\n");
            _announcements.Add(31, "Arrived at South Bank Station\r\n");
            _announcements.Add(39, "Arrived at Central Station\r\n");
            _announcements.Add(48, "Arrived at Inglewood Station\r\n");
            _announcements.Add(57, "Arrived at Overbrook Station\r\n");
            _announcements.Add(65, "Arrived at Glenbury Junction\r\n");
            _announcements.Add(73, "Arrived at Dormont Station\r\n");
            _announcements.Add(77, "Arrived at Mt. Lebanon Station\r\n");
            _announcements.Add(96, "Arrived at Castle Shannon Station\r\n");
            _announcements.Add(105, "Arrived at Dormont Station\r\n");
            _announcements.Add(114, "Arrived at Glenbury Junction\r\n"); 
            _announcements.Add(123, "Arrived at Overbrook Station\r\n");
            _announcements.Add(132, "Arrived at Inglewood Station\r\n");
            _announcements.Add(141, "Arrived at Central Station\r\n");
       
        }

        void _environment_Tick(object sender, Utility.TickEventArgs e)
        {
            processTick();
        }

        public TrainControllerUI TrainControllerUserInterface
        {
            get { return _tcGUI; }
            set { _tcGUI = value; }
        }

        private ITrainModel Train
        {
            get { return _train; }
            set { _train = value; }
        }


        private const int highestTemperature = 75;
        private const int lowestTemperature = 65;

        public ISimulationEnvironment Environment
        {
            get { return _environment; }
            set { _environment = value; }
        }

        public int DistanceToStation
        {
            set { _distanceToStation = value; }
        }
        private void processTick()
        {
            if (CurrentBlock.hasStation())
            {
                loadPassengers();
            }

            if (AuthorityLimit <= 1)
            {
                SpeedInput = 0;
            }
            if (_distanceToStation < 5 && !_currentBlock.hasStation())
            {
                SpeedInput = 0;
            }
            
           
            if (Train.CurrentVelocity < SpeedInput && Train.CurrentVelocity < SpeedLimit || Train.CurrentVelocity > SpeedInput && Train.CurrentVelocity > SpeedLimit)
            {
                if (SpeedInput > SpeedLimit)
                    sendPower(SpeedLimit);
                if (SpeedInput <= SpeedLimit)
                    sendPower(SpeedInput);
            }
            if (CurrentBlock != null && !CurrentBlock.Equals(Train.CurrentBlock))
            {
                AuthorityLimit--;
                CurrentBlock = Train.CurrentBlock;
                checkLightsOn();
                
            } 

        }

        private void loadPassengers()
        {
            if (_passengersFlag && Train.CurrentVelocity == 0)
            {
                removePassengers();
                addPassengers();
                doorClose();
                _passengersFlag = false;
            }
         
        }


 

        public string getLog(string parameter)
        {
            string local = parameter;
            return local;
        }


        public int AuthorityLimit
        {
            get
            {
                return _authorityLimit;
            }
            set
            {
                _authorityLimit = value;
                returnFeedback("Authority limit set to " + value + "\r\n");
            }
        }

        

        public double SpeedLimit
        {
            get
            {
                return _speedLimit;
            }
            set
            {
                _speedLimit = value;
                SpeedInput = value;
                integral = 0;
                returnFeedback("Speed limit set to " + value + "\r\n");

            }
        }
        public double SpeedInput
        {
            get { return _speedInput; }
            set
            {
                if (!checkSpeedLimit(value))
                {
                    if (_speedInput != value)
                    {
                        _speedInput = value;
                        integral = 0;
                        sendPower(value);
                    }
                }
                else
                    returnFeedback("Speed not implemented because it was over the speed limit\r\n");
            }
        }


        private IBlock CurrentBlock
        {
            get
            {
                return _currentBlock;
            }
            set
            {
                _currentBlock = value;
                if (_currentBlock.hasStation())
                {
                    _passengersFlag = true;
                }
            }
        }

        public string Log
        {
            get { return _log; }
            set { _log = value; }
        }

        public int Announcement
        {
            set 
            { 
                _announcement = value;
                String feedback = "";
                _announcements.TryGetValue(value, out feedback);
                returnFeedback(feedback);
            }
            get
            {
                return _announcement;
            }
        }
        public int[] Announcements
        {
           
            get
            {
                return _announcements.Keys.ToArray();
            }
        }

        public void addPassengers()
        {
            if (checkDoorOpen())
            {
                Random r = new Random();
                int newPassengers = r.Next(Train.NumPassengers, Train.MaxCapacity + 1);
                returnFeedback((newPassengers - Train.NumPassengers) + " added to the train\r\n");
                Train.NumPassengers = newPassengers;
            }
                    }

        public void removePassengers()
        {
            if (checkDoorOpen())
            {
                Random r = new Random();
                int newPassengers = r.Next(0, Train.NumPassengers + 1);
                returnFeedback((Train.NumPassengers - newPassengers) + " removed from the train\r\n");
                Train.NumPassengers = newPassengers;
            }
        }

        public void checkLightsOn()
        {
            Train.LightsOn = CurrentBlock.hasTunnel();
            returnFeedback("Lights automatically turned on/off because of the presence/abscense of tunnel\r\n");
        }



        public void returnFeedback(string Feedback)
        {
            _environment.SendLogEntry(Feedback);
            if (_log.Length > 1000)
            {
                _log = "";
            }
            _log += Feedback;
            

        }

        public void doorOpen()
        {
            Train.DoorsOpen = checkDoorOpen();
            returnFeedback("Door opening command issued. \r\n\n");

        }

        public void doorClose()
        {
            Train.DoorsOpen = false;
            returnFeedback("Door closing command issued. \r\n\n");
        }

        public int Temperature
        {
            get { return _temperature; }
            set
            {
                if (checkTemperature(value))
                    _temperature = value;
            }
        }

        private bool checkTemperature(int temp)
        {
            bool ret = temp <= highestTemperature && temp >= lowestTemperature;
            if (!ret)
                returnFeedback("Temperature invalid. Please inform a value between 65 and 75\r\n");
            return ret;
        }


        public void sendPower(double speed)
        {

                double _timeInterval = (double)Environment.GetInterval() / 1000;
                double finalPower = 0;
               double speedInMetersPerSecond = (speed / 3.6);
                double e = speedInMetersPerSecond - Train.CurrentVelocity;
                integral += integral + e * _timeInterval;
                if (integral > MaxValue) integral = MaxValue;
                double kp = 200.0;
                double ki = 1;
                finalPower = ki * integral + kp * e;
                Train.ChangeMovement(finalPower);
                returnFeedback(finalPower + "W of power sent to the engine\r\n");
            
        }


        private bool checkSpeedLimit(double speed)
        {
            return speed > SpeedLimit;
        }
        private bool checkDoorOpen()
        {
            bool ret = Train.CurrentVelocity == 0;
            if(!ret)
            {
                returnFeedback("Doors can't open because the train is in movement.\r\n");
            }
            else
            {
                returnFeedback("Doors opened.\r\n");
            }
            return ret; 
        }
 
        public void LightsOn()
        {
            Train.LightsOn = true;
            returnFeedback("Lights turned on manually.\r\n");
        }

        public void LightsOff()
        {
            Train.LightsOn = false;
            returnFeedback("Lights turned off manually.\r\n");
        }

        public void EmergencyBrakes()
        {
            Train.EmergencyBrake();
        }




    }

}
