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
        Dictionary<int, ITrain> Trains { get; }
        Dictionary<int, IBlock> Blocks { get; }
        Dictionary<int, IRoute> Routes { get; }

        void Recieve(object data);
        void LoadPLCProgram(string filename);
    }
}
