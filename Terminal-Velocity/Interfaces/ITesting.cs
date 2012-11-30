using System.Collections.Generic;

namespace Interfaces
{
    public interface ITesting
    {
        bool DoTest(out int pass, out int fail, out List<string> messages);
    }
}