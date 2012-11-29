using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public class AuthorityToolEventArgs : EventArgs
    {
        private int _authority;

        public AuthorityToolEventArgs(int authority)
        {
            _authority = authority;
        }

        public int Authority
        {
            get { return _authority; }
            set { _authority = value; }
        }

    }
}
