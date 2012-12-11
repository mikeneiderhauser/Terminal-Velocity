namespace Interfaces
{
    public interface IBlock
    {
        /// <summary>
        /// An integer representing the unique ID of the block.  ID's are unique among blocks
        /// of the same line.
        /// </summary>
        int BlockID { get; }

        /// <summary>
        /// An enum Property representing the Health State of the block.  May be 'Healthy', 
        /// 'PowerFailure', 'BrokenTrackFailure', or 'CircuitFailure'.  Changing this Property followed
        /// by a call to requestUpdateBlock will update the information permanently in the database
        /// </summary>
        StateEnum State { get; set; }

        /// <summary>
        /// An integer storing the block ID of the previous block in the track.  The term 'previous' is
        /// somewhat arbitrary, as trains can travel two ways on most track segments.
        /// </summary>
        int PrevBlockID { get; }

        /// <summary>
        /// The cumulative elevation above sea level of the current block.
        /// </summary>
        double StartingElev { get; }

        /// <summary>
        /// A double value storing the fractional grade/slope of the current block.
        /// </summary>
        double Grade { get; }

        /// <summary>
        /// A length=2 integer array holding the X,Y location of the block.  Should not be used
        /// outside the trackmodel
        /// </summary>
        int[] Location { get; }

        /// <summary>
        /// A double recording the block size of the given block
        /// </summary>
        double BlockSize { get; set; }

        /// <summary>
        /// An enum Property recording the directions of travel allowed on the block
        /// </summary>
        DirEnum Direction { get; }

        /// <summary>
        /// A property recording the 'Next Destination' block.  The term Next is somewhat arbitrary, as most
        /// track blocks allow travel in both directions.  However, it is known that the 'Next Destination' 
        /// block sits at the opposite end as the prev block.  In cases where the current block has a switch, 
        /// SwitchDest1 points to the destination block to which the switch currently leads.  To flip the switch,
        ///  switch SwitchDest1 and SwitchDest2, and then call requestUpdateSwitch to commit the change to the database
        /// </summary>
        int SwitchDest1 { get; set; }

        /// <summary>
        /// A property recording the secondary 'Next Destination' block.  The term Next is somewhat arbitrary, as most
        /// track blocks allow travel in both directions.  However, it is known that the secondary 'Next Destination'
        /// block sits at the opposite end as the prev block.  In cases where the current block has a switch,
        /// SwitchDest2 points to the destination block that the switch doesn't lead to.  To flip the swtich,
        ///  swap SwitchDest1 and SwitchDest2, and then call requestUpdateSwitch to commit the change to the database
        /// </summary>
        int SwitchDest2 { get; set; }

        /// <summary>
        /// An integer recording the Track Circuit ID, pointing to the Track Circuit controlling the given block.
        /// </summary>
        int TrackCirID { get; /**set;*/ }

        /// <summary>
        /// An attribute array storing infrastructure information.  Using this property should never be necessary, with the
        ///  presence of infrastructure specific getters.
        /// </summary>
        string[] AttrArray { get; }

        /// <summary>
        /// A string describing the Line the block belongs to.  Should be either Red or Green.
        /// </summary>
        string Line { get; }

        /// <summary>
        /// An integer speed limit which trains must stay under while traveling on the given block.
        /// </summary>
        int SpeedLimit { get; }

        /// <summary>
        /// A boolean method for checking whether the given block has a switch.
        /// </summary>
        /// <returns></returns>
        bool hasSwitch();

        /// <summary>
        /// A boolean method for determining whether this block is underground.
        /// </summary>
        /// <returns></returns>
        bool hasTunnel();

        /// <summary>
        /// A boolean method for determining whether this block has a heater.
        /// </summary>
        /// <returns></returns>
        bool hasHeater();

        /// <summary>
        /// A boolean method for determining whether this block has a crossing.
        /// </summary>
        /// <returns></returns>
        bool hasCrossing();

        /// <summary>
        /// A boolean method for determining whether this block has a station.
        /// </summary>
        /// <returns></returns>
        bool hasStation();

        /// <summary>
        /// A boolean method for determining whether this block runs North
        /// </summary>
        /// <returns></returns>
        bool runsNorth();

        /// <summary>
        /// A boolean method for determining whether this block runs South
        /// </summary>
        /// <returns></returns>
        bool runsSouth();

        /// <summary>
        /// A boolean method for determining whether this block runs East
        /// </summary>
        /// <returns></returns>
        bool runsEast();

        /// <summary>
        /// A boolean method for determining whether this block runs west
        /// </summary>
        /// <returns></returns>
        bool runsWest();

        /// <summary>
        /// A boolean method for determining whether this block runs Northeast
        /// </summary>
        /// <returns></returns>
        bool runsNorthEast();

        /// <summary>
        /// A boolean method for determining whether this block runs Northwest
        /// </summary>
        /// <returns></returns>
        bool runsNorthWest();

        /// <summary>
        /// A boolean method for determining whether this block runs Southeast
        /// </summary>
        /// <returns></returns>
        bool runsSouthEast();

        /// <summary>
        /// A boolean method for determining whether this block runs Southwest
        /// </summary>
        /// <returns></returns>
        bool runsSouthWest();

        /// <summary>
        /// A method used to determine the next block a train will travel on.  The method checks both
        /// SwitchDest1 and the PrevBlockID against the block that the train was previously on (provided
        /// as an argument), to determine which direction the train is traveling on this block, and which
        /// block it will arrive at next.
        /// </summary>
        /// <param name="prevBlockIndex">The blockID of the block the train was on previously</param>
        /// <returns>The integer ID matching the blockID of the next block the train will reach</returns>
        int nextBlockIndex(int prevBlockIndex);
    }

    public enum DirEnum
    {
        North,
        South,
        East,
        West,
        Northeast,
        Northwest,
        Southeast,
        Southwest,
        North_AND_South,
        East_AND_West,
        Northeast_AND_Southwest,
        Northwest_AND_Southeast
    }

    public enum StateEnum
    {
        PowerFailure,
        BrokenTrackFailure,
        CircuitFailure,
        Healthy,
        BlockClosed
    }
}