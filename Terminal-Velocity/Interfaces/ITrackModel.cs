namespace Interfaces
{
    public interface ITrackModel
    {
        IBlock requestBlockInfo(int blockID, string line);
        IRouteInfo requestRouteInfo(int routeID);

        IBlock[,] requestTrackGrid(int routeID);

        bool requestUpdateSwitch(IBlock bToUpdate);
        bool requestUpdateBlock(IBlock blockToChange);
    }
}