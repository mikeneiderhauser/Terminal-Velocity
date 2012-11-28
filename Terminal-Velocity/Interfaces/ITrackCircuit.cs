using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;

namespace Interfaces
{
    public interface ITrackCircuit
    {
        int ID { get; set; }
        Dictionary<int, ITrainModel> Trains { get; }
        Dictionary<int, IBlock> Blocks { get; }

        void ToTrain(int ID, int speedLimit, int authority);
    }
}
