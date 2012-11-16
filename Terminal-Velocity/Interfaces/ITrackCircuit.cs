using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;

namespace Interfaces
{
    public interface ITrackCircuit
    {
        Dictionary<int, ITrain> Trains { get; }
        void ToTrackController(object data);
        void ToTrain(int ID, int speedLimit, int authority);
    }
}
