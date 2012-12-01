using System;
using System.Data.SQLite;
using Interfaces;
using Utility;

namespace TrackModel
{
    public class TrackModel : ITrackModel
    {
        //Private parameters
        private readonly DBCreator _dbCreator;
        private readonly DBManager _dbManager;
        private ISimulationEnvironment _env;
        private TrackChanged _changeState;
        //private DisplayManager _dispManager;


        public TrackModel(ISimulationEnvironment environment)
        {
            _env = environment;
            _dbCreator = new DBCreator("");
            _dbManager = new DBManager(_dbCreator.DBCon);

            _changeState = TrackChanged.Both;

            //_environment.Tick+=
        }

        //Last minute method to allow TrackModel to directly take input file.

        public IBlock requestBlockInfo(int blockID, string line)
        {
            //Dont request patently invalid blocks
            if (blockID < 0)
                return null;

            string blockQuery = _dbManager.createQueryString("BLOCK", blockID, line);

            //Check query return val
            if (blockQuery == null)
                return null;

            //Get data reader for query
            SQLiteDataReader queryReader = _dbManager.runQuery(blockQuery);

            //Check exec return val
            if (queryReader == null)
                return null;

            IBlock temp = _dbManager.formatBlockQueryResults(queryReader);
            return temp;
        }

        public IRouteInfo requestRouteInfo(int routeID)
        {
            if (routeID != 0 && routeID != 1)
                return null;
            string routeQuery = _dbManager.createQueryString("ROUTE", routeID, null);

            //Check query return val
            if (routeQuery == null)
                return null;

            //Get data reader from query
            SQLiteDataReader queryReader = _dbManager.runQuery(routeQuery);

            //Check data reader return val
            if (queryReader == null)
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

            var temp = new IBlock[46,38];

            //Initialize our array
            for (int i = 0; i < 46; i++)
            {
                for (int j = 0; j < 38; j++)
                {
                    temp[i, j] = null;
                }
            }

            if (routeID == 0) //means red
            {
                //Update the "TRACK CHANGED" flag
                if (_changeState == TrackChanged.Both || _changeState==TrackChanged.Green)
                    _changeState = TrackChanged.Green;
                else
                    _changeState = TrackChanged.None;

                temp[10,28] = requestBlockInfo(1, "Red"); //1
                temp[9, 29] = requestBlockInfo(2, "Red"); //2
                temp[8, 29] = requestBlockInfo(3, "Red"); //3
                temp[7, 30] = requestBlockInfo(4, "Red"); //4
                temp[7, 31] = requestBlockInfo(5, "Red"); //5
                temp[7, 32] = requestBlockInfo(6, "Red"); //6
                temp[7, 33] = requestBlockInfo(7, "Red"); //7
                temp[7, 34] = requestBlockInfo(7, "Red"); //7
                temp[7, 35] = requestBlockInfo(8, "Red"); //8
                temp[8, 36] = requestBlockInfo(8, "Red"); //8
                temp[9, 36] = requestBlockInfo(9, "Red"); //9
                temp[10, 35] = requestBlockInfo(10, "Red"); //10
                temp[10, 34] = requestBlockInfo(11, "Red"); //11
                temp[11, 33] = requestBlockInfo(11, "Red"); //11
                temp[11, 32] = requestBlockInfo(12, "Red"); //12
                temp[11, 31] = requestBlockInfo(13, "Red"); //13
                temp[11, 30] = requestBlockInfo(13, "Red"); //13
                temp[11, 29] = requestBlockInfo(14, "Red"); //14
                temp[11, 28] = requestBlockInfo(14, "Red"); //14
                temp[10, 27] = requestBlockInfo(15, "Red"); //15
                temp[10, 26] = requestBlockInfo(16, "Red"); //16
                temp[10, 25] = requestBlockInfo(17, "Red"); //17
                temp[10, 24] = requestBlockInfo(18, "Red"); //18
                temp[10, 23] = requestBlockInfo(19, "Red"); //19
                temp[10, 22] = requestBlockInfo(20, "Red"); //20
                temp[10, 21] = requestBlockInfo(21, "Red"); //21
                temp[11, 20] = requestBlockInfo(22, "Red"); //22
                temp[12, 19] = requestBlockInfo(23, "Red"); //23
                temp[13, 19] = requestBlockInfo(24, "Red"); //24
                temp[14, 19] = requestBlockInfo(25, "Red"); //25
                temp[15, 19] = requestBlockInfo(26, "Red"); //26
                temp[16, 19] = requestBlockInfo(27, "Red"); //27
                temp[17, 19] = requestBlockInfo(28, "Red"); //28
                temp[18, 19] = requestBlockInfo(29, "Red"); //29
                temp[19, 19] = requestBlockInfo(30, "Red"); //30
                temp[20, 19] = requestBlockInfo(31, "Red"); //31
                temp[21, 19] = requestBlockInfo(32, "Red"); //32
                temp[22, 19] = requestBlockInfo(33, "Red"); //33
                temp[23, 19] = requestBlockInfo(34, "Red"); //34
                temp[24, 19] = requestBlockInfo(35, "Red"); //35
                temp[25, 19] = requestBlockInfo(36, "Red"); //36
                temp[26, 19] = requestBlockInfo(37, "Red"); //37
                temp[27, 19] = requestBlockInfo(38, "Red"); //38
                temp[28, 19] = requestBlockInfo(39, "Red"); //39
                temp[39, 19] = requestBlockInfo(40, "Red"); //40
                temp[30, 19] = requestBlockInfo(41, "Red"); //41
                temp[31, 19] = requestBlockInfo(42, "Red"); //42
                temp[32, 19] = requestBlockInfo(43, "Red"); //43
                temp[33, 19] = requestBlockInfo(44, "Red"); //44
                temp[34, 19] = requestBlockInfo(45, "Red"); //45
                temp[35, 19] = requestBlockInfo(46, "Red"); //46
                temp[36, 18] = requestBlockInfo(47, "Red"); //47
                temp[37, 17] = requestBlockInfo(47, "Red"); //47
                temp[37, 16] = requestBlockInfo(48, "Red"); //48
                temp[38, 15] = requestBlockInfo(49, "Red"); //49
                temp[38, 14] = requestBlockInfo(50, "Red"); //50
                temp[38, 13] = requestBlockInfo(50, "Red"); //50
                temp[38, 12] = requestBlockInfo(51, "Red"); //51
                temp[38, 11] = requestBlockInfo(52, "Red"); //52
                temp[38, 10] = requestBlockInfo(53, "Red"); //53
                temp[38, 9] = requestBlockInfo(53, "Red"); //53
                temp[38, 8] = requestBlockInfo(53, "Red"); //53
                temp[38, 7] = requestBlockInfo(54, "Red"); //54
                temp[38, 6] = requestBlockInfo(54, "Red"); //54
                temp[37, 5] = requestBlockInfo(55, "Red"); //55
                temp[36, 4] = requestBlockInfo(55, "Red"); //55
                temp[35, 3] = requestBlockInfo(56, "Red"); //56
                temp[34, 2] = requestBlockInfo(57, "Red"); //57
                temp[33, 2] = requestBlockInfo(57, "Red"); //57
                temp[32, 2] = requestBlockInfo(58, "Red"); //58
                temp[31, 2] = requestBlockInfo(58, "Red"); //58
                temp[31, 3] = requestBlockInfo(59, "Red"); //59
                temp[30, 4] = requestBlockInfo(59, "Red"); //59
                temp[30, 5] = requestBlockInfo(60, "Red"); //60
                temp[30, 6] = requestBlockInfo(60, "Red"); //60
                temp[31, 7] = requestBlockInfo(61, "Red"); //61
                temp[32, 7] = requestBlockInfo(61, "Red"); //61
                temp[33, 8] = requestBlockInfo(62, "Red"); //62
                temp[34, 8] = requestBlockInfo(63, "Red"); //63
                temp[35, 8] = requestBlockInfo(63, "Red"); //63
                temp[36, 8] = requestBlockInfo(64, "Red"); //64
                temp[36, 9] = requestBlockInfo(65, "Red"); //65
                temp[37, 10] = requestBlockInfo(66, "Red"); //66
                temp[32, 18] = requestBlockInfo(67, "Red"); //67
                temp[32, 17] = requestBlockInfo(67, "Red"); //67
                temp[31, 16] = requestBlockInfo(68, "Red"); //68
                temp[30, 16] = requestBlockInfo(69, "Red"); //69
                temp[29, 16] = requestBlockInfo(69, "Red"); //69
                temp[28, 16] = requestBlockInfo(70, "Red"); //70
                temp[27, 17] = requestBlockInfo(71, "Red"); //71
                temp[27, 18] = requestBlockInfo(71, "Red"); //71
                temp[21, 18] = requestBlockInfo(72, "Red"); //72
                temp[21, 17] = requestBlockInfo(72, "Red"); //72
                temp[20, 16] = requestBlockInfo(73, "Red"); //73
                temp[19, 16] = requestBlockInfo(74, "Red"); //74
                temp[18, 16] = requestBlockInfo(74, "Red"); //74
                temp[17, 16] = requestBlockInfo(75, "Red"); //75
                temp[16, 17] = requestBlockInfo(76, "Red"); //76
                temp[16, 18] = requestBlockInfo(76, "Red"); //76
            }
            else //routeID==1 means green
            {
                //Update the "TRACK CHANGED" flag
                if (_changeState == TrackChanged.Both || _changeState==TrackChanged.Red)
                    _changeState = TrackChanged.Red;
                else
                    _changeState = TrackChanged.None;
            }

            return temp;
        }

        public bool requestUpdateSwitch(IBlock bToUpdate)
        {
            if (bToUpdate == null)
                return false;

            string updateString = _dbManager.createUpdate("SWITCH", bToUpdate);
            if (updateString == null)
                return false;

            bool res = _dbManager.runUpdate(updateString);

            //Update our "TRACK CHANGED" flag if necessary
            if (res)
            {
                //IF we're red
                if (bToUpdate.Line.Equals("Red", StringComparison.OrdinalIgnoreCase))
                {
                    if (_changeState == TrackChanged.None || _changeState == TrackChanged.Red)
                        _changeState = TrackChanged.Red;
                    else
                        _changeState = TrackChanged.Both;
                }
                else//We're green
                {
                    if (_changeState == TrackChanged.None || _changeState == TrackChanged.Green)
                        _changeState = TrackChanged.Green;
                    else
                        _changeState = TrackChanged.Both;
                }
            }
            return res;
        }

        public bool requestUpdateBlock(IBlock bToUpdate)
        {
            if (bToUpdate == null)
                return false;

            string updateString = _dbManager.createUpdate("BLOCK", bToUpdate);
            if (updateString == null)
                return false;

            bool res = _dbManager.runUpdate(updateString);


            if (res)
            {
                //IF we're red
                if (bToUpdate.Line.Equals("Red", StringComparison.OrdinalIgnoreCase))
                {
                    if (_changeState == TrackChanged.None || _changeState == TrackChanged.Red)
                        _changeState = TrackChanged.Red;
                    else
                        _changeState = TrackChanged.Both;
                }
                else//We're green
                {
                    if (_changeState == TrackChanged.None || _changeState == TrackChanged.Green)
                        _changeState = TrackChanged.Green;
                    else
                        _changeState = TrackChanged.Both;
                }
            }
            return res;
        }

        public bool provideInputFile(string fName)
        {
            int res = _dbCreator.parseInputFile(fName);
            //Console.WriteLine("Inside TrackModel, res was: " + res);
            if (res == 0)
                return true;
            else
                return false;
        }


        //Handle environment tick
        private void _environment_Tick(object sender, TickEventArgs e)
        {
            //handle tick here
        }

        //Property
        public TrackChanged ChangeFlag
        {
            get { return _changeState; }
        }

    }
}