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
        public bool DoTest(out int pass, out int fail, out string[] message)
        {
            TerminalVelocity.Environment e = new TerminalVelocity.Environment();
            e.Tick += e_Tick;

            while (true) ;
        }

        static void e_Tick(object sender, TickEventArgs e)
        {
            Console.WriteLine("Ticks " + e.Ticks);
        }
    }
}
