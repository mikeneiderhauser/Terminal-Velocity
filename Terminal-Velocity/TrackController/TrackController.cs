using System.Collections.Generic;
using System.Linq;
using Interfaces;
using TrainModel;
using Utility;

namespace TrackController
{
    public class TrackController : ITrackController
    {
        private readonly ITrackCircuit _circuit;
        private readonly ISimulationEnvironment _env;

        private readonly Dictionary<int, IRoute> _routes;
        private Dictionary<int, IBlock> _blocks;

        private int _id;

        private List<string> _messages;
        private ITrackController _next;
        private PLC _plc;
        private ITrackController _prev;
        private Dictionary<int, ITrainModel> _trains;

        #region Constructor(s)

        /// <summary>
        ///     Constructs a new instance of the TrackController class
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
            _circuit.ID = _id;

            // PROTOTYPE - static PLC
            _plc = new PLC(_circuit);

            _messages = new List<string>();
        }

        #endregion // Constructor(s)

        #region Public Properties

        public List<string> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

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
            set { _next = value; }
        }

        public List<ITrainModel> Trains
        {
            get { return _trains.Values.ToList(); }
        }

        public List<IBlock> Blocks
        {
            get { return _blocks.Values.ToList(); }
        }

        public List<IRoute> Routes
        {
            get { return _routes.Values.ToList(); }
        }

        #endregion // Public Properties

        #region Public Methods

        public void LoadPLCProgram(string filename)
        {
            _plc = new PLC(_circuit, filename);
        }

        #endregion // Public Methods

        #region Private Methods

        // Private method for handling the request object
        private static int trainCount;

        private void SetID()
        {
            if (_prev != null)
            {
                _id = _prev.ID + 1;
            }
            else
                _id = 0;
        }

        private void HandleRequest(IRequest request)
        {
            switch (request.RequestType)
            {
                case RequestTypes.TrackControllerData:
                    {
                        if (request.TrackControllerID == ID)
                        {
                            if (request.Info != null)
                                request.Info.Trains = Trains;
                        }
                    }
                    break;
                case RequestTypes.DispatchTrain:
                    {
                        IBlock start = _env.TrackModel.requestBlockInfo(0, "Red");
                        _env.addTrain(new Train(trainCount++, start, _env));
                    }
                    break;
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

            _plc.ToggleLights(sb, st, sr, _messages);
            _plc.DoSwitch(sb, st, sr, _messages);
            _plc.IsSafe(sb, st, sr, _messages);
        }

        #endregion // Private Methods

        #region Events

        // A tick has elasped so we need to do work
        private void _env_Tick(object sender, TickEventArgs e)
        {
            //if (_prev == null)
            //{
            //    Dictionary<int, ITrainModel> trains = _circuit.Trains;

            //    var differences = trains.Where(x => !_circuit.Trains.Any(x1 => x1.Key == x.Key)).Union(_circuit.Trains.Where(x => !trains.Any(x1 => x1.Key == x.Key)));
            //    foreach (KeyValuePair<int, ITrainModel> k in differences)
            //    {
            //        _env.AllTrains.Add(k.Value);
            //    }
            //}
            if (_next == null)
            {
                Dictionary<int, ITrainModel> trains = _circuit.Trains;

                IEnumerable<KeyValuePair<int, ITrainModel>> differences =
                    trains.Where(x => !_circuit.Trains.Any(x1 => x1.Key == x.Key))
                          .Union(_circuit.Trains.Where(x => !trains.Any(x1 => x1.Key == x.Key)));
                foreach (var k in differences)
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