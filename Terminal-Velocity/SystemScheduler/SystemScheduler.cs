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

        public IRequest Request
        {
            set { HandleRequest(value); }
        }

        public int ID
        {
            get { return _ID; }
        }

        public ITrackController Previous
        {
            get { return _prev; }
        }

        public ITrackController Next
        {
            get { return _next; }
        }

        public List<ITrain> Trains
        {
            get { return _trains; }
        }

        public List<IBlock> Blocks
        {
            get { return _blocks; }
        }

        public List<IRoute> Routes
        {
            get { return _routes; }
        }

        #endregion // Public Properties

        #region Public Methods

        public void LoadPLCProgram(string filename)
        {
            _plc = new PLC(filename);
        }

        #endregion // Public Methods

        #region Private Methods

        private void HandleRequest(IRequest request)
        {
            // if request.ID = this.ID
            //else

            if (Next != null)
                Next.Request = request;
            //else
            // Environment.CTCOffice.Give(Request) 
        }

        private bool PLC_IsSafe()
        {
            return _plc.IsSafe(Blocks, Trains, Routes);
        }

        private bool PLC_LightsRequired()
        {
            return _plc.LightsRequired(Blocks, Trains, Routes);
        }

        #endregion // Private Methods

        #region Events

        void _env_Tick(object sender, TickEventArgs e)
        {
            //parse dispatches and sned any requests
        }

        #endregion // Events

    }
}
