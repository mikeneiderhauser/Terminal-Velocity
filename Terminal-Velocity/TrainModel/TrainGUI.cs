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

namespace TrainModel
{
    public partial class TrainGUI : UserControl
    {
        #region Global variables

        private int numTrains;
        private int timer;

        private Train selectedTrain;
        private List<ITrainModel> allTrains;
        private ISimulationEnvironment _environment;

        #endregion



        #region Constructors

        public TrainGUI(ISimulationEnvironment environment)
        {
            InitializeComponent();

            allTrainComboBox.SelectedIndexChanged += new EventHandler(allTrainComboBox_SelectedIndexChanged);
            allTrains = environment.AllTrains;
            numTrains = allTrains.Count;

            timer = 0;

            PopulateComboBox(allTrains);

            selectedTrain = (Train)allTrainComboBox.SelectedItem;
            UpdateGUI();

            _environment = environment;
            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);
        }

        #endregion



        #region Private methods

        /// <summary>
        /// Displays error message when a failure occurs.
        /// </summary>
        /// <param name="error"></param>
        private void DisplayError(string error)
        {
            MessageBox.Show(error, "Critical Error with Train", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Updates the GUI every 10 ticks. HARDCODED
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _environment_Tick(object sender, TickEventArgs e)
        {
            timer++;

            if (timer % 10 == 0)
            {
                timer = 0;
                UpdateGUI();
            }
        }

        /// <summary>
        /// Populates the combobox using the list of all trains.
        /// </summary>
        /// <param name="allTrains"></param>
        private void PopulateComboBox(List<ITrainModel> allTrains)
        {
            foreach (Train train in allTrains)
            {
                allTrainComboBox.Items.Add(train);
            }
        }

        /// <summary>
        /// Updates the GUI with the information for the selected train.
        /// </summary>
        private void UpdateGUI()
        {
            // check if trains have been added or removed from list
            if (numTrains != allTrains.Count)
            {
                PopulateComboBox(allTrains);
                numTrains = allTrains.Count;
            }

            // check for errors in all trains
            foreach (Train train in allTrains)
            {
                // checks for any failures
                if (train.BrakeFailure)
                {
                    DisplayError("CRITICAL ERROR: Brake failure for " + train.ToString());
                }

                if (train.EngineFailure)
                {
                    DisplayError("CRITICAL ERROR: Engine failure for " + train.ToString());
                }

                if (train.SignalPickupFailure)
                {
                    DisplayError("CRITICAL ERROR: Signal pickup failure for " + train.ToString());
                }
            }

            trainLabel.Text = selectedTrain.ToString();
            trainInfoTextBox.Text = selectedTrain.InformationLog;

            positionValueText.Text = selectedTrain.CurrentPosition.ToString();
            velocityValueText.Text = selectedTrain.CurrentVelocity.ToString();
            accelerationValueText.Text = selectedTrain.CurrentAcceleration.ToString();

            gradeValueText.Text = selectedTrain.CurrentBlock.Grade.ToString();
            massValueText.Text = selectedTrain.TotalMass.ToString();

            numPassengersValueText.Text = selectedTrain.NumPassengers.ToString();
            numCrewValueText.Text = selectedTrain.NumCrew.ToString();

            // set values for lights
            if (selectedTrain.LightsOn)
                lightsValueText.Text = "On";
            else
                lightsValueText.Text = "Off";

            // set values for doors
            if (selectedTrain.DoorsOpen)
                doorsValueText.Text = "Open";
            else
                doorsValueText.Text = "Closed";
        }

        /// <summary>
        /// Detects when the selected index of the combo box changes, and updates the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allTrainComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTrain = (Train)allTrainComboBox.SelectedItem;
            UpdateGUI();
        }

        private void trainInfoTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
