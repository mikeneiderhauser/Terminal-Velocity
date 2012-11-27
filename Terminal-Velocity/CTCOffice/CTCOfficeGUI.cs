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

namespace CTCOffice
{
    public partial class CTCOfficeGUI : UserControl
    {
        private ISimulationEnvironment _environment;
        private CTCOffice _ctcOffice;
        private int _speedState;
        private LineData _redLineData;
        private LineData _greenLineData;

        public CTCOfficeGUI(ISimulationEnvironment env, CTCOffice ctc)
        {
            InitializeComponent();
            //set refs to ctc office and environment
            _ctcOffice = ctc;
            _environment = env;
            _speedState = 0;

            //subscribe to Environment Tick
            _environment.Tick += new EventHandler<TickEventArgs>(_environment_Tick);

            //ensure the user is logged out
            _ctcOffice.Logout();
            //change button text
            _btnLoginLogout.Text = "Login";
            
            //show team logo (block out user)
            mainDisplayLogo();            
            disableUserControls();
            _loginStatusImage.Image = Utility.Properties.Resources.red;
            _imageTeamLogo.Image = Properties.Resources.TerminalVelocity;

            //get line data
            _redLineData = _ctcOffice.getLine(0);
            _greenLineData = _ctcOffice.getLine(1);

            parseLineData();

            //post to log that the gui has loaded
            _environment.sendLogEntry("CTCOffice: GUI Loaded");
        }

        private void parseLineData()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i <= _redLineData.Layout.GetUpperBound(0); i++ )
            {
                for (int j = 0; j <= _redLineData.Layout.GetUpperBound(1); j++)
                {
                    PictureBox pane = new PictureBox();
                    _panelRedLine.Controls.Add(pane);
                    pane.Name = "_imgGrid_" + i + "_" + j;
                    pane.SizeMode = PictureBoxSizeMode.CenterImage;
                    pane.Size = new Size(20, 20);
                    pane.Location = new Point(x, y);
                    pane.Image = _redLineData.Layout[i, j].Tile;
                    pane.Tag = _redLineData.Layout[i, j];
                    pane.MouseClick += new MouseEventHandler(this._layoutPiece_MouseClick);
                    x += 20;
                }
                y += 20;
                x = 0;
            }


            for (int i = 0; i < _greenLineData.Layout.GetUpperBound(0); i++)
            {
                for (int j = 0; j < _greenLineData.Layout.GetUpperBound(1); j++)
                {

                }
            }

        }


        /// <summary>
        /// Function to handle Environment Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _environment_Tick(object sender, TickEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void _btnLoginLogout_Click(object sender, EventArgs e)
        {
            if (_ctcOffice.isAuth())
            {
                //if logged in.. log out
                _ctcOffice.Logout();
                //disable user controls (lock out op)
                disableUserControls();
                _loginStatusImage.Image = Utility.Properties.Resources.red;
                _btnLoginLogout.Text = "Login";
                mainDisplayLogo();
                //post to log
                _environment.sendLogEntry("CTCOffice: Operator Logged Out!");
            }
            else
            {
                //if logged out, login
                if (_ctcOffice.Login(_txtUsername.Text, _txtPassword.Text))
                {
                    //if login pass (enable controls)
                    enableUserControls();
                    _loginStatusImage.Image = Utility.Properties.Resources.green;
                    //show red line tab
                    showRedLine();
                    //remove password
                    _txtPassword.Text = "";
                    //change button txt
                    _btnLoginLogout.Text = "Logout";
                }
                else
                {
                    //if login fail (disable controls)
                    disableUserControls();
                    _loginStatusImage.Image = Utility.Properties.Resources.red;
                    _btnLoginLogout.Text = "Login";
                    //post to log
                    _environment.sendLogEntry("CTCOffice: Operator Login Failed -> UnAuthorized!");
                    //show logo
                    mainDisplayLogo();
                    //tell user invalid creds
                    MessageBox.Show("Invalid Credentials", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }//end button LoginLogout

        private IRoute routeSelection()
        {

            return null;
        }

        private void _btnDispatchTrain_Click(object sender, EventArgs e)
        {
            IRoute route = routeSelection();
            _ctcOffice.dispatchTrainRequest(route);
        }

        private void _btnRefreshView_Click(object sender, EventArgs e)
        {

        }

        private void _btnSchedule_1_Click(object sender, EventArgs e)
        {
            //show system scheduker
        }

        private void _btnSchedule_2_Click(object sender, EventArgs e)
        {
            //show system scheduler
        }

        private void _btnRefreshMetrics_Click(object sender, EventArgs e)
        {
            updateMetrics();
        }

        private void updateMetrics()
        {
            int max = 0;
            int sumPass = 0;
            int sumCrew = 0;
            List<ITrainModel> trains = _environment.AllTrains;
            foreach (ITrainModel t in trains)
            {
                sumPass += t.NumPassengers;
                sumCrew += t.NumCrew;
                max += t.MaxCapacity;
            }

            _lblTrainsMetrics.Text = trains.Count.ToString();
            _lblCrewMetrics.Text = sumCrew.ToString();
            _lblPassengersMetrics.Text = sumPass.ToString();
            _lblTotalMetrics.Text = (sumPass + sumCrew).ToString() + " / " + max.ToString();
        }

        private void _txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        private void _txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        private void _checkAutomatedScheduling_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkAutomatedScheduling.Checked)
            {
                _ctcOffice.StartScheduling();
            }
            else
            {
                _ctcOffice.StopScheduling();
            }
        }

        private void disableUserControls()
        {
            mainDisplayLogo();
            setControlState(false);
        }

        private void enableUserControls()
        {
            setControlState(true);
        }

        private void mainDisplayLogo()
        {
            _systemViewTabs.SelectedIndex = 2;//Terminal Velocity Tab
        }

        private void showRedLine()
        {
            _systemViewTabs.SelectedIndex = 0;//Red line
        }

        private void showGreenLine()
        {
            _systemViewTabs.SelectedIndex = 1;//Green Line
        }

        private void setControlState(bool state)
        {
            _btnDispatchTrain.Enabled = state;
            _btnSchedule_1.Enabled = state;
            _btnSchedule_2.Enabled = state;
            _btnRefreshView.Enabled = state;
            _btnRefreshMetrics.Enabled = state;
            _checkAutomatedScheduling.Enabled = state;
            _systemViewTabs.Enabled = state;
            _btnGlobalTime10WallSpeed.Enabled = state;
            _btnGlobalTimeWallSpeed.Enabled = state;

            if (state)
            {
                if (_speedState == 0)
                {
                    //at wall speed, show 10x
                    _btnGlobalTimeWallSpeed.Enabled = false;
                }
                else
                {
                    //at 10x, show wall
                    _btnGlobalTimeWallSpeed.Enabled = true;
                }
            }
        }

        private void _layoutPiece_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //cast sender as picturebox
                PictureBox s = (PictureBox)sender;

                //if sender data is not null (valid track piece / train piece)
                if(s.Tag != null)
                {
                    //Cast Tag to Data Container
                    LayoutCellDataContainer c = (LayoutCellDataContainer)s.Tag;

                    //Create right click menu
                    ContextMenu cm = new ContextMenu();

                    //Add Track Menu
                    MenuItem trackItem = new MenuItem("Track (ID: " + c.Block.BlockID + ")");
                    trackItem.Tag = c;
                    trackItem.MenuItems.Add("Open Track", HandleMenuClick);
                    trackItem.MenuItems.Add("Close Track", HandleMenuClick);
                    trackItem.MenuItems.Add("Display Track Info", HandleMenuClick);
                    cm.MenuItems.Add(trackItem);

                    //Add Train Menu if Train is contained by block
                    if (c.Train != null || true)
                    {
                        int trainID = -1;
                        if (c.Train != null)
                        {
                            trainID = c.Train.TrainID;
                        }
                        MenuItem trainItem = new MenuItem("Train (ID: " + trainID + ")");
                        trainItem.Tag = c;
                        trainItem.MenuItems.Add("Assign Train Route", HandleMenuClick);
                        trainItem.MenuItems.Add("Set Train Authority", HandleMenuClick);
                        trainItem.MenuItems.Add("Set Train Speed", HandleMenuClick);
                        trainItem.MenuItems.Add("Set Train OOS", HandleMenuClick);
                        trainItem.MenuItems.Add("Display Train Info", HandleMenuClick);
                        cm.MenuItems.Add(trainItem);
                    }
                    //Show the context menu at cursor click
                    cm.Show((Control)sender, new Point(e.X, e.Y));
                
                }//end if tag != null
            }//end if right click
        }

        private void HandleMenuClick(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            LayoutCellDataContainer c = (LayoutCellDataContainer)s.Tag;

            if (s.Text.CompareTo("Open Track")==0)
            {
               
            }
            else if (s.Text.CompareTo("Close Track") == 0)
            {

            }
            else if (s.Text.CompareTo("Display Track Info") == 0)
            {

            }
            else if (s.Text.CompareTo("Assign Train Route") == 0)
            {

            }
            else if (s.Text.CompareTo("Set Train Authority") == 0)
            {

            }
            else if (s.Text.CompareTo("Set Train Speed") == 0)
            {

            }
            else if (s.Text.CompareTo("Set Train OOS") == 0)
            {

            }
            else if (s.Text.CompareTo("Display Train Info") == 0)
            {

            }
            //else do noting
        }

        /// <summary>
        /// Function to set simulation speed to normal (wall speed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnGlobalTimeWallSpeed_Click(object sender, EventArgs e)
        {
            _environment.setInterval(_environment.getInterval()*10);
            _btnGlobalTimeWallSpeed.Enabled = false;
        }

        /// <summary>
        /// Function to set simulation speed to 10x normal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnGlobalTime10WallSpeed_Click(object sender, EventArgs e)
        {
            _environment.setInterval(_environment.getInterval()/10);
            _btnGlobalTimeWallSpeed.Enabled = true;
        }

        private void _btnGlobalTimeWallSpeed_EnabledChanged(object sender, EventArgs e)
        {
            //only allow 1 button to be controllable
            _btnGlobalTime10WallSpeed.Enabled = (!_btnGlobalTimeWallSpeed.Enabled);
        }

        public override void Refresh()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(this.Refresh));
                return;
            }
        }
    }//end ctc gui
}
