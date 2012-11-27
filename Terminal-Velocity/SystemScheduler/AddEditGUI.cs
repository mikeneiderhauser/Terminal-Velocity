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

namespace SystemScheduler
{
    public partial class AddEditGUI : UserControl
    {
        private IDispatchDatabase _dispatchDatabase;
        private ISimulationEnvironment _environment;

        public AddEditGUI(ISimulationEnvironment env, IDispatchDatabase dispatchDatabase, int isEdit)
        {
            _environment = env;
            _dispatchDatabase = dispatchDatabase;
            InitializeComponent();
        }

        private void rdbDefined_CheckedChanged(object sender, EventArgs e)
        {
            txtCustom.Enabled = false;
            txtCustom.Text = "";
            btnOK.Enabled = false;
            cmbSelect.Enabled = true;
        }

        private void rdbCustom_CheckedChanged(object sender, EventArgs e)
        {
            cmbSelect.Enabled = false;
            cmbSelect.SelectedIndex = -1;
            btnOK.Enabled = false;
            txtCustom.Enabled = true;
        }

        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
        }

        private void txtCustom_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //close the window?
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _dispatchDatabase.DispatchList.Add(new Dispatch(_environment, cmbHour.SelectedText + cmbMinute.SelectedText, "20", "0", txtCustom.Text));
        }
    }
}
