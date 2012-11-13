using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace TerminalVelocity
{
    class Route
    {
        #region Private Class Variables
        private RouteTypes _routeType;
        private IBlock _endBlock;
        private int _routeID;
        private List<IBlock> _routeBlocks;
        #endregion

        #region Constructor
        public Route(RouteTypes rt, IBlock end, int ID, List<IBlock> blocks)
        {
            _routeType = rt;
            _endBlock = end;
            _routeID = ID;
            _routeBlocks = blocks;
        }
        #endregion

        #region Public Properties
        public RouteTypes RouteType
        {
            get { return _routeType; }
        }

        public IBlock EndBlock
        {
            get { return _endBlock; }
        }

        public int RouteID
        {
            get { return _routeID; }
        }

        public List<IBlock> RouteBlocks
        {
            get { return _routeBlocks; }
        }
        #endregion
    }
}
