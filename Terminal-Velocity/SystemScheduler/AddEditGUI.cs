using System;
using System.Windows.Forms;
using Interfaces;

namespace SystemScheduler
{
    public partial class AddEditGUI : Form
    {
        # region Private Variables

        private readonly DispatchDatabase _dispatchDatabase;
        private readonly string[] _incomingData;
        private readonly bool _isEdit;
        private ISimulationEnvironment _environment;

        # endregion

        # region Constructor(s)

        public AddEditGUI(ISimulationEnvironment env, DispatchDatabase dispatchDatabase, bool isEdit, string[] data)
        {
            _environment = env;
            _dispatchDatabase = dispatchDatabase;
            _incomingData = data;
            _isEdit = isEdit;
            InitializeComponent();

            if (isEdit)
            {
                DateTime tempDateTime = DateTime.Parse(data[1]);
                cmbHour.SelectedIndex = (tempDateTime.Hour%12) - 1;
                cmbMinute.SelectedIndex = (tempDateTime.Minute/15);
                if (data[3].Equals("Red"))
                {
                    cmbSelect.SelectedIndex = 0;
                }
                else
                {
                    cmbSelect.SelectedIndex = 1;
                }
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
                    txtCustom.Text = data[4];
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
            if (((cmbHour.SelectedIndex != -1) && (cmbMinute.SelectedIndex != -1) && (cmbAMPM.SelectedIndex != -1)) &&
                (rdbCustom.Checked || rdbDefined.Checked) && (cmbSelect.SelectedIndex != -1))
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }

        private void AddNewDispatch(string balls)
        {
            if (rdbCustom.Checked)
            {
                _dispatchDatabase.AddDispatch(balls,
                                              "11/27/2012 " + (string) cmbHour.SelectedItem + ":" +
                                              (string) cmbMinute.SelectedItem + ":00 " + (string) cmbAMPM.SelectedItem,
                                              "0", (string) cmbSelect.SelectedItem, txtCustom.Text);
            }
            else
            {
                _dispatchDatabase.AddDispatch(balls,
                                              "11/27/2012 " + (string) cmbHour.SelectedItem + ":" +
                                              (string) cmbMinute.SelectedItem + ":00 " + (string) cmbAMPM.SelectedItem,
                                              "1", (string) cmbSelect.SelectedItem, cmbSelect.SelectedIndex.ToString());
            }
        }

        # endregion

        # region Events

        private void rdbDefined_CheckedChanged(object sender, EventArgs e)
        {
            txtCustom.Enabled = false;
            txtCustom.Text = "";
            checkOKEnable();
        }

        private void rdbCustom_CheckedChanged(object sender, EventArgs e)
        {
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_isEdit)
            {
                _dispatchDatabase.RemoveDispatch(int.Parse(_incomingData[0]));
                AddNewDispatch(_incomingData[0]);
            }
            else
            {
                AddNewDispatch("-1");
            }
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