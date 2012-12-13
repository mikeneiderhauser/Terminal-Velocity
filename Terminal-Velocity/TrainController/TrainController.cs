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
        private String[] _announcements = { "Approaching station A", "Station A", "Approaching station B", "Station B" };
        private int _authorityLimit;
        private double _speedLimit;
        private double _speedInput;
        private int _announcement;
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

            if (AuthorityLimit == 0)
            {
                SpeedInput = 0;
            }
            if (_distanceToStation < 5 && _currentBlock != null && !_currentBlock.hasStation())
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
                    _speedInput = value;
                    integral = 0;
                    sendPower(value);
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
                returnFeedback(_announcements[value]);
            }
            get
            {
                return _announcement;
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
