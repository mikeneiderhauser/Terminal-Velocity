using System;
using System.Collections.Generic;
using System.Timers;
using Interfaces;
using Utility;

namespace SimulationEnvironment
{
    public class SimulationEnvironment : ISimulationEnvironment
    {
        private const long Interval = 250;

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
            _sysLog = new SystemLog();
        }

        #endregion

        public void Start()
        {
            _timer.Start();
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
            get { return _allTrains; }
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
                Tick(this, e);
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _total += Interval;
            OnTick(new TickEventArgs(_total));
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
            _timer.Interval = interval;
        }

        public long getInterval()
        {
            return (long) _timer.Interval;
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
                this.addTrain(new TrainModel.Train(randomNumber, start, this));
            }
        }

        #endregion
    }
}