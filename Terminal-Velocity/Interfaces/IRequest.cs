using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IRequest
    {
        int TrackControllerID { get; }
        int TrainID { get; }
        int TrainAuthority { get; }
        IRoute TrainRoute { get; }
        DateTime IssueDateTime { get; }
        IBlock Block { get; }
        IStatus Info { get; set; }
        RequestTypes RequestType { get; }
    }

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
