using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrainController
{
    public partial class TrainControllerUI : UserControl
    {
        private TrainController _currentTrainController;

        public TrainControllerUI(TrainController tc)
        {
            InitializeComponent();
            _currentTrainController = tc;
        }

        public void RecLog(string entry)
        {
            _listLog.Items.Add(entry);
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
