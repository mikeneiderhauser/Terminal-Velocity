using System;
using System.Collections.Generic;
using Interfaces;

namespace TrackController
{
    public class Plc
    {
        private readonly ITrackCircuit _circuit;
        private readonly string _filename;
        private readonly Dictionary<int, IBlock> _broken;

        /// <summary>
        ///     Construct a new instance of a PLC
        /// </summary>
        /// <param name="circuit">The track circuit for the TrackController</param>
        /// <param name="filename">The file containing the program to load</param>
        public Plc(ITrackCircuit circuit, string filename = "ladder.xml")
        {
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
            // Get speed limit from TrackModel
            double speedLimit = 10.0;
            int authority = 10;

            foreach (IBlock b in blocks)
            {
                if (b.State == StateEnum.BrokenTrackFailure)
                {
                    // Stop all trains
                    speedLimit = 0D;
                    authority = 0;

                    if (!_broken.ContainsKey(b.BlockID))
                    {
                        messages.Add(string.Format("Block {0} {1}", b.BlockID, Enum.GetName(typeof (StateEnum), b.State)));
                        _broken.Add(b.BlockID, b);
                    }
                }
            }

            foreach (ITrainModel t in trains)
            {
                if (Math.Abs(t.TrainController.SpeedLimit - speedLimit) > 0)
                    messages.Add(string.Format("Train {0} speed changed {1} -> {2}", t.TrainID, t.TrainController.SpeedLimit,
                                               speedLimit));
                _circuit.ToTrain(t.TrainID, speedLimit, authority);
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