namespace CTCOffice
{
    partial class OfficeGUITest
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
            this._panelCTC = new System.Windows.Forms.Panel();
            this._panelRequestGreen = new System.Windows.Forms.Panel();
            this._panelRequestRed = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _panelCTC
            // 
            this._panelCTC.Location = new System.Drawing.Point(3, 3);
            this._panelCTC.Name = "_panelCTC";
            this._panelCTC.Size = new System.Drawing.Size(1272, 716);
            this._panelCTC.TabIndex = 0;
            // 
            // _panelRequestGreen
            // 
            this._panelRequestGreen.Location = new System.Drawing.Point(1281, 290);
            this._panelRequestGreen.Name = "_panelRequestGreen";
            this._panelRequestGreen.Size = new System.Drawing.Size(223, 281);
            this._panelRequestGreen.TabIndex = 1;
            // 
            // _panelRequestRed
            // 
            this._panelRequestRed.Location = new System.Drawing.Point(1281, 3);
            this._panelRequestRed.Name = "_panelRequestRed";
            this._panelRequestRed.Size = new System.Drawing.Size(223, 281);
            this._panelRequestRed.TabIndex = 2;
            // 
            // OfficeGUITest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelRequestRed);
            this.Controls.Add(this._panelRequestGreen);
            this.Controls.Add(this._panelCTC);
            this.Name = "OfficeGUITest";
            this.Size = new System.Drawing.Size(1525, 720);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelCTC;
        private System.Windows.Forms.Panel _panelRequestGreen;
        private System.Windows.Forms.Panel _panelRequestRed;
    }
}
