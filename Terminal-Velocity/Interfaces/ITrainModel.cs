using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface ITrainModel
    {
        int TrainID { get; }
        int Length { get; }
        double TotalMass { get; }
        string InformationLog { get; }
        bool LightsOn { get; set; }
        bool DoorsOpen { get; set; }
        int Temperature { get; set; }
        double CurrentAcceleration { get; }
        double CurrentVelocity { get; }
        double CurrentPosition { get; }
        int MaxCapacity { get; }
        int NumPassengers { get; set; }
        int NumCrew { get; set; }
        bool BrakeFailure { get; }
        bool EngineFailure { get; }
        bool SignalPickupFailure { get; }

        bool ChangeMovement(double power);
        void EmergencyBrake();
    }
}
