using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IRequest
    {
        /// <summary>
        /// Property to access _trackControllerID (Identificaiton of track controller the message is for)
        /// </summary>
        int TrackControllerID { get; }

        /// <summary>
        /// Property to access _trainID (Identificaiton of the train the message is for)
        /// </summary>
        int TrainID { get; }

        /// <summary>
        /// Property to access _trainAuthority (authority / speed information)
        /// </summary>
        int TrainAuthority { get; }

        /// <summary>
        /// Property to access _trainRoute (train routing information)
        /// </summary>
        IRoute TrainRoute { get; }

        /// <summary>
        /// Property to access _issueDateTime (DateTime of when the request was created)
        /// </summary>
        DateTime IssueDateTime { get; }

        /// <summary>
        /// Property to access _block (the block the request is for)
        /// </summary>
        IBlock Block { get; }

        /// <summary>
        /// Property to access _info (Information returned by the Track Controller)
        /// </summary>
        IStatus Info { get; set; }

        /// <summary>
        /// Property to access the type of request
        /// </summary>
        RequestTypes RequestType { get; }
    }

    /// <summary>
    /// Enumeration of types of requests
    /// </summary>
    public enum RequestTypes
    {
        DispatchTrain,
        SetTrainOOS,
        AssignTrainRoute,
        SetTrainAuthority,
        TrackMaintenanceOpen,
        TrackMaintenanceClose,
        TrackControllerData,
        SetTrainSpeed
    }
}
