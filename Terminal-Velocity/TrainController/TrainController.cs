using System;
using Interfaces;
using Utility;

namespace TrainController
{
    public class TrainController : ITrainController
    {
        private const int highestTemperature = 75;
        private const int lowestTemperature = 65;
        private int _announcement;
        private int _authorityLimit;
        private ISimulationEnvironment _environment;
        private string _log;
        private double _speedInput;
        private double _speedLimit = 40;
        private int _temperature;
        private double integral;

        public TrainController(ISimulationEnvironment env, ITrainModel tm)
        {
            _environment = env;
            _environment.Tick += _environment_Tick;
            TrainControllerUserInterface = null;
            Train = tm;
        }

        public TrainControllerUI TrainControllerUserInterface { get; set; }

        private ITrainModel Train { get; set; }


        public ISimulationEnvironment Environment
        {
            get { return _environment; }
            set { _environment = value; }
        }

        private IBlock CurrentBlock { get; set; }

        public string Log
        {
            get { return _log; }
            set { _log = value; }
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


        public int AuthorityLimit
        {
            get { return _authorityLimit; }
            set
            {
                _authorityLimit = value;
                returnFeedback("Authority limit set to " + value + "\r\n");
            }
        }

        public double SpeedLimit
        {
            get { return _speedLimit; }
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
                }
                else
                    returnFeedback("Speed not implemented because it was over the speed limit\r\n");
            }
        }


        public int Announcement
        {
            set { _announcement = value; }
        }

        public void addPassengers()
        {
            var r = new Random();
            int newPassengers = r.Next(Train.NumPassengers, Train.MaxCapacity + 1);
            Train.NumPassengers = newPassengers;
            //Send throughput afterwards
        }

        public void removePassengers()
        {
            var r = new Random();
            int newPassengers = r.Next(0, Train.NumPassengers + 1);
            Train.NumPassengers = newPassengers;
        }

        public void checkLightsOn()
        {
            Train.LightsOn = CurrentBlock.hasTunnel();
        }

        private void _environment_Tick(object sender, TickEventArgs e)
        {
            processTick();
        }

        private void processTick()
        {
            if (Train.CurrentVelocity < SpeedInput && Train.CurrentVelocity < SpeedLimit ||
                Train.CurrentVelocity > SpeedInput && Train.CurrentVelocity > SpeedLimit)
            {
                if (SpeedInput > SpeedLimit)
                    sendPower(SpeedLimit);
                if (SpeedInput <= SpeedLimit)
                    sendPower(SpeedInput);
            }
            /*if (!CurrentBlock.Equals(Train.currentBlock))
            {
                AuthorityLimit--;
                CurrentBlock = Train.currentBlock;
                if(checkStationNearby()) {
                    start slowing down each tick;
                    }
            } */
        }


        //private bool checkStationNearby()
        //{
        //    for(int i = 0; i<authoritylimit; i++) {
        //      figure a way to fetch n blocks
        //          if(block.hasStation())
        //              return true
        //}

        public string getLog(string parameter)
        {
            string local = parameter;
            return local;
        }


        public void returnFeedback(string Feedback)
        {
            _environment.sendLogEntry(Feedback);
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

        private bool checkTemperature(int temp)
        {
            bool ret = temp <= highestTemperature && temp >= lowestTemperature;
            if (!ret)
                returnFeedback("Temperature invalid. Please inform a value between 65 and 75\r\n");
            return ret;
        }


        public void sendPower(double speed)
        {
            double _timeInterval = (double) Environment.getInterval()/1000;
            double finalPower = 0;
            double e = speed - Train.CurrentVelocity;
            e = e/(3.6*_timeInterval);
            integral += integral + e*_timeInterval;
            double kp = 2;
            double ki = 0.5;
            finalPower = ki*integral + kp*e;
            Train.ChangeMovement(finalPower);
            returnFeedback(finalPower + "kW of power sent to the engine\r\n");
        }

        private bool checkSpeedLimit(double speed)
        {
            return speed > SpeedLimit;
        }

        private bool checkDoorOpen()
        {
            bool ret = Train.CurrentVelocity == 0;
            if (!ret)
            {
                returnFeedback("Doors can't open because the train is in movement.");
            }
            else
            {
                returnFeedback("Doors opened.");
            }
            return ret;
        }

        public void EmergencyBrakes()
        {
            Train.EmergencyBrake();
        }
    }
}