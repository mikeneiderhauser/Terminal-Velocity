using System;
using Interfaces;

namespace CTCOffice
{
    public class RoutingToolEventArgs : EventArgs
    {
        public RoutingToolEventArgs(IRoute route,IBlock block)
        {
            Route = route;
            Block = block;
        }

        public IRoute Route { get; set; }

        public IBlock Block { get; set; }
    }
}