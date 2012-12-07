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
            //provideInputFile should return false when given a null argument
            resBool = tm.provideInputFile(null);
            if (resBool == false)
            {
                pass++;
                message.Add("Pass: provideInputFile returns false when given a null argument");
            }
            else
            {
                fail++;
                message.Add("Fail: provideInputFile should return false when given a null argument");
            }
            //End Test 4
            /////////////////////////////////


            /////////////////////////////////
            //Test 5
            //provideInputFile should return false when given a non-csv file
            resBool = tm.provideInputFile(@"..\..\red.bad");//An existent but non csv file
            if (resBool == false)
            {
                pass++;
                message.Add("Pass: provideInputFile returns false when given a non-csv file");
            }
            else
            {
                fail++;
                message.Add("Fail: provideInputFile did not return false when given a non-csv file as expected");
            }
            //End Test 5
            /////////////////////////////////


            /////////////////////////////////
            //Test 6
            //provideInputFile should return true when given a valid file
            resBool=tm.provideInputFile(@"..\..\red.csv");
            if (resBool == true)
            {
                pass++;
                message.Add("Pass: provideInputFile returned true when given a valid, existing, and properly formatted file (red.csv)");
            }
            else
            {
                fail++;
                message.Add("Pass: provideInputFile returned false when given a valid and proper file, expected val was true (red.csv)");
            }
            //End Test 6
            /////////////////////////////////


            /////////////////////////////////
            //Test 7
            //provideInputFile should return true when given a valid file
            resBool = tm.provideInputFile(@"..\..\green.csv");
            if (resBool == true)
            {
                pass++;
                message.Add("Pass: provideInputFile returned true when given a valid, existing, and properly formatted file (green.csv)");
            }
            else
            {
                fail++;
                message.Add("Pass: provideInputFile returned false when given a valid and proper file, expected val was true (green.csv)");
            }
            //End Test 7
            /////////////////////////////////


            /////////////////////////////////
            //Test 8
            //Check that RedLoaded property returns true after loading
            if (tm.RedLoaded)
            {
                pass++;
                message.Add("Pass: the RedLoaded Property shows that the red line has now been loaded");
            }
            else
            {
                fail++;
                message.Add("Fail: the RedLoaded Property does not show that the red line has been loaded");
            }
            //End Test 8
            /////////////////////////////////


            /////////////////////////////////
            //Test 9
            if (tm.GreenLoaded)
            {
                pass++;
                message.Add("Pass: The GreenLoaded Property shows that the green line has now been loaded");
            }
            else
            {
                fail++;
                message.Add("Fail: The GreenLoaded Property does not show that the green line has been loaded");
            }
            //End Test 9
            /////////////////////////////////


            /////////////////////////////////
            //Test 10
            //requestBlockInfo should return null when given a negative block number
            IBlock tempIBlock = tm.requestBlockInfo(-1, "Red");
            if (tempIBlock == null)
            {
                pass++;
                message.Add("Pass: requestBlockInfo returned null when given a negative blockID");
            }
            else
            {
                fail++;
                message.Add("Fail: requestBlockInfo returned a valid block when given negative blockID, expected val was null");
            }
            //End Test 10
            /////////////////////////////////


            /////////////////////////////////
            //Test 11
            //requestBlockInfo should return null when given a blockID=0
            tempIBlock = tm.requestBlockInfo(0, "Red");
            if (tempIBlock != null)
            {
                pass++;
                message.Add("Pass: requestBlockInfo was able to retrieve the 0 block of a line");
            }
            else
            {
                fail++;
                message.Add("Fail: requestBlockInfo returned null when given blockID=0, expected val was valid block");
            }
            //End Test 11
            /////////////////////////////////


            /////////////////////////////////
            //Test 12
            //requestBlockInfo should return null when asked to retrieve an out-of-range block id
            tempIBlock = tm.requestBlockInfo(19191, "Red");
            if (tempIBlock == null)
            {
                pass++;
                message.Add("Pass: requestBlockInfo returned null as expected when given an out of range block ID");
            }
            else
            {
                fail++;
                message.Add("Fail: requestBlockInfo did not return null when given an out of range blockID, expected val was null");
            }
            //End Test 12
            /////////////////////////////////


            /////////////////////////////////
            //Test 13
            //requestBlockInfo should return null when given a bad line string
            tempIBlock = tm.requestBlockInfo(0, "asdf");
            if (tempIBlock == null)
            {
                pass++;
                message.Add("Pass: requestBlockInfo returned null when given an invalid line string");
            }
            else
            {
                fail++;
                message.Add("Fail: requestBlockInfo did not return null when given an invalid line string");
            }
            //End Test 13
            /////////////////////////////////


            /////////////////////////////////
            //Test 14
            //requestBlockInfo should return a valid block when asked to retrieve a basic block (without extra infrastructure)
            tempIBlock = tm.requestBlockInfo(1, "Red");
            if (tempIBlock != null)
            {
                pass++;
                message.Add("Pass: requestBlockInfo successfully returned a basic block without extra infrastructure");
            }
            else
            {
                fail++;
                message.Add("Fail: requestBlockInfo returned null when asked to retrieve a basic block without extra infrastructure");
            }
            //End Test 14
            /////////////////////////////////


            /////////////////////////////////
            //Test 15
            //requestBlockInfo should return a valid block when asked to retrieve a more complex block (containing infrastructure such as switch, heater, etc).
            tempIBlock = tm.requestBlockInfo(27, "Red");
            if (tempIBlock != null)
            {
                pass++;
                message.Add("Pass: requestBlockInfo successfully returned a complex block (with additional infra)");
            }
            else
            {
                fail++;
                message.Add("Fail: requestBlockInfo returns null when asked for a more complex block (with additional infra)");
            }
            //End Test 15
            /////////////////////////////////


            /////////////////////////////////
            //Test 16
            //requestUpdateBlock returns false when provided with a null value
            resBool=tm.requestUpdateBlock(null);
            if (resBool == false)
            {
                pass++;
                message.Add("Pass: requestUpdateBlock returns false when given a null value");
            }
            else
            {
                fail++;
                message.Add("Fail: requestUpdateBlock returns true when given a null value, expected value is false");
            }
            //End Test 16
            /////////////////////////////////



            /////////////////////////////////
            //Test 17
            //requestUpdateBlock returns false when provided with a block w/ ID=0 (no updating the yard)
            tempIBlock=tm.requestBlockInfo(0, "Red");
            resBool = tm.requestUpdateBlock(tempIBlock);
            if (resBool == false)
            {
                pass++;
                message.Add("Pass: requestUpdateBlock returned false when attempting to update 0 block (yard)");
            }
            else
            {
                fail++;
                message.Add("Fail: requestUpdateBlock returned true when attempting to update 0 block (yard), expected value was false");
            }
            //End Test 17
            /////////////////////////////////


            /////////////////////////////////
            //Test 18
            //requestBlockUpdate should allow change of Health State
            tempIBlock=tm.requestBlockInfo(1,"Red");

            //Store old state, and assign new state in a way that ensures oldState and newState are different
            var oldState = tempIBlock.State;
            if(oldState!=StateEnum.CircuitFailure)
                tempIBlock.State = StateEnum.CircuitFailure;
            else
                tempIBlock.State=StateEnum.BrokenTrackFailure;

            //Update the block
            resBool=tm.requestUpdateBlock(tempIBlock);
            //Re-request info from database
            tempIBlock=tm.requestBlockInfo(1,"Red");
            if (resBool && tempIBlock.State != oldState)
            {
                pass++;
            }
            else if (!resBool && tempIBlock.State==oldState)
            {
                fail++;
                message.Add("Fail: requestUpdateBlock returnd a false value, when given a changed state, expected val was true");
            }
            else
            {
                fail++;
                message.Add("Fail: requestUpdateBlock returned true, but failed to update the database");
            }
            //End Test 18
            /////////////////////////////////


            /////////////////////////////////
            //Test 19
            //requestUpdateBlock should allow updating of heater state

                    //////////////////////////////////
                            //THIS NEEDS IMPLEMENTED STILL
                            //No blocks in the init file contain heaters by default
                            //Should I assume that every block has a heater?
                            //I prob want to write a method in the BLOCK object that lets you turn the heater on and off
                            //
                    //////////////////////////////////
            //End Test 19
            /////////////////////////////////


            /////////////////////////////////
            //Test 20
            //requestUpdateBlock should return true but fail to update swith when asked to update switch information in db
            tempIBlock=tm.requestBlockInfo(1, "Red");
            int oldSD1 = tempIBlock.SwitchDest1;
            tempIBlock.SwitchDest1 = tempIBlock.SwitchDest2;
            tempIBlock.SwitchDest2 = oldSD1;
            resBool=tm.requestUpdateBlock(tempIBlock);
            tempIBlock=tm.requestBlockInfo(1, "Red");
            if (resBool == true && tempIBlock.SwitchDest1 == oldSD1)
            {
                pass++;
                message.Add("Pass: requestUpdateBlock did not update Switch state in database");
            }
            else
            {
                fail++;
                message.Add("Fail: either the returned boolean was false, or the data was updated in the db.");
            }
            //End Test 20
            /////////////////////////////////


            /////////////////////////////////
            //Test 21
            //requestRouteInfo should return a null value when given a negative route ID
            IRouteInfo tempIRouteInfo=tm.requestRouteInfo(-1);
            if (tempIRouteInfo == null)
            {
                pass++;
                message.Add("Pass: requestRouteInfo returns null when given a negative routeID");
            }
            else
            {
                fail++;
                message.Add("Fail: requestRouteInfo returned a Route when given a negative routeID, expected val was null");
            }
            //End Test 21
            /////////////////////////////////


            /////////////////////////////////
            //Test 22
            //requestRouteInfo should return a null value when given a line other than 0 or 1
            tempIRouteInfo = tm.requestRouteInfo(2);
            if (tempIRouteInfo == null)
            {
                pass++;
                message.Add("Pass: requestRouteInfo returns null when given an out of range routeID");
            }
            else
            {
                fail++;
                message.Add("Fail: requestRouteInfo returned a Route when given an out of range route ID, expected val was null");
            }
            //End Test 22
            /////////////////////////////////


            /////////////////////////////////
            //Test 23
            //requestRouteInfo returns a valid IRouteInfo object when given a line ID of 0 or 1
            tempIRouteInfo = tm.requestRouteInfo(0);
            if (tempIRouteInfo != null)
            {
                pass++;
                message.Add("Pass: requestRouteInfo returns valid IRouteInfo when given valid lineID");
            }
            else
            {
                fail++;
                message.Add("Fail: requestRouteInfo returns null when given valid lineID, expected val was null");
            }
            //End Test 23
            /////////////////////////////////


            /////////////////////////////////
            //Test 24
            //requestTrackGrid should return null when given an invalid line ID
            IBlock[,] tempIBlockArr = tm.requestTrackGrid(-1);
            if (tempIBlockArr == null)
            {
                pass++;
                message.Add("Pass: requestTrackGrid returns null when given invalid line ID");
            }
            else
            {
                fail++;
                message.Add("Fail: requestTrackGrid returns valid IBlock[,] when given invalid line id, expected value was null");
            }
            //End Test 24
            /////////////////////////////////


            /////////////////////////////////
            //Test 25
            //requestTrackGrid should return valid IBlock[,] when given valid line ID
            tempIBlockArr = tm.requestTrackGrid(0);
            if (tempIBlockArr != null)
            {
                pass++;
                message.Add("Pass: requestTrackGrid returns valid IBlock[,] when given valid line id");
            }
            else
            {
                fail++;
                message.Add("Fail: requestTrackGrid returns null when given valid line id, expected val was IBlock[,] object");
            }
            //End Test 25
            /////////////////////////////////


            /////////////////////////////////
            //Test 26
            //End Test 26
            /////////////////////////////////


            /////////////////////////////////
            //Test 27
            //End Test 27
            /////////////////////////////////


            /////////////////////////////////
            //Test 28
            //End Test 28
            /////////////////////////////////


            /////////////////////////////////
            //Test 29
            //End Test 29
            /////////////////////////////////


            /////////////////////////////////
            //Test 30
            //End Test 30
            /////////////////////////////////


            /////////////////////////////////
            //Test 31
            //End Test 31
            /////////////////////////////////


            /////////////////////////////////
            //Test 32
            //End Test 32
            /////////////////////////////////


            /////////////////////////////////
            //Test 33
            //End Test 33
            /////////////////////////////////


            /////////////////////////////////
            //Test 34
            //End Test 34
            /////////////////////////////////


            /////////////////////////////////
            //Test 35
            //End Test 35
            /////////////////////////////////


            /////////////////////////////////
            //Test 36
            //End Test 36
            /////////////////////////////////


            /////////////////////////////////
            //Test 37
            //End Test 37
            /////////////////////////////////


            /////////////////////////////////
            //Test 38
            //End Test 38
            /////////////////////////////////


            /////////////////////////////////
            //Test 39
            //End Test 39
            /////////////////////////////////


            /////////////////////////////////
            //Test 40
            //End Test 40
            /////////////////////////////////


            /////////////////////////////////
            //Test 41
            //End Test 41
            /////////////////////////////////


            /////////////////////////////////
            //Test 42
            //End Test 42
            /////////////////////////////////

            return true;
        }
    }
}