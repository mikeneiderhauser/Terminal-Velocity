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
        public event EventHandler<TrainDetectedEventArgs> TrainDetected;

        private IEnvironment _env;
        private ITrackController _trackController;
        private List<ITrain> _trains;

        #region Constructor(s)

        public TrackCircuit(IEnvironment env)
        {
            _trains = new List<ITrain>();

            _env = env;
            _env.Tick += _env_Tick;
        }

        #endregion // Constructor(s)

        #region Public Properties

        public ITrackController TrackController
        {
            get { return _trackController; }
            set { _trackController = value; }
        }

        #endregion // Public Properties

        #region Public Methods

        public void ToTrackController(ITrain train)
        {
            _trackController.Recieve(train);
        }

        public void ToTrain(int ID)
        {
            foreach (ITrain t in _trains)
            {
                // if (t.ID == _ID
                // return t;
            }
        }

        #endregion // Public Methods

        #region Events

        protected virtual void OnTrainDetected(TrainDetectedEventArgs e)
        {
            if (TrainDetected != null)
            {
                TrainDetected(this, e);
            }
        }

        private void _env_Tick(object sender, TickEventArgs e)
        {
            // foreach train in environment,
            // if train is in area of control, add train
        }

        #endregion // Events
    }
}
