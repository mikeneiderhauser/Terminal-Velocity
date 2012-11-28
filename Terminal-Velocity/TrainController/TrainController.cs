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

        public TrainController(ISimulationEnvironment env)
        {
            _environment = env;
            _environment.Tick += _environment_Tick;
            _tcGUI = null;
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
            get { return Train; }
            set { Train = value; }
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
            
        }


        //private bool checkBlockHasEnded()
        //{
        //    Train.
        //}
        private List<IBlock> AuthorityBlocks
        {
            get
            {
                return AuthorityBlocks;
            }
            set
            {
                AuthorityBlocks = value;
            }
        }

        public int AuthorityLimit
        {
            get
            {
                return AuthorityLimit;
            }
            set
            {
                AuthorityLimit = value;
            }
        }

        public double SpeedLimit
        {
            get
            {
                return SpeedLimit;
            }
            set
            {
                SpeedLimit = value;
              
            }
        }
        public double SpeedInput
        {
            get { return SpeedInput; }
            set 
            {
                if (!checkSpeedLimit(value))
                    SpeedInput = value;
                else
                    returnFeedback("Speed not implemented because it was over the speed limit");
            }
        }


        private IBlock CurrentBlock
        {
            get
            {
                return CurrentBlock;
            }
            set
            {
                CurrentBlock = value;
            }
        }

        public int Announcement
        {
            set { Announcement = value; }
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
            set { 
                if(checkTemperature(value))
                Temperature = value;
            }
        }

        private bool checkTemperature(int temp)
        {
            return temp <= highestTemperature && temp >= lowestTemperature; 
        }


        public void sendPower(double speed)
        {
           //CHECK PAPERS ASAP
            double e = speed - Train.CurrentVelocity;
            //double kp = 0.25;
            //double ki = 1;
            //need annotations
            
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
