using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace Testing
{
    public class EnvironmentTest : ITesting
    {
        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0; fail = 0; message = new List<string>();

            TerminalVelocity.Environment e = new TerminalVelocity.Environment();
            e.Tick += e_Tick;

            e.Tick -= e_Tick;

            return false;

        }

        static void e_Tick(object sender, TickEventArgs e)
        {

            Console.WriteLine("Ticks " + e.Ticks);
        }
    }
}
