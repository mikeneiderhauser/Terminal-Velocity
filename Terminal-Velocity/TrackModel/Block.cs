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
        private int _switchDest1;

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
            TrackCirID = tCID;
            _line = l;
            _speedLimit = sL;
        }

        public Block(int bID)
        {
            throw new Exception(
                "Don't use this constructor please.  Instead, use TrackModel.requestBlockInfo(0,\"LineName\")");
        }

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

        public bool hasTunnel()
        {
            for (int i = 0; i < _attributes.Length; i++)
            {
                if (_attributes[i].Equals("TUNNEL", StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

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

        public int BlockID
        {
            get { return _blockID; }
        }

        public int SpeedLimit
        {
            get { return _speedLimit; }
        }

        public StateEnum State { get; set; }

        public int PrevBlockID
        {
            get { return _prevBlockID; }
        }

        public double StartingElev
        {
            get { return _startingElev; }
        }

        public double Grade
        {
            get { return _grade; }
        }

        public int[] Location
        {
            get { return _location; }
        }

        public double BlockSize { get; set; }

        public DirEnum Direction
        {
            get { return _direction; }
        }

        public int SwitchDest1
        {
            get { return _switchDest1; }
            set { _switchDest1 = value; }
        }

        public int SwitchDest2 { get; set; }

        public int TrackCirID { get; set; }

        public string[] AttrArray
        {
            get { return _attributes; }
        }

        public string Line
        {
            get { return _line; }
        }

        #endregion
    }
}