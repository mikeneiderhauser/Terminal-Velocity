using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Interfaces;
using Utility;

namespace TrackController
{
    public partial class TrackControllerUi : UserControl
    {
        private List<IBlock> _blocks;
        private List<ITrainModel> _trains;
        private TrackController _current;

        public TrackControllerUi(ISimulationEnvironment e, ITrackController primary)
        {
            _current = (TrackController) primary;
            _trains = Tc.Trains;
            _blocks = Tc.Blocks;

            InitializeComponent();

            e.Tick += ETick;
        }

        private TrackController Tc
        {
            get { return _current; }
            set { _current = value; }
        }

        public void Draw()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(Draw));
                return;
            }

            foreach (var s in _current.Messages)
                messageTextBox.Text = string.Format("{0}\n{1}", messageTextBox.Text, s);
            _current.Messages = new List<string>();

            trainGrid.Rows.Clear();
            blockGrid.Rows.Clear();

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
                // Setup the BlockGrid
                for (int i = 0; i < _blocks.Count; i++)
                {
                    blockGrid.Rows.Add();
                    blockGrid.Rows[i].SetValues(_blocks[i].BlockID.ToString(),
                                                Enum.GetName(typeof (StateEnum), _blocks[i].hasSwitch()));
                }
            }

            {
                // Setup the ListBox with information about the controller
                tcListBoxInfo.Items.Add(string.Format("Track Controller: {0}", _current.ID));
                tcListBoxInfo.Items.Add(string.Format("Blocks: {0}", _blocks.Count));
                tcListBoxInfo.Items.Add(string.Format("Trains: {0}", _trains.Count));
            }
        }

        private void NextButtonClick(object sender, EventArgs e)
        {
            if (Tc.Next == null)
                MessageBox.Show("No next Track Controller!");
            else
            {
                Tc = (TrackController) Tc.Next;
                Draw();
            }
        }

        private void PrevButtonClick(object sender, EventArgs e)
        {
            if (Tc.Previous == null)
                MessageBox.Show("No prior Track Controller!");
            else
            {
                Tc = (TrackController) Tc.Previous;
                Draw();
            }
        }

        private void ETick(object sender, TickEventArgs e)
        {
            var newTrains = Tc.Trains;
            var newBlocks = Tc.Blocks;

            var differentTrains =
                newTrains.Where(x => _trains.All(x1 => x1.TrainID != x.TrainID)).Union(
                    _trains.Where(x => newTrains.All(x1 => x1.TrainID != x.TrainID)));

            var differentBlocks = newBlocks.Where(x => _blocks.All(x1 => x1.State != x.State))
                .Union(_blocks.Where(x => newBlocks.All(x1 => x1.State != x.State)));

            _trains = Tc.Trains;
            _blocks = Tc.Blocks;

            if (differentTrains.Any() || differentBlocks.Any())
                Draw();
        }
    }
}