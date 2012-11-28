using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    class TestingBlock : IBlock
    {
        private int _id;
        private int _prevID;
        private StateEnum _state;
        private double _size;
        string _line;
        int[] _location;

        public TestingBlock(string line, int id, int prevID, double size, int[] loc)
        {
            _line = line;
            _id = id;
            _prevID = prevID;
            _state = StateEnum.Healthy;
            _size = size;
            _location = loc;
            
        }

        public int BlockID
        {
            get { return _id; }
        }

        public StateEnum State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
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

        public double BlockSize
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public DirEnum Direction
        {
            get { return DirEnum.East; }
        }

        public int SwitchDest1
        {
            get
            {
                return 0;
            }
            set
            {
                //not needed for ctc office testing
            }
        }

        public int SwitchDest2
        {
            get
            {
                return 0;
            }
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
