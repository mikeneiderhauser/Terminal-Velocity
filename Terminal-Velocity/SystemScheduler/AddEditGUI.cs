# region Header

/*
 * Kent W. Nixon
 * Software Engineering
 * December 13, 2012
 */

# endregion

using System;
using System.Windows.Forms;
using Interfaces;

namespace SystemScheduler
{
    //This is the logical backend for the dispatch add/edit form
    public partial class AddEditGUI : Form
    {
        # region Private Variables

        //Only one dispatch database is read at a time
        private readonly DispatchDatabase _dispatchDatabase;

        //Incoming data from the main GUI representing an existing route
        private readonly string[] _incomingData;

        //If this dialog box was opened as an edit and not a new
        private readonly bool _isEdit;

        //Reference to the ever-present environment
        private ISimulationEnvironment _environment;

        # endregion

        # region Constructor(s)

        //Contructor for the add and edit dispatch GUI
        public AddEditGUI(ISimulationEnvironment env, DispatchDatabase dispatchDatabase, bool isEdit, string[] data)
        {
            //Store all incoming information to our globals
            _environment = env;
            _dispatchDatabase = dispatchDatabase;
            _incomingData = data;
            _isEdit = isEdit;

            //Load up the form
            InitializeComponent();

            //If this is an edit
            if (isEdit)
            {
                //Prepopulate the fields in the form with information

                //Time of dispatch
                DateTime tempDateTime = DateTime.Parse(data[1]);
                cmbHour.SelectedIndex = (tempDateTime.Hour%12) - 1;

                //Round to closest 15 minute interval
                cmbMinute.SelectedIndex = (tempDateTime.Minute/15);

                //Select the right line (green or red)
                if (data[3].Equals("Red"))
                {
                    cmbSelect.SelectedIndex = 0;
                }
                else
                {
                    cmbSelect.SelectedIndex = 1;
                }

                //Select AM or PM
                if (tempDateTime.Hour > 11)
                {
                    cmbAMPM.SelectedIndex = 1;
                }
                else
                {
                    cmbAMPM.SelectedIndex = 0;
                }

                //Select custom or predefined route
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

        //Method to determine if we should enable our OK button
        private void checkOKEnable()
        {
            //If there is at least some data in all fields of the form
            if (((cmbHour.SelectedIndex != -1) && (cmbMinute.SelectedIndex != -1) && (cmbAMPM.SelectedIndex != -1)) &&
                (rdbCustom.Checked || rdbDefined.Checked) && (cmbSelect.SelectedIndex != -1))
            {
                //Enable the button
                btnOK.Enabled = true;
            }
            else
            {
                //Otherwise, don't
                btnOK.Enabled = false;
            }
        }

        //Method to add a new dispatch to our dispatch database
        private void AddNewDispatch(string balls)
        {
            //If this is a custom route
            if (rdbCustom.Checked)
            {
                //Add everything as entered
                _dispatchDatabase.AddDispatch(balls,
                                              "11/27/2012 " + (string) cmbHour.SelectedItem + ":" +
                                              (string) cmbMinute.SelectedItem + ":00 " + (string) cmbAMPM.SelectedItem,
                                              "0", (string) cmbSelect.SelectedItem, txtCustom.Text);
            }
            //If it is a predefined route
            else
            {
                //List the route number as the only waypoint
                _dispatchDatabase.AddDispatch(balls,
                                              "11/27/2012 " + (string) cmbHour.SelectedItem + ":" +
                                              (string) cmbMinute.SelectedItem + ":00 " + (string) cmbAMPM.SelectedItem,
                                              "1", (string) cmbSelect.SelectedItem, cmbSelect.SelectedIndex.ToString());
            }
        }

        # endregion

        # region Events

        //When the "Defined" radio button is selected
        private void rdbDefined_CheckedChanged(object sender, EventArgs e)
        {
            //Disable the custom waypoint entry field
            txtCustom.Enabled = false;
            txtCustom.Text = "";

            //Check if we can enable OK
            checkOKEnable();
        }

        //When the "Custom" radio button is selected
        private void rdbCustom_CheckedChanged(object sender, EventArgs e)
        {
            //Enable the custom waypoint entry field
            txtCustom.Enabled = true;

            //Check if we can enable OK
            checkOKEnable();
        }

        //When a selection is made from the line combo box
        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check if we can enable OK
            checkOKEnable();
        }

        //When text is entered into the custom waypoint field
        private void txtCustom_TextChanged(object sender, EventArgs e)
        {
            //Check if we can enable OK
            checkOKEnable();
        }

        //When the OK button is clicked
        private void btnOK_Click(object sender, EventArgs e)
        {
            //Make sure the text entered into the custom waypoint field is correctly formatted before doing anything
            if (((rdbCustom.Checked == true) && (System.Text.RegularExpressions.Regex.IsMatch(txtCustom.Text, @"^[0-9](\|[0-9])*$"))) || (rdbCustom.Checked == false))
            {
                //If this was an edit of an existing route
                if (_isEdit)
                {
                    //Remove the old route from the database
                    _dispatchDatabase.RemoveDispatch(int.Parse(_incomingData[0]));

                    //Add the edited one as if it were a new one, but maintain its ID
                    AddNewDispatch(_incomingData[0]);
                }

                //If this was just a new route instead
                else
                {
                    //Just add it as a new route altogether
                    AddNewDispatch("-1");
                }

                //Return OK and close the dialog box
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            //If the text in the custom waypoint text field is improperly formatted
            else
            {
                //Display an error message to the user
                MessageBox.Show("Custom route information is improperly formatted.\n", "Error");
            }
        }

        //When an hour is selected from the combo box
        private void cmbHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check if we can enable OK
            checkOKEnable();
        }

        //When a minute is selected from the combo box
        private void cmbMinute_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check if we can enable OK
            checkOKEnable();
        }

        //When AM or PM was selected from the combo box
        private void cmbAMPM_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check if we can enable OK
            checkOKEnable();
        }

        # endregion
    }
}