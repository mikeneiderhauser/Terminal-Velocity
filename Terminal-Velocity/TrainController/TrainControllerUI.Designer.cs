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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(92, 221);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(92, 276);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(224, 338);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // TrainControllerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this._btnEmergencyBrakes);
            this.Name = "TrainControllerUI";
            this.Size = new System.Drawing.Size(551, 428);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnEmergencyBrakes;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}
