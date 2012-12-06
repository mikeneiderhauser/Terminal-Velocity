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
            //Test 2
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


            /////////////////////////////////
            //Test 3
            //provideInputFile returns false when given bad input file
            bool resBool=tm.provideInputFile("asdfawecxwe");/** Gee, Really hope this file doesnt exist*/
            if (resBool == false)
            {
                pass++;
                message.Add("Pass: provideInputFile returns false when given bad file");
            }
            else
            {
                fail++;
                message.Add("Fail: provideInputFile didn't return false when given an invalid filename");
            }
            //End Test 3
            /////////////////////////////////


            /////////////////////////////////
            //Test 4
            //
            //End Test 4
            /////////////////////////////////

            /////////////////////////////////
            //Test 5
            //
            //End Test 5
            /////////////////////////////////

            /////////////////////////////////
            //Test 6
            //
            //End Test 6
            /////////////////////////////////


            /////////////////////////////////
            //Test 7
            //
            //End Test 7
            /////////////////////////////////


            /////////////////////////////////
            //Test 8
            //
            //End Test 8
            /////////////////////////////////


            /////////////////////////////////
            //Test 9
            //
            //End Test 9
            /////////////////////////////////


            /////////////////////////////////
            //Test 10
            //
            //End Test 10
            /////////////////////////////////


            /////////////////////////////////
            //Test 11
            //
            //End Test 11
            /////////////////////////////////


            /////////////////////////////////
            //Test 12
            //
            //End Test 12
            /////////////////////////////////


            /////////////////////////////////
            //Test 13
            //
            //End Test 13
            /////////////////////////////////


            /////////////////////////////////
            //Test 14
            //
            //End Test 14
            /////////////////////////////////


            /////////////////////////////////
            //Test 15
            //
            //End Test 15
            /////////////////////////////////


            /////////////////////////////////
            //Test 16
            //
            //End Test 16
            /////////////////////////////////



            /////////////////////////////////
            //Test 17
            //
            //End Test 17
            /////////////////////////////////


            /////////////////////////////////
            //Test 18
            //
            //End Test 18
            /////////////////////////////////


            /////////////////////////////////
            //Test 19
            //
            //End Test 19
            /////////////////////////////////


            /////////////////////////////////
            //Test 20
            //
            //End Test 20
            /////////////////////////////////


            /////////////////////////////////
            //Test 21
            //
            //End Test 21
            /////////////////////////////////


            /////////////////////////////////
            //Test 22
            //
            //End Test 22
            /////////////////////////////////


            /////////////////////////////////
            //Test 23
            //
            //End Test 23
            /////////////////////////////////


            /////////////////////////////////
            //Test 24
            //
            //End Test 24
            /////////////////////////////////


            /////////////////////////////////
            //Test 25
            //
            //End Test 25
            /////////////////////////////////

            return true;
        }
    }
}