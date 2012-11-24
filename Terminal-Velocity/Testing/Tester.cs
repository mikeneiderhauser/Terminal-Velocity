﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

using Interfaces;

namespace Testing
{
    public class Tester
    {
        static int Main(String[] args)
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
                        GUITestFramework(test);
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

        static void GUITestFramework(int test)
        {
            ////////////////////////////////////////////////////////////////////////////////////////
            //                              Initializations                                       //
            ////////////////////////////////////////////////////////////////////////////////////////

            // Environment object
            SimulationEnvironment.SimulationEnvironment environment = new SimulationEnvironment.SimulationEnvironment();
            //TerminalVelocity.Environment environment = new TerminalVelocity.Environment();
            // Our track circuit
            TrackController.TrackCircuit currCircuit = new TrackController.TrackCircuit(environment);
            // Next track controller's circuit
            TrackController.TrackCircuit nextCircuit = new TrackController.TrackCircuit(environment);
            // Previous track controller's circuit
            TrackController.TrackCircuit prevCircuit = new TrackController.TrackCircuit(environment);

            TrackController.TrackController prev = new TrackController.TrackController(environment, currCircuit);
            TrackController.TrackController curr = new TrackController.TrackController(environment, currCircuit);
            TrackController.TrackController next = new TrackController.TrackController(environment, currCircuit);

            prev.Previous = null;
            prev.Next = curr;

            curr.Previous = prev;
            curr.Next = next;

            next.Previous = curr;
            next.Next = null;

            // Assign the same track controller to both lines
            CTCOffice.CTCOffice office = new CTCOffice.CTCOffice(environment, prev, prev);

            environment.CTCOffice = office;
            environment.PrimaryTrackControllerGreen = prev;
            environment.PrimaryTrackControllerRed = prev;

            ////////////////////////////////////////////////////////////////////////////////////////
            //                              Initializations                                       //
            ////////////////////////////////////////////////////////////////////////////////////////

            Form form = new Form();
            UserControl control = new UserControl();
            switch (test)
            {
                case 0: // SystemScheduler
                    break;
                case 1: // CTCOffice
                    //using all testing classes the ctc office (created a new instance of ctc)

                    //create environment instance
                    SimulationEnvironment.SimulationEnvironment env = new SimulationEnvironment.SimulationEnvironment();

                    //create testing track model
                    CTCOffice.TestingTrackModel tm = new CTCOffice.TestingTrackModel();

                    //creating testing track controllers
                    CTCOffice.TestingTrackController primaryRed = new CTCOffice.TestingTrackController(0);
                    CTCOffice.TestingTrackController primaryGreen = new CTCOffice.TestingTrackController(1);

                    //creating office instance
                    CTCOffice.CTCOffice ctc = new CTCOffice.CTCOffice(env, primaryRed, primaryGreen);

                    //creating testing system scheduler
                    CTCOffice.TestingSystemScheduler ss = new CTCOffice.TestingSystemScheduler();

                    env.CTCOffice = ctc;
                    env.PrimaryTrackControllerRed = primaryRed;
                    env.PrimaryTrackControllerGreen = primaryGreen;
                    env.TrackModel = tm;
                    env.SystemScheduler = ss;

                    //making Request Panel Objects (For red and green)
                    CTCOffice.RequestFrame RequestRed = new CTCOffice.RequestFrame("Red", primaryRed);
                    CTCOffice.RequestFrame RequestGreen = new CTCOffice.RequestFrame("Green", primaryGreen);

                    //creating office gui
                    CTCOffice.CTCOfficeGUI CTCOfficeGUI= new CTCOffice.CTCOfficeGUI(environment, ctc);

                    //creating testing gui
                    control = new CTCOffice.OfficeGUITest(
                        CTCOfficeGUI, 
                        RequestRed, 
                        RequestGreen
                        );
                    
                    //control = new CTCOffice.CTCOfficeGUI(environment, office);
                    break;
                case 2: // TrackModel
                    break;
                case 3: // TrackController
                    control = new TrackController.TrackControllerUI();
                    break;
                case 4: // TrainModel
                    break;
                case 5: // TrainController
                    break;
            }

            form.Controls.Add(control);
            form.AutoSize = true;
            form.ShowDialog();
        }

        static void UnitTestFramework()
        {
            int totalPass = 0;
            int totalFail = 0;
            int totalFatal = 0;

            var type = typeof(ITesting);
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p));
            foreach (Type t in types)
            {
                if (!t.IsInterface)
                {
                    Console.WriteLine("==================================================");
                    Console.WriteLine(string.Format("[{0}]", t.ToString()));

                    object o = Activator.CreateInstance(t);
                    MethodInfo info = t.GetMethod("DoTest");
                    object[] parameters = new object[] { null, null, null };
                    Boolean result = (Boolean)info.Invoke(o, parameters);

                    if (result)
                    {
                        int pass = (int)parameters[0];
                        int fail = (int)parameters[1];
                        List<string> messages = (List<string>)parameters[2];

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
                        totalFail++;
                    }

                    Console.WriteLine("\n");
                }
            }

            Console.WriteLine("==================================================");
            Console.WriteLine("==================================================");
            Console.WriteLine("Testing completed.\n");
            Console.WriteLine(string.Format("Total Passed : {0} \nTotal Fail : {1} \nTotal Fatal Errors : {2}", totalPass, totalFail, totalFatal));
            Console.WriteLine("==================================================");
            Console.WriteLine("==================================================");
        }
    }
}