namespace Interfaces
{
    public interface ITrackModel
    {
        TrackChanged ChangeFlag { get; }

        IBlock requestBlockInfo(int blockID, string line);
        IRouteInfo requestRouteInfo(int routeID);

        IBlock[,] requestTrackGrid(int routeID);

        bool requestUpdateSwitch(IBlock bToUpdate);
        bool requestUpdateBlock(IBlock blockToChange);

        bool RedLoaded { get; }
        bool GreenLoaded { get; }
    }

    public enum TrackChanged
    {
        None,
        Red,
        Green,
        Both
    }
}