using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Interfaces;
using Utility;

namespace TrackController
{
    public partial class TrackControllerUI : UserControl
    {
        private static int ticks = 0;
        private List<IBlock> _blocks;
        private TrackController _current;
        private List<IRoute> _routes;
        private List<ITrainModel> _trains;
        private bool en = false;

        public TrackControllerUI(SimulationEnvironment.SimulationEnvironment e)
        {
            _current = (TrackController) e.PrimaryTrackControllerGreen;
            _trains = TC.Trains;
            _blocks = TC.Blocks;
            _routes = TC.Routes;

            InitializeComponent();

            e.Tick += e_Tick;
        }

        private TrackController TC
        {
            get { return _current; }
            set { _current = value; }
        }

        public override void Refresh()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(Refresh));
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

            {
                // Setup the TrainGrid
                for (int i = 0; i < _trains.Count; i++)
                {
                    trainGrid.Rows.Add();
                    trainGrid.Rows[i].SetValues(_trains[i].TrainID,
                                                "RouteID",
                                                _trains[i].TrainController.AuthorityLimit,
                                                _trains[i].CurrentVelocity);
                }
            }

            {
                // Setup the BlockGrid and SwitchGrid
                for (int i = 0; i < _blocks.Count; i++)
                {
                    blockGrid.Rows.Add();
                    blockGrid.Rows[i].SetValues(_blocks[i].BlockID.ToString(),
                                                Enum.GetName(typeof (StateEnum), _blocks[i].State));
                }
            }

            {
                // Setup the ListBox with information about the controller
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
                TC = (TrackController) TC.Next;
                Refresh();
            }
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            if (TC.Previous == null)
                MessageBox.Show("No prior Track Controller!");
            else
            {
                TC = (TrackController) TC.Previous;
                Refresh();
            }
        }

        private void e_Tick(object sender, TickEventArgs e)
        {
            Refresh();
        }
    }
}