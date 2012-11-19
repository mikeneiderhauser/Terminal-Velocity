using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sqlite;
using Interfaces;
using Utility;

namespace TrackModel
{

    public class DBManager
    {
        //Private parameters
	private SqliteConnection _dbCon;

        public DBManager(SqliteConnection con)
        {
		_dbCon=con;
        }
		
		
	//Public methods
        public String createQueryString(string qType, int ID)
        {
		//Check basic ID validity
		if(ID<=0)
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
                        bool exists=blockExists(ID);
                        if(exists)
                        {
                                string blockQuery=      "SELECT *"+
                                                        "FROM BLOCKS"+
                                                        "WHERE blockID="+ID;
                                return blockQuery;
                        }
                        else
                                return null;

		}
		else//(qType.Equals("ROUTE",StringComparison.OrdinalIgnoreCase))//Format for ROUTE
		{
			//Test whether block exists
                        bool exists=routeExists(ID);
                        if(exists)
                        {
                                string routeQuery=      "SELECT *"+
                                                        "FROM ROUTES"+
                                                        "WHERE routeID="+ID;
                                return routeQuery;
                        }
                        else
                                return null;
		}
	}

	private bool routeExists(int id)
	{
		string selectString=	"SELECT *"+
					"FROM ROUTES"+
					"WHERE routeID="+id;

                _dbCon.Open();
                        //Initialize command to create BLOCKS TABLE
                        SqliteCommand selCom=new SqliteCommand(selectString);
                        selCom.Connection=_dbCon;
                        try
                        {
                                SqlDataReader tempReader=selCom.ExecuteReader();
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

	private bool blockExists(int id)
	{
		string selectString=	"SELECT *"+
					"FROM BLOCKS" +
					"WHERE blockID="+id;

                _dbCon.Open();
                        //Initialize command to create BLOCKS TABLE
                        SqliteCommand selCom=new SqliteCommand(selectString);
                        selCom.Connection=_dbCon;
                        try
                        {
                                SqlDataReader tempReader=selCom.ExecuteReader();
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
	public String createUpdate(string updateType, Block bToUpdate)
	{
		//Check that block isnt null
		if(bToUpdate==null)
			return null;

		//Get block ID and check that it exists
		int bID=bToUpdate.BlockID;
		bool exists=blockExists(bID);
		if(!exists)
			return null;

		//if updateType was an unexpected value, quit out
		if(!updateType.Equals("SWITCH",StringComparison.OrdinalIgnoreCase) && !updateType.Equals("BLOCK",StringComparison.OrdinalIgnoreCase)
		{
			return null;
		}
		else if(updateType.Equals("SWITCH",StringComparison.OrdinalIgnoreCase) )
		{
			//Create switch update string
			string updateString=	"UPDATE BLOCKS "+
						"SET dest1="+bToUpdate.SwitchDest2+", dest2="+bToUpdate.SwitchDest1+
						" WHERE blockID="+bID;
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
			for(int i=0;i<bToUpdate.Length;i++)
			{
				attrString=attrString+bToUpdate[i]+"; ";
			}


			//Create block update string			
			string updateString=	"UPDATE BLOCKS "+
						"SET state='"+bToUpdate.State+"', trackCirID="+bToUpdate.TrackCirID+", infra='"+attrString+"' "
						" WHERE blockID="+bID;
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
	public SqlDataReader runQuery(string sqlQuery)
	{
                _dbCon.Open();
                        //Initialize command to create BLOCKS TABLE
                        SqliteCommand selCom=new SqliteCommand(sqlQuery);
                        selCom.Connection=_dbCon;
                        try
                        {
                                SqlDataReader tempReader=selCom.ExecuteReader();
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
                SqliteCommand updateCom=new SqliteCommand(sqlUpdate);
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
                SqliteCommand insertCom=new SqliteCommand(sqlInsert);
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
	public Block formatBlockQueryResults(SqlDataReader blockReader)
	{
		return null;
	}

	//Argument to this function should be changed
	//into the SQLResults object returned from
	//runQuery above (and used in fQR above)
	public Route formatRouteQueryResults(SqlDataReader routeReader)
	{
		return null;
	}
		
		
		
        #region Properties
	#endregion

    }
}
