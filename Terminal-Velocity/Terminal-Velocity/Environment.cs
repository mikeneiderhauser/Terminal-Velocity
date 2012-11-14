using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using Interfaces;
using Utility;

namespace TerminalVelocity
{
    public class Environment : IEnvironment
    {
        public event EventHandler<TickEventArgs> Tick;

        private ICTCOffice _CTCOffice;
        private ITrackController _primaryTC;

        private long _total;
        private long _interval = 100;
        private Timer _timer = new Timer();

        public Environment()
        {
            _timer.Interval = _interval;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        public ICTCOffice CTCOffice
        {
            get { return _CTCOffice; }
            set { _CTCOffice = value; }
        }

        public ITrackController PrimaryTrackController
        {
            get { return _primaryTC; }
            set { _primaryTC = value; }
        }

        /// <summary>
        /// The on tick event.  Fires every time a clock interval elapses.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTick(TickEventArgs e)
        {
            if (Tick != null)
            {
                Tick(this, e);
            }
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _total += _interval;
            this.OnTick(new TickEventArgs(_total));
        }
    }
}
