namespace CTCOffice
{
    partial class AuthorityTool
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
            this._groupBoxAuthorityTool = new System.Windows.Forms.GroupBox();
            this._btnSubmit = new System.Windows.Forms.Button();
            this._txtAuthority = new System.Windows.Forms.TextBox();
            this._groupBoxAuthorityTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupBoxAuthorityTool
            // 
            this._groupBoxAuthorityTool.Controls.Add(this._btnSubmit);
            this._groupBoxAuthorityTool.Controls.Add(this._txtAuthority);
            this._groupBoxAuthorityTool.Location = new System.Drawing.Point(3, 3);
            this._groupBoxAuthorityTool.Name = "_groupBoxAuthorityTool";
            this._groupBoxAuthorityTool.Size = new System.Drawing.Size(133, 47);
            this._groupBoxAuthorityTool.TabIndex = 0;
            this._groupBoxAuthorityTool.TabStop = false;
            this._groupBoxAuthorityTool.Text = "Authority Tool";
            // 
            // _btnSubmit
            // 
            this._btnSubmit.Location = new System.Drawing.Point(53, 17);
            this._btnSubmit.Name = "_btnSubmit";
            this._btnSubmit.Size = new System.Drawing.Size(75, 23);
            this._btnSubmit.TabIndex = 1;
            this._btnSubmit.Text = "Submit";
            this._btnSubmit.UseVisualStyleBackColor = true;
            this._btnSubmit.Click += new System.EventHandler(this._btnSubmit_Click);
            // 
            // _txtAuthority
            // 
            this._txtAuthority.Location = new System.Drawing.Point(6, 19);
            this._txtAuthority.Name = "_txtAuthority";
            this._txtAuthority.Size = new System.Drawing.Size(41, 20);
            this._txtAuthority.TabIndex = 0;
            // 
            // AuthorityTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._groupBoxAuthorityTool);
            this.Name = "AuthorityTool";
            this.Size = new System.Drawing.Size(141, 52);
            this._groupBoxAuthorityTool.ResumeLayout(false);
            this._groupBoxAuthorityTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBoxAuthorityTool;
        private System.Windows.Forms.Button _btnSubmit;
        private System.Windows.Forms.TextBox _txtAuthority;
    }
}
