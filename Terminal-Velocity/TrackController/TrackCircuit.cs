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

        public TrackCircuit(ISimulationEnvironment env)
        {
            _trains = new Dictionary<int, ITrainModel>();
            _blocks = new Dictionary<int, IBlock>();

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

            ITrainModel train;
            if (Trains.TryGetValue(ID, out train))
            {
                if (speedLimit != Double.NaN)
                {
                    // set speed
                }

                if (authority != Int32.MinValue)
                {
                    // set authority
                }
            }
        }

        #endregion // Public Methods

        #region Events

        private void _env_Tick(object sender, TickEventArgs e)
        {
            // foreach train in environment,
            // if train is in area of control, add train

            //ITrain train = null;
            //_trains.Add(trainID, train);
        }

        #endregion // Events
    }
}
