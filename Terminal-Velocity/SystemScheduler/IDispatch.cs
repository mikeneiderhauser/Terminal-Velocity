using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IDispatch
    {
        DateTime Time { get; }
        int ID { get; }
        IRoute Route { get; }
    }
}
