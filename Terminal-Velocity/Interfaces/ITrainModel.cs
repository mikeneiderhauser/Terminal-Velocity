using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface ITrainModel
    {
        public int TrainID { get; }
        public int TotalLength { get; }
        public double TotalMass { get; }
        public string InformationLog { get; }
        public bool LightsOn { get; set; }
        public bool DoorOpen { get; set; }
        public int Temperature { get; set; }
        public double CurrentAcceleration { get; }
        public double CurrentVelocity { get; }
        public double CurrentPosition { get; }
        public int MaxCapacity { get; }
        public int NumPassengers { get; set; }
        public int NumCrew { get; set; }
        public bool BrakeFailure { get; }
        public bool EngineFailure { get; }
        public bool SignalPickupFailure { get; }

        public bool ChangeMovement(double power);
        public void EmergencyBrake();
    }
}
