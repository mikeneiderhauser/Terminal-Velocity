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
        private TrackController _current;
        private int _dirty = 0;

        private List<ITrainModel> _trains;
        private List<IBlock> _blocks;
        private List<IRoute> _routes;

        public TrackControllerUI(SimulationEnvironment.SimulationEnvironment e)
        {
            _current = (TrackController) e.PrimaryTrackControllerGreen;
            _trains = TC.Trains;
            _blocks = TC.Blocks;
            _routes = TC.Routes;

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

            trainGrid.Rows.Clear();
            blockGrid.Rows.Clear();
            switchGrid.Rows.Clear();

            { // Setup the TrainGrid
                for (int i = 0; i < _trains.Count; i++)
                {
                    trainGrid.Rows.Add();
                    trainGrid.Rows[i].Cells[0].Value = _trains[i].TrainID;
                    trainGrid.Rows[i].Cells[1].Value = "Route...";
                    trainGrid.Rows[i].Cells[2].Value = _trains[i].CurrentVelocity;
                    trainGrid.Rows[i].Cells[3].Value = "Authority...";
                }
            }

            { // Setup the BlockGrid and SwitchGrid
                for (int i = 0; i < _blocks.Count; i++)
                {
                    blockGrid.Rows.Add();
                    blockGrid.Rows[i].Cells[0].Value = _blocks[i].BlockID;
                    blockGrid.Rows[i].Cells[1].Value = "Info...";
                    blockGrid.Rows[i].Cells[2].Value = "Info...";
                    blockGrid.Rows[i].Cells[3].Value = "Info...";

                    if (_blocks[i].hasSwitch())
                    {
                        switchGrid.Rows.Add();
                        switchGrid.Rows[switchGrid.Rows.Count - 1].Cells[0].Value = _blocks[i].BlockID;
                    }
                }
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            TC = (TrackController) TC.Next;
            DoUpdate();
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            TC = (TrackController) TC.Previous;
            DoUpdate();
        }

        void e_Tick(object sender, Utility.TickEventArgs e)
        {
            if (!_trains.Equals(TC.Trains))
                _dirty++;
            
            _trains = TC.Trains;
            _blocks = TC.Blocks;
            _routes = TC.Routes;

            DoUpdate();
        }
    }
}
