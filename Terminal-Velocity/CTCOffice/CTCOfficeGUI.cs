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

            //commit comment
            //subscribe to Environment Tick
            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);
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

        }

        private void _btnDispatchTrain_Click(object sender, EventArgs e)
        {

        }

        private void _btnRefreshView_Click(object sender, EventArgs e)
        {

        }

        private void _btnSchedule_1_Click(object sender, EventArgs e)
        {

        }

        private void _btnSchedule_2_Click(object sender, EventArgs e)
        {

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

        }
    }
}
