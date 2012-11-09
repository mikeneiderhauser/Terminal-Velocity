using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;

namespace Interfaces
{
    public interface IEnvironment
    {
        event EventHandler<TickEventArgs> Tick;
    }
}
