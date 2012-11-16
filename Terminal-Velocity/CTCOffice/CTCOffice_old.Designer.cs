namespace CTCOffice
{
    partial class CTCOffice_old
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._groupLoginLogout = new System.Windows.Forms.GroupBox();
            this.loginStatusImage = new System.Windows.Forms.PictureBox();
            this._btnLoginLogout = new System.Windows.Forms.Button();
            this._txtPassword = new System.Windows.Forms.TextBox();
            this._lblPassword = new System.Windows.Forms.Label();
            this._lblUsername = new System.Windows.Forms.Label();
            this._txtUsername = new System.Windows.Forms.TextBox();
            this._groupOperatorControls = new System.Windows.Forms.GroupBox();
            this._btnRefreshView = new System.Windows.Forms.Button();
            this._btnSchedule_1 = new System.Windows.Forms.Button();
            this._btnDispatchTrain = new System.Windows.Forms.Button();
            this._groupGlobalTimeControls = new System.Windows.Forms.GroupBox();
            this._lblSpeedDetail = new System.Windows.Forms.Label();
            this._btnSpeed = new System.Windows.Forms.Button();
            this.txtGlobalTimeArea = new System.Windows.Forms.TextBox();
            this._lblSpeed = new System.Windows.Forms.Label();
            this._groupSystemSchedulerControls = new System.Windows.Forms.GroupBox();
            this._btnSchedule_2 = new System.Windows.Forms.Button();
            this._checkAutomatedScheduling = new System.Windows.Forms.CheckBox();
            this.d = new System.Windows.Forms.GroupBox();
            this._btnRefreshMetrics = new System.Windows.Forms.Button();
            this._lblTotalMetrics = new System.Windows.Forms.Label();
            this._lblPassengersMetrics = new System.Windows.Forms.Label();
            this._lblTrainsMetrics = new System.Windows.Forms.Label();
            this._lblTotalLoadHeading = new System.Windows.Forms.Label();
            this._lblPassengersHeading = new System.Windows.Forms.Label();
            this._lblTrainsHeading = new System.Windows.Forms.Label();
            this._lblSystemMetricsHeading = new System.Windows.Forms.Label();
            this.groupSystemNotifications = new System.Windows.Forms.GroupBox();
            this.listSystemNotifications = new System.Windows.Forms.ListBox();
            this.groupSystemStatus = new System.Windows.Forms.GroupBox();
            this.groupRunningStatus = new System.Windows.Forms.GroupBox();
            this.lblEnvironmentHead = new System.Windows.Forms.Label();
            this.lblPTrackControllerHead = new System.Windows.Forms.Label();
            this.lblTrackModelStatusHead = new System.Windows.Forms.Label();
            this.lblSystemSchedulerStatusHead = new System.Windows.Forms.Label();
            this.lblCTCOfficeStatusHead = new System.Windows.Forms.Label();
            this.dataGridTrackLayout = new System.Windows.Forms.DataGridView();
            this._groupLoginLogout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginStatusImage)).BeginInit();
            this._groupOperatorControls.SuspendLayout();
            this._groupGlobalTimeControls.SuspendLayout();
            this._groupSystemSchedulerControls.SuspendLayout();
            this.d.SuspendLayout();
            this.groupSystemNotifications.SuspendLayout();
            this.groupSystemStatus.SuspendLayout();
            this.groupRunningStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTrackLayout)).BeginInit();
            this.SuspendLayout();
            // 
            // _groupLoginLogout
            // 
            this._groupLoginLogout.Controls.Add(this.loginStatusImage);
            this._groupLoginLogout.Controls.Add(this._btnLoginLogout);
            this._groupLoginLogout.Controls.Add(this._txtPassword);
            this._groupLoginLogout.Controls.Add(this._lblPassword);
            this._groupLoginLogout.Controls.Add(this._lblUsername);
            this._groupLoginLogout.Controls.Add(this._txtUsername);
            this._groupLoginLogout.Location = new System.Drawing.Point(2, 3);
            this._groupLoginLogout.Name = "_groupLoginLogout";
            this._groupLoginLogout.Size = new System.Drawing.Size(303, 60);
            this._groupLoginLogout.TabIndex = 0;
            this._groupLoginLogout.TabStop = false;
            this._groupLoginLogout.Text = "Login / Logout";
            // 
            // loginStatusImage
            // 
            this.loginStatusImage.InitialImage = null;
            this.loginStatusImage.Location = new System.Drawing.Point(272, 9);
            this.loginStatusImage.Name = "loginStatusImage";
            this.loginStatusImage.Size = new System.Drawing.Size(16, 16);
            this.loginStatusImage.TabIndex = 6;
            this.loginStatusImage.TabStop = false;
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
            this._groupOperatorControls.Controls.Add(this._btnSchedule_1);
            this._groupOperatorControls.Controls.Add(this._btnDispatchTrain);
            this._groupOperatorControls.Location = new System.Drawing.Point(2, 69);
            this._groupOperatorControls.Name = "_groupOperatorControls";
            this._groupOperatorControls.Size = new System.Drawing.Size(303, 49);
            this._groupOperatorControls.TabIndex = 1;
            this._groupOperatorControls.TabStop = false;
            this._groupOperatorControls.Text = "Operator Controls";
            // 
            // _btnRefreshView
            // 
            this._btnRefreshView.Location = new System.Drawing.Point(204, 19);
            this._btnRefreshView.Name = "_btnRefreshView";
            this._btnRefreshView.Size = new System.Drawing.Size(78, 23);
            this._btnRefreshView.TabIndex = 2;
            this._btnRefreshView.Text = "Refresh View";
            this._btnRefreshView.UseVisualStyleBackColor = true;
            // 
            // _btnSchedule_1
            // 
            this._btnSchedule_1.Location = new System.Drawing.Point(98, 19);
            this._btnSchedule_1.Name = "_btnSchedule_1";
            this._btnSchedule_1.Size = new System.Drawing.Size(100, 23);
            this._btnSchedule_1.TabIndex = 1;
            this._btnSchedule_1.Text = "Schedule Trains";
            this._btnSchedule_1.UseVisualStyleBackColor = true;
            this._btnSchedule_1.Click += new System.EventHandler(this._btnSchedule_1_Click);
            // 
            // _btnDispatchTrain
            // 
            this._btnDispatchTrain.Location = new System.Drawing.Point(6, 19);
            this._btnDispatchTrain.Name = "_btnDispatchTrain";
            this._btnDispatchTrain.Size = new System.Drawing.Size(86, 23);
            this._btnDispatchTrain.TabIndex = 0;
            this._btnDispatchTrain.Text = "Dispatch Train";
            this._btnDispatchTrain.UseVisualStyleBackColor = true;
            // 
            // _groupGlobalTimeControls
            // 
            this._groupGlobalTimeControls.Controls.Add(this._lblSpeedDetail);
            this._groupGlobalTimeControls.Controls.Add(this._btnSpeed);
            this._groupGlobalTimeControls.Controls.Add(this.txtGlobalTimeArea);
            this._groupGlobalTimeControls.Controls.Add(this._lblSpeed);
            this._groupGlobalTimeControls.Location = new System.Drawing.Point(15, 428);
            this._groupGlobalTimeControls.Name = "_groupGlobalTimeControls";
            this._groupGlobalTimeControls.Size = new System.Drawing.Size(252, 77);
            this._groupGlobalTimeControls.TabIndex = 2;
            this._groupGlobalTimeControls.TabStop = false;
            this._groupGlobalTimeControls.Text = "Track Model";
            // 
            // _lblSpeedDetail
            // 
            this._lblSpeedDetail.AutoSize = true;
            this._lblSpeedDetail.ForeColor = System.Drawing.SystemColors.GrayText;
            this._lblSpeedDetail.Location = new System.Drawing.Point(6, 55);
            this._lblSpeedDetail.Name = "_lblSpeedDetail";
            this._lblSpeedDetail.Size = new System.Drawing.Size(162, 13);
            this._lblSpeedDetail.TabIndex = 3;
            this._lblSpeedDetail.Text = "Min (Wall Speed): 1  -->  Max: 10";
            // 
            // _btnSpeed
            // 
            this._btnSpeed.Location = new System.Drawing.Point(115, 29);
            this._btnSpeed.Name = "_btnSpeed";
            this._btnSpeed.Size = new System.Drawing.Size(75, 23);
            this._btnSpeed.TabIndex = 2;
            this._btnSpeed.Text = "Apply";
            this._btnSpeed.UseVisualStyleBackColor = true;
            // 
            // txtGlobalTimeArea
            // 
            this.txtGlobalTimeArea.Location = new System.Drawing.Point(9, 32);
            this.txtGlobalTimeArea.Name = "txtGlobalTimeArea";
            this.txtGlobalTimeArea.Size = new System.Drawing.Size(100, 20);
            this.txtGlobalTimeArea.TabIndex = 1;
            // 
            // _lblSpeed
            // 
            this._lblSpeed.AutoSize = true;
            this._lblSpeed.Location = new System.Drawing.Point(6, 16);
            this._lblSpeed.Name = "_lblSpeed";
            this._lblSpeed.Size = new System.Drawing.Size(97, 13);
            this._lblSpeed.TabIndex = 0;
            this._lblSpeed.Text = "Global Time Speed";
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
            // d
            // 
            this.d.Controls.Add(this._btnRefreshMetrics);
            this.d.Controls.Add(this._lblTotalMetrics);
            this.d.Controls.Add(this._lblPassengersMetrics);
            this.d.Controls.Add(this._lblTrainsMetrics);
            this.d.Controls.Add(this._lblTotalLoadHeading);
            this.d.Controls.Add(this._lblPassengersHeading);
            this.d.Controls.Add(this._lblTrainsHeading);
            this.d.Controls.Add(this._lblSystemMetricsHeading);
            this.d.Location = new System.Drawing.Point(13, 299);
            this.d.Name = "d";
            this.d.Size = new System.Drawing.Size(252, 123);
            this.d.TabIndex = 4;
            this.d.TabStop = false;
            this.d.Text = "System Metrics";
            // 
            // _btnRefreshMetrics
            // 
            this._btnRefreshMetrics.Location = new System.Drawing.Point(171, 89);
            this._btnRefreshMetrics.Name = "_btnRefreshMetrics";
            this._btnRefreshMetrics.Size = new System.Drawing.Size(75, 23);
            this._btnRefreshMetrics.TabIndex = 7;
            this._btnRefreshMetrics.Text = "Refresh";
            this._btnRefreshMetrics.UseVisualStyleBackColor = true;
            // 
            // _lblTotalMetrics
            // 
            this._lblTotalMetrics.AutoSize = true;
            this._lblTotalMetrics.Location = new System.Drawing.Point(68, 94);
            this._lblTotalMetrics.Name = "_lblTotalMetrics";
            this._lblTotalMetrics.Size = new System.Drawing.Size(13, 13);
            this._lblTotalMetrics.TabIndex = 6;
            this._lblTotalMetrics.Text = "0";
            // 
            // _lblPassengersMetrics
            // 
            this._lblPassengersMetrics.AutoSize = true;
            this._lblPassengersMetrics.Location = new System.Drawing.Point(68, 71);
            this._lblPassengersMetrics.Name = "_lblPassengersMetrics";
            this._lblPassengersMetrics.Size = new System.Drawing.Size(13, 13);
            this._lblPassengersMetrics.TabIndex = 5;
            this._lblPassengersMetrics.Text = "0";
            // 
            // _lblTrainsMetrics
            // 
            this._lblTrainsMetrics.AutoSize = true;
            this._lblTrainsMetrics.Location = new System.Drawing.Point(68, 49);
            this._lblTrainsMetrics.Name = "_lblTrainsMetrics";
            this._lblTrainsMetrics.Size = new System.Drawing.Size(13, 13);
            this._lblTrainsMetrics.TabIndex = 4;
            this._lblTrainsMetrics.Text = "0";
            // 
            // _lblTotalLoadHeading
            // 
            this._lblTotalLoadHeading.AutoSize = true;
            this._lblTotalLoadHeading.Location = new System.Drawing.Point(6, 94);
            this._lblTotalLoadHeading.Name = "_lblTotalLoadHeading";
            this._lblTotalLoadHeading.Size = new System.Drawing.Size(64, 13);
            this._lblTotalLoadHeading.TabIndex = 3;
            this._lblTotalLoadHeading.Text = "Total Load: ";
            // 
            // _lblPassengersHeading
            // 
            this._lblPassengersHeading.AutoSize = true;
            this._lblPassengersHeading.Location = new System.Drawing.Point(6, 71);
            this._lblPassengersHeading.Name = "_lblPassengersHeading";
            this._lblPassengersHeading.Size = new System.Drawing.Size(65, 13);
            this._lblPassengersHeading.TabIndex = 2;
            this._lblPassengersHeading.Text = "Passengers:";
            // 
            // _lblTrainsHeading
            // 
            this._lblTrainsHeading.AutoSize = true;
            this._lblTrainsHeading.Location = new System.Drawing.Point(29, 49);
            this._lblTrainsHeading.Name = "_lblTrainsHeading";
            this._lblTrainsHeading.Size = new System.Drawing.Size(42, 13);
            this._lblTrainsHeading.TabIndex = 1;
            this._lblTrainsHeading.Text = "Trains: ";
            // 
            // _lblSystemMetricsHeading
            // 
            this._lblSystemMetricsHeading.AutoSize = true;
            this._lblSystemMetricsHeading.Location = new System.Drawing.Point(6, 26);
            this._lblSystemMetricsHeading.Name = "_lblSystemMetricsHeading";
            this._lblSystemMetricsHeading.Size = new System.Drawing.Size(105, 13);
            this._lblSystemMetricsHeading.TabIndex = 0;
            this._lblSystemMetricsHeading.Text = "Current System Load";
            // 
            // groupSystemNotifications
            // 
            this.groupSystemNotifications.Controls.Add(this.listSystemNotifications);
            this.groupSystemNotifications.Location = new System.Drawing.Point(311, 3);
            this.groupSystemNotifications.Name = "groupSystemNotifications";
            this.groupSystemNotifications.Size = new System.Drawing.Size(958, 115);
            this.groupSystemNotifications.TabIndex = 5;
            this.groupSystemNotifications.TabStop = false;
            this.groupSystemNotifications.Text = "System Notifications";
            // 
            // listSystemNotifications
            // 
            this.listSystemNotifications.FormattingEnabled = true;
            this.listSystemNotifications.Location = new System.Drawing.Point(6, 14);
            this.listSystemNotifications.Name = "listSystemNotifications";
            this.listSystemNotifications.Size = new System.Drawing.Size(946, 95);
            this.listSystemNotifications.TabIndex = 0;
            // 
            // groupSystemStatus
            // 
            this.groupSystemStatus.Controls.Add(this.groupRunningStatus);
            this.groupSystemStatus.Controls.Add(this.d);
            this.groupSystemStatus.Controls.Add(this._groupGlobalTimeControls);
            this.groupSystemStatus.Controls.Add(this._groupSystemSchedulerControls);
            this.groupSystemStatus.Location = new System.Drawing.Point(1275, 3);
            this.groupSystemStatus.Name = "groupSystemStatus";
            this.groupSystemStatus.Size = new System.Drawing.Size(273, 622);
            this.groupSystemStatus.TabIndex = 6;
            this.groupSystemStatus.TabStop = false;
            this.groupSystemStatus.Text = "System Status";
            // 
            // groupRunningStatus
            // 
            this.groupRunningStatus.Controls.Add(this.lblEnvironmentHead);
            this.groupRunningStatus.Controls.Add(this.lblPTrackControllerHead);
            this.groupRunningStatus.Controls.Add(this.lblTrackModelStatusHead);
            this.groupRunningStatus.Controls.Add(this.lblSystemSchedulerStatusHead);
            this.groupRunningStatus.Controls.Add(this.lblCTCOfficeStatusHead);
            this.groupRunningStatus.Location = new System.Drawing.Point(13, 19);
            this.groupRunningStatus.Name = "groupRunningStatus";
            this.groupRunningStatus.Size = new System.Drawing.Size(252, 274);
            this.groupRunningStatus.TabIndex = 5;
            this.groupRunningStatus.TabStop = false;
            this.groupRunningStatus.Text = "System Module Status";
            // 
            // lblEnvironmentHead
            // 
            this.lblEnvironmentHead.AutoSize = true;
            this.lblEnvironmentHead.Location = new System.Drawing.Point(11, 225);
            this.lblEnvironmentHead.Name = "lblEnvironmentHead";
            this.lblEnvironmentHead.Size = new System.Drawing.Size(72, 13);
            this.lblEnvironmentHead.TabIndex = 4;
            this.lblEnvironmentHead.Text = "Environment: ";
            // 
            // lblPTrackControllerHead
            // 
            this.lblPTrackControllerHead.AutoSize = true;
            this.lblPTrackControllerHead.Location = new System.Drawing.Point(11, 172);
            this.lblPTrackControllerHead.Name = "lblPTrackControllerHead";
            this.lblPTrackControllerHead.Size = new System.Drawing.Size(125, 13);
            this.lblPTrackControllerHead.TabIndex = 3;
            this.lblPTrackControllerHead.Text = "Primary Track Controller: ";
            // 
            // lblTrackModelStatusHead
            // 
            this.lblTrackModelStatusHead.AutoSize = true;
            this.lblTrackModelStatusHead.Location = new System.Drawing.Point(11, 120);
            this.lblTrackModelStatusHead.Name = "lblTrackModelStatusHead";
            this.lblTrackModelStatusHead.Size = new System.Drawing.Size(73, 13);
            this.lblTrackModelStatusHead.TabIndex = 2;
            this.lblTrackModelStatusHead.Text = "Track Model: ";
            // 
            // lblSystemSchedulerStatusHead
            // 
            this.lblSystemSchedulerStatusHead.AutoSize = true;
            this.lblSystemSchedulerStatusHead.Location = new System.Drawing.Point(11, 71);
            this.lblSystemSchedulerStatusHead.Name = "lblSystemSchedulerStatusHead";
            this.lblSystemSchedulerStatusHead.Size = new System.Drawing.Size(98, 13);
            this.lblSystemSchedulerStatusHead.TabIndex = 1;
            this.lblSystemSchedulerStatusHead.Text = "System Scheduler: ";
            // 
            // lblCTCOfficeStatusHead
            // 
            this.lblCTCOfficeStatusHead.AutoSize = true;
            this.lblCTCOfficeStatusHead.Location = new System.Drawing.Point(11, 28);
            this.lblCTCOfficeStatusHead.Name = "lblCTCOfficeStatusHead";
            this.lblCTCOfficeStatusHead.Size = new System.Drawing.Size(65, 13);
            this.lblCTCOfficeStatusHead.TabIndex = 0;
            this.lblCTCOfficeStatusHead.Text = "CTC Office: ";
            // 
            // dataGridTrackLayout
            // 
            this.dataGridTrackLayout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTrackLayout.Location = new System.Drawing.Point(8, 124);
            this.dataGridTrackLayout.Name = "dataGridTrackLayout";
            this.dataGridTrackLayout.Size = new System.Drawing.Size(1261, 644);
            this.dataGridTrackLayout.TabIndex = 7;
            // 
            // CTCOffice_old
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1552, 771);
            this.Controls.Add(this.dataGridTrackLayout);
            this.Controls.Add(this.groupSystemStatus);
            this.Controls.Add(this.groupSystemNotifications);
            this.Controls.Add(this._groupOperatorControls);
            this.Controls.Add(this._groupLoginLogout);
            this.Name = "CTCOffice_old";
            this.Text = "CTC Office";
            this.Load += new System.EventHandler(this.CTCOffice_Load);
            this._groupLoginLogout.ResumeLayout(false);
            this._groupLoginLogout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginStatusImage)).EndInit();
            this._groupOperatorControls.ResumeLayout(false);
            this._groupGlobalTimeControls.ResumeLayout(false);
            this._groupGlobalTimeControls.PerformLayout();
            this._groupSystemSchedulerControls.ResumeLayout(false);
            this._groupSystemSchedulerControls.PerformLayout();
            this.d.ResumeLayout(false);
            this.d.PerformLayout();
            this.groupSystemNotifications.ResumeLayout(false);
            this.groupSystemStatus.ResumeLayout(false);
            this.groupRunningStatus.ResumeLayout(false);
            this.groupRunningStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTrackLayout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupLoginLogout;
        private System.Windows.Forms.TextBox _txtPassword;
        private System.Windows.Forms.Label _lblPassword;
        private System.Windows.Forms.Label _lblUsername;
        private System.Windows.Forms.TextBox _txtUsername;
        private System.Windows.Forms.Button _btnLoginLogout;
        private System.Windows.Forms.GroupBox _groupOperatorControls;
        private System.Windows.Forms.Button _btnRefreshView;
        private System.Windows.Forms.Button _btnSchedule_1;
        private System.Windows.Forms.Button _btnDispatchTrain;
        private System.Windows.Forms.GroupBox _groupGlobalTimeControls;
        private System.Windows.Forms.Label _lblSpeedDetail;
        private System.Windows.Forms.Button _btnSpeed;
        private System.Windows.Forms.TextBox txtGlobalTimeArea;
        private System.Windows.Forms.Label _lblSpeed;
        private System.Windows.Forms.GroupBox _groupSystemSchedulerControls;
        private System.Windows.Forms.Button _btnSchedule_2;
        private System.Windows.Forms.CheckBox _checkAutomatedScheduling;
        private System.Windows.Forms.GroupBox d;
        private System.Windows.Forms.Button _btnRefreshMetrics;
        private System.Windows.Forms.Label _lblTotalMetrics;
        private System.Windows.Forms.Label _lblPassengersMetrics;
        private System.Windows.Forms.Label _lblTrainsMetrics;
        private System.Windows.Forms.Label _lblTotalLoadHeading;
        private System.Windows.Forms.Label _lblPassengersHeading;
        private System.Windows.Forms.Label _lblTrainsHeading;
        private System.Windows.Forms.Label _lblSystemMetricsHeading;
        private System.Windows.Forms.GroupBox groupSystemNotifications;
        private System.Windows.Forms.GroupBox groupSystemStatus;
        private System.Windows.Forms.ListBox listSystemNotifications;
        private System.Windows.Forms.GroupBox groupRunningStatus;
        private System.Windows.Forms.Label lblEnvironmentHead;
        private System.Windows.Forms.Label lblPTrackControllerHead;
        private System.Windows.Forms.Label lblTrackModelStatusHead;
        private System.Windows.Forms.Label lblSystemSchedulerStatusHead;
        private System.Windows.Forms.Label lblCTCOfficeStatusHead;
        private System.Windows.Forms.DataGridView dataGridTrackLayout;
        private System.Windows.Forms.PictureBox loginStatusImage;
    }
}

