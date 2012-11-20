using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using Utility;

namespace TrackModel
{

    public class RouteInfo : IRouteInfo
    {
        //Private parameters
		private int _routeID;
		private string _routeName;
		private int _numBlocks;
		private Block[] _blockList;
		private int _startBlockID;
		private int _endBlockID;

        public RouteInfo(int rID,string rName,int nBlocks,Block[] blocks, int sID, int eID)
        {
		_routeID=rID;
		_routeName=rName;
		_numBlocks=nBlocks;
		_blockList=blocks;
		_startBlockID=sID;
		_endBlockID=eID;
        }
		
        #region Properties
        public int RouteID
        {
            get { return _routeID; }
        }

	public string RouteName
	{
		get {return _routeName;}
	}

	public int NumBlocks
	{
		get {return _numBlocks;}
	}

	public int StartBlock
	{
		get {return _startBlockID;}
	}

	public int EndBlock
	{
		get {return _endBlockID;}
	}

	public IBlock[] BlockList
	{
		get {return _blockList;}
	}	
	#endregion
    }
}
