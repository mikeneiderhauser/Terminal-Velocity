using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;

namespace TrainController
{
    public class TrainController : ITrainController
    {
        private TrainControllerUI _tcGUI;
        private ISimulationEnvironment _environment;
        private ITrainModel _train;
        private IBlock _currentBlock;
        private int _authorityLimit;
        private double _speedLimit;
        private double _speedInput;
        private int _announcement;


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


        private void processTick()
        {
            if (Train.CurrentVelocity < SpeedInput && Train.CurrentVelocity < SpeedLimit || Train.CurrentVelocity > SpeedInput && Train.CurrentVelocity > SpeedLimit)
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


        public int AuthorityLimit
        {
            get
            {
                return _authorityLimit;
            }
            set
            {
                _authorityLimit = value;
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

            }
        }
        public double SpeedInput
        {
            get { return _speedInput; }
            set
            {
                if (!checkSpeedLimit(value))
                    _speedInput = value;
                else
                    returnFeedback("Speed not implemented because it was over the speed limit");
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

        public int Announcement
        {
            set { _announcement = value; }
        }

        public void addPassengers()
        {

            Random r = new Random();
            int newPassengers = r.Next(Train.NumPassengers, Train.MaxCapacity + 1);
            Train.NumPassengers = newPassengers;
            //Send throughput afterwards
        }

        public void removePassengers()
        {
            Random r = new Random();
            int newPassengers = r.Next(0, Train.NumPassengers + 1);
            Train.NumPassengers = newPassengers;

        }

        public void checkLightsOn()
        {
            Train.LightsOn = CurrentBlock.hasTunnel();
        }



        public void returnFeedback(string Feedback)
        {
            _environment.sendLogEntry(Feedback);
            if (_tcGUI != null)
            {
                _tcGUI.RecLog(Feedback);
            }

        }

        public void doorOpen()
        {
            Train.DoorsOpen = checkDoorOpen();

        }

        public void doorClose()
        {
            Train.DoorsOpen = false;
        }

        public int Temperature
        {
            get { return Temperature; }
            set
            {
                if (checkTemperature(value))
                    Temperature = value;
            }
        }

        private bool checkTemperature(int temp)
        {
            return temp <= highestTemperature && temp >= lowestTemperature;
        }


        public void sendPower(double speed)
        {

            double e = speed - Train.CurrentVelocity;
            double kp = 1;
            double ki = 1;
            Train.ChangeMovement(kp * e);
            returnFeedback(kp * e + "kW of power sent to the engine");

        }
        private bool checkSpeedLimit(double speed)
        {
            return speed >= SpeedLimit;
        }
        private bool checkDoorOpen()
        {
            //write log here
            return Train.CurrentVelocity == 0;
        }

        public void EmergencyBrakes()
        {
            Train.EmergencyBrake();
        }




    }

}
