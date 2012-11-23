namespace CTCOffice
{
    partial class CTCOfficeGUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._groupLoginLogout = new System.Windows.Forms.GroupBox();
            this._loginStatusImage = new System.Windows.Forms.PictureBox();
            this._btnLoginLogout = new System.Windows.Forms.Button();
            this._txtPassword = new System.Windows.Forms.TextBox();
            this._lblPassword = new System.Windows.Forms.Label();
            this._lblUsername = new System.Windows.Forms.Label();
            this._txtUsername = new System.Windows.Forms.TextBox();
            this._groupOperatorControls = new System.Windows.Forms.GroupBox();
            this._btnRefreshView = new System.Windows.Forms.Button();
            this._btnDispatchTrain = new System.Windows.Forms.Button();
            this._btnSchedule_1 = new System.Windows.Forms.Button();
            this._groupSystemStatus = new System.Windows.Forms.GroupBox();
            this._groupRunningStatus = new System.Windows.Forms.GroupBox();
            this._lblEnvironmentHead = new System.Windows.Forms.Label();
            this._lblPTrackControllerHead = new System.Windows.Forms.Label();
            this._lblTrackModelStatusHead = new System.Windows.Forms.Label();
            this._lblSystemSchedulerStatusHead = new System.Windows.Forms.Label();
            this._lblCTCOfficeStatusHead = new System.Windows.Forms.Label();
            this._groupSystemMetrics = new System.Windows.Forms.GroupBox();
            this._lblCrewMetrics = new System.Windows.Forms.Label();
            this._lblSystemMetricsHeading = new System.Windows.Forms.Label();
            this._lblCrewHeading = new System.Windows.Forms.Label();
            this._btnRefreshMetrics = new System.Windows.Forms.Button();
            this._lblTotalMetrics = new System.Windows.Forms.Label();
            this._lblPassengersMetrics = new System.Windows.Forms.Label();
            this._lblTrainsMetrics = new System.Windows.Forms.Label();
            this._lblTotalLoadHeading = new System.Windows.Forms.Label();
            this._lblPassengersHeading = new System.Windows.Forms.Label();
            this._lblTrainsHeading = new System.Windows.Forms.Label();
            this._groupGlobalTimeControls = new System.Windows.Forms.GroupBox();
            this._btnGlobalTime10WallSpeed = new System.Windows.Forms.Button();
            this._btnGlobalTimeWallSpeed = new System.Windows.Forms.Button();
            this._lblSpeed = new System.Windows.Forms.Label();
            this._groupSystemSchedulerControls = new System.Windows.Forms.GroupBox();
            this._btnSchedule_2 = new System.Windows.Forms.Button();
            this._checkAutomatedScheduling = new System.Windows.Forms.CheckBox();
            this._systemViewTabs = new System.Windows.Forms.TabControl();
            this._tabRedLine = new System.Windows.Forms.TabPage();
            this._panelRedLine = new System.Windows.Forms.Panel();
            this._tabGreenLine = new System.Windows.Forms.TabPage();
            this._panelGreenLine = new System.Windows.Forms.Panel();
            this._tabTeamLogo = new System.Windows.Forms.TabPage();
            this._imageTeamLogo = new System.Windows.Forms.PictureBox();
            this.groupSystemNotifications = new System.Windows.Forms.GroupBox();
            this.listSystemNotifications = new System.Windows.Forms.ListBox();
            this._groupLoginLogout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loginStatusImage)).BeginInit();
            this._groupOperatorControls.SuspendLayout();
            this._groupSystemStatus.SuspendLayout();
            this._groupRunningStatus.SuspendLayout();
            this._groupSystemMetrics.SuspendLayout();
            this._groupGlobalTimeControls.SuspendLayout();
            this._groupSystemSchedulerControls.SuspendLayout();
            this._systemViewTabs.SuspendLayout();
            this._tabRedLine.SuspendLayout();
            this._tabGreenLine.SuspendLayout();
            this._tabTeamLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._imageTeamLogo)).BeginInit();
            this.groupSystemNotifications.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupLoginLogout
            // 
            this._groupLoginLogout.Controls.Add(this._loginStatusImage);
            this._groupLoginLogout.Controls.Add(this._btnLoginLogout);
            this._groupLoginLogout.Controls.Add(this._txtPassword);
            this._groupLoginLogout.Controls.Add(this._lblPassword);
            this._groupLoginLogout.Controls.Add(this._lblUsername);
            this._groupLoginLogout.Controls.Add(this._txtUsername);
            this._groupLoginLogout.Location = new System.Drawing.Point(3, 3);
            this._groupLoginLogout.Name = "_groupLoginLogout";
            this._groupLoginLogout.Size = new System.Drawing.Size(303, 60);
            this._groupLoginLogout.TabIndex = 1;
            this._groupLoginLogout.TabStop = false;
            this._groupLoginLogout.Text = "Login / Logout";
            // 
            // _loginStatusImage
            // 
            this._loginStatusImage.InitialImage = null;
            this._loginStatusImage.Location = new System.Drawing.Point(272, 9);
            this._loginStatusImage.Name = "_loginStatusImage";
            this._loginStatusImage.Size = new System.Drawing.Size(16, 16);
            this._loginStatusImage.TabIndex = 6;
            this._loginStatusImage.TabStop = false;
            // 
            // _btnLoginLogout
            // 
            this._btnLoginLogout.Location = new System.Drawing.Point(218, 29);
            this._btnLoginLogout.Name = "_btnLoginLogout";
            this._btnLoginLogout.Size = new System.Drawing.Size(75, 23);
            this._btnLoginLogout.TabIndex = 1;
            this._btnLoginLogout.Text = "Login";
            this._btnLoginLogout.UseVisualStyleBackColor = true;
            this._btnLoginLogout.Click += new System.EventHandler(this._btnLoginLogout_Click);
            // 
            // _txtPassword
            // 
            this._txtPassword.Location = new System.Drawing.Point(112, 32);
            this._txtPassword.Name = "_txtPassword";
            this._txtPassword.Size = new System.Drawing.Size(100, 20);
            this._txtPassword.TabIndex = 3;
            this._txtPassword.UseSystemPasswordChar = true;
            this._txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtPassword_KeyPress);
            // 
            // _lblPassword
            // 
            this._lblPassword.AutoSize = true;
            this._lblPassword.Location = new System.Drawing.Point(109, 16);
            this._lblPassword.Name = "_lblPassword";
            this._lblPassword.Size = new System.Drawing.Size(53, 13);
            this._lblPassword.TabIndex = 5;
            this._lblPassword.Text = "Password";
            // 
            // _lblUsername
            // 
            this._lblUsername.AutoSize = true;
            this._lblUsername.Location = new System.Drawing.Point(6, 16);
            this._lblUsername.Name = "_lblUsername";
            this._lblUsername.Size = new System.Drawing.Size(55, 13);
            this._lblUsername.TabIndex = 4;
            this._lblUsername.Text = "Username";
            // 
            // _txtUsername
            // 
            this._txtUsername.Location = new System.Drawing.Point(6, 32);
            this._txtUsername.Name = "_txtUsername";
            this._txtUsername.Size = new System.Drawing.Size(100, 20);
            this._txtUsername.TabIndex = 2;
            this._txtUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtUsername_KeyPress);
            // 
            // _groupOperatorControls
            // 
            this._groupOperatorControls.Controls.Add(this._btnRefreshView);
            this._groupOperatorControls.Controls.Add(this._btnDispatchTrain);
            this._groupOperatorControls.Controls.Add(this._btnSchedule_1);
            this._groupOperatorControls.Location = new System.Drawing.Point(312, 3);
            this._groupOperatorControls.Name = "_groupOperatorControls";
            this._groupOperatorControls.Size = new System.Drawing.Size(303, 60);
            this._groupOperatorControls.TabIndex = 2;
            this._groupOperatorControls.TabStop = false;
            this._groupOperatorControls.Text = "Operator Controls";
            // 
            // _btnRefreshView
            // 
            this._btnRefreshView.Location = new System.Drawing.Point(204, 29);
            this._btnRefreshView.Name = "_btnRefreshView";
            this._btnRefreshView.Size = new System.Drawing.Size(78, 23);
            this._btnRefreshView.TabIndex = 2;
            this._btnRefreshView.Text = "Refresh View";
            this._btnRefreshView.UseVisualStyleBackColor = true;
            this._btnRefreshView.Click += new System.EventHandler(this._btnRefreshView_Click);
            // 
            // _btnDispatchTrain
            // 
            this._btnDispatchTrain.Location = new System.Drawing.Point(6, 29);
            this._btnDispatchTrain.Name = "_btnDispatchTrain";
            this._btnDispatchTrain.Size = new System.Drawing.Size(86, 23);
            this._btnDispatchTrain.TabIndex = 0;
            this._btnDispatchTrain.Text = "Dispatch Train";
            this._btnDispatchTrain.UseVisualStyleBackColor = true;
            this._btnDispatchTrain.Click += new System.EventHandler(this._btnDispatchTrain_Click);
            // 
            // _btnSchedule_1
            // 
            this._btnSchedule_1.Location = new System.Drawing.Point(98, 29);
            this._btnSchedule_1.Name = "_btnSchedule_1";
            this._btnSchedule_1.Size = new System.Drawing.Size(100, 23);
            this._btnSchedule_1.TabIndex = 1;
            this._btnSchedule_1.Text = "Schedule Trains";
            this._btnSchedule_1.UseVisualStyleBackColor = true;
            this._btnSchedule_1.Click += new System.EventHandler(this._btnSchedule_1_Click);
            // 
            // _groupSystemStatus
            // 
            this._groupSystemStatus.Controls.Add(this._groupRunningStatus);
            this._groupSystemStatus.Controls.Add(this._groupSystemMetrics);
            this._groupSystemStatus.Controls.Add(this._groupGlobalTimeControls);
            this._groupSystemStatus.Controls.Add(this._groupSystemSchedulerControls);
            this._groupSystemStatus.Location = new System.Drawing.Point(996, 3);
            this._groupSystemStatus.Name = "_groupSystemStatus";
            this._groupSystemStatus.Size = new System.Drawing.Size(273, 706);
            this._groupSystemStatus.TabIndex = 7;
            this._groupSystemStatus.TabStop = false;
            this._groupSystemStatus.Text = "System Status";
            // 
            // _groupRunningStatus
            // 
            this._groupRunningStatus.Controls.Add(this._lblEnvironmentHead);
            this._groupRunningStatus.Controls.Add(this._lblPTrackControllerHead);
            this._groupRunningStatus.Controls.Add(this._lblTrackModelStatusHead);
            this._groupRunningStatus.Controls.Add(this._lblSystemSchedulerStatusHead);
            this._groupRunningStatus.Controls.Add(this._lblCTCOfficeStatusHead);
            this._groupRunningStatus.Location = new System.Drawing.Point(13, 19);
            this._groupRunningStatus.Name = "_groupRunningStatus";
            this._groupRunningStatus.Size = new System.Drawing.Size(252, 274);
            this._groupRunningStatus.TabIndex = 5;
            this._groupRunningStatus.TabStop = false;
            this._groupRunningStatus.Text = "System Module Status";
            // 
            // _lblEnvironmentHead
            // 
            this._lblEnvironmentHead.AutoSize = true;
            this._lblEnvironmentHead.Location = new System.Drawing.Point(11, 225);
            this._lblEnvironmentHead.Name = "_lblEnvironmentHead";
            this._lblEnvironmentHead.Size = new System.Drawing.Size(72, 13);
            this._lblEnvironmentHead.TabIndex = 4;
            this._lblEnvironmentHead.Text = "Environment: ";
            // 
            // _lblPTrackControllerHead
            // 
            this._lblPTrackControllerHead.AutoSize = true;
            this._lblPTrackControllerHead.Location = new System.Drawing.Point(11, 172);
            this._lblPTrackControllerHead.Name = "_lblPTrackControllerHead";
            this._lblPTrackControllerHead.Size = new System.Drawing.Size(125, 13);
            this._lblPTrackControllerHead.TabIndex = 3;
            this._lblPTrackControllerHead.Text = "Primary Track Controller: ";
            // 
            // _lblTrackModelStatusHead
            // 
            this._lblTrackModelStatusHead.AutoSize = true;
            this._lblTrackModelStatusHead.Location = new System.Drawing.Point(11, 120);
            this._lblTrackModelStatusHead.Name = "_lblTrackModelStatusHead";
            this._lblTrackModelStatusHead.Size = new System.Drawing.Size(73, 13);
            this._lblTrackModelStatusHead.TabIndex = 2;
            this._lblTrackModelStatusHead.Text = "Track Model: ";
            // 
            // _lblSystemSchedulerStatusHead
            // 
            this._lblSystemSchedulerStatusHead.AutoSize = true;
            this._lblSystemSchedulerStatusHead.Location = new System.Drawing.Point(11, 71);
            this._lblSystemSchedulerStatusHead.Name = "_lblSystemSchedulerStatusHead";
            this._lblSystemSchedulerStatusHead.Size = new System.Drawing.Size(98, 13);
            this._lblSystemSchedulerStatusHead.TabIndex = 1;
            this._lblSystemSchedulerStatusHead.Text = "System Scheduler: ";
            // 
            // _lblCTCOfficeStatusHead
            // 
            this._lblCTCOfficeStatusHead.AutoSize = true;
            this._lblCTCOfficeStatusHead.Location = new System.Drawing.Point(11, 28);
            this._lblCTCOfficeStatusHead.Name = "_lblCTCOfficeStatusHead";
            this._lblCTCOfficeStatusHead.Size = new System.Drawing.Size(65, 13);
            this._lblCTCOfficeStatusHead.TabIndex = 0;
            this._lblCTCOfficeStatusHead.Text = "CTC Office: ";
            // 
            // _groupSystemMetrics
            // 
            this._groupSystemMetrics.Controls.Add(this._lblCrewMetrics);
            this._groupSystemMetrics.Controls.Add(this._lblSystemMetricsHeading);
            this._groupSystemMetrics.Controls.Add(this._lblCrewHeading);
            this._groupSystemMetrics.Controls.Add(this._btnRefreshMetrics);
            this._groupSystemMetrics.Controls.Add(this._lblTotalMetrics);
            this._groupSystemMetrics.Controls.Add(this._lblPassengersMetrics);
            this._groupSystemMetrics.Controls.Add(this._lblTrainsMetrics);
            this._groupSystemMetrics.Controls.Add(this._lblTotalLoadHeading);
            this._groupSystemMetrics.Controls.Add(this._lblPassengersHeading);
            this._groupSystemMetrics.Controls.Add(this._lblTrainsHeading);
            this._groupSystemMetrics.Location = new System.Drawing.Point(13, 299);
            this._groupSystemMetrics.Name = "_groupSystemMetrics";
            this._groupSystemMetrics.Size = new System.Drawing.Size(252, 123);
            this._groupSystemMetrics.TabIndex = 4;
            this._groupSystemMetrics.TabStop = false;
            this._groupSystemMetrics.Text = "System Metrics";
            // 
            // _lblCrewMetrics
            // 
            this._lblCrewMetrics.AutoSize = true;
            this._lblCrewMetrics.Location = new System.Drawing.Point(68, 68);
            this._lblCrewMetrics.Name = "_lblCrewMetrics";
            this._lblCrewMetrics.Size = new System.Drawing.Size(13, 13);
            this._lblCrewMetrics.TabIndex = 9;
            this._lblCrewMetrics.Text = "0";
            // 
            // _lblSystemMetricsHeading
            // 
            this._lblSystemMetricsHeading.AutoSize = true;
            this._lblSystemMetricsHeading.Location = new System.Drawing.Point(10, 16);
            this._lblSystemMetricsHeading.Name = "_lblSystemMetricsHeading";
            this._lblSystemMetricsHeading.Size = new System.Drawing.Size(105, 13);
            this._lblSystemMetricsHeading.TabIndex = 0;
            this._lblSystemMetricsHeading.Text = "Current System Load";
            // 
            // _lblCrewHeading
            // 
            this._lblCrewHeading.AutoSize = true;
            this._lblCrewHeading.Location = new System.Drawing.Point(37, 65);
            this._lblCrewHeading.Name = "_lblCrewHeading";
            this._lblCrewHeading.Size = new System.Drawing.Size(34, 13);
            this._lblCrewHeading.TabIndex = 8;
            this._lblCrewHeading.Text = "Crew:";
            // 
            // _btnRefreshMetrics
            // 
            this._btnRefreshMetrics.Location = new System.Drawing.Point(171, 89);
            this._btnRefreshMetrics.Name = "_btnRefreshMetrics";
            this._btnRefreshMetrics.Size = new System.Drawing.Size(75, 23);
            this._btnRefreshMetrics.TabIndex = 7;
            this._btnRefreshMetrics.Text = "Refresh";
            this._btnRefreshMetrics.UseVisualStyleBackColor = true;
            this._btnRefreshMetrics.Click += new System.EventHandler(this._btnRefreshMetrics_Click);
            // 
            // _lblTotalMetrics
            // 
            this._lblTotalMetrics.AutoSize = true;
            this._lblTotalMetrics.Location = new System.Drawing.Point(68, 81);
            this._lblTotalMetrics.Name = "_lblTotalMetrics";
            this._lblTotalMetrics.Size = new System.Drawing.Size(13, 13);
            this._lblTotalMetrics.TabIndex = 6;
            this._lblTotalMetrics.Text = "0";
            // 
            // _lblPassengersMetrics
            // 
            this._lblPassengersMetrics.AutoSize = true;
            this._lblPassengersMetrics.Location = new System.Drawing.Point(68, 52);
            this._lblPassengersMetrics.Name = "_lblPassengersMetrics";
            this._lblPassengersMetrics.Size = new System.Drawing.Size(13, 13);
            this._lblPassengersMetrics.TabIndex = 5;
            this._lblPassengersMetrics.Text = "0";
            // 
            // _lblTrainsMetrics
            // 
            this._lblTrainsMetrics.AutoSize = true;
            this._lblTrainsMetrics.Location = new System.Drawing.Point(68, 39);
            this._lblTrainsMetrics.Name = "_lblTrainsMetrics";
            this._lblTrainsMetrics.Size = new System.Drawing.Size(13, 13);
            this._lblTrainsMetrics.TabIndex = 4;
            this._lblTrainsMetrics.Text = "0";
            // 
            // _lblTotalLoadHeading
            // 
            this._lblTotalLoadHeading.AutoSize = true;
            this._lblTotalLoadHeading.Location = new System.Drawing.Point(10, 81);
            this._lblTotalLoadHeading.Name = "_lblTotalLoadHeading";
            this._lblTotalLoadHeading.Size = new System.Drawing.Size(61, 13);
            this._lblTotalLoadHeading.TabIndex = 3;
            this._lblTotalLoadHeading.Text = "Total Load:";
            // 
            // _lblPassengersHeading
            // 
            this._lblPassengersHeading.AutoSize = true;
            this._lblPassengersHeading.Location = new System.Drawing.Point(6, 52);
            this._lblPassengersHeading.Name = "_lblPassengersHeading";
            this._lblPassengersHeading.Size = new System.Drawing.Size(65, 13);
            this._lblPassengersHeading.TabIndex = 2;
            this._lblPassengersHeading.Text = "Passengers:";
            // 
            // _lblTrainsHeading
            // 
            this._lblTrainsHeading.AutoSize = true;
            this._lblTrainsHeading.Location = new System.Drawing.Point(32, 39);
            this._lblTrainsHeading.Name = "_lblTrainsHeading";
            this._lblTrainsHeading.Size = new System.Drawing.Size(39, 13);
            this._lblTrainsHeading.TabIndex = 1;
            this._lblTrainsHeading.Text = "Trains:";
            // 
            // _groupGlobalTimeControls
            // 
            this._groupGlobalTimeControls.Controls.Add(this._btnGlobalTime10WallSpeed);
            this._groupGlobalTimeControls.Controls.Add(this._btnGlobalTimeWallSpeed);
            this._groupGlobalTimeControls.Controls.Add(this._lblSpeed);
            this._groupGlobalTimeControls.Location = new System.Drawing.Point(15, 428);
            this._groupGlobalTimeControls.Name = "_groupGlobalTimeControls";
            this._groupGlobalTimeControls.Size = new System.Drawing.Size(252, 77);
            this._groupGlobalTimeControls.TabIndex = 2;
            this._groupGlobalTimeControls.TabStop = false;
            this._groupGlobalTimeControls.Text = "Track Model";
            // 
            // _btnGlobalTime10WallSpeed
            // 
            this._btnGlobalTime10WallSpeed.Location = new System.Drawing.Point(129, 36);
            this._btnGlobalTime10WallSpeed.Name = "_btnGlobalTime10WallSpeed";
            this._btnGlobalTime10WallSpeed.Size = new System.Drawing.Size(93, 23);
            this._btnGlobalTime10WallSpeed.TabIndex = 2;
            this._btnGlobalTime10WallSpeed.Text = "10x Wall Speed";
            this._btnGlobalTime10WallSpeed.UseVisualStyleBackColor = true;
            this._btnGlobalTime10WallSpeed.Click += new System.EventHandler(this._btnGlobalTime10WallSpeed_Click);
            // 
            // _btnGlobalTimeWallSpeed
            // 
            this._btnGlobalTimeWallSpeed.Location = new System.Drawing.Point(12, 36);
            this._btnGlobalTimeWallSpeed.Name = "_btnGlobalTimeWallSpeed";
            this._btnGlobalTimeWallSpeed.Size = new System.Drawing.Size(75, 23);
            this._btnGlobalTimeWallSpeed.TabIndex = 1;
            this._btnGlobalTimeWallSpeed.Text = "Wall Speed";
            this._btnGlobalTimeWallSpeed.UseVisualStyleBackColor = true;
            this._btnGlobalTimeWallSpeed.EnabledChanged += new System.EventHandler(this._btnGlobalTimeWallSpeed_EnabledChanged);
            this._btnGlobalTimeWallSpeed.Click += new System.EventHandler(this._btnGlobalTimeWallSpeed_Click);
            // 
            // _lblSpeed
            // 
            this._lblSpeed.AutoSize = true;
            this._lblSpeed.Location = new System.Drawing.Point(6, 16);
            this._lblSpeed.Name = "_lblSpeed";
            this._lblSpeed.Size = new System.Drawing.Size(181, 13);
            this._lblSpeed.TabIndex = 0;
            this._lblSpeed.Text = "Global Time Speed - Click to Change";
            // 
            // _groupSystemSchedulerControls
            // 
            this._groupSystemSchedulerControls.Controls.Add(this._btnSchedule_2);
            this._groupSystemSchedulerControls.Controls.Add(this._checkAutomatedScheduling);
            this._groupSystemSchedulerControls.Location = new System.Drawing.Point(13, 511);
            this._groupSystemSchedulerControls.Name = "_groupSystemSchedulerControls";
            this._groupSystemSchedulerControls.Size = new System.Drawing.Size(252, 77);
            this._groupSystemSchedulerControls.TabIndex = 3;
            this._groupSystemSchedulerControls.TabStop = false;
            this._groupSystemSchedulerControls.Text = "System Scheduler Controls";
            // 
            // _btnSchedule_2
            // 
            this._btnSchedule_2.Location = new System.Drawing.Point(9, 42);
            this._btnSchedule_2.Name = "_btnSchedule_2";
            this._btnSchedule_2.Size = new System.Drawing.Size(132, 23);
            this._btnSchedule_2.TabIndex = 1;
            this._btnSchedule_2.Text = "Open System Scheduler";
            this._btnSchedule_2.UseVisualStyleBackColor = true;
            this._btnSchedule_2.Click += new System.EventHandler(this._btnSchedule_2_Click);
            // 
            // _checkAutomatedScheduling
            // 
            this._checkAutomatedScheduling.AutoSize = true;
            this._checkAutomatedScheduling.Location = new System.Drawing.Point(9, 19);
            this._checkAutomatedScheduling.Name = "_checkAutomatedScheduling";
            this._checkAutomatedScheduling.Size = new System.Drawing.Size(215, 17);
            this._checkAutomatedScheduling.TabIndex = 0;
            this._checkAutomatedScheduling.Text = "Enable / Disable Automated Scheduling";
            this._checkAutomatedScheduling.UseVisualStyleBackColor = true;
            this._checkAutomatedScheduling.CheckedChanged += new System.EventHandler(this._checkAutomatedScheduling_CheckedChanged);
            // 
            // _systemViewTabs
            // 
            this._systemViewTabs.Controls.Add(this._tabRedLine);
            this._systemViewTabs.Controls.Add(this._tabGreenLine);
            this._systemViewTabs.Controls.Add(this._tabTeamLogo);
            this._systemViewTabs.Location = new System.Drawing.Point(3, 69);
            this._systemViewTabs.Name = "_systemViewTabs";
            this._systemViewTabs.SelectedIndex = 0;
            this._systemViewTabs.Size = new System.Drawing.Size(987, 644);
            this._systemViewTabs.TabIndex = 8;
            // 
            // _tabRedLine
            // 
            this._tabRedLine.Controls.Add(this._panelRedLine);
            this._tabRedLine.Location = new System.Drawing.Point(4, 22);
            this._tabRedLine.Name = "_tabRedLine";
            this._tabRedLine.Padding = new System.Windows.Forms.Padding(3);
            this._tabRedLine.Size = new System.Drawing.Size(979, 618);
            this._tabRedLine.TabIndex = 0;
            this._tabRedLine.Text = "Red Line";
            this._tabRedLine.UseVisualStyleBackColor = true;
            // 
            // _panelRedLine
            // 
            this._panelRedLine.Location = new System.Drawing.Point(2, 3);
            this._panelRedLine.Name = "_panelRedLine";
            this._panelRedLine.Size = new System.Drawing.Size(975, 612);
            this._panelRedLine.TabIndex = 0;
            // 
            // _tabGreenLine
            // 
            this._tabGreenLine.Controls.Add(this._panelGreenLine);
            this._tabGreenLine.Location = new System.Drawing.Point(4, 22);
            this._tabGreenLine.Name = "_tabGreenLine";
            this._tabGreenLine.Padding = new System.Windows.Forms.Padding(3);
            this._tabGreenLine.Size = new System.Drawing.Size(979, 618);
            this._tabGreenLine.TabIndex = 1;
            this._tabGreenLine.Text = "Green Line";
            this._tabGreenLine.UseVisualStyleBackColor = true;
            // 
            // _panelGreenLine
            // 
            this._panelGreenLine.Location = new System.Drawing.Point(0, 3);
            this._panelGreenLine.Name = "_panelGreenLine";
            this._panelGreenLine.Size = new System.Drawing.Size(976, 612);
            this._panelGreenLine.TabIndex = 0;
            // 
            // _tabTeamLogo
            // 
            this._tabTeamLogo.Controls.Add(this._imageTeamLogo);
            this._tabTeamLogo.Location = new System.Drawing.Point(4, 22);
            this._tabTeamLogo.Name = "_tabTeamLogo";
            this._tabTeamLogo.Padding = new System.Windows.Forms.Padding(3);
            this._tabTeamLogo.Size = new System.Drawing.Size(979, 618);
            this._tabTeamLogo.TabIndex = 2;
            this._tabTeamLogo.Text = "Terminal Velocity";
            this._tabTeamLogo.UseVisualStyleBackColor = true;
            // 
            // _imageTeamLogo
            // 
            this._imageTeamLogo.BackColor = System.Drawing.Color.Black;
            this._imageTeamLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._imageTeamLogo.Location = new System.Drawing.Point(-4, 0);
            this._imageTeamLogo.Name = "_imageTeamLogo";
            this._imageTeamLogo.Size = new System.Drawing.Size(987, 622);
            this._imageTeamLogo.TabIndex = 0;
            this._imageTeamLogo.TabStop = false;
            // 
            // groupSystemNotifications
            // 
            this.groupSystemNotifications.Controls.Add(this.listSystemNotifications);
            this.groupSystemNotifications.Location = new System.Drawing.Point(621, 3);
            this.groupSystemNotifications.Name = "groupSystemNotifications";
            this.groupSystemNotifications.Size = new System.Drawing.Size(369, 60);
            this.groupSystemNotifications.TabIndex = 9;
            this.groupSystemNotifications.TabStop = false;
            this.groupSystemNotifications.Text = "System Notifications";
            // 
            // listSystemNotifications
            // 
            this.listSystemNotifications.FormattingEnabled = true;
            this.listSystemNotifications.Location = new System.Drawing.Point(6, 14);
            this.listSystemNotifications.Name = "listSystemNotifications";
            this.listSystemNotifications.Size = new System.Drawing.Size(357, 43);
            this.listSystemNotifications.TabIndex = 0;
            // 
            // CTCOfficeGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupSystemNotifications);
            this.Controls.Add(this._systemViewTabs);
            this.Controls.Add(this._groupSystemStatus);
            this.Controls.Add(this._groupOperatorControls);
            this.Controls.Add(this._groupLoginLogout);
            this.Name = "CTCOfficeGUI";
            this.Size = new System.Drawing.Size(1272, 716);
            this._groupLoginLogout.ResumeLayout(false);
            this._groupLoginLogout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loginStatusImage)).EndInit();
            this._groupOperatorControls.ResumeLayout(false);
            this._groupSystemStatus.ResumeLayout(false);
            this._groupRunningStatus.ResumeLayout(false);
            this._groupRunningStatus.PerformLayout();
            this._groupSystemMetrics.ResumeLayout(false);
            this._groupSystemMetrics.PerformLayout();
            this._groupGlobalTimeControls.ResumeLayout(false);
            this._groupGlobalTimeControls.PerformLayout();
            this._groupSystemSchedulerControls.ResumeLayout(false);
            this._groupSystemSchedulerControls.PerformLayout();
            this._systemViewTabs.ResumeLayout(false);
            this._tabRedLine.ResumeLayout(false);
            this._tabGreenLine.ResumeLayout(false);
            this._tabTeamLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._imageTeamLogo)).EndInit();
            this.groupSystemNotifications.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupLoginLogout;
        private System.Windows.Forms.PictureBox _loginStatusImage;
        private System.Windows.Forms.Button _btnLoginLogout;
        private System.Windows.Forms.TextBox _txtPassword;
        private System.Windows.Forms.Label _lblPassword;
        private System.Windows.Forms.Label _lblUsername;
        private System.Windows.Forms.TextBox _txtUsername;
        private System.Windows.Forms.GroupBox _groupOperatorControls;
        private System.Windows.Forms.Button _btnRefreshView;
        private System.Windows.Forms.Button _btnDispatchTrain;
        private System.Windows.Forms.Button _btnSchedule_1;
        private System.Windows.Forms.GroupBox _groupSystemStatus;
        private System.Windows.Forms.GroupBox _groupRunningStatus;
        private System.Windows.Forms.Label _lblEnvironmentHead;
        private System.Windows.Forms.Label _lblPTrackControllerHead;
        private System.Windows.Forms.Label _lblTrackModelStatusHead;
        private System.Windows.Forms.Label _lblSystemSchedulerStatusHead;
        private System.Windows.Forms.Label _lblCTCOfficeStatusHead;
        private System.Windows.Forms.GroupBox _groupSystemMetrics;
        private System.Windows.Forms.Button _btnRefreshMetrics;
        private System.Windows.Forms.Label _lblTotalMetrics;
        private System.Windows.Forms.Label _lblPassengersMetrics;
        private System.Windows.Forms.Label _lblTrainsMetrics;
        private System.Windows.Forms.Label _lblTotalLoadHeading;
        private System.Windows.Forms.Label _lblPassengersHeading;
        private System.Windows.Forms.Label _lblTrainsHeading;
        private System.Windows.Forms.Label _lblSystemMetricsHeading;
        private System.Windows.Forms.GroupBox _groupGlobalTimeControls;
        private System.Windows.Forms.Label _lblSpeed;
        private System.Windows.Forms.GroupBox _groupSystemSchedulerControls;
        private System.Windows.Forms.Button _btnSchedule_2;
        private System.Windows.Forms.CheckBox _checkAutomatedScheduling;
        private System.Windows.Forms.TabControl _systemViewTabs;
        private System.Windows.Forms.TabPage _tabRedLine;
        private System.Windows.Forms.TabPage _tabGreenLine;
        private System.Windows.Forms.TabPage _tabTeamLogo;
        private System.Windows.Forms.GroupBox groupSystemNotifications;
        private System.Windows.Forms.ListBox listSystemNotifications;
        private System.Windows.Forms.Panel _panelRedLine;
        private System.Windows.Forms.Panel _panelGreenLine;
        private System.Windows.Forms.PictureBox _imageTeamLogo;
        private System.Windows.Forms.Button _btnGlobalTime10WallSpeed;
        private System.Windows.Forms.Button _btnGlobalTimeWallSpeed;
        private System.Windows.Forms.Label _lblCrewHeading;
        private System.Windows.Forms.Label _lblCrewMetrics;

    }
}
