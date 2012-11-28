using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public class TestingSystemScheduler : ISystemScheduler
    {
        public TestingSystemScheduler()
        {

        }

        public IRequest GetRoute
        {
            get { throw new NotImplementedException(); }
        }
    }
}
