using System;
using System.Collections.Generic;
using Interfaces;

namespace TrackController
{
    public class Plc
    {
        private readonly ISimulationEnvironment _env;
        private readonly ITrackCircuit _circuit;
        private readonly string _filename;
        private readonly Dictionary<int, IBlock> _broken;

        private const double EPSILON = 0.0001;

        /// <summary>
        ///     Construct a new instance of a PLC
        /// </summary>
        /// <param name="env">The envrionment </param>
        /// <param name="circuit">The track circuit for the TrackController</param>
        /// <param name="filename">The file containing the program to load</param>
        public Plc(ISimulationEnvironment env, ITrackCircuit circuit, string filename = "ladder.xml")
        {
            _env = env;
            _circuit = circuit;
            _filename = filename;
            _broken = new Dictionary<int, IBlock>();
        }

        public string Filename
        {
            get { return _filename; }
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
            foreach (IBlock b in blocks)
            {            
                if (b.State == StateEnum.BrokenTrackFailure)
                {
                    if (!_broken.ContainsKey(b.BlockID))
                    {
                        // Report the broken block
                        messages.Add(string.Format("Block {0} {1}", b.BlockID, Enum.GetName(typeof (StateEnum), b.State)));
                        _broken.Add(b.BlockID, b);
                    }
                }
            }

            foreach (ITrainModel t in trains)
            {
                double speedLim = t.CurrentBlock.SpeedLimit;
                int authority = 1;

                if (_broken.Count > 0)
                    authority = 0;

                if (Math.Abs(t.TrainController.SpeedLimit - speedLim) > EPSILON)
                    messages.Add(string.Format("Train {0} speed changed {1} -> {2}", t.TrainID,
                                               t.TrainController.SpeedLimit, authority));

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
                if (t.CurrentBlock.hasTunnel())
                    t.LightsOn = true;
            }
    }

        /// <summary>
        ///     Performs switching operations as needed
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="trains"></param>
        /// <param name="routes"></param>
        /// <param name="messages">A list of messages set by the PLC</param>
        public void DoSwitch(List<IBlock> blocks, List<ITrainModel> trains, List<IRoute> routes, List<string> messages)
        {
        }

        internal class Action
        {
            public Action(List<Edge> edges)
            {
                Edges = edges;
            }

            public List<Edge> Edges { get; set; }
        }

        internal class Edge
        {
            public Edge(List<Action> actions)
            {
                Actions = actions;
            }

            public List<Action> Actions { get; set; }
        }
    }
}