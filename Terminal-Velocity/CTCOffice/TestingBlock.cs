using System;
using Interfaces;

namespace CTCOffice
{
    public class TestingBlock : IBlock
    {
        private readonly int _id;
        private readonly string _line;
        private readonly int[] _location;
        private readonly int _prevID;

        private readonly bool _track;
        private readonly bool _sw;
        private readonly bool _tunnel;
        private readonly bool _heater;
        private readonly bool _crossing;
        private readonly bool _station;
        private StateEnum _state;

        public TestingBlock(string line, int id, int prevID, double size, int[] loc, bool track, bool sw, bool tunnel, bool heater, bool crossing, bool station, StateEnum state)
        {
            _line = line;
            _id = id;
            _prevID = prevID;
            State = StateEnum.Healthy;
            BlockSize = size;
            _location = loc;
            _track = track;
            _sw = sw;
            _tunnel = tunnel;
            _heater = heater;
            _crossing = crossing;
            _station = station;
            _state = state;
        }

        public int BlockID
        {
            get { return _id; }
        }

        public StateEnum State { get { return _state; } set { _state = value; } }

        public int SpeedLimit
        {
            get { return 50;}
        }

        public int PrevBlockID
        {
            get { return _prevID; }
        }

        public double StartingElev
        {
            get { return 0; }
        }

        public double Grade
        {
            get { return 0; }
        }

        public int[] Location
        {
            get { return _location; }
        }

        public double BlockSize { get; set; }

        public DirEnum Direction
        {
            get { return DirEnum.East; }
        }

        public int SwitchDest1
        {
            get { return 0; }
            set
            {
                //not needed for ctc office testing
            }
        }

        public int SwitchDest2
        {
            get { return 0; }
            set
            {
                //not needed for CTC Office Testing
            }
        }

        public int TrackCirID
        {
            get
            {
                //not needed for CTC Office Testing
                return 0;
            }
            set
            {
                //not needed for CTC Office Testing
            }
        }

        public string[] AttrArray
        {
            get { return null; }
        }

        public string Line
        {
            get { return _line; }
        }

        public int nextBlockIndex(int prevBlockIndex)
        {
            throw new NotImplementedException();
        }

        #region Bool Attribs

        public bool hasSwitch()
        {
            return _sw;
        }

        public bool hasTunnel()
        {
            return _tunnel;
        }

        public bool hasHeater()
        {
            return _heater;
        }

        public bool hasCrossing()
        {
            return _crossing;
        }

        public bool hasStation()
        {
            return _station;
        }

        public bool runsNorth()
        {
            return false;
        }

        public bool runsSouth()
        {
            return false;
        }

        public bool runsEast()
        {
            return false;
        }

        public bool runsWest()
        {
            return false;
        }

        public bool runsNorthEast()
        {
            return false;
        }

        public bool runsNorthWest()
        {
            return false;
        }

        public bool runsSouthEast()
        {
            return false;
        }

        public bool runsSouthWest()
        {
            return false;
        }

        #endregion
    }
}