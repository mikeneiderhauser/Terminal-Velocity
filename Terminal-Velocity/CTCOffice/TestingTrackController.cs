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

        private Dictionary<int, ITrain> _trains;
        private Dictionary<int, IBlock> _blocks;
        private Dictionary<int, IRoute> _routes;

        private int _id;
        public TestingTrackController(int id)
        {
            _id = id;
            _trains = new Dictionary<int, ITrain>();
            _blocks = new Dictionary<int, IBlock>();
            _routes = new Dictionary<int, IRoute>();
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

        public Dictionary<int, ITrain> Trains
        {
            get { return _trains; }
        }

        public Dictionary<int, IBlock> Blocks
        {
            get { return _blocks; }
        }

        public Dictionary<int, IRoute> Routes
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
