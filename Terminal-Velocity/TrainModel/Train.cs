﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Interfaces;

namespace TrainModel
{
    public class Train : ITrainModel
    {
        //Commit for Train Class 11/16/2012 4:30
        // Has public parameters
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

        private IBlock _currentBlock;
        private double _blockLength;

        // Does not have public parameters
        private const double _initialMass = 40900; // kilograms
        private const double _personMass = 90; // kilograms
        private const double _width = 2.65; // meters
        private const double _height = 3.42; // meters
        private const double _physicalAccelerationLimit = 0.5; // meters/second^2
        private const double _physicalDecelerationLimit = -1.2; // meters/second^2
        private const double _physicalVelocityLimit = 70000; // meters/hour
        private const double _emergencyBrakeDeceleration = -2.73; // meters/second^2
        private const double _accelerationGravity = -9.8; // meters/second^2

        //TODO: get list of trains from environment
        private List<Train> allTrains;

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
            _blockLength = _currentBlock.BlockSize;

            _environment = environment;
            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);

            // TODO: set allTrains equal to list contained in environment and add this train
        }

        #endregion


        #region Public Methods

        //TODO
        public void EmergencyBrake()
        {
            _informationLog += "Train " + _trainID + "'s emergency brake pulled!\n";
        }

        //TODO
        public bool ChangeMovement(double power)
        {

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

        //TODO: Update block changes, whether going forwards or backwards
        //      Handle elevation calculations
        private void updateMovement()
        {
            _currentVelocity += _currentAcceleration; // TODO: + elevation values
            _currentPosition += _currentVelocity;


            // Handles edge of blocks for forwards and backwards
            if (_currentPosition >= _blockLength)
            {
                //_currentBlock = _currentBlock.NEXT;
                _currentPosition = _currentPosition - _blockLength;
                _blockLength = _currentBlock.BlockSize;
            }
            else if (_currentPosition < 0)
            {
                //_currentBlock = _currentBlock.PREVIOUS;
                _blockLength = _currentBlock.BlockSize;
                _currentPosition = _blockLength - _currentPosition * -1;
            }
        }

        private double calculateMass()
        {
            return (_initialMass + _personMass * (_numPassengers + _numCrew));
        }

        //TODO
        private bool checkFailures()
        {
            return false;
        }

        private void _environment_Tick(object sender, TickEventArgs e)
        {
            //handle tick here
            updateMovement();
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

        #endregion


        int ITrainModel.Length
        {
            get { throw new NotImplementedException(); }
        }
    }
}