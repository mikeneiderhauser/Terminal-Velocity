using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;

namespace Interfaces
{
    public interface ITrackCircuit
    {
        event EventHandler<TrainDetectedEventArgs> TrainDetected;

        void ToTrackController(ITrain train);
        void ToTrain(int ID);
    }
}
