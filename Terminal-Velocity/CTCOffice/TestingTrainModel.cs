using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrainModel : ITrackModel
    {
        public TestingTrainModel()
        {

        }

        public IBlock requestBlockInfo(int blockID)
        {
            throw new NotImplementedException();
        }

        public IRouteInfo requestRouteInfo(int routeID)
        {
            throw new NotImplementedException();
        }

        public IBlock[][] requestTrackGrid(int routeID)
        {
            throw new NotImplementedException();
        }

        public bool requestUpdateSwitch(IBlock bToUpdate)
        {
            throw new NotImplementedException();
        }

        public bool requestUpdateBlock(IBlock blockToChange)
        {
            throw new NotImplementedException();
        }
    }
}
