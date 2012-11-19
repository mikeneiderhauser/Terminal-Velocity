using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface ITrackModel
    {
        IBlock requestBlockInfo(int blockID);
        IRouteInfo requestRouteInfo(int routeID);

        IBlock[][] requestTrackGrid(int routeID);

        bool requestUpdateSwitch(IBlock bToUpdate);
        bool requestUpdateBlock(IBlock blockToChange);
    }
}
