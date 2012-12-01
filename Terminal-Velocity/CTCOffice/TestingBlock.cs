using System;
using Interfaces;

namespace CTCOffice
{
    internal class TestingBlock : IBlock
    {
        private readonly int _id;
        private readonly string _line;
        private readonly int[] _location;
        private readonly int _prevID;

        public TestingBlock(string line, int id, int prevID, double size, int[] loc)
        {
            _line = line;
            _id = id;
            _prevID = prevID;
            State = StateEnum.Healthy;
            BlockSize = size;
            _location = loc;
        }

        public int BlockID
        {
            get { return _id; }
        }

        public StateEnum State { get; set; }

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
            return false;
        }

        public bool hasTunnel()
        {
            return false;
        }

        public bool hasHeater()
        {
            return false;
        }

        public bool hasCrossing()
        {
            return false;
        }

        public bool hasStation()
        {
            return false;
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