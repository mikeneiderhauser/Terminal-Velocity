using System.Collections.Generic;
using Interfaces;
using TrackModel;
using TrainModel;

namespace Testing
{
    public class TrackModelTest : ITesting
    {
        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0;
            fail = 0;
            message = new List<string>();

            ISimulationEnvironment environment = new SimulationEnvironment.SimulationEnvironment();
            var tm = new TrackModel.TrackModel(environment);

            /////////////////////////////////
            //Test 1
            //Check that RedLoaded is initially false
            if (tm.RedLoaded == false)
            {
                pass++;
                message.Add("Pass: RedLoaded is initially false");
            }
            else
            {
                fail++;
                message.Add("Fail: RedLoaded was not initially false, as it should be prior to loading RedLine");
            }
            //End test 1
            /////////////////////////////////


            /////////////////////////////////
            //Check that GreenLoaded is initially false
            if (tm.GreenLoaded == false)
            {
                pass++;
                message.Add("Pass: GreenLoaded is initially false, as desired");
            }
            else
            {
                fail++;
                message.Add("Fail: GreenLoaded was not initially false, as it should be prior to loading GreenLine");
            }
            /////////////////////////////////

            return true;
        }
    }
}