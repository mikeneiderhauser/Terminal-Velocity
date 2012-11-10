using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;

namespace Interfaces
{
    public class ITrackCircuit
    {
        public event EventHandler<TrainDetectedEventArgs> TrainDetected;
    }
}
