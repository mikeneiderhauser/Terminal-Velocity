using System;
using System.Collections.Generic;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrackController : ITrackController
    {
        private int _id;
        private List<IBlock> _blocks;
        private List<ITrainModel> _trains;

        public TestingTrackController(int id)
        {
            _id = id;
            _blocks = new List<IBlock>();
            _trains = new List<ITrainModel>();
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
            get { return _trains; }
        }

        public List<IBlock> Blocks
        {
            get { return _blocks; }
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