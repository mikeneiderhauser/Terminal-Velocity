using System.Collections.Generic;
using Interfaces;

namespace Testing
{
    public class CTCOfficeTest : ITesting
    {
        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0;
            fail = 0;
            message = new List<string>();

            // Environment object
            ISimulationEnvironment environment = new SimulationEnvironment.SimulationEnvironment();
           
            /*
            // The CTC Office
            //ICTC

            CTCOffice.CTCOffice ctc = new CTCOffice.CTCOffice(environment, prev, curr);

            //test valid user login (implicity tests operator isAuth)
            if (ctc.Login("root", "admin"))
            {
                pass++;
            }
            else
            {
                fail++;
            }

            //test invalid user login (implicity tests operator isAuth)
            if (!ctc.Login("no", "no"))
            {
                pass++;
            }
            else
            {
                fail++;
            }

            //test Logout
            if (ctc.Logout())
            {
                pass++;
            }
            else
            {
                fail--;
            }
            */
            return true;
        }
    }
}