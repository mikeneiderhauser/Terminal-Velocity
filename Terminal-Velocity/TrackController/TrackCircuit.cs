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
        private Dictionary<int, ITrain> _trains;

        #region Constructor(s)

        public TrackCircuit(ISimulationEnvironment env)
        {
            _trains = new Dictionary<int, ITrain>();

            _env = env;
            _env.Tick += _env_Tick;
        }

        #endregion // Constructor(s)

        #region Public Properties

        public Dictionary<int, ITrain> Trains
        {
            get { return _trains; }
        }

        public ITrackController TrackController
        {
            get { return _trackController; }
            set { _trackController = value; }
        }

        #endregion // Public Properties

        #region Public Methods

        public void ToTrackController(object data)
        {
            _trackController.Recieve(data);
        }

        public void ToTrain(int ID, int speedLimit = -1, int authority = -1)
        {
            ITrain train;
            if (Trains.TryGetValue(ID, out train))
            {
                if (speedLimit >= 0)
                {
                    // set speed
                }

                if (authority >= 0)
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

            int trainID = 0;
            //ITrain train = null;
            //_trains.Add(trainID, train);
        }

        #endregion // Events
    }
}
