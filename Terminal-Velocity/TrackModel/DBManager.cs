using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Interfaces;

namespace TrackModel
{
    public class DBManager
    {
        //Private parameters
        private readonly SQLiteConnection _dbCon;

        /// <summary>
        /// A public constructor allowing the Track Model or outside modules to create a DB Manager
        /// </summary>
        /// <param name="con">A connection to an SQLite database</param>
        public DBManager(SQLiteConnection con)
        {
            _dbCon = con;
        }


        /// <summary>
        /// A public method which creates a SQL query for use on the database
        /// </summary>
        /// <param name="qType">A string representing the query type: either BLOCK or ROUTE</param>
        /// <param name="ID"> The ID of the object required</param>
        /// <param name="line">The name of the line required: either "Red" or "Green"</param>
        /// <returns>The SQL query stored as a string</returns>
        public String createQueryString(string qType, int ID, string line)
        {
            //Check basic ID validity
            //We need to allow zero's--> Route 0 is red, and Block 0 is yard


            if (ID < 0)
            {
                return null;
            }

            if (!qType.Equals("BLOCK", StringComparison.OrdinalIgnoreCase) &&
                !qType.Equals("ROUTE", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (qType.Equals("BLOCK", StringComparison.OrdinalIgnoreCase)) //Format for block
            {
                //Test whether block exists
                if (!line.Equals("Red", StringComparison.OrdinalIgnoreCase) &&
                    !line.Equals("Green", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }
                bool exists = blockExists(ID, line);
                if (exists)
                {
                    string blockQuery = "SELECT *" +
                                        "FROM BLOCKS " +
                                        "WHERE blockID=" + ID + " AND line='" + line + "'";
                    return blockQuery;
                }
                else
                    return null;
            }
            else //(qType.Equals("ROUTE",StringComparison.OrdinalIgnoreCase))//Format for ROUTE
            {
                //Only accept 0 or 1 for route id's
                string routeName;
                if (ID == 0)
                {
                    routeName = "Red";
                }
                else if (ID == 1)
                {
                    routeName = "Green";
                }
                else
                {
                    //Only supports red and green lines
                    return null;
                }

                //Routes right now only exists as the set of
                //BLOCK table tuples sharing a line name.
                string routeQuery = "SELECT *" +
                                    "FROM BLOCKS " +
                                    "WHERE line='" + routeName + "'";
                return routeQuery;
            }
        }

        private bool routeExists(int id)
        {
            string selectString = "SELECT *" +
                                  "FROM ROUTES " +
                                  "WHERE routeID=" + id;

            if (_dbCon.State != ConnectionState.Open)
                _dbCon.Open();
            //Initialize command to create BLOCKS TABLE
            var selCom = new SQLiteCommand(selectString);
            selCom.Connection = _dbCon;
            try
            {
                SQLiteDataReader tempReader = selCom.ExecuteReader();
                bool exists = tempReader.HasRows;
                //tempReader.Close(); //Close reader
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close(); //CLOSE DB
                return exists;
            }
            catch (Exception)
            {
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close();
                return false;
            }
        }

        private bool blockExists(int id, string line)
        {
            string selectString = "SELECT *" +
                                  "FROM BLOCKS " +
                                  "WHERE blockID=" + id + " AND line='" + line + "'";
            if (_dbCon.State != ConnectionState.Open)
                _dbCon.Open();
            //Initialize command to create BLOCKS TABLE
            var selCom = new SQLiteCommand(selectString);
            selCom.Connection = _dbCon;
            try
            {
                SQLiteDataReader tempReader = selCom.ExecuteReader();
                bool exists = tempReader.HasRows;
                //tempReader.Close(); //Close reader
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close(); //CLOSE DB
                return exists;
            }
            catch (Exception)
            {
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close();
                return false;
            }
        }

        //Allows updates types of "SWITCH" or "BLOCK"
        //	SWITCH updates only affect the switch
        //	BLOCK updates are allowed to update state info, track circuit info, heater info

        /// <summary>
        /// A public method allowing modules to create an SQL update statement
        /// </summary>
        /// <param name="updateType">A string representing the update type: either "SWITCH" or "BLOCK"</param>
        /// <param name="bToUpdate">An IBlock object holding changed values needed to update the database</param>
        /// <returns>A SQL update statement in the form of a string</returns>
        public String createUpdate(string updateType, IBlock bToUpdate)
        {
            //Check that block isnt null
            if (bToUpdate == null)
                return null;

            //Get block ID and check that it exists
            int bID = bToUpdate.BlockID;
            string line = bToUpdate.Line;
            bool exists = blockExists(bID,line);
            if (!exists)
                return null;

            //if updateType was an unexpected value, quit out
            if (!updateType.Equals("SWITCH", StringComparison.OrdinalIgnoreCase) &&
                !updateType.Equals("BLOCK", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            else if (updateType.Equals("SWITCH", StringComparison.OrdinalIgnoreCase))
            {
                //Create switch update string
                string updateString = "UPDATE BLOCKS " +
                                      "SET dest1=" + bToUpdate.SwitchDest1 + ", dest2=" + bToUpdate.SwitchDest2 +
                                      " WHERE blockID=" + bID + " AND line='" + line + "'";
                return updateString;
            }
            else //updateType.Equals("BLOCK",StringComparison.OrdinalIgnoreCase)
            {
                //blocks are allowed to update heater, track circuit info, state
                //To get the heater information, we have to turn the whole attributes
                //array of the block into a string for putting in the DB
                string attrString = "";

                //Assemble attribute string from split array
                //	--will be put in database
                string[] tempArr = bToUpdate.AttrArray;
                for (int i = 0; i < tempArr.Length; i++)
                {
                    if(string.IsNullOrWhiteSpace(tempArr[i])==false)
                        attrString = attrString + tempArr[i] + "; ";
                }


                //Create block update string			
                string updateString = "UPDATE BLOCKS " +
                                      "SET state='" + bToUpdate.State + "', trackCirID=" + bToUpdate.TrackCirID +
                                      ", infra='" + attrString + "' " +
                                      " WHERE blockID=" + bID + " AND line='" + line + "'";
                return updateString;
            }
        }

        /// <summary>
        /// A public method used for creating valid insert statements on the database.  This method was
        /// deprecated early on, and should not be used.  The DBCreator class should instead by used for
        /// inserting into the database.
        /// </summary>
        /// <param name="b">The block to be inserted into the database</param>
        /// <returns>A SQL insert statement stored as a string</returns>
        public String createInsert(Block b)
        {
            //Looking back, im not sure why I thought I needed this method.
            //DB insertion should be done entirely through the DBCreator and not through the DBManager

            //I will hold off on implementing this method until I'm certain I need to
            return null;
        }


        /// <summary>
        /// A public method allowing outside modules to run valid SQL queries.  The only supported queries
        /// are those created with the create* methods included in this DBManager class
        /// </summary>
        /// <param name="sqlQuery">A sqlQuery provided by the createQuery method</param>
        /// <returns>A SQLiteDataReader object holding the results of the query</returns>
        public SQLiteDataReader runQuery(string sqlQuery)
        {
            if (_dbCon.State != ConnectionState.Open)
                _dbCon.Open();
            //Initialize command to create BLOCKS TABLE
            var selCom = new SQLiteCommand(sqlQuery);
            selCom.Connection = _dbCon;
            try
            {
                SQLiteDataReader tempReader = selCom.ExecuteReader();
                //_dbCon.Close();//CLOSE DB
                return tempReader;
            }
            catch (Exception)
            {
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close();
                return null;
            }
        }


        /// <summary>
        /// A public method allowing outside modules, and the Track Model to run updates to the database
        /// </summary>
        /// <param name="sqlUpdate">A SQL Update provided by the createUpdate method in the DBManager</param>
        /// <returns>A boolean value denoting the success or failure of the update</returns>
        public bool runUpdate(string sqlUpdate)
        {
            if (_dbCon == null)
                return false;

            if (_dbCon.State != ConnectionState.Open)
                _dbCon.Open();
            var updateCom = new SQLiteCommand(sqlUpdate);
            updateCom.Connection = _dbCon;
            try
            {
                int res = updateCom.ExecuteNonQuery(); //Exec CREATE
                //Console.WriteLine(res);
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close(); //CLOSE DB
                if (res != 1)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close();
                //Console.WriteLine(crap.Message.ToStrin
                return false;
            }
        }

        /// <summary>
        /// A public method allowing outside modules to run insertion statements
        /// on the database.
        /// </summary>
        /// <param name="sqlInsert">This is a valid SQL insert provided by the createInsert method</param>
        /// <returns>A boolean denoting the success or failure of the insert operation.</returns>
        public bool runInsert(string sqlInsert)
        {
            if (_dbCon == null)
                return false;

            if (_dbCon.State != ConnectionState.Open)
                _dbCon.Open();
            var insertCom = new SQLiteCommand(sqlInsert);
            insertCom.Connection = _dbCon;
            try
            {
                int res = insertCom.ExecuteNonQuery(); //Exec insert
                //Console.WriteLine(res);
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close(); //CLOSE DB
                if (res != 1)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close();
                //Console.WriteLine(crap.Message.ToStrin
                return false;
            }
        }


        //Argument to this function shouldbe changed
        //into the SQLResults object returned from
        //runQuery() above

        /// <summary>
        /// A public method allowing the track model and other modules to format 
        /// database query results into coherent Block objects
        /// </summary>
        /// <param name="bR">The SQLiteDataReader containing the results of the query</param>
        /// <returns>A valid Block object, or null in the case of an error</returns>
        public Block formatBlockQueryResults(SQLiteDataReader bR)
        {
            Block tempBlock = null;
            int i = 0;
            if (!bR.IsClosed)
            {
                while (bR.Read())
                {
                    //Get all fields for a given block
                    string line = null, infra = null, dir = null;
                    string state = null;

                    int bIDFinal = -1, locXFinal = -1, locYFinal = -1;
                    double sEFinal = -1.0, gradeFinal = -1.0, bSizeFinal = -1.0;
                    int prevFinal = -1, dest1Final = -1, dest2Final = -1;
                    int trackCirIDFinal = -1;
                    int speedLimitFinal = -1;
                    try
                    {
                        bIDFinal = bR.GetInt32(bR.GetOrdinal("blockID"));
                        line = bR.GetString(bR.GetOrdinal("line"));
                        infra = bR.GetString(bR.GetOrdinal("infra"));
                        dir = bR.GetString(bR.GetOrdinal("dir"));
                        state = bR.GetString(bR.GetOrdinal("state"));
                        if (bIDFinal != 0)
                        {
                            sEFinal = bR.GetDouble(bR.GetOrdinal("starting_elev"));
                            gradeFinal = bR.GetDouble(bR.GetOrdinal("grade"));
                            locXFinal = bR.GetInt32(bR.GetOrdinal("locX"));
                            locYFinal = bR.GetInt32(bR.GetOrdinal("locY"));
                            bSizeFinal = bR.GetDouble(bR.GetOrdinal("bSize"));
                            prevFinal = bR.GetInt32(bR.GetOrdinal("prev"));
                            dest1Final = bR.GetInt32(bR.GetOrdinal("dest1"));
                            dest2Final = bR.GetInt32(bR.GetOrdinal("dest2"));
                            trackCirIDFinal = bR.GetInt32(bR.GetOrdinal("trackCirID"));
                            speedLimitFinal = bR.GetInt32(bR.GetOrdinal("speedLimit"));
                        }
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine("Exception was raised");
                    }
                    //////////////////////////////////////////////////////////////////////
                    //Parse fields that must be provided as a different type
                    string[] infraFinal = infra.Split(';');
                    var dirFinal = (DirEnum) Enum.Parse(typeof (DirEnum), dir, true);
                    var stateFinal = (StateEnum) Enum.Parse(typeof (StateEnum), state, true);


                    var locFinal = new int[2];
                    locFinal[0] = locXFinal;
                    locFinal[1] = locYFinal;

                    tempBlock = new Block(bIDFinal, stateFinal, prevFinal, sEFinal, gradeFinal, locFinal, bSizeFinal,
                                          dirFinal, infraFinal, dest1Final, dest2Final, trackCirIDFinal, line,speedLimitFinal);
                    i++; //Inc counter
                }
            }
            else
            {
                //Console.WriteLine("Reader was closed, this was a mistake.");
            }

            if (i != 1)
                return null;
            else
                return tempBlock;
        }

        //Argument to this function should be changed
        //into the SQLResults object returned from
        //runQuery above (and used in fQR above)

        /// <summary>
        /// A public method allowing the Track Model and external modules to format 
        /// database queries into coherent RouteInfo objects
        /// </summary>
        /// <param name="rr">A SQLiteDataReader object provided by the runQuery method in the DBManager</param>
        /// <returns>A valid RouteInfo object, or null in the case of a database error.</returns>
        public RouteInfo formatRouteQueryResults(SQLiteDataReader rr)
        {
            if (rr == null)
                return null;
            //Temp list used to store blocks, since we dont know
            //ahead of time how many to expect
            var blockList = new List<Block>();
            int nBlocks = 0;

            //We need to get our list of blocks, and the number of blocks
            int bIDFinal = -1, locXFinal = -1, locYFinal = -1;
            double sEFinal = -1.0, gradeFinal = -0.0, bSizeFinal = -1.0;
            int dest1Final = -1, dest2Final = -1, prevFinal = -1;
            int trackCirIDFinal = -1;
            int speedLimitFinal =-1;
            while (rr.Read())
            {
                //Get all fields for a given block
                bIDFinal = rr.GetInt32(rr.GetOrdinal("blockID"));
                string line = rr.GetString(rr.GetOrdinal("line"));
                string infra = rr.GetString(rr.GetOrdinal("infra"));
                string dir = rr.GetString(rr.GetOrdinal("dir"));
                string state = rr.GetString(rr.GetOrdinal("state"));
                sEFinal = rr.GetDouble(rr.GetOrdinal("starting_elev"));
                gradeFinal = rr.GetDouble(rr.GetOrdinal("grade"));
                bSizeFinal = rr.GetDouble(rr.GetOrdinal("bSize"));
                trackCirIDFinal = rr.GetInt32(rr.GetOrdinal("trackCirID"));
                speedLimitFinal = rr.GetInt32(rr.GetOrdinal("speedLimit"));
                locXFinal = rr.GetInt32(rr.GetOrdinal("locX"));
                locYFinal = rr.GetInt32(rr.GetOrdinal("locY"));

                prevFinal = rr.GetInt32(rr.GetOrdinal("prev"));
                dest1Final = rr.GetInt32(rr.GetOrdinal("dest1"));
                dest2Final = rr.GetInt32(rr.GetOrdinal("dest2"));

                //////////////////////////////////////////////////////////////////////
                //Parse fields that must be provided as a different type
                string[] infraFinal = infra.Split(';');
                var dirFinal = (DirEnum) Enum.Parse(typeof (DirEnum), dir, true);
                var stateFinal = (StateEnum) Enum.Parse(typeof (StateEnum), state, true);

                var locFinal = new int[2];
                locFinal[0] = locXFinal;
                locFinal[1] = locYFinal;

                blockList.Add(new Block(bIDFinal, stateFinal, prevFinal, sEFinal, gradeFinal, locFinal, bSizeFinal,
                                        dirFinal, infraFinal, dest1Final, dest2Final, trackCirIDFinal, line,speedLimitFinal));
                nBlocks++;
            }

            //If we didnt find any blocks, give up.
            if (nBlocks == 0)
            {
                //if (_dbCon.State != ConnectionState.Closed)
                  //  _dbCon.Close();
                return null;
            }


            Block[] blocks = blockList.ToArray();
            string rName = blocks[0].Line;
            int rID;
            if (rName.Equals("Red", StringComparison.OrdinalIgnoreCase))
                rID = 0;
            else
                rID = 1;


            //All routes start and end at the yard
            int sID = 0;
            int eID = 0;

            var tempRoute = new RouteInfo(rID, rName, nBlocks, blocks, sID, eID);
            //if (_dbCon.State != ConnectionState.Closed)
              //  _dbCon.Close();
            return tempRoute;
        }

        #region Properties

        #endregion
    }
}