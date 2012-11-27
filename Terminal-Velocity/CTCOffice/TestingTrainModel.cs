using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrainModel : ITrainModel
    {

        public int TrainID
        {
            get { throw new NotImplementedException(); }
        }

        public double Length
        {
            get { throw new NotImplementedException(); }
        }

        public double TotalMass
        {
            get { throw new NotImplementedException(); }
        }

        public string InformationLog
        {
            get { throw new NotImplementedException(); }
        }

        public bool LightsOn
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool DoorsOpen
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Temperature
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public double CurrentAcceleration
        {
            get { throw new NotImplementedException(); }
        }

        public double CurrentVelocity
        {
            get { throw new NotImplementedException(); }
        }

        public double CurrentPosition
        {
            get { throw new NotImplementedException(); }
        }

        public int MaxCapacity
        {
            get { throw new NotImplementedException(); }
        }

        public int NumPassengers
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int NumCrew
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool BrakeFailure
        {
            get { throw new NotImplementedException(); }
        }

        public bool EngineFailure
        {
            get { throw new NotImplementedException(); }
        }

        public bool SignalPickupFailure
        {
            get { throw new NotImplementedException(); }
        }

        public bool ChangeMovement(double power)
        {
            throw new NotImplementedException();
        }

        public void EmergencyBrake()
        {
            throw new NotImplementedException();
        }
    }
}
