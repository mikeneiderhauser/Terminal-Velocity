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
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Environment
            SimulationEnvironment.SimulationEnvironment e = new SimulationEnvironment.SimulationEnvironment();
            
            // TrackModel
            TrackModel.TrackModel trackModel = new TrackModel.TrackModel(e);
            TrackModel.TrackModelGUI trackModelGui = new TrackModelGUI(e, trackModel);

            // CTCOffice
            CTCOffice.CTCOffice ctcOffice = new CTCOffice.CTCOffice(e, e.PrimaryTrackControllerRed, e.PrimaryTrackControllerGreen);
            CTCOffice.CTCOfficeGUI ctcOfficeGui = new CTCOfficeGUI(e, ctcOffice);

            // Scheduler
            SystemScheduler.SystemScheduler scheduler = new SystemScheduler.SystemScheduler(e, ctcOffice);
            SystemScheduler.SystemSchedulerGUI schedulerGui = new SystemScheduler.SystemSchedulerGUI(e, scheduler, ctcOffice);

            Form ctcForm = new Form() { Controls = { ctcOfficeGui }, AutoSize = true };
            Form schedulerForm = new Form() { Controls = { schedulerGui }, TopLevel = false, Parent = ctcForm };
            Form trackModelForm = new Form() { Controls = { trackModelGui }, TopLevel = false, Parent = ctcForm };

            Application.Run(ctcForm);
        }
    }
}