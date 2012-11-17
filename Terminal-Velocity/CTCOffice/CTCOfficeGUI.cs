using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public partial class CTCOfficeGUI : UserControl
    {
        private IEnvironment _environment;
        private CTCOffice _ctcOffice;

        public CTCOfficeGUI(IEnvironment env, CTCOffice ctc)
        {
            InitializeComponent();
            _ctcOffice = ctc;
            _environment = env;

            //subscribe to Environment Tick
            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);
            _ctcOffice.Logout();
            _btnLoginLogout.Text = "Logout";
            
            mainDisplayLogo();            
            disableUserControls();
            _loginStatusImage.Image = Properties.Resources.red;
        }


        /// <summary>
        /// Function to handle Environment Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _environment_Tick(object sender, TickEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void _btnLoginLogout_Click(object sender, EventArgs e)
        {
            if (_ctcOffice.isAuth())
            {
                _ctcOffice.Logout();
                disableUserControls();
                _loginStatusImage.Image = Properties.Resources.red;
                _btnLoginLogout.Text = "Login";
            }
            else
            {
                if (_ctcOffice.Login(_txtUsername.Text, _txtPassword.Text))
                {
                    enableUserControls();
                    _loginStatusImage.Image = Properties.Resources.green;
                    _btnLoginLogout.Text = "Logout";
                }
                else
                {
                    disableUserControls();
                    _loginStatusImage.Image = Properties.Resources.red;
                    _btnLoginLogout.Text = "Login";
                    _environment.sendLogEntry("CTCOffice: Operator Logged Out!");
                }

            }
        }//end button LoginLogout

        private void _btnDispatchTrain_Click(object sender, EventArgs e)
        {

        }

        private void _btnRefreshView_Click(object sender, EventArgs e)
        {

        }

        private void _btnSchedule_1_Click(object sender, EventArgs e)
        {
            //show system scheduker
        }

        private void _btnSchedule_2_Click(object sender, EventArgs e)
        {
            //show system scheduler
        }

        private void _btnRefreshMetrics_Click(object sender, EventArgs e)
        {

        }

        private void _btnSpeed_Click(object sender, EventArgs e)
        {

        }

        private void _txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        private void _txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        private void _checkAutomatedScheduling_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkAutomatedScheduling.Checked)
            {
                _ctcOffice.StartScheduling();
            }
            else
            {
                _ctcOffice.StopScheduling();
            }
        }

        private void disableUserControls()
        {
            mainDisplayLogo();
            setControlState(false);
        }

        private void enableUserControls()
        {
            setControlState(true);
        }

        private void mainDisplayLogo()
        {
            _systemViewTabs.SelectedIndex = 2;//Terminal Velocity Tab
        }

        private void setControlState(bool state)
        {
            _btnDispatchTrain.Enabled = state;
            _btnSchedule_1.Enabled = state;
            _btnSchedule_2.Enabled = state;
            _btnRefreshView.Enabled = state;
            _btnRefreshMetrics.Enabled = state;
            _btnSpeed.Enabled = state;
            _checkAutomatedScheduling.Enabled = state;
            _systemViewTabs.Enabled = state;
        }
    }//end ctc gui
}
