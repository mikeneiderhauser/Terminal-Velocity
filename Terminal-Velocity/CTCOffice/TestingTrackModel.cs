﻿using System;
using System.Collections.Generic;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrackModel : ITrackModel
    {
        private readonly List<IBlock> _blocks;
        private readonly IBlock[,] _greenLineLayout;
        private readonly IBlock[,] _redLineLayout;
        private ISimulationEnvironment _env;

        public TestingTrackModel(ISimulationEnvironment env)
        {
            _env = env;
            _blocks = new List<IBlock>();

            var r0 = new TestingBlock("Red", 0, 0, 50, null);
            var r1 = new TestingBlock("Red", 1, 0, 50, null);
            var r2 = new TestingBlock("Red", 2, 1, 50, null);
            var r3 = new TestingBlock("Red", 3, 2, 50, null);

            var g0 = new TestingBlock("Green", 4, 4, 50, null);
            var g1 = new TestingBlock("Green", 5, 4, 50, null);
            var g2 = new TestingBlock("Green", 6, 5, 50, null);
            var g3 = new TestingBlock("Green", 7, 6, 50, null);

            _blocks.Add(r0);
            _blocks.Add(r1);
            _blocks.Add(r2);
            _blocks.Add(r3);
            _blocks.Add(g0);
            _blocks.Add(g1);
            _blocks.Add(g2);
            _blocks.Add(g3);

            _redLineLayout = new IBlock[1,4];
            _greenLineLayout = new IBlock[1,4];

            _redLineLayout[0, 0] = r0;
            _redLineLayout[0, 1] = r1;
            _redLineLayout[0, 2] = r2;
            _redLineLayout[0, 3] = r3;

            _greenLineLayout[0, 0] = g0;
            _greenLineLayout[0, 1] = g1;
            _greenLineLayout[0, 2] = g2;
            _greenLineLayout[0, 3] = g3;
        }

        public IRouteInfo requestRouteInfo(int routeID)
        {
            throw new NotImplementedException();
        }

        public IBlock[,] requestTrackGrid(int routeID)
        {
            if (routeID == 0)
            {
                return _redLineLayout;
            }
            else
            {
                return _greenLineLayout;
            }
        }

        public bool requestUpdateSwitch(IBlock bToUpdate)
        {
            throw new NotImplementedException();
        }

        public bool requestUpdateBlock(IBlock blockToChange)
        {
            throw new NotImplementedException();
        }

        public IBlock requestBlockInfo(int blockID, string line)
        {
            throw new NotImplementedException();
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

        //Property
        public TrackChanged ChangeFlag
        {
            get { return TrackChanged.Both; }
        }

        public bool RedLoaded
        {
            get { return false; }
        }

        public bool GreenLoaded
        {
            get { return false; }
        }
    }
}