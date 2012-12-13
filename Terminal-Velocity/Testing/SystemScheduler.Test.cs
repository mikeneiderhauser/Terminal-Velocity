using System.Collections.Generic;
using Interfaces;
using SystemScheduler;

namespace Testing
{
    class SystemSchedulerTest : ITesting
    {
        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0;
            fail = 0;
            message = new List<string>();

            ISimulationEnvironment environment = new SimulationEnvironment.SimulationEnvironment();
            ICTCOffice fakeCTC = new SystemScheduler.CTCOffice();
            var testSystemScheduler = new SystemScheduler.SystemScheduler(environment, fakeCTC);

            /////////////////////////////////
            //Test 1
            //Check that there is no initial dispatch database loaded
            if (testSystemScheduler.DispatchDatabase == null)
            {
                pass++;
                message.Add("Pass: The DispatchDatabase is initially null");
            }
            else
            {
                fail++;
                message.Add("Fail: The DispatchDatabase is not initially null. Did you hardcode it to load, you n00b?");
            }
            //End test 1
            /////////////////////////////////

            /////////////////////////////////
            //Test 2
            //Attempt to load a database that doesn't exist
            testSystemScheduler.NewFile("theresnowaythisactuallyexistslol");
            if (testSystemScheduler.DispatchDatabase == null)
            {
                pass++;
                message.Add("Pass: The scheduler responded correctly to an attempt to load a missing file.");
            }
            else
            {
                fail++;
                message.Add("Fail: I'm not sure how you would get here, but just in case, your scheduler is loading from files that don't exist. Nice.");
            }
            //End test 2
            /////////////////////////////////

            /////////////////////////////////
            //Test 3
            //Attempt to load a database that isn't properly formatted
            testSystemScheduler.NewFile("..\\..\\Computer Benchmarks.csv");
            if (testSystemScheduler.DispatchDatabase.SuccessfulParse == false)
            {
                pass++;
                message.Add("Pass: The scheduler responded correctly to an attempt to load an improperly formatted file.");
            }
            else
            {
                fail++;
                message.Add("Fail: If you see me, it means your error checking isn't robust enough :) It loaded a bad file.");
            }
            //End test 3
            /////////////////////////////////

            /////////////////////////////////
            //Test 3
            //Attempt to load a database that isn't properly formatted
            testSystemScheduler.NewFile("..\\..\\Computer Benchmarks.csv");
            if (testSystemScheduler.DispatchDatabase.SuccessfulParse == false)
            {
                pass++;
                message.Add("Pass: The scheduler responded correctly to an attempt to load an improperly formatted file.");
            }
            else
            {
                fail++;
                message.Add("Fail: If you see me, it means your error checking isn't robust enough :) It loaded a bad file.");
            }
            //End test 3
            /////////////////////////////////

            /////////////////////////////////
            //Test 4
            //Attempt to load a correctly formatted database
            testSystemScheduler.NewFile("..\\..\\Correct File.csv");
            if (testSystemScheduler.DispatchDatabase.SuccessfulParse == true)
            {
                pass++;
                message.Add("Pass: The scheduler loaded in the values from a database.");
            }
            else
            {
                fail++;
                message.Add("Fail: Your scheduler did not load in the data it was supposed to. Find out what went wrong!");
            }
            //End test 4
            /////////////////////////////////

            /////////////////////////////////
            //Test 5
            //Did it load the correct number of dispatches
            if (testSystemScheduler.DispatchDatabase.DispatchList.Count == 5)
            {
                pass++;
                message.Add("Pass: The scheduler loaded in the correct number of values from a database.");
            }
            else
            {
                fail++;
                message.Add("Fail: Your scheduler did not load in the correct amount of data it was supposed to. Check the file to see if it's messed up.");
            }
            //End test 5
            /////////////////////////////////

            /////////////////////////////////
            //Test 6
            //Can it delete from the database
            testSystemScheduler.DispatchDatabase.RemoveDispatch(1);
            if (testSystemScheduler.DispatchDatabase.DispatchList.Count == 4)
            {
                pass++;
                message.Add("Pass: The scheduler was able to remove a dispatch from its database.");
            }
            else
            {
                fail++;
                message.Add("Fail: Your scheduler did not remove a record from the database. Check to make sure the file is okay.");
            }
            //End test 6
            /////////////////////////////////

            /////////////////////////////////
            //Test 7
            //Can it add to the database
            testSystemScheduler.DispatchDatabase.AddDispatch("-1", "3:00 AM", "1", "Red", "0");
            testSystemScheduler.DispatchDatabase.AddDispatch("-1", "3:00 AM", "1", "Red", "0");
            if (testSystemScheduler.DispatchDatabase.DispatchList.Count == 6)
            {
                pass++;
                message.Add("Pass: The scheduler was able to add 2 dispatch orders to its database.");
            }
            else
            {
                fail++;
                message.Add("Fail: Your scheduler did not add records to the database. Find out what went wrong.");
            }
            //End test 7
            /////////////////////////////////

            /////////////////////////////////
            //Test 8
            //Can it add to the database without generating a new ID
            testSystemScheduler.DispatchDatabase.AddDispatch("7", "3:00 AM", "1", "Green", "1");
            if (testSystemScheduler.DispatchDatabase.DispatchList.Count == 7)
            {
                pass++;
                message.Add("Pass: The scheduler was able to add a dispatch order without generating and ID to its database.");
            }
            else
            {
                fail++;
                message.Add("Fail: Your scheduler did not add records to the database as would occur in an edit. If test 7 works, you can probably guess where this is broken.");
            }
            //End test 8
            /////////////////////////////////

            /////////////////////////////////
            //Test 9
            //Can it add to the database without generating a new ID
            testSystemScheduler.DispatchDatabase.AddDispatch("7", "3:00 AM", "1", "Green", "1");
            if (testSystemScheduler.DispatchDatabase.DispatchList.Count == 7)
            {
                pass++;
                message.Add("Pass: The scheduler was able to add a dispatch order without generating and ID to its database.");
            }
            else
            {
                fail++;
                message.Add("Fail: Your scheduler did not add records to the database as would occur in an edit. If test 7 works, you can probably guess where this is broken.");
            }
            //End test 9
            /////////////////////////////////



            return true;

        }
    }
}