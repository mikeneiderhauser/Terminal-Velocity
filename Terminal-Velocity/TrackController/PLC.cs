using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;

namespace TrackController
{
    public class PLC
    {
        /// <summary>
        /// Construct a new instance of a PLC
        /// </summary>
        /// <param name="filename">The file containing the program to load</param>
        public PLC(string filename = "ladder.xml")
        {
        }

        /// <summary>
        /// Checks whether we are in a safe state and performs actions to ensure it
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        public void IsSafe(List<IBlock> blocks, List<ITrainModel> trains, List<IRoute> routes)
        {
        }

        /// <summary>
        /// Checks whether we need to turn on lights and does so if necessary
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        public void ToggleLights(List<IBlock> blocks, List<ITrainModel> trains, List<IRoute> routes)
        {
        }

        /// <summary>
        /// Performs switching operations as needed
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="trains"></param>
        /// <param name="routes"></param>
        /// <returns></returns>
        public void DoSwitch(List<IBlock> blocks, List<ITrainModel> trains, List<IRoute> routes)
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
