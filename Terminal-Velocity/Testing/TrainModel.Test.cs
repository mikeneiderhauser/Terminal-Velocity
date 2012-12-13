using System.Collections.Generic;
using Interfaces;
using TrackModel;
using TrainModel;
using System.Linq;
using System;

namespace Testing
{
    public class TrainModelTest : ITesting
    {
        ISimulationEnvironment _environment;

        const float BlockLengh = 0.5F;
        Block startBlock;
        List<IBlock> blocks;

        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0;
            fail = 0;
            message = new List<string>();

            _environment = new SimulationEnvironment.SimulationEnvironment();

            // Create 100 blocks for the red line
            blocks = new List<IBlock>();

            // First block
            startBlock = new Block(0, StateEnum.Healthy, 99, 0, 0, new[] { 0, 0 }, BlockLengh, DirEnum.East, new[] { "" }, 2, 0, 0, "Red", 100);
            blocks.Add(startBlock);

            // Next 99 blocks
            for (var i = 1; i < 99; i++)
                blocks.Add(new Block(i, StateEnum.Healthy, i - 1, 0, 0, new[] { 0, 0 }, BlockLengh, DirEnum.East, new[] { "" }, i + 1, 0, 0, "Red", 100));

            // Last block
            blocks.Add(new Block(99, StateEnum.Healthy, 98, 0, 0, new[] { 0, 0 }, BlockLengh, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 100));

            _environment.TrackModel = new DummyTrackModel(blocks);

            // mass test
            if (TestMass(message))
                pass++;
            else
                fail++;

            // adding passengers test
            if (TestAddPassengers(message))
                pass++;
            else
                fail++;

            // removing passengers test
            if (TestRemovePassengers(message))
                pass++;
            else
                fail++;

            // lights test
            if (TestLights(message))
                pass++;
            else
                fail++;

            // doors test
            if (TestDoors(message))
                pass++;
            else
                fail++;

            // changing temperature test
            if (TestTemperature(message))
                pass++;
            else
                fail++;

            // test emergency brake
            if (TestEmergencyBrake(message))
                pass++;
            else
                fail++;

            // test movement without grade
            if (TestMovement_NoGrade(message))
                pass++;
            else
                fail++;

            // test movement with positive grade
            if (TestMovement_PositiveGrade(message))
                pass++;
            else
                fail++;

            // test movement with negative grade
            if (TestMovement_NegativeGrade(message))
                pass++;
            else
                fail++;

            // test braking during brake failure
            if (TestBrakeFailureMovement(message))
                pass++;
            else
                fail++;

            // test acceleration during engine failure
            if (TestEngineFailureMovement(message))
                pass++;
            else
                fail++;

            return true;
        }

        private bool TestMass(List<string> messages)
        {
            bool result = true;

            double initialTrainMass = 40900; //kilograms
            double personMass = 90; //kilograms
            Random random = new Random();

            Train train = new Train(0, startBlock, _environment);

            int randomNum = random.Next(0, 223); // 0 to 222 passengers
            train.NumPassengers = randomNum;

            double compareMass = initialTrainMass + personMass * randomNum;

            if (compareMass != train.TotalMass) // error
            {
                string error = train.ToString() + " did not calculate mass correctly.";
                messages.Add(error);
                result = false;
            }

            train.NumPassengers = 0; // reset

            return result; // all passed
        }

        private bool TestAddPassengers(List<string> messages)
        {
            bool result = true;

            Train train = new Train(0, startBlock, _environment);

            // correct add passengers
            train.NumPassengers = 25;
            train.NumCrew = 10;

            if ((train.NumPassengers != 25) || (train.NumCrew != 10)) // error
            {
                string error = train.ToString() + " did not add";
                if (train.NumPassengers != 25)
                    error += " passengers,";
                if (train.NumCrew != 10)
                    error += " crew";

                error += " correctly.";
                messages.Add(error);
                result = false;
            }

            // incorrect negative add passengers
            train.NumPassengers = -25;
            train.NumCrew = -10;

            if ((train.NumPassengers != 25) || (train.NumCrew != 10)) // error
            {
                string error = train.ToString() + " set";
                if (train.NumPassengers == -25)
                    error += " passengers,";
                if (train.NumCrew == -10)
                    error += " crew";

                error += " to negative number.";
                messages.Add(error);
                result = false;
            }

            // incorrect too many passengers
            train.NumPassengers = 500;
            train.NumCrew = 400;

            if ((train.NumPassengers != 25) || (train.NumCrew != 10)) // error
            {
                string error = train.ToString() + " set";
                if (train.NumPassengers == -25)
                    error += " passengers,";
                if (train.NumCrew == -10)
                    error += " crew";

                error += " over maximum capacity.";
                messages.Add(error);
                result = false;
            }

            // reset
            train.NumPassengers = 0;
            train.NumCrew = 0;

            return result;
        }

        private bool TestRemovePassengers(List<string> messages)
        {
            bool result = true;

            Train train = new Train(0, startBlock, _environment);
            train.NumPassengers = 25;
            train.NumCrew = 10;

            // remove passengers
            train.NumPassengers = 10;
            train.NumCrew = 0;

            if ((train.NumPassengers != 10) || (train.NumCrew != 0)) // error
            {
                string error = train.ToString() + " did not remove";
                if (train.NumPassengers == -25)
                    error += " passengers,";
                if (train.NumCrew == -10)
                    error += " crew";

                error += " correctly.";
                messages.Add(error);
                result = false;
            }

            // reset
            train.NumPassengers = 0;
            train.NumCrew = 0;

            return result;
        }

        private bool TestLights(List<string> messages)
        {
            Train train = new Train(0, startBlock, _environment);
            train.LightsOn = true;

            if (train.LightsOn == false) // error
            {
                string error = train.ToString() + " did not turn the lights on correctly.";
                messages.Add(error);
                return false;
            }

            train.LightsOn = false;

            if (train.LightsOn == true) // error
            {
                string error = train.ToString() + " did not turn the lights off correctly.";
                messages.Add(error);
                return false;
            }

            return true;
        }

        private bool TestDoors(List<string> messages)
        {
            Train train = new Train(0, startBlock, _environment);
            train.DoorsOpen = true;

            if (train.DoorsOpen == false) // error
            {
                string error = train.ToString() + " did not open doors correctly.";
                messages.Add(error);
                return false;
            }

            train.DoorsOpen = false;

            if (train.DoorsOpen == true) // error
            {
                string error = train.ToString() + " did not close doors correctly.";
                messages.Add(error);
                return false;
            }

            return true;
        }

        private bool TestTemperature(List<string> messages)
        {
            Train train = new Train(0, startBlock, _environment);
            train.Temperature = 70;

            if (train.Temperature != 70) // error
            {
                string error = train.ToString() + " did not set temperature correctly.";
                messages.Add(error);
                return false;
            }

            return true;
        }

        private bool TestEmergencyBrake(List<string> messages)
        {
            Train train = new Train(0, startBlock, _environment);

            train.ChangeMovement(200);

            // allow 10 iterations of update movement
            for (int i = 0; i < 10; i++)
            {
                train.updateMovement();
            }

            train.EmergencyBrake();
            train.updateMovement();

            if (train.CurrentAcceleration != -2.73) // error
            {
                string error = train.ToString() + " emergency brake did not set maximum deceleration correctly.";
                messages.Add(error);
                return false;
            }
            return true;
        }

        private bool TestMovement_NoGrade(List<string> messages)
        {
            Train train = new Train(0, startBlock, _environment);

            train.ChangeMovement(200); // defaults to 0.5 because of zero grade
            train.updateMovement();

            if (train.CurrentAcceleration != 0.5) // error
            {
                string error = train.ToString() + " acceleration did not default to 0.5.";
                messages.Add(error);
                return false;
            }
            return true;
        }

        private bool TestMovement_PositiveGrade(List<string> messages)
        {
            bool result = true;

            Block oldStart = startBlock;
            startBlock = new Block(0, StateEnum.Healthy, 0, 0, 0.01, new[] { 0, 0 }, 100000, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            blocks[0] = startBlock;
            Train train = new Train(0, startBlock, _environment);

            train.ChangeMovement(200);
            train.updateMovement(); // should be less than 0.5

            if (train.CurrentAcceleration >= 0.5) // error
            {
                string error = train.ToString() + " did not lose any acceleration due to slope.";
                messages.Add(error);
                result = false;
            }

            // reset
            blocks[0] = oldStart;
            startBlock = oldStart;

            return result;
        }

        private bool TestMovement_NegativeGrade(List<string> messages)
        {
            bool result = true;

            Block oldStart = startBlock;
            startBlock = new Block(0, StateEnum.Healthy, 0, 0, -0.01, new[] { 0, 0 }, 100000, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            blocks[0] = startBlock;
            Train train = new Train(1, startBlock, _environment);

            train.ChangeMovement(200);

            // allow 10 iterations of update movement
            for (int i = 0; i < 8; i++)
            {
                train.updateMovement();
            }

            train.EmergencyBrake();
            train.updateMovement(); // should be greater than -2.73

            if (train.CurrentAcceleration == -2.73) // error
            {
                string error = train.ToString() + " did not gain any acceleration due to slope.";
                messages.Add(error);
                result = false;
            }

            // reset
            blocks[0] = oldStart;
            startBlock = oldStart;

            return result;
        }

        private bool TestBrakeFailureMovement(List<string> messages)
        {
            bool result = true;

            Train train = new Train(0, startBlock, _environment);
            train.BrakeFailure = true;
            if (train.ChangeMovement(-200)) // error, braked during brake failure
            {
                string error = train.ToString() + " applied brakes during brake failure.";
                messages.Add(error);
                result = false;
            }

            if (!train.ChangeMovement(200)) // error, unable to supply power during brake failure
            {
                string error = train.ToString() + " was not able to accelerate during brake failure.";
                messages.Add(error);
                result = false;
            }

            train.BrakeFailure = false; // reset

            return result;
        }

        private bool TestEngineFailureMovement(List<string> messages)
        {
            bool result = true;

            Train train = new Train(0, startBlock, _environment);
            train.EngineFailure = true;
            if (train.ChangeMovement(200)) // error, changed movement during engine failure
            {
                string error = train.ToString() + " applied power during engine failure.";
                messages.Add(error);
                result = false;
            }

            if (train.ChangeMovement(-200)) // error, able to brake during engine failure
            {
                string error = train.ToString() + " applied brake during engine failure.";
                messages.Add(error);
                result = false;
            }

            train.EngineFailure = false; // reset

            return result;
        }

        internal class DummyTrackModel : ITrackModel
        {
            readonly List<IBlock> _blocks;

            public DummyTrackModel(List<IBlock> blocks)
            {
                _blocks = blocks;

                RedLoaded = true;
                GreenLoaded = false;
            }

            public TrackChanged ChangeFlag { get; private set; }
            public IBlock requestBlockInfo(int blockID, string line)
            {
                return _blocks.First(x => x.BlockID == blockID);
            }

            public IRouteInfo requestRouteInfo(int routeID)
            {
                throw new NotImplementedException();
            }

            public event EventHandler<EventArgs> TrackChangedEvent;
            public IBlock[] requestPath(int startBlockID, int endBlockID, string line)
            {
                return _blocks.Where(x => x.BlockID >= startBlockID && x.BlockID <= endBlockID).ToArray();
            }

            public IBlock[,] requestTrackGrid(int routeID)
            {
                throw new NotImplementedException();
            }

            public bool requestUpdateSwitch(IBlock bToUpdate)
            {
                throw new NotImplementedException();
            }

            public bool requestUpdateBlock(IBlock blockToChange)
            {
                for (var i = 0; i < _blocks.Count; i++)
                {
                    if (_blocks[i].BlockID == blockToChange.BlockID)
                        _blocks[i] = blockToChange;
                }
                return true;
            }

            public bool RedLoaded { get; private set; }
            public bool GreenLoaded { get; private set; }
        }
    }
}