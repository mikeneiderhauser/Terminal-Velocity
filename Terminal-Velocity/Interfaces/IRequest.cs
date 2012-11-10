using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IRequest
    {
        public int TrackControllerID;
        public int TrainID;
        public int TrainAuthority;
        public IRoute TrainRoute;
        public DateTime IssueDateTime;
        public IBlock Block;
        public IStatus Info;
        public Enum RequestType;
    }
}
