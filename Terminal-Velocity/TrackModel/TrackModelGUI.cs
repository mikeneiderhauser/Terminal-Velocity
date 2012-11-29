using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interfaces;

namespace TrackModel
{
    public partial class TrackModelGUI : UserControl
    {
        //Private variables
        private TrackModel _tm;
        private ISimulationEnvironment _environment;

        //Constructor
        public TrackModelGUI(ISimulationEnvironment env, TrackModel tm)
        {
            //var initialization
            _environment = env;
            _tm = tm;

            //Subscribe to an environment Tick
            //_environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);

            //Component initialization
            InitializeComponent();
        }


        private void loadFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog getFName = new OpenFileDialog();

            if (getFName.ShowDialog() == DialogResult.OK)
            {
                string fName = getFName.FileName;
                //bool res=_tm.provideInputFile(fName);
      
            }
        }

        private void trackDisplayPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = trackDisplayPanel.CreateGraphics();
            Pen pen = new Pen(Color.Black, 4);
            Brush brush = new SolidBrush(Color.Red);

            int totalH=trackDisplayPanel.Height;
            int totalW = trackDisplayPanel.Width;

            IBlock[,] temp = _tm.requestTrackGrid(0);//Get red's track grid
            int numRows=temp.GetUpperBound(0);
            int numCols = temp.GetUpperBound(1);

            int squareHeight = totalH / numRows;
            int squareWidth = totalW / numCols;
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if(temp[i,j]!=null)
                        g.FillRectangle(brush,j*squareWidth,i*squareHeight, squareWidth, squareHeight);
                }
            }
        }
    }
}
