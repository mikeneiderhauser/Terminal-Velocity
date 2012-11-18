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
        #region Private Variables
        private ICTCOffice _CTCOffice;
        private ISystemScheduler _systemScheduler;
        private ITrackController _primaryTCRed;
        private ITrackController _primaryTCGreen;
        private ITrackModel _trackModel;
        private List<ITrainModel> _allTrains;
        private SystemLog _sysLog;
        
        private long _total;
        private long _interval = 100;
        private Timer _timer = new Timer();
        #endregion

        #region Constructor
        public Environment()
        {
            _timer.Interval = _interval;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();

            _allTrains = new List<ITrainModel>();
            _sysLog = new SystemLog();
        }
        #endregion

        #region Public Property
        public ICTCOffice CTCOffice
        {
            get { return _CTCOffice; }
            set { _CTCOffice = value; }
        }

        public ISystemScheduler SystemScheduler
        {
            get { return _systemScheduler; }
            set { _systemScheduler = value; }
        }

        public ITrackController PrimaryTrackControllerRed
        {
            get { return _primaryTCRed; }
            set { _primaryTCRed = value; }
        }

        public ITrackController PrimaryTrackControllerGreen
        {
            get { return _primaryTCGreen; }
            set { _primaryTCGreen = value; }
        }

        public ITrackModel TrackModel
        {
            get { return _trackModel; }
            set { _trackModel = value; }
        }


        public List<ITrainModel> AllTrains
        {
            get { return _allTrains; }
        }
        #endregion

        #region Events

        /// <summary>
        /// Tick For Interface
        /// </summary>
        public event EventHandler<TickEventArgs> Tick;

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
        #endregion

        #region Functions
        public void addTrain(ITrainModel train)
        {
            _allTrains.Add(train);
        }

        public void removeTrain(ITrainModel train)
        {
            _allTrains.Remove(train);
        }

        public void sendLogEntry(string msg)
        {
            if (_sysLog != null)
            {
                _sysLog.writeLog(msg);
            }
        }

        public void setInterval(long interval)
        {
            _timer.Interval = (double)interval;
        }

        public long getInterval()
        {
            return (long)_timer.Interval;
        }

        public void stopTick(object sender)
        {
            if (sender == _CTCOffice)
            {
                _timer.Stop();
                sendLogEntry("Environment: Envoked Timer Stop");
            }
            else
            {
                sendLogEntry("Environment: Attempted Envoke of stopTimer -> Caller not CTC Office: DENIED");
            }
        }

        public void startTick(object sender)
        {
            if (sender == CTCOffice)
            {
                _timer.Start();
                sendLogEntry("Environment: Envoked Timer Start");
            }
            else
            {
                sendLogEntry("Environment: Attempted Envoke of stopTimer -> Caller not CTC Office: DENIED");
            }
        }
        #endregion
    }
}
