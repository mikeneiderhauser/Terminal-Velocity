using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    interface ITesting
    {
        bool DoTest(out int pass, out int fail, out List<string> messages);
    }
}
