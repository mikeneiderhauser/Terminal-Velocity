using System.Collections.Generic;

namespace Interfaces
{
    public interface ITrackController
    {
        IRequest Request { set; }
        int ID { get; }

        ITrackController Previous { get; set; }
        ITrackController Next { get; set; }

        List<ITrainModel> Trains { get; }
        List<IBlock> Blocks { get; }
        Dictionary<int, List<IBlock>>  Routes { get; }
    }
}