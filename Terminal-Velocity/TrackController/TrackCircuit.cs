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

            foreach (IBlock b in blocks)
                _blocks.Add(b.BlockID, b);

            _env = env;
            _env.Tick += _env_Tick;
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
            Dictionary<int, ITrainModel> snapshot = Trains;

            ITrainModel train;
            if (snapshot.TryGetValue(id, out train))
            {
                if (!double.IsNaN(speedLimit))
                {
                    train.TrainController.SpeedLimit = speedLimit;
                }

                if (authority != Int32.MinValue)
                {
                    train.TrainController.AuthorityLimit = authority;
                }
            }
        }

        #endregion // Public Methods

        #region Events

        private void _env_Tick(object sender, TickEventArgs e)
        {
            _trains.Clear();
            foreach (ITrainModel t in _env.AllTrains)
            {
                if (!_trains.ContainsKey(t.TrainID) &&
                    _blocks.ContainsKey(t.CurrentBlock.BlockID))
                    _trains.Add(t.TrainID, t);
            }
        }

        #endregion // Events
    }
}