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

        private int _ID;

        private int _dirty;

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

        /// <summary>
        /// Whether or not an update is required
        /// </summary>
        public bool UpdateRequired
        {
            get 
            {
                int d = _dirty;
                _dirty = 0;

                if (d > 10)
                    return true;
                return false;
            }
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
            Dictionary<int, ITrainModel> trains = _circuit.Trains;
            Dictionary<int, IBlock> blocks = _circuit.Blocks;

            if (trains.Count != _trains.Count)
                _dirty++;

            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].State != _blocks[i].State)
                    _dirty++;
                if (blocks[i].SwitchDest1 != _blocks[i].SwitchDest1)
                    _dirty++;
                if (blocks[i].SwitchDest2 != _blocks[i].SwitchDest2)
                    _dirty++;
            }

            _trains = trains;
            _blocks = blocks;

            PLC_DoWork();

            _dirty++;
        }

        #endregion // Events
        
    }
}
