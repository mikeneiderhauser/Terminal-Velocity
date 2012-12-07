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
            
            Block noGrade = new Block(1, StateEnum.Healthy, 0, 0, 0, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            Block withPositiveGrade = new Block(1, StateEnum.Healthy, 0, 0, 0.01, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            Block withNegativeGrade = new Block(1, StateEnum.Healthy, 0, 0, -0.01, new[] { 0, 0 }, 10, DirEnum.East, new[] { "" }, 0, 0, 0, "Red", 70);
            
            Train train_noGrade = new Train(0, noGrade, environment);
            Train train_posGrade = new Train(1, withPositiveGrade, environment);
            Train train_negGrade = new Train(2, withNegativeGrade, environment);

            return true;
        }

        private bool TestMass(Train train, List<string> messages)
        {
            double initialTrainMass = 40900; //kilograms
            double personMass = 90; //kilograms



            return true;
        }

        private bool TestAddPassengers(Train train, List<string> messages)
        {
            return true;
        }

        private bool TestRemovePassengers(Train train, List<string> messages)
        {
            return true;
        }

        private bool TestLights(Train train, List<string> messages)
        {
            return true;
        }

        private bool TestDoors(Train train, List<string> messages)
        {
            return true;
        }

        private bool TestTemperature(Train train, List<string> messages)
        {
            return true;
        }

        private bool TestMovement_PositiveGrade(Train train, List<string> messages)
        {
            return true;
        }

        private bool TestMovement_NegativeGrade(Train train, List<string> messages)
        {
            return true;
        }

        private bool TestMovement_Grade(Train train, List<string> messages)
        {
            return true;
        }

        private bool TestBrakeFailureMovement(Train train, List<string> messages)
        {
            return true;
        }

        private bool TestEngineFailureMovement(Train train, List<string> messages)
        {
            return true;
        }

    }
}