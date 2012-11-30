using System;
using Interfaces;

namespace CTCOffice
{
    public class RoutingToolEventArgs : EventArgs
    {
        public RoutingToolEventArgs(IRoute route)
        {
            Route = route;
        }

        public IRoute Route { get; set; }
    }
}