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
        private bool _lightsOn;
        private int _numCrew;
        private int _numPassengers;
        private int _previousBlockID;
        private int _temperature;
        private bool _emergencyBrakePulled;

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
            appendInformationLog("Created.");
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

            _currentBlock = startingBlock;
            _previousBlockID = 0;
            _currentBlockID = _currentBlock.BlockID;
            _blockLength = _currentBlock.BlockSize;
            _trackCircuitID = _currentBlock.TrackCirID;

            _trackModel = environment.TrackModel;
            _trainController = new TrainController.TrainController(_environment, this);

            // set allTrains equal to list contained in environment
            allTrains = environment.AllTrains;

            _timeInterval = (environment.getInterval() / 1000.0);
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

            double currentForce = 0;
            double newAcceleration = _physicalAccelerationLimit;

            if (_currentVelocity > 0)
            {
                currentForce = power / _currentVelocity;
                newAcceleration = currentForce / _totalMass;
            }
            else
            {
                appendInformationLog("NOTIFICATION: Velocity was zero. Ignored given power and defaulted to maximum acceleration.");
            }

            // check that the new acceleration does not exceed the physical limit
            if (newAcceleration > 0 && newAcceleration > _physicalAccelerationLimit)
            {
                appendInformationLog("ERROR: POWER LEVEL CAUSED ACCELERATION TO EXCEED PHYSICAL LIMIT. POWER IGNORED.");
                return false;
            }
            // check that the new deceleration does not exceed the physical limit
            else if (newAcceleration < 0 && newAcceleration < _physicalDecelerationLimit)
            {
                appendInformationLog("ERROR: POWER LEVEL CAUSED DECELERATION TO EXCEED PHYSICAL LIMIT. POWER IGNORED");
                return false;
            }

            if ((newAcceleration > 0) && (power < 0)) // acceleration positive despite using brakes
            {
                _brakeFailure = true;
                appendInformationLog("WARNING: BRAKES APPLIED BUT TRAIN NOT SLOWING DOWN.");
            }

            if ((newAcceleration < 0) && (power > 0)) // acceleration negative despite giving positive power
            {
                appendInformationLog("WARNING: POSITIVE POWER GIVEN BUT TRAIN IS SLOWING DOWN.");
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
        ///     Updates the movement of the train. Accounts for slope and changes the block if necessary.
        /// </summary>
        private void updateMovement()
        {
            _timeInterval = (_environment.getInterval() / 1000.0); // milliseconds to seconds

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

            _currentVelocity = _currentVelocity + (_currentAcceleration * _timeInterval);

            if (_currentVelocity < 0)
            {
                _currentVelocity = 0;
                _currentAcceleration = 0;
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
                }
                else
                {
                    _currentPosition = _blockLength;
                    _currentVelocity = 0;
                    _currentAcceleration = 0;
                }
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

        /// <summary>
        ///     Appends the given string to the information log.
        /// </summary>
        /// <param name="s">The string to to append.</param>
        private void appendInformationLog(string s)
        {
            _informationLog += "(" + DateTime.Now.ToString("h\\:mm\\:ss tt") + ") ";
            _informationLog += s + "\r\n";

            _environment.sendLogEntry("For " + this.ToString() + ": " + s);
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
                _lightsOn = value;

                if (_lightsOn)
                    appendInformationLog("Lights turned on.");
                else
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
                _doorsOpen = value;

                if (_doorsOpen)
                    appendInformationLog("Doors opened.");
                else
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

                if (_numPassengers >= 0)
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

                if (_numCrew >= 0)
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
                    appendInformationLog("WARNING: EXPERIENCING BRAKE FAILURE.");
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
                    appendInformationLog("WARNING: EXPERIENCING ENGINE FAILURE.");
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
                    appendInformationLog("WARNING: EXPERIENCING SIGNAL PICKUP FAILURE.");
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
                    appendInformationLog("WARNING: Emergency brake pulled.");
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