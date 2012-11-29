namespace CTCOffice
{
    partial class SpeedTool
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
            this._groupBoxSpeedTool = new System.Windows.Forms.GroupBox();
            this._btnSubmit = new System.Windows.Forms.Button();
            this._txtSpeed = new System.Windows.Forms.TextBox();
            this._lblUnits = new System.Windows.Forms.Label();
            this._groupBoxSpeedTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupBoxSpeedTool
            // 
            this._groupBoxSpeedTool.Controls.Add(this._lblUnits);
            this._groupBoxSpeedTool.Controls.Add(this._txtSpeed);
            this._groupBoxSpeedTool.Controls.Add(this._btnSubmit);
            this._groupBoxSpeedTool.Location = new System.Drawing.Point(3, 3);
            this._groupBoxSpeedTool.Name = "_groupBoxSpeedTool";
            this._groupBoxSpeedTool.Size = new System.Drawing.Size(93, 77);
            this._groupBoxSpeedTool.TabIndex = 0;
            this._groupBoxSpeedTool.TabStop = false;
            this._groupBoxSpeedTool.Text = "Speed Tool";
            // 
            // _btnSubmit
            // 
            this._btnSubmit.Location = new System.Drawing.Point(6, 45);
            this._btnSubmit.Name = "_btnSubmit";
            this._btnSubmit.Size = new System.Drawing.Size(81, 23);
            this._btnSubmit.TabIndex = 0;
            this._btnSubmit.Text = "Submit";
            this._btnSubmit.UseVisualStyleBackColor = true;
            this._btnSubmit.Click += new System.EventHandler(this._btnSubmit_Click);
            // 
            // _txtSpeed
            // 
            this._txtSpeed.Location = new System.Drawing.Point(6, 19);
            this._txtSpeed.Name = "_txtSpeed";
            this._txtSpeed.Size = new System.Drawing.Size(43, 20);
            this._txtSpeed.TabIndex = 1;
            this._txtSpeed.TextChanged += new System.EventHandler(this._txtSpeed_TextChanged);
            // 
            // _lblUnits
            // 
            this._lblUnits.AutoSize = true;
            this._lblUnits.Location = new System.Drawing.Point(55, 22);
            this._lblUnits.Name = "_lblUnits";
            this._lblUnits.Size = new System.Drawing.Size(32, 13);
            this._lblUnits.TabIndex = 2;
            this._lblUnits.Text = "km/h";
            // 
            // SpeedTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._groupBoxSpeedTool);
            this.Name = "SpeedTool";
            this.Size = new System.Drawing.Size(99, 82);
            this._groupBoxSpeedTool.ResumeLayout(false);
            this._groupBoxSpeedTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBoxSpeedTool;
        private System.Windows.Forms.Label _lblUnits;
        private System.Windows.Forms.TextBox _txtSpeed;
        private System.Windows.Forms.Button _btnSubmit;
    }
}
