using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interfaces;

namespace TrackModel
{
    public partial class TrackModelGUI : UserControl
    {
        //Private variables
        private TrackModel _tm;
        private ISimulationEnvironment _environment;

        //Constructor
        public TrackModelGUI(ISimulationEnvironment env, TrackModel tm)
        {
            //var initialization
            _environment = env;
            _tm = tm;

            //Subscribe to an environment Tick
            //_environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);

            //Component initialization
            InitializeComponent();
        }


        private void loadFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog getFName = new OpenFileDialog();

            if (getFName.ShowDialog() == DialogResult.OK)
            {
                string fName = getFName.FileName;
            }
        }
    }
}
