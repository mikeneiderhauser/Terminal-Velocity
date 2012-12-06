using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Interfaces;
using Utility;

namespace TrainModel
{
    public partial class TrainGUI : UserControl
    {
        #region Global variables

        private readonly ISimulationEnvironment _environment;
        private readonly List<ITrainModel> _allTrains;
        private List<string> _errorMessages;
        private int _numTrains;

        private Train _selectedTrain;
        private int _timer;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the Train GUI.
        /// </summary>
        /// <param name="environment">The environment being used by the entire simulation.</param>
        public TrainGUI(ISimulationEnvironment environment)
        {
            InitializeComponent();

            allTrainComboBox.SelectedIndexChanged += allTrainComboBox_SelectedIndexChanged;
            _allTrains = environment.AllTrains;
            _numTrains = _allTrains.Count;
            _errorMessages = new List<string>();

            _timer = 0;

            PopulateComboBox(_allTrains);

            if (_allTrains != null && _allTrains.Count > 0)
            {
                _selectedTrain = (Train)_allTrains[0];
                allTrainComboBox.SelectedItem = _selectedTrain;
            }

            UpdateGUI();

            _environment = environment;
            _environment.Tick += _environment_Tick;
        }

        #endregion

        #region Private methods

        /// <summary>
        ///     Displays error message when a failure occurs.
        /// </summary>
        /// <param name="error">The error message to display.</param>
        private void DisplayError(string error)
        {
            if (!_errorMessages.Contains(error))
            {
                _errorMessages.Add(error);
                MessageBox.Show(error, "Critical Error with Train", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        ///     Updates the GUI every 10 ticks. HARDCODED
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Tick event args</param>
        private void _environment_Tick(object sender, TickEventArgs e)
        {
            _timer++;

            if (_timer % 10 == 0)
            {
                _timer = 0;
                UpdateGUI();
            }
        }

        /// <summary>
        ///     Populates the combobox using the list of all trains.
        /// </summary>
        /// <param name="_allTrains">The list of all trains contained in the environment.</param>
        private void PopulateComboBox(List<ITrainModel> _allTrains)
        {
            allTrainComboBox.Items.Clear();

            foreach (Train train in _allTrains)
            {
                allTrainComboBox.Items.Add(train);
            }
        }

        /// <summary>
        ///     Updates the GUI with the information for the selected train.
        /// </summary>
        private void UpdateGUI()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateGUI));
                return;
            }

            // check if trains have been added or removed from list
            if (_numTrains != _allTrains.Count)
            {
                PopulateComboBox(_allTrains);
                _numTrains = _allTrains.Count;
            }

            // check for errors in all trains
            foreach (Train train in _allTrains)
            {
                // checks for any failures
                if (train.BrakeFailure)
                {
                    DisplayError("CRITICAL ERROR: Brake failure for " + train);
                }

                if (train.EngineFailure)
                {
                    DisplayError("CRITICAL ERROR: Engine failure for " + train);
                }

                if (train.SignalPickupFailure)
                {
                    DisplayError("CRITICAL ERROR: Signal pickup failure for " + train);
                }
            }

            if (_selectedTrain != null)
            {
                trainLabel.Text = _selectedTrain.ToString();
                trainInfoTextBox.Text = _selectedTrain.InformationLog;

                positionValueText.Text = Math.Round(_selectedTrain.CurrentPosition, 3).ToString();
                velocityValueText.Text = Math.Round(_selectedTrain.CurrentVelocity, 3).ToString();
                accelerationValueText.Text = Math.Round(_selectedTrain.CurrentAcceleration, 3).ToString();

                gradeValueText.Text = _selectedTrain.CurrentBlock.Grade.ToString();
                massValueText.Text = Math.Round(_selectedTrain.TotalMass, 3).ToString();

                numPassengersValueText.Text = _selectedTrain.NumPassengers.ToString();
                numCrewValueText.Text = _selectedTrain.NumCrew.ToString();

                // set values for lights
                if (_selectedTrain.LightsOn)
                {
                    lightsValueText.Text = "On";
                }
                else
                {
                    lightsValueText.Text = "Off";
                }

                // set values for doors
                if (_selectedTrain.DoorsOpen)
                {
                    doorsValueText.Text = "Open";
                }
                else
                {
                    doorsValueText.Text = "Closed";
                }
            }
        }

        /// <summary>
        ///     Detects when the selected index of the combo box changes, and updates the GUI.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        private void allTrainComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedTrain = (Train)allTrainComboBox.SelectedItem;
            UpdateGUI();
        }

        #endregion
    }
}