using System;
using System.Collections.Generic;
using Interfaces;
using Utility;

namespace TrackController
{
    public class TrackCircuit : ITrackCircuit
    {
        private readonly Dictionary<int, IBlock> _blocks;
        private readonly ISimulationEnvironment _env;

        private readonly Dictionary<int, ITrainModel> _trains;

        #region Constructor(s)

        public TrackCircuit(ISimulationEnvironment env, List<IBlock> blocks)
        {
            _trains = new Dictionary<int, ITrainModel>();
            _blocks = new Dictionary<int, IBlock>();

            foreach (var b in blocks)
                _blocks.Add(b.BlockID, b);

            _env = env;
            _env.Tick += EnvTick;
        }

        #endregion // Constructor(s)

        #region Public Properties

        public ITrackController TrackController { get; set; }
        public int ID { get; set; }

        public Dictionary<int, ITrainModel> Trains
        {
            get { return _trains; }
        }

        public Dictionary<int, IBlock> Blocks
        {
            get { return _blocks; }
        }

        #endregion // Public Properties

        #region Public Methods

        public void ToTrain(int id, double speedLimit = Double.NaN, int authority = Int32.MinValue)
        {
            var snapshot = Trains;

            ITrainModel train;
            if (!snapshot.TryGetValue(id, out train)) 
                throw new Exception(string.Format("Targeted train ID:{0} was expected in TrackCircuit ID:{1} but was not found.", id, this.ID));

            if (!double.IsNaN(speedLimit))
            {
                train.TrainController.SpeedLimit = speedLimit;
            }

            if (authority != Int32.MinValue)
            {
                train.TrainController.AuthorityLimit = authority;
            }
        }

        #endregion // Public Methods

        #region Events

        private void EnvTick(object sender, TickEventArgs e)
        {
            _trains.Clear();
            
            // Find all the trains within the TrackCircuits area
            foreach (var t in _env.AllTrains)
            {
                if (!_trains.ContainsKey(t.TrainID) &&
                    _blocks.ContainsKey(t.CurrentBlock.BlockID))
                    _trains.Add(t.TrainID, t);
            }
        }

        #endregion // Events
    }
}