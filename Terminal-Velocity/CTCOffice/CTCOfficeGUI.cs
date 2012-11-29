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
        //TODO Create Speed Tool and Authority Tool

        private enum RoutingMode
        {
            Dispatch,
            Update
        }

        //Typical Globals
        private ISimulationEnvironment _environment;
        private CTCOffice _ctcOffice;
        private int _speedState;
        private LineData _redLineData;
        private LineData _greenLineData;

        //Global Time Extension
        private double _rate;
        private double _tickCount;

        //routing variables
        private bool _inRoutingPoint;
        private bool _routingToolOpen;
        private RoutingTool _routeTool;
        private RoutingMode _routeToolMode;
        public event EventHandler<EventArgs> RoutingToolResponse;

        //speedtool vars
        private double _newSpeed;
        private bool _speedToolOpen;
        private SpeedTool _speedTool;

        //authoritytool vars
        private int _newAuthority;
        private bool _authorityToolOpen;
        private AuthorityTool _authorityTool;

        //last item selected
        LayoutCellDataContainer _lastRightClickContainer;
        private EventHandler _layoutPiece_MouseHover;


        public event EventHandler<ShowTrainEventArgs> ShowTrain;

        #region Constructor
        public CTCOfficeGUI(ISimulationEnvironment env, CTCOffice ctc)
        {
            InitializeComponent();
            //set refs to ctc office and environment
            _ctcOffice = ctc;
            _environment = env;
            _speedState = 0;

            _rate = 100;
            _tickCount = 0;

            //init routing vars
            _routeTool = null;
            _inRoutingPoint = false;
            _routingToolOpen = false;
            _routeToolMode = RoutingMode.Dispatch;

            _lastRightClickContainer = null;
            
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

            //populate red line and green line panel
            parseLineData();

            //post to log that the gui has loaded
            _environment.sendLogEntry("CTCOffice: GUI Loaded");
        }
        #endregion

        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        private void parseLineData()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(this.parseLineData));
                return;
            }

            watch.Reset();
            watch.Start();

            int x = 0;
            int y = 0;

            
            LineData red = _ctcOffice.getLine(0);
            LineData green = _ctcOffice.getLine(1);

            if (_redLineData != red)
            {
                _redLineData = red;

                _panelRedLine.Controls.Clear();
                for (int i = 0; i <= _redLineData.Layout.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= _redLineData.Layout.GetUpperBound(1); j++)
                    {
                        PictureBox pane = new PictureBox();
                        _panelRedLine.Controls.Add(pane);
                        pane.Name = "_imgGridRed_" + i + "_" + j;
                        pane.SizeMode = PictureBoxSizeMode.CenterImage;
                        pane.Size = new Size(20, 20);
                        pane.Location = new Point(x, y);
                        pane.Image = _redLineData.Layout[i, j].Tile;
                        pane.Tag = _redLineData.Layout[i, j];
                        pane.MouseClick += new MouseEventHandler(this._layoutPiece_MouseClick);
                        //pane.MouseHover += new EventHandler(this._layoutPiece_MouseHover);
                        x += 20;
                    }
                    y += 20;
                    x = 0;
                }
                x = 0;
                y = 0;
            }//ed process red line

            if (_greenLineData != green)
            {
                _greenLineData = green;

                _panelGreenLine.Controls.Clear();
                for (int i = 0; i <= _greenLineData.Layout.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= _greenLineData.Layout.GetUpperBound(1); j++)
                    {
                        PictureBox pane = new PictureBox();
                        _panelGreenLine.Controls.Add(pane);
                        pane.Name = "_imgGridGreen_" + i + "_" + j;
                        pane.SizeMode = PictureBoxSizeMode.CenterImage;
                        pane.Size = new Size(20, 20);
                        pane.Location = new Point(x, y);
                        pane.Image = _greenLineData.Layout[i, j].Tile;
                        pane.Tag = _greenLineData.Layout[i, j];
                        pane.MouseClick += new MouseEventHandler(this._layoutPiece_MouseClick);
                        //pane.MouseHover += new EventHandler(this._layoutPiece_MouseHover);
                        x += 20;
                    }
                    y += 20;
                    x = 0;
                }
            }//end process green
            watch.Stop();
            Console.WriteLine(string.Format("{0} seconds {1} ms", watch.Elapsed.Seconds, watch.Elapsed.Milliseconds));
        }//end ParseLineData

        /// <summary>
        /// Function to handle Environment Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _environment_Tick(object sender, TickEventArgs e)
        {
            _tickCount++;
            if (_tickCount >= _rate)
            {
                //updateMetrics();
                _ctcOffice.PopulateTrack();
                parseLineData();
                _tickCount = 0;
            }
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

        #region Route Tool
        private void OpenRoutingTool(IBlock start, RoutingMode requestMode)
        {
            _routingToolOpen = true;
            _routeToolMode = requestMode;

            Form popup = new Form();
            RoutingTool rt = new RoutingTool(this, _ctcOffice, _environment, start);

            //set ctc gui ref
            _routeTool = rt;

            rt.EnablePointSelection += new EventHandler<EventArgs>(rt_EnablePointSelection);
            rt.SubmitRoute += new EventHandler<RoutingToolEventArgs>(rt_SubmitRoute);

            popup.Controls.Add(rt);
            popup.Text = "Routing Tool";
            popup.AutoSize = true;

            popup.FormClosed += new FormClosedEventHandler(popup_RoutingTool_FormClosed);
            popup.Show();
        }

        /// <summary>
        /// Event Sent by routing tool.. caught by ctc gui.  enables the point selection of a route
        /// </summary>
        /// <param name="sender">RoutingTool</param>
        /// <param name="e">Standard Event Args</param>
        void rt_EnablePointSelection(object sender, EventArgs e)
        {
            //enable left clicking on the blocks
            _inRoutingPoint = true;
            MessageBox.Show("Please select an end block for the point route");
        }

        private void rt_SubmitRoute(object sender, RoutingToolEventArgs e)
        {
            IRoute r = e.Route;

            if (_routeToolMode == RoutingMode.Dispatch)
            {
                //dispatch train request
                _ctcOffice.dispatchTrainRequest(r);

                //close routing tool
                if (_routeTool != null)
                {
                    _routeTool.ParentForm.Close();
                }
            }
            else if (_routeToolMode == RoutingMode.Update)
            {
                //assign route request
                _ctcOffice.assignTrainRouteRequest(
                    _lastRightClickContainer.Train.TrainID,
                    _lastRightClickContainer.Block.TrackCirID,
                    r,
                    _lastRightClickContainer.Block
                    );

                //close routing tool
                if (_routeTool != null)
                {
                    _routeTool.ParentForm.Close();
                }
            }
        }

        void popup_RoutingTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            _routeTool = null;
            _routingToolOpen = false;
        }
        #endregion

        #region Speed Tool
        private void OpenSpeedTool()
        {
            if (_speedToolOpen)
            {
                popup_SpeedTool_FormClosed(this, new FormClosedEventArgs(CloseReason.None));
            }

            _speedToolOpen = true;
            Form popup = new Form();
            SpeedTool st = new SpeedTool(this, _ctcOffice, _environment);
            _speedTool = st;

            st.SubmitSpeed += new EventHandler<SpeedToolEventArgs>(st_SubmitSpeed);
            popup.Controls.Add(st);
            popup.Text = "Speed Tool";
            popup.AutoSize = true;

            popup.FormClosed += new FormClosedEventHandler(popup_SpeedTool_FormClosed);
            popup.Show();
        }

        void st_SubmitSpeed(object sender, SpeedToolEventArgs e)
        {
            double speed = e.Speed;
            
            _ctcOffice.setTrainSpeedRequest(_lastRightClickContainer.Train.TrainID, _lastRightClickContainer.Block.TrackCirID, speed, _lastRightClickContainer.Block);
            if (_speedTool != null)
            {
                _speedTool.ParentForm.Close();
            }
        }

        void popup_SpeedTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            _speedTool = null;
            _speedToolOpen = false;
        }
        #endregion

        #region Authority Tool
        private void OpenAuthorityTool()
        {
            if (_authorityToolOpen)
            {
                popup_AuthorityTool_FormClosed(this, new FormClosedEventArgs(CloseReason.None));
            }

            _authorityToolOpen = true;

            Form popup = new Form();
            AuthorityTool at = new AuthorityTool(this, _ctcOffice, _environment);

            _authorityTool = at;
            at.SubmitAuthority += new EventHandler<AuthorityToolEventArgs>(at_SubmitAuthority);
            popup.Controls.Add(at);
            popup.Text = "Authority Tool";
            popup.AutoSize = true;


            popup.FormClosed += new FormClosedEventHandler(popup_AuthorityTool_FormClosed);
            popup.Show();
        }

        void at_SubmitAuthority(object sender, AuthorityToolEventArgs e)
        {
            int authority = e.Authority;
            _ctcOffice.setTrainAuthorityRequest(_lastRightClickContainer.Train.TrainID, _lastRightClickContainer.Block.TrackCirID, authority, _lastRightClickContainer.Block);
        }

        void popup_AuthorityTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            _authorityTool = null;
            _authorityToolOpen = false;
        }
        #endregion


        private void _btnDispatchTrain_Click(object sender, EventArgs e)
        {
            if (!_routingToolOpen)
            {
                //Todo -> verify Yard Line
                IBlock block = _environment.TrackModel.requestBlockInfo(0, "Red");
                OpenRoutingTool(block, RoutingMode.Dispatch);
            }
            //_ctcOffice.dispatchTrainRequest(route);
        }

        private void _btnRefreshView_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void _btnSchedule_1_Click(object sender, EventArgs e)
        {
            //TODO
            //show system scheduker
        }

        private void _btnSchedule_2_Click(object sender, EventArgs e)
        {
            //TODO
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
            if (e.Button == MouseButtons.Right && !_routingToolOpen)
            {
                //cast sender as picturebox
                PictureBox s = (PictureBox)sender;

                //if sender data is not null (valid track piece / train piece)
                if(s.Tag != null)
                {
                    //TODO check if block is null
                    //Cast Tag to Data Container
                    LayoutCellDataContainer c = (LayoutCellDataContainer)s.Tag;

                    if (c.Block != null)
                    {
                        //Create right click menu
                        ContextMenu cm = new ContextMenu();
                        List<string> trackMenuTitles = new List<string>();

                        //Add Track Menu
                        MenuItem trackItem = new MenuItem("Track (ID: " + c.Block.BlockID + ")");
                        trackItem.Tag = c;

                        trackMenuTitles.Add("Open Track");
                        trackMenuTitles.Add("Close Track");
                        //trackMenuTitles.Add("Display Track Info");

                        foreach (string t in trackMenuTitles)
                        {
                            MenuItem temp = new MenuItem();
                            temp.Text = t.ToString();
                            temp.Tag = c;
                            temp.Click += HandleMenuClick;
                            trackItem.MenuItems.Add(temp);
                        }

                        cm.MenuItems.Add(trackItem);


                        //Add Train Menu if Train is contained by block
                        if (c.Train != null)
                        {
                            int trainID = -1;
                            if (c.Train != null)
                            {
                                trainID = c.Train.TrainID;
                            }

                            MenuItem trainItem = new MenuItem("Train (ID: " + trainID + ")");
                            trainItem.Tag = c;
                            List<string> trainMenuTitles = new List<string>();

                            trainMenuTitles.Add("Assign Train Route");
                            trainMenuTitles.Add("Set Train Authority");
                            trainMenuTitles.Add("Set Train Speed");
                            trainMenuTitles.Add("Set Train OOS");
                            //trainMenuTitles.Add("Display Train Info");

                            foreach (string t in trainMenuTitles)
                            {
                                MenuItem temp = new MenuItem();
                                temp.Text = t.ToString();
                                temp.Tag = c;
                                temp.Click += HandleMenuClick;
                                trainItem.MenuItems.Add(temp);
                            }

                            cm.MenuItems.Add(trainItem);

                        }
                        //Show the context menu at cursor click
                        cm.Show((Control)sender, new Point(e.X, e.Y));
                    }
                }//end if tag != null
            }//end if right click
            else if (e.Button == MouseButtons.Left)
            {
                //only process info if in sub menu
                if (_inRoutingPoint && _routeTool!=null && _routingToolOpen)
                {
                    PictureBox s = (PictureBox)sender;
                    //if sender data is not null (valid track piece / train piece)
                    if (s.Tag != null)
                    {
                        //Cast Tag to Data Container
                        LayoutCellDataContainer c = (LayoutCellDataContainer)s.Tag;
                        if (c.Block != null)
                        {
                            _routeTool.EndBlock = c.Block;
                            RoutingToolResponse(this, EventArgs.Empty);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Block Selection. Please select another block!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Block Selection. Please select another block!");
                    }
                }//end if RoutingPoint
            }//end if Mouse Button
        }//end event

        private void HandleMenuClick(object sender, EventArgs e)
        {
            MenuItem s = (MenuItem)sender;
            LayoutCellDataContainer c = (LayoutCellDataContainer)s.Tag;

            //assing last right click container
            _lastRightClickContainer = c;

            if (s.Text.CompareTo("Open Track")==0)
            {
                _ctcOffice.openTrackBlockRequest(c.Block.TrackCirID, c.Block);
            }
            else if (s.Text.CompareTo("Close Track") == 0)
            {
                _ctcOffice.closeTrackBlockRequest(c.Block.TrackCirID, c.Block);
            }
            else if (s.Text.CompareTo("Display Track Info") == 0)
            {
                //TODO
                IBlock block = c.Block;
            }
            else if (s.Text.CompareTo("Assign Train Route") == 0)
            {
                //get route somehow
                OpenRoutingTool(c.Block, RoutingMode.Update);
                //_ctcOffice.assignTrainRouteRequest(c.Train.TrainID, c.Block.TrackCirID, null, c.Block);
            }
            else if (s.Text.CompareTo("Set Train Authority") == 0)
            {
                OpenAuthorityTool();
            }
            else if (s.Text.CompareTo("Set Train Speed") == 0)
            {
                OpenSpeedTool();
            }
            else if (s.Text.CompareTo("Set Train OOS") == 0)
            {
                _ctcOffice.setTrainOutOfServiceRequest(c.Train.TrainID, c.Block.TrackCirID, c.Block);
            }
            else if (s.Text.CompareTo("Display Train Info") == 0)
            {
                //TODO
                if (ShowTrain != null)
                {
                    ShowTrain(this, new ShowTrainEventArgs(c.Train));
                }
            }
            //else do noting
        }

        #region Global Time Button Handlers
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
        #endregion

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
