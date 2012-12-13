using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="proximityTrain">Whether a train is within 3 blocks past the start of the next TC</param>
        /// <param name="proximityBlock">Whether a broken block is within 3 blocks past the start of the next TC</param>
        public void IsSafe(List<IBlock> blocks, List<ITrainModel> trains, Dictionary<int, List<IBlock>> routes,
                           List<string> messages, bool proximityTrain, bool proximityBlock)
        {
            // Get the station location, if any
            IBlock[] station = blocks.Where(x => x.hasStation()).ToArray();

            // Collection of broken blocks
            _broken =
                blocks.Where(o => (o.State == StateEnum.BlockClosed || o.State == StateEnum.BrokenTrackFailure)).ToList();

            // Set train speeds an authorities
            foreach (ITrainModel t in trains)
            {
                int distanceToEnd =
                    _env.TrackModel.requestPath(t.CurrentBlock.BlockID, blocks[blocks.Count - 1].BlockID,
                                                t.CurrentBlock.Line).Length;
                int speedLim = t.CurrentBlock.SpeedLimit;
                int authority = 3;

                // If there is a station, give its block id to the trainController
                if (station.Length > 0)
                {
                    t.TrainController.DistanceToStation =
                        _env.TrackModel.requestPath(t.CurrentBlock.BlockID, station[0].BlockID, t.CurrentBlock.Line).
                            Length;
                }

                // Adjust train speed to match that of the track speed limit
                // or if the train is too close to another train
                foreach (ITrainModel n in trains.Where(x => x.TrainID != t.TrainID))
                {
                    // Number of blocks till the next train (assumes the Route accounted for)
                    int length =
                        _env.TrackModel.requestPath(t.CurrentBlock.BlockID, n.CurrentBlock.BlockID, t.CurrentBlock.Line)
                            .Length;
                    // Stop the train for now
                    if (length < 3 || (distanceToEnd < 3 && proximityBlock))
                    {
                        authority = 0;
                        messages.Add(string.Format("Train {0} is near train {1} (stopping)", t.TrainID, n.TrainID));
                    }

                    // Slow the train by half if close to another (in this section or the next)
                    if (length < 5 || (distanceToEnd < 3 && proximityTrain))
                    {
                        speedLim = t.CurrentBlock.SpeedLimit/2;
                        messages.Add(string.Format("Train {0} is near train {1} (slowing)", t.TrainID, n.TrainID));
                    }
                }

                // Handle broken blocks
                // Overrides previous authority
                if (_broken.Count > 0)
                {
                    foreach (IBlock b in _broken)
                    {
                        // Length of the path to the next broken block
                        int length = _env.TrackModel.requestPath(t.CurrentBlock.BlockID, b.BlockID, b.Line).Count();

                        // Stop the train if a broken block is too close
                        if (length < 3)
                        {
                            authority = 0;
                            messages.Add(string.Format("Train {0} is too close to broken block {1}", t.TrainID,
                                                       b.BlockID));
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
        public void ToggleLights(List<IBlock> blocks, List<ITrainModel> trains, Dictionary<int, List<IBlock>> routes,
                                 List<string> messages)
        {
            foreach (ITrainModel t in trains)
            {
                t.LightsOn = t.CurrentBlock.hasTunnel();
            }

            if (trains.Count > 0)
            {
                foreach (IBlock b in blocks.Where(b => b.hasCrossing()))
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
        public void ToggleSwitches(List<IBlock> blocks, List<ITrainModel> trains, Dictionary<int, List<IBlock>> routes,
                                   List<string> messages)
        {
            foreach (ITrainModel t in trains)
            {
                List<IBlock> trainRoute;
                if (routes.TryGetValue(t.TrainID, out trainRoute))
                {
                    IBlock switchBlock = trainRoute.First(b => b.hasSwitch());
                    IBlock nextBlock = trainRoute.First(b => b.PrevBlockID == switchBlock.BlockID);

                    // Switch the block destinations and update the trackModel
                    if (switchBlock.SwitchDest1 != nextBlock.BlockID)
                    {
                        int dest1 = switchBlock.SwitchDest1;

                        switchBlock.SwitchDest1 = nextBlock.BlockID;
                        switchBlock.SwitchDest2 = dest1;

                        _env.TrackModel.requestUpdateSwitch(switchBlock);

                        messages.Add(string.Format("Block {0} switched: dest {1}, dest {2}", switchBlock.BlockID,
                                                   switchBlock.SwitchDest1, switchBlock.SwitchDest2));
                    }
                }
            }
        }

        /// <summary>
        /// Updates the given blocks
        /// </summary>
        /// <param name="blocks">The blocks to update</param>
        public void UpdateBlocks(List<IBlock> blocks)
        {
            foreach (var b in blocks)
                _env.TrackModel.requestUpdateBlock(b);
        }
    }
}