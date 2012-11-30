using System;
using System.Windows.Forms;
using Interfaces;

namespace CTCOffice
{
    public partial class SpeedTool : UserControl
    {
        private readonly ISimulationEnvironment _env;
        private CTCOffice _ctc;
        private CTCOfficeGUI _ctcGui;

        public SpeedTool(CTCOfficeGUI ctcgui, CTCOffice ctc, ISimulationEnvironment env)
        {
            InitializeComponent();
            _ctcGui = ctcgui;
            _ctc = ctc;
            _env = env;
        }

        public event EventHandler<SpeedToolEventArgs> SubmitSpeed;

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