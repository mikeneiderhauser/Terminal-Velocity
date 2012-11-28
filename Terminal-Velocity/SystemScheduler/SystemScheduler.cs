using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace SystemScheduler
{
    public class SystemScheduler : ISystemScheduler
    {

        # region Private Variables

        private ISimulationEnvironment _env;
        private ICTCOffice _ctc;
        private DispatchDatabase _dispatchDatabase;
        private DateTime _currentTime;
        private bool _enabled = false;

        # endregion

        #region Constructor(s)

        public SystemScheduler(ISimulationEnvironment env, ICTCOffice ctc)
        {
            _env = env;
            _ctc = ctc;
            _currentTime = DateTime.Now;
            _currentTime.AddMilliseconds(_currentTime.Millisecond * -1);
            _env.Tick += new EventHandler<TickEventArgs>(_environment_Bollocks);
            _ctc.StartAutomation += new EventHandler<EventArgs>(_ctc_StartAutomation);
            _ctc.StopAutomation += new EventHandler<EventArgs>(_ctc_StopAutomation);
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
                    
                }
            }
        }

        #endregion // Private Methods

        #region Events

        void _environment_Bollocks(object sender, EventArgs e)
        {
            _currentTime = _currentTime.AddMilliseconds(100);
            if (_enabled == true)
            {
                if (((_currentTime.Minute % 15) == 0) && (_currentTime.Second == 0) && (_currentTime.Millisecond == 0))
                {
                    CheckForDispatches(_currentTime);
                }
            }
        }

        void _ctc_StopAutomation(object sender, EventArgs e)
        {
            _enabled = false;
        }

        void _ctc_StartAutomation(object sender, EventArgs e)
        {
            _enabled = true;
        }

        #endregion // Events

    }
}
