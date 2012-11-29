using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;

namespace TrackController
{
    public class PLC
    {
        private ITrackCircuit _circuit;
        private Dictionary<int, IBlock> _broken;

        /// <summary>
        /// Construct a new instance of a PLC
        /// </summary>
        /// <param name="filename">The file containing the program to load</param>
        public PLC(ITrackCircuit circuit, string filename = "ladder.xml")
        {
            _circuit = circuit;
            _broken = new Dictionary<int, IBlock>();
        }

        /// <summary>
        /// Checks whether we are in a safe state and performs actions to ensure it
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
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
                        messages.Add(string.Format("Block {0} {1}", b.BlockID, Enum.GetName(typeof(StateEnum), b.State)));
                        _broken.Add(b.BlockID, b);
                    }
                }
            }

            foreach (ITrainModel t in trains)
            {
                if (t.TrainController.SpeedLimit != speedLimit)
                    messages.Add(string.Format("Train {0} reduced {1} -> {2}", t.TrainID, t.TrainController.SpeedLimit, speedLimit));
                _circuit.ToTrain(t.TrainID, speedLimit, authority); 
            }
        }

        /// <summary>
        /// Checks whether we need to turn on lights and does so if necessary
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        public void ToggleLights(List<IBlock> blocks, List<ITrainModel> trains, List<IRoute> routes, List<string> messages)
        {
        }

        /// <summary>
        /// Performs switching operations as needed
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="trains"></param>
        /// <param name="routes"></param>
        /// <returns></returns>
        public void DoSwitch(List<IBlock> blocks, List<ITrainModel> trains, List<IRoute> routes, List<string> messages)
        {
        }

        #pragma warning disable 0169
        internal class Action
        {

            private List<Edge> _edges;

            public Action(List<Edge> edges)
            {
            }
        }

        internal class Edge
        {
            private List<Action> _actions;

            public Edge(List<Action> actions)
            {
            }
        }
        #pragma warning restore 0169
    }
}
