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
        private Dictionary<int, List<IBlock>> _routes; 
        private TrackController _current;

        private long _tickCount;

        public TrackControllerUi(ISimulationEnvironment e, ITrackController primary)
        {
            _current = (TrackController) primary;
            _trains = Tc.Trains;
            _blocks = Tc.Blocks;
            _routes = Tc.Routes;

            InitializeComponent();

            _tickCount = 0;
            e.Tick += ETick;

            Draw();
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
                    var r = string.Empty;
                    List<IBlock> route;
                    if (_routes.TryGetValue(i, out route))
                        r = string.Join(", ", from item in route select item.BlockID);

                    trainGrid.Rows.Add();
                    trainGrid.Rows[i].SetValues(_trains[i].TrainID,
                                                r,
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
                                                Enum.GetName(typeof (StateEnum), _blocks[i].SwitchDest1));
                }
            }

            {
                // Setup the ListBox with information about the controller
                tcListBoxInfo.Items.Add(string.Format("Track Controller: {0}", _current.ID));
                tcListBoxInfo.Items.Add(string.Format("Blocks: {0}", _blocks.Count));
                tcListBoxInfo.Items.Add(string.Format("Trains: {0}", _trains.Count));
            }

            trainGrid.ClearSelection();
            blockGrid.ClearSelection();
            tcListBoxInfo.ClearSelected();
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
            _routes = Tc.Routes;

            var newTrains = Tc.Trains;
            var newBlocks = Tc.Blocks;

            if (newTrains.Count != _trains.Count)
            {
                _trains = Tc.Trains;
                _blocks = Tc.Blocks;
                Draw();
            }

            for (var i = 0; i < _trains.Count; i++)
            {
                if (_trains[i].TrainID != newTrains[i].TrainID)
                {
                    _trains = Tc.Trains;
                    _blocks = Tc.Blocks;
                    Draw();
                }
            }

            for (var i = 0; i < _blocks.Count; i++)
            {
                if (_blocks[i].State != newBlocks[i].State)
                {
                    _trains = Tc.Trains;
                    _blocks = Tc.Blocks;
                    Draw();
                }
            }

            if (_tickCount % 4 == 0) Draw();
            _tickCount++;
        }
    }
}