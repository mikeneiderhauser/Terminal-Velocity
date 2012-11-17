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
		private String _routeName;
		private int _numBlocks;
		private Block[] _blockList;
		private int _startBlockID;
		private int _endBlockID;
		private IEnvironment _env

        public RouteInfo(IEnvironment environment)
        {
		_env=environment;
            //_environment.Tick +=
        }
		
		
        //Handle environment tick
        void  _environment_Tick(object sender, TickEventArgs e)
        {
                //handle tick here
        }
		
		
		
        #region Properties
        public int RouteID
        {
            get { return _trainID; }
        }

	public int RouteName
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
