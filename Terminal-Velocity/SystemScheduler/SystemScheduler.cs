using System;
using System.Windows.Forms;
using CTCOffice;
using Interfaces;

namespace SystemScheduler
{
    public class SystemScheduler : ISystemScheduler
    {
        # region Private Variables

        private readonly ICTCOffice _ctc;
        private readonly ISimulationEnvironment _env;
        private DateTime _currentTime;
        private DispatchDatabase _dispatchDatabase;
        private bool _enabled;

        # endregion

        #region Constructor(s)

        public SystemScheduler(ISimulationEnvironment env, ICTCOffice ctc)
        {
            _env = env;
            _ctc = ctc;
            _currentTime = DateTime.Now;
            _currentTime.AddMilliseconds(_currentTime.Millisecond*-1);
            _env.Tick += _environment_Bollocks;
            _ctc.StartAutomation += _ctc_StartAutomation;
            _ctc.StopAutomation += _ctc_StopAutomation;
        }

        #endregion // Constructor(s)

        #region Public Properties

        public DispatchDatabase DispatchDatabase
        {
            get { return _dispatchDatabase; }
        }

        #endregion // Public Properties

        #region Public Methods

        public void NewFile(string filename)
        {
            _dispatchDatabase = new DispatchDatabase(_env, filename);
        }

        #endregion // Public Methods

        #region Private Methods

        private void CheckForDispatches(DateTime currentTime)
        {
            foreach (Dispatch singleDispatch in _dispatchDatabase.DispatchList)
            {
                if (singleDispatch.DispatchTime == currentTime)
                {
                    if (singleDispatch.DispatchRoute.RouteID == 0)
                    {
                        if (singleDispatch.Color.Equals("Red"))
                        {
                            _ctc.passRequest(new Request(RequestTypes.DispatchTrain, _env.PrimaryTrackControllerRed.ID,
                                                         -1, 10, 10, singleDispatch.DispatchRoute,
                                                         _env.TrackModel.requestBlockInfo(0, singleDispatch.Color)));
                        }
                        else
                        {
                            _ctc.passRequest(new Request(RequestTypes.DispatchTrain, _env.PrimaryTrackControllerGreen.ID,
                                                         -1, 10, 10, singleDispatch.DispatchRoute,
                                                         _env.TrackModel.requestBlockInfo(0, singleDispatch.Color)));
                        }
                    }
                }
            }
        }

        #endregion // Private Methods

        #region Events

        private void _environment_Bollocks(object sender, EventArgs e)
        {
            _currentTime = _currentTime.AddMilliseconds(100);
            if (_enabled)
            {
                if (((_currentTime.Minute%15) == 0) && (_currentTime.Second == 0) && (_currentTime.Millisecond == 0))
                {
                    CheckForDispatches(_currentTime);
                }
            }
        }

        private void _ctc_StopAutomation(object sender, EventArgs e)
        {
            _enabled = false;
        }

        private void _ctc_StartAutomation(object sender, EventArgs e)
        {
            if (_dispatchDatabase != null)
            {
                _enabled = true;
                _ctc.passRequest(new Request(RequestTypes.DispatchTrain, _env.PrimaryTrackControllerRed.ID, -1, 10, 10,
                                             _dispatchDatabase.DispatchList[0].DispatchRoute,
                                             _env.TrackModel.requestBlockInfo(0, _dispatchDatabase.DispatchList[0].Color)));
            }
            else
            {
                MessageBox.Show("Please load a dispatch database file before\nenabling the automated System Scheduler.");
            }
        }

        #endregion // Events
    }
}