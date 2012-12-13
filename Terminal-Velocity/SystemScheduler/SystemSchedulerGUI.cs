# region Header

/*
 * Kent W. Nixon
 * Software Engineering
 * December 13, 2012
 */

# endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Interfaces;

namespace SystemScheduler
{
    //This is the backend logic for the system scheduler GUI
    public partial class SystemSchedulerGUI : UserControl
    {
        # region Private Variables

        //The ever-present environment
        private readonly ISimulationEnvironment _environment;

        //A reference to the system scheduler
        private readonly SystemScheduler _systemScheduler;

        //A reference to the CTC Office
        private ICTCOffice _ctcOffice;

        //The current time
        private DateTime _currentTime;

        # endregion

        #region Constructor(s)

        //The constructor for the GUI
        public SystemSchedulerGUI(ISimulationEnvironment env, SystemScheduler systemScheduler, ICTCOffice ctc)
        {
            //Get everything up and running on the form
            InitializeComponent();

            //Copy all of our incoming data to our globals
            _environment = env;
            _ctcOffice = ctc;
            _systemScheduler = systemScheduler;

            //Subscribed to the environment tick event
            //_environment.Tick += EnvironmentTickHandler;
        }

        # endregion

        # region Private Methods

        //Method for converting an incoming list to a DataTable, which can be used to map dispatches into our
        //DataGridView
        private DataTable ConvertListToDataTable(List<string[]> list)
        {

            //Create the new DataTables
            var table = new DataTable();

            //Set current columns to zero
            int columns = 0;

            //For each array in the incoming list
            foreach (var array in list)
            {
                //Make sure it's not longer than the number of columns we have
                if (array.Length > columns)
                {
                    //If so, increase our columns to match
                    columns = array.Length;
                }
            }

            //Create the headings for all of our columns
            table.Columns.Add("Dispatch ID", typeof(string));
            table.Columns.Add("Dispatch Time", typeof(string));
            table.Columns.Add("Dispatch Type", typeof(string));
            table.Columns.Add("Dispatch Line", typeof(string));
            table.Columns.Add("Dispatch Waypoints", typeof(string));

            //Add generically named columns for any overflow
            for (int i = 5; i < columns; i++)
            {
                table.Columns.Add();
            }

            //Once that's done, load all of the data in
            foreach (var array in list)
            {
                array[1] = DateTime.Parse(array[1]).ToShortTimeString();
                table.Rows.Add(array);
            }

            //Return our shiny new data structure
            return table;
        }

        //Method to update the DataGridView
        private void UpdateGUI()
        {
            //Set its source to null and then set it back to the update information
            grdDispatches.DataSource = null;
            grdDispatches.DataSource =
                ConvertListToDataTable(_systemScheduler.DispatchDatabase.DispatchDatabaseDataSource);
        }

        //Method to determine if we can enable the add, remove, and edit buttons
        private void CheckButtonEnable()
        {
            //If a database has been loaded and a row selected
            if (txtFilepath.Text != "" && grdDispatches.CurrentRow.Index >= 0)
            {
                //Enable them
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }

            //Otherwise
            else
            {
                //Disable them
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        //Method used to select a new dispatch database to load
        private void GetSchedulerFile()
        {

            //Black magic
            if (InvokeRequired)
            {
                BeginInvoke(new Action(GetSchedulerFile));
                return;
            }

            //If the user has opened our file picker, selected a file, and hit "OK"
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                //Get the filepath and put it in our text box
                txtFilepath.Text = dlgOpen.FileName;

                //Start the machinations to get the new database loaded in
                _systemScheduler.NewFile(txtFilepath.Text);

                //If it loaded in successfully
                if (_systemScheduler.DispatchDatabase != null)
                {
                    //Display the info on the grid
                    grdDispatches.DataSource = ConvertListToDataTable(_systemScheduler.DispatchDatabase.DispatchDatabaseDataSource);
                }

                //If it failed
                else
                {
                    //Clear everything out
                    grdDispatches.DataSource = null;
                    grdDispatches.Columns.Clear();
                    txtFilepath.Text = "";
                }
            }
            CheckButtonEnable();
        }

        # endregion

        # region Events

        //When the browse button is clicked
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Go and get a new dispatch database
            GetSchedulerFile();
        }

        //When the delete button is clicked
        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            //Display a message box to the user asking them if they are sure they want to delete the dispatch
            if (MessageBox.Show("Are you sure you want to delete dispatch " + grdDispatches[0, grdDispatches.CurrentRow.Index].Value + "?", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //If they are sure, remove the dispatch
                _systemScheduler.DispatchDatabase.RemoveDispatch(int.Parse((string)grdDispatches[0, grdDispatches.CurrentRow.Index].Value));
                
                //Update the DataGridView
                UpdateGUI();

                //Check if we should still have all of our buttons enabled
                CheckButtonEnable();
            }
        }

        //When the user clicks a cell inside the GridView
        private void grdDispatches_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check if we should have all of our buttons enabled
            CheckButtonEnable();
        }

        //When the add button is clicked
        private void btnAdd_Click(object sender, EventArgs e)
        {

            //Create a new add or edit route dialog box
            var objCustomDialogBox = new AddEditGUI(_environment, _systemScheduler.DispatchDatabase, false, new[] {""});

            //Give it a name too, because why not?
            objCustomDialogBox.Text = "Dispatch Editor";

            //Show it to the user
            //If they do things and then hit "OK"
            if (objCustomDialogBox.ShowDialog() == DialogResult.OK)
            {

                //Update the GridView
                UpdateGUI();
            }

            //When its all done null out the dialog box to save memory
            objCustomDialogBox = null;

            //Check if we should have all of our buttons enabled
            CheckButtonEnable();
        }

        //When the edit button is clicked
        private void btnEdit_Click(object sender, EventArgs e)
        {

            //Create a new add edit dialog box and pre-populate it with all of the existing information
            var objCustomDialogBox = new AddEditGUI(_environment, _systemScheduler.DispatchDatabase, true,
                                                    new[]
                                                        {
                                                            ((string)
                                                             grdDispatches[0, grdDispatches.CurrentRow.Index].Value),
                                                            ((string)
                                                             grdDispatches[1, grdDispatches.CurrentRow.Index].Value),
                                                            ((string)
                                                             grdDispatches[2, grdDispatches.CurrentRow.Index].Value),
                                                            ((string)
                                                             grdDispatches[3, grdDispatches.CurrentRow.Index].Value),
                                                            ((string)
                                                             grdDispatches[4, grdDispatches.CurrentRow.Index].Value)
                                                        });

            //Give it a name too, because why not?
            objCustomDialogBox.Text = "Dispatch Editor";

            //If the user did stuff and then clicked the OK button
            if (objCustomDialogBox.ShowDialog() == DialogResult.OK)
            {
                
                //Update the GridView to reflect the changes
                UpdateGUI();
            }

            //Null out the dialog box to save memory
            objCustomDialogBox = null;

            //Check to see if we should still enable all of our buttons
            CheckButtonEnable();
        }

        /*
        //When we recieve a tick from the environment
        private void EnvironmentTickHandler(object sender, EventArgs e)
        {
            string temporary = _systemScheduler.SchedulerTime.ToLongTimeString();
            lblTest.Text = temporary;
        }
        */

        # endregion
    }
}