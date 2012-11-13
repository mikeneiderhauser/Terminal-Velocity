using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;

namespace TrainModel
{
    public class Train : ITrainModel
    {
        // Has public parameters
        private int _trainID;
        private int _totalLength;
        private double _totalMass;
        private string _informationLog;
        private bool _lightsOn;
        private bool _doorOpen;
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
        private const double _width = 2.65; // meters
        private const double _height = 3.42; // meters
        private const double _physicalAccelerationLimit;
        private const double _physicalDecelerationLimit;
        private const double _physicalVelocityLimit;
        private IEnvironment _environment;

        public Train(int trainID, IEnvironment environment)
        {
            _trainID = trainID;
            _environment = environment;
            _currentAcceleration = 0;
            _currentVelocity = 0;
            _currentPosition = 0;
        }

        public void EmergencyBrake()
        {
        }

        public bool ChangeMovement(double power)
        {

            return true;
        }

        // private functions
        private double calculateMass()
        {
            return 0;
        }

        private void updateMovement()
        {

        }

        private bool checkFailures()
        {
            return false;
        }
    }
}
