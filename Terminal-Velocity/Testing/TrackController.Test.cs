using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using TrackModel;

namespace Testing
{
    public class TrackControllerTest : ITesting
    {
        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0;
            fail = 0;
            message = new List<string>();

            var blocks = new List<IBlock>();
            // First block
            blocks.Add(new Block(1, StateEnum.Healthy, 100, 0, 0, new[] { 0, 0 }, 100, DirEnum.East, new[] { "" }, 2, 0, 0, "Red", 100));
            // Next 99 blocks
            for (var i = 2; i < 100; i++)
                blocks.Add(new Block(i, StateEnum.Healthy, i - 1, 0, 0, new[] { 0, 0 }, 100, DirEnum.East, new[] { "" }, i + 1, 0, 0, "Red", 100));
            // Last block
            blocks.Add(new Block(100, StateEnum.Healthy, 99, 0, 0, new[] { 0, 0 }, 100, DirEnum.East, new[] { "" }, 1, 0, 0, "Red", 100));

            // Environment object
            ISimulationEnvironment environment = new SimulationEnvironment.SimulationEnvironment();
            // Our track circuit
            ITrackCircuit currCircuit = new TrackController.TrackCircuit(environment, blocks.Where(x => x.BlockID > 0 && x.BlockID <= 34).ToList());
            // Next track controller's circuit
            ITrackCircuit nextCircuit = new TrackController.TrackCircuit(environment, blocks.Where(x => x.BlockID > 34 && x.BlockID <= 67).ToList());
            // Previous track controller's circuit
            ITrackCircuit prevCircuit = new TrackController.TrackCircuit(environment, blocks.Where(x => x.BlockID > 67 && x.BlockID <= 100).ToList());

            ITrackController prev = new TrackController.TrackController(environment, currCircuit);
            ITrackController curr = new TrackController.TrackController(environment, currCircuit);
            ITrackController next = new TrackController.TrackController(environment, currCircuit);

            prev.Previous = null;
            prev.Next = curr;

            curr.Previous = prev;
            curr.Next = next;

            next.Previous = curr;
            next.Next = null;

            // The CTC Office
            ICTCOffice office = new CTCOffice.CTCOffice(environment, prev, prev);

            environment.CTCOffice = office;
            environment.PrimaryTrackControllerGreen = prev;
            environment.PrimaryTrackControllerRed = prev;
            environment.TrackModel = new DummyTrackModel(blocks);

            return true;
        }

        internal class DummyTrackModel : ITrackModel
        {
            readonly List<IBlock> _blocks; 

            public DummyTrackModel(List<IBlock> blocks)
            {
                _blocks = blocks;

                RedLoaded = true;
                GreenLoaded = false;
            }

            public TrackChanged ChangeFlag { get; private set; }
            public IBlock requestBlockInfo(int blockID, string line)
            {
                return _blocks.First(x => x.BlockID == blockID);
            }

            public IRouteInfo requestRouteInfo(int routeID)
            {
                throw new NotImplementedException();
            }

            public event EventHandler<EventArgs> TrackChangedEvent;
            public IBlock[] requestPath(int startBlockID, int endBlockID, string line)
            {
                return _blocks.Where(x => x.BlockID >= startBlockID && x.BlockID <= endBlockID).ToArray();
            }

            public IBlock[,] requestTrackGrid(int routeID)
            {
                throw new NotImplementedException();
            }

            public bool requestUpdateSwitch(IBlock bToUpdate)
            {
                throw new NotImplementedException();
            }

            public bool requestUpdateBlock(IBlock blockToChange)
            {
                for (var i = 0; i < _blocks.Count; i++)
                {
                    if (_blocks[i].BlockID == blockToChange.BlockID)
                        _blocks[i] = blockToChange;
                }
                return true;
            }

            public bool RedLoaded { get; private set; }
            public bool GreenLoaded { get; private set; }
        }
    }
}