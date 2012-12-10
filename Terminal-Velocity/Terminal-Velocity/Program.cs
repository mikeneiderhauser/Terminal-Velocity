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
        static SimulationEnvironment.SimulationEnvironment e;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Environment
            e = new SimulationEnvironment.SimulationEnvironment();
            
            // TrackModel
            TrackModel.TrackModel trackModel = new TrackModel.TrackModel(e);
            e.TrackModel = trackModel;
            TrackModel.TrackModelGUI trackModelGui = new TrackModelGUI(e, trackModel);

            // CTCOffice
            CTCOffice.CTCOffice ctcOffice = new CTCOffice.CTCOffice(e, e.PrimaryTrackControllerRed, e.PrimaryTrackControllerGreen);
            e.CTCOffice = ctcOffice;
            CTCOffice.CTCOfficeGUI ctcOfficeGui = new CTCOfficeGUI(e, ctcOffice);

            // Scheduler
            SystemScheduler.SystemScheduler scheduler = new SystemScheduler.SystemScheduler(e, ctcOffice);
            e.SystemScheduler = scheduler;
            SystemScheduler.SystemSchedulerGUI schedulerGui = new SystemScheduler.SystemSchedulerGUI(e, scheduler, ctcOffice);

            ctcForm = new Form() { Controls = { ctcOfficeGui }, AutoSize = true };
            schedulerForm = new Form() { Controls = { schedulerGui }, TopLevel = true, AutoSize = true, Parent = null };
            trackModelForm = new Form() { Controls = { trackModelGui }, TopLevel = true, AutoSize = true, Parent = null };

            ctcForm.Shown += new EventHandler(ctcForm_Shown);

            Application.Run(ctcForm);
        }

        static void ctcForm_Shown(object sender, EventArgs ea)
        {
            //start global timer
            e.startTick();

            //schedulerForm.ShowDialog(ctcForm);
            trackModelForm.Show();
        }
    }
}