using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace TrackController
{
    public class TrackController
    {
        private PLC _plc;
        private IEnvironment _env;

        private ITrackCircuit _circuit;

        private ITrackController _prev;
        private ITrackController _next;

        private List<IBlock> _blocks;
        private List<ITrain> _trains;
        private List<IRoute> _routes;

        private int _ID;

        #region Constructor(s)

        /// <summary>
        /// Constructs a new instance of the TrackController class
        /// </summary>
        /// <param name="env">A reference to the global environment object</param>
        /// <param name="circuit">A track circuit for the track controller</param>
        /// <param name="prev">The previous track controller, or null</param>
        /// <param name="next">The next track contrtoller, or null</param>
        public TrackController(IEnvironment env, ITrackCircuit circuit, ITrackController prev, ITrackController next)
        {
            _env = env;
            _env.Tick += _env_Tick;

            _circuit = circuit;
            _circuit.TrainDetected += _circuit_TrainDetected;

            _prev = prev;
            _next = next;

            if (_prev != null)
                _ID = _prev.ID;
            else
                _ID = 0;
        }

        #endregion // Constructor(s)

        #region Public Properties

        public IRequest Request
        {
            set { HandleRequest(value); }
        }

        public int ID
        {
            get { return _ID; }
        }

        public ITrackController Previous
        {
            get { return _prev; }
            set { _prev = value; }
        }

        public ITrackController Next
        {
            get { return _next; }
        }

        public List<ITrain> Trains
        {
            get { return _trains; }
        }

        public List<IBlock> Blocks
        {
            get { return _blocks; }
        }

        public List<IRoute> Routes
        {
            get { return _routes; }
        }

        #endregion // Public Properties

        #region Public Methods

        public void LoadPLCProgram(string filename)
        {
            _plc = new PLC(filename);
        }

        #endregion // Public Methods

        #region Private Methods

        // Private method for handling the request object
        private void HandleRequest(IRequest request)
        {
            //if request.ID = this.ID
                // Populate the object
            //else if ALL
                // Populate the object
            //else 
            if (Next != null)
                Next.Request = request;
            //else
                // Environment.CTCOffice.Give(Request) 
        }

        // Calls into the PLC passing in the current Blocks, Trains, and Routes
        private bool PLC_IsSafe()
        {
            return _plc.IsSafe(Blocks, Trains, Routes);
        }

        // Calls into the PLC passing in the current Blocks, Trains, and Routes
        private bool PLC_LightsRequired()
        {
            return _plc.LightsRequired(Blocks, Trains, Routes);
        }

        #endregion // Private Methods

        #region Events

        // A tick has elasped so we need to do work
        private void _env_Tick(object sender, TickEventArgs e)
        {
            bool safe = PLC_IsSafe();
            bool lights = PLC_LightsRequired();
        }

        // A train is detected so add it to the list of trains
        private void _circuit_TrainDetected(object sender, TrainDetectedEventArgs e)
        {
            int trainID = e.TrainID;
            // lookup train
            // Trains.add(new Train);
        }

        #endregion // Events

        /// <summary>
        /// An internal class for representing a PLC
        /// </summary>
        internal class PLC
        {
            /// <summary>
            /// Construct a new instance of a PLC
            /// </summary>
            /// <param name="filename">The file containing the program to load</param>
            public PLC(string filename)
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
        }
    }
}
