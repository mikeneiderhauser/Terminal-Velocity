using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Interfaces;
using TrackController;
using SimulationEnvironment;

namespace TrackController
{
    public partial class TrackControllerUI : UserControl
    {
        private TrackController _current;

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

        public override void Refresh()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(this.Refresh));
                return;
            }

            foreach (string s in _current.Messages)
                messageTextBox.Text = string.Format("{0}\n{1}", messageTextBox.Text, s);
            _current.Messages = new List<string>();

            _trains = TC.Trains;
            _blocks = TC.Blocks;
            _routes = TC.Routes;

            trainGrid.Rows.Clear();
            blockGrid.Rows.Clear();
            switchGrid.Rows.Clear();

            tcListBoxInfo.Items.Clear();

            { // Setup the TrainGrid
                for (int i = 0; i < _trains.Count; i++)
                {
                    trainGrid.Rows.Add();
                    trainGrid.Rows[i].SetValues(_trains[i].TrainID,
                                                "RouteID",
                                                _trains[i].TrainController.AuthorityLimit,
                                                _trains[i].CurrentVelocity);

                }
            }

            { // Setup the BlockGrid and SwitchGrid
                for (int i = 0; i < _blocks.Count; i++)
                {
                    blockGrid.Rows.Add();
                    blockGrid.Rows[i].SetValues(_blocks[i].BlockID.ToString(), 
                                                Enum.GetName(typeof(StateEnum), _blocks[i].State));
                }
            }

            { // Setup the ListBox with information about the controller
                tcListBoxInfo.Items.Add(string.Format("Track Controller: {0}", _current.ID));
                tcListBoxInfo.Items.Add(string.Format("Blocks: {0}", _blocks.Count));
                tcListBoxInfo.Items.Add(string.Format("Trains: {0}", _trains.Count));
            }

            base.Refresh();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (TC.Next == null)
                MessageBox.Show("No next Track Controller!");
            else
            {
                TC = (TrackController)TC.Next;
                Refresh();
            }
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            if (TC.Previous == null)
                MessageBox.Show("No prior Track Controller!");
            else
            {
                TC = (TrackController)TC.Previous;
                Refresh();
            }
        }

        static int ticks = 0;
        void e_Tick(object sender, Utility.TickEventArgs e)
        {
            Refresh();
        }
    }
}
