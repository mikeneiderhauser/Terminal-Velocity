using System;
using System.Collections.Generic;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrackController : ITrackController
    {
        private readonly List<IBlock> _blocks;

        private readonly int _id;

        public TestingTrackController(int id)
        {
            _id = id;
        }

        public IRequest Request
        {
            set 
            {
                if (TransferRequest != null)
                {
                    TransferRequest(this, new RequestEventArgs(value));
                }
            }
        }

        public int ID
        {
            get { return _id; }
        }

        public ITrackController Previous
        {
            get
            {
                return null;
            }
            set
            {
                //do nothing for ctc simulation
            }
        }

        public ITrackController Next
        {
            get
            {
                return null;
            }
            set
            {
                //do nothing for ctc simulation
            }
        }

        public List<ITrainModel> Trains
        {
            get { return null; }
        }

        public List<IBlock> Blocks
        {
            get { return null; }
        }

        public List<IRoute> Routes
        {
            get { return null; }
        }

        public void LoadPLCProgram(string filename)
        {
            //
        }

        public event EventHandler<EventArgs> TransferRequest;
    }
}