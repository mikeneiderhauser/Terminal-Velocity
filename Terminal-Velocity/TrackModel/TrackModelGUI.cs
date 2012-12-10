using System;
using System.Drawing;
using System.Windows.Forms;
using Interfaces;

namespace TrackModel
{
    public partial class TrackModelGUI : UserControl
    {
        //Private variables
        private readonly TrackModel _tm;
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
            var getFName = new OpenFileDialog();

            if (getFName.ShowDialog() == DialogResult.OK)
            {
                string fName = getFName.FileName;
                bool res=_tm.provideInputFile(fName);
            }
        }

        private void trackDisplayPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = trackDisplayPanel.CreateGraphics();
            var pen = new Pen(Color.Black, 4);
            Brush brush = new SolidBrush(Color.Red);

            int totalH = trackDisplayPanel.Height;
            int totalW = trackDisplayPanel.Width;

            IBlock[,] temp = _tm.requestTrackGrid(0); //Get red's track grid
            int numRows = temp.GetUpperBound(0);
            int numCols = temp.GetUpperBound(1);

            int squareHeight = totalH/numRows;
            int squareWidth = totalW/numCols;
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (temp[i, j] != null)
                        g.FillRectangle(brush, j*squareWidth, i*squareHeight, squareWidth, squareHeight);
                }
            }



            //Repeat drawing process for the Green Line
            brush = new SolidBrush(Color.Green);

            temp = _tm.requestTrackGrid(1);
            numRows = temp.GetUpperBound(0);
            numCols = temp.GetUpperBound(1);

            squareHeight = totalH / numRows;
            squareWidth = totalW / numCols;
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (temp[i, j] != null)
                        g.FillRectangle(brush, j * squareWidth, i * squareHeight, squareWidth, squareHeight);
                }
            }
        }

        private void trackDisplayPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //Get x and y coordinates from MouseEventArgs e
            int xCoord = e.X;
            int yCoord = e.Y;


            //Get line information
            IBlock[,] tempRed = null;
            IBlock[,] tempGreen = null;
            if(_tm.RedLoaded)
                tempRed = _tm.requestTrackGrid(0);
            if(_tm.GreenLoaded)
                tempGreen = _tm.requestTrackGrid(1);

            if (_tm.RedLoaded == true || _tm.GreenLoaded == true)
            {
                //Translate screen coordinates back in to blockID and line
                int totalHeight = trackDisplayPanel.Height;
                int totalWidth = trackDisplayPanel.Width;
                int numRows;
                int numCols;
                if (_tm.RedLoaded)
                {
                    numRows = tempRed.GetUpperBound(0);
                    numCols = tempRed.GetUpperBound(1);
                }
                else
                {
                    numRows = tempGreen.GetUpperBound(0);
                    numCols = tempGreen.GetUpperBound(1);
                }

                int coordsPerRow = totalHeight / numRows;
                int rowClicked = yCoord / coordsPerRow;

                int coordsPerCol = totalWidth / numCols;
                int colClicked = xCoord / coordsPerCol;

                IBlock potentialRedBlock = null;
                IBlock potentialGreenBlock = null;
                if (tempRed != null)
                    potentialRedBlock = tempRed[rowClicked, colClicked];
                if (tempGreen != null)
                    potentialGreenBlock = tempGreen[rowClicked, colClicked];

                //This if/else chain gives priority to red line.  If red and green line cross,
                //a click on that point will always display the information for the red line
                if (potentialRedBlock != null)//If we found a valid red block
                {
                    valBlockID.Text = potentialRedBlock.BlockID.ToString(); ;
                    valState.Text = potentialRedBlock.State.ToString();
                    valHeater.Text = potentialRedBlock.hasHeater().ToString();
                    valCircuit.Text = potentialRedBlock.BlockID.ToString();

                    string switchString;
                    if (potentialRedBlock.hasSwitch())
                    {
                        if (potentialRedBlock.SwitchDest1 == 0)//Points to yard
                            switchString = "Points to Yard";
                        else
                            switchString = "Points to Block " + potentialRedBlock.SwitchDest1;
                    }
                    else
                    {
                        switchString = "No Switch";
                    }

                    valSwitch.Text = switchString;
                    valTunnel.Text = potentialRedBlock.hasTunnel().ToString();
                    valLine.Text = "Red Line";
                }
                else if (potentialGreenBlock != null)//if there was no red block, but we found a green block
                {
                    valBlockID.Text = potentialGreenBlock.BlockID.ToString(); ;
                    valState.Text = potentialGreenBlock.State.ToString();
                    valHeater.Text = potentialGreenBlock.hasHeater().ToString();
                    valCircuit.Text = potentialGreenBlock.BlockID.ToString();

                    string switchString;
                    if (potentialGreenBlock.hasSwitch())
                    {
                        if (potentialGreenBlock.SwitchDest1 == 0)//Points to yard
                            switchString = "Points to Yard";
                        else
                            switchString = "Points to Block " + potentialGreenBlock.SwitchDest1;
                    }
                    else
                    {
                        switchString = "No Switch";
                    }

                    valSwitch.Text = switchString;
                    valTunnel.Text = potentialGreenBlock.hasTunnel().ToString();
                    valLine.Text = "Green Line";
                }
                else
                {
                    valBlockID.Text = "NoBlockSelected";
                    valState.Text = "NoBlockSelected";
                    valHeater.Text = "NoBlockSelected";
                    valCircuit.Text = "NoBlockSelected";
                    valSwitch.Text = "NoBlockSelected";
                    valTunnel.Text = "NoBlockSelected";
                    valLine.Text = "NoBlockSelected";
                }
            }//End if at least one line was loaded
        }
    }
}