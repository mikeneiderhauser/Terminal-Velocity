namespace Interfaces
{
    public interface ITrainController
    {
        /// <summary>
        /// Get the Authority Limit of the train
        /// </summary>
        int AuthorityLimit { get; set; }
        /// <summary>
        /// Get the Speed Limit of the train
        /// </summary>
        double SpeedLimit { get; set; }
        /// <summary>
        /// Set the announcement
        /// </summary>
        int Announcement { set; }
        /// <summary>
        /// Get the speed command from the operator
        /// </summary>
        double SpeedInput { get; set; }
        /// <summary>
        /// How many blocks to the station, provided by the Track Controller
        /// </summary>
        int DistanceToStation { set; }


    }
}