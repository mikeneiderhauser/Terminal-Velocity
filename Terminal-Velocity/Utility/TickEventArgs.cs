using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class TickEventArgs : EventArgs
    {
        public long Ticks { get; private set; }

        public TickEventArgs(long ticks)
        {
            Ticks = Ticks;
        }
    }
}
