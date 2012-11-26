using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrackController : ITrackController
    {
        public event EventHandler<RequestEventArgs> RequestRec;

        private List<ITrainModel> _trains;
        private List<IBlock> _blocks;
        private List<IRoute> _routes;

        private int _id;
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

        private void handleRequest(IRequest value)
        {
            RequestRec(this, new RequestEventArgs(value));
        }

        public int ID
        {
            get { return _id; }
        }

        public ITrackController Previous
        {
            get
            {
                return this;
            }
            set
            {
                //do nothing
            }
        }

        public ITrackController Next
        {
            get
            {
                return this;
            }
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

        public void Recieve(object data)
        {
            //not used for CTC Testing. Required for interface
        }

        public void LoadPLCProgram(string filename)
        {
            //not used for CTC Testing. Required for interface
        }
    }
}
