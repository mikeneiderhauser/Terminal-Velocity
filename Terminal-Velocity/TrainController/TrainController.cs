using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;

namespace TrainController
{
   public class TrainController : ITrainController
    {
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
            
            Random r = new Random(Train.getCurrentPassengerNumber(), Train.getMaxPassengers());
            int newPassengers = r.Next();
            Train.setCurrentPassengerNumber();
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
