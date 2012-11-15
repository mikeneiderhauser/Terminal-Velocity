using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace TrackController
{
    public class TrackController : ITrackController
    {
        private PLC _plc;
        private IEnvironment _env;

        private ITrackCircuit _circuit;

        private ITrackController _prev;
        private ITrackController _next;

        private Dictionary<int, IBlock> _blocks;
        private Dictionary<int, ITrain> _trains;
        private Dictionary<int, IRoute> _routes;

        private int _ID;

        #region Constructor(s)

        /// <summary>
        /// Constructs a new instance of the TrackController class
        /// </summary>
        /// <param name="env">A reference to the global environment object</param>
        /// <param name="circuit">A track circuit for the track controller</param>
        /// <param name="prev">The previous track controller, or null</param>
        /// <param name="next">The next track contrtoller, or null</param>
        public TrackController(IEnvironment env, ITrackCircuit circuit)
        {
            _trains = new Dictionary<int, ITrain>();
            _blocks = new Dictionary<int, IBlock>();
            _routes = new Dictionary<int, IRoute>();

            _env = env;
            _env.Tick += _env_Tick;

            _circuit = circuit;

            _ID = -1;
            SetID();

            // PROTOTYPE - static PLC
            _plc = new PLC();
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
            set 
            { 
                _prev = value;
                SetID();
            }
        }

        public ITrackController Next
        {
            get { return _next; }
            set 
            { 
                _next = value;
                SetID();
            }
        }

        public Dictionary<int, ITrain> Trains
        {
            get { return _trains; }
        }

        public Dictionary<int, IBlock> Blocks
        {
            get { return _blocks; }
        }

        public Dictionary<int, IRoute> Routes
        {
            get { return _routes; }
        }

        #endregion // Public Properties

        #region Public Methods

        /// <summary>
        /// Recieve and process data sent from a train
        /// </summary>
        /// <param name="data"></param>
        public void Recieve(object data)
        {
            // foreach ITrain in Train
            // if not found, error
            // else do work if ID matches
        }

        public void LoadPLCProgram(string filename)
        {
            _plc = new PLC(filename);
        }

        #endregion // Public Methods

        #region Private Methods

        private void SetID()
        {
            if (_prev != null)
            {
                _ID = _prev.ID + 1;
            }
            else
                _ID = 0;
        }

        // Private method for handling the request object
        private void HandleRequest(IRequest request)
        {
            switch (request.RequestType)
            {
                case RequestTypes.TrackControllerData:
                    {
                        if (request.TrackControllerID == this.ID)
                        {
                            request.Info.Trains = Trains.Values.ToList<ITrain>();
                        }
                        return;
                    }
                case RequestTypes.TrackMaintenanceClose:
                    break;
                case RequestTypes.TrackMaintenanceOpen:
                    break;
                case RequestTypes.SetTrainSpeed:
                    if (Trains.Keys.Contains(request.TrainID))
                    {
                        // set train speed
                    }
                    break;
                case RequestTypes.SetTrainAuthority:
                    if (Trains.Keys.Contains(request.TrainID))
                    {
                        _circuit.ToTrain(request.TrainID, -1, request.TrainAuthority);
                    }
                    break;
            }

            // Send the request object to the next TC or return it
            if (Next != null)
                Next.Request = request;
            else
                _env.CTCOffice.passRequest(request);
        }

        // Calls into the PLC passing in the current Blocks, Trains, and Routes
        private void PLC_DoWork()
        {
            // Snapshot values
            List<IBlock> sb = Blocks.Values.ToList();
            List<ITrain> st = Trains.Values.ToList();
            List<IRoute> sr = Routes.Values.ToList();

            // _plc.LightsRequired(sb, st, sr);
            // _plc.IsSafe(sb, st, sr);
        }

        #endregion // Private Methods

        #region Events

        // A tick has elasped so we need to do work
        private void _env_Tick(object sender, TickEventArgs e)
        {
            _trains = _circuit.Trains;
            PLC_DoWork();
        }

        #endregion // Events
        
    }
}
