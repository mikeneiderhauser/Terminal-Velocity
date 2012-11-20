﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;

namespace TrainController
{
    public class TrainController : ITrainController
    {
        public IEnvironment _environment
        {
            get { return _environment; }
            set { _environment = value; }
        }
        public ITrainModel Train
        {
            get { return Train; }
        }

        public List<IBlock> AuthorityBlocks
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
        private double SpeedInput
        {
            get { return SpeedInput; }
            set {if(  checkSpeedLimit())
                SpeedInput = value; }
        }


        public IBlock CurrentBlock
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
        public static ITrainController makeTrainController()
        {
            return new TrainController();
        }
        public void removePassengers()
        {
            Random r = new Random();
            int newPassengers = r.Next(0, Train.NumPassengers + 1);
            Train.NumPassengers = newPassengers;

        }

        public void lightsOn()
        {
            Train.LightsOn = true;
        }

        public void lightsOff()
        {
            Train.LightsOn = false;
        }

        public void returnFeedback(string Feedback)
        {
            throw new NotImplementedException();
        }

        public void doorOpen()
        {
            Train.DoorsOpen = true;

        }

        public void doorClose()
        {
            Train.DoorsOpen = false;
        }

        public void sendPower(double speed)
        {
            throw new NotImplementedException();
        }
        private bool checkSpeedLimit()
        {
            return SpeedInput <= SpeedLimit;
        }
        private bool checkDoorOpen()
        {
            return Train.CurrentVelocity == 0;
        }
        private void checkLightsOn()
        {
            Train.LightsOn = CurrentBlock.hasTunnel();
        }
        public void EmergencyBrakes()
        {
            Train.EmergencyBrake();
        }

    }

}
