using System;
using System.Collections.Generic;
using Interfaces;
using Utility;

namespace TrainModel
{
    public class Train : ITrainModel
    {
        #region Global variables

        private const double _length = 32.33; // meters

        private const int _maxCapacity = 222; // 74 seated, 148 standing

        private readonly bool _brakeFailure;
        private readonly bool _engineFailure;
        private readonly ISimulationEnvironment _environment;
        private readonly bool _signalPickupFailure;
        private readonly ITrackModel _trackModel;
        private readonly ITrainController _trainController;
        private readonly int _trainID;
        private double _blockLength;
        private double _currentAcceleration;
        private IBlock _currentBlock;
        private int _currentBlockID;
        private double _currentPosition;
        private double _currentVelocity;
        private bool _doorsOpen;
        private string _informationLog;
        private bool _lightsOn;
        private int _numCrew;
        private int _numPassengers;
        private int _previousBlockID;
        private int _temperature;

        private double _timeInterval;
        private double _totalMass;
        private int _trackCircuitID;
        private List<ITrainModel> allTrains;

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
        ///     This constructor is used when passenger, crew, and temperature information is not given.
        ///     It adds no passengers or crew and sets the temperature equal to 32 degrees Celcius.
        /// </summary>
        public Train(int trainID, IBlock startingBlock, ISimulationEnvironment environment)
        {
            _trainID = trainID;
            _totalMass = calculateMass();
            _informationLog = "Created on " + DateTime.Now + ".\r\n";
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
            _environment.Tick += _environment_Tick;

            _trackModel = environment.TrackModel;
            _trainController = new TrainController.TrainController(_environment, this);

            // set allTrains equal to list contained in environment
            allTrains = environment.AllTrains;

            _timeInterval = (environment.getInterval() / 1000.0);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Applies the maximum deceleration limit to the train to stop it.
        /// </summary>
        public void EmergencyBrake()
        {
            _informationLog += "Train " + _trainID + "'s emergency brake pulled!\r\n";
            _currentAcceleration = _emergencyBrakeDeceleration;
        }

        /// <summary>
        ///     Changes the acceleration of the train based on the given power.
        /// </summary>
        /// <param name="power">Power given.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool ChangeMovement(double power)
        {
            _informationLog += "Train " + _trainID + " given power of " + Math.Round(power, 3) + " W.\r\n";

            double currentForce = 0;
            double newAcceleration = _physicalAccelerationLimit;

            if (_currentVelocity > 0)
            {
                currentForce = power / _currentVelocity;
                newAcceleration = currentForce / _totalMass;
            }

            // check that the new acceleration does not exceed the physical limit
            if (newAcceleration > 0 && newAcceleration > _physicalAccelerationLimit)
            {
                _informationLog += "Train " + _trainID + "'s power level exceeded physical acceleration limit.\r\n";
                return false;
            }

            // check that the new deceleration does not exceed the physical limit
            else if (newAcceleration < 0 && newAcceleration < _physicalDecelerationLimit)
            {
                _informationLog += "Train " + _trainID + "'s power level exceeded physical deceleration limit.\r\n";
                return false;
            }

            _informationLog += "Train " + _trainID + " acceleration set to " + Math.Round(newAcceleration, 3) + " m/s^2.\r\n";
            _currentAcceleration = newAcceleration;
            return true;
        }

        /// <summary>
        ///     This overrides the regular ToString() method for a Train.
        /// </summary>
        /// <returns>Returns "Train " + trainID</returns>
        public override string ToString()
        {
            return "Train " + _trainID;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Occurs on every tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _environment_Tick(object sender, TickEventArgs e)
        {
            updateMovement();
        }

        /// <summary>
        ///     Updates the movement of the train. Accounts for slope and changes the block if necessary.
        /// </summary>
        private void updateMovement()
        {
            _timeInterval = (_environment.getInterval() / 1000.0); // milliseconds to seconds

            // acceleration changes due to elevation
            double angle = Math.Acos(Math.Abs(_currentBlock.Grade));
            if (_currentBlock.Grade > 0) // up hill
            {
                _currentAcceleration -= (_accelerationGravity * Math.Sin(angle));
            }
            else if (_currentBlock.Grade < 0) // down hill
            {
                _currentAcceleration += (_accelerationGravity * Math.Sin(angle));
            }

            _currentVelocity += (_currentAcceleration * _timeInterval);

            if (_currentVelocity < 0)
            {
                _currentVelocity = 0;
            }

            _currentPosition += (_currentVelocity * _timeInterval);

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
        ///     Calculates the mass of the train based on the number of passengers.
        /// </summary>
        /// <returns>The total mass.</returns>
        private double calculateMass()
        {
            return (_initialMass + _personMass * (_numPassengers + _numCrew));
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Get the ID of the train.
        /// </summary>
        public int TrainID
        {
            get { return _trainID; }
        }

        /// <summary>
        ///     Get the length of the train.
        /// </summary>
        public double Length
        {
            get { return _length; }
        }

        /// <summary>
        ///     Get the total mass of the train, including passengers.
        /// </summary>
        public double TotalMass
        {
            get { return _totalMass; }
        }

        /// <summary>
        ///     Get the information log.
        /// </summary>
        public string InformationLog
        {
            get { return _informationLog; }
        }

        /// <summary>
        ///     Get and set the lights.
        /// </summary>
        public bool LightsOn
        {
            get { return _lightsOn; }
            set
            {
                _lightsOn = value;
                _informationLog += "Train " + _trainID;

                if (_lightsOn)
                    _informationLog += " lights were turned on.\r\n";
                else
                    _informationLog += " lights were turned off.\r\n";
            }
        }

        /// <summary>
        ///     Get and set the doors.
        /// </summary>
        public bool DoorsOpen
        {
            get { return _doorsOpen; }
            set
            {
                _doorsOpen = value;
                _informationLog += "Train " + _trainID;

                if (_doorsOpen)
                    _informationLog += " doors were opened.\r\n";
                else
                    _informationLog += " doors were closed.\r\n";
            }
        }

        /// <summary>
        ///     Get and set the temperature of the train.
        /// </summary>
        public int Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                _informationLog += "Train " + _trainID + " temperature was set to " + _temperature;
            }
        }

        /// <summary>
        ///     Get the current acceleration of the train.
        /// </summary>
        public double CurrentAcceleration
        {
            get { return _currentAcceleration; }
        }

        /// <summary>
        ///     Get the current velocity of the train.
        /// </summary>
        public double CurrentVelocity
        {
            get { return _currentVelocity; }
        }

        /// <summary>
        ///     Get the current position of the train along the block.
        /// </summary>
        public double CurrentPosition
        {
            get { return _currentPosition; }
        }

        /// <summary>
        ///     Get the maximum capacity.
        /// </summary>
        public int MaxCapacity
        {
            get { return _maxCapacity; }
        }

        /// <summary>
        ///     Get and set the number of passengers. Updates the mass.
        /// </summary>
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
                    _informationLog += difference + " passengers got off of Train " + _trainID + ".\r\n";
                }
                else // people get on train
                    _informationLog += difference + " passengers got on Train " + _trainID + ".\r\n";

                _totalMass = calculateMass();
            }
        }

        /// <summary>
        ///     Get and set the number of crew members. Updates the mass.
        /// </summary>
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
                    _informationLog += difference + " crew members got off of Train " + _trainID + ".\r\n";
                }
                else // people get on train
                    _informationLog += difference + " crew members got on Train " + _trainID + ".\r\n";

                _totalMass = calculateMass();
            }
        }

        /// <summary>
        ///     Get if there is a brake failure.
        /// </summary>
        public bool BrakeFailure
        {
            get { return _brakeFailure; }
        }

        /// <summary>
        ///     Get if there is an engine failure.
        /// </summary>
        public bool EngineFailure
        {
            get { return _engineFailure; }
        }

        /// <summary>
        ///     Get if there is a signal pickup failure.
        /// </summary>
        public bool SignalPickupFailure
        {
            get { return _signalPickupFailure; }
        }

        /// <summary>
        ///     Get the current block for the train.
        /// </summary>
        public IBlock CurrentBlock
        {
            get { return _currentBlock; }
        }

        public ITrainController TrainController
        {
            get { return _trainController; }
        }

        #region Track Controller communication parameters

        /// <summary>
        ///     Get and set the train controller's speed limit.
        /// </summary>
        public double SpeedLimit
        {
            get { return _trainController.SpeedLimit; }
            set { _trainController.SpeedLimit = value; }
        }

        /// <summary>
        ///     Get and set the train controller's authority limit.
        /// </summary>
        public int AuthorityLimit
        {
            get { return _trainController.AuthorityLimit; }
            set { _trainController.AuthorityLimit = value; }
        }

        #endregion

        // TODO: double check that it works
        // for track controller communications

        #endregion
    }
}