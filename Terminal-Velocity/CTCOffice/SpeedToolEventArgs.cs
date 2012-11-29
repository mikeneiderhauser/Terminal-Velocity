using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public class SpeedToolEventArgs : EventArgs
    {
        private double _speed;

        public SpeedToolEventArgs(double speed)
        {
            _speed = speed;
        }

        public double Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

    }
}
