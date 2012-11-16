using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IDispatchDatabase
    {
        string Filename { get; }
        List<IDispatch> Dispatch { get; }
    }
}
