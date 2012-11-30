using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace TrackController
{
    public class TrackCircuit : ITrackCircuit
    {
        private ISimulationEnvironment _env;
        private ITrackController _trackController;

        private int _id;

        private Dictionary<int, ITrainModel> _trains;
        private Dictionary<int, IBlock> _blocks;

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

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Dictionary<int, ITrainModel> Trains
        {
            get { return _trains; }
        }

        public Dictionary<int, IBlock> Blocks
        {
            get { return _blocks; }
        }

        public ITrackController TrackController
        {
            get { return _trackController; }
            set { _trackController = value; }
        }

        #endregion // Public Properties

        #region Public Methods

        public void ToTrain(int ID, double speedLimit = Double.NaN, int authority = Int32.MinValue)
        {
            Dictionary<int, ITrainModel> snapshot = Trains;

            ITrainModel train;
            if (snapshot.TryGetValue(ID, out train))
            {
                if (speedLimit != Double.NaN)
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
                if (_blocks.ContainsKey(t.CurrentBlock.BlockID))
                    _trains.Add(t.TrainID, t);
            }
        }

        #endregion // Events
    }
}
