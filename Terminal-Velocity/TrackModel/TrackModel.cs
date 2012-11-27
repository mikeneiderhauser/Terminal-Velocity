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
		if(routeID!=0 && routeID!=1)
			return null;
		string routeQuery=_dbManager.createQueryString("ROUTE",routeID);
	
		//Check query return val
		if(routeQuery==null)
			return null;

		//Get data reader from query
		SqlDataReader queryReader=_dbManager.runQuery(routeQuery);
		
		//Check data reader return val
		if(queryReader==null)
			return null;

		IRouteInfo temp = _dbManager.formatRouteQueryResults(queryReader);
		return temp;
	}

	public IBlock[,] requestTrackGrid(int routeID)
	{
		if(routeID!=0 && routeID!=1)
			return null;

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
			temp[10,28]=this.requestBlockInfo(1);//1
			temp[9,29]=this.requestBlockInfo(2);//2
			temp[8,29]=this.requestBlockInfo(3);//3
			temp[7,30]=this.requestBlockInfo(4);//4
			temp[7,31]=this.requestBlockInfo(5);//5
			temp[7,32]=this.requestBlockInfo(6);//6
			temp[7,33]=this.requestBlockInfo(7);//7
			temp[7,34]=this.requestBlockInfo(7);//7
			temp[7,35]=this.requestBlockInfo(8);//8
			temp[8,36]=this.requestBlockInfo(8);//8
			temp[9,36]=this.requestBlockInfo(9);//9
			temp[10,35]=this.requestBlockInfo(10);//10
			temp[10,34]=this.requestBlockInfo(11);//11
			temp[11,33]=this.requestBlockInfo(11);//11
			temp[11,32]=this.requestBlockInfo(12);//12
			temp[11,31]=this.requestBlockInfo(13);//13
			temp[11,30]=this.requestBlockInfo(13);//13
			temp[11,29]=this.requestBlockInfo(14);//14
			temp[11,28]=this.requestBlockInfo(14);//14
			temp[10,27]=this.requestBlockInfo(15);//15
			temp[10,26]=this.requestBlockInfo(16);//16
			temp[10,25]=this.requestBlockInfo(17);//17
			temp[10,24]=this.requestBlockInfo(18);//18
			temp[10,23]=this.requestBlockInfo(19);//19
			temp[10,22]=this.requestBlockInfo(20);//20
			temp[10,21]=this.requestBlockInfo(21);//21
			temp[11,20]=this.requestBlockInfo(22);//22
			temp[12,19]=this.requestBlockInfo(23);//23
			temp[13,19]=this.requestBlockInfo(24);//24
			temp[14,19]=this.requestBlockInfo(25);//25
			temp[15,19]=this.requestBlockInfo(26);//26
			temp[16,19]=this.requestBlockInfo(27);//27
			temp[17,19]=this.requestBlockInfo(28);//28
			temp[18,19]=this.requestBlockInfo(29);//29
			temp[19,19]=this.requestBlockInfo(30);//30
			temp[20,19]=this.requestBlockInfo(31);//31
			temp[21,19]=this.requestBlockInfo(32);//32
			temp[22,19]=this.requestBlockInfo(33);//33
			temp[23,19]=this.requestBlockInfo(34);//34
			temp[24,19]=this.requestBlockInfo(35);//35
			temp[25,19]=this.requestBlockInfo(36);//36
			temp[26,19]=this.requestBlockInfo(37);//37
			temp[27,19]=this.requestBlockInfo(38);//38
			temp[28,19]=this.requestBlockInfo(39);//39
			temp[39,19]=this.requestBlockInfo(40);//40
			temp[30,19]=this.requestBlockInfo(41);//41
			temp[31,19]=this.requestBlockInfo(42);//42
			temp[32,19]=this.requestBlockInfo(43);//43
			temp[33,19]=this.requestBlockInfo(44);//44
			temp[34,19]=this.requestBlockInfo(45);//45
			temp[35,19]=this.requestBlockInfo(46);//46
			temp[36,18]=this.requestBlockInfo(47);//47
			temp[37,17]=this.requestBlockInfo(47);//47
			temp[37,16]=this.requestBlockInfo(48);//48
			temp[38,15]=this.requestBlockInfo(49);//49
			temp[38,14]=this.requestBlockInfo(50);//50
			temp[38,13]=this.requestBlockInfo(50);//50
			temp[38,12]=this.requestBlockInfo(51);//51
			temp[38,11]=this.requestBlockInfo(52);//52
			temp[38,10]=this.requestBlockInfo(53);//53
			temp[38,9]=this.requestBlockInfo(53);//53
			temp[38,8]=this.requestBlockInfo(53);//53
			temp[38,7]=this.requestBlockInfo(54);//54
			temp[38,6]=this.requestBlockInfo(54);//54
			temp[37,5]=this.requestBlockInfo(55);//55
			temp[36,4]=this.requestBlockInfo(55);//55
			temp[35,3]=this.requestBlockInfo(56);//56
			temp[34,2]=this.requestBlockInfo(57);//57
			temp[33,2]=this.requestBlockInfo(57);//57
			temp[32,2]=this.requestBlockInfo(58);//58
			temp[31,2]=this.requestBlockInfo(58);//58
			temp[31,3]=this.requestBlockInfo(59);//59
			temp[30,4]=this.requestBlockInfo(59);//59
			temp[30,5]=this.requestBlockInfo(60);//60
			temp[30,6]=this.requestBlockInfo(60);//60
			temp[31,7]=this.requestBlockInfo(61);//61
			temp[32,7]=this.requestBlockInfo(61);//61
			temp[33,8]=this.requestBlockInfo(62);//62
			temp[34,8]=this.requestBlockInfo(63);//63
			temp[35,8]=this.requestBlockInfo(63);//63
			temp[36,8]=this.requestBlockInfo(64);//64
			temp[36,9]=this.requestBlockInfo(65);//65
			temp[37,10]=this.requestBlockInfo(66);//66
			temp[32,18]=this.requestBlockInfo(67);//67
			temp[32,17]=this.requestBlockInfo(67);//67
			temp[31,16]=this.requestBlockInfo(68);//68
			temp[30,16]=this.requestBlockInfo(69);//69
			temp[29,16]=this.requestBlockInfo(69);//69
			temp[28,16]=this.requestBlockInfo(70);//70
			temp[27,17]=this.requestBlockInfo(71);//71
			temp[27,18]=this.requestBlockInfo(71);//71
			temp[21,18]=this.requestBlockInfo(72);//72
			temp[21,17]=this.requestBlockInfo(72);//72
			temp[20,16]=this.requestBlockInfo(73);//73
			temp[19,16]=this.requestBlockInfo(74);//74
			temp[18,16]=this.requestBlockInfo(74);//74
			temp[17,16]=this.requestBlockInfo(75);//75
			temp[16,17]=this.requestBlockInfo(76);//76
			temp[16,18]=this.requestBlockInfo(76);//76
		}
		else//routeID==1 means green
		{

		}
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
