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
                                   "CONSTRAINT pk_Blocks PRIMARY KEY(blockID) )";

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

				int dest1ID=0;
				int.TryParse(blockID,out dest1ID);
				dest1ID++;
				string dest1Str=dest1ID.ToString();

				//PREV RED LINE HARDCODING
				if(fields[1].Equals("O",StringComparison.OrdinalIgnoreCase))
					prevID="43";
				else if(fields[1].Equals("R",StringComparison.OrdinalIgnoreCase))
					prevID="32";


				//DEST1 RED LINE HARDCODING
				if(blockID.Equals("66",StringComparison.OrdinalIgnoreCase))
					dest1Str="52";
				else if(fields[1].Equals("Q",StringComparison.OrdinalIgnoreCase))
					dest1Str="38";
				else if(fields[1].Equals("T",StringComparison.OrdinalIgnoreCase))
					dest1Str="27";

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
				string singleInsert="INSERT INTO BLOCKS(blockID,prev,dest1, line, infra, starting_elev, grade, bSize,dir,state,trackCirID) VALUES(" +
						blockID+", "+prevID+", "+dest1Str+", '"+lineName+"', '"+infra+"', "+sElev+", "+grade+", "+blockSize+",'North', 'Healthy',-1)";
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
			}//End if statement for valid data lines
		}
		//End for loop iterating through all strings
		////////////////////////////////////////////////////////

		return 0;//If you get to this point, you've executed successfully.
	}

	public int handleUpdateRequest(int bID, int newBlockSize)
	{
		return -1;
	}

	//Properties
}//End class
}//End namespace
