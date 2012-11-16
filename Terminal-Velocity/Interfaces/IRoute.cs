using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IRoute
    {
        /// <summary>
        /// Property that contains the type of route
        /// </summary>
        RouteTypes RouteType { get; }

        /// <summary>
        /// Property that contains the destination block given PointRoute
        /// </summary>
        IBlock EndBlock { get; }

        /// <summary>
        /// Property that contains the ID op the route (id of DB entry)
        /// </summary>
        int RouteID { get; }

        /// <summary>
        /// Property that contains a list of blocks contained by the route (given DefinedRoute)
        /// </summary>
        List<IBlock> RouteBlocks { get; }
    }

    /// <summary>
    /// Enumeration of possible route types
    /// </summary>
    public enum RouteTypes
    {
        PointRoute,
        DefinedRoute
    }
}
