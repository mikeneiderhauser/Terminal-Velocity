using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

using System.Threading;
using Interfaces;
using Utility;

namespace Testing
{
    public class PrototypeDemo
    {
        SimulationEnvironment.SimulationEnvironment _env;
        TrackModel.TrackModel _trackMod;
        TrackController.TrackCircuit _currCircuit;
        TrackController.TrackCircuit _nextCircuit;
        TrackController.TrackCircuit _prevCircuit;
        TrackController.TrackController _prev;
        TrackController.TrackController _curr;
        TrackController.TrackController _next;
        CTCOffice.CTCOffice _office;
        SystemScheduler.SystemScheduler _scheduler;


        public PrototypeDemo()
        {
            //Underlying Classes

            /*
            Thread t1 = new Thread(new ThreadStart(this.createEnvironment));
            t1.Start();
            Thread t2 = new Thread(new ThreadStart(this.createTrackModel));
            t2.Start();
            Thread t3 = new Thread(new ThreadStart(this.createTrackController));
            t3.Start();
            Thread t4 = new Thread(new ThreadStart(this.createCTCOffice));
            t4.Start();
            Thread t5 = new Thread(new ThreadStart(this.createSystemScheduler));
            t5.Start();
             */

            createEnvironment();
            createTrackModel();
            createTrackController();
            createCTCOffice();
            createSystemScheduler();


            Thread t6 = new Thread(new ThreadStart(this.createTrackForm));
            t6.Start();
            Thread t7 = new Thread(new ThreadStart(this.createTrackControllerForm));
            t7.Start();
            Thread t8 = new Thread(new ThreadStart(this.createCTCOfficeForm));
            t8.Start();
            Thread t9 = new Thread(new ThreadStart(this.createTrainModelForm));
            t9.Start();
            Thread t10 = new Thread(new ThreadStart(this.createSystemSchedulerForm));
            t10.Start();

        }


        #region Framework Creation Methods
        public void createEnvironment()
        {
            // Environment object
            _env = new SimulationEnvironment.SimulationEnvironment();
        }

        public void createTrackModel()
        {
            //Create TrackModel
            _trackMod = new TrackModel.TrackModel(_env);
            //Let TrackModel read in the lines before you proceed..shouldnt be done this way, but needed to stop CTC Office from faulting 
            bool Proto_res = _trackMod.provideInputFile(@"..\..\Resources\red.csv");
            //Console.WriteLine("Res was "+res);
            Proto_res = _trackMod.provideInputFile(@"..\..\Resources\green.csv");
            //Console.WriteLine("Res was " + res);
            _env.TrackModel = _trackMod;
        }

        public void createTrackController()
        {
            IBlock b0 = new TrackModel.Block(0, StateEnum.Healthy, -1, 0, 0, new int[] { 0, 0 }, 1000, DirEnum.East, new string[] { "" }, 0, 0, 0, "Green");
            IBlock b1 = new TrackModel.Block(1, StateEnum.Healthy, 0, 0, 0, new int[] { 1, 1 }, 1000, DirEnum.East, new string[] { "" }, 0, 0, 0, "Green");
            IBlock b2 = new TrackModel.Block(2, StateEnum.Healthy, 1, 0, 0, new int[] { 2, 2 }, 1000, DirEnum.East, new string[] { "" }, 0, 0, 0, "Green");
            IBlock b3 = new TrackModel.Block(3, StateEnum.BrokenTrackFailure, 2, 0, 0, new int[] { 3, 3 }, 1000, DirEnum.East, new string[] { "" }, 0, 0, 0, "Green");

            List<IBlock> sectionA = new List<IBlock>();
            sectionA.Add(b0);
            List<IBlock> sectionB = new List<IBlock>();
            sectionB.Add(b1);
            sectionB.Add(b2);
            List<IBlock> sectionC = new List<IBlock>();
            sectionC.Add(b3);

            // Track Controller
            _currCircuit = new TrackController.TrackCircuit(_env, sectionA);
            // Next track controller's circuit
            _nextCircuit = new TrackController.TrackCircuit(_env, sectionB);
            // Previous track controller's circuit
            _prevCircuit = new TrackController.TrackCircuit(_env, sectionC);

            _prev = new TrackController.TrackController(_env, _prevCircuit);
            _curr = new TrackController.TrackController(_env, _currCircuit);
            _next = new TrackController.TrackController(_env, _nextCircuit);


            _prev.Previous = null;
            _prev.Next = _curr;

            _curr.Previous = _prev;
            _curr.Next = _next;

            _next.Previous = _curr;
            _next.Next = null;

            _env.PrimaryTrackControllerGreen = _prev;
            _env.PrimaryTrackControllerRed = _prev;
        }

        public void createCTCOffice()
        {
            // Assign the same track controller to both lines
            _office = new CTCOffice.CTCOffice(_env, _prev, _prev);

            _env.CTCOffice = _office;
        }

        private void createSystemScheduler()
        {
            _scheduler = new SystemScheduler.SystemScheduler(_env, _office);
        }
#endregion

        #region Gui Creation Methods
        public void createTrackForm()
        {
            Form formTrack = new Form();
            UserControl controlTrack;
            controlTrack = new TrackModel.TrackModelGUI(_env, _trackMod);

            formTrack.Controls.Add(controlTrack);
            formTrack.AutoSize = true;
            formTrack.Text = "Track Model";
            formTrack.ShowDialog();
        }

        public void createTrackControllerForm()
        {
            Form formTrackController = new Form();
            UserControl controlTrackController;
            controlTrackController = new TrackController.TrackControllerUI(_env);

            formTrackController.Text = "Track Controller";
            formTrackController.Controls.Add(controlTrackController);
            formTrackController.AutoSize = true;
            formTrackController.ShowDialog();
        }

        public void createCTCOfficeForm()
        {
            Form formCTC = new Form();
            CTCOffice.CTCOfficeGUI controlCTC;
            controlCTC = new CTCOffice.CTCOfficeGUI(_env, _office);

            controlCTC.ShowTrain += new EventHandler<CTCOffice.ShowTrainEventArgs>(controlCTC_ShowTrain);

            formCTC.Text = "CTC Office";
            formCTC.Controls.Add(controlCTC);
            formCTC.AutoSize = true;
            formCTC.ShowDialog();
        }

        void controlCTC_ShowTrain(object sender, CTCOffice.ShowTrainEventArgs e)
        {
            Form formTrainController = new Form();
            UserControl controlTrainController = null;
            TrainController.TrainController tc = (TrainController.TrainController)e.TrainModel.TrainController;
            controlTrainController = new TrainController.TrainControllerUI(tc, _env);
            formTrainController.Text = "Train Controller";
            formTrainController.Controls.Add(controlTrainController);
            formTrainController.AutoSize = true;
            formTrainController.ShowDialog();
        }

        public void createTrainModelForm()
        {
            Form formTrain = new Form();
            UserControl controlTrain;
            controlTrain = new TrainModel.TrainGUI(_env);

            formTrain.Text = "Train Model";
            formTrain.Controls.Add(controlTrain);
            formTrain.AutoSize = true;
            formTrain.ShowDialog();
        }

        public void createSystemSchedulerForm()
        {
            Form formScheduler = new Form();
            UserControl controlScheduler;
            controlScheduler = new SystemScheduler.SystemSchedulerGUI(_env, _scheduler, _office);
            formScheduler.Text = "System Scheduler";
            formScheduler.Controls.Add(controlScheduler);
            formScheduler.AutoSize = true;
            formScheduler.ShowDialog();
        }
        #endregion
    }//end protodemo class
}
