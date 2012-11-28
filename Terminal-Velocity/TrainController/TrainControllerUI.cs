using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrainController
{
    public partial class TrainControllerUI : UserControl
    {
        private TrainController _currentTrainController;

        public TrainControllerUI(TrainController tc)
        {
            InitializeComponent();
            _currentTrainController = tc;
        }

        public void RecLog(string entry)
        {
            _listLog.Items.Add(entry);
        }
     

        private void _btnEmergencyBrake_Click(object sender, EventArgs e)
        {
            _currentTrainController.EmergencyBrakes();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
