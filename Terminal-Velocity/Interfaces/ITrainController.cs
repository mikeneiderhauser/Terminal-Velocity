namespace Interfaces
{
    public interface ITrainController
    {
        int AuthorityLimit { get; set; }
        double SpeedLimit { get; set; }
        int Announcement { set; }
        double SpeedInput { get; set; }
        bool StationIncoming { set; }

        void addPassengers();
        void removePassengers();
        void checkLightsOn();
    }
}