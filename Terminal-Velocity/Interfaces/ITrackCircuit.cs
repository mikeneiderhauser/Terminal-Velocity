using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;

namespace Interfaces
{
    public interface ITrackCircuit
    {
        Dictionary<int, ITrainModel> Trains { get; }
        void ToTrackController(ITrainModel data);
        void ToTrain(int ID, int speedLimit, int authority);
    }
}
