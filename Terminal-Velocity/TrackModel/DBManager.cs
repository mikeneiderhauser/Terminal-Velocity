using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.SqlClient;
using Interfaces;
using Utility;

namespace TrackModel
{

    public class DBManager
    {
        //Private parameters
	private SQLiteConnection _dbCon;

        public DBManager(SQLiteConnection con)
        {
		_dbCon=con;
        }
		
		
	//Public methods
        public String createQueryString(string qType, int ID, string line)
        {
		//Check basic ID validity
			//We need to allow zero's--> Route 0 is red, and Block 0 is yard

            


		if(ID<0)
		{
			return null;
		}
	
		if(!qType.Equals("BLOCK",StringComparison.OrdinalIgnoreCase) && !qType.Equals("ROUTE",StringComparison.OrdinalIgnoreCase) )
		{
			return null;
		}
		else if(qType.Equals("BLOCK",StringComparison.OrdinalIgnoreCase))//Format for block
		{
                      //Test whether block exists
            if (!line.Equals("Red", StringComparison.OrdinalIgnoreCase) && !line.Equals("Green", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
                        bool exists=blockExists(ID,line);
                        if(exists)
                        {
                                string blockQuery=      "SELECT *"+
                                                        "FROM BLOCKS"+
                                                        "WHERE blockID="+ID+" AND line='"+line+"'";
                                return blockQuery;
                        }
                        else
                                return null;

		}
		else//(qType.Equals("ROUTE",StringComparison.OrdinalIgnoreCase))//Format for ROUTE
		{

			//Only accept 0 or 1 for route id's
			string routeName;
			if(ID==0)
			{
				routeName="Red";
			}
			else if(ID==1)
			{
				routeName="Green";
			}
			else
			{
				//Only supports red and green lines
				return null;
			}
	
			//Routes right now only exists as the set of
			//BLOCK table tuples sharing a line name.
                        string routeQuery=      "SELECT *"+
                                                "FROM BLOCKS"+
                                                "WHERE line='"+routeName+"'";
                        return routeQuery;
		}
	}

	private bool routeExists(int id)
	{
		string selectString=	"SELECT *"+
					"FROM ROUTES"+
					"WHERE routeID="+id;

                _dbCon.Open();
                        //Initialize command to create BLOCKS TABLE
                        SQLiteCommand selCom=new SQLiteCommand(selectString);
                        selCom.Connection=_dbCon;
                        try
                        {
                                SQLiteDataReader tempReader=selCom.ExecuteReader();
                                bool exists=tempReader.HasRows;
                                tempReader.Close();//Close reader
                                _dbCon.Close();//CLOSE DB
                                return exists;
                        }
                        catch(Exception crap)
                        {
                                _dbCon.Close();
                                return false;
                        }


	}

	private bool blockExists(int id, string line)
	{
		string selectString=	"SELECT *"+
					"FROM BLOCKS" +
					"WHERE blockID="+id+" AND line='"+line+"'";

                _dbCon.Open();
                        //Initialize command to create BLOCKS TABLE
                        SQLiteCommand selCom=new SQLiteCommand(selectString);
                        selCom.Connection=_dbCon;
                        try
                        {
                            SQLiteDataReader tempReader = selCom.ExecuteReader();
    				bool exists=tempReader.HasRows;
				tempReader.Close();//Close reader
                                _dbCon.Close();//CLOSE DB
				return exists;        
                        }
                        catch(Exception crap)
                        {
                                _dbCon.Close();
                                return false;
                        }
	}
	
	//Allows updates types of "SWITCH" or "BLOCK"
	//	SWITCH updates only affect the switch
	//	BLOCK updates are allowed to update state info, track circuit info, heater info
	public String createUpdate(string updateType, IBlock bToUpdate)
	{
		//Check that block isnt null
		if(bToUpdate==null)
			return null;

		//Get block ID and check that it exists
		int bID=bToUpdate.BlockID;
		string line=bToUpdate.Line;
		bool exists=blockExists(bID,"Red");
		if(!exists)
			return null;

		//if updateType was an unexpected value, quit out
		if(!updateType.Equals("SWITCH",StringComparison.OrdinalIgnoreCase) && !updateType.Equals("BLOCK",StringComparison.OrdinalIgnoreCase))
		{
			return null;
		}
		else if(updateType.Equals("SWITCH",StringComparison.OrdinalIgnoreCase) )
		{
			//Create switch update string
			string updateString=	"UPDATE BLOCKS "+
						"SET dest1="+bToUpdate.SwitchDest2+", dest2="+bToUpdate.SwitchDest1+
						" WHERE blockID="+bID+" AND line='"+line+"'";
			return updateString;
		}
		else//updateType.Equals("BLOCK",StringComparison.OrdinalIgnoreCase)
		{
			//blocks are allowed to update heater, track circuit info, state
			//To get the heater information, we have to turn the whole attributes
				//array of the block into a string for putting in the DB
			string attrString="";
			
			//Assemble attribute string from split array
			//	--will be put in database
			string[] tempArr=bToUpdate.AttrArray;
			for(int i=0;i<tempArr.Length;i++)
			{
				attrString=attrString+tempArr[i]+"; ";
			}


			//Create block update string			
			string updateString=	"UPDATE BLOCKS "+
						"SET state='"+bToUpdate.State+"', trackCirID="+bToUpdate.TrackCirID+", infra='"+attrString+"' "+
						" WHERE blockID="+bID+" AND line='"+line+"'";
			return  updateString;
		}
	}

	public String createInsert(Block b)
	{
		//Looking back, im not sure why I thought I needed this method.
		//DB insertion should be done entirely through the DBCreator and not through the DBManager

		//I will hold off on implementing this method until I'm certain I need to
		return null;
	}


	//Return type should be changed into some sort of
	//SQLResults object after I examine the libraries
	//and classes used for this type of thing in C#
    public SQLiteDataReader runQuery(string sqlQuery)
	{
                _dbCon.Open();
                        //Initialize command to create BLOCKS TABLE
                        SQLiteCommand selCom=new SQLiteCommand(sqlQuery);
                        selCom.Connection=_dbCon;
                        try
                        {
                                SQLiteDataReader tempReader = selCom.ExecuteReader();
                                _dbCon.Close();//CLOSE DB
                                return tempReader;
                        }
                        catch(Exception crap)
                        {
                                _dbCon.Close();
                                return null;
                        }

	}


	public bool runUpdate(string sqlUpdate)
	{
		if(_dbCon==null)
			return false;


                _dbCon.Open();
                SQLiteCommand updateCom=new SQLiteCommand(sqlUpdate);
                updateCom.Connection=_dbCon;
                try
                {
                     int res=updateCom.ExecuteNonQuery();//Exec CREATE
                     //Console.WriteLine(res);
                     _dbCon.Close();//CLOSE DB
                     if(res!=1)
                          return false;
		     else
			  return true;
                }
                catch(Exception crap)
                {
                      _dbCon.Close();
                     //Console.WriteLine(crap.Message.ToStrin
                     return false;
                }


	}

	public bool runInsert(string sqlInsert)
	{
		if(_dbCon==null)
			return false;

                _dbCon.Open();
                SQLiteCommand insertCom=new SQLiteCommand(sqlInsert);
                insertCom.Connection=_dbCon;
                try
                {
                     int res=insertCom.ExecuteNonQuery();//Exec insert
                     //Console.WriteLine(res);
                     _dbCon.Close();//CLOSE DB
                     if(res!=1)
                          return false;
                     else
                          return true;
                }
                catch(Exception crap)
                {
                      _dbCon.Close();
                     //Console.WriteLine(crap.Message.ToStrin
                     return false;
                }

	}


	//Argument to this function shouldbe changed
	//into the SQLResults object returned from
	//runQuery() above
    public Block formatBlockQueryResults(SQLiteDataReader bR)
	{
		Block tempBlock=null;
		int i=0;
		while (bR.Read() )
		{
			//Get all fields for a given block
			string bID=bR.GetString(bR.GetOrdinal("blockID"));
			string line=bR.GetString(bR.GetOrdinal("line"));
			string infra=bR.GetString(bR.GetOrdinal("infra"));
			string sE=bR.GetString(bR.GetOrdinal("starting_elev"));
			string grade=bR.GetString(bR.GetOrdinal("grade"));
			string locX=bR.GetString(bR.GetOrdinal("locX"));
			string locY=bR.GetString(bR.GetOrdinal("locY"));
			string bSize=bR.GetString(bR.GetOrdinal("bSize"));
			string dir=bR.GetString(bR.GetOrdinal("dir"));
			string state=bR.GetString(bR.GetOrdinal("state"));
			string prev=bR.GetString(bR.GetOrdinal("prev"));
			string dest1=bR.GetString(bR.GetOrdinal("dest1"));
			string dest2=bR.GetString(bR.GetOrdinal("dest2"));
			string trackCirID=bR.GetString(bR.GetOrdinal("trackCirID"));

			//////////////////////////////////////////////////////////////////////
			//Parse fields that must be provided as a different type
			string[] infraFinal=infra.Split(';');
                        DirEnum dirFinal=(DirEnum)Enum.Parse(typeof(DirEnum),dir,true);
                        StateEnum stateFinal=(StateEnum)Enum.Parse(typeof(StateEnum),state,true);
			int bIDFinal; 
				bool bIDRes=int.TryParse(bID, out bIDFinal);
				if(!bIDRes) {bIDFinal=-1;}
			double sEFinal; 
				bool sERes=double.TryParse(sE,out sEFinal);
				if(!sERes) {sEFinal=-1.0;}
			double gradeFinal; 
				bool gradeRes=double.TryParse(grade,out gradeFinal);
				if(!gradeRes) {gradeFinal=-1.0;}
			int[] locFinal=new int[2];
				bool locXRes=int.TryParse(locX,out locFinal[0]);
				if (!locXRes) {locFinal[0]=-1;}
				bool locYRes=int.TryParse(locY,out locFinal[1]);
				if(!locYRes) {locFinal[1]=-1;}
			double bSizeFinal;
				bool bSizeRes=double.TryParse(bSize,out bSizeFinal);
				if(!bSizeRes) {bSizeFinal=-1.0;}
			int prevFinal;
				bool prevRes=int.TryParse(prev,out prevFinal);
				if(!prevRes) {prevFinal=-1;}
			int dest1Final;
				bool dest1Res=int.TryParse(dest1,out dest1Final);
				if(!dest1Res) {dest1Final=-1;}
			int dest2Final;
				bool dest2Res=int.TryParse(dest2,out dest2Final);
				if(!dest2Res) {dest2Final=-1;}
			int trackCirIDFinal;
				bool trackCirIDRes=int.TryParse(trackCirID,out trackCirIDFinal);
				if(!trackCirIDRes) {trackCirIDFinal=-1;}

			tempBlock=new Block(bIDFinal,stateFinal,prevFinal,sEFinal,gradeFinal,locFinal,bSizeFinal,dirFinal,infraFinal,dest1Final,dest2Final,trackCirIDFinal,line);
			i++;//Inc counter
		}

		if(i!=1)
			return null;
		else
			return tempBlock;
	}

	//Argument to this function should be changed
	//into the SQLResults object returned from
	//runQuery above (and used in fQR above)
    public RouteInfo formatRouteQueryResults(SQLiteDataReader rr)
	{

		if(rr==null)
			return null;
		//Temp list used to store blocks, since we dont know
		//ahead of time how many to expect
		List<Block> blockList = new List<Block>();
		int nBlocks=0;

		//We need to get our list of blocks, and the number of blocks
		while (rr.Read() )
		{
			//Get all fields for a given block
                        string bID=rr.GetString(rr.GetOrdinal("blockID"));
                        string line=rr.GetString(rr.GetOrdinal("line"));
                        string infra=rr.GetString(rr.GetOrdinal("infra"));
                        string sE=rr.GetString(rr.GetOrdinal("starting_elev"));
                        string grade=rr.GetString(rr.GetOrdinal("grade"));
                        string locX=rr.GetString(rr.GetOrdinal("locX"));
                        string locY=rr.GetString(rr.GetOrdinal("locY"));
                        string bSize=rr.GetString(rr.GetOrdinal("bSize"));
                        string dir=rr.GetString(rr.GetOrdinal("dir"));
                        string state=rr.GetString(rr.GetOrdinal("state"));
                        string prev=rr.GetString(rr.GetOrdinal("prev"));
                        string dest1=rr.GetString(rr.GetOrdinal("dest1"));
                        string dest2=rr.GetString(rr.GetOrdinal("dest2"));
                        string trackCirID=rr.GetString(rr.GetOrdinal("trackCirID"));
			
                        //////////////////////////////////////////////////////////////////////
                        //Parse fields that must be provided as a different type
                        string[] infraFinal=infra.Split(';');
                        DirEnum dirFinal=(DirEnum)Enum.Parse(typeof(DirEnum),dir,true);
                        StateEnum stateFinal=(StateEnum)Enum.Parse(typeof(StateEnum),state,true);
                        int bIDFinal;
                                bool bIDRes=int.TryParse(bID, out bIDFinal);
                                if(!bIDRes) {bIDFinal=-1;}
                        double sEFinal;
                                bool sERes=double.TryParse(sE,out sEFinal);
                                if(!sERes) {sEFinal=-1.0;}
                        double gradeFinal;
                                bool gradeRes=double.TryParse(grade,out gradeFinal);
                                if(!gradeRes) {gradeFinal=-1.0;}
                        int[] locFinal=new int[2];
                                bool locXRes=int.TryParse(locX,out locFinal[0]);
                                if (!locXRes) {locFinal[0]=-1;}
                                bool locYRes=int.TryParse(locY,out locFinal[1]);
                                if(!locYRes) {locFinal[1]=-1;}
                        double bSizeFinal;
                                bool bSizeRes=double.TryParse(bSize,out bSizeFinal);
                                if(!bSizeRes) {bSizeFinal=-1.0;}
                        int prevFinal;
                                bool prevRes=int.TryParse(prev,out prevFinal);
                                if(!prevRes) {prevFinal=-1;}
                        int dest1Final;
                                bool dest1Res=int.TryParse(dest1,out dest1Final);
                                if(!dest1Res) {dest1Final=-1;}
                        int dest2Final;
                                bool dest2Res=int.TryParse(dest2,out dest2Final);
                                if(!dest2Res) {dest2Final=-1;}
                        int trackCirIDFinal;
                                bool trackCirIDRes=int.TryParse(trackCirID,out trackCirIDFinal);
                                if(!trackCirIDRes) {trackCirIDFinal=-1;}

			blockList.Add(new Block(bIDFinal,stateFinal,prevFinal,sEFinal,gradeFinal,locFinal,bSizeFinal,dirFinal,infraFinal,dest1Final,dest2Final,trackCirIDFinal,line) );
			nBlocks++;
		}

		//If we didnt find any blocks, give up.
		if(nBlocks==0)
			return null;

		
		Block[] blocks=blockList.ToArray();
		string rName=blocks[0].Line;
		int rID;
		if(rName.Equals("Red",StringComparison.OrdinalIgnoreCase))
			rID=0;
		else
			rID=1;


		//All routes start and end at the yard
		int sID=0;
		int eID=0;

		RouteInfo tempRoute=new RouteInfo(rID,rName,nBlocks,blocks,sID,eID);
		return tempRoute;
		
	}
		
		
		
        #region Properties
	#endregion

    }
}
