using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IRoute
    {
        public Enum RouteType;
        public IBlock EndBlock;
        public int RouteID;
        public List<IBlock> RouteBlocks;
    }
}
