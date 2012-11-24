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
        public TestingTrackController()
        {

        }

        public IRequest Request
        {
            set { throw new NotImplementedException(); }
        }

        public int ID
        {
            get { throw new NotImplementedException(); }
        }

        public ITrackController Previous
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

        public ITrackController Next
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

        public Dictionary<int, ITrain> Trains
        {
            get { throw new NotImplementedException(); }
        }

        public Dictionary<int, IBlock> Blocks
        {
            get { throw new NotImplementedException(); }
        }

        public Dictionary<int, IRoute> Routes
        {
            get { throw new NotImplementedException(); }
        }

        public void Recieve(object data)
        {
            throw new NotImplementedException();
        }

        public void LoadPLCProgram(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
