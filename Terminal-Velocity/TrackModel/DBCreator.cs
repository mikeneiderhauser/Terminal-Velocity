using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using Interfaces;

namespace TrackModel
{
    public class DBCreator
    {
        //Private parameters
        private readonly SQLiteConnection _dbCon;

        /// <summary>
        /// A public constructor allowing external modules or TrackModel to create DBCreator objects
        /// </summary>
        /// <param name="fPath">The filepath to the database to connect to</param>
        public DBCreator(string fPath)
        {
            

            //fPath is the filename of the database to connect to.
            //Check filepath's legit'ness, give it the default otherwise
            if (fPath == null || fPath.Equals(""))
            {
                fPath = "trackDatabase.db";
            }

            //Check if the database file exists (before you create it).  If it does, we need
            //to CREATE the necessary tables
            bool needsInitFlag = false;
            if (!File.Exists(fPath))
            {
                needsInitFlag = true;
            }

            //Create a db connection (creating the db file if it doesnt already exist)
            //string connectionString = "Data Source=" + fPath;
            string connectionString="Data Source=" + Path.GetDirectoryName(Application.ExecutablePath)+@"\"+fPath;
            _dbCon = new SQLiteConnection(connectionString);

            //If you needed to create the db tables,do so now
            if (needsInitFlag)
            {
                //Console.WriteLine("About to enter initDB");
                int initResult = initDB();

                //If creating the db failed
                if (initResult == -1)
                {
                    _dbCon = null;
                }
            }
        }

        /// <summary>
        /// An accessor for the DBConnection
        /// </summary>
        public SQLiteConnection DBCon
        {
            get { return _dbCon; }
        }

        private int initDB()
        {
            //BLOCKS TABLE declaration
            string createBLOCKS = "CREATE TABLE BLOCKS (" +
                                  "blockID int NOT NULL," + //
                                  "line varchar2(25) NOT NULL," + //
                                  "infra varchar2(200)," + //
                                  "starting_elev float(25)," + //
                                  "grade float(25)," + //
                                  "locX int," +
                                  "locY int," +
                                  "bSize float(25)," + //
                                  "dir varchar2(50)," + //
                                  "state varchar2(100)," + //
                                  "prev int," + //
                                  "dest1 int," +
                                  "dest2 int," +
                                  "trackCirID int," + //
                                  "speedLimit int," + //
                                  "CONSTRAINT pk_Blocks PRIMARY KEY(blockID,line) )";

            if (_dbCon.State != ConnectionState.Open)
                _dbCon.Open();

            //Initialize command to create BLOCKS TABLE
            var createCommand = new SQLiteCommand(createBLOCKS);
            createCommand.Connection = _dbCon;
            try
            {
                int res = createCommand.ExecuteNonQuery(); //Exec CREATEi
                //Console.WriteLine(res);
                if (_dbCon.State != ConnectionState.Closed)
                    _dbCon.Close(); //CLOSE DB
                if (res != 0)
                    return -1;
            }
            catch (Exception crap)
            {
                if (_dbCon.State != ConnectionState.Closed)
                    _dbCon.Close();
                return -1;
            }


            //After creating the database, first insert the YARD block
            if (_dbCon.State != ConnectionState.Open)
                _dbCon.Open();
            string yardRed =
                "INSERT INTO BLOCKS(blockID, line, infra, starting_elev, grade, locX, locY, bSize, dir, state, prev, dest1, dest2, trackCirID, speedLimit) VALUES(0,'Red','none', 0.0, 0.0, -1, -1, 0.0,'North','Healthy',0,9,-1,3,500)";
            string yardGreen =
                "INSERT INTO BLOCKS(blockID, line, infra, starting_elev, grade, locX, locY, bSize, dir, state, prev, dest1, dest2, trackCirID, speedLimit) VALUES(0,'Green','none',0.0,0.0,-1,-1,0.0,'North','Healthy',0,62,-1,30,500)";
            var insRedCommand = new SQLiteCommand(yardRed);
            var insGreenCommand = new SQLiteCommand(yardGreen);
            insRedCommand.Connection = _dbCon;
            insGreenCommand.Connection = _dbCon;
            try
            {
                int res1 = insRedCommand.ExecuteNonQuery(); //Exec insert yard statement
                int res2 = insGreenCommand.ExecuteNonQuery();
                if (_dbCon.State != ConnectionState.Closed)
                    _dbCon.Close();
                if (res1 != 1 || res2 != 1)
                    return -1;
                else
                    return 0;
            }
            catch (Exception crap)
            {

                if (_dbCon.State != ConnectionState.Closed)
                    _dbCon.Close();
                return -1;
            }
        }


        /// <summary>
        /// A public method, most notably called by the TrackModel, that parses the input file and
        /// requests inserts into the database.
        /// </summary>
        /// <param name="fPath">The path to the file to be parsed</param>
        /// <returns>An integer flag: 0 for success, -3 for a null database, -1 for an invalid path, 
        /// -2 for a file format error</returns>
        public int parseInputFile(string fPath)
        {
            //If our constructor failed, return an error code please
            if (_dbCon == null)
            {
                return -3;
            }

            if (fPath == null)
            {
                //Console.WriteLine("Input fPath was null");
                return -1;
            }

            //Check that file exists
            if (!File.Exists(fPath))
            {
                //Console.WriteLine("File apparently doesnt exist");
                return -1;
            }

            //Check the file format
            if (!fPath.EndsWith(".csv"))
            {
                return -2;
            }

            //Read all information from file into an array of lines
            string[] fileLines = File.ReadAllLines(fPath);


            //Set up character delimiter
            var commaArr = new char[1];
            commaArr[0] = ',';

            //Console.WriteLine("About to enter forloop");
            ////////////////////////////////////////////////////////
            //Iterate through each one.

            List<IBlock>[] trackConBlockLists=new List<IBlock>[16];
            for (int i = 0; i < trackConBlockLists.Length; i++)
            {
                trackConBlockLists[i] = new List<IBlock>();
            }

            foreach (string line in fileLines)
            {
                
                //Console.WriteLine("Line is: "+line);
                //IF we're not a blank line (begins with comma) or a header line (begins with Line)
                //THEN process and insert
                if (!line.StartsWith(",") && !line.StartsWith("Line") && !line.StartsWith(" "))
                {
                    //Delimit around comma's
                    string[] fields = line.Split(commaArr);
                    string blockID = fields[2];
                    string lineName = fields[0];
                    string speedLimit = fields[5];
                    int trackConID=int.Parse(fields[10]);
                    int prev = int.Parse(fields[11]);
                    int d1 = int.Parse(fields[12]);
                    int d2 = int.Parse(fields[13]);
                    string dirString = fields[14];
                    //Once we get our line name, we should check if it's already in the database.
                    //If its already in the database, then we can assume success and just return 0
                    //Maybe this should just be some sort of continue, but I can sort that out later
                    if (blockExists(int.Parse(blockID), lineName))
                    {
                        return 0;
                    }


                    string infra;
                    if (fields[6].Equals("", StringComparison.OrdinalIgnoreCase))
                    {
                        infra = "none";
                    }
                    else
                    {
                        infra = fields[6];
                    }
                    string sElev = fields[9];
                    string grade = fields[4];
                    string blockSize = fields[3];
                    string singleInsert =
                        "INSERT INTO BLOCKS(blockID, line, infra, starting_elev, grade, bSize,dir,state,trackCirID,locX,locY,speedLimit,prev,dest1,dest2) VALUES(" +blockID + ", '" + lineName + "', '" + infra + "', " + sElev + ", " + grade + ", " + blockSize +",'"+dirString+"', 'Healthy',"+trackConID+",-1,-1, "+speedLimit+","+prev+","+d1+","+d2+")";
                    //Console.WriteLine(singleInsert);
                    if (_dbCon.State != ConnectionState.Open)
                        _dbCon.Open();
                    var insertCommand = new SQLiteCommand(singleInsert);
                    insertCommand.Connection = _dbCon;


                    try
                    {
                        int res = insertCommand.ExecuteNonQuery(); //Exec CREATE
                        //Console.WriteLine(res);
                        if (_dbCon.State != ConnectionState.Closed)
                            _dbCon.Close(); //CLOSE DB


                        //Prepare values to create block
                        char[] splitArr = new char[1];
                        splitArr[0] = ';';
                        string[] tempAtts = infra.Split(splitArr);
                        int[] locArray = new int[2];
                        locArray[0] = -1; locArray[1] = -1;

                        //Create block and add to the list of blocks for temp
                        IBlock temp = new Block(int.Parse(blockID), StateEnum.Healthy, prev, double.Parse(sElev), double.Parse(grade),locArray, double.Parse(blockSize), (DirEnum)Enum.Parse(typeof(DirEnum),dirString), tempAtts, d1, d2, trackConID, lineName, int.Parse(speedLimit));
                        trackConBlockLists[trackConID].Add(temp);

                        if (res != 1)
                        {
                            //Console.WriteLine("Database insert failed");
                            return -1;
                        }
                    }
                    catch (Exception crap)
                    {
                        if (_dbCon.State != ConnectionState.Closed)
                            _dbCon.Close();
                        //Console.WriteLine(crap.Message.ToString());
                        return -1;
                    }
                } //End if statement for valid data line
            }
            //End for loop iterating through all strings
            ///////////////////////////////////////////////////



            return 0; //If you get to this point, you've executed successfully.
        }

        /// <summary>
        /// A deprecated public method used to handle update requests.  Database updates have been moved
        /// into the DBManager's domain, and as such this method should not be used. 
        /// </summary>
        /// <param name="bID">The block ID of the block to be updated</param>
        /// <param name="newBlockSize">The new block size of the block</param>
        /// <returns>An integer flag denoting the success or failure of the operation: 0 denotes success, -1 denotes failure</returns>
        public int handleUpdateRequest(int bID, int newBlockSize)
        {
            return -1;
        }


        private bool blockExists(int id, string line)
        {
            string selectString = "SELECT *" +
                                  " FROM BLOCKS" +
                                  " WHERE blockID=" + id + " AND line='" + line + "'";

            if (_dbCon.State != ConnectionState.Open)
                _dbCon.Open();
            //Initialize command to create BLOCKS TABLE
            var selCom = new SQLiteCommand(selectString);
            selCom.Connection = _dbCon;
            try
            {
                SQLiteDataReader tempReader = selCom.ExecuteReader();
                bool exists = tempReader.HasRows;
                tempReader.Close(); //Close reader
                if (_dbCon.State != ConnectionState.Closed)
                    _dbCon.Close(); //CLOSE DB
                return exists;
            }
            catch (Exception crap)
            {
                if (_dbCon.State != ConnectionState.Closed)
                    _dbCon.Close();
                return false;
            }
        }

        //Properties
    }

//End class
}

//End namespace