using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IStatus
    {
        List<ITrain> Trains { get; set; }
        List<IBlock> Blocks { get; set; }
    }
}
