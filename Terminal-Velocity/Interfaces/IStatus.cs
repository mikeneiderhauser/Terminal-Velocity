using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IStatus
    {
        public List<ITrain> Trains;
        public List<IBlock> Blocks;
    }
}
