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
    public partial class SystemSchedulerGUI : UserControl
    {
        private ISimulationEnvironment _environment;
        private ICTCOffice _ctcOffice;
        private SystemScheduler _systemScheduler;
        private DateTime _currentTime;

        #region Constructor(s)

        public SystemSchedulerGUI(ISimulationEnvironment env, SystemScheduler systemScheduler, ICTCOffice ctc)
        {
            InitializeComponent();
            _environment = env;
            _ctcOffice = ctc;
            _systemScheduler = systemScheduler;
        }

        # endregion

        # region Events

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            dlgOpen.ShowDialog();
            txtFilepath.Text = dlgOpen.FileName;
            _systemScheduler.NewFile(txtFilepath.Text);
            grdDispatches.DataSource = ConvertListToDataTable(_systemScheduler.DispatchDatabase.DispatchDatabaseDataSource);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _systemScheduler.DispatchDatabase.Remove(int.Parse((string)grdDispatches[0, grdDispatches.CurrentRow.Index].Value));
            grdDispatches.DataSource = null;
            grdDispatches.DataSource = ConvertListToDataTable(_systemScheduler.DispatchDatabase.DispatchDatabaseDataSource);
        }

        private void grdDispatches_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
        }

        # endregion

        # region Private Methods

        private DataTable ConvertListToDataTable(List<string[]> list)
        {
            DataTable table = new DataTable();

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

        # endregion

    }
}
