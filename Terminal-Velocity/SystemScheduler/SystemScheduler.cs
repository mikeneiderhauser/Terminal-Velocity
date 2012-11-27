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
        private ISimulationEnvironment _env;
        private ICTCOffice _ctc;
        private IDispatchDatabase _dispatchDatabase;

        #region Constructor(s)

        public SystemScheduler(ISimulationEnvironment env, ICTCOffice ctc)
        {
            _env = env;
            _env.Tick += _env_Tick;

            _ctc = ctc;
    
        }

        #endregion // Constructor(s)

        #region Public Properties
        public IDispatchDatabase DispatchDatabase
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

        #endregion // Private Methods

        #region Events

        void _env_Tick(object sender, TickEventArgs e)
        {
            //parse dispatches and sned any requests
        }

        #endregion // Events

    }
}
