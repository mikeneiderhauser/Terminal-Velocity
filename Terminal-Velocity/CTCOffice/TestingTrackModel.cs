using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrackModel : ITrackModel
    {
        private List<IBlock> _blocks;

        public TestingTrackModel()
        {
            _blocks = new List<IBlock>();
            
        }

        public IBlock requestBlockInfo(int blockID)
        {
            if (_blocks.Count > blockID)
            {
                return _blocks[blockID];
            }
            else
            {
                return null;
            }
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
