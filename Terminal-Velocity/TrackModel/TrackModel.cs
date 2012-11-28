using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using System.Data.SQLite;
using System.Data.SqlClient;
using Utility;

namespace TrackModel
{

    public class TrackModel : ITrackModel
    {
        //Private parameters
		private DBManager _dbManager;
		private ISimulationEnvironment _env;
		//private DisplayManager _dispManager;
		private DBCreator _dbCreator;
		

        public TrackModel(ISimulationEnvironment environment)
        {
		_env=environment;
		_dbCreator=new DBCreator("");
		_dbManager=new DBManager(_dbCreator.DBCon);

		//_environment.Tick+=
        }

        //Last minute method to allow TrackModel to directly take input file.
        public bool provideInputFile(string fName)
        {
            int res=_dbCreator.parseInputFile(fName);
            //Console.WriteLine("Inside TrackModel, res was: " + res);
            if (res == 0)
                return true;
            else
                return false;
        }

	public IBlock requestBlockInfo(int blockID,string line)
	{
		//Dont request patently invalid blocks
		if(blockID<0)
			return null;

		string blockQuery=_dbManager.createQueryString("BLOCK",blockID,line);

		//Check query return val
		if(blockQuery==null)
			return null;

		//Get data reader for query
        SQLiteDataReader queryReader = _dbManager.runQuery(blockQuery);

		//Check exec return val
		if(queryReader==null)
			return null;

		IBlock temp=_dbManager.formatBlockQueryResults(queryReader);
		return temp;
	}

	public IRouteInfo requestRouteInfo(int routeID)
	{
		if(routeID!=0 && routeID!=1)
			return null;
		string routeQuery=_dbManager.createQueryString("ROUTE",routeID,null);
	
		//Check query return val
		if(routeQuery==null)
			return null;

		//Get data reader from query
        SQLiteDataReader queryReader = _dbManager.runQuery(routeQuery);
		
		//Check data reader return val
		if(queryReader==null)
			return null;

		IRouteInfo temp = _dbManager.formatRouteQueryResults(queryReader);
		return temp;
	}

	public IBlock[,] requestTrackGrid(int routeID)
	{
        if (routeID != 0 && routeID != 1)
        {
            //Console.WriteLine("Given invalid routeID in requestTrackGrid: "+routeID);
            return null;
        }

		IBlock[,] temp=new IBlock[46,38];

		//Initialize our array
		for(int i=0;i<46;i++)
		{
			for(int j=0;j<38;j++)
			{
				temp[i,j]=null;
			}
		}

		if(routeID==0)//means red
		{
			temp[10,28]=this.requestBlockInfo(1,"Red");//1
			temp[9,29]=this.requestBlockInfo(2,"Red");//2
			temp[8,29]=this.requestBlockInfo(3,"Red");//3
			temp[7,30]=this.requestBlockInfo(4,"Red");//4
			temp[7,31]=this.requestBlockInfo(5,"Red");//5
			temp[7,32]=this.requestBlockInfo(6,"Red");//6
			temp[7,33]=this.requestBlockInfo(7,"Red");//7
			temp[7,34]=this.requestBlockInfo(7,"Red");//7
			temp[7,35]=this.requestBlockInfo(8,"Red");//8
			temp[8,36]=this.requestBlockInfo(8,"Red");//8
			temp[9,36]=this.requestBlockInfo(9,"Red");//9
			temp[10,35]=this.requestBlockInfo(10,"Red");//10
			temp[10,34]=this.requestBlockInfo(11,"Red");//11
			temp[11,33]=this.requestBlockInfo(11,"Red");//11
			temp[11,32]=this.requestBlockInfo(12,"Red");//12
			temp[11,31]=this.requestBlockInfo(13,"Red");//13
			temp[11,30]=this.requestBlockInfo(13,"Red");//13
			temp[11,29]=this.requestBlockInfo(14,"Red");//14
			temp[11,28]=this.requestBlockInfo(14,"Red");//14
			temp[10,27]=this.requestBlockInfo(15,"Red");//15
			temp[10,26]=this.requestBlockInfo(16,"Red");//16
			temp[10,25]=this.requestBlockInfo(17,"Red");//17
			temp[10,24]=this.requestBlockInfo(18,"Red");//18
			temp[10,23]=this.requestBlockInfo(19,"Red");//19
			temp[10,22]=this.requestBlockInfo(20,"Red");//20
			temp[10,21]=this.requestBlockInfo(21,"Red");//21
			temp[11,20]=this.requestBlockInfo(22,"Red");//22
			temp[12,19]=this.requestBlockInfo(23,"Red");//23
			temp[13,19]=this.requestBlockInfo(24,"Red");//24
			temp[14,19]=this.requestBlockInfo(25,"Red");//25
			temp[15,19]=this.requestBlockInfo(26,"Red");//26
			temp[16,19]=this.requestBlockInfo(27,"Red");//27
			temp[17,19]=this.requestBlockInfo(28,"Red");//28
			temp[18,19]=this.requestBlockInfo(29,"Red");//29
			temp[19,19]=this.requestBlockInfo(30,"Red");//30
			temp[20,19]=this.requestBlockInfo(31,"Red");//31
			temp[21,19]=this.requestBlockInfo(32,"Red");//32
			temp[22,19]=this.requestBlockInfo(33,"Red");//33
			temp[23,19]=this.requestBlockInfo(34,"Red");//34
			temp[24,19]=this.requestBlockInfo(35,"Red");//35
			temp[25,19]=this.requestBlockInfo(36,"Red");//36
			temp[26,19]=this.requestBlockInfo(37,"Red");//37
			temp[27,19]=this.requestBlockInfo(38,"Red");//38
			temp[28,19]=this.requestBlockInfo(39,"Red");//39
			temp[39,19]=this.requestBlockInfo(40,"Red");//40
			temp[30,19]=this.requestBlockInfo(41,"Red");//41
			temp[31,19]=this.requestBlockInfo(42,"Red");//42
			temp[32,19]=this.requestBlockInfo(43,"Red");//43
			temp[33,19]=this.requestBlockInfo(44,"Red");//44
			temp[34,19]=this.requestBlockInfo(45,"Red");//45
			temp[35,19]=this.requestBlockInfo(46,"Red");//46
			temp[36,18]=this.requestBlockInfo(47,"Red");//47
			temp[37,17]=this.requestBlockInfo(47,"Red");//47
			temp[37,16]=this.requestBlockInfo(48,"Red");//48
			temp[38,15]=this.requestBlockInfo(49,"Red");//49
			temp[38,14]=this.requestBlockInfo(50,"Red");//50
			temp[38,13]=this.requestBlockInfo(50,"Red");//50
			temp[38,12]=this.requestBlockInfo(51,"Red");//51
			temp[38,11]=this.requestBlockInfo(52,"Red");//52
			temp[38,10]=this.requestBlockInfo(53,"Red");//53
			temp[38,9]=this.requestBlockInfo(53,"Red");//53
			temp[38,8]=this.requestBlockInfo(53,"Red");//53
			temp[38,7]=this.requestBlockInfo(54,"Red");//54
			temp[38,6]=this.requestBlockInfo(54,"Red");//54
			temp[37,5]=this.requestBlockInfo(55,"Red");//55
			temp[36,4]=this.requestBlockInfo(55,"Red");//55
			temp[35,3]=this.requestBlockInfo(56,"Red");//56
			temp[34,2]=this.requestBlockInfo(57,"Red");//57
			temp[33,2]=this.requestBlockInfo(57,"Red");//57
			temp[32,2]=this.requestBlockInfo(58,"Red");//58
			temp[31,2]=this.requestBlockInfo(58,"Red");//58
			temp[31,3]=this.requestBlockInfo(59,"Red");//59
			temp[30,4]=this.requestBlockInfo(59,"Red");//59
			temp[30,5]=this.requestBlockInfo(60,"Red");//60
			temp[30,6]=this.requestBlockInfo(60,"Red");//60
			temp[31,7]=this.requestBlockInfo(61,"Red");//61
			temp[32,7]=this.requestBlockInfo(61,"Red");//61
			temp[33,8]=this.requestBlockInfo(62,"Red");//62
			temp[34,8]=this.requestBlockInfo(63,"Red");//63
			temp[35,8]=this.requestBlockInfo(63,"Red");//63
			temp[36,8]=this.requestBlockInfo(64,"Red");//64
			temp[36,9]=this.requestBlockInfo(65,"Red");//65
			temp[37,10]=this.requestBlockInfo(66,"Red");//66
			temp[32,18]=this.requestBlockInfo(67,"Red");//67
			temp[32,17]=this.requestBlockInfo(67,"Red");//67
			temp[31,16]=this.requestBlockInfo(68,"Red");//68
			temp[30,16]=this.requestBlockInfo(69,"Red");//69
			temp[29,16]=this.requestBlockInfo(69,"Red");//69
			temp[28,16]=this.requestBlockInfo(70,"Red");//70
			temp[27,17]=this.requestBlockInfo(71,"Red");//71
			temp[27,18]=this.requestBlockInfo(71,"Red");//71
			temp[21,18]=this.requestBlockInfo(72,"Red");//72
			temp[21,17]=this.requestBlockInfo(72,"Red");//72
			temp[20,16]=this.requestBlockInfo(73,"Red");//73
			temp[19,16]=this.requestBlockInfo(74,"Red");//74
			temp[18,16]=this.requestBlockInfo(74,"Red");//74
			temp[17,16]=this.requestBlockInfo(75,"Red");//75
			temp[16,17]=this.requestBlockInfo(76,"Red");//76
			temp[16,18]=this.requestBlockInfo(76,"Red");//76
		}
		else//routeID==1 means green
		{

		}

        return temp;
	}

	public bool requestUpdateSwitch(IBlock bToUpdate)
	{
		if(bToUpdate==null)
			return false;

		string updateString=_dbManager.createUpdate("SWITCH",bToUpdate);
		if(updateString==null)
			return false;

		bool res=_dbManager.runUpdate(updateString);

		return res;
	}

	public bool requestUpdateBlock(IBlock bToUpdate)
	{
		if(bToUpdate==null)
			return false;
		
		string updateString=_dbManager.createUpdate("BLOCK",bToUpdate);
		if(updateString==null)
			return false;

		bool res=_dbManager.runUpdate(updateString);
		
		return res;
	}
		
		
        //Handle environment tick
        void  _environment_Tick(object sender, TickEventArgs e)
        {
                //handle tick here
      	}
    }
}
