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
        public EventHandler<TrainDetectedEventArgs> TrainDetected;

        private IEnvironment _env;

        #region Constructor(s)

        public TrackCircuit(IEnvironment env)
        {
            _env = env;
            _env.Tick += _env_Tick;
        }

        #endregion // Constructor(s)

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
            // Do Work to see if a train has been detected.
            // if (train)
                // OnTrainDectected( train ID)
        }

        #endregion // Events
    }
}
