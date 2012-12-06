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

        /// <summary>
        /// A public constructor allowing the TrackModel or other modules to create RouteInfo objects.
        /// Though external moduels are allowed to create RouteInfo objects, the objects arent of any 
        /// inherent use, as the object is unreflected in the underlying database.
        /// </summary>
        /// <param name="rID">The route/line ID..0 or 1</param>
        /// <param name="rName">The route/line name: "Red" or "Green"</param>
        /// <param name="nBlocks">The number of different blocks in the line</param>
        /// <param name="blocks">An array of blocks in the line</param>
        /// <param name="sID">The ID of the first block in the line</param>
        /// <param name="eID">The ID of the final block in the line </param>
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

        /// <summary>
        /// A property to access the RouteID of the object: 0 for Red, 1 for Green
        /// </summary>
        public int RouteID
        {
            get { return _routeID; }
        }

        /// <summary>
        /// A property to access the RouteName of the object: either "Red" or "Green"
        /// </summary>
        public string RouteName
        {
            get { return _routeName; }
        }

        /// <summary>
        /// A property holding the number of blocks in the line
        /// </summary>
        public int NumBlocks
        {
            get { return _numBlocks; }
        }

        /// <summary>
        /// A property to access the ID of the first block in the line
        /// </summary>
        public int StartBlock
        {
            get { return _startBlockID; }
        }

        /// <summary>
        /// A property to access id of the end block in the line
        /// </summary>
        public int EndBlock
        {
            get { return _endBlockID; }
        }

        /// <summary>
        /// A property to access the array holding all blocks in the line
        /// </summary>
        public IBlock[] BlockList
        {
            get { return _blockList; }
        }

        #endregion
    }
}