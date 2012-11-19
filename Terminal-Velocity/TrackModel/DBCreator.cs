using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sqlite;
using System.IO;
using Utilities;
using Interfaces;


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
                                   "blockID int NOT NULL," +
                                   "line varchar2(25) NOT NULL," +
                                   "infra varchar2(200)," +
                                   "starting_elev float(25)," +
                                   "grade float(25),"+
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
				string singleInsert="INSERT INTO BLOCKS(blockID, line, infra, starting_elev, grade) VALUES(" +
						blockID+", '"+lineName+"', '"+infra+"', "+sElev+", "+grade+")";
				//Console.WriteLine(singleInsert);

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
