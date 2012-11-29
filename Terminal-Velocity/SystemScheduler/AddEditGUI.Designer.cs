namespace SystemScheduler
{
    partial class AddEditGUI
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
            this.cmbHour = new System.Windows.Forms.ComboBox();
            this.cmbMinute = new System.Windows.Forms.ComboBox();
            this.lblHour = new System.Windows.Forms.Label();
            this.lblMinute = new System.Windows.Forms.Label();
            this.grpType = new System.Windows.Forms.GroupBox();
            this.rdbCustom = new System.Windows.Forms.RadioButton();
            this.rdbDefined = new System.Windows.Forms.RadioButton();
            this.lblSelect = new System.Windows.Forms.Label();
            this.cmbSelect = new System.Windows.Forms.ComboBox();
            this.lblCustom = new System.Windows.Forms.Label();
            this.txtCustom = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblAMPM = new System.Windows.Forms.Label();
            this.cmbAMPM = new System.Windows.Forms.ComboBox();
            this.grpType.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbHour
            // 
            this.cmbHour.FormattingEnabled = true;
            this.cmbHour.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbHour.Location = new System.Drawing.Point(69, 19);
            this.cmbHour.Name = "cmbHour";
            this.cmbHour.Size = new System.Drawing.Size(62, 28);
            this.cmbHour.TabIndex = 0;
            this.cmbHour.SelectedIndexChanged += new System.EventHandler(this.cmbHour_SelectedIndexChanged);
            // 
            // cmbMinute
            // 
            this.cmbMinute.FormattingEnabled = true;
            this.cmbMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.cmbMinute.Location = new System.Drawing.Point(206, 19);
            this.cmbMinute.Name = "cmbMinute";
            this.cmbMinute.Size = new System.Drawing.Size(62, 28);
            this.cmbMinute.TabIndex = 1;
            this.cmbMinute.SelectedIndexChanged += new System.EventHandler(this.cmbMinute_SelectedIndexChanged);
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Location = new System.Drawing.Point(15, 22);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(48, 20);
            this.lblHour.TabIndex = 2;
            this.lblHour.Text = "Hour:";
            // 
            // lblMinute
            // 
            this.lblMinute.AutoSize = true;
            this.lblMinute.Location = new System.Drawing.Point(139, 22);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(61, 20);
            this.lblMinute.TabIndex = 3;
            this.lblMinute.Text = "Minute:";
            // 
            // grpType
            // 
            this.grpType.Controls.Add(this.rdbCustom);
            this.grpType.Controls.Add(this.rdbDefined);
            this.grpType.Location = new System.Drawing.Point(19, 53);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(387, 62);
            this.grpType.TabIndex = 4;
            this.grpType.TabStop = false;
            this.grpType.Text = "Type of Route";
            // 
            // rdbCustom
            // 
            this.rdbCustom.AutoSize = true;
            this.rdbCustom.Location = new System.Drawing.Point(231, 25);
            this.rdbCustom.Name = "rdbCustom";
            this.rdbCustom.Size = new System.Drawing.Size(89, 24);
            this.rdbCustom.TabIndex = 1;
            this.rdbCustom.TabStop = true;
            this.rdbCustom.Text = "Custom";
            this.rdbCustom.UseVisualStyleBackColor = true;
            this.rdbCustom.CheckedChanged += new System.EventHandler(this.rdbCustom_CheckedChanged);
            // 
            // rdbDefined
            // 
            this.rdbDefined.AutoSize = true;
            this.rdbDefined.Location = new System.Drawing.Point(50, 25);
            this.rdbDefined.Name = "rdbDefined";
            this.rdbDefined.Size = new System.Drawing.Size(90, 24);
            this.rdbDefined.TabIndex = 0;
            this.rdbDefined.TabStop = true;
            this.rdbDefined.Text = "Defined";
            this.rdbDefined.UseVisualStyleBackColor = true;
            this.rdbDefined.CheckedChanged += new System.EventHandler(this.rdbDefined_CheckedChanged);
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Location = new System.Drawing.Point(15, 122);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(106, 20);
            this.lblSelect.TabIndex = 5;
            this.lblSelect.Text = "Select Route:";
            // 
            // cmbSelect
            // 
            this.cmbSelect.FormattingEnabled = true;
            this.cmbSelect.Items.AddRange(new object[] {
            "Red",
            "Green"});
            this.cmbSelect.Location = new System.Drawing.Point(131, 119);
            this.cmbSelect.Name = "cmbSelect";
            this.cmbSelect.Size = new System.Drawing.Size(275, 28);
            this.cmbSelect.TabIndex = 6;
            this.cmbSelect.SelectedIndexChanged += new System.EventHandler(this.cmbSelect_SelectedIndexChanged);
            // 
            // lblCustom
            // 
            this.lblCustom.AutoSize = true;
            this.lblCustom.Location = new System.Drawing.Point(15, 154);
            this.lblCustom.Name = "lblCustom";
            this.lblCustom.Size = new System.Drawing.Size(162, 20);
            this.lblCustom.TabIndex = 7;
            this.lblCustom.Text = "Custom Route Stops:";
            // 
            // txtCustom
            // 
            this.txtCustom.Enabled = false;
            this.txtCustom.Location = new System.Drawing.Point(19, 177);
            this.txtCustom.Name = "txtCustom";
            this.txtCustom.Size = new System.Drawing.Size(387, 26);
            this.txtCustom.TabIndex = 8;
            this.txtCustom.TextChanged += new System.EventHandler(this.txtCustom_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(19, 210);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(179, 29);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(227, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 29);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblAMPM
            // 
            this.lblAMPM.AutoSize = true;
            this.lblAMPM.Location = new System.Drawing.Point(274, 22);
            this.lblAMPM.Name = "lblAMPM";
            this.lblAMPM.Size = new System.Drawing.Size(64, 20);
            this.lblAMPM.TabIndex = 11;
            this.lblAMPM.Text = "AM/PM:";
            // 
            // cmbAMPM
            // 
            this.cmbAMPM.FormattingEnabled = true;
            this.cmbAMPM.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.cmbAMPM.Location = new System.Drawing.Point(344, 19);
            this.cmbAMPM.Name = "cmbAMPM";
            this.cmbAMPM.Size = new System.Drawing.Size(62, 28);
            this.cmbAMPM.TabIndex = 12;
            this.cmbAMPM.SelectedIndexChanged += new System.EventHandler(this.cmbAMPM_SelectedIndexChanged);
            // 
            // AddEditGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 255);
            this.Controls.Add(this.cmbAMPM);
            this.Controls.Add(this.lblAMPM);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtCustom);
            this.Controls.Add(this.lblCustom);
            this.Controls.Add(this.cmbSelect);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.grpType);
            this.Controls.Add(this.lblMinute);
            this.Controls.Add(this.lblHour);
            this.Controls.Add(this.cmbMinute);
            this.Controls.Add(this.cmbHour);
            this.Name = "AddEditGUI";
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbHour;
        private System.Windows.Forms.ComboBox cmbMinute;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.Label lblMinute;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.RadioButton rdbCustom;
        private System.Windows.Forms.RadioButton rdbDefined;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.ComboBox cmbSelect;
        private System.Windows.Forms.Label lblCustom;
        private System.Windows.Forms.TextBox txtCustom;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblAMPM;
        private System.Windows.Forms.ComboBox cmbAMPM;
    }
}
