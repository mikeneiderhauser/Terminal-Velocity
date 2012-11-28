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
        private IRoute _route;

        public RoutingToolEventArgs(IRoute route)
        {
            _route = route;
        }

        public IRoute Route
        {
            get { return _route; }
            set { _route = value; }
        }

    }
}
