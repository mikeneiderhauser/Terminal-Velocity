using System;
using System.Collections.Generic;
using System.Threading;
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
    public class PrototypeDemo
    {
        private TrackController.TrackController _curr;
        private TrackCircuit _currCircuit;
        private SimulationEnvironment.SimulationEnvironment _env;
        private TrackController.TrackController _next;
        private TrackCircuit _nextCircuit;
        private CTCOffice.CTCOffice _office;
        private TrackController.TrackController _prev;
        private TrackCircuit _prevCircuit;

        private Form _scheduleHook;
        private SystemScheduler.SystemScheduler _scheduler;
        private SystemSchedulerGUI _ssGUI;
        private TrackModel.TrackModel _trackMod;


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

            //TrainModel.Train t = new TrainModel.Train(999,_env.TrackModel.requestBlockInfo(0,"Red"), _env);
//_env.addTrain(t);

            var t6 = new Thread(createTrackForm);
            t6.Start();
            var t7 = new Thread(createTrackControllerForm);
            t7.Start();
            var t8 = new Thread(createCTCOfficeForm);
            t8.Start();
            var t9 = new Thread(createTrainModelForm);
            t9.Start();
            var t10 = new Thread(createSystemSchedulerForm);
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
            IBlock b0 = new Block(0, StateEnum.Healthy, -1, 0, 0, new[] {0, 0}, 1000, DirEnum.East, new[] {""}, 0, 0, 0,
                                  "Green");
            IBlock b1 = new Block(1, StateEnum.Healthy, 0, 0, 0, new[] {1, 1}, 1000, DirEnum.East, new[] {""}, 0, 0, 0,
                                  "Green");
            IBlock b2 = new Block(2, StateEnum.Healthy, 1, 0, 0, new[] {2, 2}, 1000, DirEnum.East, new[] {""}, 0, 0, 0,
                                  "Green");
            IBlock b3 = new Block(3, StateEnum.BrokenTrackFailure, 2, 0, 0, new[] {3, 3}, 1000, DirEnum.East, new[] {""},
                                  0, 0, 0, "Green");

            var sectionA = new List<IBlock>();
            sectionA.Add(b0);
            var sectionB = new List<IBlock>();
            sectionB.Add(b1);
            sectionB.Add(b2);
            var sectionC = new List<IBlock>();
            sectionC.Add(b3);

            // Track Controller
            _currCircuit = new TrackCircuit(_env, sectionA);
            // Next track controller's circuit
            _nextCircuit = new TrackCircuit(_env, sectionB);
            // Previous track controller's circuit
            _prevCircuit = new TrackCircuit(_env, sectionC);

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
            var formTrack = new Form();
            UserControl controlTrack;
            controlTrack = new TrackModelGUI(_env, _trackMod);

            formTrack.Controls.Add(controlTrack);
            formTrack.AutoSize = true;
            formTrack.Text = "Track Model";
            formTrack.ShowDialog();
        }

        public void createTrackControllerForm()
        {
            var formTrackController = new Form();
            UserControl controlTrackController;
            controlTrackController = new TrackControllerUi(_env);

            formTrackController.Text = "Track Controller";
            formTrackController.Controls.Add(controlTrackController);
            formTrackController.AutoSize = true;
            formTrackController.ShowDialog();
        }

        public void createCTCOfficeForm()
        {
            var formCTC = new Form();
            CTCOfficeGUI controlCTC;
            controlCTC = new CTCOfficeGUI(_env, _office);

            controlCTC.ShowTrain += controlCTC_ShowTrain;
            controlCTC.ShowSchedule += controlCTC_ShowSchedule;

            formCTC.Text = "CTC Office";
            formCTC.Controls.Add(controlCTC);
            formCTC.AutoSize = true;
            formCTC.ShowDialog();
        }

        private void controlCTC_ShowSchedule(object sender, EventArgs e)
        {
            showSchedule();
        }

        private void showSchedule()
        {
            if (_scheduleHook.InvokeRequired)
            {
                _scheduleHook.BeginInvoke(new Action(showSchedule));
                return;
            }

            if (_scheduleHook != null)
            {
                _scheduleHook.TopMost = true;
                _scheduleHook.Show();
                _scheduleHook.TopMost = false;
            }
        }

        private void controlCTC_ShowTrain(object sender, ShowTrainEventArgs e)
        {
            var formTrainController = new Form();
            UserControl controlTrainController = null;
            var tc = (TrainController.TrainController) e.TrainModel.TrainController;
            controlTrainController = new TrainControllerUI(tc, _env);
            formTrainController.Text = "Train Controller";
            formTrainController.Controls.Add(controlTrainController);
            formTrainController.AutoSize = true;
            formTrainController.ShowDialog();
        }

        public void createTrainModelForm()
        {
            var formTrain = new Form();
            UserControl controlTrain;
            controlTrain = new TrainGUI(_env);

            formTrain.Text = "Train Model";
            formTrain.Controls.Add(controlTrain);
            formTrain.AutoSize = true;
            formTrain.ShowDialog();
        }

        public void createSystemSchedulerForm()
        {
            var formScheduler = new Form();
            UserControl controlScheduler;
            controlScheduler = new SystemSchedulerGUI(_env, _scheduler, _office);
            formScheduler.Text = "System Scheduler";
            formScheduler.Controls.Add(controlScheduler);
            formScheduler.AutoSize = true;
            _scheduleHook = formScheduler;
            formScheduler.ShowDialog();
        }

        #endregion
    }

//end protodemo class
}