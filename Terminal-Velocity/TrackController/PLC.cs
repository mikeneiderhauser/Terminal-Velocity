using System;
using System.Collections.Generic;
using Interfaces;

namespace TrackController
{
    public class Plc
    {
        private readonly ISimulationEnvironment _env;
        private readonly ITrackCircuit _circuit;
        private readonly Dictionary<int, IBlock> _broken;

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
            _broken = new Dictionary<int, IBlock>();
        }

        /// <summary>
        ///     Checks whether we are in a safe state and performs actions to ensure it
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        /// <param name="messages">A list of messages set by the PLC</param>
        public void IsSafe(List<IBlock> blocks, List<ITrainModel> trains, List<IRoute> routes, List<string> messages)
        {
            // Each track controller shall have one switch at maximum
            bool noSwitch = blocks.TrueForAll(o => o.hasSwitch() == false);

            foreach (IBlock b in blocks)
            {
                // Check for broken track
                if (b.State == StateEnum.BrokenTrackFailure)
                {
                    if (!_broken.ContainsKey(b.BlockID))
                    {
                        messages.Add(string.Format("Block {0} {1}", b.BlockID, Enum.GetName(typeof (StateEnum), b.State)));
                        _broken.Add(b.BlockID, b);
                    }
                }
            }

            foreach (ITrainModel t in trains)
            {
                var speedLim = t.CurrentBlock.SpeedLimit;
                var authority = t.AuthorityLimit;

                // Stop the train
                if (_broken.Count > 0 && noSwitch)
                    authority = 0;

                foreach (var n in trains)
                {
                    // Number of blocks till the next train
                    int lengh = _env.TrackModel.requestPath(t.CurrentBlock.BlockID, n.CurrentBlock.BlockID, t.CurrentBlock.Line).Length;
                    // Stop the train for now
                    if (lengh <= 1)
                        speedLim = 0;
                        // Slow the train by half
                    else if (lengh <= 2)
                        speedLim /= 2;
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
        public void ToggleLights(List<IBlock> blocks, List<ITrainModel> trains, List<IRoute> routes,
                                 List<string> messages)
        {
            foreach (var t in trains)
            {
                t.LightsOn = t.CurrentBlock.hasTunnel();
            }
        }

        /// <summary>
        /// Toggles the switch controlled by the TrackController (one per TrackController)
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        /// <param name="messages">A list of messages set by the PLC</param>
        public void ToggleSwitches(List<IBlock> blocks, List<ITrainModel> trains, List<IRoute> routes,
                                   List<string> messages)
        {
            foreach (var t in trains)
            {
            }
        }
    }
}