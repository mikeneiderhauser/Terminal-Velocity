using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrainModel
{
    public partial class TrainGUI : UserControl
    {
        // TODO: Set selectedTrain = to train selected by combo box
        private Train selectedTrain;

        public TrainGUI()
        {
            InitializeComponent();

            // TODO: populate combo box by setting allTrains equal to list contained in environment

            UpdateGUI();
        }

        public void DisplayError(string error)
        {
            MessageBox.Show(error, "Critical Error with Train", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void PopulateComboBox(List<Train> allTrains)
        {
            foreach(Train train in allTrains)
            {
                allTrainComboBox.Items.Add(train.ToString());
            }
        }

        private void UpdateGUI()
        {
            
            positionValueText.Text = selectedTrain.CurrentPosition.ToString();
            velocityValueText.Text = selectedTrain.CurrentVelocity.ToString();
            accelerationValueText.Text = selectedTrain.CurrentAcceleration.ToString();
            
            elevationValueText.Text = selectedTrain.CurrentBlock.Grade.ToString();
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
            
        }
    }
}
