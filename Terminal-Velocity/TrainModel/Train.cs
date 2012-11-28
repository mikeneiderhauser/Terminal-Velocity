using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace TrainModel
{
    public class Train : ITrainModel
    {
        #region Global variables

        private int _trainID;
        private const double _length = 32.33; // meters
        private double _totalMass;
        private string _informationLog;
        private bool _lightsOn;
        private bool _doorsOpen;
        private int _temperature;
        private double _currentAcceleration;
        private double _currentVelocity;
        private double _currentPosition;

        private const int _maxCapacity = 222; // 74 seated, 148 standing
        private int _numPassengers;
        private int _numCrew;

        private bool _brakeFailure;
        private bool _engineFailure;
        private bool _signalPickupFailure;

        private ITrainController _trainController;
        private ISimulationEnvironment _environment;
        private ITrackModel _trackModel;
        private List<ITrainModel> allTrains;

        private int _trackCircuitID;
        private IBlock _currentBlock;
        private int _currentBlockID;
        private int _previousBlockID;
        private double _blockLength;

        #endregion



        #region Constant values

        private const double _initialMass = 40900; // kilograms
        private const double _personMass = 90; // kilograms
        private const double _width = 2.65; // meters
        private const double _height = 3.42; // meters
        private const double _physicalAccelerationLimit = 0.5; // meters/second^2
        private const double _physicalDecelerationLimit = -1.2; // meters/second^2
        private const double _physicalVelocityLimit = 70000; // meters/hour
        private const double _emergencyBrakeDeceleration = -2.73; // meters/second^2
        private const double _accelerationGravity = 9.8; // meters/second^2

        #endregion



        #region Constructors

        /// <summary>
        /// This constructor is used when passenger, crew, and temperature information is not given.
        /// It adds no passengers or crew and sets the temperature equal to 32 degrees Celcius.
        /// </summary>
        public Train(int trainID, IBlock startingBlock, ISimulationEnvironment environment)
        {
            _trainID = trainID;
            _totalMass = calculateMass();
            _informationLog = "Created at " + DateTime.Now + ".\n";
            _lightsOn = false;
            _doorsOpen = false;
            _temperature = 32;

            _currentAcceleration = 0;
            _currentVelocity = 0;
            _currentPosition = 0;

            _numPassengers = 0;
            _numCrew = 0;

            _brakeFailure = false;
            _engineFailure = false;
            _signalPickupFailure = false;

            _currentBlock = startingBlock;
            _previousBlockID = 0;
            _currentBlockID = _currentBlock.BlockID;
            _blockLength = _currentBlock.BlockSize;
            _trackCircuitID = _currentBlock.TrackCirID;

            _environment = environment;
            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);

            _trackModel = environment.TrackModel;
            _trainController = new TrainController.TrainController(_environment);

            // set allTrains equal to list contained in environment
            allTrains = environment.AllTrains;
        }

        #endregion



        #region Public Methods


        /// <summary>
        /// Applies the maximum deceleration limit to the train to stop it.
        /// </summary>
        public void EmergencyBrake()
        {
            _informationLog += "Train " + _trainID + "'s emergency brake pulled!\n";
            _currentAcceleration = _emergencyBrakeDeceleration;
        }

        /// <summary>
        /// Changes the acceleration of the train based on the given power. 
        /// </summary>
        /// <param name="power">Power given.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool ChangeMovement(double power)
        {
            _informationLog += "Train " + _trainID + " given power of " + power + " kW.\n";

            double currentForce = 0;

            if (_currentVelocity > 0 || _currentVelocity < 0)
            {
                currentForce = power / _currentVelocity;
            }

            double newAcceleration = currentForce / _totalMass;

            // check that the new acceleration does not exceed the physical limit
            if (newAcceleration > 0 && newAcceleration > _physicalAccelerationLimit)
            {
                _informationLog += "Train " + _trainID + "'s power level exceeded physical acceleration limit.\n";
                return false;
            }

            // check that the new deceleration does not exceed the physical limit
            else if (newAcceleration < 0 && newAcceleration < _physicalDecelerationLimit)
            {
                _informationLog += "Train " + _trainID + "'s power level exceeded physical deceleration limit.\n";
                return false;
            }

            _informationLog += "Train " + _trainID + " acceleration set to " + newAcceleration + " m/s^2.\n";
            _currentAcceleration = newAcceleration;
            return true;
        }

        /// <summary>
        /// This overrides the regular ToString() method for a Train.
        /// </summary>
        /// <returns>Returns "Train " + trainID</returns>
        public override string ToString()
        {
            return "Train " + _trainID;
        }

        #endregion



        #region Private Methods

        /// <summary>
        /// Occurs on every tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _environment_Tick(object sender, TickEventArgs e)
        {
            updateMovement();
        }

        /// <summary>
        /// Updates the movement of the train. Accounts for slope and changes the block if necessary.
        /// </summary>
        private void updateMovement()
        {
            // acceleration changes due to elevation
            double angle = Math.Acos(Math.Abs(_currentBlock.Grade));
            if (_currentBlock.Grade > 0) // up hill
            {
                _currentAcceleration -= _accelerationGravity * Math.Sin(angle);
            }
            else if (_currentBlock.Grade < 0) // down hill
            {
                _currentAcceleration += _accelerationGravity * Math.Sin(angle);
            }

            _currentVelocity += _currentAcceleration;

            if (_currentVelocity < 0)
            {
                _currentVelocity = 0;
            }

            _currentPosition += _currentVelocity;

            // Handles edge of block, only going forward
            if (_currentPosition >= _blockLength)
            {
                // get next block ID based on the previous ID
                int nextBlockID = _currentBlock.nextBlockIndex(_previousBlockID);

                _previousBlockID = _currentBlockID; // previous block is now current block

                // update the current block to be the next block
                _currentBlock = _trackModel.requestBlockInfo(nextBlockID, _currentBlock.Line);
                _currentBlockID = _currentBlock.BlockID;
                
                // update the current position of the train
                _currentPosition = _currentPosition - _blockLength;
                _blockLength = _currentBlock.BlockSize;
            }


            // TODO: added implementation
            if (_trackCircuitID != _currentBlock.TrackCirID)
            {
                _trackCircuitID = _currentBlock.TrackCirID;
            }
        }

        /// <summary>
        /// Calculates the mass of the train based on the number of passengers.
        /// </summary>
        /// <returns>The total mass.</returns>
        private double calculateMass()
        {
            return (_initialMass + _personMass * (_numPassengers + _numCrew));
        }

        #endregion



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
            set
            {
                _lightsOn = value;
                _informationLog += "Train " + _trainID;

                if (_lightsOn)
                    _informationLog += " lights were turned on.\n";
                else
                    _informationLog += " lights were turned off.\n";
            }
        }

        public bool DoorsOpen
        {
            get { return _doorsOpen; }
            set
            {
                _doorsOpen = value;
                _informationLog += "Train " + _trainID;

                if (_doorsOpen)
                    _informationLog += " doors were opened.\n";
                else
                    _informationLog += " doors were closed.\n";
            }
        }

        public int Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                _informationLog += "Train " + _trainID + " temperature was set to " + _temperature;
            }
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
                int oldNumPassengers = _numPassengers;
                _numPassengers = value;
                int difference = _numPassengers - oldNumPassengers;

                if (difference < 0) // people get off train
                {
                    difference *= -1;
                    _informationLog += difference + " passengers got off of Train " + _trainID + ".\n";
                }
                else // people get on train
                    _informationLog += difference + " passengers got on Train " + _trainID + ".\n";

                _totalMass = calculateMass();
            }
        }

        public int NumCrew
        {
            get { return _numCrew; }
            set
            {
                int oldNumCrew = _numCrew;
                _numCrew = value;
                int difference = _numCrew - oldNumCrew;

                if (difference < 0) // people get off train
                {
                    difference *= -1;
                    _informationLog += difference + " crew members got off of Train " + _trainID + ".\n";
                }
                else // people get on train
                    _informationLog += difference + " crew members got on Train " + _trainID + ".\n";

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

        public IBlock CurrentBlock
        {
            get { return _currentBlock; }
        }

        // TODO: double check that it works
        // for track controller communications

        #region Track Controller communication parameters

        public double SpeedLimit
        {
            get { return _trainController.SpeedLimit; }
            set { _trainController.SpeedLimit = value; }
        }

        public int AuthorityLimit
        {
            get { return _trainController.AuthorityLimit; }
            set { _trainController.AuthorityLimit = value; }
        }

        #endregion

        #endregion
    }
}