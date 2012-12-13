# region Header

/*
 * Kent W. Nixon
 * Software Engineering
 * December 13, 2012
 */

# endregion

using System;
using System.Windows.Forms;
using CTCOffice;
using Interfaces;

namespace SystemScheduler
{
    //This is the class that comprises the largest amount of backend code for the scheduler
    public class SystemScheduler : ISystemScheduler
    {
        # region Private Variables

        //Store a reference to the CTC Office
        private readonly ICTCOffice _ctc;

        //The ever-present environment
        private readonly ISimulationEnvironment _env;

        //Our current count of time
        private DateTime _currentTime;

        //Have only one dispatch database open at a time
        private DispatchDatabase _dispatchDatabase;

        //Whether or not we are actually enabled
        private bool _enabled;

        # endregion

        #region Constructor(s)

        //The constructor for the system scheduler
        public SystemScheduler(ISimulationEnvironment env, ICTCOffice ctc)
        {
            //Store all of the incoming data to our globals
            _env = env;
            _ctc = ctc;

            //Get the current time
            _currentTime = DateTime.Now;
            _currentTime.AddMilliseconds(_currentTime.Millisecond * -1);

            //Subscribed to the environment tick event
            _env.Tick += _environment_Bollocks;

            //Also listen for when we are asked to start and stop
            _ctc.StartAutomation += _ctc_StartAutomation;
            _ctc.StopAutomation += _ctc_StopAutomation;
        }

        #endregion // Constructor(s)

        #region Public Properties

        //Accessor for the dispatch database
        public DispatchDatabase DispatchDatabase
        {
            get { return _dispatchDatabase; }
        }

        //Accessor for the enabled boolean
        public bool IsEnabled
        {
            get { return _enabled; }
        }

        //Accessor for the current time
        public DateTime SchedulerTime
        {
            get { return _currentTime; }
        }

        #endregion

        #region Public Methods

        //Method used to load a new database from an incoming filepath
        public void NewFile(string filename)
        {
            _dispatchDatabase = new DispatchDatabase(_env, filename);

            //If there were any problem, make our reference now null
            if (_dispatchDatabase.SuccessfulParse == false)
            {
                _dispatchDatabase = null;
            }
        }

        #endregion

        #region Private Methods

        //Method used to check if it is time to dispatch any trains
        private void CheckForDispatches(DateTime currentTime)
        {
            //For all of the dispatches in our database
            foreach (Dispatch singleDispatch in _dispatchDatabase.DispatchList)
            {
                //If the dispatch time matches the current time
                if ((singleDispatch.DispatchTime.Hour == currentTime.Hour) && (singleDispatch.DispatchTime.Minute == currentTime.Minute))
                {


                    //If the dispatch is for the red line
                    if (singleDispatch.Color.Equals("Red"))
                    {
                        //Pass a new request calling for the red line primary controller
                        _ctc.passRequest(new Request(RequestTypes.DispatchTrain, _env.PrimaryTrackControllerRed.ID,
                                                     -1, 10, 10, singleDispatch.DispatchRoute,
                                                     _env.TrackModel.requestBlockInfo(0, singleDispatch.Color)));
                    }

                    //If the dispatch is for the green line
                    else
                    {
                        //Pass a new request calling for the green line primary controller
                        _ctc.passRequest(new Request(RequestTypes.DispatchTrain, _env.PrimaryTrackControllerGreen.ID,
                                                     -1, 10, 10, singleDispatch.DispatchRoute,
                                                     _env.TrackModel.requestBlockInfo(0, singleDispatch.Color)));
                    }
                }
            }
        }

        #endregion

        #region Events

        //When we recieve a tick from the environment
        private void _environment_Bollocks(object sender, EventArgs e)
        {
            //Increment our time by 100 milliseconds
            _currentTime = _currentTime.AddMilliseconds(100);

            //If we are enabled
            if (_enabled)
            {
                //Check if this is a 15 minute mark
                if (((_currentTime.Minute % 15) == 0) && (_currentTime.Second == 0))
                {
                    //If so, check if we have to dispatch anything
                    CheckForDispatches(_currentTime);
                }
            }
        }

        //When we are told to shut off
        private void _ctc_StopAutomation(object sender, EventArgs e)
        {
            //Disable
            _enabled = false;
        }

        //When we are told to start up
        private void _ctc_StartAutomation(object sender, EventArgs e)
        {
            //If our dispatch database is not null
            if (_dispatchDatabase != null)
            {
                //Enable us
                _enabled = true;
            }

            //Otherwise
            else
            {

                //Tell the user to load a database into us before enabling
                MessageBox.Show("Please load a dispatch database file before\nenabling the automated System Scheduler.");
            }
        }

        #endregion
    }
}