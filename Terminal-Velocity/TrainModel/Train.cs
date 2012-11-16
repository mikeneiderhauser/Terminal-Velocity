using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using Utility;

namespace TrainModel
{
    public class Train : ITrainModel
    {
        // Has public parameters
        private int _trainID;
        private const double _length = 32.33; //meters
        private double _totalMass;
        private string _informationLog;
        private bool _lightsOn;
        private bool _doorsOpen;
        private int _temperature;
        private double _currentAcceleration;
        private double _currentVelocity;
        private double _currentPosition;
        
        private const int _maxCapacity = 222;
        private int _numPassengers;
        private int _numCrew;
        
        private bool _brakeFailure;
        private bool _engineFailure;
        private bool _signalPickupFailure;
        
        private ITrainController _trainController;

       
        // Does not have public parameters
        private const double _initialMass = 40900; // kilograms
        private const double _personMass = 200; // kilograms
        private const double _width = 2.65; // meters
        private const double _height = 3.42; // meters
        private const double _physicalAccelerationLimit = 0.5; // meters/second^2
        private const double _physicalDecelerationLimit = -1.2; // meters/second^2
        private const double _physicalVelocityLimit = 70000; // meters/hour
        private const double _emergencyBrakeDeceleration = -2.73; // meters/second^2
        
        private IEnvironment _environment;

        public Train(int trainID, int temperature, IEnvironment environment)
        {
            _trainID = trainID;
            _totalMass = calculateMass();
            _informationLog = "Created at " + DateTime.Now + ".\n";
            _lightsOn = false;
            _doorsOpen = false;
            _temperature = temperature;

            _currentAcceleration = 0;
            _currentVelocity = 0;
            _currentPosition = 0;

            _numPassengers = 0;
            _numCrew = 0;

            _brakeFailure = false;
            _engineFailure = false;
            _signalPickupFailure = false;

            _environment = environment;

            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);
        }

        void  _environment_Tick(object sender, TickEventArgs e)
        {
 	        //handle tick here
        }

        //TODO
        public void EmergencyBrake()
        {
        }

        //TODO
        public bool ChangeMovement(double power)
        {
            return true;
        }

        // private functions
        private double calculateMass()
        {
            return (_initialMass + _personMass*(_numPassengers + _numCrew));
        }

        //TODO
        private void updateMovement()
        {

        }

        //TODO
        private bool checkFailures()
        {
            return false;
        }

        #region Properties
        public int TrainID
        {
            get { return _trainID; }
        }

        public double Length
        {
            get { return _length; }
        }

        public double TotalMass
        {
            get { return _totalMass; }
        }

        public string InformationLog
        {
            get { return _informationLog; }
        }

        public bool LightsOn
        {
            get { return _lightsOn; }
            set { _lightsOn = value; }
        }

        public bool DoorsOpen
        {
            get { return _doorsOpen; }
            set { _doorsOpen = value; }
        }

        public int Temperature
        {
            get { return _temperature; }
            set { _temperature = value; }
        }

        public double CurrentAcceleration
        {
            get { return _currentAcceleration; }
        }

        public double CurrentVelocity
        {
            get { return _currentVelocity; }
        }

        public double CurrentPosition
        {
            get { return _currentPosition; }
        }

        public int MaxCapacity
        {
            get { return _maxCapacity; }
        }

        public int NumPassengers
        {
            get { return _numPassengers; }
            set 
            { 
                _numPassengers = value;
                _totalMass = calculateMass();
            }
        }

        public int NumCrew
        {
            get { return _numCrew; }
            set 
            { 
                _numCrew = value;
                _totalMass = calculateMass();
            }
        }

        // TODO
        public bool BrakeFailure
        {
            get { return _brakeFailure; }
        }

        public bool EngineFailure
        {
            get { return _engineFailure; }
        }

        public bool SignalPickupFailure
        {
            get { return _signalPickupFailure; }
        }
        
        #endregion


        int ITrainModel.Length
        {
            get { throw new NotImplementedException(); }
        }
    }
}
