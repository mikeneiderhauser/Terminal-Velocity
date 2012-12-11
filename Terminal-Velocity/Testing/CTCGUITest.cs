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
    public class CTCGUITest
    {
        private SimulationEnvironment.SimulationEnvironment _env;
        private CTCOffice.TestingTrackModel _trackMod;
        private CTCOffice.CTCOffice _ctc;
        private UserControl _control;
        private CTCOffice.CTCOfficeGUI _ctcGui;
        private CTCOffice.TestingTrackController _red;
        private CTCOffice.TestingTrackController _green;
        private CTCOffice.RequestFrame _redRequest;
        private CTCOffice.RequestFrame _greenRequest;

        public CTCGUITest()
        {
            //using all testing classes the ctc office (created a new instance of ctc)

            //create environment instance
            _env = new SimulationEnvironment.SimulationEnvironment();

            //create testing track model
            _trackMod = new TestingTrackModel(_env);

            //creating testing track controllers
            _red = new TestingTrackController(0,_trackMod);
            _green = new TestingTrackController(1,_trackMod);

            //hook to environment
            _env.PrimaryTrackControllerRed = _red;
            _env.PrimaryTrackControllerGreen = _green;
            _env.TrackModel = _trackMod;

            //creating office instance
            _ctc = new CTCOffice.CTCOffice(_env, _red, _green);

            _env.CTCOffice = _ctc;
            _ctc.StartAutomation += new EventHandler<EventArgs>(_ctc_StartAutomation);
            _ctc.StopAutomation += new EventHandler<EventArgs>(_ctc_StopAutomation);

            //making Request Panel Objects (For red and green)
            _redRequest = new RequestFrame("Red", _red);
            _greenRequest = new RequestFrame("Green", _green);

            //creating office gui
            _ctcGui = new CTCOfficeGUI(_env, _ctc);
            _ctcGui.ShowSchedule += new EventHandler<EventArgs>(_ctcGui_ShowSchedule);


            var MyTestingControls = new TestingControls(_trackMod);
            //creating testing gui
            _control = new OfficeGUITest(
                _ctcGui,
                _redRequest,
                _greenRequest,
                MyTestingControls
                );

            _env.startTick();

            Form f = new Form();
            f.AutoSize = true;
            f.Text = "CTC Office Standalone GUI Test";
            f.Controls.Add(_control);
            f.Show();
        }

        void _ctcGui_ShowSchedule(object sender, EventArgs e)
        {
            MessageBox.Show("Scheduler Window");
        }

        void _ctc_StopAutomation(object sender, EventArgs e)
        {
            MessageBox.Show("Stop Scheduling");
        }

        void _ctc_StartAutomation(object sender, EventArgs e)
        {
            MessageBox.Show("Start Scheduling");
        }

    }
}