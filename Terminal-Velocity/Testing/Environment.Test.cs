using System.Collections.Generic;
using System.Diagnostics;
using Interfaces;
using Utility;

namespace Testing
{
    public class EnvironmentTest : ITesting
    {
        private const int MAXCOUNT = 3;
        private const int MAXTIMEOUT = 1000;
        private static int counter;

        private readonly SimulationEnvironment.SimulationEnvironment e =
            new SimulationEnvironment.SimulationEnvironment();

        private readonly Stopwatch timeout = new Stopwatch();

        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0;
            fail = 0;
            message = new List<string>();

            // Tick Test
            {
                // Test for tick event
                e.Tick += e_Tick;
                // While timer < MAXTIMEOUT
                timeout.Start();
                while (counter < MAXCOUNT && timeout.ElapsedMilliseconds < MAXTIMEOUT) ;
                // Cleanup
                timeout.Stop();
                e.Tick -= e_Tick;

                if (counter >= MAXCOUNT)
                {
                    pass++;
                    message.Add(string.Format("{0} tick events in {1} ms (timeout: {2})", counter,
                                              timeout.ElapsedMilliseconds, MAXTIMEOUT));
                }
                else if (counter < MAXCOUNT)
                {
                    fail++;
                    message.Add(string.Format("{0} tick events in {1} ms (timeout: {2})", counter,
                                              timeout.ElapsedMilliseconds, MAXTIMEOUT));
                }
                else
                    return false;
            }

            return true;
        }

        private static void e_Tick(object sender, TickEventArgs e)
        {
            counter++;
        }
    }
}