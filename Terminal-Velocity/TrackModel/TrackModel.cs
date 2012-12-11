using System;
using System.Collections;
using System.Collections.Generic;
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
        private bool _redLoaded;
        private bool _greenLoaded;

        /// <summary>
        /// A constructor for the TrackModel module
        /// </summary>
        /// <param name="environment">Requires a reference to the surrounding environment object</param>
        public TrackModel(ISimulationEnvironment environment)
        {
            _redLoaded = false;
            _greenLoaded = false;
            _env = environment;
            _dbCreator = new DBCreator("",environment);
            _dbManager = new DBManager(_dbCreator.DBCon);

            _changeState = TrackChanged.None;

            IBlock redTemp = requestBlockInfo(1, "Red");
            IBlock greenTemp = requestBlockInfo(1, "Green");
            if (redTemp != null)
            {
                List<IBlock>[] tempBlockList=createBlockListArray(0);
                _dbCreator.populateTCs(tempBlockList, true);
                _changeState = TrackChanged.Red;
                _redLoaded = true;
            }

            if (greenTemp != null)
            {
                List<IBlock>[] tempBlockList = createBlockListArray(1);
                _dbCreator.populateTCs(tempBlockList, false);
                if (_changeState == TrackChanged.Red)
                    _changeState = TrackChanged.Both;
                else
                    _changeState = TrackChanged.Green;
                _greenLoaded = true;
            }

            if (_changeState != TrackChanged.None)
            {
                alertTrackChanged();
            }

            //_environment.Tick+=
        }


        private List<IBlock>[] createBlockListArray(int lineID)
        {
            List<IBlock>[] blockList=new List<IBlock>[16];
            for(int i=0;i<blockList.Length;i++)
                blockList[i]=new List<IBlock>();

                IRouteInfo temp=this.requestRouteInfo(lineID);
                IBlock[] blockArr=temp.BlockList;
                for (int i = 0; i < blockArr.Length; i++)
                {
                    if (blockArr[i].BlockID != 0)//Dont add 0 block to Track Controllers responsibility
                    {
                        blockList[blockArr[i].TrackCirID].Add(blockArr[i]);
                    }
                }
                return blockList;
        }
        
        /// <summary>
        /// A public method allowing other modules to request information on specific blocks.
        /// This information is returned in the form of 
        /// </summary>
        /// <param name="blockID">The ID of the requested block.  ID's are unique across all blocks in a given line</param>
        /// <param name="line">The name of the line containing the requested block.  Either "Red" or "Green"</param>
        /// <returns>An IBlock object holding information pertaining the block requested, or null if an error occurred</returns>
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

        public ITrackCircuit getCircuitByID(int id)
        {
            return _dbCreator.getITrackCircuitByID(id);
        }

        /// <summary>
        /// A public method allowing outside modules to request info pertaining to a track line.
        /// </summary>
        /// <param name="routeID">The route ID should be 0 for the Red line, or 1 for the Green line</param>
        /// <returns>an IRouteInfo object containing information pertaining to the Line/Route requested, or null if an error occurred</returns>
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


        /// <summary>
        /// A public method used to request a 2D array holding the Track Layour for a given line
        /// </summary>
        /// <param name="routeID">A routeID, corresponding to which line the user requests.  0 for Red, 1 for Green.</param>
        /// <returns>The returned IBlock[,] represents the track line.  The grid shows the 2D placement of blocks
        /// in space.  Where no blocks are found, the value is null.  </returns>
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
                temp[29, 19] = requestBlockInfo(40, "Red"); //40
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
                temp[2, 20] = requestBlockInfo(1, "Green"); //1
                temp[3, 21] = requestBlockInfo(2, "Green"); //2
                temp[4, 21] = requestBlockInfo(3, "Green"); //3
                temp[5, 22] = requestBlockInfo(4, "Green"); //4
                temp[5, 23] = requestBlockInfo(5, "Green"); //5
                temp[6, 24] = requestBlockInfo(6, "Green"); //6
                temp[6, 25] = requestBlockInfo(7, "Green"); //7
                temp[6, 26] = requestBlockInfo(7, "Green"); //7
                temp[5, 27] = requestBlockInfo(8, "Green"); //8
                temp[4, 27] = requestBlockInfo(8, "Green"); //8
                temp[3, 27] = requestBlockInfo(9, "Green"); //9
                temp[2, 27] = requestBlockInfo(9, "Green"); //9
                temp[1, 26] = requestBlockInfo(10, "Green"); //10
                temp[1, 25] = requestBlockInfo(10, "Green"); //10
                temp[1, 24] = requestBlockInfo(11, "Green"); //11
                temp[1, 23] = requestBlockInfo(11, "Green"); //11
                temp[1, 22] = requestBlockInfo(11, "Green"); //11
                temp[1, 21] = requestBlockInfo(11, "Green"); //11
                temp[1, 20] = requestBlockInfo(11, "Green"); //11
                temp[1, 19] = requestBlockInfo(12, "Green"); //12
                temp[1, 18] = requestBlockInfo(13, "Green"); //13
                temp[1, 17] = requestBlockInfo(13, "Green"); //13
                temp[1, 16] = requestBlockInfo(14, "Green"); //14
                temp[1, 15] = requestBlockInfo(15, "Green"); //15
                temp[1, 14] = requestBlockInfo(15, "Green"); //15
                temp[1, 13] = requestBlockInfo(16, "Green"); //16
                temp[1, 12] = requestBlockInfo(16, "Green"); //16
                temp[1, 11] = requestBlockInfo(17, "Green"); //17
                temp[2, 10] = requestBlockInfo(18, "Green"); //18
                temp[2, 9] = requestBlockInfo(19, "Green"); //19
                temp[3, 8] = requestBlockInfo(20, "Green"); //20
                temp[4, 8] = requestBlockInfo(21, "Green"); //21
                temp[5, 7] = requestBlockInfo(22, "Green"); //22
                temp[6, 7] = requestBlockInfo(23, "Green"); //23
                temp[7, 7] = requestBlockInfo(24, "Green"); //24
                temp[8, 7] = requestBlockInfo(25, "Green"); //25
                temp[9, 7] = requestBlockInfo(26, "Green"); //26
                temp[10, 7] = requestBlockInfo(27, "Green"); //27
                temp[11, 7] = requestBlockInfo(28, "Green"); //28
                temp[12, 7] = requestBlockInfo(29, "Green"); //29
                temp[13, 7] = requestBlockInfo(30, "Green"); //30
                temp[14, 7] = requestBlockInfo(31, "Green"); //31
                temp[15, 7] = requestBlockInfo(32, "Green"); //32
                temp[16, 7] = requestBlockInfo(33, "Green"); //33
                temp[17, 8] = requestBlockInfo(34, "Green"); //34
                temp[18, 9] = requestBlockInfo(35, "Green"); //35
                temp[18, 10] = requestBlockInfo(36, "Green"); //36
                temp[18, 11] = requestBlockInfo(37, "Green"); //37
                temp[18, 12] = requestBlockInfo(38, "Green"); //38
                temp[18, 13] = requestBlockInfo(39, "Green"); //39
                temp[18, 14] = requestBlockInfo(40, "Green"); //40
                temp[18, 15] = requestBlockInfo(41, "Green"); //41
                temp[18, 16] = requestBlockInfo(42, "Green"); //42
                temp[18, 17] = requestBlockInfo(43, "Green"); //43
                temp[18, 18] = requestBlockInfo(44, "Green"); //44
                temp[18, 19] = requestBlockInfo(45, "Green"); //45
                temp[18, 20] = requestBlockInfo(46, "Green"); //46
                temp[18, 21] = requestBlockInfo(47, "Green"); //47
                temp[18, 22] = requestBlockInfo(48, "Green"); //48
                temp[18, 23] = requestBlockInfo(49, "Green"); //49
                temp[18, 24] = requestBlockInfo(50, "Green"); //50
                temp[18, 25] = requestBlockInfo(51, "Green"); //51
                temp[18, 26] = requestBlockInfo(52, "Green"); //52
                temp[18, 27] = requestBlockInfo(53, "Green"); //53
                temp[18, 28] = requestBlockInfo(54, "Green"); //54
                temp[18, 29] = requestBlockInfo(55, "Green"); //55
                temp[18, 30] = requestBlockInfo(56, "Green"); //56
                temp[18, 31] = requestBlockInfo(57, "Green"); //57
                temp[18, 32] = requestBlockInfo(58, "Green"); //58
                temp[19, 33] = requestBlockInfo(59, "Green"); //59
                temp[20, 34] = requestBlockInfo(60, "Green"); //60
                temp[21, 35] = requestBlockInfo(61, "Green"); //61
                temp[22, 36] = requestBlockInfo(62, "Green"); //62
                temp[23, 36] = requestBlockInfo(63, "Green"); //63
                temp[24, 36] = requestBlockInfo(63, "Green"); //63
                temp[25, 36] = requestBlockInfo(64, "Green"); //64
                temp[26, 36] = requestBlockInfo(64, "Green"); //64
                temp[27, 36] = requestBlockInfo(64, "Green"); //64
                temp[28, 36] = requestBlockInfo(65, "Green"); //65
                temp[29, 36] = requestBlockInfo(65, "Green"); //65
                temp[30, 36] = requestBlockInfo(66, "Green"); //66
                temp[31, 36] = requestBlockInfo(66, "Green"); //66
                temp[32, 36] = requestBlockInfo(67, "Green"); //67
                temp[33, 36] = requestBlockInfo(67, "Green"); //67
                temp[34, 36] = requestBlockInfo(68, "Green"); //68
                temp[35, 36] = requestBlockInfo(68, "Green"); //68
                temp[36, 36] = requestBlockInfo(69, "Green"); //69
                temp[37, 36] = requestBlockInfo(69, "Green"); //69
                temp[38, 36] = requestBlockInfo(70, "Green"); //70
                temp[39, 36] = requestBlockInfo(70, "Green"); //70
                temp[40, 36] = requestBlockInfo(71, "Green"); //71
                temp[41, 35] = requestBlockInfo(71, "Green"); //71
                temp[42, 34] = requestBlockInfo(72, "Green"); //72
                temp[43, 33] = requestBlockInfo(72, "Green"); //72
                temp[44, 32] = requestBlockInfo(73, "Green"); //73
                temp[44, 31] = requestBlockInfo(73, "Green"); //73
                temp[44, 30] = requestBlockInfo(73, "Green"); //73
                temp[44, 29] = requestBlockInfo(74, "Green"); //74
                temp[44, 28] = requestBlockInfo(74, "Green"); //74
                temp[44, 27] = requestBlockInfo(74, "Green"); //74
                temp[44, 26] = requestBlockInfo(74, "Green"); //74
                temp[44, 25] = requestBlockInfo(75, "Green"); //75
                temp[44, 24] = requestBlockInfo(75, "Green"); //75
                temp[44, 23] = requestBlockInfo(75, "Green"); //75
                temp[44, 22] = requestBlockInfo(76, "Green"); //76
                temp[44, 21] = requestBlockInfo(77, "Green"); //77
                temp[44, 20] = requestBlockInfo(78, "Green"); //78
                temp[44, 19] = requestBlockInfo(79, "Green"); //79
                temp[44, 18] = requestBlockInfo(80, "Green"); //80
                temp[44, 17] = requestBlockInfo(81, "Green"); //81
                temp[44, 16] = requestBlockInfo(82, "Green"); //82
                temp[44, 15] = requestBlockInfo(83, "Green"); //83
                temp[44, 14] = requestBlockInfo(84, "Green"); //84
                temp[44, 13] = requestBlockInfo(85, "Green"); //85
                temp[44, 12] = requestBlockInfo(86, "Green"); //86
                temp[44, 11] = requestBlockInfo(87, "Green"); //87
                temp[44, 10] = requestBlockInfo(87, "Green"); //87
                temp[44, 9] = requestBlockInfo(88, "Green"); //88
                temp[44, 8] = requestBlockInfo(88, "Green"); //88
                temp[44, 7] = requestBlockInfo(89, "Green"); //89
                temp[44, 6] = requestBlockInfo(90, "Green"); //90
                temp[43, 5] = requestBlockInfo(91, "Green"); //91
                temp[42, 4] = requestBlockInfo(92, "Green"); //92
                temp[41, 4] = requestBlockInfo(93, "Green"); //93
                temp[40, 5] = requestBlockInfo(94, "Green"); //94
                temp[40, 6] = requestBlockInfo(95, "Green"); //95
                temp[40, 7] = requestBlockInfo(96, "Green"); //96
                temp[41, 8] = requestBlockInfo(97, "Green"); //97
                temp[42, 9] = requestBlockInfo(98, "Green"); //98
                temp[43, 10] = requestBlockInfo(99, "Green"); //99
                temp[43, 11] = requestBlockInfo(100, "Green"); //100
                temp[43, 23] = requestBlockInfo(101, "Green"); //101
                temp[42, 24] = requestBlockInfo(101, "Green"); //101
                temp[41, 25] = requestBlockInfo(101, "Green"); //101
                temp[41, 26] = requestBlockInfo(102, "Green"); //102
                temp[41, 27] = requestBlockInfo(103, "Green"); //103
                temp[41, 28] = requestBlockInfo(103, "Green"); //103
                temp[41, 29] = requestBlockInfo(104, "Green"); //104
                temp[41, 30] = requestBlockInfo(104, "Green"); //104
                temp[41, 31] = requestBlockInfo(105, "Green"); //105
                temp[40, 32] = requestBlockInfo(106, "Green"); //106
                temp[39, 33] = requestBlockInfo(107, "Green"); //107
                temp[38, 33] = requestBlockInfo(107, "Green"); //107
                temp[37, 33] = requestBlockInfo(108, "Green"); //108
                temp[36, 33] = requestBlockInfo(109, "Green"); //109
                temp[35, 33] = requestBlockInfo(110, "Green"); //110
                temp[34, 33] = requestBlockInfo(111, "Green"); //111
                temp[33, 33] = requestBlockInfo(112, "Green"); //112
                temp[32, 33] = requestBlockInfo(112, "Green"); //112
                temp[31, 33] = requestBlockInfo(113, "Green"); //113
                temp[30, 33] = requestBlockInfo(114, "Green"); //114
                temp[29, 33] = requestBlockInfo(115, "Green"); //115
                temp[28, 33] = requestBlockInfo(116, "Green"); //116
                temp[27, 33] = requestBlockInfo(117, "Green"); //117
                temp[26, 33] = requestBlockInfo(117, "Green"); //117
                temp[25, 33] = requestBlockInfo(118, "Green"); //118
                temp[24, 32] = requestBlockInfo(119, "Green"); //119
                temp[23, 31] = requestBlockInfo(120, "Green"); //120
                temp[23, 30] = requestBlockInfo(121, "Green"); //121
                temp[23, 29] = requestBlockInfo(122, "Green"); //122
                temp[23, 28] = requestBlockInfo(123, "Green"); //123
                temp[23, 27] = requestBlockInfo(124, "Green"); //124
                temp[23, 26] = requestBlockInfo(125, "Green"); //125
                temp[23, 25] = requestBlockInfo(126, "Green"); //126
                temp[23, 24] = requestBlockInfo(127, "Green"); //127
                temp[23, 23] = requestBlockInfo(128, "Green"); //128
                temp[23, 22] = requestBlockInfo(129, "Green"); //129
                temp[23, 21] = requestBlockInfo(130, "Green"); //130
                temp[23, 20] = requestBlockInfo(131, "Green"); //131
                temp[23, 19] = requestBlockInfo(132, "Green"); //132
                temp[23, 18] = requestBlockInfo(133, "Green"); //133
                temp[23, 17] = requestBlockInfo(134, "Green"); //134
                temp[23, 16] = requestBlockInfo(135, "Green"); //135
                temp[23, 15] = requestBlockInfo(136, "Green"); //136
                temp[23, 14] = requestBlockInfo(137, "Green"); //137
                temp[23, 13] = requestBlockInfo(138, "Green"); //138
                temp[23, 12] = requestBlockInfo(139, "Green"); //139
                temp[23, 11] = requestBlockInfo(140, "Green"); //140
                temp[23, 10] = requestBlockInfo(141, "Green"); //141
                temp[23, 9] = requestBlockInfo(142, "Green"); //142
                temp[23, 8] = requestBlockInfo(143, "Green"); //143
                temp[23, 7] = requestBlockInfo(143, "Green"); //143
                temp[23, 6] = requestBlockInfo(144, "Green"); //144
                temp[23, 5] = requestBlockInfo(144, "Green"); //144
                temp[22, 4] = requestBlockInfo(145, "Green"); //145
                temp[21, 3] = requestBlockInfo(145, "Green"); //145
                temp[20, 2] = requestBlockInfo(146, "Green"); //146
                temp[19, 2] = requestBlockInfo(146, "Green"); //146
                temp[18, 2] = requestBlockInfo(147, "Green"); //147
                temp[17, 2] = requestBlockInfo(148, "Green"); //148
                temp[16, 2] = requestBlockInfo(149, "Green"); //149
                temp[15, 2] = requestBlockInfo(150, "Green"); //150
                temp[14, 3] = requestBlockInfo(150, "Green"); //150
                temp[13, 4] = requestBlockInfo(150, "Green"); //150
                temp[13, 5] = requestBlockInfo(150, "Green"); //150
                temp[12, 6] = requestBlockInfo(150, "Green"); //150
            } 

            return temp;
        }

        /// <summary>
        /// This public method is used to allow other modules to request a change in switch state for
        /// a block.  
        /// </summary>
        /// <param name="bToUpdate"> An IBlock object with the updated switch state.</param>
        /// <returns>A boolean object representing the success or failure of the database update</returns>
        public bool requestUpdateSwitch(IBlock bToUpdate)
        {
            if (bToUpdate == null)
                return false;

            if (bToUpdate.hasSwitch() == false)
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
                    _changeState = TrackChanged.Red;
                    alertTrackChanged();
                }
                else//We're green
                {
                    _changeState = TrackChanged.Green;
                    alertTrackChanged();
                }
            }
            return res;
        }

        /// <summary>
        /// A public method allowing external modules to update variable attributes of a block.
        /// The attributes that are changable include the health state (broken, failed, healthy).
        /// That's it.
        /// </summary>
        /// <param name="bToUpdate">The IBlock object</param>
        /// <returns></returns>
        public bool requestUpdateBlock(IBlock bToUpdate)
        {
            if (bToUpdate == null)
                return false;

            if (bToUpdate.BlockID == 0)
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
                    _changeState = TrackChanged.Red;
                    alertTrackChanged();
                }
                else//We're green
                {
                    _changeState = TrackChanged.Green;
                    alertTrackChanged();
                }
            }
            return res;
        }

        /// <summary>
        /// A public method allowing modules to give input csv files to the TrackModel.  This method 
        /// takes the input files and parses them for insertion into the database.
        /// </summary>
        /// <param name="fName">The file name of the file to be parsed</param>
        /// <returns>A boolean corresponding to the success or failure of the file parsing and insertion</returns>
        public bool provideInputFile(string fName)
        {
            if (fName == null)
                return false;

            //Check if file is already in db, if so return true
            if (fName.Contains("red") || fName.Contains("RED") || fName.Contains("Red"))
            {
                if (_redLoaded == true)
                {
                    return true;
                }
            }

            //Check if file is already in db, if so return true;
            if (fName.Contains("green") || fName.Contains("GREEN") || fName.Contains("Green"))
            {
                if (_greenLoaded == true)
                {
                    return true;
                }
            }

            int res = _dbCreator.parseInputFile(fName);

            if ( res==0 && (fName.Contains("red") || fName.Contains("RED") || fName.Contains("Red")))
            {
                _redLoaded = true;
                _changeState = TrackChanged.Red;
                alertTrackChanged();
            }

            if (res==0 && (fName.Contains("green") || fName.Contains("GREEN") || fName.Contains("Green")))
            {
                _greenLoaded = true;
                _changeState = TrackChanged.Green;
                alertTrackChanged();
            }


            if (res == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// A public method used by the CTC Office and Track Controller to determine the path between two
        /// points (startBlockID and endBlockID) on a given line.  Uses BFS to find a decent path between
        /// the two points.
        /// </summary>
        /// <param name="startBlockID">The Block ID of the starting block</param>
        /// <param name="endBlockID">The Block ID of the ending block</param>
        /// <param name="line">The line containing the path: either "Red" or "Green"</param>
        /// <returns>An array of IBlock objects on the path</returns>
        public IBlock[] requestPath(int startBlockID, int endBlockID, string line)
        {
            bool destFound = false;

            //If start or end block ID was obviously invalid.
            if (startBlockID < 0 || endBlockID < 0)
            {
                return null;
            }

            //If line was invalid
            if (!line.Equals("Red", StringComparison.OrdinalIgnoreCase) && !line.Equals("Green", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            //Init list to hold startBlock
            List<IBlock> path = new List<IBlock>();
            IBlock startBlock = requestBlockInfo(startBlockID, line);
            if (startBlock == null)
            {
                return null;
            }
            path.Add(startBlock);
            Queue<List<IBlock>> pathQueue = new Queue<List<IBlock>>();
            pathQueue.Enqueue(path);

            while (pathQueue.Count != 0)
            {
                List<IBlock> temp= pathQueue.Dequeue();

                //Find neighbors of current node
                List<IBlock> neighborList = new List<IBlock>();
                int nextID = temp[temp.Count-1].SwitchDest1;
                int altNext = temp[temp.Count-1].SwitchDest2;
                int prev = temp[temp.Count-1].PrevBlockID;
                if (nextID != -1 && nextID != temp[temp.Count-1].BlockID && !temp.Contains(requestBlockInfo(nextID,line)))
                {
                    neighborList.Add(requestBlockInfo(nextID, line));
                }

                if (altNext != -1 && altNext != temp[temp.Count - 1].BlockID && !temp.Contains(requestBlockInfo(altNext, line)))
                {
                    neighborList.Add(requestBlockInfo(altNext, line));
                }

                if (prev != -1 && prev != temp[temp.Count - 1].BlockID && !temp.Contains(requestBlockInfo(prev, line)))
                {
                    neighborList.Add(requestBlockInfo(prev, line));
                }


                //If one of the neighbors is the destination
                //Add the dest to the path, and set the destFound flag
                //so we can break from our loop
                foreach (IBlock n in neighborList)
                {
                    if (n.BlockID == endBlockID)
                    {
                        temp.Add(n);
                        path = temp;
                        destFound = true;
                        break;
                    }
                }
                if (destFound)
                    break;

                //If(Block has at least 1 neighbor not visited yet, push neighbor to path-stack
                foreach (IBlock n in neighborList)
                {
                    if (!temp.Contains(n))
                    {
                        IBlock[] arrCopy=temp.ToArray();
                        List<IBlock> copyList=new List<IBlock>(arrCopy);
                        copyList.Add(n);
                        pathQueue.Enqueue(copyList);
                        
                    }
                }

            }//End while loop

            //If path contains no elements, then path does not exist
            if (path.Count == 0)
            {
                return null;
            }
            else//If path contains elements, we have a valid path stored in our stack.
            {
                //Copy stack to array
                IBlock[] pathArr = path.ToArray();
                return pathArr;
            }

        }//End method


        //Handle environment tick
        private void _environment_Tick(object sender, TickEventArgs e)
        {
            //handle tick here
        }

        /// <summary>
        /// A Property returning an enum corresponding to which line's have been changed since
        /// the last check.  Red, Green, Both, or None
        /// </summary>
        public TrackChanged ChangeFlag
        {
            get { return _changeState; }
        }

        /// <summary>
        /// A boolean value corresponding to whether the red line has been loaded yet
        /// </summary>
        public bool RedLoaded
        {
            get { return _redLoaded; }
        }

        /// <summary>
        /// A boolean value corresponding to whether the green line has been loaded yet
        /// </summary>
        public bool GreenLoaded
        {
            get { return _greenLoaded; }
        }


        /// <summary>
        /// A public event allowing other modules to see when track state changes
        /// </summary>
        public event EventHandler<EventArgs> TrackChangedEvent;

        /// <summary>
        /// A public method encapsulating the throwing of TrackChangedEvent events
        /// </summary>
        public void alertTrackChanged()
        {
            if (TrackChangedEvent != null)
            {
                TrackChangedEvent(this, EventArgs.Empty);
            }
        }

    }
}