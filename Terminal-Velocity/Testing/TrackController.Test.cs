using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;

namespace Testing
{
    public class TrackControllerTest : ITesting
    {
        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0; fail = 0; message = new List<string>();


            return true;
        }
    }
}
