using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface ITrackController
    {
        IRequest Request { set; }
        int ID { get; }
        ITrackController Previous { get; set; }
        ITrackController Next { get; set; }
        List<ITrain> Trains { get; }
        List<IBlock> Blocks { get; }
        List<IRoute> Routes { get; }

        void Recieve(ITrain train);
        void LoadPLCProgram(string filename);
    }
}
