using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Interfaces;
using TrackController;
using SimulationEnvironment;

namespace TrackController
{
    public partial class TrackControllerUI : UserControl
    {
        private Thread refresh = null;

        private TrackController _current;

        public TrackControllerUI(SimulationEnvironment.SimulationEnvironment e)
        {
            _current = (TrackController) e.PrimaryTrackControllerGreen;
            e.Tick += e_Tick;

            InitializeComponent();
        }

        private TrackController TC
        {
            get { return _current; }
            set { _current = value; }
        }

        private void DoUpdate()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(this.DoUpdate));
                return;
            }

            List<ITrainModel> trains = TC.Trains;
            List<IBlock> blocks = TC.Blocks;
            List<IRoute> routes = TC.Routes;

            trainGrid.Rows.Clear();
            blockGrid.Rows.Clear();
            switchGrid.Rows.Clear();

            { // Setup the TrainGrid
                for (int i = 0; i < trains.Count; i++)
                {
                    trainGrid.Rows.Add();
                    trainGrid.Rows[i].Cells[0].Value = trains[i].TrainID;
                    trainGrid.Rows[i].Cells[1].Value = "Route...";
                    trainGrid.Rows[i].Cells[2].Value = trains[i].CurrentVelocity;
                    trainGrid.Rows[i].Cells[3].Value = "Authority...";
                }
            }

            { // Setup the BlockGrid and SwitchGrid
                for (int i = 0; i < blocks.Count; i++)
                {
                    blockGrid.Rows.Add();
                    blockGrid.Rows[i].Cells[0].Value = blocks[i].BlockID;
                    blockGrid.Rows[i].Cells[1].Value = "Info...";
                    blockGrid.Rows[i].Cells[2].Value = "Info...";
                    blockGrid.Rows[i].Cells[3].Value = "Info...";

                    if (blocks[i].hasSwitch())
                    {
                        switchGrid.Rows.Add();
                        switchGrid.Rows[switchGrid.Rows.Count - 1].Cells[0].Value = blocks[i].BlockID;
                    }
                }
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            TC = (TrackController) TC.Next;
            AsyncUpdate();
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            TC = (TrackController) TC.Previous;
            AsyncUpdate();
        }

        void e_Tick(object sender, Utility.TickEventArgs e)
        {
            AsyncUpdate();
        }

        private void AsyncUpdate()
        {
            this.refresh = new Thread(new ThreadStart(this.DoUpdate));
            this.refresh.Start();
        }
    }
}
