using System;
using System.Windows.Forms;
using Interfaces;
using Utility;

namespace TrainController
{
    public partial class TrainControllerUI : UserControl
    {
        private readonly TrainController _currentTrainController;
        private readonly ISimulationEnvironment _environment;
        private int timer;

        public TrainControllerUI(TrainController tc, ISimulationEnvironment env)
        {
            InitializeComponent();
            _currentTrainController = tc;


            timer = 0;


            _environment = env;
            _environment.Tick += _environment_Tick;
        }


        private void _environment_Tick(object sender, TickEventArgs e)
        {
            timer++;

            if (timer%10 == 0)
            {
                timer = 0;
                UpdateGUI(sender, e);
            }
        }

        private void UpdateGUI(object sender, TickEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, TickEventArgs>(UpdateGUI), sender, e);
                return;
            }


            _logOutput.Text = _currentTrainController.Log;
        }


        private void _btnEmergencyBrake_Click(object sender, EventArgs e)
        {
            _currentTrainController.EmergencyBrakes();
        }


        private void _btnDoorOpen_Click(object sender, EventArgs e)
        {
            _currentTrainController.doorOpen();
        }

        private void _btnDoorClose_Click(object sender, EventArgs e)
        {
            _currentTrainController.doorClose();
        }

        private void _btnSubmit_Click(object sender, EventArgs e)
        {
            _currentTrainController.SpeedInput = Double.Parse(SpeedInput.Text);
            _currentTrainController.Temperature = Int16.Parse(TemperatureInput.Text);
            SpeedInput.Text = "0.0";
            TemperatureInput.Text = "70";
        }
    }
}