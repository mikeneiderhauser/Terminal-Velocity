using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using Utility;

namespace TrackModel
{

    public class TrackModel : ITrackModel
    {
        //Private parameters
		private IEnvironment _env;
		private DBManager _dbManager;
		//private DisplayManager _dispManager;
		private DBCreator _dbCreator;
		

        public TrackModel(IEnvironment environment)
        {
		_env=environment;
		_dbCreator=new DBCreator("");
		_dbManager=new DBManager(_dbCreator.DBCon);

		//_environment.Tick+=
        }

	public IBlock requestBlockInfo(int blockID)
	{
		//Dont request patently invalid blocks
		if(blockID<0)
			return null;

		string blockQuery=_dbManager.createQueryString("BLOCK",blockID);

		//Check query return val
		if(blockQuery==null)
			return null;

		//Get data reader for query
		SqlDataReader queryReader=_dbManager.runQuery(blockQuery);

		//Check exec return val
		if(queryReader==null)
			return null;

		IBlock temp=_dbManager.formatBlockQueryResults(queryReader);
		return temp;
	}

	public IRouteInfo requestRouteInfo(int routeID)
	{
		return null;
	}

	public IBlock[,] requestTrackGrid(int routeID)
	{
		return null;
	}

	public bool requestUpdateSwitch(IBlock bToUpdate)
	{
		return false;
	}

	public bool requestUpdateBlock(IBlock bToUpdate)
	{
		return false;
	}
		
		
        //Handle environment tick
        void  _environment_Tick(object sender, TickEventArgs e)
        {
                //handle tick here
      	}
    }
}
