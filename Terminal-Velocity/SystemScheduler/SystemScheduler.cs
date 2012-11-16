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
        private IEnvironment _env;
        private ICTCOffice _ctc;

        #region Constructor(s)

        public SystemScheduler(IEnvironment env, ICTCOffice ctc)
        {
            _env = env;
            _env.Tick += _env_Tick;

            _ctc = ctc;
    
        }

        #endregion // Constructor(s)

        #region Public Properties

        #endregion // Public Properties

        #region Public Methods

        #endregion // Public Methods

        #region Private Methods

        #endregion // Private Methods

        #region Events

        void _env_Tick(object sender, TickEventArgs e)
        {
            //parse dispatches and sned any requests
        }

        #endregion // Events


        public IRequest GetRoute
        {
            get { throw new NotImplementedException(); }
        }
    }
}
