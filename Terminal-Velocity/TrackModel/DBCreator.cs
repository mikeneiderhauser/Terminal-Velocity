using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.IO;
using Interfaces;
using Utility;

namespace TrackModel
{
    public class DBCreator
    {
        //Private parameters
	private SQLiteConnection _dbCon;
    private static int _curCirID=-1;
    private static List<TrackController.TrackCircuit> _trackCirList=null;
    private ISimulationEnvironment _env;

        public DBCreator(string fPath,ISimulationEnvironment environment)
        {
            //Store environment reference
            _env = environment;

            //Init initial circuitID
            if (_curCirID == -1)
            {
                _curCirID = 0;
                _trackCirList=new List<TrackController.TrackCircuit>();
            }


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
		_dbCon=new SQLiteConnection(connectionString);

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
			SQLiteCommand createCommand=new SQLiteCommand(createBLOCKS);
			createCommand.Connection=_dbCon;
			try
			{
				int res=createCommand.ExecuteNonQuery();//Exec CREATEi
				//Console.WriteLine(res);
				_dbCon.Close();//CLOSE DB
				if(res!=0)
					return -1;
			}
			catch(Exception crap)
			{
				_dbCon.Close();
				return -1;
			}


			//After creating the database, first insert the YARD block
			_dbCon.Open();
			string yardIns="INSERT INTO BLOCKS(blockID, line, infra) VALUES(0,'YARD','NONE')";
			SQLiteCommand insCommand=new SQLiteCommand(yardIns);
			insCommand.Connection=_dbCon;
			try
			{
				int res=insCommand.ExecuteNonQuery();//Exec insert yard statement
				_dbCon.Close();
				if(res!=1)
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

        //Used to compare against next section
        string curSection = "";

		if(_dbCon==null)
		{
			return -3;
		}

		if(fPath==null)
		{
            //Console.WriteLine("Input fPath was null");
			return -1;
		}

		//Check that file exists
		if(!File.Exists(fPath))
		{
            //Console.WriteLine("File apparently doesnt exist");
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


                //Once we get our line name, we should check if it's already in the database.
                //If its already in the database, then we can assume success and just return 0
                //Maybe this should just be some sort of continue, but I can sort that out later
                if (blockExists(int.Parse(blockID), line))
                {
                    return 0;
                }

		
		
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


                int trackCirID = -1;
                //Initialize trackCirID field for each block
                if (curSection.Equals(fields[1], StringComparison.OrdinalIgnoreCase))
                {
                    trackCirID = _curCirID;
                }
                else
                {
                    //Update ID information
                    _curCirID++;
                    trackCirID = _curCirID;
                    curSection = fields[1];

                    TrackController.TrackCircuit temp = new TrackController.TrackCircuit(_env);
                    TrackController.TrackController tempCon = new TrackController.TrackController(_env,temp);
                    _trackCirList.Add(temp);
                }


				string singleInsert="INSERT INTO BLOCKS(blockID, line, infra, starting_elev, grade, bSize,dir,state,trackCirID) VALUES(" +
						blockID+", '"+lineName+"', '"+infra+"', "+sElev+", "+grade+", "+blockSize+",'North', 'Healthy',"+trackCirID+")";
				//Console.WriteLine(singleInsert);
				prevID=blockID;
				_dbCon.Open();
	                	        SQLiteCommand insertCommand=new SQLiteCommand(singleInsert);
        		                insertCommand.Connection=_dbCon;



		                        try
                		        {
                                		int res=insertCommand.ExecuteNonQuery();//Exec CREATE
						//Console.WriteLine(res);
         	                        	_dbCon.Close();//CLOSE DB
                                        if (res != 1)
                                        {
                                            //Console.WriteLine("Database insert failed");
                                            return -1;
                                        }
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
			updateList.Add("UPDATE BLOCKS SET prev=15, dest1=2, dest2=-1,dir='Northeast_AND_Southwest' WHERE line='Red' AND blockID=1");
                        updateList.Add("UPDATE BLOCKS SET prev=1, dest1=3, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Red' AND blockID=2");
                        updateList.Add("UPDATE BLOCKS SET prev=2, dest1=4, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Red' AND blockID=3");
                        updateList.Add("UPDATE BLOCKS SET prev=3, dest1=5, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=4");
                        updateList.Add("UPDATE BLOCKS SET prev=4, dest1=6, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=5");
                        updateList.Add("UPDATE BLOCKS SET prev=5, dest1=7, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=6");
                        updateList.Add("UPDATE BLOCKS SET prev=6, dest1=8, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=7");
                        updateList.Add("UPDATE BLOCKS SET prev=7, dest1=9, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Red' AND blockID=8");
                        updateList.Add("UPDATE BLOCKS SET prev=8, dest1=10, dest2=0, dir='North_AND_South' WHERE line='Red' AND blockID=9");
                        updateList.Add("UPDATE BLOCKS SET prev=9, dest1=11, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Red' AND blockID=10");
                        updateList.Add("UPDATE BLOCKS SET prev=10, dest1=12, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=11");
                        updateList.Add("UPDATE BLOCKS SET prev=11, dest1=13, dest2=-1, dir='East_AND_West'  WHERE line='Red' AND blockID=12");
                        updateList.Add("UPDATE BLOCKS SET prev=12, dest1=14, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=13");
                        updateList.Add("UPDATE BLOCKS SET prev=13, dest1=15, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=14");
                        updateList.Add("UPDATE BLOCKS SET prev=16, dest1=1, dest2=14, dir='East_AND_West' WHERE line='Red' AND blockID=15");
                        updateList.Add("UPDATE BLOCKS SET prev=15, dest1=17, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=16");
                        updateList.Add("UPDATE BLOCKS SET prev=16, dest1=18, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=17");
                        updateList.Add("UPDATE BLOCKS SET prev=17, dest1=19, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=18");
                        updateList.Add("UPDATE BLOCKS SET prev=18, dest1=20, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=19");
                        updateList.Add("UPDATE BLOCKS SET prev=19, dest1=21, dest2=-1, dir='East_AND_West'  WHERE line='Red' AND blockID=20");
                        updateList.Add("UPDATE BLOCKS SET prev=20, dest1=22, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=21");
                        updateList.Add("UPDATE BLOCKS SET prev=21, dest1=23, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Red' AND blockID=22");
                        updateList.Add("UPDATE BLOCKS SET prev=22, dest1=24, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=23");
                        updateList.Add("UPDATE BLOCKS SET prev=23, dest1=25, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=24");
                        updateList.Add("UPDATE BLOCKS SET prev=24, dest1=26, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=25");
                        updateList.Add("UPDATE BLOCKS SET prev=25, dest1=27, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=26");
                        updateList.Add("UPDATE BLOCKS SET prev=26, dest1=28, dest2=76, dir='North_AND_South' WHERE line='Red' AND blockID=27");
                        updateList.Add("UPDATE BLOCKS SET prev=27, dest1=29, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=28");
                        updateList.Add("UPDATE BLOCKS SET prev=28, dest1=30, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=29");
                        updateList.Add("UPDATE BLOCKS SET prev=29, dest1=31, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=30");
                        updateList.Add("UPDATE BLOCKS SET prev=30, dest1=32, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=31");
                        updateList.Add("UPDATE BLOCKS SET prev=33, dest1=31, dest2=72, dir='North_AND_South' WHERE line='Red' AND blockID=32");
                        updateList.Add("UPDATE BLOCKS SET prev=32, dest1=34, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=33");
                        updateList.Add("UPDATE BLOCKS SET prev=33, dest1=35, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=34");
                        updateList.Add("UPDATE BLOCKS SET prev=34, dest1=36, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=35");
                        updateList.Add("UPDATE BLOCKS SET prev=35, dest1=37, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=36");
                        updateList.Add("UPDATE BLOCKS SET prev=36, dest1=38, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=37");
                        updateList.Add("UPDATE BLOCKS SET prev=37, dest1=39, dest2=71, dir='North_AND_South' WHERE line='Red' AND blockID=38");
                        updateList.Add("UPDATE BLOCKS SET prev=38, dest1=40, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=39");
                        updateList.Add("UPDATE BLOCKS SET prev=39, dest1=41, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=40");
                        updateList.Add("UPDATE BLOCKS SET prev=40, dest1=42, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=41");
                        updateList.Add("UPDATE BLOCKS SET prev=41, dest1=43, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=42");
                        updateList.Add("UPDATE BLOCKS SET prev=44, dest1=42, dest2=67, dir='North_AND_South' WHERE line='Red' AND blockID=43");
                        updateList.Add("UPDATE BLOCKS SET prev=43, dest1=45, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=44");
                        updateList.Add("UPDATE BLOCKS SET prev=44, dest1=46, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=45");
                        updateList.Add("UPDATE BLOCKS SET prev=45, dest1=47, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=46");
                        updateList.Add("UPDATE BLOCKS SET prev=46, dest1=48, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Red' AND blockID=47");
                        updateList.Add("UPDATE BLOCKS SET prev=47, dest1=49, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Red' AND blockID=48");
                        updateList.Add("UPDATE BLOCKS SET prev=48, dest1=50, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=49");
                        updateList.Add("UPDATE BLOCKS SET prev=49, dest1=51, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=50");
                        updateList.Add("UPDATE BLOCKS SET prev=50, dest1=52, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=51");
                        updateList.Add("UPDATE BLOCKS SET prev=51, dest1=53, dest2=66, dir='East_AND_West' WHERE line='Red' AND blockID=52");
                        updateList.Add("UPDATE BLOCKS SET prev=52, dest1=54, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=53");
                        updateList.Add("UPDATE BLOCKS SET prev=53, dest1=55, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=54");
                        updateList.Add("UPDATE BLOCKS SET prev=54, dest1=56, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Red' AND blockID=55");
                        updateList.Add("UPDATE BLOCKS SET prev=55, dest1=57, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Red' AND blockID=56");
                        updateList.Add("UPDATE BLOCKS SET prev=56, dest1=58, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=57");
                        updateList.Add("UPDATE BLOCKS SET prev=57, dest1=59, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=58");
                        updateList.Add("UPDATE BLOCKS SET prev=58, dest1=60, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Red' AND blockID=59");
                        updateList.Add("UPDATE BLOCKS SET prev=59, dest1=61, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=60");
                        updateList.Add("UPDATE BLOCKS SET prev=60, dest1=62, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Red' AND blockID=61");
                        updateList.Add("UPDATE BLOCKS SET prev=61, dest1=63, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Red' AND blockID=62");
                        updateList.Add("UPDATE BLOCKS SET prev=62, dest1=64, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=63");
                        updateList.Add("UPDATE BLOCKS SET prev=63, dest1=65, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=64");
                        updateList.Add("UPDATE BLOCKS SET prev=64, dest1=66, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Red' AND blockID=65");
                        updateList.Add("UPDATE BLOCKS SET prev=65, dest1=52, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Red' AND blockID=66");
                        updateList.Add("UPDATE BLOCKS SET prev=43, dest1=68, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=67");
                        updateList.Add("UPDATE BLOCKS SET prev=67, dest1=69, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=68");
                        updateList.Add("UPDATE BLOCKS SET prev=68, dest1=70, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=69");
                        updateList.Add("UPDATE BLOCKS SET prev=69, dest1=71, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=70");
                        updateList.Add("UPDATE BLOCKS SET prev=70, dest1=38, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=71");
                        updateList.Add("UPDATE BLOCKS SET prev=32, dest1=73, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=72");
                        updateList.Add("UPDATE BLOCKS SET prev=72, dest1=74, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=73");
                        updateList.Add("UPDATE BLOCKS SET prev=73, dest1=75, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=74");
                        updateList.Add("UPDATE BLOCKS SET prev=74, dest1=76, dest2=-1, dir='North_AND_South' WHERE line='Red' AND blockID=75");
                        updateList.Add("UPDATE BLOCKS SET prev=75, dest1=27, dest2=-1, dir='East_AND_West' WHERE line='Red' AND blockID=76");
		}
		else if(fPath.Contains("green") || fPath.Contains("Green") )//we're the green track
		{
                        updateList.Add("UPDATE BLOCKS SET prev=12, dest1=2, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=1");
                        updateList.Add("UPDATE BLOCKS SET prev=1, dest1=3, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=2");
                        updateList.Add("UPDATE BLOCKS SET prev=2, dest1=4, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=3");
                        updateList.Add("UPDATE BLOCKS SET prev=3, dest1=5, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=4");
                        updateList.Add("UPDATE BLOCKS SET prev=4, dest1=6, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=5");
                        updateList.Add("UPDATE BLOCKS SET prev=5, dest1=7, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=6");
                        updateList.Add("UPDATE BLOCKS SET prev=6, dest1=8, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=7");
                        updateList.Add("UPDATE BLOCKS SET prev=7, dest1=9, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=8");
                        updateList.Add("UPDATE BLOCKS SET prev=8, dest1=10, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=9");
                        updateList.Add("UPDATE BLOCKS SET prev=9, dest1=11, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=10");
                        updateList.Add("UPDATE BLOCKS SET prev=10, dest1=12, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=11");
                        updateList.Add("UPDATE BLOCKS SET prev=13, dest1=11, dest2=1, dir='East_AND_West' WHERE line='Green' AND blockID=12");
                        updateList.Add("UPDATE BLOCKS SET prev=12, dest1=14, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=13");
                        updateList.Add("UPDATE BLOCKS SET prev=13, dest1=15, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=14");
                        updateList.Add("UPDATE BLOCKS SET prev=14, dest1=16, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=15");
                        updateList.Add("UPDATE BLOCKS SET prev=15, dest1=17, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=16");
                        updateList.Add("UPDATE BLOCKS SET prev=16, dest1=18, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=17");
                        updateList.Add("UPDATE BLOCKS SET prev=17, dest1=19, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=18");
                        updateList.Add("UPDATE BLOCKS SET prev=18, dest1=20, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=19");
                        updateList.Add("UPDATE BLOCKS SET prev=19, dest1=21, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=20");
                        updateList.Add("UPDATE BLOCKS SET prev=20, dest1=22, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=21");
                        updateList.Add("UPDATE BLOCKS SET prev=21, dest1=23, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=22");
                        updateList.Add("UPDATE BLOCKS SET prev=22, dest1=24, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=23");
                        updateList.Add("UPDATE BLOCKS SET prev=23, dest1=25, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=24");
                        updateList.Add("UPDATE BLOCKS SET prev=24, dest1=26, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=25");
                        updateList.Add("UPDATE BLOCKS SET prev=25, dest1=27, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=26");
                        updateList.Add("UPDATE BLOCKS SET prev=26, dest1=28, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=27");
                        updateList.Add("UPDATE BLOCKS SET prev=27, dest1=29, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=28");
                        updateList.Add("UPDATE BLOCKS SET prev=28, dest1=30, dest2=150, dir='North_AND_South' WHERE line='Green' AND blockID=29");
                        updateList.Add("UPDATE BLOCKS SET prev=29, dest1=31, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=30");
                        updateList.Add("UPDATE BLOCKS SET prev=30, dest1=32, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=31");
                        updateList.Add("UPDATE BLOCKS SET prev=31, dest1=33, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=32");
                        updateList.Add("UPDATE BLOCKS SET prev=32, dest1=34, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=33");
                        updateList.Add("UPDATE BLOCKS SET prev=33, dest1=35, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=34");
                        updateList.Add("UPDATE BLOCKS SET prev=34, dest1=36, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=35");
                        updateList.Add("UPDATE BLOCKS SET prev=35, dest1=37, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=36");
                        updateList.Add("UPDATE BLOCKS SET prev=36, dest1=38, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=37");
                        updateList.Add("UPDATE BLOCKS SET prev=37, dest1=39, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=38");
                        updateList.Add("UPDATE BLOCKS SET prev=38, dest1=40, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=39");
                        updateList.Add("UPDATE BLOCKS SET prev=39, dest1=41, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=40");
                        updateList.Add("UPDATE BLOCKS SET prev=40, dest1=42, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=41");
                        updateList.Add("UPDATE BLOCKS SET prev=41, dest1=43, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=42");
                        updateList.Add("UPDATE BLOCKS SET prev=42, dest1=44, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=43");
                        updateList.Add("UPDATE BLOCKS SET prev=43, dest1=45, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=44");
                        updateList.Add("UPDATE BLOCKS SET prev=44, dest1=46, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=45");
                        updateList.Add("UPDATE BLOCKS SET prev=45, dest1=47, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=46");
                        updateList.Add("UPDATE BLOCKS SET prev=46, dest1=48, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=47");
                        updateList.Add("UPDATE BLOCKS SET prev=47, dest1=49, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=48");
                        updateList.Add("UPDATE BLOCKS SET prev=48, dest1=50, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=49");
                        updateList.Add("UPDATE BLOCKS SET prev=49, dest1=51, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=50");
                        updateList.Add("UPDATE BLOCKS SET prev=50, dest1=52, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=51");
                        updateList.Add("UPDATE BLOCKS SET prev=51, dest1=53, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=52");
                        updateList.Add("UPDATE BLOCKS SET prev=52, dest1=54, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=53");
                        updateList.Add("UPDATE BLOCKS SET prev=53, dest1=55, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=54");
                        updateList.Add("UPDATE BLOCKS SET prev=54, dest1=56, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=55");
                        updateList.Add("UPDATE BLOCKS SET prev=55, dest1=57, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=56");
                        updateList.Add("UPDATE BLOCKS SET prev=56, dest1=58, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=57");
                        updateList.Add("UPDATE BLOCKS SET prev=57, dest1=59, dest2=0, dir='East_AND_West' WHERE line='Green' AND blockID=58");
                        updateList.Add("UPDATE BLOCKS SET prev=58, dest1=60, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=59");
                        updateList.Add("UPDATE BLOCKS SET prev=59, dest1=61, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=60");
                        updateList.Add("UPDATE BLOCKS SET prev=60, dest1=62, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=61");
                        updateList.Add("UPDATE BLOCKS SET prev=63, dest1=61, dest2=0, dir='North_AND_South' WHERE line='Green' AND blockID=62");
                        updateList.Add("UPDATE BLOCKS SET prev=62, dest1=64, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=63");
                        updateList.Add("UPDATE BLOCKS SET prev=63, dest1=65, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=64");
                        updateList.Add("UPDATE BLOCKS SET prev=64, dest1=66, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=65");
                        updateList.Add("UPDATE BLOCKS SET prev=65, dest1=67, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=66");
                        updateList.Add("UPDATE BLOCKS SET prev=66, dest1=68, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=67");
                        updateList.Add("UPDATE BLOCKS SET prev=67, dest1=69, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=68");
                        updateList.Add("UPDATE BLOCKS SET prev=68, dest1=70, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=69");
                        updateList.Add("UPDATE BLOCKS SET prev=69, dest1=71, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=70");
                        updateList.Add("UPDATE BLOCKS SET prev=70, dest1=72, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=71");
                        updateList.Add("UPDATE BLOCKS SET prev=71, dest1=73, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=72");
                        updateList.Add("UPDATE BLOCKS SET prev=72, dest1=74, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=73");
                        updateList.Add("UPDATE BLOCKS SET prev=73, dest1=75, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=74");
                        updateList.Add("UPDATE BLOCKS SET prev=74, dest1=76, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=75");
                        updateList.Add("UPDATE BLOCKS SET prev=77, dest1=75, dest2=101, dir='East_AND_West' WHERE line='Green' AND blockID=76");
                        updateList.Add("UPDATE BLOCKS SET prev=76, dest1=78, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=77");
                        updateList.Add("UPDATE BLOCKS SET prev=77, dest1=79, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=78");
                        updateList.Add("UPDATE BLOCKS SET prev=78, dest1=80, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=79");
                        updateList.Add("UPDATE BLOCKS SET prev=79, dest1=81, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=80");
                        updateList.Add("UPDATE BLOCKS SET prev=80, dest1=82, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=81");
                        updateList.Add("UPDATE BLOCKS SET prev=81, dest1=83, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=82");
                        updateList.Add("UPDATE BLOCKS SET prev=82, dest1=84, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=83");
                        updateList.Add("UPDATE BLOCKS SET prev=83, dest1=85, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=84");
                        updateList.Add("UPDATE BLOCKS SET prev=84, dest1=86, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=85");
                        updateList.Add("UPDATE BLOCKS SET prev=85, dest1=87, dest2=100, dir='East_AND_West' WHERE line='Green' AND blockID=86");
                        updateList.Add("UPDATE BLOCKS SET prev=86, dest1=88, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=87");
                        updateList.Add("UPDATE BLOCKS SET prev=87, dest1=89, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=88");
                        updateList.Add("UPDATE BLOCKS SET prev=88, dest1=90, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=89");
                        updateList.Add("UPDATE BLOCKS SET prev=89, dest1=91, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=90");
                        updateList.Add("UPDATE BLOCKS SET prev=90, dest1=92, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=91");
                        updateList.Add("UPDATE BLOCKS SET prev=91, dest1=93, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=92");
                        updateList.Add("UPDATE BLOCKS SET prev=92, dest1=94, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=93");
                        updateList.Add("UPDATE BLOCKS SET prev=93, dest1=95, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=94");
                        updateList.Add("UPDATE BLOCKS SET prev=94, dest1=96, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=95");
                        updateList.Add("UPDATE BLOCKS SET prev=95, dest1=97, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=96");
                        updateList.Add("UPDATE BLOCKS SET prev=96, dest1=98, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=97");
                        updateList.Add("UPDATE BLOCKS SET prev=97, dest1=99, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=98");
                        updateList.Add("UPDATE BLOCKS SET prev=98, dest1=100, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=99");
                        updateList.Add("UPDATE BLOCKS SET prev=99, dest1=86, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=100");
                        updateList.Add("UPDATE BLOCKS SET prev=76, dest1=102, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=101");
                        updateList.Add("UPDATE BLOCKS SET prev=101, dest1=103, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=102");
                        updateList.Add("UPDATE BLOCKS SET prev=102, dest1=104, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=103");
                        updateList.Add("UPDATE BLOCKS SET prev=103, dest1=105, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=104");
                        updateList.Add("UPDATE BLOCKS SET prev=104, dest1=106, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=105");
                        updateList.Add("UPDATE BLOCKS SET prev=105, dest1=107, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=106");
                        updateList.Add("UPDATE BLOCKS SET prev=106, dest1=108, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=107");
                        updateList.Add("UPDATE BLOCKS SET prev=107, dest1=109, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=108");
                        updateList.Add("UPDATE BLOCKS SET prev=108, dest1=110, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=109");
                        updateList.Add("UPDATE BLOCKS SET prev=109, dest1=111, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=110");
                        updateList.Add("UPDATE BLOCKS SET prev=110, dest1=112, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=111");
                        updateList.Add("UPDATE BLOCKS SET prev=111, dest1=113, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=112");
                        updateList.Add("UPDATE BLOCKS SET prev=112, dest1=114, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=113");
                        updateList.Add("UPDATE BLOCKS SET prev=113, dest1=115, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=114");
                        updateList.Add("UPDATE BLOCKS SET prev=114, dest1=116, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=115");
                        updateList.Add("UPDATE BLOCKS SET prev=115, dest1=117, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=116");
                        updateList.Add("UPDATE BLOCKS SET prev=116, dest1=118, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=117");
                        updateList.Add("UPDATE BLOCKS SET prev=117, dest1=119, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=118");
                        updateList.Add("UPDATE BLOCKS SET prev=118, dest1=120, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=119");
                        updateList.Add("UPDATE BLOCKS SET prev=119, dest1=121, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=120");
                        updateList.Add("UPDATE BLOCKS SET prev=120, dest1=122, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=121");
                        updateList.Add("UPDATE BLOCKS SET prev=121, dest1=123, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=122");
                        updateList.Add("UPDATE BLOCKS SET prev=122, dest1=124, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=123");
                        updateList.Add("UPDATE BLOCKS SET prev=123, dest1=125, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=124");
                        updateList.Add("UPDATE BLOCKS SET prev=124, dest1=126, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=125");
                        updateList.Add("UPDATE BLOCKS SET prev=125, dest1=127, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=126");
                        updateList.Add("UPDATE BLOCKS SET prev=126, dest1=128, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=127");
                        updateList.Add("UPDATE BLOCKS SET prev=127, dest1=129, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=128");
                        updateList.Add("UPDATE BLOCKS SET prev=128, dest1=130, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=129");
                        updateList.Add("UPDATE BLOCKS SET prev=129, dest1=131, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=130");
                        updateList.Add("UPDATE BLOCKS SET prev=130, dest1=132, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=131");
                        updateList.Add("UPDATE BLOCKS SET prev=131, dest1=133, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=132");
                        updateList.Add("UPDATE BLOCKS SET prev=132, dest1=134, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=133");
                        updateList.Add("UPDATE BLOCKS SET prev=133, dest1=135, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=134");
                        updateList.Add("UPDATE BLOCKS SET prev=134, dest1=136, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=135");
                        updateList.Add("UPDATE BLOCKS SET prev=135, dest1=137, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=136");
                        updateList.Add("UPDATE BLOCKS SET prev=136, dest1=138, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=137");
                        updateList.Add("UPDATE BLOCKS SET prev=137, dest1=139, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=138");
                        updateList.Add("UPDATE BLOCKS SET prev=138, dest1=140, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=139");
                        updateList.Add("UPDATE BLOCKS SET prev=139, dest1=141, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=140");
                        updateList.Add("UPDATE BLOCKS SET prev=140, dest1=142, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=141");
                        updateList.Add("UPDATE BLOCKS SET prev=141, dest1=143, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=142");
                        updateList.Add("UPDATE BLOCKS SET prev=142, dest1=144, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=143");
                        updateList.Add("UPDATE BLOCKS SET prev=143, dest1=145, dest2=-1, dir='East_AND_West' WHERE line='Green' AND blockID=144");
                        updateList.Add("UPDATE BLOCKS SET prev=144, dest1=146, dest2=-1, dir='Northwest_AND_Southeast' WHERE line='Green' AND blockID=145");
                        updateList.Add("UPDATE BLOCKS SET prev=145, dest1=147, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=146");
                        updateList.Add("UPDATE BLOCKS SET prev=146, dest1=148, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=147");
                        updateList.Add("UPDATE BLOCKS SET prev=147, dest1=149, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=148");
                        updateList.Add("UPDATE BLOCKS SET prev=148, dest1=150, dest2=-1, dir='North_AND_South' WHERE line='Green' AND blockID=149");
                        updateList.Add("UPDATE BLOCKS SET prev=149, dest1=29, dest2=-1, dir='Northeast_AND_Southwest' WHERE line='Green' AND blockID=150");
		}


		

		foreach(string s in updateList)
		{
                       try
                       {
			   _dbCon.Open();
                           SQLiteCommand updateCommand=new SQLiteCommand(s);
                           updateCommand.Connection=_dbCon;

                            int res=updateCommand.ExecuteNonQuery();//Exec update
                            //Console.WriteLine(res);
                            _dbCon.Close();//CLOSE DB
                            if (res != 1)
                            {
                                //Console.WriteLine("Database UPDATE failed");
                                return -1;
                            }
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



    private bool blockExists(int id, string line)
    {
        string selectString = "SELECT *" +
                    "FROM BLOCKS" +
                    "WHERE blockID=" + id + " AND line='" + line + "'";

        _dbCon.Open();
        //Initialize command to create BLOCKS TABLE
        SQLiteCommand selCom = new SQLiteCommand(selectString);
        selCom.Connection = _dbCon;
        try
        {
            SQLiteDataReader tempReader = selCom.ExecuteReader();
            bool exists = tempReader.HasRows;
            tempReader.Close();//Close reader
            _dbCon.Close();//CLOSE DB
            return exists;
        }
        catch (Exception crap)
        {
            _dbCon.Close();
            return false;
        }
    }

	//Properties
        public SQLiteConnection DBCon
        {
            get { return _dbCon; }
        }

}//End class
}//End namespace
