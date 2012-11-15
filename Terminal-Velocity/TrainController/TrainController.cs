using System;
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
        public ITrain Train
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
            // TODO: Definition of ITrain is incomplete


            //Random r = new Random(Train.getCurrentPassengerNumber(), Train.getMaxPassengers());
            //int newPassengers = r.Next();
            //Train.setCurrentPassengerNumber();
        }

        public void removePassengers()
        {
            throw new NotImplementedException();
        }

        public void lightsOn()
        {
            throw new NotImplementedException();
        }

        public void lightsOff()
        {
            throw new NotImplementedException();
        }

        public void returnFeedback(string Feedback)
        {
            throw new NotImplementedException();
        }

        public void doorOpen()
        {
            throw new NotImplementedException();
        }

        public void doorClose()
        {
            throw new NotImplementedException();
        }

        public void sendPower(double Power)
        {
            throw new NotImplementedException();
        }
    }
}
