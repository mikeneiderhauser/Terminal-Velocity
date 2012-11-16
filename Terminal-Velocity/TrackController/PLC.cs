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
        /// Checks whether we are in a safe state
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        /// <returns></returns>
        public bool IsSafe(List<IBlock> blocks, List<ITrain> trains, List<IRoute> routes)
        {
            return false;
        }

        /// <summary>
        /// Checks whether we need to turn on lights
        /// </summary>
        /// <param name="blocks">The blocks in question</param>
        /// <param name="trains">The trains in question</param>
        /// <param name="routes">The routes ub quetstion</param>
        /// <returns></returns>
        public bool LightsRequired(List<IBlock> blocks, List<ITrain> trains, List<IRoute> routes)
        {
            return false;
        }

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
    }
}
