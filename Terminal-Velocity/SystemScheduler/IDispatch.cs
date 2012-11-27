using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;

namespace SystemScheduler
{
    public interface IDispatch
    {
        DateTime DispatchTime { get; }
        int DispatchID { get; }
        int DispatchRouteType { get; }
        IRoute DispatchRoute { get; }
    }
}
