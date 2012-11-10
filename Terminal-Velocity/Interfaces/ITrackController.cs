using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public class ITrackController
    {
        public IRequest Request;
        public int ID;
        public ITrackController Previous;
        public ITrackController Next;
        public List<ITrain> Trains;
        public List<IBlock> Blocks;
        public List<IRoute> Routes;
    }
}
