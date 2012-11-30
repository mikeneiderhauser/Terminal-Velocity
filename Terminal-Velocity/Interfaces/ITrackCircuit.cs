using System.Collections.Generic;

namespace Interfaces
{
    public interface ITrackCircuit
    {
        /// <summary>
        ///     The ID of the TrackCircuit, which is also the ID of the TrackController
        /// </summary>
        int ID { get; set; }

        Dictionary<int, ITrainModel> Trains { get; }
        Dictionary<int, IBlock> Blocks { get; }

        void ToTrain(int ID, double speedLimit, int authority);
    }
}