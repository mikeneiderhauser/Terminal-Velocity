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
            this.SuspendLayout();
            // 
            // _btnEmergencyBrakes
            // 
            this._btnEmergencyBrakes.Location = new System.Drawing.Point(684, 0);
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
            this._btnSubmit.Location = new System.Drawing.Point(158, 308);
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
            // 
            // TemperatureInput
            // 
            this.TemperatureInput.Location = new System.Drawing.Point(199, 126);
            this.TemperatureInput.Name = "TemperatureInput";
            this.TemperatureInput.Size = new System.Drawing.Size(100, 22);
            this.TemperatureInput.TabIndex = 9;
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
            this._logOutput.Location = new System.Drawing.Point(412, 29);
            this._logOutput.Multiline = true;
            this._logOutput.Name = "_logOutput";
            this._logOutput.ReadOnly = true;
            this._logOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._logOutput.Size = new System.Drawing.Size(466, 380);
            this._logOutput.TabIndex = 12;
            // 
            // TrainControllerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Size = new System.Drawing.Size(909, 428);
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
    }
}
