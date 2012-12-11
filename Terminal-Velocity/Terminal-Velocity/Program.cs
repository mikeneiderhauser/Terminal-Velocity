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
        static Form _ctcForm;
        static Form _schedulerForm;
        static Form _trackModelForm;
        static Form _redTrackControllerForm;
        static Form _greenTrackControllerForm;

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

            e.TrackModel = trackModel;

            // CTCOffice
            CTCOffice.CTCOffice ctcOffice = new CTCOffice.CTCOffice(e, e.PrimaryTrackControllerRed, e.PrimaryTrackControllerGreen);
            CTCOffice.CTCOfficeGUI ctcOfficeGui = new CTCOfficeGUI(e, ctcOffice);

            // Scheduler
            SystemScheduler.SystemScheduler scheduler = new SystemScheduler.SystemScheduler(e, ctcOffice);
            SystemScheduler.SystemSchedulerGUI schedulerGui = new SystemScheduler.SystemSchedulerGUI(e, scheduler, ctcOffice);

            // Setup environment
            e.SystemScheduler = scheduler;

            // TrackControllerUI
            if (e.PrimaryTrackControllerRed != null)
            {
                TrackControllerUi redTrackControllerGui = new TrackControllerUi(e, e.PrimaryTrackControllerRed);
                _redTrackControllerForm = new Form() { Controls = { redTrackControllerGui }, TopLevel = true, AutoSize = true, Parent = null };
            }
            if (e.PrimaryTrackControllerGreen != null)
            {
                TrackControllerUi greenTrackControllerGui = new TrackControllerUi(e, e.PrimaryTrackControllerGreen);
                _greenTrackControllerForm = new Form() { Controls = { greenTrackControllerGui }, TopLevel = true, AutoSize = true, Parent = null };
            }

            _ctcForm = new Form() { Controls = { ctcOfficeGui }, AutoSize = true };
            _schedulerForm = new Form() { Controls = { schedulerGui }, TopLevel = true, AutoSize = true, Parent = null };
            _trackModelForm = new Form() { Controls = { trackModelGui }, TopLevel = true, AutoSize = true, Parent = null };
           
            

            _ctcForm.Shown += new EventHandler(CtcFormShown);

            Application.Run(_ctcForm);
        }

        static void CtcFormShown(object sender, EventArgs e)
        {
            //schedulerForm.ShowDialog(ctcForm);
            _trackModelForm.Show();

            if (_redTrackControllerForm != null)
                _redTrackControllerForm.Show();
            if (_greenTrackControllerForm != null)
                _greenTrackControllerForm.Show();
        }
    }
}