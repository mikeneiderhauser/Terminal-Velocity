using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sqlite;
using System.IO;
using Interfaces;
using Utilities;

namespace TrackModel
{
    public class DBCreator
    {
        //Private parameters
	private SqliteConnection _dbCon;

        public DBCreator(string fPath)
        {
		//fPath is the filename of the database to connect to.
		//Check filepath's legit'ness, give it the default otherwise
		if(fPath==null || fPath.Equals(""))
		{
			fPath="trackDatabase.db";
		}

		//Check if the database file exists (before you create it).  If it does, we need
		//to CREATE the necessary tables
		bool needsInitFlag=false;
		if(!File.Exists(fPath))
		{
			needsInitFlag=true;
		}

		//Create a db connection (creating the db file if it doesnt already exist)
		string connectionString="Data Source="+fPath;
		_dbCon=new SqliteConnection(connectionString);

		//If you needed to create the db tables,do so now
		if(needsInitFlag==true)
		{
			//Console.WriteLine("About to enter initDB");
			int initResult=initDB();

			//If creating the db failed
			if(initResult==-1)
			{
				_dbCon=null;
			}
		}

        }

	private int initDB()
	{
		//BLOCKS TABLE declaration
                string createBLOCKS="CREATE TABLE BLOCKS ("+
                                   "blockID int NOT NULL," +		//
                                   "line varchar2(25) NOT NULL," +	//
                                   "infra varchar2(200)," +		//
                                   "starting_elev float(25)," +		//
                                   "grade float(25),"+			//
				   "locX int,"+
				   "locY int,"+
				   "bSize float(25),"+			//
				   "dir varchar2(50),"+			//
				   "state varchar2(100),"+		//
				   "prev int,"+				//
				   "dest1 int,"+
				   "dest2 int,"+
				   "trackCirID int,"+			//
                                   "CONSTRAINT pk_Blocks PRIMARY KEY(blockID,line) )";

		_dbCon.Open();
			//Initialize command to create BLOCKS TABLE
			SqliteCommand createCommand=new SqliteCommand(createBLOCKS);
			createCommand.Connection=_dbCon;
			try
			{
				int res=createCommand.ExecuteNonQuery();//Exec CREATEi
				//Console.WriteLine(res);
				_dbCon.Close();//CLOSE DB
				if(res!=0)
					return -1;
				else
					return 0;
			}
			catch(Exception crap)
			{
				_dbCon.Close();
				return -1;
			}
	}
		
		
	public int parseInputFile(string fPath)
	{
		//If our constructor failed, return an error code please
		if(_dbCon==null)
		{
			return -3;
		}

		if(fPath==null)
		{
			return -1;
		}

		//Check that file exists
		if(!File.Exists(fPath))
		{
			return -1;
		}

		//Check the file format
		if(!fPath.EndsWith(".csv") )
		{
			return -2;
		}

		//Read all information from file into an array of lines
		string[] fileLines = File.ReadAllLines(fPath);


                //Set up character delimiter
                char[] commaArr=new char[1];
                commaArr[0]=',';

		//Console.WriteLine("About to enter forloop");
		////////////////////////////////////////////////////////
		//Iterate through each one.
		
		string prevID="16";//
		foreach(string line in fileLines)
		{
			//Console.WriteLine("Line is: "+line);
			//IF we're not a blank line (begins with comma) or a header line (begins with Line)
			//THEN process and insert
			if(!line.StartsWith(",") && !line.StartsWith("Line") && !line.StartsWith(" "))
			{
				
			
				//Delimit around comma's
				string[] fields=line.Split(commaArr);
				string blockID=fields[2];
				string lineName=fields[0];

				
				string infra;
				if(fields[6].Equals("",StringComparison.OrdinalIgnoreCase))
				{
					infra="none";
				}
				else
				{
					infra=fields[6];
				}
				string sElev=fields[9];
				string grade=fields[4];
				string blockSize=fields[3];
				string singleInsert="INSERT INTO BLOCKS(blockID, line, infra, starting_elev, grade, bSize,dir,state,trackCirID) VALUES(" +
						blockID+", '"+lineName+"', '"+infra+"', "+sElev+", "+grade+", "+blockSize+",'North', 'Healthy',-1)";
				//Console.WriteLine(singleInsert);
				prevID=blockID;
				_dbCon.Open();
	                	        SqliteCommand insertCommand=new SqliteCommand(singleInsert);
        		                insertCommand.Connection=_dbCon;



		                        try
                		        {
                                		int res=insertCommand.ExecuteNonQuery();//Exec CREATE
						//Console.WriteLine(res);
         	                        	_dbCon.Close();//CLOSE DB
                 	               		if(res!=1)
                        	                	return -1;
                        		}
                        		catch(Exception crap)
                        		{
                                		_dbCon.Close();
						//Console.WriteLine(crap.Message.ToString());
                                		return -1;
                        		}
			}//End if statement for valid data line
		}
		//End for loop iterating through all strings
		////////////////////////////////////////////////////////

		

		//Now we need to do hardcoded updates to the file.
			//Each update will be put in a list of strings, and which will be looped through
		List<string> updateList=new List<string>();
		//If we're the red track
		if(fPath.Contains("red") || fPath.Contains("Red"))
		{
			updateList.Add("UPDATE BLOCKS SET prev=15, dest1=2, dest2=-1 WHERE line='Red' AND blockID=1");
                        updateList.Add("UPDATE BLOCKS SET prev=1, dest1=3, dest2=-1 WHERE line='Red' AND blockID=2");
                        updateList.Add("UPDATE BLOCKS SET prev=2, dest1=4, dest2=-1 WHERE line='Red' AND blockID=3");
                        updateList.Add("UPDATE BLOCKS SET prev=3, dest1=5, dest2=-1 WHERE line='Red' AND blockID=4");
                        updateList.Add("UPDATE BLOCKS SET prev=4, dest1=6, dest2=-1 WHERE line='Red' AND blockID=5");
                        updateList.Add("UPDATE BLOCKS SET prev=5, dest1=7, dest2=-1 WHERE line='Red' AND blockID=6");
                        updateList.Add("UPDATE BLOCKS SET prev=6, dest1=8, dest2=-1 WHERE line='Red' AND blockID=7");
                        updateList.Add("UPDATE BLOCKS SET prev=7, dest1=9, dest2=-1 WHERE line='Red' AND blockID=8");
                        updateList.Add("UPDATE BLOCKS SET prev=8, dest1=10, dest2=0 WHERE line='Red' AND blockID=9");
                        updateList.Add("UPDATE BLOCKS SET prev=9, dest1=11, dest2=-1 WHERE line='Red' AND blockID=10");
                        updateList.Add("UPDATE BLOCKS SET prev=10, dest1=12, dest2=-1 WHERE line='Red' AND blockID=11");
                        updateList.Add("UPDATE BLOCKS SET prev=11, dest1=13, dest2=-1 WHERE line='Red' AND blockID=12");
                        updateList.Add("UPDATE BLOCKS SET prev=12, dest1=14, dest2=-1 WHERE line='Red' AND blockID=13");
                        updateList.Add("UPDATE BLOCKS SET prev=13, dest1=15, dest2=-1 WHERE line='Red' AND blockID=14");
                        updateList.Add("UPDATE BLOCKS SET prev=16, dest1=1, dest2=14 WHERE line='Red' AND blockID=15");
                        updateList.Add("UPDATE BLOCKS SET prev=15, dest1=17, dest2=-1 WHERE line='Red' AND blockID=16");
                        updateList.Add("UPDATE BLOCKS SET prev=16, dest1=18, dest2=-1 WHERE line='Red' AND blockID=17");
                        updateList.Add("UPDATE BLOCKS SET prev=17, dest1=19, dest2=-1 WHERE line='Red' AND blockID=18");
                        updateList.Add("UPDATE BLOCKS SET prev=18, dest1=20, dest2=-1 WHERE line='Red' AND blockID=19");
                        updateList.Add("UPDATE BLOCKS SET prev=19, dest1=21, dest2=-1 WHERE line='Red' AND blockID=20");
                        updateList.Add("UPDATE BLOCKS SET prev=20, dest1=22, dest2=-1 WHERE line='Red' AND blockID=21");
                        updateList.Add("UPDATE BLOCKS SET prev=21, dest1=23, dest2=-1 WHERE line='Red' AND blockID=22");
                        updateList.Add("UPDATE BLOCKS SET prev=22, dest1=24, dest2=-1 WHERE line='Red' AND blockID=23");
                        updateList.Add("UPDATE BLOCKS SET prev=23, dest1=25, dest2=-1 WHERE line='Red' AND blockID=24");
                        updateList.Add("UPDATE BLOCKS SET prev=24, dest1=26, dest2=-1 WHERE line='Red' AND blockID=25");
                        updateList.Add("UPDATE BLOCKS SET prev=25, dest1=27, dest2=-1 WHERE line='Red' AND blockID=26");
                        updateList.Add("UPDATE BLOCKS SET prev=26, dest1=28, dest2=76 WHERE line='Red' AND blockID=27");
                        updateList.Add("UPDATE BLOCKS SET prev=27, dest1=29, dest2=-1 WHERE line='Red' AND blockID=28");
                        updateList.Add("UPDATE BLOCKS SET prev=28, dest1=30, dest2=-1 WHERE line='Red' AND blockID=29");
                        updateList.Add("UPDATE BLOCKS SET prev=29, dest1=31, dest2=-1 WHERE line='Red' AND blockID=30");
                        updateList.Add("UPDATE BLOCKS SET prev=30, dest1=32, dest2=-1 WHERE line='Red' AND blockID=31");
                        updateList.Add("UPDATE BLOCKS SET prev=33, dest1=31, dest2=72 WHERE line='Red' AND blockID=32");
                        updateList.Add("UPDATE BLOCKS SET prev=32, dest1=34, dest2=-1 WHERE line='Red' AND blockID=33");
                        updateList.Add("UPDATE BLOCKS SET prev=33, dest1=35, dest2=-1 WHERE line='Red' AND blockID=34");
                        updateList.Add("UPDATE BLOCKS SET prev=34, dest1=36, dest2=-1 WHERE line='Red' AND blockID=35");
                        updateList.Add("UPDATE BLOCKS SET prev=35, dest1=37, dest2=-1 WHERE line='Red' AND blockID=36");
                        updateList.Add("UPDATE BLOCKS SET prev=36, dest1=38, dest2=-1 WHERE line='Red' AND blockID=37");
                        updateList.Add("UPDATE BLOCKS SET prev=37, dest1=39, dest2=71 WHERE line='Red' AND blockID=38");
                        updateList.Add("UPDATE BLOCKS SET prev=38, dest1=40, dest2=-1 WHERE line='Red' AND blockID=39");
                        updateList.Add("UPDATE BLOCKS SET prev=39, dest1=41, dest2=-1 WHERE line='Red' AND blockID=40");
                        updateList.Add("UPDATE BLOCKS SET prev=40, dest1=42, dest2=-1 WHERE line='Red' AND blockID=41");
                        updateList.Add("UPDATE BLOCKS SET prev=41, dest1=43, dest2=-1 WHERE line='Red' AND blockID=42");
                        updateList.Add("UPDATE BLOCKS SET prev=44, dest1=42, dest2=67 WHERE line='Red' AND blockID=43");
                        updateList.Add("UPDATE BLOCKS SET prev=43, dest1=45, dest2=-1 WHERE line='Red' AND blockID=44");
                        updateList.Add("UPDATE BLOCKS SET prev=44, dest1=46, dest2=-1 WHERE line='Red' AND blockID=45");
                        updateList.Add("UPDATE BLOCKS SET prev=45, dest1=47, dest2=-1 WHERE line='Red' AND blockID=46");
                        updateList.Add("UPDATE BLOCKS SET prev=46, dest1=48, dest2=-1 WHERE line='Red' AND blockID=47");
                        updateList.Add("UPDATE BLOCKS SET prev=47, dest1=49, dest2=-1 WHERE line='Red' AND blockID=48");
                        updateList.Add("UPDATE BLOCKS SET prev=48, dest1=50, dest2=-1 WHERE line='Red' AND blockID=49");
                        updateList.Add("UPDATE BLOCKS SET prev=49, dest1=51, dest2=-1 WHERE line='Red' AND blockID=50");
                        updateList.Add("UPDATE BLOCKS SET prev=50, dest1=52, dest2=-1 WHERE line='Red' AND blockID=51");
                        updateList.Add("UPDATE BLOCKS SET prev=51, dest1=53, dest2=66 WHERE line='Red' AND blockID=52");
                        updateList.Add("UPDATE BLOCKS SET prev=52, dest1=54, dest2=-1 WHERE line='Red' AND blockID=53");
                        updateList.Add("UPDATE BLOCKS SET prev=53, dest1=55, dest2=-1 WHERE line='Red' AND blockID=54");
                        updateList.Add("UPDATE BLOCKS SET prev=54, dest1=56, dest2=-1 WHERE line='Red' AND blockID=55");
                        updateList.Add("UPDATE BLOCKS SET prev=55, dest1=57, dest2=-1 WHERE line='Red' AND blockID=56");
                        updateList.Add("UPDATE BLOCKS SET prev=56, dest1=58, dest2=-1 WHERE line='Red' AND blockID=57");
                        updateList.Add("UPDATE BLOCKS SET prev=57, dest1=59, dest2=-1 WHERE line='Red' AND blockID=58");
                        updateList.Add("UPDATE BLOCKS SET prev=58, dest1=60, dest2=-1 WHERE line='Red' AND blockID=59");
                        updateList.Add("UPDATE BLOCKS SET prev=59, dest1=61, dest2=-1 WHERE line='Red' AND blockID=60");
                        updateList.Add("UPDATE BLOCKS SET prev=60, dest1=62, dest2=-1 WHERE line='Red' AND blockID=61");
                        updateList.Add("UPDATE BLOCKS SET prev=61, dest1=63, dest2=-1 WHERE line='Red' AND blockID=62");
                        updateList.Add("UPDATE BLOCKS SET prev=62, dest1=64, dest2=-1 WHERE line='Red' AND blockID=63");
                        updateList.Add("UPDATE BLOCKS SET prev=63, dest1=65, dest2=-1 WHERE line='Red' AND blockID=64");
                        updateList.Add("UPDATE BLOCKS SET prev=64, dest1=66, dest2=-1 WHERE line='Red' AND blockID=65");
                        updateList.Add("UPDATE BLOCKS SET prev=65, dest1=52, dest2=-1 WHERE line='Red' AND blockID=66");
                        updateList.Add("UPDATE BLOCKS SET prev=43, dest1=68, dest2=-1 WHERE line='Red' AND blockID=67");
                        updateList.Add("UPDATE BLOCKS SET prev=67, dest1=69, dest2=-1 WHERE line='Red' AND blockID=68");
                        updateList.Add("UPDATE BLOCKS SET prev=68, dest1=70, dest2=-1 WHERE line='Red' AND blockID=69");
                        updateList.Add("UPDATE BLOCKS SET prev=69, dest1=71, dest2=-1 WHERE line='Red' AND blockID=70");
                        updateList.Add("UPDATE BLOCKS SET prev=70, dest1=38, dest2=-1 WHERE line='Red' AND blockID=71");
                        updateList.Add("UPDATE BLOCKS SET prev=32, dest1=73, dest2=-1 WHERE line='Red' AND blockID=72");
                        updateList.Add("UPDATE BLOCKS SET prev=72, dest1=74, dest2=-1 WHERE line='Red' AND blockID=73");
                        updateList.Add("UPDATE BLOCKS SET prev=73, dest1=75, dest2=-1 WHERE line='Red' AND blockID=74");
                        updateList.Add("UPDATE BLOCKS SET prev=74, dest1=76, dest2=-1 WHERE line='Red' AND blockID=75");
                        updateList.Add("UPDATE BLOCKS SET prev=75, dest1=27, dest2=-1 WHERE line='Red' AND blockID=76");
		}
		else if(fPath.Contains("green") || fPath.Contains("Green") )//we're the green track
		{

		}
		

		foreach(string s in updateList)
		{
                       try
                       {
			   _dbCon.Open();
                           SqliteCommand updateCommand=new SqliteCommand(s);
                           insertCommand.Connection=_dbCon;

                            int res=insertCommand.ExecuteNonQuery();//Exec update
                            //Console.WriteLine(res);
                            _dbCon.Close();//CLOSE DB
                            if(res!=1)
                                 return -1;
                       }
                       catch(Exception crap)
                       {
                            _dbCon.Close();
                            //Console.WriteLine(crap.Message.ToString());
                            return -1;
                       }

		}
		return 0;//If you get to this point, you've executed successfully.
	}

	public int handleUpdateRequest(int bID, int newBlockSize)
	{
		return -1;
	}

	//Properties
}//End class
}//End namespace
