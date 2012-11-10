using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace TrackController
{
    public class TrackController
    {
        private PLC _plc;
        private IEnvironment _env;

        private ITrackController _prev;
        private ITrackController _next;

        private List<IBlock> _blocks;
        private List<ITrain> _trains;
        private List<IRoute> _routes;

        private int _ID;

        #region Constructor(s)

        public TrackController(IEnvironment env, ITrackController prev, ITrackController next)
        {
            _env = env;
            _env.Tick += _env_Tick;

            _prev = prev;
            _next = next;

            if (_prev != null)
                _ID = _prev.ID;
            else
                _ID = 0;
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
            bool safe = PLC_IsSafe();
            bool lights = PLC_LightsRequired();
        }

        #endregion // Events

        internal class PLC
        {
            public PLC(string filename)
            {
            }

            public bool IsSafe(List<IBlock> blocks, List<ITrain> trains, List<IRoute> routes)
            {
                return false;
            }

            public bool LightsRequired(List<IBlock> blocks, List<ITrain> trains, List<IRoute> routes)
            {
                return false;
            }
        }
    }
}
