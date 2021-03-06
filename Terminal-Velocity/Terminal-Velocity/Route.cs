﻿using System.Collections.Generic;
using Interfaces;

namespace TerminalVelocity
{
    internal class Route
    {
        #region Private Class Variables

        /// <summary>
        ///     Holds the destination block of the route
        /// </summary>
        private readonly IBlock _endBlock;

        /// <summary>
        ///     Holds the list of blocks contained by the route
        /// </summary>
        private readonly List<IBlock> _routeBlocks;

        /// <summary>
        ///     holds the DB ID of the route
        /// </summary>
        private readonly int _routeID;

        /// <summary>
        ///     Holds the route type
        /// </summary>
        private readonly RouteTypes _routeType;

        #endregion

        #region Constructor

        /// <summary>
        ///     Default Constructor of Route
        /// </summary>
        /// <param name="rt">Route Type</param>
        /// <param name="end">destination block given PointRoute</param>
        /// <param name="ID">DB ID of route given DefinedRoute</param>
        /// <param name="blocks">List of blocks contained by that route</param>
        public Route(RouteTypes rt, IBlock end, int ID, List<IBlock> blocks)
        {
            _routeType = rt;
            _endBlock = end;
            _routeID = ID;
            _routeBlocks = blocks;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Property that contains the type of route
        /// </summary>
        public RouteTypes RouteType
        {
            get { return _routeType; }
        }

        /// <summary>
        ///     Property that contains the destination block given PointRoute
        /// </summary>
        public IBlock EndBlock
        {
            get { return _endBlock; }
        }

        /// <summary>
        ///     Property that contains the ID op the route (id of DB entry)
        /// </summary>
        public int RouteID
        {
            get { return _routeID; }
        }

        /// <summary>
        ///     Property that contains a list of blocks contained by the route (given DefinedRoute)
        /// </summary>
        public List<IBlock> RouteBlocks
        {
            get { return _routeBlocks; }
        }

        #endregion
    }
}