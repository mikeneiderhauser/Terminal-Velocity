using System;
using System.Windows.Forms;

using SimulationEnvironment;
using CTCOffice;
using SystemScheduler;
using TrackController;
using TrackModel;
using TrainController;
using TrainModel;


namespace TerminalVelocity
{
    public class Program
    {
        static Form ctcForm;
        static Form schedulerForm;
        static Form trackModelForm;
        static Form trainModelForm;
        static SimulationEnvironment.SimulationEnvironment env;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Environment
            env = new SimulationEnvironment.SimulationEnvironment();
            
            // TrackModel
            TrackModel.TrackModel trackModel = new TrackModel.TrackModel(env);
            env.TrackModel = trackModel;
            TrackModel.TrackModelGUI trackModelGui = new TrackModelGUI(env, trackModel);

            // CTCOffice
            CTCOffice.CTCOffice ctcOffice = new CTCOffice.CTCOffice(env, env.PrimaryTrackControllerRed, env.PrimaryTrackControllerGreen);
            env.CTCOffice = ctcOffice;
            CTCOffice.CTCOfficeGUI ctcOfficeGui = new CTCOfficeGUI(env, ctcOffice);
            ctcOfficeGui.ShowTrain += new EventHandler<ShowTrainEventArgs>(ctcOfficeGui_ShowTrain);
            ctcOfficeGui.ShowSchedule += new EventHandler<EventArgs>(ctcOfficeGui_ShowSchedule);


            // Scheduler
            SystemScheduler.SystemScheduler scheduler = new SystemScheduler.SystemScheduler(env, ctcOffice);
            env.SystemScheduler = scheduler;
            SystemScheduler.SystemSchedulerGUI schedulerGui = new SystemScheduler.SystemSchedulerGUI(env, scheduler, ctcOffice);

            //train model form
            TrainModel.TrainGUI trainGui = new TrainGUI(env);

            ctcForm = new Form() { Controls = { ctcOfficeGui }, AutoSize = true, Text="Terminal Velocity - CTC Office"};
            schedulerForm = new Form() { Controls = { schedulerGui }, TopLevel = true, AutoSize = true, Parent = null, Text="Terminal Velocity - System Scheduler" };
            trackModelForm = new Form() { Controls = { trackModelGui }, TopLevel = true, AutoSize = true, Parent = null, Text="Terminal Velocity - Track Model"};
            trainModelForm = new Form() { Controls = { trainGui }, TopLevel = true, AutoSize = true, Parent = null, Text = "Terminal Velocity - Trains" };
            //TODO
            /*
             * Train Controller Form(s)
             * Train Model Form 
            */

            ctcForm.Shown += new EventHandler(ctcForm_Shown);

            Application.Run(ctcForm);
        }

        static void ctcOfficeGui_ShowSchedule(object sender, EventArgs e)
        {
            schedulerForm.ShowDialog();
        }

        static void ctcOfficeGui_ShowTrain(object sender, ShowTrainEventArgs e)
        {
            var formTrainController = new Form();
            UserControl controlTrainController = null;
            var tc = (TrainController.TrainController)e.TrainModel.TrainController;
            controlTrainController = new TrainControllerUI(tc,env);
            formTrainController.Text = "Terminal Velocity - Train Controller (ID:"+e.TrainModel.TrainID+")";
            formTrainController.Controls.Add(controlTrainController);
            formTrainController.AutoSize = true;
            formTrainController.ShowDialog();
        }

        static void ctcForm_Shown(object sender, EventArgs ea)
        {
            //start global timer
            env.StartTick();

            //schedulerForm.ShowDialog(ctcForm);
            trackModelForm.Show();

            trainModelForm.Show();
        }
    }
}