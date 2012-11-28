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
        private ISimulationEnvironment _env;

        private ITrackCircuit _circuit;

        private ITrackController _prev;
        private ITrackController _next;

        private Dictionary<int, IBlock> _blocks;
        private Dictionary<int, ITrainModel> _trains;
        private Dictionary<int, IRoute> _routes;

        private int _id;

        #region Constructor(s)

        /// <summary>
        /// Constructs a new instance of the TrackController class
        /// </summary>
        /// <param name="env">A reference to the global environment object</param>
        /// <param name="circuit">A track circuit for the track controller</param>
        /// <param name="prev">The previous track controller, or null</param>
        /// <param name="next">The next track contrtoller, or null</param>
        public TrackController(ISimulationEnvironment env, ITrackCircuit circuit)
        {
            _trains = new Dictionary<int, ITrainModel>();
            _blocks = new Dictionary<int, IBlock>();
            _routes = new Dictionary<int, IRoute>();

            _env = env;
            _env.Tick += _env_Tick;

            _id = -1;
            SetID();

            _circuit = circuit;
            _circuit.ID = this._id;

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
            get { return _id; }
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

        public List<ITrainModel> Trains
        {
            get { return _trains.Values.ToList<ITrainModel>(); }
        }

        public List<IBlock> Blocks
        {
            get { return _blocks.Values.ToList<IBlock>(); }
        }

        public List<IRoute> Routes
        {
            get { return _routes.Values.ToList<IRoute>(); }
        }

        #endregion // Public Properties

        #region Public Methods

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
                _id = _prev.ID + 1;
            }
            else
                _id = 0;
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
                            if (request.Info != null)
                                request.Info.Trains = Trains;
                        }
                        return;
                    }
                case RequestTypes.TrackMaintenanceClose:
                    break;
                case RequestTypes.TrackMaintenanceOpen:
                    break;
                case RequestTypes.SetTrainSpeed:
                    if (_trains.Keys.Contains(request.TrainID))
                    {
                        // TrainAuthority also contains the speed, if RequestTypes is SetTrainSpeed
                        _circuit.ToTrain(request.TrainID, request.TrainAuthority, -1);
                    }
                    break;
                case RequestTypes.SetTrainAuthority:
                    if (_trains.Keys.Contains(request.TrainID))
                    {
                        _circuit.ToTrain(request.TrainID, -1, request.TrainAuthority);
                    }
                    break;
            }

            // Send the request object to the next TC or return it
            if (Next != null)
                Next.Request = request;
            else
                _env.CTCOffice.handleResponse(request);
        }

        // Calls into the PLC passing in the current Blocks, Trains, and Routes
        private void PLC_DoWork()
        {
            // Snapshot values
            List<IBlock> sb = Blocks;
            List<ITrainModel> st = Trains;
            List<IRoute> sr = Routes;

            _plc.ToggleLights(sb, st, sr);
            _plc.DoSwitch(sb, st, sr);
            _plc.IsSafe(sb, st, sr);
        }

        #endregion // Private Methods

        #region Events

        // A tick has elasped so we need to do work
        private void _env_Tick(object sender, TickEventArgs e)
        {
            if (_prev == null)
            {
                Dictionary<int, ITrainModel> trains = _circuit.Trains;

                var differences = trains.Where(x => !_circuit.Trains.Any(x1 => x1.Key == x.Key)).Union(_circuit.Trains.Where(x => !trains.Any(x1 => x1.Key == x.Key)));
                foreach (KeyValuePair<int, ITrainModel> k in differences)
                {
                    _env.AllTrains.Add(k.Value);
                }
            }
            else if (_next == null)
            {
                Dictionary<int, ITrainModel> trains = _circuit.Trains;

                var differences = trains.Where(x => !_circuit.Trains.Any(x1 => x1.Key == x.Key)).Union(_circuit.Trains.Where(x => !trains.Any(x1 => x1.Key == x.Key)));
                foreach (KeyValuePair<int, ITrainModel> k in differences)
                {
                    _env.AllTrains.Remove(k.Value);
                }
            }

            _trains = _circuit.Trains;
            _blocks = _circuit.Blocks;

            PLC_DoWork();

        }

        #endregion // Events
        
    }
}
