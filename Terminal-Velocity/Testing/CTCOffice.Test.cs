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

            /*
            // Environment object
            ISimulationEnvironment environment = new SimulationEnvironment.SimulationEnvironment();
            // Our track circuit
            ITrackCircuit currCircuit = new TrackController.TrackCircuit(environment);
            // Next track controller's circuit
            ITrackCircuit nextCircuit = new TrackController.TrackCircuit(environment);
            // Previous track controller's circuit
            ITrackCircuit prevCircuit = new TrackController.TrackCircuit(environment);
            // The CTC Office
            //ICTC

            ITrackController prev = new TrackController.TrackController(environment, currCircuit);
            ITrackController curr = new TrackController.TrackController(environment, currCircuit);
            ITrackController next = new TrackController.TrackController(environment, currCircuit);

            prev.Previous = null;
            prev.Next = curr;

            curr.Previous = prev;
            curr.Next = next;

            next.Previous = curr;
            next.Next = null;

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

            return true;
             * */
            return true;
        }
    }
}