namespace CTCOffice
{
    partial class TestingControls
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
            this._btnCloseGreenBlock = new System.Windows.Forms.Button();
            this._btnCloseRedBlock = new System.Windows.Forms.Button();
            this._btnThrowChange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _btnCloseGreenBlock
            // 
            this._btnCloseGreenBlock.Location = new System.Drawing.Point(3, 12);
            this._btnCloseGreenBlock.Name = "_btnCloseGreenBlock";
            this._btnCloseGreenBlock.Size = new System.Drawing.Size(116, 23);
            this._btnCloseGreenBlock.TabIndex = 0;
            this._btnCloseGreenBlock.Text = "Close Green 1";
            this._btnCloseGreenBlock.UseVisualStyleBackColor = true;
            this._btnCloseGreenBlock.Click += new System.EventHandler(this._btnCloseGreenBlock_Click);
            // 
            // _btnCloseRedBlock
            // 
            this._btnCloseRedBlock.Location = new System.Drawing.Point(3, 41);
            this._btnCloseRedBlock.Name = "_btnCloseRedBlock";
            this._btnCloseRedBlock.Size = new System.Drawing.Size(116, 23);
            this._btnCloseRedBlock.TabIndex = 1;
            this._btnCloseRedBlock.Text = "Close Red 1";
            this._btnCloseRedBlock.UseVisualStyleBackColor = true;
            this._btnCloseRedBlock.Click += new System.EventHandler(this._btnCloseRedBlock_Click);
            // 
            // _btnThrowChange
            // 
            this._btnThrowChange.Location = new System.Drawing.Point(3, 70);
            this._btnThrowChange.Name = "_btnThrowChange";
            this._btnThrowChange.Size = new System.Drawing.Size(116, 23);
            this._btnThrowChange.TabIndex = 2;
            this._btnThrowChange.Text = "TrackChangedEvent";
            this._btnThrowChange.UseVisualStyleBackColor = true;
            this._btnThrowChange.Click += new System.EventHandler(this._btnThrowChange_Click);
            // 
            // TestingControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btnThrowChange);
            this.Controls.Add(this._btnCloseRedBlock);
            this.Controls.Add(this._btnCloseGreenBlock);
            this.Name = "TestingControls";
            this.Size = new System.Drawing.Size(124, 104);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnCloseGreenBlock;
        private System.Windows.Forms.Button _btnCloseRedBlock;
        private System.Windows.Forms.Button _btnThrowChange;
    }
}
