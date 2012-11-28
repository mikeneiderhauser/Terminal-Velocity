using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;

namespace Interfaces
{
    public interface ITrackCircuit
    {
        /// <summary>
        /// The ID of the TrackCircuit, which is also the ID of the TrackController
        /// </summary>
        int ID { get; set; }

        Dictionary<int, ITrainModel> Trains { get; }
        Dictionary<int, IBlock> Blocks { get; }

        void ToTrain(int ID, double speedLimit, int authority);
    }
}
