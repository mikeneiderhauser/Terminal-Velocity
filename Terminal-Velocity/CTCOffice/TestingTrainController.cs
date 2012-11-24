using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrainController : ITrainController
    {

        public TestingTrainController()
        {

        }

        public ITrain Train
        {
            get { throw new NotImplementedException(); }
        }

        public List<IBlock> AuthorityBlocks
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int AuthorityLimit
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public double SpeedLimit
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IBlock CurrentBlock
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Announcement
        {
            set { throw new NotImplementedException(); }
        }

        public void addPassengers()
        {
            throw new NotImplementedException();
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

        public void commit()
        {
            throw new NotImplementedException();
        }
    }
}
