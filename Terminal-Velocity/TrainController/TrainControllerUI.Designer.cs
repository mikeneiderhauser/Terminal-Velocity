namespace TrainController
{
    partial class TrainControllerUI
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
            this._btnEmergencyBrakes = new System.Windows.Forms.Button();
            this._btnDoorOpen = new System.Windows.Forms.Button();
            this._btnDoorClose = new System.Windows.Forms.Button();
            this._btnSubmit = new System.Windows.Forms.Button();
            this.SpeedInput = new System.Windows.Forms.TextBox();
            this.TemperatureInput = new System.Windows.Forms.TextBox();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.TemperatureLabel = new System.Windows.Forms.Label();
            this._logOutput = new System.Windows.Forms.TextBox();
            this.EngineerInput = new System.Windows.Forms.RadioButton();
            this.TrackControllerInput = new System.Windows.Forms.RadioButton();
            this.SpeedLimitLabel = new System.Windows.Forms.Label();
            this.SpeedLimitInput = new System.Windows.Forms.TextBox();
            this.AuthorityLabel = new System.Windows.Forms.Label();
            this.AuthorityLimitInput = new System.Windows.Forms.TextBox();
            this.AnnouncementLabel = new System.Windows.Forms.Label();
            this.AddPassengerButton = new System.Windows.Forms.Button();
            this.RemovePassengersButton = new System.Windows.Forms.Button();
            this.SubmitTrackButton = new System.Windows.Forms.Button();
            this.AnnouncementComboBox = new System.Windows.Forms.ComboBox();
            this.LightsOn = new System.Windows.Forms.Button();
            this.LightsOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _btnEmergencyBrakes
            // 
            this._btnEmergencyBrakes.Location = new System.Drawing.Point(1160, 3);
            this._btnEmergencyBrakes.Name = "_btnEmergencyBrakes";
            this._btnEmergencyBrakes.Size = new System.Drawing.Size(139, 23);
            this._btnEmergencyBrakes.TabIndex = 1;
            this._btnEmergencyBrakes.Text = "Emergency Brakes";
            this._btnEmergencyBrakes.UseVisualStyleBackColor = true;
            this._btnEmergencyBrakes.Click += new System.EventHandler(this._btnEmergencyBrake_Click);
            // 
            // _btnDoorOpen
            // 
            this._btnDoorOpen.Location = new System.Drawing.Point(92, 221);
            this._btnDoorOpen.Name = "_btnDoorOpen";
            this._btnDoorOpen.Size = new System.Drawing.Size(90, 23);
            this._btnDoorOpen.TabIndex = 4;
            this._btnDoorOpen.Text = "Door Open";
            this._btnDoorOpen.UseVisualStyleBackColor = true;
            this._btnDoorOpen.Click += new System.EventHandler(this._btnDoorOpen_Click);
            // 
            // _btnDoorClose
            // 
            this._btnDoorClose.Location = new System.Drawing.Point(224, 221);
            this._btnDoorClose.Name = "_btnDoorClose";
            this._btnDoorClose.Size = new System.Drawing.Size(89, 23);
            this._btnDoorClose.TabIndex = 5;
            this._btnDoorClose.Text = "Door Close";
            this._btnDoorClose.UseVisualStyleBackColor = true;
            this._btnDoorClose.Click += new System.EventHandler(this._btnDoorClose_Click);
            // 
            // _btnSubmit
            // 
            this._btnSubmit.Location = new System.Drawing.Point(158, 354);
            this._btnSubmit.Name = "_btnSubmit";
            this._btnSubmit.Size = new System.Drawing.Size(75, 23);
            this._btnSubmit.TabIndex = 6;
            this._btnSubmit.Text = "Submit";
            this._btnSubmit.UseVisualStyleBackColor = true;
            this._btnSubmit.Click += new System.EventHandler(this._btnSubmit_Click);
            // 
            // SpeedInput
            // 
            this.SpeedInput.Location = new System.Drawing.Point(199, 87);
            this.SpeedInput.Name = "SpeedInput";
            this.SpeedInput.Size = new System.Drawing.Size(100, 22);
            this.SpeedInput.TabIndex = 7;
            this.SpeedInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SpeedInput_Key_press);
            // 
            // TemperatureInput
            // 
            this.TemperatureInput.Location = new System.Drawing.Point(199, 126);
            this.TemperatureInput.Name = "TemperatureInput";
            this.TemperatureInput.Size = new System.Drawing.Size(100, 22);
            this.TemperatureInput.TabIndex = 9;
            this.TemperatureInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TemperatureInput_Keypress);
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Location = new System.Drawing.Point(92, 87);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(49, 17);
            this.SpeedLabel.TabIndex = 10;
            this.SpeedLabel.Text = "Speed";
            // 
            // TemperatureLabel
            // 
            this.TemperatureLabel.AutoSize = true;
            this.TemperatureLabel.Location = new System.Drawing.Point(92, 131);
            this.TemperatureLabel.Name = "TemperatureLabel";
            this.TemperatureLabel.Size = new System.Drawing.Size(90, 17);
            this.TemperatureLabel.TabIndex = 11;
            this.TemperatureLabel.Text = "Temperature";
            // 
            // _logOutput
            // 
            this._logOutput.BackColor = System.Drawing.Color.White;
            this._logOutput.Location = new System.Drawing.Point(758, 26);
            this._logOutput.Multiline = true;
            this._logOutput.Name = "_logOutput";
            this._logOutput.ReadOnly = true;
            this._logOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._logOutput.Size = new System.Drawing.Size(541, 380);
            this._logOutput.TabIndex = 12;
            // 
            // EngineerInput
            // 
            this.EngineerInput.AutoSize = true;
            this.EngineerInput.Checked = true;
            this.EngineerInput.Location = new System.Drawing.Point(92, 26);
            this.EngineerInput.Name = "EngineerInput";
            this.EngineerInput.Size = new System.Drawing.Size(86, 21);
            this.EngineerInput.TabIndex = 13;
            this.EngineerInput.TabStop = true;
            this.EngineerInput.Text = "Engineer";
            this.EngineerInput.UseVisualStyleBackColor = true;
            // 
            // TrackControllerInput
            // 
            this.TrackControllerInput.AutoSize = true;
            this.TrackControllerInput.Location = new System.Drawing.Point(473, 26);
            this.TrackControllerInput.Name = "TrackControllerInput";
            this.TrackControllerInput.Size = new System.Drawing.Size(130, 21);
            this.TrackControllerInput.TabIndex = 14;
            this.TrackControllerInput.Text = "Track Controller";
            this.TrackControllerInput.UseVisualStyleBackColor = true;
            this.TrackControllerInput.CheckedChanged += new System.EventHandler(this.TrackControllerInput_CheckedChanged);
            // 
            // SpeedLimitLabel
            // 
            this.SpeedLimitLabel.AutoSize = true;
            this.SpeedLimitLabel.Location = new System.Drawing.Point(433, 87);
            this.SpeedLimitLabel.Name = "SpeedLimitLabel";
            this.SpeedLimitLabel.Size = new System.Drawing.Size(82, 17);
            this.SpeedLimitLabel.TabIndex = 15;
            this.SpeedLimitLabel.Text = "Speed Limit";
            // 
            // SpeedLimitInput
            // 
            this.SpeedLimitInput.Enabled = false;
            this.SpeedLimitInput.Location = new System.Drawing.Point(573, 87);
            this.SpeedLimitInput.Name = "SpeedLimitInput";
            this.SpeedLimitInput.Size = new System.Drawing.Size(100, 22);
            this.SpeedLimitInput.TabIndex = 16;
            this.SpeedLimitInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SpeedLimitInput_keypress);
            // 
            // AuthorityLabel
            // 
            this.AuthorityLabel.AutoSize = true;
            this.AuthorityLabel.Location = new System.Drawing.Point(433, 128);
            this.AuthorityLabel.Name = "AuthorityLabel";
            this.AuthorityLabel.Size = new System.Drawing.Size(97, 17);
            this.AuthorityLabel.TabIndex = 17;
            this.AuthorityLabel.Text = "Authority Limit";
            // 
            // AuthorityLimitInput
            // 
            this.AuthorityLimitInput.Enabled = false;
            this.AuthorityLimitInput.Location = new System.Drawing.Point(573, 125);
            this.AuthorityLimitInput.Name = "AuthorityLimitInput";
            this.AuthorityLimitInput.Size = new System.Drawing.Size(100, 22);
            this.AuthorityLimitInput.TabIndex = 18;
            this.AuthorityLimitInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AuthorityLimitInput_keypress);
            // 
            // AnnouncementLabel
            // 
            this.AnnouncementLabel.AutoSize = true;
            this.AnnouncementLabel.Location = new System.Drawing.Point(433, 168);
            this.AnnouncementLabel.Name = "AnnouncementLabel";
            this.AnnouncementLabel.Size = new System.Drawing.Size(103, 17);
            this.AnnouncementLabel.TabIndex = 19;
            this.AnnouncementLabel.Text = "Announcement";
            // 
            // AddPassengerButton
            // 
            this.AddPassengerButton.Location = new System.Drawing.Point(40, 266);
            this.AddPassengerButton.Name = "AddPassengerButton";
            this.AddPassengerButton.Size = new System.Drawing.Size(138, 23);
            this.AddPassengerButton.TabIndex = 21;
            this.AddPassengerButton.Text = "Add Passengers";
            this.AddPassengerButton.UseVisualStyleBackColor = true;
            this.AddPassengerButton.Click += new System.EventHandler(this.AddPassengerButton_Click);
            // 
            // RemovePassengersButton
            // 
            this.RemovePassengersButton.Location = new System.Drawing.Point(224, 266);
            this.RemovePassengersButton.Name = "RemovePassengersButton";
            this.RemovePassengersButton.Size = new System.Drawing.Size(158, 23);
            this.RemovePassengersButton.TabIndex = 22;
            this.RemovePassengersButton.Text = "Remove Passengers";
            this.RemovePassengersButton.UseVisualStyleBackColor = true;
            this.RemovePassengersButton.Click += new System.EventHandler(this.RemovePassengersButton_Click);
            // 
            // SubmitTrackButton
            // 
            this.SubmitTrackButton.Enabled = false;
            this.SubmitTrackButton.Location = new System.Drawing.Point(528, 354);
            this.SubmitTrackButton.Name = "SubmitTrackButton";
            this.SubmitTrackButton.Size = new System.Drawing.Size(75, 23);
            this.SubmitTrackButton.TabIndex = 23;
            this.SubmitTrackButton.Text = "Submit";
            this.SubmitTrackButton.UseVisualStyleBackColor = true;
            this.SubmitTrackButton.Click += new System.EventHandler(this.SubmitTrackButton_Click);
            // 
            // AnnouncementComboBox
            // 
            this.AnnouncementComboBox.FormattingEnabled = true;
            this.AnnouncementComboBox.Location = new System.Drawing.Point(573, 165);
            this.AnnouncementComboBox.Name = "AnnouncementComboBox";
            this.AnnouncementComboBox.Size = new System.Drawing.Size(121, 24);
            this.AnnouncementComboBox.TabIndex = 24;
            // 
            // LightsOn
            // 
            this.LightsOn.Location = new System.Drawing.Point(92, 308);
            this.LightsOn.Name = "LightsOn";
            this.LightsOn.Size = new System.Drawing.Size(86, 23);
            this.LightsOn.TabIndex = 25;
            this.LightsOn.Text = "Lights On";
            this.LightsOn.UseVisualStyleBackColor = true;
            this.LightsOn.Click += new System.EventHandler(this.LightsOn_Click);
            // 
            // LightsOff
            // 
            this.LightsOff.Location = new System.Drawing.Point(224, 308);
            this.LightsOff.Name = "LightsOff";
            this.LightsOff.Size = new System.Drawing.Size(89, 23);
            this.LightsOff.TabIndex = 26;
            this.LightsOff.Text = "Lights Off";
            this.LightsOff.UseVisualStyleBackColor = true;
            // 
            // TrainControllerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LightsOff);
            this.Controls.Add(this.LightsOn);
            this.Controls.Add(this.AnnouncementComboBox);
            this.Controls.Add(this.SubmitTrackButton);
            this.Controls.Add(this.RemovePassengersButton);
            this.Controls.Add(this.AddPassengerButton);
            this.Controls.Add(this.AnnouncementLabel);
            this.Controls.Add(this.AuthorityLimitInput);
            this.Controls.Add(this.AuthorityLabel);
            this.Controls.Add(this.SpeedLimitInput);
            this.Controls.Add(this.SpeedLimitLabel);
            this.Controls.Add(this.TrackControllerInput);
            this.Controls.Add(this.EngineerInput);
            this.Controls.Add(this._logOutput);
            this.Controls.Add(this.TemperatureLabel);
            this.Controls.Add(this.SpeedLabel);
            this.Controls.Add(this.TemperatureInput);
            this.Controls.Add(this.SpeedInput);
            this.Controls.Add(this._btnSubmit);
            this.Controls.Add(this._btnDoorClose);
            this.Controls.Add(this._btnDoorOpen);
            this.Controls.Add(this._btnEmergencyBrakes);
            this.Name = "TrainControllerUI";
            this.Size = new System.Drawing.Size(1351, 428);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnEmergencyBrakes;
        private System.Windows.Forms.Button _btnDoorOpen;
        private System.Windows.Forms.Button _btnDoorClose;
        private System.Windows.Forms.Button _btnSubmit;
        private System.Windows.Forms.TextBox SpeedInput;
        private System.Windows.Forms.TextBox TemperatureInput;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.Label TemperatureLabel;
        private System.Windows.Forms.TextBox _logOutput;
        private System.Windows.Forms.RadioButton EngineerInput;
        private System.Windows.Forms.RadioButton TrackControllerInput;
        private System.Windows.Forms.Label SpeedLimitLabel;
        private System.Windows.Forms.TextBox SpeedLimitInput;
        private System.Windows.Forms.Label AuthorityLabel;
        private System.Windows.Forms.TextBox AuthorityLimitInput;
        private System.Windows.Forms.Label AnnouncementLabel;
        private System.Windows.Forms.Button AddPassengerButton;
        private System.Windows.Forms.Button RemovePassengersButton;
        private System.Windows.Forms.Button SubmitTrackButton;
        private System.Windows.Forms.ComboBox AnnouncementComboBox;
        private System.Windows.Forms.Button LightsOn;
        private System.Windows.Forms.Button LightsOff;
    }
}
