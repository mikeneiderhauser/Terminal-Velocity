using System;
using System.Collections.Generic;
using System.Timers;
using Interfaces;
using Utility;
using Timer = System.Timers.Timer;

namespace SimulationEnvironment
{
    public class SimulationEnvironment : ISimulationEnvironment
    {
        private const long Interval = 1000;

        #region Private Variables

        private readonly List<ITrainModel> _allTrains;
        private readonly SystemLog _sysLog;

        private readonly Timer _timer = new Timer();
        private ICTCOffice _CTCOffice;
        private long _total;

        #endregion

        #region Constructor

        public SimulationEnvironment()
        {
            _timer.Interval = Interval;
            _timer.Elapsed += _timer_Elapsed;

            _allTrains = new List<ITrainModel>();
            _sysLog = new SystemLog(this);
        }

        #endregion

        /// <summary>
        /// Allows explicitly starting the Tick event
        /// Thhis is only available in debug builds and is suitable for testing
        /// </summary>
        public void Start()
        {
            #if (DEBUG)
            if (!_timer.Enabled)
                _timer.Start();
            #endif
        }

        /// <summary>
        /// Allows explicitly stopping the Tick event
        /// This is only available in debug builds and is suitable for testing
        /// </summary>
        public void Stop()
        {
            #if (DEBUG)
            if (_timer.Enabled)
                _timer.Stop();
            #endif
        }

        #region Public Property

        public ICTCOffice CTCOffice
        {
            get { return _CTCOffice; }
            set { _CTCOffice = value; }
        }

        public ISystemScheduler SystemScheduler { get; set; }

        public ITrackController PrimaryTrackControllerRed { get; set; }

        public ITrackController PrimaryTrackControllerGreen { get; set; }

        public ITrackModel TrackModel { get; set; }


        public List<ITrainModel> AllTrains
        {
            get
            {
                lock (_allTrains)
                {
                    return _allTrains;
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Tick For Interface
        /// </summary>
        public event EventHandler<TickEventArgs> Tick;

        /// <summary>
        ///     The on tick event.  Fires every time a clock interval elapses.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTick(TickEventArgs e)
        {
            if (Tick != null)
            {
                Stop();
                Tick(this, e);
                Start();
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _total += Interval;
            OnTick(new TickEventArgs(_total));
        }

        #endregion

        #region Functions

        public void AddTrain(ITrainModel train)
        {
            _allTrains.Add(train);
        }

        public void RemoveTrain(ITrainModel train)
        {
            _allTrains.Remove(train);
        }

        public void SendLogEntry(string msg)
        {
            if (_sysLog != null)
            {
                _sysLog.writeLog(msg);
            }
        }

        public void SetInterval(long interval)
        {
            _timer.Interval = interval;
        }

        public long GetInterval()
        {
            return (long) _timer.Interval;
        }

        public void StopTick()
        {
            _timer.Stop();
            SendLogEntry("Environment: Envoked Timer Stop");
        }

        public void StartTick()
        {
            _timer.Start();
            SendLogEntry("Environment: Envoked Timer Start");
        }

        public void Dispatch(IRequest request)
        {
            var random = new Random();
            int randomNumber = 0;
            bool uniqueID = true; //unique until invalidated

            do
            {
                randomNumber = random.Next(0, 500); // well over total number of blocks.. 1 train per block
                foreach (ITrainModel t in _allTrains)
                {
                    if (t.TrainID == randomNumber)
                    {
                        uniqueID = false;
                    }
                }
            } while (!uniqueID);

            
            IBlock start = this.TrackModel.requestBlockInfo(0, request.Block.Line);
            //detect collision on dispatch
            if ((PrimaryTrackControllerRed.Trains.Count == 0 && start.Line.CompareTo("Red") == 0) || (PrimaryTrackControllerGreen.Trains.Count == 0 && start.Line.CompareTo("Green") == 0))
            {
                this.AddTrain(new TrainModel.Train(randomNumber, start, this));
                _CTCOffice.ExternalRefresh();
            }
        }

        #endregion
    }
}