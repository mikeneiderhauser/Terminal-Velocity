using System;
using System.Linq;
using System.Collections.Generic;
using Interfaces;

namespace TrackController
{
    public class Plc
    {
        private readonly ISimulationEnvironment _env;
        private readonly ITrackCircuit _circuit;
        private List<IBlock> _broken;

        private const double EPSILON = 0.0001;

        /// <summary>
        ///     Construct a new instance of a PLC
        /// </summary>
        /// <param name="env">The envrionment </param>
        /// <param name="circuit">The track circuit for the TrackController</param>
        public Plc(ISimulationEnvironment env, ITrackCircuit circuit)
        {
            _env = env;
            _circuit = circuit;
            _broken = new List<IBlock>();
        }

        /// <summary>
        ///     Checks whether we are in a safe state and performs actions to ensure it
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        /// <param name="messages">A list of messages set by the PLC</param>
        // TODO: This method assumes a switch can only be found at the END of a section controlled by a TrackController
        public void IsSafe(List<IBlock> blocks, List<ITrainModel> trains, Dictionary<int, List<IBlock>> routes, List<string> messages)
        {
            // Update the trackModel
            //foreach (var b in blocks)
            //    _env.TrackModel.requestUpdateBlock(b);

            // Collection of broken blocks
            _broken = blocks.Where(o => (o.State == StateEnum.BlockClosed || o.State == StateEnum.BrokenTrackFailure)).ToList();

            // Set train speeds an authorities
            foreach (var t in trains)
            {
                var speedLim = t.CurrentBlock.SpeedLimit;
                var authority = 3;

                // Adjust train speed to match that of the track speed limit
                // or if the train is too close to another train
                foreach (var n in trains.Where(x => x.TrainID != t.TrainID))
                {
                    // Number of blocks till the next train (assumes the Route accounted for)
                    var length = _env.TrackModel.requestPath(t.CurrentBlock.BlockID, n.CurrentBlock.BlockID, t.CurrentBlock.Line).Length;
                    // Stop the train for now
                    if (length < 3)
                    {
                        authority = 0;
                        messages.Add(string.Format("Train {0} is near train {1} (stopping)", t.TrainID, n.TrainID));
                    }

                    // Slow the train by half
                    if (length < 5)
                    {
                        speedLim = t.CurrentBlock.SpeedLimit / 2;
                        messages.Add(string.Format("Train {0} is near train {1} (slowing)", t.TrainID, n.TrainID));
                    }
                }

                // Handle broken blocks
                if (_broken.Count > 0)
                {
                    foreach (var b in _broken)
                    {
                        // Length of the path to the next broken block
                        var length = _env.TrackModel.requestPath(t.CurrentBlock.BlockID, b.BlockID, b.Line).Count();

                        // Stop the train if a broken block is too close
                        // TODO: The block may be behind the train
                        if (length < 3)
                        {
                            authority = 0;
                            messages.Add(string.Format("Train {0} is too close to broken block {1}", t.TrainID, b.BlockID));
                        }
                    }
                }

                if (Math.Abs(t.TrainController.SpeedLimit - speedLim) > EPSILON)
                    messages.Add(string.Format("Train {0} speed changed {1} -> {2}", t.TrainID,
                                               t.TrainController.SpeedLimit, speedLim));

                if (t.TrainController.AuthorityLimit != authority)
                    messages.Add(string.Format("Train {0} authority changed {1} -> {2}", t.TrainID,
                                               t.TrainController.AuthorityLimit, authority));

                _circuit.ToTrain(t.TrainID, speedLim, authority);
            }
        }

        /// <summary>
        ///     Checks whether we need to turn on lights and does so if necessary
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        /// <param name="messages">A list of messages set by the PLC</param>
        public void ToggleLights(List<IBlock> blocks, List<ITrainModel> trains, Dictionary<int, List<IBlock>> routes, List<string> messages)
        {
            foreach (var t in trains)
            {
                t.LightsOn = t.CurrentBlock.hasTunnel();
            }

            if (trains.Count > 0)
            {
                foreach (var b in blocks.Where(b => b.hasCrossing()))
                {
                    // TODO lower crossing bars
                }
            }
        }

        /// <summary>
        /// Toggles the switch controlled by the TrackController (one per TrackController)
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        /// <param name="messages">A list of messages set by the PLC</param>
        public void ToggleSwitches(List<IBlock> blocks, List<ITrainModel> trains, Dictionary<int, List<IBlock>> routes, List<string> messages)
        {
            foreach (var t in trains)
            {
                List<IBlock> trainRoute;
                if (routes.TryGetValue(t.TrainID, out trainRoute))
                {
                    var switchBlock = trainRoute.First(b => b.hasSwitch());
                    var nextBlock = trainRoute.First(b => b.PrevBlockID == switchBlock.BlockID);

                    // Switch the block destinations and update the trackModel
                    if (switchBlock.SwitchDest1 != nextBlock.BlockID)
                    {
                        var dest1 = switchBlock.SwitchDest1;

                        switchBlock.SwitchDest1 = nextBlock.BlockID;
                        switchBlock.SwitchDest2 = dest1;

                        _env.TrackModel.requestUpdateSwitch(switchBlock);

                        messages.Add(string.Format("Block {0} switched: dest {1}, dest {2}", switchBlock.BlockID, switchBlock.SwitchDest1, switchBlock.SwitchDest2));
                    }
                }
            }
        }
    }
}