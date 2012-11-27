using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;

namespace SystemScheduler
{
    public interface IDispatchDatabase
    {
        string DispatchDatabaseFilename { get; }
        List<IDispatch> DispatchList { get; }
        List<string[]> DispatchDatabaseDataSource { get; }
    }
}
