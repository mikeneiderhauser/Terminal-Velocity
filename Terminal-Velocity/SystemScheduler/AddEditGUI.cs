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
    public partial class AddEditGUI : Form
    {

        # region Private Variables

        private DispatchDatabase _dispatchDatabase;
        private ISimulationEnvironment _environment;
        private string[] _incomingData;

        # endregion

        # region Constructor(s)

        public AddEditGUI(ISimulationEnvironment env, DispatchDatabase dispatchDatabase, bool isEdit, string[] data)
        {
            _environment = env;
            _dispatchDatabase = dispatchDatabase;
            _incomingData = data;
            InitializeComponent();

            if (isEdit)
            {
                DateTime tempDateTime = DateTime.Parse(data[1]);
                cmbHour.SelectedIndex = (tempDateTime.Hour % 12) - 1;
                cmbMinute.SelectedIndex = (int)(tempDateTime.Minute / 15);
                if (tempDateTime.Hour > 11)
                {
                    cmbAMPM.SelectedIndex = 1;
                }
                else
                {
                    cmbAMPM.SelectedIndex = 0;
                }
                if (int.Parse(data[2]) == 0)
                {
                    rdbDefined.Checked = false;
                    rdbCustom.Checked = true;
                    txtCustom.Text = data[3];
                }
                else
                {
                    rdbDefined.Checked = true;
                    rdbCustom.Checked = false;
                    cmbSelect.SelectedIndex = int.Parse(data[4]);
                }
            }
        }

        # endregion

        # region Private Methods

        private void checkOKEnable()
        {
            if (((cmbHour.SelectedIndex != -1) && (cmbMinute.SelectedIndex != -1) && (cmbAMPM.SelectedIndex != -1)) && (((rdbCustom.Checked == true) && (txtCustom.Text != "")) || ((rdbDefined.Checked == true) && (cmbSelect.SelectedIndex != -1))))
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }

        # endregion

        # region Events

        private void rdbDefined_CheckedChanged(object sender, EventArgs e)
        {
            txtCustom.Enabled = false;
            txtCustom.Text = "";
            cmbSelect.Enabled = true;
            checkOKEnable();
        }

        private void rdbCustom_CheckedChanged(object sender, EventArgs e)
        {
            cmbSelect.Enabled = false;
            cmbSelect.SelectedIndex = -1;
            txtCustom.Enabled = true;
            checkOKEnable();
        }

        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkOKEnable();
        }

        private void txtCustom_TextChanged(object sender, EventArgs e)
        {
            checkOKEnable();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //close the window?
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _dispatchDatabase.AddDispatch("20", "11/27/2012 " + (string)cmbHour.SelectedItem + ":" + (string)cmbMinute.SelectedItem + ":00 " + (string)cmbAMPM.SelectedItem, "0", txtCustom.Text);
        }

        private void cmbHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkOKEnable();
        }

        private void cmbMinute_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkOKEnable();
        }

        private void cmbAMPM_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkOKEnable();
        }

        # endregion

    }
}
