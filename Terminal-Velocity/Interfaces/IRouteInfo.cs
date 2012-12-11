namespace Interfaces
{
    public interface IRouteInfo
    {
        /// <summary>
        /// An integer representing the RouteID.  0 for Red, 1 for Green.  Route is synonomous with 'line', 
        /// as in red or green line.
        /// </summary>
        int RouteID { get; }

        /// <summary>
        /// A string representing the name of the route/line.  Either "Red" or "Green"
        /// </summary>
        string RouteName { get; }

        /// <summary>
        /// An integer storing the number of different blocks in the line.
        /// </summary>
        int NumBlocks { get; }

        /// <summary>
        /// An array containing the different blocks in the line.
        /// </summary>
        IBlock[] BlockList { get; }

        /// <summary>
        /// An integer representing the ID of the first block in the line
        /// </summary>
        int StartBlock { get; }

        /// <summary>
        /// An integer representing the ID of the last block in the line
        /// </summary>
        int EndBlock { get; }
    }
}