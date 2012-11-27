﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public class RequestEventArgs : EventArgs
    {
        public IRequest _request;

        public RequestEventArgs(IRequest r)
        {
            _request = r;
        }

        public IRequest Request
        {
            get { return _request; }
            set { _request = value; }
        }
    }
}
