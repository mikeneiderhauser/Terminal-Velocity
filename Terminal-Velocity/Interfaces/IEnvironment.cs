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

        /// <summary>
        /// A reference to the CTCOffice
        /// </summary>
        ICTCOffice CTCOffice { get; set; }

        /// <summary>
        /// A reference to the first, or primary, track controller
        /// </summary>
        ITrackController PrimaryTrackController { get; set; }
    }
}
