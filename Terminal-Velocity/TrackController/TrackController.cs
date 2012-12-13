using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Utility;

namespace TrackController
{
    public class TrackController : ITrackController
    {
        private readonly ITrackCircuit _circuit;
        private readonly ISimulationEnvironment _env;

        private Dictionary<int, ITrainModel> _trains;
        private Dictionary<int, List<IBlock>> _routes;
        private Dictionary<int, IBlock> _blocks;
        private Dictionary<int, IBlock> _updateBlocks;

        private List<string> _messages;
        private ITrackController _next;
        private Plc _plc;
        private ITrackController _prev;


        #region Constructor(s)

        /// <summary>
        ///     Constructs a new instance of the TrackController class
        /// </summary>
        /// <param name="env">A reference to the global environment object</param>
        /// <param name="circuit">A track circuit for the track controller</param>
        public TrackController(ISimulationEnvironment env, ITrackCircuit circuit)
        {
            _trains = new Dictionary<int, ITrainModel>();
            _blocks = new Dictionary<int, IBlock>();
            _routes = new Dictionary<int, List<IBlock>>();
            _updateBlocks = new Dictionary<int, IBlock>();

            _env = env;
            _env.Tick += EnvTick;

            ID = -1;
            SetId();

            _circuit = circuit;
            _circuit.ID = ID;

            // PROTOTYPE - static PLC
            _plc = new Plc(_env, _circuit);

            _messages = new List<string>();
        }

        #endregion // Constructor(s)

        #region Public Properties

        /// <summary>
        /// A list of messages for the TrackController to report
        /// </summary>
        public List<string> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        /// <summary>
        /// A request object this TrackController should process
        /// </summary>
        public IRequest Request
        {
            set { HandleRequest(value); }
        }

        public int ID { get; private set; }

        /// <summary>
        ///  The previous TrackController
        /// </summary>
        public ITrackController Previous
        {
            get { return _prev; }
            set
            {
                _prev = value;
                SetId();
            }
        }

        /// <summary>
        /// The next TrackController
        /// </summary>
        public ITrackController Next
        {
            get { return _next; }
            set { _next = value; }
        }

        /// <summary>
        /// Returns a list of Trains for this TrackController
        /// </summary>
        public List<ITrainModel> Trains
        {
            get { return _trains.Values.ToList(); }
        }

        /// <summary>
        /// Returns a list of Blocks for this TrackController
        /// </summary>
        public List<IBlock> Blocks
        {
            get { return _blocks.Values.ToList(); }
        }

        /// <summary>
        /// Returnes a list of Routes this TrackController interacts with
        /// </summary>
        public Dictionary<int, List<IBlock>> Routes
        {
            get { return _routes; }
        }

        #endregion // Public Properties

        #region Public Methods

        #endregion // Public Methods

        #region Private Methods

        // Sets the Id of the track controller based on its predecessor
        private void SetId()
        {
            if (_prev != null)
                ID = _prev.ID + 1;
            else
                ID = 0;
        }

        // Private method for handling the request object
        private void HandleRequest(IRequest request)
        {
            switch (request.RequestType)
            {
                case RequestTypes.AssignTrainRoute:
                    {
                        if (_routes.ContainsKey(request.TrainID))
                            _routes.Remove(request.TrainID);
                        _routes.Add(request.TrainID, request.Info.Blocks);
                    }
                    break;
                case RequestTypes.TrackControllerData:
                    {
                        if (request.TrackControllerID == ID)
                        {
                            if (request.Info != null)
                                request.Info.Trains = Trains;
                        }
                    }
                    break;
                case RequestTypes.TrackMaintenanceClose:
                    {
                        if (_blocks.Keys.Contains((request.Block.BlockID)))
                        {
                            IBlock b;
                            if (_blocks.TryGetValue(request.Block.BlockID, out b))
                            {
                                b.State = StateEnum.BlockClosed;
                                _updateBlocks.Add(b.BlockID, b);
                            }
                        }
                    }
                    break;
                case RequestTypes.TrackMaintenanceOpen:
                    {
                        if (_blocks.Keys.Contains((request.Block.BlockID)))
                        {
                            IBlock b;
                            if (_blocks.TryGetValue(request.Block.BlockID, out b))
                            {
                                if (b.State == StateEnum.BlockClosed)
                                    b.State = StateEnum.Healthy;

                                _updateBlocks.Add(b.BlockID, b);
                            }
                        }
                    }
                    break;
                case RequestTypes.SetTrainSpeed:
                    {
                        // Set the speed. The PLC will check if it is safe on a tick
                        if (_trains.Keys.Contains(request.TrainID))
                        {
                            // TrainAuthority also contains the speed, if RequestTypes is SetTrainSpeed
                            _circuit.ToTrain(request.TrainID, request.TrainAuthority, -1);
                        }
                    }
                    break;
                case RequestTypes.SetTrainAuthority:
                    {
                        // Set the authority. The PLC will check if it is safe on a tick
                        if (_trains.Keys.Contains(request.TrainID))
                        {
                            _circuit.ToTrain(request.TrainID, -1, request.TrainAuthority);
                        }
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
        private void PlcDoWork()
        {
            // Snapshot values. A new train entering the track
            // will not be processed by this track controller
            var sb = Blocks;
            var st = Trains;
            var sr = Routes;
            var up = _updateBlocks.Values.ToList();

            var proximityBlock = false;
            var proximityTrain = false;

            if (Next != null)
            {
                proximityBlock =
                    Next.Blocks.Any(
                        x =>
                        x.BlockID < Next.Blocks[0].BlockID + 3 && (x.State == StateEnum.BrokenTrackFailure) ||
                        (x.State == StateEnum.BlockClosed));
                proximityTrain = Next.Trains.Any(t => t.CurrentBlock.BlockID < Next.Blocks[0].BlockID + 3);
            }

            _plc.IsSafe(sb, st, sr, _messages, proximityTrain, proximityBlock);
            _plc.ToggleSwitches(sb, st, sr, _messages);
            _plc.ToggleLights(sb, st, sr, _messages);
            _plc.UpdateBlocks(up);
        }

        #endregion // Private Methods

        #region Events

        private static readonly Random Random = new Random((int)DateTime.Now.ToBinary());
        private const int Max = 1000;

        // A tick has elasped so we need to do work
        private void EnvTick(object sender, TickEventArgs e)
        {
            if (_next == null)
            {
                var trains = _circuit.Trains;

                var differences =
                    trains.Where(x => _circuit.Trains.All(x1 => x1.Key != x.Key))
                          .Union(_circuit.Trains.Where(x => trains.All(x1 => x1.Key != x.Key)));
                foreach (var k in differences)
                {
                    _env.AllTrains.Remove(k.Value);
                }
            }

            _trains = _circuit.Trains;
            _blocks = _circuit.Blocks;


            // Randomly create broken blocks
            if (Random.Next(Max) > Max * 0.999)
            {
                IBlock broken;
                if (_blocks.TryGetValue(Random.Next(_blocks.Count - 1), out broken))
                {
                    broken.State = StateEnum.BrokenTrackFailure;
                    _updateBlocks.Add(broken.BlockID, broken);
                }
            }

            PlcDoWork();
        }

        #endregion // Events
    }
}