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

        public SystemSchedulerGUI(ISimulationEnvironment env, ICTCOffice ctc)
        {
            InitializeComponent();
            _environment = env;
            _ctcOffice = ctc;
            _systemScheduler = new SystemScheduler(_environment, _ctcOffice);
            _currentTime = DateTime.Now;
            _currentTime.AddMilliseconds(_currentTime.Millisecond * -1);

            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Bollocks);
        }

        void _environment_Bollocks(object sender, TickEventArgs e)
        {
            _currentTime = _currentTime.AddMilliseconds(100);
            if (((_currentTime.Minute % 15) == 0) && (_currentTime.Second == 0) && (_currentTime.Millisecond == 0))
            {
                CheckForDispatches(_currentTime);
            }
        }

        private void CheckForDispatches(DateTime currentTime)
        {
            foreach (IDispatch singleDispatch in _systemScheduler.DispatchDatabase.DispatchList)
            {
                if (singleDispatch.DispatchTime == currentTime) {
                    
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            dlgOpen.ShowDialog();
            txtFilepath.Text = dlgOpen.FileName;
            _systemScheduler.NewFile(txtFilepath.Text);
            grdDispatches.DataSource = _systemScheduler.DispatchDatabase.DispatchDatabaseDataSource;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _systemScheduler.DispatchDatabase.Remove((int)grdDispatches[0, grdDispatches.CurrentRow.Index].Value);
        }

        private void grdDispatches_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
        }            
    }
}
