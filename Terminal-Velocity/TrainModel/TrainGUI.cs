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
        public TrainGUI()
        {
            InitializeComponent();
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
    }
}
