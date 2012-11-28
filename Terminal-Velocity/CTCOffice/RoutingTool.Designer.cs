namespace CTCOffice
{
    partial class RoutingTool
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
            this._groupBoxRoutingTool = new System.Windows.Forms.GroupBox();
            this._btnRed = new System.Windows.Forms.Button();
            this._btnGreen = new System.Windows.Forms.Button();
            this._btnPoint = new System.Windows.Forms.Button();
            this._txtSelected = new System.Windows.Forms.TextBox();
            this._groupBoxRoutingTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupBoxRoutingTool
            // 
            this._groupBoxRoutingTool.Controls.Add(this._txtSelected);
            this._groupBoxRoutingTool.Controls.Add(this._btnPoint);
            this._groupBoxRoutingTool.Controls.Add(this._btnGreen);
            this._groupBoxRoutingTool.Controls.Add(this._btnRed);
            this._groupBoxRoutingTool.Location = new System.Drawing.Point(3, 3);
            this._groupBoxRoutingTool.Name = "_groupBoxRoutingTool";
            this._groupBoxRoutingTool.Size = new System.Drawing.Size(311, 54);
            this._groupBoxRoutingTool.TabIndex = 0;
            this._groupBoxRoutingTool.TabStop = false;
            this._groupBoxRoutingTool.Text = "Routeing Options";
            // 
            // _btnRed
            // 
            this._btnRed.Location = new System.Drawing.Point(6, 19);
            this._btnRed.Name = "_btnRed";
            this._btnRed.Size = new System.Drawing.Size(75, 23);
            this._btnRed.TabIndex = 0;
            this._btnRed.Text = "Red Line";
            this._btnRed.UseVisualStyleBackColor = true;
            this._btnRed.Click += new System.EventHandler(this._btnRed_Click);
            // 
            // _btnGreen
            // 
            this._btnGreen.Location = new System.Drawing.Point(87, 19);
            this._btnGreen.Name = "_btnGreen";
            this._btnGreen.Size = new System.Drawing.Size(75, 23);
            this._btnGreen.TabIndex = 1;
            this._btnGreen.Text = "Green Line";
            this._btnGreen.UseVisualStyleBackColor = true;
            this._btnGreen.Click += new System.EventHandler(this._btnGreen_Click);
            // 
            // _btnPoint
            // 
            this._btnPoint.Location = new System.Drawing.Point(168, 19);
            this._btnPoint.Name = "_btnPoint";
            this._btnPoint.Size = new System.Drawing.Size(75, 23);
            this._btnPoint.TabIndex = 2;
            this._btnPoint.Text = "Point Route";
            this._btnPoint.UseVisualStyleBackColor = true;
            this._btnPoint.Click += new System.EventHandler(this._btnPoint_Click);
            // 
            // _txtSelected
            // 
            this._txtSelected.Location = new System.Drawing.Point(249, 19);
            this._txtSelected.Name = "_txtSelected";
            this._txtSelected.ReadOnly = true;
            this._txtSelected.Size = new System.Drawing.Size(57, 20);
            this._txtSelected.TabIndex = 3;
            this._txtSelected.Text = "0";
            // 
            // RoutingTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._groupBoxRoutingTool);
            this.Name = "RoutingTool";
            this.Size = new System.Drawing.Size(319, 60);
            this._groupBoxRoutingTool.ResumeLayout(false);
            this._groupBoxRoutingTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBoxRoutingTool;
        private System.Windows.Forms.TextBox _txtSelected;
        private System.Windows.Forms.Button _btnPoint;
        private System.Windows.Forms.Button _btnGreen;
        private System.Windows.Forms.Button _btnRed;
    }
}
