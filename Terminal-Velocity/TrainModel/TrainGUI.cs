using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interfaces;

namespace TrainModel
{
    public partial class TrainGUI : UserControl
    {
        // TODO: Set selectedTrain = to train selected by combo box
        private Train selectedTrain;
        private List<ITrainModel> allTrains;
        private int numTrains;

        public TrainGUI(ISimulationEnvironment environment)
        {
            InitializeComponent();

            allTrainComboBox.SelectedIndexChanged += new EventHandler(allTrainComboBox_SelectedIndexChanged);

            allTrains = environment.AllTrains;
            numTrains = allTrains.Count;

            PopulateComboBox(allTrains); // TODO: update combobox each time new train is added

            selectedTrain = (Train)allTrainComboBox.SelectedItem;
            UpdateGUI();
        }

        public void DisplayError(string error)
        {
            MessageBox.Show(error, "Critical Error with Train", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void PopulateComboBox(List<ITrainModel> allTrains)
        {
            foreach(Train train in allTrains)
            {
                allTrainComboBox.Items.Add(train);
            }
        }

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

        private void allTrainComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (numTrains != allTrains.Count) // TODO: check if trains have been added or removed from list
                PopulateComboBox(allTrains);

            selectedTrain = (Train)allTrainComboBox.SelectedItem;
            UpdateGUI();
        }
    }
}
