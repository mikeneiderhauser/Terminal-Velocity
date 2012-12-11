using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Interfaces;
using Utility;
using Utility.Properties;

namespace CTCOffice
{
    public partial class CTCOfficeGUI : UserControl
    {
        #region Private Variables (Some Public Events)
        //System Hooks
        private CTCOffice _ctcOffice;
        private ISimulationEnvironment _environment;
        private ResourceWrapper _res;

        //CTCOffice Data Containers
        private LineData _greenLineData;
        private LineData _redLineData;

        //Timer Data
        private double _rate;
        private double _tickCount;
        private int _speedState;

        //Authority Tool
        private AuthorityTool _authorityTool;
        private bool _authorityToolOpen;

        //Routing Tool
        /// <summary>
        /// Enumeration of Route methods.
        /// </summary>
        private enum RoutingMode
        {
            Dispatch,
            Update
        }

        public event EventHandler<EventArgs> RoutingToolResponse;
        private bool _inRoutingPoint;
        private LayoutCellDataContainer _lastRightClickContainer;
        private RoutingTool _routeTool;
        private RoutingMode _routeToolMode;
        private bool _routingToolOpen;

        //Speed Tool
        private SpeedTool _speedTool;
        private bool _speedToolOpen;

        //Key / Legend 
        private bool _keyOpen;
        private Form _keyForm;

        private bool _loginState;

        //Public Events To Hook to the Environment
        public event EventHandler<ShowTrainEventArgs> ShowTrain;
        public event EventHandler<EventArgs> ShowSchedule;

        #endregion

        #region Constructor + Environment Tick

        public CTCOfficeGUI(ISimulationEnvironment env, CTCOffice ctc)
        {
            InitializeComponent();
            //set refs to ctc office and environment
            _ctcOffice = ctc;
            _environment = env;
            _speedState = 0;

            _res = _ctcOffice.Resource;

            _ctcOffice.LoadData += new EventHandler<EventArgs>(_ctcOffice_LoadData);
            _ctcOffice.UnlockLogin += new EventHandler<EventArgs>(_ctcOffice_UnlockLogin);
            _ctcOffice.MessagesReady += new EventHandler<EventArgs>(_ctcOffice_MessagesReady);
            _ctcOffice.UpdatedData += new EventHandler<EventArgs>(_ctcOffice_UpdatedData);
            _loginState = false;

            _btnLoginLogout.Enabled = _loginState;
            _txtPassword.Enabled = _loginState;
            _txtUsername.Enabled = _loginState;

            _rate = 100;
            _tickCount = 0;

            //init routing vars
            _routeTool = null;
            _inRoutingPoint = false;
            _routingToolOpen = false;
            _routeToolMode = RoutingMode.Dispatch;

            _lastRightClickContainer = null;

            _keyForm = null;
            _keyOpen = false;

            //subscribe to Environment Tick
            _environment.Tick += _environment_Tick;

            //ensure the user is logged out
            _ctcOffice.Logout();
            //change button text
            _btnLoginLogout.Text = "Login";


            //show team logo (block out user)
            MainDisplayLogo();
            DisableUserControls();
            _loginStatusImage.Image = _res.RedLight;
            _imageTeamLogo.Image = Properties.Resources.TerminalVelocity;

            UpdateMetrics();
            RefreshStatus();

            //populate red line and green line panel
            //parseLineData();

            //post to log that the gui has loaded
            _environment.sendLogEntry("CTCOffice: GUI Loaded");
        }

        /// <summary>
        /// Function to handle Environment Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _environment_Tick(object sender, TickEventArgs e)
        {
            _tickCount++;
            RefreshStatus();
            if ((_tickCount >= _rate))
            {
                UpdateMetrics();

                _tickCount = 0;
            }
        }
        #endregion

        #region Load Line Information
        void _ctcOffice_LoadData(object sender, EventArgs e)
        {
            ParseLineData();
        }


        /// <summary>
        /// Takes Line Data From CTC Office and makes interaction possible
        /// </summary>
        private void ParseLineData()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(ParseLineData));
                return;
            }

            int x = 0;
            int y = 0;


            LineData red = _ctcOffice.GetLine(0);
            LineData green = _ctcOffice.GetLine(1);

            if (_redLineData != red)
            {
                _redLineData = red;

                _panelRedLine.Controls.Clear();
                for (int i = 0; i <= _redLineData.Layout.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= _redLineData.Layout.GetUpperBound(1); j++)
                    {
                        MyPictureBox pane = new MyPictureBox(_panelRedLine,this);
                        _panelRedLine.Controls.Add(pane);
                        pane.Name = "_imgGridRed_" + i + "_" + j;
                        pane.SizeMode = PictureBoxSizeMode.CenterImage;
                        pane.Size = new Size(20, 20);
                        pane.Location = new Point(x, y);
                        pane.Image = _redLineData.Layout[i, j].Tile;
                        pane.Tag = _redLineData.Layout[i, j];
                        _redLineData.Layout[i, j].Panel = pane;
                        pane.MouseClick += _layoutPiece_MouseClick;
                        //pane.MouseHover += new EventHandler(this._layoutPiece_MouseHover);
                        x += 20;
                    }
                    y += 20;
                    x = 0;
                }
                x = 0;
                y = 0;
            } //ed process red line

            if (_greenLineData != green)
            {
                _greenLineData = green;

                _panelGreenLine.Controls.Clear();
                for (int i = 0; i <= _greenLineData.Layout.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= _greenLineData.Layout.GetUpperBound(1); j++)
                    {
                        MyPictureBox pane = new MyPictureBox(_panelGreenLine,this);
                        _panelGreenLine.Controls.Add(pane);
                        pane.Name = "_imgGridGreen_" + i + "_" + j;
                        pane.SizeMode = PictureBoxSizeMode.CenterImage;
                        pane.Size = new Size(20, 20);
                        pane.Location = new Point(x, y);
                        pane.Image = _greenLineData.Layout[i, j].Tile;
                        pane.Tag = _greenLineData.Layout[i, j];
                        
                        _greenLineData.Layout[i, j].Panel = pane;
                        pane.MouseClick += _layoutPiece_MouseClick;
                        //pane.MouseHover += new EventHandler(this._layoutPiece_MouseHover);
                        x += 20;
                    }
                    y += 20;
                    x = 0;
                }
            } //end process green
        }//end ParseLineData

        #endregion

        #region Refresh Controls and Functions

        private void _ctcOffice_MessagesReady(object sender, EventArgs e)
        {
            if (listSystemNotifications.Items.Count > 100)
            {
                listSystemNotifications.Items.Clear();
            }
            listSystemNotifications.Items.AddRange(_ctcOffice.SystemMessages.ToArray());
            _ctcOffice.SystemMessages.Clear();
        }

        private void _ctcOffice_UpdatedData(object sender, EventArgs e)
        {
            _ctcOffice.PopulateTrack();
        }

        /// <summary>
        /// Button To force Refresh GUI (may not be needed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnRefreshView_Click(object sender, EventArgs e)
        {
            if (_systemViewTabs.SelectedIndex == 0)
            {
                //refresh red line
                foreach (Control c in _panelRedLine.Controls)
                {
                    c.Refresh();
                }
            }

            if (_systemViewTabs.SelectedIndex == 1)
            {
                //refresh green line
                foreach (Control c in _panelGreenLine.Controls)
                {
                    c.Refresh();
                }
            }

            UpdateMetrics();
            RefreshStatus();
        }

        /// <summary>
        /// Button to refresh system metrics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnRefreshMetrics_Click(object sender, EventArgs e)
        {
            UpdateMetrics();
        }

        /// <summary>
        /// Override Graphics Refresh (Possibly Not Needed)
        /// </summary>
        public override void Refresh()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(Refresh));
                return;
            }
        }

        /// <summary>
        /// Function to update System Metrics
        /// </summary>
        private void UpdateMetrics()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(UpdateMetrics));
                return;
            }

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

        private void RefreshStatus()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(RefreshStatus));
                return;
            }

            #region Environment Status
            if (_environment != null)
            {
                _imgSysStatusEnvironment.Image = _res.GreenLight;
            }
            else
            {
                _imgSysStatusEnvironment.Image = _res.RedLight;
            }
            #endregion

            #region Track Model Status
            if (_environment.TrackModel != null)
            {
                if (_environment.TrackModel.RedLoaded && _environment.TrackModel.GreenLoaded)
                {
                    _imgSysStatusTrackModel.Image = _res.GreenLight;
                }
                else if (_environment.TrackModel.RedLoaded || _environment.TrackModel.GreenLoaded)
                {
                    _imgSysStatusTrackModel.Image = _res.YellowLight;
                }
                else
                {
                    _imgSysStatusTrackModel.Image = _res.RedLight;
                }
            }
            else
            {
                _imgSysStatusTrackModel.Image = _res.RedLight;
            }
            #endregion

            #region Primary Track Controller (Red) Status
            if (_environment.PrimaryTrackControllerRed != null)
            {
                _imgSysStatusTrackControllerRed.Image = _res.GreenLight;
            }
            else
            {
                _imgSysStatusTrackControllerRed.Image = _res.RedLight;
            }
            #endregion

            #region Primary Track Controller (Green) Status
            if (_environment.PrimaryTrackControllerGreen != null)
            {
                _imgSysStatusTrackControllerGreen.Image = _res.GreenLight;
            }
            else
            {
                _imgSysStatusTrackControllerGreen.Image = _res.RedLight;
            }
            #endregion

            #region CTC Office Status
            if (_environment.CTCOffice != null)
            {
                _imgSysStatusCTCOffice.Image = _res.GreenLight;
            }
            else
            {
                _imgSysStatusCTCOffice.Image = _res.RedLight;
            }
            #endregion

            #region System Scheduler Status
            if (_environment.SystemScheduler != null)
            {
                _imgSysStatusSystemScheduler.Image = _res.GreenLight;
            }
            else
            {
                _imgSysStatusSystemScheduler.Image = _res.RedLight;
            }
            #endregion
        }

        #endregion

        #region Login Control Functions
        /// <summary>
        /// Handles Keyboard "Enter" while in Username field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles Keyboard "Enter" while in Password field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        private void _ctcOffice_UnlockLogin(object sender, EventArgs e)
        {
            LocalUnlockLogin();
        }

        private void LocalUnlockLogin()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(LocalUnlockLogin));
                return;
            }

            if (!_loginState)
            {
                _loginState = true;
                _btnLoginLogout.Enabled = _loginState;
                _txtPassword.Enabled = _loginState;
                _txtUsername.Enabled = _loginState;
                //unsubscribe from unlock event
                _ctcOffice.UnlockLogin -= _ctcOffice_UnlockLogin;
            }
        }

        /// <summary>
        /// Handles User interaction with login section of form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnLoginLogout_Click(object sender, EventArgs e)
        {
            if (_ctcOffice.IsAuth())
            {
                //if logged in.. log out
                _ctcOffice.Logout();
                //disable user controls (lock out op)
                DisableUserControls();
                _loginStatusImage.Image = _res.RedLight;
                _btnLoginLogout.Text = "Login";
                MainDisplayLogo();
                //post to log
                _environment.sendLogEntry("CTCOffice: Operator Logged Out!");
            }
            else
            {
                //if logged out, login
                if (_ctcOffice.Login(_txtUsername.Text, _txtPassword.Text))
                {
                    //if login pass (enable controls)
                    EnableUserControls();
                    _loginStatusImage.Image = _res.GreenLight;
                    //show red line tab
                    ShowRedLine();
                    //remove password
                    _txtPassword.Text = "";
                    //change button txt
                    _btnLoginLogout.Text = "Logout";
                }
                else
                {
                    //if login fail (disable controls)
                    DisableUserControls();
                    _loginStatusImage.Image = _res.RedLight;
                    _btnLoginLogout.Text = "Login";
                    //post to log
                    _environment.sendLogEntry("CTCOffice: Operator Login Failed -> UnAuthorized!");
                    //show logo
                    MainDisplayLogo();
                    //tell user invalid creds
                    MessageBox.Show("Invalid Credentials", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }//end login/logout button click
        #endregion

        #region Control Enable/Disable Functions
        /// <summary>
        /// Disables User controls (Except Login Controls)
        /// </summary>
        private void DisableUserControls()
        {
            MainDisplayLogo();
            SetControlState(false);
        }

        /// <summary>
        /// Enables User controls
        /// </summary>
        private void EnableUserControls()
        {
            SetControlState(true);
        }

        /// <summary>
        /// Forces tab view to show Terminal Velocity Logo
        /// </summary>
        private void MainDisplayLogo()
        {
            _systemViewTabs.SelectedIndex = 2; //Terminal Velocity Tab
        }

        /// <summary>
        /// Forces Red Line Tab View to Display
        /// </summary>
        private void ShowRedLine()
        {
            _systemViewTabs.SelectedIndex = 0; //Red line
        }

        /// <summary>
        /// Forces Green Line Tab View To Show
        /// </summary>
        private void ShowGreenLine()
        {
            _systemViewTabs.SelectedIndex = 1; //Green Line
        }

        /// <summary>
        /// Method to set enabled state of most controls in GUI
        /// </summary>
        /// <param name="state"></param>
        private void SetControlState(bool state)
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
            _btnShowKey.Enabled = state;

            //handle simulation time event handlers
            if (state)
            {
                _btnGlobalTimeWallSpeed.Click += new EventHandler(_btnGlobalTimeWallSpeed_Click);
                _btnGlobalTime10WallSpeed.Click += new EventHandler(_btnGlobalTime10WallSpeed_Click);

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
            else
            {
                _btnGlobalTimeWallSpeed.Click -= new EventHandler(_btnGlobalTimeWallSpeed_Click);
                _btnGlobalTime10WallSpeed.Click -= new EventHandler(_btnGlobalTime10WallSpeed_Click);
            }
        }
        #endregion

        #region Global Time Button Handler
        /// <summary>
        ///     Function to set simulation speed to normal (wall speed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnGlobalTimeWallSpeed_Click(object sender, EventArgs e)
        {
            _environment.setInterval(_environment.getInterval()*10);
            _btnGlobalTimeWallSpeed.Enabled = false;
        }

        /// <summary>
        ///     Function to set simulation speed to 10x normal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnGlobalTime10WallSpeed_Click(object sender, EventArgs e)
        {
            _environment.setInterval(_environment.getInterval()/10);
            _btnGlobalTimeWallSpeed.Enabled = true;
        }

        /// <summary>
        /// Handles only displaying 1 button at a time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnGlobalTimeWallSpeed_EnabledChanged(object sender, EventArgs e)
        {
            //only allow 1 button to be controllable
            _btnGlobalTime10WallSpeed.Enabled = (!_btnGlobalTimeWallSpeed.Enabled);
        }
        #endregion

        #region Mouse Actions
        /// <summary>
        /// Handle Mouse Clicks in the Graphics Panel (generates Context Menus)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _layoutPiece_MouseClick(object sender, MouseEventArgs e)
        {
            //TODO - REVIEW AND VERIFY
            if (e.Button == MouseButtons.Right && !_routingToolOpen)
            {
                //cast sender as picturebox
                var s = (PictureBox)sender;

                //if sender data is not null (valid track piece / train piece)
                if (s.Tag != null)
                {
                    //TODO check if block is null
                    //Cast Tag to Data Container
                    var c = (LayoutCellDataContainer)s.Tag;

                    if (c.Block != null)
                    {
                        //Create right click menu
                        var cm = new ContextMenu();
                        var trackMenuTitles = new List<string>();

                        //Add Track Menu
                        var trackItem = new MenuItem("Track (ID: " + c.Block.BlockID + ")");
                        trackItem.Tag = c;

                        if (c.Block.State == StateEnum.Healthy)
                        {
                            trackMenuTitles.Add("Close Track");
                        }
                        else
                        {
                            trackMenuTitles.Add("Open Track");
                        }
                        
                        
                        //trackMenuTitles.Add("Display Track Info");

                        foreach (string t in trackMenuTitles)
                        {
                            var temp = new MenuItem();
                            temp.Text = t;
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

                            var trainItem = new MenuItem("Train (ID: " + trainID + ")");
                            trainItem.Tag = c;
                            var trainMenuTitles = new List<string>();

                            trainMenuTitles.Add("Assign Train Route");
                            trainMenuTitles.Add("Set Train Authority");
                            trainMenuTitles.Add("Set Train Speed");
                            trainMenuTitles.Add("Set Train OOS");
                            trainMenuTitles.Add("Display Train Info");

                            foreach (string t in trainMenuTitles)
                            {
                                var temp = new MenuItem();
                                temp.Text = t;
                                temp.Tag = c;
                                temp.Click += HandleMenuClick;
                                trainItem.MenuItems.Add(temp);
                            }

                            cm.MenuItems.Add(trainItem);
                        }
                        //Show the context menu at cursor click
                        cm.Show((Control)sender, new Point(e.X, e.Y));
                    }
                } //end if tag != null
            } //end if right click
            else if (e.Button == MouseButtons.Left)
            {
                //only process info if in sub menu
                if (_inRoutingPoint && _routeTool != null && _routingToolOpen)
                {
                    var s = (PictureBox)sender;
                    //if sender data is not null (valid track piece / train piece)
                    if (s.Tag != null)
                    {
                        //Cast Tag to Data Container
                        var c = (LayoutCellDataContainer)s.Tag;
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
                } //end if RoutingPoint
            } //end if Mouse Button
        }

        /// <summary>
        /// Creates the request that is sent to the PTC's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMenuClick(object sender, EventArgs e)
        {
            var s = (MenuItem)sender;
            var c = (LayoutCellDataContainer)s.Tag;

            //assing last right click container
            _lastRightClickContainer = c;

            if (s.Text.CompareTo("Open Track") == 0)
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
                //TODO
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
                if (ShowTrain != null)
                {
                    ShowTrain(this, new ShowTrainEventArgs(c.Train));
                }
            }
            //else do noting
        }

        #endregion

        #region Dispatch / Scheduler Controls

        /// <summary>
        /// HandlesvButton to dispatch train
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Throws enent to show the system scheduler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnSchedule_1_Click(object sender, EventArgs e)
        {
            //show system scheduker
            if (ShowSchedule != null)
            {
                ShowSchedule(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Throws enent to show the system scheduler (calles _btnSchedule_1_Clcik)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnSchedule_2_Click(object sender, EventArgs e)
        {
            _btnSchedule_1_Click(sender, e);
        }

        /// <summary>
        /// Handles Scheduling Checkbox and sends message to CTC Office to enable automated scheduling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkAutomatedScheduling_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkAutomatedScheduling.Checked)
            {
                _ctcOffice.StartScheduling();
                _systemSchedulerIndicator.Image = _res.GreenLight;
            }
            else
            {
                _ctcOffice.StopScheduling();
                _systemSchedulerIndicator.Image = null;

            }
        }

        #endregion

        #region Routing Tool

        private void OpenRoutingTool(IBlock start, RoutingMode requestMode)
        {
            _routingToolOpen = true;
            _routeToolMode = requestMode;

            var popup = new Form();
            popup.Text = "Routing Tool";
            popup.AutoSize = true;
            popup.FormClosed += Popup_RoutingTool_FormClosed;

            var rt = new RoutingTool(this, _ctcOffice, _environment, start);
            //set ctc gui ref
            _routeTool = rt;
            rt.EnablePointSelection += Rt_EnablePointSelection;
            rt.SubmitRoute += Rt_SubmitRoute;

            popup.Controls.Add(rt);
            popup.Show();
        }

        /// <summary>
        ///     Event Sent by routing tool.. caught by ctc gui.  enables the point selection of a route
        /// </summary>
        /// <param name="sender">RoutingTool</param>
        /// <param name="e">Standard Event Args</param>
        private void Rt_EnablePointSelection(object sender, EventArgs e)
        {
            //enable left clicking on the blocks
            _inRoutingPoint = true;
            MessageBox.Show("Please select an end block for the point route");
        }

        private void Rt_SubmitRoute(object sender, RoutingToolEventArgs e)
        {
            IRoute r = e.Route;

            if (_routeToolMode == RoutingMode.Dispatch)
            {
                //dispatch train request
                _ctcOffice.dispatchTrainRequest(r);
                MessageBox.Show("Routing Selection Complete (Dispatch). Sending Request");

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

                MessageBox.Show("Routing Selection Complete. Sending Request");

                //close routing tool
                if (_routeTool != null)
                {
                    _routeTool.ParentForm.Close();
                }
            }
        }

        private void Popup_RoutingTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            _routeTool = null;
            _routingToolOpen = false;
            _popupIndicatorCTC.Image = null;
            
        }

        #endregion

        #region Speed Tool

        private void OpenSpeedTool()
        {
            if (_speedToolOpen)
            {
                Popup_SpeedTool_FormClosed(this, new FormClosedEventArgs(CloseReason.None));
            }

            _speedToolOpen = true;
            var popup = new Form();
            popup.Text = "Speed Tool";
            popup.AutoSize = true;
            popup.FormClosed += Popup_SpeedTool_FormClosed;

            var st = new SpeedTool(this, _ctcOffice, _environment);
            _speedTool = st;
            st.SubmitSpeed += St_SubmitSpeed;

            popup.Controls.Add(st);
            _popupIndicatorCTC.Image = _res.GreenLight;
            popup.Show();
        }

        private void St_SubmitSpeed(object sender, SpeedToolEventArgs e)
        {
            double speed = e.Speed;

            _ctcOffice.setTrainSpeedRequest(_lastRightClickContainer.Train.TrainID,
                                            _lastRightClickContainer.Block.TrackCirID, speed,
                                            _lastRightClickContainer.Block);

            MessageBox.Show("New Speed Request Sent");

            if (_speedTool != null)
            {
                _speedTool.ParentForm.Close();
            }
        }

        private void Popup_SpeedTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            _speedTool = null;
            _speedToolOpen = false;
            _popupIndicatorCTC.Image = null;
        }

        #endregion

        #region Authority Tool

        private void OpenAuthorityTool()
        {
            if (_authorityToolOpen)
            {
                Popup_AuthorityTool_FormClosed(this, new FormClosedEventArgs(CloseReason.None));
            }

            _authorityToolOpen = true;

            var popup = new Form();
            popup.AutoSize = true;
            popup.Text = "Authority Tool";
            popup.FormClosed += Popup_AuthorityTool_FormClosed;

            var at = new AuthorityTool(this, _ctcOffice, _environment);
            _authorityTool = at;
            at.SubmitAuthority += At_SubmitAuthority;

            popup.Controls.Add(at);
            _popupIndicatorCTC.Image = _res.GreenLight;
            popup.Show();
        }

        private void At_SubmitAuthority(object sender, AuthorityToolEventArgs e)
        {
            int authority = e.Authority;
            _ctcOffice.setTrainAuthorityRequest(_lastRightClickContainer.Train.TrainID,
                                                _lastRightClickContainer.Block.TrackCirID, authority,
                                                _lastRightClickContainer.Block);

            MessageBox.Show("New Authority Request Sent");

            if (_authorityTool != null)
            {
                _speedTool.ParentForm.Close();
            }
        }

        private void Popup_AuthorityTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            _authorityTool = null;
            _authorityToolOpen = false;
            _popupIndicatorCTC.Image = null;
        }

        #endregion

        #region Key / Legend Tool
        private void _btnShowKey_Click(object sender, EventArgs e)
        {
            if (!_keyOpen)
            {
                _keyForm = new Form();
                UserControl keyInfo = new KeyInfo(_res);
                _keyForm.AutoSize = true;
                _keyForm.Text = "CTC Office Legend Key";
                _keyForm.FormClosed += new FormClosedEventHandler(_keyForm_FormClosed);
                _keyForm.Controls.Add(keyInfo);
                _keyOpen = true;
                _popupIndicatorCTC.Image = _res.GreenLight;
                _keyForm.Show();
            }
            else
            {
                //display if already open
                _keyForm.TopMost = true;
                _keyForm.TopMost = false;
            }
        }

        private void _keyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _keyOpen = false;
            _keyForm = null;
            _popupIndicatorCTC.Image = null;
        }
        #endregion
    }

//end ctc gui
}