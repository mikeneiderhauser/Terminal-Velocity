using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public class RoutingToolEventArgs : EventArgs
    {
        private IBlock _block;
        private int _line;

        public RoutingToolEventArgs(IBlock b, int line)
        {
            _block = b;
            _line = line;
        }

        public IBlock Block
        {
            get { return _block; }
            set { _block = value; }
        }

        public int Line
        {
            get { return _line; }
            set { _line = value; }
        }
    }
}
