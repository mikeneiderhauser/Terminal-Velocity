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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this._btnEmergencyBrakes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(99, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 0;
            // 
            // _btnEmergencyBrakes
            // 
            this._btnEmergencyBrakes.Location = new System.Drawing.Point(397, 4);
            this._btnEmergencyBrakes.Name = "_btnEmergencyBrakes";
            this._btnEmergencyBrakes.Size = new System.Drawing.Size(139, 23);
            this._btnEmergencyBrakes.TabIndex = 1;
            this._btnEmergencyBrakes.Text = "Emergency Brakes";
            this._btnEmergencyBrakes.UseVisualStyleBackColor = true;
            this._btnEmergencyBrakes.Click += new System.EventHandler(this._btnEmergencyBrake_Click);
            // 
            // TrainControllerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btnEmergencyBrakes);
            this.Controls.Add(this.comboBox1);
            this.Name = "TrainControllerUI";
            this.Size = new System.Drawing.Size(551, 428);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button _btnEmergencyBrakes;
    }
}
