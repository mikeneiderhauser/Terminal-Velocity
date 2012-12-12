namespace Interfaces
{
    public interface ITrainController
    {
        int AuthorityLimit { get; set; }
        double SpeedLimit { get; set; }
        int Announcement { set; }
        double SpeedInput { get; set; }
        int DistanceToStation { set; }

        void addPassengers();
        void removePassengers();
        void checkLightsOn();
    }
}