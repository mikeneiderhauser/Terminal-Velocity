using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Interfaces;

namespace SystemScheduler
{
    public partial class SystemSchedulerGUI : UserControl
    {
        # region Private Variables

        private readonly ISimulationEnvironment _environment;
        private readonly SystemScheduler _systemScheduler;
        private ICTCOffice _ctcOffice;
        private DateTime _currentTime;

        # endregion

        #region Constructor(s)

        public SystemSchedulerGUI(ISimulationEnvironment env, SystemScheduler systemScheduler, ICTCOffice ctc)
        {
            InitializeComponent();
            _environment = env;
            _ctcOffice = ctc;
            _systemScheduler = systemScheduler;
        }

        # endregion

        # region Private Methods

        private DataTable ConvertListToDataTable(List<string[]> list)
        {
            var table = new DataTable();

            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        private void UpdateGUI()
        {
            grdDispatches.DataSource = null;
            grdDispatches.DataSource =
                ConvertListToDataTable(_systemScheduler.DispatchDatabase.DispatchDatabaseDataSource);
        }

        private void CheckButtonEnable()
        {
            if (txtFilepath.Text != "" && grdDispatches.CurrentRow.Index >= 0)
            {
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        # endregion

        # region Events

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            GetSchedulerFile();
        }

        private void GetSchedulerFile()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(GetSchedulerFile));
                return;
            }

            //dlgOpen.ShowDialog();
            txtFilepath.Text =
                @"D:\Users\Mike\Documents\GitHub\Terminal-Velocity\Terminal-Velocity\SystemScheduler\Resources\KillMe.csv";
                //hardcoded dlgOpen.FileName;
            _systemScheduler.NewFile(txtFilepath.Text);
            grdDispatches.DataSource =
                ConvertListToDataTable(_systemScheduler.DispatchDatabase.DispatchDatabaseDataSource);
            CheckButtonEnable();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _systemScheduler.DispatchDatabase.RemoveDispatch(
                int.Parse((string) grdDispatches[0, grdDispatches.CurrentRow.Index].Value));
            UpdateGUI();
            CheckButtonEnable();
        }

        private void grdDispatches_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CheckButtonEnable();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var objCustomDialogBox = new AddEditGUI(_environment, _systemScheduler.DispatchDatabase, false, new[] {""});

            if (objCustomDialogBox.ShowDialog() == DialogResult.OK)
            {
                UpdateGUI();
            }
            else
            {
                MessageBox.Show("You clicked Cancel.");
            }

            objCustomDialogBox = null;
            CheckButtonEnable();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
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

            if (objCustomDialogBox.ShowDialog() == DialogResult.OK)
            {
                UpdateGUI();
            }
            else
            {
                MessageBox.Show("You clicked Cancel.");
            }

            objCustomDialogBox = null;
            CheckButtonEnable();
        }

        # endregion
    }
}