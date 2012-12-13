using System;
using System.Collections.Generic;
using Interfaces;

namespace TrainController
{
    public class TestingTrackModel : ITrackModel
    {
        private List<IBlock> _redBlocks;
        private List<IBlock> _greenBlocks;
        private IBlock[,] _greenLineLayout;
        private IBlock[,] _redLineLayout;
        private IBlock[] _redPath;
        private IBlock[] _greenPath;
        private ISimulationEnvironment _env;

        public TestingTrackModel(ISimulationEnvironment env)
        {
            _env = env;
            _redBlocks = new List<IBlock>();
            _greenBlocks = new List<IBlock>();

            IBlock r0 = new TestingBlock("Red", 0, 6, 100, null, true, false, false, false, false, false, StateEnum.Healthy);
            IBlock r1 = new TestingBlock("Red", 1, 0, 100, null, true, false, false, false, false, false, StateEnum.Healthy);
            IBlock r2 = new TestingBlock("Red", 2, 1, 100, null, false, true, false, false, false, false, StateEnum.Healthy);
            IBlock r3 = new TestingBlock("Red", 3, 2, 100, null, false, false, true, false, false, false, StateEnum.Healthy);
            IBlock r4 = new TestingBlock("Red", 4, 3, 100, null, false, false, false, true, false, false, StateEnum.Healthy);
            IBlock r5 = new TestingBlock("Red", 5, 4, 100, null, false, false, false, false, true, false, StateEnum.Healthy);
            IBlock r6 = new TestingBlock("Red", 6, 5, 100, null, false, false, false, false, false, true, StateEnum.Healthy);

            _redBlocks.Add(r0);
            _redBlocks.Add(r1);
            _redBlocks.Add(r2);
            _redBlocks.Add(r3);
            _redBlocks.Add(r4);
            _redBlocks.Add(r5);
            _redBlocks.Add(r6);

            _redLineLayout = new IBlock[1, 7];
            _redLineLayout[0, 0] = r0;
            _redLineLayout[0, 1] = r1;
            _redLineLayout[0, 2] = r2;
            _redLineLayout[0, 3] = r3;
            _redLineLayout[0, 4] = r4;
            _redLineLayout[0, 5] = r5;
            _redLineLayout[0, 6] = r6;

            _redPath = _redBlocks.ToArray();

            IBlock g0 = new TestingBlock("Green", 0, 6, 100, null, true, false, false, false, false, false, StateEnum.Healthy);
            IBlock g1 = new TestingBlock("Green", 1, 0, 100, null, true, false, false, false, false, false, StateEnum.Healthy);
            IBlock g2 = new TestingBlock("Green", 2, 1, 100, null, false, true, false, false, false, false, StateEnum.Healthy);
            IBlock g3 = new TestingBlock("Green", 3, 2, 100, null, false, false, true, false, false, false, StateEnum.Healthy);
            IBlock g4 = new TestingBlock("Green", 4, 3, 100, null, false, false, false, true, false, false, StateEnum.Healthy);
            IBlock g5 = new TestingBlock("Green", 5, 4, 100, null, false, false, false, false, true, false, StateEnum.Healthy);
            IBlock g6 = new TestingBlock("Green", 6, 5, 100, null, false, false, false, false, false, true, StateEnum.Healthy);

            _greenBlocks.Add(g0);
            _greenBlocks.Add(g1);
            _greenBlocks.Add(g2);
            _greenBlocks.Add(g3);
            _greenBlocks.Add(g4);
            _greenBlocks.Add(g5);
            _greenBlocks.Add(g6);

            _greenLineLayout = new IBlock[1, 7];
            _greenLineLayout[0, 0] = g0;
            _greenLineLayout[0, 1] = g1;
            _greenLineLayout[0, 2] = g2;
            _greenLineLayout[0, 3] = g3;
            _greenLineLayout[0, 4] = g4;
            _greenLineLayout[0, 5] = g5;
            _greenLineLayout[0, 6] = g6;

            _greenPath = _greenBlocks.ToArray();

        }

        public event EventHandler<EventArgs> TrackChangedEvent;

        public IRouteInfo requestRouteInfo(int routeID)
        {
            if (routeID == 0)
            {
                return new TestingRouteInfo(routeID, "Red", _redBlocks.Count, _redPath, 1, 6);
            }
            else if (routeID == 1)
            {
                return new TestingRouteInfo(routeID, "Green", _greenBlocks.Count, _greenPath, 1, 6);
            }
            return null;
        }

        public IBlock[] requestPath(int startBlock, int endBlock, string line)
        {
            if (line.CompareTo("Red") == 0)
            {
                return _redPath;
            }

            if (line.CompareTo("Green") == 0)
            {
                return _greenPath;
            }

            return null;
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
            //not needed for ctc simulation
            //throw new NotImplementedException();
            return true;
        }

        public bool requestUpdateBlock(IBlock blockToChange)
        {
             //not needed for ctc simulation
            //throw new NotImplementedException();
            return true;
        }

        public IBlock requestBlockInfo(int blockID, string line)
        {
            if (line.CompareTo("Red") == 0)
            {
                return _redPath[blockID];
            }

            if (line.CompareTo("Green") == 0)
            {
                return _greenPath[blockID];
            }

            return null;
        }

        //Property
        public TrackChanged ChangeFlag
        {
            get { return TrackChanged.Both; }
        }

        public bool RedLoaded
        {
            get { return true; }
        }

        public bool GreenLoaded
        {
            get { return true; }
        }

        public void ThrowTrackChanged()
        {
            if (TrackChangedEvent != null)
            {
                TrackChangedEvent(this, EventArgs.Empty);
            }
        }

        public void CloseBlock(int id, string line)
        {
            if (line.CompareTo("Red") == 0)
            {
                _redPath[id].State = StateEnum.BrokenTrackFailure;
            }

            if (line.CompareTo("Green") == 0)
            {
                _greenPath[id].State = StateEnum.BrokenTrackFailure;
            }
        }

        public void OpenBlock(int id, string line)
        {
            if (line.CompareTo("Red") == 0)
            {
                _redPath[id].State = StateEnum.Healthy;
            }

            if (line.CompareTo("Green") == 0)
            {
                _greenPath[id].State = StateEnum.Healthy;
            }
        }
    }
}