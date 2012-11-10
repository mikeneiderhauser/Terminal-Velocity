using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IRoute
    {
        RouteTypes RouteType { get; }
        IBlock EndBlock { get; }
        int RouteID { get; }
        List<IBlock> RouteBlocks { get; }
    }

    public enum RouteTypes
    {
        PointRoute,
        DefinedRoute
    }
}
