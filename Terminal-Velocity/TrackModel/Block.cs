using System;
using Interfaces;

namespace TrackModel
{
    public class Block : IBlock
    {
        //Private parameters
        private readonly string[] _attributes;
        private readonly int _blockID;
        private readonly DirEnum _direction;
        private readonly double _grade;
        private readonly string _line;
        private readonly int[] _location;
        private readonly int _prevBlockID;
        private readonly double _startingElev;
        private readonly int _speedLimit;
        private readonly int _trackCirID;
        private int _switchDest1;

        /// <summary>
        /// A public constructor for block objects.  The constructor takes an argument for each datafield in the
        /// object, allowing them all to be initialized at once.
        /// </summary>
        /// <param name="bID">The block ID of the block</param>
        /// <param name="state">The StateEnum state of the block (Healthy, TrackCircuitFailure, etc)</param>
        /// <param name="pBID">The previous* block ID of the block.</param>
        /// <param name="sElev">The cumulative elevation of the block</param>
        /// <param name="g">The grade of the track on this block</param>
        /// <param name="loc">An X,Y pair stored in an integer array representing the </param>
        /// <param name="bS">The block size of this block</param>
        /// <param name="dir">The DirEnum direction of the block</param>
        /// <param name="atts">An array of infrastructure attributes for the block</param>
        /// <param name="d1">The block ID of the next* block.</param>
        /// <param name="d2">The block ID of the alternate next* block.  Only valid if block has switch</param>
        /// <param name="tCID">The Track Circuit ID that the given block reports on</param>
        /// <param name="l">The line of the current block: either "Red" or "Green"</param>
        /// <param name="sL">The Speed Limit of the block.  Trains must maintain speeds below posted limits</param>
        public Block(int bID, StateEnum state, int pBID, double sElev, double g, int[] loc, double bS, DirEnum dir,
                     string[] atts, int d1, int d2, int tCID, string l, int sL)
        {
            _blockID = bID;
            State = state;
            _prevBlockID = pBID;
            _startingElev = sElev;
            _grade = g;
            _location = loc;
            BlockSize = bS;
            _direction = dir;
            _attributes = atts;
            _switchDest1 = d1;
            SwitchDest2 = d2;
            _trackCirID = tCID;
            _line = l;
            _speedLimit = sL;
        }

        /// <summary>
        /// A deprecated constructor that doesnt initialize any block values.  Using this constructor
        /// isnt allowed and will throw an exception.  That's right, playin' hardball.
        /// </summary>
        /// <param name="bID">A block ID for the block</param>
        public Block(int bID)
        {
            throw new Exception(
                "Don't use this constructor please.  Instead, use TrackModel.requestBlockInfo(0,\"LineName\")");
        }

        /// <summary>
        /// A method used by trains to determine which block they're traveling on.  As most track blocks
        /// are bi-directional, for a train to determine which block will be their TRUE next.  The true 
        /// next can be determined by using the ID of the block that the train most recently left to establish direction/
        /// </summary>
        /// <param name="prevBlockIndex">The Block ID that the Train most recently left</param>
        /// <returns>An Integer corresponding the the ID of the TRUE next block</returns>
        public int nextBlockIndex(int prevBlockIndex)
        {
            //Were starting in the yard
            if (_blockID == 0)
            {
                if (_line.Equals("Red", StringComparison.OrdinalIgnoreCase))
                {
                    return 9; //Only exit from yard on red line is to block 9
                }
                else //Assume line is green
                {
                    return 62; //Only exit from yard on green line is block 62
                }
            }
            else
            {
                if (prevBlockIndex == _prevBlockID)
                    return _switchDest1;
                else
                    return _prevBlockID;
            }
        }

        //Public methods
        /// <summary>
        /// A public method to determine if the given block contains a switch
        /// </summary>
        /// <returns>True if the block has a switch, false otherwise</returns>
        public bool hasSwitch()
        {
            for (int i = 0; i < _attributes.Length; i++)
            {
                if (_attributes[i].Equals("SWITCH", StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;

            //Alternate implementation involves checking if switchDest2=-1
        }

        /// <summary>
        /// A method to determine if this block is underground (in a tunnel)
        /// </summary>
        /// <returns>True if the block is underground, false otherwise</returns>
        public bool hasTunnel()
        {
            for (int i = 0; i < _attributes.Length; i++)
            {
                if (_attributes[i].Equals("UNDERGROUND", StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// A method to determine if the block has an associated heater
        /// </summary>
        /// <returns>True if the block has an associated heater, false otherwise.</returns>
        public bool hasHeater()
        {
            for (int i = 0; i < _attributes.Length; i++)
            {
                if (_attributes[i].Equals("HEATER", StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// A method to determine if the block has a crossing
        /// </summary>
        /// <returns>True if the block contains a crossing, false otherwise</returns>
        public bool hasCrossing()
        {
            for (int i = 0; i < _attributes.Length; i++)
            {
                if (_attributes[i].Equals("CROSSING", StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// A method to determine if this block contains a station
        /// </summary>
        /// <returns>True if the block contains a station, false otherwise</returns>
        public bool hasStation()
        {
            for (int i = 0; i < _attributes.Length; i++)
            {
                if (_attributes[i].Equals("STATION", StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// A method to determine if this block runs north
        /// </summary>
        /// <returns>Returns true if the block runs northward, false otherwise</returns>
        public bool runsNorth()
        {
            if (_direction == DirEnum.North || _direction == DirEnum.North_AND_South)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A method used to determine if the given block runs southward
        /// </summary>
        /// <returns>True if the given block runs southward, false otherwise</returns>
        public bool runsSouth()
        {
            if (_direction == DirEnum.South || _direction == DirEnum.North_AND_South)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A method used to determine if the given block runs eastward
        /// </summary>
        /// <returns>True if the block travels east, false otherwise</returns>
        public bool runsEast()
        {
            if (_direction == DirEnum.East || _direction == DirEnum.East_AND_West)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A method used to determine if the given block runs westward
        /// </summary>
        /// <returns>True if the block runs west, false otherwise</returns>
        public bool runsWest()
        {
            if (_direction == DirEnum.West || _direction == DirEnum.East_AND_West)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A method to determine if the given block runs Northeast
        /// </summary>
        /// <returns>True if the block turns northeast, false otherwise</returns>
        public bool runsNorthEast()
        {
            if (_direction == DirEnum.Northeast || _direction == DirEnum.Northeast_AND_Southwest)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A method to determine if the given block runs northwest
        /// </summary>
        /// <returns>True if the block runs northwest, false otherwise</returns>
        public bool runsNorthWest()
        {
            if (_direction == DirEnum.Northwest || _direction == DirEnum.Northwest_AND_Southeast)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A method to determine if the given block runs southeast
        /// </summary>
        /// <returns>True if the block runs southeast, false otherwise</returns>
        public bool runsSouthEast()
        {
            if (_direction == DirEnum.Southeast || _direction == DirEnum.Northwest_AND_Southeast)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A method to determine if the given block runs southwest
        /// </summary>
        /// <returns>True if the block runs southwest, false otherwise</returns>
        public bool runsSouthWest()
        {
            if (_direction == DirEnum.Southwest || _direction == DirEnum.Northeast_AND_Southwest)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Properties

        /// <summary>
        /// A property to return the integer block ID of the block
        /// </summary>
        public int BlockID
        {
            get { return _blockID; }
        }

        /// <summary>
        /// A property to return the integer speed limit of the block
        /// </summary>
        public int SpeedLimit
        {
            get { return _speedLimit; }
        }

        /// <summary>
        /// A property to get or set the Health State of the block.  
        /// </summary>
        public StateEnum State { get; set; }

        /// <summary>
        /// A property to get the 'previous' block ID of the block.  As most blocks are bi-directional,
        /// there is no guarantee that this is truly the block preceding the given block rather than the
        /// block following it.  The notion of 'previous' depends on the travel direction of the involved 
        /// train.  To test for true previous, and next-ness, use the method nextBlockIndex
        /// </summary>
        public int PrevBlockID
        {
            get { return _prevBlockID; }
        }

        /// <summary>
        /// An accessor to get the starting elevation of the block
        /// </summary>
        public double StartingElev
        {
            get { return _startingElev; }
        }

        /// <summary>
        /// An accessor to get the fractional grade of the block's track
        /// </summary>
        public double Grade
        {
            get { return _grade; }
        }

        /// <summary>
        /// An accessor to get the Length=2 array holding the X,Y pair of the blocks 'location'
        /// Location is a concept that only is valid in reference to the 2D track grid used to display
        /// the line in other methods.
        /// </summary>
        public int[] Location
        {
            get { return _location; }
        }

        /// <summary>
        /// An accessor for the block size of the block
        /// </summary>
        public double BlockSize { get; set; }

        /// <summary>
        /// An accessor for the DirEnum giving the blocks direction
        /// </summary>
        public DirEnum Direction
        {
            get { return _direction; }
        }

        /// <summary>
        /// An accessor for the 'next' destination blockID.  As most blocks are bi-directional,
        /// there is no guarantee that this is truly the block follows the given block rather than the
        /// block following it.  The notion of 'next and previous' depends on the travel direction of the involved 
        /// train.  To test for true previous, and next-ness, use the method nextBlockIndex
        /// </summary>
        public int SwitchDest1
        {
            get { return _switchDest1; }
            set { _switchDest1 = value; }
        }

        /// <summary>
        /// An accessor for the alternate 'next' destination blockID.  For blocks without switches, this alternate block is -1
        /// As most blocks are bi-directional,
        /// there is no guarantee that this is truly the block follows the given block rather than the
        /// block following it.  The notion of 'next and previous' depends on the travel direction of the involved 
        /// train.  To test for true previous, and next-ness, use the method nextBlockIndex
        /// </summary>
        public int SwitchDest2 { get; set; }

        /// <summary>
        /// A property for the ID of the Track Circuit that this block is monitored by.
        /// </summary>
        public int TrackCirID
        {
            get { return _trackCirID; }
        }

        /// <summary>
        /// A Property to access the raw attribute array that holds this block's infrastructure.
        /// Raw access of this data can usually be avoided by using the provided direction, and boolean
        /// infrastructure has*() functions.
        /// </summary>
        public string[] AttrArray
        {
            get { return _attributes; }
        }

        /// <summary>
        /// A Property used to access the name of the Line the block belongs to.  This should
        /// be either "Red" or "Green".
        /// </summary>
        public string Line
        {
            get { return _line; }
        }

        #endregion
    }
}