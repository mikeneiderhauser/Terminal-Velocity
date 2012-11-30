using System;

namespace CTCOffice
{
    public class SpeedToolEventArgs : EventArgs
    {
        public SpeedToolEventArgs(double speed)
        {
            Speed = speed;
        }

        public double Speed { get; set; }
    }
}