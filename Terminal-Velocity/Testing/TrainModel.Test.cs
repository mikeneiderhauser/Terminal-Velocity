using System.Collections.Generic;
using Interfaces;
using TrackModel;
using TrainModel;

namespace Testing
{
    public class TrainModelTest : ITesting
    {
        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0;
            fail = 0;
            message = new List<string>();

            ISimulationEnvironment environment = new SimulationEnvironment.SimulationEnvironment();
            var train = new Train(0, new Block(0), environment);

            // test that the train ID is zero
            if (train.TrainID == 0)
            {
                pass++;
                message.Add("Pass: Train ID is 0, as declared.");
            }
            else
            {
                fail++;
                message.Add("Fail: Train ID is not found to be 0 when it is.");
            }

            // give acceptable power level
            if (train.ChangeMovement(50))
            {
                pass++;
                message.Add("Pass: ChangeMovement(50) succeeds");
            }
            else
            {
                fail++;
                message.Add("Fail: ChangeMovement(50) fails.");
            }

            // give unacceptable power
            if (!train.ChangeMovement(500))
            {
                pass++;
                message.Add("Pass: ChangeMovement(500) failed, as expected.");
            }
            else
            {
                fail++;
                message.Add("Fail: ChangeMovement(500) did not return false");
            }

            return true;
        }
    }
}