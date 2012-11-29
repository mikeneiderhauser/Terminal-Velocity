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
    public partial class SpeedTool : UserControl
    {

        private ISimulationEnvironment _env;
        private CTCOfficeGUI _ctcGui;
        private CTCOffice _ctc;
        public event EventHandler<SpeedToolEventArgs> SubmitSpeed;

        public SpeedTool(CTCOfficeGUI ctcgui, CTCOffice ctc, ISimulationEnvironment env)
        {
            InitializeComponent();
            _ctcGui = ctcgui;
            _ctc = ctc;
            _env = env;
        }

        private void _txtSpeed_TextChanged(object sender, EventArgs e)
        {

        }

        private void _btnSubmit_Click(object sender, EventArgs e)
        {
            double speed = ValidateSpeed();
            if (speed == -1)
            {
                //invalid do nothing (message box should alread appear)
                _env.sendLogEntry("CTCOffice:SpeedTool: Operator inserted invalid Speed.");
                MessageBox.Show("Speed cannot be negative! Please enter a positive double.");
            }
            else
            {
                if (SubmitSpeed != null)
                {
                    SubmitSpeed(this, new SpeedToolEventArgs(speed));
                }
            }
        }

        private double ValidateSpeed()
        {
            double speed = -1;

            try
            {
                if (double.TryParse(_txtSpeed.Text, out speed))
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Speed!  Please enter a double for speed.");
                return -1;
            }

            return speed;
        }

        private void _lblUnits_Click(object sender, EventArgs e)
        {

        }
    }
}
