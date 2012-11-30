using Interfaces;

namespace TrackModel
{
    public class RouteInfo : IRouteInfo
    {
        //Private parameters
        private readonly Block[] _blockList;
        private readonly int _endBlockID;
        private readonly int _numBlocks;
        private readonly int _routeID;
        private readonly string _routeName;
        private readonly int _startBlockID;

        public RouteInfo(int rID, string rName, int nBlocks, Block[] blocks, int sID, int eID)
        {
            _routeID = rID;
            _routeName = rName;
            _numBlocks = nBlocks;
            _blockList = blocks;
            _startBlockID = sID;
            _endBlockID = eID;
        }

        #region Properties

        public int RouteID
        {
            get { return _routeID; }
        }

        public string RouteName
        {
            get { return _routeName; }
        }

        public int NumBlocks
        {
            get { return _numBlocks; }
        }

        public int StartBlock
        {
            get { return _startBlockID; }
        }

        public int EndBlock
        {
            get { return _endBlockID; }
        }

        public IBlock[] BlockList
        {
            get { return _blockList; }
        }

        #endregion
    }
}