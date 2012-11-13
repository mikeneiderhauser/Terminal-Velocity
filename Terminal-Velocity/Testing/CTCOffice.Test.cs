using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;

namespace Testing
{
    public class CTCOfficeTest : ITesting
    {
        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0; fail = 0; message = new List<string>();

            // Environment object
            IEnvironment environment = new TerminalVelocity.Environment();
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


            //CTCOffice.CTCOffice c = new CTCOffice.CTCOffice(environment, prev, curr);
            

            return true;
        }
    }
}
