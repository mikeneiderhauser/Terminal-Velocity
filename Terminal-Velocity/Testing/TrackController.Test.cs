using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Interfaces;
using TrackModel;
using TrainModel;
using SimulationEnvironment = SimulationEnvironment.SimulationEnvironment;

namespace Testing
{
    public class TrackControllerTest : ITesting
    {
        static readonly Random R = new Random((int)DateTime.Now.ToBinary());
        static ISimulationEnvironment _env;
        static IBlock _startBlock;

        const int Min = 500;
        const int Max = 3 * Min;
        const int Timeout = 3 * Max;
        static int _elapsed = 0;

        static int _trainCount = 0;

        public bool DoTest(out int pass, out int fail, out List<string> messages)
        {
            pass = 0;
            fail = 0;
            messages = new List<string>();

            // Create 100 blocks for the red line
            var blocks = new List<IBlock>();
            // First block
            blocks.Add(new Block(1, StateEnum.Healthy, 100, 0, 0, new[] { 0, 0 }, 100, DirEnum.East, new[] { "" }, 2, 0, 0, "Red", 100));
            // Next 99 blocks
            for (var i = 2; i < 100; i++)
                blocks.Add(new Block(i, StateEnum.Healthy, i - 1, 0, 0, new[] { 0, 0 }, 100, DirEnum.East, new[] { "" }, i + 1, 0, 0, "Red", 100));
            // Last block
            blocks.Add(new Block(100, StateEnum.Healthy, 99, 0, 0, new[] { 0, 0 }, 100, DirEnum.East, new[] { "" }, 1, 0, 0, "Red", 100));

            // Environment object
            ISimulationEnvironment environment = new global::SimulationEnvironment.SimulationEnvironment();
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

            _env = environment;
            _startBlock = blocks[0];
              
            return DoTestInternal(out pass, out fail, out messages);
        }

        private static bool DoTestInternal(out int pass, out int fail, out List<string> messages)
        {
            pass = 0;
            fail = 0;
            messages = new List<string>();

            // Populate the track with three trains
            // Each train will appear at a random time between Min and Max
            {
                var timer = new Timer { Interval = 100, AutoReset = true };
                timer.Elapsed += TimerElapsed;
                timer.Start();

                while (_env.AllTrains.Count < 3 && _elapsed < Timeout)
                    System.Threading.Thread.Sleep(500);

                if (_elapsed >= Timeout) return false;

                timer.Stop();
                timer.Elapsed -= TimerElapsed;
            }

            // Ensure that the trains have the correct authority and speeds
            {
                foreach (var t in _env.AllTrains)
                {
                    if (t.AuthorityLimit != 1)
                    {
                        fail++;
                        messages.Add(string.Format("[Error] Train {0}: authority was {1}, expected {2}", t.TrainID, t.AuthorityLimit, 1));
                    }
                    else
                        pass++;

                    if (Math.Abs(t.SpeedLimit - 100F) > 0.00001)
                    {
                        fail++;
                        messages.Add(string.Format("[Error] Train {0}: speed limit was {1}, expected {2}", t.TrainID, t.SpeedLimit, 100));
                    }
                    else
                        pass++;
                }
            }

            // Ensure multiple trains do not occupy the same block
            {
                var trains = _env.AllTrains.Where((t, n) => t.TrainID == _env.AllTrains[n].TrainID).ToList();

                foreach (var t in trains)
                {
                    fail++;
                    messages.Add(string.Format("[Error] Train {0} occupies block {1}", t.TrainID, t.CurrentBlock.BlockID));
                }

            }

            // Print the train velocities to console. If they are not moving, the previous tests are invalid
            {
                messages.AddRange(_env.AllTrains.Select(t => string.Format("[Info] Train {0} traveling at velocity {1}", t.TrainID, t.CurrentVelocity)));
            }

            return true;
        }

        static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            var n =  R.Next(Min, Max);
            _elapsed += n;
            ((Timer) sender).Interval = n;

            _env.AllTrains.Add(new Train(_trainCount++, _startBlock, _env));
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