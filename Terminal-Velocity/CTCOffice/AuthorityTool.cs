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
    public partial class AuthorityTool : UserControl
    {
        private ISimulationEnvironment _env;
        private CTCOfficeGUI _ctcgui;
        private CTCOffice _ctc;

        public event EventHandler<AuthorityToolEventArgs> SubmitAuthority;
        public AuthorityTool(CTCOfficeGUI ctcgui, CTCOffice ctc, ISimulationEnvironment env)
        {
            InitializeComponent();
            _ctc = ctc;
            _ctcgui = ctcgui;
            _env = env;
        }

        private void _btnSubmit_Click(object sender, EventArgs e)
        {
            int speed = ValidateValue();
            if (speed >= 0)
            {
                if (SubmitAuthority != null)
                {
                    SubmitAuthority(this, new AuthorityToolEventArgs(speed));
                }
            }
        }

        private int ValidateValue()
        {
            int auth = -1;

            if (!Int32.TryParse(_txtAuthority.Text, out auth))
            {
                MessageBox.Show("Not a valid Authority. Please enter an integer value");
            }
            else
            {
                if (auth < 0)
                {
                    MessageBox.Show("Negative Authority Not Allowed");
                }
            }
            return auth;
        }
    }
}
