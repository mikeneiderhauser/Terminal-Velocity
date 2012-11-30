using System;

namespace Utility
{
    public class TickEventArgs : EventArgs
    {
        public TickEventArgs(long ticks)
        {
            Ticks = ticks;
        }

        public long Ticks { get; private set; }
    }
}