using System;
using System.Collections.Generic;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrackController : ITrackController
    {
        private readonly List<IBlock> _blocks;

        private readonly int _id;
        private readonly List<IRoute> _routes;
        private readonly List<ITrainModel> _trains;

        public TestingTrackController(int id)
        {
            _id = id;
            _trains = new List<ITrainModel>();
            _blocks = new List<IBlock>();
            _routes = new List<IRoute>();
        }

        public IRequest Request
        {
            set { handleRequest(value); }
        }

        public int ID
        {
            get { return _id; }
        }

        public ITrackController Previous
        {
            get { return this; }
            set
            {
                //do nothing
            }
        }

        public ITrackController Next
        {
            get { return this; }
            set
            {
                //do nothing
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
            get { return _routes; }
        }

        public void LoadPlcProgram(string filename)
        {
            //not used for CTC Testing. Required for interface
        }

        public event EventHandler<RequestEventArgs> RequestRec;

        private void handleRequest(IRequest value)
        {
            if (RequestRec != null)
            {
                RequestRec(this, new RequestEventArgs(value));
            }
        }

        public void Recieve(object data)
        {
            //not used for CTC Testing. Required for interface
        }
    }
}