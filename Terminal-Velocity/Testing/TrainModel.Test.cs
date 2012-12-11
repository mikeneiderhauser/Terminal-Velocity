using System.Collections.Generic;
using Interfaces;
using TrackModel;
using TrainModel;
using System;

namespace Testing
{
    public class TrainModelTest : ITesting
    {
        ISimulationEnvironment _environment;

        public bool DoTest(out int pass, out int fail, out List<string> message)
        {
            pass = 0;
            fail = 0;
            message = new List<string>();

            _environment = new SimulationEnvironment.SimulationEnvironment();
            
            Block noGrade = new Block(1, StateEnum.Healthy, 0, 0, 0, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            Block withPositiveGrade = new Block(1, StateEnum.Healthy, 0, 0, 0.01, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            Block withNegativeGrade = new Block(1, StateEnum.Healthy, 0, 0, -0.01, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            
            Train train_noGrade = new Train(0, noGrade, _environment);
            Train train_posGrade = new Train(1, withPositiveGrade, _environment);
            Train train_negGrade = new Train(2, withNegativeGrade, _environment);

            List<Train> testTrains = new List<Train>();
            
            testTrains.Add(train_noGrade);
            testTrains.Add(train_posGrade);
            testTrains.Add(train_negGrade);

            // mass test
            if (TestMass(testTrains, message))
                pass++;
            else
                fail++;

            // adding passengers test
            if (TestAddPassengers(testTrains, message))
                pass++;
            else
                fail++;

            // removing passengers test
            if (TestRemovePassengers(testTrains, message))
                pass++;
            else
                fail++;

            // lights test
            if (TestLights(testTrains, message))
                pass++;
            else
                fail++;

            // doors test
            if (TestDoors(testTrains, message))
                pass++;
            else
                fail++;

            // changing temperature test
            if (TestTemperature(testTrains, message))
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
            if (TestBrakeFailureMovement(testTrains, message))
                pass++;
            else
                fail++;

            // test acceleration during engine failure
            if (TestEngineFailureMovement(testTrains, message))
                pass++;
            else
                fail++;

            return true;
        }

        private bool TestMass(List<Train> testTrains, List<string> messages)
        {
            double initialTrainMass = 40900; //kilograms
            double personMass = 90; //kilograms
            Random random = new Random();

            foreach (Train train in testTrains)
            {
                int randomNum = random.Next(0, 223); // 0 to 222 passengers
                train.NumPassengers = randomNum;

                double compareMass = initialTrainMass + personMass * randomNum;

                if (compareMass != train.TotalMass) // error
                {
                    string error = train.ToString() + " did not calculate mass correctly.";
                    messages.Add(error);
                    return false;
                }

                train.NumPassengers = 0; // reset
            }

            return true; // all passed
        }

        private bool TestAddPassengers(List<Train> testTrains, List<string> messages)
        {
            foreach (Train train in testTrains)
            {
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
                    return false;
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
                    return false;
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
                    return false;
                }

                // reset
                train.NumPassengers = 0;
                train.NumCrew = 0;
            }

            return true;
        }

        private bool TestRemovePassengers(List<Train> testTrains, List<string> messages)
        {
            foreach (Train train in testTrains)
            {
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
                    return false;
                }

                // reset
                train.NumPassengers = 0;
                train.NumCrew = 0;
            }

            return true;
        }

        private bool TestLights(List<Train> testTrains, List<string> messages)
        {
            foreach (Train train in testTrains)
            {
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
            }

            return true;
        }

        private bool TestDoors(List<Train> testTrains, List<string> messages)
        {
            foreach (Train train in testTrains)
            {
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
            }

            return true;
        }

        private bool TestTemperature(List<Train> testTrains, List<string> messages)
        {
            foreach (Train train in testTrains)
            {
                train.Temperature = 70;

                if (train.Temperature != 70) // error
                {
                    string error = train.ToString() + " did not set temperature correctly.";
                    messages.Add(error);
                    return false;
                }
            }

            return true;
        }

        private bool TestEmergencyBrake(List<string> messages)
        {
            Block noGrade = new Block(1, StateEnum.Healthy, 0, 0, 0, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            Train train = new Train(0, noGrade, _environment);

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
            Block noGrade = new Block(1, StateEnum.Healthy, 0, 0, 0, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            Train train = new Train(0, noGrade, _environment);

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
            Block withPositiveGrade = new Block(1, StateEnum.Healthy, 0, 0, 0.01, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            Train train = new Train(1, withPositiveGrade, _environment);

            train.ChangeMovement(200);
            train.updateMovement(); // should be less than 0.5

            if (train.CurrentAcceleration >= 0.5) // error
            {
                string error = train.ToString() + " did not lose any acceleration due to slope.";
                messages.Add(error);
                return false;
            }

            return true;
        }

        private bool TestMovement_NegativeGrade(List<string> messages)
        {
            Block withNegativeGrade = new Block(1, StateEnum.Healthy, 0, 0, -0.01, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            Train train = new Train(2, withNegativeGrade, _environment);

            train.ChangeMovement(200);

            // allow 10 iterations of update movement
            for (int i = 0; i < 10; i++)
            {
                train.updateMovement();
            }

            train.EmergencyBrake();
            train.updateMovement(); // should be greater than -2.73

            if (train.CurrentAcceleration == -2.73) // error
            {
                string error = train.ToString() + " did not gain any acceleration due to slope.";
                messages.Add(error);
                return false;
            }

            return true;
        }

        private bool TestBrakeFailureMovement(List<Train> testTrains, List<string> messages)
        {
            foreach (Train train in testTrains)
            {
                train.BrakeFailure = true;
                if (train.ChangeMovement(-200)) // error, braked during brake failure
                {
                    string error = train.ToString() + " applied brakes during brake failure.";
                    messages.Add(error);
                    return false;
                }

                if (!train.ChangeMovement(200)) // error, unable to supply power during brake failure
                {
                    string error = train.ToString() + " was not able to accelerate during brake failure.";
                    messages.Add(error);
                    return false;
                }

                train.BrakeFailure = false; // reset
            }
            return true;
        }

        private bool TestEngineFailureMovement(List<Train> testTrains, List<string> messages)
        {
            foreach (Train train in testTrains)
            {
                train.EngineFailure = true;
                if (train.ChangeMovement(200)) // error, changed movement during engine failure
                {
                    string error = train.ToString() + " applied power during engine failure.";
                    messages.Add(error);
                    return false;
                }

                if (train.ChangeMovement(-200)) // error, able to brake during engine failure
                {
                    string error = train.ToString() + " applied brake during engine failure.";
                    messages.Add(error);
                    return false;
                }

                train.EngineFailure = false; // reset
            }
            return true;
        }

    }
}