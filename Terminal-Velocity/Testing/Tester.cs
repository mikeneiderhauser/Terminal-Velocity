using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SystemScheduler;
using CTCOffice;
using Interfaces;
using TrackController;
using TrackModel;
using TrainController;
using TrainModel;

namespace Testing
{
    public class Tester
    {
        [STAThread]
        private static int Main(String[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("Tester.exe <type> [number]\n");
                Console.WriteLine("type        the type of test to run");
                Console.WriteLine(" -> unit    (to run unit tests)");
                Console.WriteLine(" -> gui     (to run gui tests)\n");
                Console.WriteLine("number      which gui to launch");
                Console.WriteLine(" ->  0      SystemScheduler");
                Console.WriteLine(" ->  1      CTCOffice");
                Console.WriteLine(" ->  2      TrackModel");
                Console.WriteLine(" ->  3      TrackController");
                Console.WriteLine(" ->  4      TrainModel");
                Console.WriteLine(" ->  5      TrainController");
                Console.WriteLine(" -> 10      PROTOTYPEDEMO");

                Console.WriteLine("\n\nPress enter to continue...");
                Console.ReadLine();
            }
                // Run Unit tests and GUI tests (currently only one)
            else if (args.Length == 2)
            {
                UnitTestFramework();

                if (args[0].CompareTo("gui") == 0)
                {
                    int test;
                    if (Int32.TryParse(args[1], out test))
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(GuiTestFramework(test));
                    }
                }
            }
                // Run only Unit tests
            else if (args.Length == 1)
            {
                if (args[0].CompareTo("unit") == 0)
                {
                    UnitTestFramework();

                    Console.WriteLine("\n\nPress enter to continue...");
                    Console.ReadLine();
                }
            }

            return 0;
        }

        private static Form GuiTestFramework(int test)
        {
            ////////////////////////////////////////////////////////////////////////////////////////
            //                              Initializations                                       //
            ////////////////////////////////////////////////////////////////////////////////////////


            // Environment object
            var environment = new SimulationEnvironment.SimulationEnvironment();

            IBlock b0 = new Block(1, StateEnum.Healthy, 0, 0, 0, new[] {0, 0}, 10, DirEnum.East, new[] {""}, 0, 0, 0, "Red",70);
            IBlock b1 = new Block(2, StateEnum.Healthy, 1, 0, 0, new[] {1, 1}, 10, DirEnum.East, new[] {""}, 0, 0, 0, "Red",70);
            IBlock b2 = new Block(3, StateEnum.Healthy, 2, 0, 0, new[] {2, 2}, 10, DirEnum.East, new[] {""}, 0, 0, 0, "Red",70);
            IBlock b3 = new Block(4, StateEnum.BrokenTrackFailure, 3, 0, 0, new[] {3, 3}, 10, DirEnum.East, new[] {""}, 0, 0, 0, "Red",70);

            var sectionA = new List<IBlock> {b0};
            var sectionB = new List<IBlock> {b1, b2};
            var sectionC = new List<IBlock> {b3};

            // Previous track controller's circuit
            var prevCircuit = new TrackCircuit(environment, sectionA);
            // Our track circuit
            var currCircuit = new TrackCircuit(environment, sectionB);
            // Next track controller's circuit
            var nextCircuit = new TrackCircuit(environment, sectionC);

            var prev = new TrackController.TrackController(environment, prevCircuit);
            var curr = new TrackController.TrackController(environment, currCircuit);
            var next = new TrackController.TrackController(environment, nextCircuit);

            //Create TrackModel
            var trackMod = new TrackModel.TrackModel(environment);
            //Let TrackModel read in the lines before you proceed..shouldnt be done this way, but needed to stop CTC Office from faulting 
            bool res = trackMod.provideInputFile("red.csv");
            //Console.WriteLine("Res was "+res);
            res = trackMod.provideInputFile("green.csv");
            //Console.WriteLine("Res was " + res);


            environment.TrackModel = trackMod;
            prev.Previous = null;
            prev.Next = curr;

            curr.Previous = prev;
            curr.Next = next;

            next.Previous = curr;
            next.Next = null;

            // Assign the same track controller to both lines
            var office = new CTCOffice.CTCOffice(environment, prev, prev);

            environment.CTCOffice = office;
            environment.PrimaryTrackControllerGreen = prev;
            environment.PrimaryTrackControllerRed = prev;


            ////////////////////////////////////////////////////////////////////////////////////////
            //                            End Initializations                                     //
            ////////////////////////////////////////////////////////////////////////////////////////

            var form = new Form();
            var control = new UserControl();
            switch (test)
            {
                case 0: // SystemScheduler

                    var testSystemScheduler = new SystemScheduler.SystemScheduler(environment, office);
                    control = new SystemSchedulerGUI(environment, testSystemScheduler, office);

                    break;
                case 1: // CTCOffice
                    //using all testing classes the ctc office (created a new instance of ctc)

                    //create environment instance
                    var env = new SimulationEnvironment.SimulationEnvironment();

                    //create testing track model
                    var tm = new TestingTrackModel(env);

                    //creating testing track controllers
                    var primaryRed = new TestingTrackController(0);
                    var primaryGreen = new TestingTrackController(1);

                    //hook to environment
                    env.PrimaryTrackControllerRed = primaryRed;
                    env.PrimaryTrackControllerGreen = primaryGreen;
                    env.TrackModel = tm;

                    //creating office instance
                    var ctc = new CTCOffice.CTCOffice(env, primaryRed, primaryGreen);

                    env.CTCOffice = ctc;

                    //making Request Panel Objects (For red and green)
                    var RequestRed = new RequestFrame("Red", primaryRed);
                    var RequestGreen = new RequestFrame("Green", primaryGreen);

                    //creating office gui
                    var CTCOfficeGUI = new CTCOfficeGUI(env, ctc);

                    var MyTestingControls = new TestingControls(tm);
                    //creating testing gui
                    control = new OfficeGUITest(
                        CTCOfficeGUI,
                        RequestRed,
                        RequestGreen,
                        MyTestingControls
                        );

                    //control = new CTCOffice.CTCOfficeGUI(environment, office);
                    break;
                case 2: // TrackModel
                    control = new TrackModelGUI(environment, trackMod);
                    break;
                case 3: // TrackController
                    ITrainModel t = new Train(0, b0, environment);

                    environment.AllTrains.Add(t);

                    prevCircuit.Trains.Add(0, t);

                    control = new TrackControllerUi(environment);
                    break;
                case 4: // TrainModel
                    var loc = new int[2];
                    loc[0] = 10;
                    loc[1] = 10;
                    var start = new Block(0, StateEnum.Healthy, 0, 0, -0.02, loc, 100, DirEnum.East, null, 1, 2, 0, "Red",70);
                    environment.addTrain(new Train(0, start, environment));
                    environment.addTrain(new Train(1, start, environment));

                    var train0 = (Train)environment.AllTrains[0];
                    train0.ChangeMovement(200);

                    control = new TrainGUI(environment);

                    break;
                case 5: // TrainController
                    var loc2 = new int[2];
                    loc2[0] = 10;
                    loc2[1] = 10;
                    var start2 = new Block(0, StateEnum.Healthy, 0, 0, 0, loc2, 100, DirEnum.East, null, 1, 2, 0, "Red",70);
                    var tc = new TrainController.TrainController(environment, new Train(0, start2, environment));
                    control = new TrainControllerUI(tc, environment);
                    break;
            }

            environment.startTick();

            form.Controls.Add(control);
            form.AutoSize = true;


            return form;
        }

        private static void UnitTestFramework()
        {
            int totalPass = 0;
            int totalFail = 0;
            int totalFatal = 0;

            Type type = typeof (ITesting);
            IEnumerable<Type> types =
                AppDomain.CurrentDomain.GetAssemblies()
                         .ToList()
                         .SelectMany(s => s.GetTypes())
                         .Where(p => type.IsAssignableFrom(p));
            foreach (Type t in types)
            {
                if (!t.IsInterface)
                {
                    Console.WriteLine("==================================================");
                    Console.WriteLine(string.Format("[{0}]", t));

                    object o = Activator.CreateInstance(t);
                    MethodInfo info = t.GetMethod("DoTest");
                    var parameters = new object[] {null, null, null};
                    var result = (Boolean) info.Invoke(o, parameters);

                    if (result)
                    {
                        var pass = (int) parameters[0];
                        var fail = (int) parameters[1];
                        var messages = (List<string>) parameters[2];

                        Console.WriteLine(string.Format("{0} tests passed", pass));
                        Console.WriteLine(string.Format("{0} non-fatal errors", fail));
                        foreach (string s in messages)
                            Console.WriteLine(string.Format("Message : {0}", s));

                        totalPass += pass;
                        totalFail += fail;
                    }
                    else
                    {
                        Console.WriteLine(string.Format("A fatal error has occured"));
                        totalFatal++;
                    }

                    Console.WriteLine("\n");
                }
            }

            Console.WriteLine("==================================================");
            Console.WriteLine("==================================================");
            Console.WriteLine("Testing completed.\n");
            Console.WriteLine(string.Format("Total Passed : {0} \nTotal Fail : {1} \nTotal Fatal Errors : {2}",
                                            totalPass, totalFail, totalFatal));
            Console.WriteLine("==================================================");
            Console.WriteLine("==================================================");
        }
    }
}