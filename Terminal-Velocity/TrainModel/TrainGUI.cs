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
        // TODO: Set selectedTrain = to train selected by combo box
        private Train selectedTrain;
        private List<ITrainModel> allTrains;
        private int numTrains;
        private int timer;
        private ISimulationEnvironment _environment;

        public TrainGUI(ISimulationEnvironment environment)
        {
            InitializeComponent();

            allTrainComboBox.SelectedIndexChanged += new EventHandler(allTrainComboBox_SelectedIndexChanged);

            allTrains = environment.AllTrains;
            numTrains = allTrains.Count;

            timer = 0;

            PopulateComboBox(allTrains); // TODO: update combobox each time new train is added

            selectedTrain = (Train)allTrainComboBox.SelectedItem;
            UpdateGUI();

            _environment = environment;
            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);
        }

        public void DisplayError(string error)
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
            foreach(Train train in allTrains)
            {
                allTrainComboBox.Items.Add(train);
            }
        }


        /// <summary>
        /// Updates the GUI with the information for the selected train.
        /// </summary>
        private void UpdateGUI()
        {
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
            // check if trains have been added or removed from list
            if (numTrains != allTrains.Count)
            {
                PopulateComboBox(allTrains);
                numTrains = allTrains.Count;
            }

            selectedTrain = (Train)allTrainComboBox.SelectedItem;
            UpdateGUI();
        }
    }
}
