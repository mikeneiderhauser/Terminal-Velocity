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

            // Environment object
            IEnvironment environment = new TerminalVelocity.Environment();
            // Our track circuit
            ITrackCircuit currCircuit = new TrackController.TrackCircuit(environment);
            // Next track controller's circuit
            ITrackCircuit nextCircuit = new TrackController.TrackCircuit(environment);
            // Previous track controller's circuit
            ITrackCircuit prevCircuit = new TrackController.TrackCircuit(environment);

            ITrackController prev = new TrackController.TrackController(environment, currCircuit);
            ITrackController curr = new TrackController.TrackController(environment, currCircuit);
            ITrackController next = new TrackController.TrackController(environment, currCircuit);

            prev.Previous = null;
            prev.Next = curr;

            curr.Previous = prev;
            curr.Next = next;

            next.Previous = curr;
            next.Next = null;

            // The CTC Office
            ICTCOffice office = new CTCOffice.CTCOffice(environment, prev, prev);

            environment.CTCOffice = office;
            environment.PrimaryTrackControllerGreen = prev;

            return true;
        }
    }
}
