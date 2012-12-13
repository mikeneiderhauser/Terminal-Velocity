using System;
using System.Collections.Generic;
using Interfaces;
using Utility;

namespace TrainModel
{
    public class Train : ITrainModel
    {
        #region Global variables

        private bool _brakeFailure;
        private bool _engineFailure;
        private bool _signalPickupFailure;
        private readonly ISimulationEnvironment _environment;
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
        private int _informationCount;

        private bool _lightsOn;
        private int _numCrew;
        private int _numPassengers;
        private int _previousBlockID;
        private int _temperature;
        private bool _emergencyBrakePulled;

        private bool _positionWarningGiven;
        private bool _velocityWarningGiven;
        private bool _accelerationWarningGiven;
        private bool _decelerationWarningGiven;

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
        private const double _physicalVelocityLimit = 19.444; // meters/second
        private const double _emergencyBrakeDeceleration = -2.73; // meters/second^2
        private const double _accelerationGravity = 9.8; // meters/second^2
        private const double _length = 32.33; // meters
        private const int _maxCapacity = 222; // 74 seated, 148 standing

        #endregion

        #region Constructors

        /// <summary>
        /// This constructor is used when passenger, crew, and temperature information is not given.
        /// It adds no passengers or crew and sets the temperature equal to 32 degrees Celcius.
        /// </summary>
        /// <param name="trainID">The ID to give to the train.</param>
        /// <param name="startingBlock">The starting block of the train.</param>
        /// <param name="environment">The environment being used by the entire simulation.</param>
        public Train(int trainID, IBlock startingBlock, ISimulationEnvironment environment)
        {
            _environment = environment;
            _environment.Tick += _environment_Tick;

            _trainID = trainID;
            _totalMass = calculateMass();
            _informationLog = "";
            _informationCount = 0;
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

            _emergencyBrakePulled = false;

            _positionWarningGiven = false;
            _velocityWarningGiven = false;
            _accelerationWarningGiven = false;
            _decelerationWarningGiven = false;

            _currentBlock = startingBlock;
            _previousBlockID = 0;
            _currentBlockID = _currentBlock.BlockID;
            _blockLength = _currentBlock.BlockSize;
            _trackCircuitID = _currentBlock.TrackCirID;

            _trackModel = environment.TrackModel;
            _trainController = new TrainController.TrainController(_environment, this);

            // set allTrains equal to list contained in environment
            allTrains = environment.AllTrains;

            _timeInterval = (environment.GetInterval() / 1000.0);

            appendInformationLog("Created on block " + _currentBlockID + ".");
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Toggles the emergency brake on and off.
        /// </summary>
        public void EmergencyBrake()
        {
            this.EmergencyBrakePulled = !this.EmergencyBrakePulled;
        }

        /// <summary>
        ///     Changes the acceleration of the train based on the given power.
        /// </summary>
        /// <param name="power">Power given.</param>
        /// <returns>True if power level was within bounds, false otherwise.</returns>
        public bool ChangeMovement(double power)
        {
            // can't change movement if engine is failing
            if (_engineFailure)
            {
                appendInformationLog("ERROR: Engine cannot be given power because of engine failure.");
                return false;
            }

            // cannot apply brakes if brakes are failing
            if (_brakeFailure && (power < 0)) 
            {
                appendInformationLog("ERROR: Brakes cannot be applied due to brake failure.");
                return false;
            }

            appendInformationLog("Given power of " + Math.Round(power, 3) + " W.");

            // acceleration changes due to elevation
            double absGrade = Math.Abs(_currentBlock.Grade);
            double angle = Math.Atan(absGrade);
            
            double accelerationChange;
            double newAcceleration;

            if (_currentBlock.Grade > 0) // up hill
            {
                accelerationChange = -1 * _accelerationGravity * Math.Sin(angle);
                newAcceleration = _physicalAccelerationLimit + accelerationChange;
            }
            else if (_currentBlock.Grade < 0) // down hill
            {
                accelerationChange = _accelerationGravity * Math.Sin(angle);
                newAcceleration = _physicalAccelerationLimit + accelerationChange;
            }
            else // no grade
            {
                accelerationChange = 0;
                newAcceleration = _physicalAccelerationLimit + accelerationChange;
            }

            double currentForce = 0;

            if (_currentVelocity > 0)
            {
                currentForce = power / _currentVelocity;
                newAcceleration = currentForce / _totalMass;
            }
            else
            {
                appendInformationLog("NOTIFICATION: Velocity was zero. Defaulted to maximum acceleration.");
            }


            if ((newAcceleration > 0) && (power < 0)) // acceleration positive despite using brakes
            {
                appendInformationLog("WARNING: Brakes applied but train not slowing down.");
            }

            if ((newAcceleration < 0) && (power > 0)) // acceleration negative despite giving positive power
            {
                appendInformationLog("WARNING: Positive power given but train is slowing down.");
            }

            _currentAcceleration = newAcceleration;
            appendInformationLog("Acceleration set to " + Math.Round(newAcceleration, 3) + " m/s^2.");
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

        /// <summary>
        ///     Updates the movement of the train. Accounts for slope and changes the block if necessary.
        /// </summary>
        public void updateMovement()
        {
            _timeInterval = (_environment.GetInterval() / 1000.0); // milliseconds to seconds

            // can't accelerate or decelerate if engine has failed
            if (_engineFailure)
            {
                _currentAcceleration = 0;
            }

            if (_emergencyBrakePulled)
            {
                _currentAcceleration = _emergencyBrakeDeceleration;
            }

            // acceleration changes due to elevation
            double absGrade = Math.Abs(_currentBlock.Grade);
            double angle = Math.Atan(absGrade);

            if (_currentBlock.Grade > 0) // up hill
            {
                _currentAcceleration = _currentAcceleration - (_accelerationGravity * Math.Sin(angle));
            }
            else if (_currentBlock.Grade < 0) // down hill
            {
                _currentAcceleration = _currentAcceleration + (_accelerationGravity * Math.Sin(angle));
            }

            // stops acceleration due to slope when emergency brake is on
            if ((_currentAcceleration > 0) && (_emergencyBrakePulled))
            {
                _currentAcceleration = 0;
            }

            // check if the acceleration is greater than the physical limit
            if (_currentAcceleration > _physicalAccelerationLimit)
            {
                _currentAcceleration = _physicalAccelerationLimit;

                if (!_accelerationWarningGiven)
                {
                    appendInformationLog("WARNING: Acceleration exceeded physical limit.");
                    _accelerationWarningGiven = true;
                }
            }
            else
            {
                _accelerationWarningGiven = false;
            }

            // check if the deceleration is greater than the physical limit and emergency brake isn't toggled
            if (_currentAcceleration < _physicalDecelerationLimit && !_emergencyBrakePulled)
            {
                _currentAcceleration = _physicalDecelerationLimit;

                if (!_decelerationWarningGiven)
                {
                    appendInformationLog("WARNING: Deceleration exceeded physical limit.");
                    _decelerationWarningGiven = true;
                }
            }
            else
            {
                _decelerationWarningGiven = false;
            }

            _currentVelocity = _currentVelocity + (_currentAcceleration * _timeInterval);

            // check if velocity is less than zero or greater than the limit
            if (_currentVelocity < 0)
            {
                _currentVelocity = 0;
                _currentAcceleration = 0;

                if (!_velocityWarningGiven)
                {
                    appendInformationLog("WARNING: Velocity less than zero. Train stopped.");
                    _velocityWarningGiven = true;
                }
            }
            else if (_currentVelocity > _physicalVelocityLimit)
            {
                _currentVelocity = _physicalVelocityLimit;
                _currentAcceleration = 0;

                // check if velocity warning already given
                if (!_velocityWarningGiven)
                {
                    appendInformationLog("WARNING: Velocity exceeded physical limit.");
                    _velocityWarningGiven = true;
                }
            }
            else
            {
                _velocityWarningGiven = false;
            }

            _currentPosition = _currentPosition + (_currentVelocity * _timeInterval);

            // Handles edge of block, only going forward
            if (_currentPosition >= _blockLength)
            {
                // get next block ID based on the previous ID
                int nextBlockID = _currentBlock.nextBlockIndex(_previousBlockID);
                IBlock nextBlock = _trackModel.requestBlockInfo(nextBlockID, _currentBlock.Line);

                if (nextBlock != null)
                {
                    _previousBlockID = _currentBlockID; // previous block is now current block
                    _currentBlock = nextBlock;
                    _currentBlockID = _currentBlock.BlockID; // update the current block to be the next block

                    // update the current position of the train
                    _currentPosition = _currentPosition - _blockLength;
                    _blockLength = _currentBlock.BlockSize;

                    appendInformationLog("Moved to block " + _currentBlockID + ".");
                }
                else // cannot find block
                {
                    _currentPosition = _blockLength;
                    _currentVelocity = 0;
                    _currentAcceleration = 0;

                    // check if already given warning
                    if (!_positionWarningGiven)
                    {
                        appendInformationLog("WARNING: Cannot find the next block.");
                        _positionWarningGiven = true;
                    }
                    else
                    {
                        _positionWarningGiven = false;
                    }

                }
            }


            // TODO: added implementation
            if (_trackCircuitID != _currentBlock.TrackCirID)
            {
                _trackCircuitID = _currentBlock.TrackCirID;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Occurs on every tick.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Tick event args</param>
        private void _environment_Tick(object sender, TickEventArgs e)
        {
            updateMovement();
        }

        /// <summary>
        ///     Calculates the mass of the train based on the number of passengers.
        /// </summary>
        /// <returns>The total mass.</returns>
        private double calculateMass()
        {
            return (_initialMass + _personMass * (_numPassengers + _numCrew));
        }

        /// <summary>
        ///     Appends the given string to the information log.
        /// </summary>
        /// <param name="s">The string to to append.</param>
        private void appendInformationLog(string s)
        {
            if (_informationCount > 2000)
            {
                _informationLog = ""; // clear the log
                _informationCount = 0;
            }

            _informationLog += "(" + DateTime.Now.ToString("h\\:mm\\:ss tt") + ") ";
            _informationLog += s + "\r\n";

            _environment.SendLogEntry("For " + this.ToString() + ": " + s);

            _informationCount++;
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
        ///     Get and set the lights to on (true) or off (false).
        /// </summary>
        public bool LightsOn
        {
            get { return _lightsOn; }
            set
            {
                bool previousValue = _lightsOn;
                _lightsOn = value;

                if (_lightsOn && (_lightsOn != previousValue))
                    appendInformationLog("Lights turned on.");
                else if(!_lightsOn && (_lightsOn != previousValue))
                    appendInformationLog("Lights turned off.");
            }
        }

        /// <summary>
        ///     Get and set the doors to open (true) or closed (false).
        /// </summary>
        public bool DoorsOpen
        {
            get { return _doorsOpen; }
            set
            {
                bool previousValue = _doorsOpen;
                _doorsOpen = value;

                if (_doorsOpen && (_doorsOpen != previousValue))
                    appendInformationLog("Doors opened.");
                else if (!_doorsOpen && (_doorsOpen != previousValue))
                    appendInformationLog("Doors closed.");
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
                appendInformationLog("Temperature set to " + _temperature + " °F.");
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
                int totalPassengers = _numPassengers + _numCrew;

                if (totalPassengers > _maxCapacity) // too many people
                {
                    _numPassengers = oldNumPassengers;
                    appendInformationLog("ERROR: Tried to add " + (totalPassengers - _maxCapacity) + " more people than allowed.");
                }
                else if (_numPassengers >= 0) // positive number
                {
                    if (difference < 0) // people get off train
                    {
                        difference *= -1;
                        appendInformationLog(difference + " passengers got off.");
                    }
                    else // people get on train
                    {
                        appendInformationLog(difference + " passengers got on.");
                    }

                    _totalMass = calculateMass();
                }
                else
                {
                    appendInformationLog("ERROR: Attempted to set number of passengers to negative number.");
                    _numPassengers = oldNumPassengers;
                }
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
                int totalPassengers = _numPassengers + _numCrew;

                if (totalPassengers > _maxCapacity) // too many people
                {
                    _numCrew = oldNumCrew;
                    appendInformationLog("ERROR: Tried to add " + (totalPassengers - _maxCapacity) + " more people than allowed.");
                }
                else if (_numCrew >= 0) // positive number
                {
                    if (difference < 0) // crew get off train
                    {
                        difference *= -1;
                        appendInformationLog(difference + " crew members got off.");
                    }
                    else // crew get on train
                    {
                        appendInformationLog(difference + " crew members got on.");
                    }

                    _totalMass = calculateMass();
                }
                else
                {
                    appendInformationLog("ERROR: Attempted to set number of crew members to negative number.");
                    _numCrew = oldNumCrew;
                }
            }
        }

        /// <summary>
        ///     Get if there is a brake failure.
        /// </summary>
        public bool BrakeFailure
        {
            get { return _brakeFailure; }
            set
            {
                _brakeFailure = value;

                if (_brakeFailure)
                {
                    appendInformationLog("CRITICAL: EXPERIENCING BRAKE FAILURE.");
                }
            }
        }

        /// <summary>
        ///     Get if there is an engine failure.
        /// </summary>
        public bool EngineFailure
        {
            get { return _engineFailure; }
            set
            {
                _engineFailure = value;

                if (_engineFailure)
                {
                    appendInformationLog("CRITICAL: EXPERIENCING ENGINE FAILURE.");
                }
            }
        }

        /// <summary>
        ///     Get if there is a signal pickup failure.
        /// </summary>
        public bool SignalPickupFailure
        {
            get { return _signalPickupFailure; }
            set
            {
                _signalPickupFailure = value;

                if (_signalPickupFailure)
                {
                    appendInformationLog("CRITICAL: EXPERIENCING SIGNAL PICKUP FAILURE.");
                }
            }
        }

        /// <summary>
        ///     Get the current block for the train.
        /// </summary>
        public IBlock CurrentBlock
        {
            get { return _currentBlock; }
        }

        /// <summary>
        ///     Get the Train Controller assigned to the train.
        /// </summary>
        public ITrainController TrainController
        {
            get { return _trainController; }
        }

        public bool EmergencyBrakePulled
        {
            get { return _emergencyBrakePulled; }
            set
            {
                _emergencyBrakePulled = value;

                if (_emergencyBrakePulled)
                {
                    appendInformationLog("Emergency brake pulled.");
                }
                else
                {
                    appendInformationLog("Emergency brake released.");
                }
            }
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


        #endregion
    }
}