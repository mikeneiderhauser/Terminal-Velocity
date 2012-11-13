using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;

namespace Interfaces
{
    public interface IEnvironment
    {
        /// <summary>
        /// Event that generates clock
        /// </summary>
        event EventHandler<TickEventArgs> Tick;
    }
}
