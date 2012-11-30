using System;
using Interfaces;

namespace CTCOffice
{
    public class TestingSystemScheduler : ISystemScheduler
    {
        public IRequest GetRoute
        {
            get { throw new NotImplementedException(); }
        }
    }
}