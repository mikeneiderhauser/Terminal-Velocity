using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;

namespace TrainController
{
    public partial class TrainControllerUI : UserControl
    {
        private TrainController _currentTrainController;
        private int timer;
        private Interfaces.ISimulationEnvironment _environment;

        public TrainControllerUI(TrainController tc, Interfaces.ISimulationEnvironment env)
        {
            InitializeComponent();
            _currentTrainController = tc;
            SpeedLimitInput.Text = tc.SpeedLimit.ToString();
            AuthorityLimitInput.Text = tc.AuthorityLimit.ToString();
            String[] announcements = { "0", "1", "2", "3" };
            AnnouncementComboBox.DataSource = announcements;
            AnnouncementComboBox.Enabled = false;
            timer = 0;

            

            _environment = env;
            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);
            
        }


        private void _environment_Tick(object sender, TickEventArgs e)
        {
            timer++;

            if (timer % 10 == 0)
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
            _currentTrainController.Temperature = Int16.TryParse(TemperatureInput.Text);
        

        }

        private void SpeedInput_Key_press(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
       && !char.IsDigit(e.KeyChar)
       && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void TemperatureInput_Keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
       && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void TrackControllerInput_CheckedChanged(object sender, EventArgs e)
        {
            if (TrackControllerInput.Checked)
            {
                SpeedLimitInput.Enabled = true;
                AuthorityLimitInput.Enabled = true;
                AnnouncementComboBox.Enabled = true;
                SubmitTrackButton.Enabled = true;
                SpeedInput.Enabled = false;
                TemperatureInput.Enabled = false;
                _btnDoorOpen.Enabled = false;
                _btnDoorClose.Enabled = false;
                AddPassengerButton.Enabled = false;
                RemovePassengersButton.Enabled = false;
                _btnSubmit.Enabled = false;


            }
            else
            {
                SpeedLimitInput.Enabled = false;
                AuthorityLimitInput.Enabled = false;
                AnnouncementComboBox.Enabled = false;
                SubmitTrackButton.Enabled = false;
                SpeedInput.Enabled = true;
                TemperatureInput.Enabled = true;
                _btnDoorOpen.Enabled = true;
                _btnDoorClose.Enabled = true;
                AddPassengerButton.Enabled = true;
                RemovePassengersButton.Enabled = true;
                _btnSubmit.Enabled = true;
            }
        }

        private void AddPassengerButton_Click(object sender, EventArgs e)
        {
            _currentTrainController.addPassengers();
        }

        private void RemovePassengersButton_Click(object sender, EventArgs e)
        {
            _currentTrainController.removePassengers();
        }

        private void SubmitTrackButton_Click(object sender, EventArgs e)
        {
            _currentTrainController.SpeedLimit = !SpeedLimitInput.Text.Equals("") ? Double.Parse(SpeedLimitInput.Text):_currentTrainController.SpeedLimit;
            _currentTrainController.AuthorityLimit = SpeedLimitInput.Text.Equals("") ? Int32.Parse(AuthorityLimitInput.Text):_currentTrainController.AuthorityLimit;
            _currentTrainController.Announcement = SpeedLimitInput.Text.Equals("") ? Int32.Parse(AnnouncementComboBox.SelectedValue.ToString()) : _currentTrainController.Announcement;
           

        }

        private void SpeedLimitInput_keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
       && !char.IsDigit(e.KeyChar)
       && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void AuthorityLimitInput_keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
      && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LightsOn_Click(object sender, EventArgs e)
        {

        }

       

      
    
    }
}
