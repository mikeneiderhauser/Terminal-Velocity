namespace Interfaces
{
    public interface ITrackModel
    {
        /// <summary>
        /// Property returns an enum indicating which (if any) of the Track Lines
        /// have been changed since the last check.
        /// </summary>
        TrackChanged ChangeFlag { get; }

        /// <summary>
        /// Best way to get basic information on a single block within a given line
        /// </summary>
        /// <param name="blockID"> The blockID requested</param>
        /// <param name="line">The line desired: should be "Red" or "Green"</param>
        /// <returns>Returns a Block object implementing the IBlock interface</returns>
        IBlock requestBlockInfo(int blockID, string line);

        /// <summary>
        /// Best way to get basic information on a single route/line.  For the purpose
        /// of this method, route in synonymous of 'line' (as in Red, or Green).
        /// </summary>
        /// <param name="routeID">An integer uniquely identifying the route.  0 represents the 
        /// Red Line, and 1 represents the Green Line.</param>
        /// <returns>An IRouteInfo block.  Contains list of blocks, along with start and stop information</returns>
        IRouteInfo requestRouteInfo(int routeID);

        /// <summary>
        /// A method used to get an array of blocks from 1 point to another specified point
        /// </summary>
        /// <param name="startBlockID">The block ID of the starting point</param>
        /// <param name="endBlockID">The block ID of the ending point</param>
        /// <param name="line">The line the blocks reside on: either "Red" or "Green"</param>
        /// <returns></returns>
        IBlock[] requestPath(int startBlockID, int endBlockID, string line);


        /// <summary>
        /// Generally useful for displaying the track visually.
        /// </summary>
        /// <param name="routeID">Route ID refers to the unique identifier for the track line.  0 corresponds to Red, 
        /// while 1 corresponds to Green.</param>
        /// <returns>Returns a 2D grid of IBlock objects</returns>
        IBlock[,] requestTrackGrid(int routeID);

        /// <summary>
        /// Allows a database update to the switch state for a given block
        /// </summary>
        /// <param name="bToUpdate">The IBlock object containing the switch changes the user wishes to make to the database</param>
        /// <returns>A boolean relating to the success of the update operation</returns>
        bool requestUpdateSwitch(IBlock bToUpdate);

        /// <summary>
        /// Allows a database update to other variable pieces of a given block.  Updated values include state 
        /// (healthy, broken, failed).  That's it.
        /// </summary>
        /// <param name="blockToChange">The IBlock object containing any changes the user wishes to make to the database</param>
        /// <returns>A boolean representing the success of the update operation</returns>
        bool requestUpdateBlock(IBlock blockToChange);

        /// <summary>
        /// A boolean readonly property representing whether or not the Track Model has loaded the Red line yet
        /// </summary>
        bool RedLoaded { get; }

        /// <summary>
        /// A boolean readonly property representing whether or not the Track Model has loaded the Green line yet.
        /// </summary>
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