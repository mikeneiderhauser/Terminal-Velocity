namespace Interfaces
{
    public interface ITrainModel
    {
        /// <summary>
        ///     Get the ID of the train.
        /// </summary>
        int TrainID { get; }

        /// <summary>
        ///     Get the length of the train.
        /// </summary>
        double Length { get; }

        /// <summary>
        ///     Get the total mass of the train, including passengers.
        /// </summary>
        double TotalMass { get; }

        /// <summary>
        ///     Get the information log.
        /// </summary>
        string InformationLog { get; }

        /// <summary>
        ///     Get and set the lights to on (true) or off (false).
        /// </summary>
        bool LightsOn { get; set; }

        /// <summary>
        ///     Get and set the doors to open (true) or closed (false).
        /// </summary>
        bool DoorsOpen { get; set; }

        /// <summary>
        ///     Get and set the temperature of the train.
        /// </summary>
        int Temperature { get; set; }

        /// <summary>
        ///     Get the current acceleration of the train.
        /// </summary>
        double CurrentAcceleration { get; }

        /// <summary>
        ///     Get the current velocity of the train.
        /// </summary>
        double CurrentVelocity { get; }

        /// <summary>
        ///     Get the current position of the train along the block.
        /// </summary>
        double CurrentPosition { get; }

        /// <summary>
        ///     Get the maximum capacity.
        /// </summary>
        int MaxCapacity { get; }

        /// <summary>
        ///     Get and set the number of passengers. Updates the mass.
        /// </summary>
        int NumPassengers { get; set; }

        /// <summary>
        ///     Get and set the number of crew members. Updates the mass.
        /// </summary>
        int NumCrew { get; set; }

        /// <summary>
        ///     Get and set brake failure.
        /// </summary>
        bool BrakeFailure { get; set; }

        /// <summary>
        ///     Get and set engine failure.
        /// </summary>
        bool EngineFailure { get; set; }

        /// <summary>
        ///     Get and set signal pickup failure.
        /// </summary>
        bool SignalPickupFailure { get; set; }

        /// <summary>
        ///     Get the current block for the train.
        /// </summary>
        IBlock CurrentBlock { get; }

        /// <summary>
        ///     Get the Train Controller assigned to the train.
        /// </summary>
        ITrainController TrainController { get; }

        /// <summary>
        ///     Get and set the train controller's speed limit.
        /// </summary>
        double SpeedLimit { get; set; }

        /// <summary>
        ///     Get and set the train controller's authority limit.
        /// </summary>
        int AuthorityLimit { get; set; }

        /// <summary>
        ///     Changes the acceleration of the train based on the given power.
        /// </summary>
        /// <param name="power">Power given.</param>
        /// <returns>True if power level was within bounds, false otherwise.</returns>
        bool ChangeMovement(double power);

        /// <summary>
        ///     Applies the maximum deceleration limit to the train to stop it.
        /// </summary>
        void EmergencyBrake();
    }
}