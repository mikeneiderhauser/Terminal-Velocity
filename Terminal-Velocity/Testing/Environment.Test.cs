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
        const int MAXTIMEOUT = 10000;
        static int counter;
        
        TerminalVelocity.Environment e = new TerminalVelocity.Environment();
        System.Diagnostics.Stopwatch timeout = new System.Diagnostics.Stopwatch();

        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0; fail = 0; message = new List<string>();

            // Tick Test
            {
            // Test for tick event
                e.Tick += e_Tick;
                // While timer < MAXTIMEOUT
                timeout.Start();
                while (counter < 10 && timeout.ElapsedMilliseconds < MAXTIMEOUT) ;
                // Cleanup
                timeout.Stop();
                e.Tick -= e_Tick;

                if (counter >= 10)
                {
                    pass++;
                    message.Add(string.Format("{0} tick events in {1} ms (timeout: {2})", counter, timeout.ElapsedMilliseconds, MAXTIMEOUT));
                }
                else if (counter < 10)
                {
                    fail++;
                    message.Add(string.Format("{0} tick events in {1} ms (timeout: {2})", counter, timeout.ElapsedMilliseconds, MAXTIMEOUT));
                }
                else
                    return false;
            }
             
            return true;

        }

        static void e_Tick(object sender, TickEventArgs e)
        {
            counter++;
        }
    }
}
