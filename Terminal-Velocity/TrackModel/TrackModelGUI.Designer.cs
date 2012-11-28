namespace TrackModel
{
    partial class TrackModelGUI
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
            this.BlockInfoTitle = new System.Windows.Forms.Label();
            this.lblBlockID = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblHeater = new System.Windows.Forms.Label();
            this.lblCircuit = new System.Windows.Forms.Label();
            this.lblSwitch = new System.Windows.Forms.Label();
            this.lblTunnel = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.loadFileBtn = new System.Windows.Forms.Button();
            this.trackDisplayPanel = new System.Windows.Forms.Panel();
            this.valBlockID = new System.Windows.Forms.Label();
            this.valState = new System.Windows.Forms.Label();
            this.valHeater = new System.Windows.Forms.Label();
            this.valCircuit = new System.Windows.Forms.Label();
            this.valSwitch = new System.Windows.Forms.Label();
            this.valTunnel = new System.Windows.Forms.Label();
            this.valLine = new System.Windows.Forms.Label();
            this.titleBar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BlockInfoTitle
            // 
            this.BlockInfoTitle.AutoSize = true;
            this.BlockInfoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlockInfoTitle.Location = new System.Drawing.Point(125, 157);
            this.BlockInfoTitle.Name = "BlockInfoTitle";
            this.BlockInfoTitle.Size = new System.Drawing.Size(106, 25);
            this.BlockInfoTitle.TabIndex = 0;
            this.BlockInfoTitle.Text = "Block Info";
            //this.BlockInfoTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblBlockID
            // 
            this.lblBlockID.AutoSize = true;
            this.lblBlockID.Location = new System.Drawing.Point(109, 192);
            this.lblBlockID.Name = "lblBlockID";
            this.lblBlockID.Size = new System.Drawing.Size(51, 13);
            this.lblBlockID.TabIndex = 1;
            this.lblBlockID.Text = "Block ID:";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(125, 214);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(35, 13);
            this.lblState.TabIndex = 2;
            this.lblState.Text = "State:";
            // 
            // lblHeater
            // 
            this.lblHeater.AutoSize = true;
            this.lblHeater.Location = new System.Drawing.Point(118, 236);
            this.lblHeater.Name = "lblHeater";
            this.lblHeater.Size = new System.Drawing.Size(42, 13);
            this.lblHeater.TabIndex = 3;
            this.lblHeater.Text = "Heater:";
            //this.lblHeater.Click += new System.EventHandler(this.label4_Click);
            // 
            // lblCircuit
            // 
            this.lblCircuit.AutoSize = true;
            this.lblCircuit.Location = new System.Drawing.Point(118, 258);
            this.lblCircuit.Name = "lblCircuit";
            this.lblCircuit.Size = new System.Drawing.Size(42, 13);
            this.lblCircuit.TabIndex = 4;
            this.lblCircuit.Text = "Circuit: ";
            // 
            // lblSwitch
            // 
            this.lblSwitch.AutoSize = true;
            this.lblSwitch.Location = new System.Drawing.Point(118, 281);
            this.lblSwitch.Name = "lblSwitch";
            this.lblSwitch.Size = new System.Drawing.Size(42, 13);
            this.lblSwitch.TabIndex = 5;
            this.lblSwitch.Text = "Switch:";
            // 
            // lblTunnel
            // 
            this.lblTunnel.AutoSize = true;
            this.lblTunnel.Location = new System.Drawing.Point(117, 307);
            this.lblTunnel.Name = "lblTunnel";
            this.lblTunnel.Size = new System.Drawing.Size(43, 13);
            this.lblTunnel.TabIndex = 6;
            this.lblTunnel.Text = "Tunnel:";
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Location = new System.Drawing.Point(127, 330);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(33, 13);
            this.lblLine.TabIndex = 7;
            this.lblLine.Text = "Line: ";
            // 
            // loadFileBtn
            // 
            this.loadFileBtn.Location = new System.Drawing.Point(12, 18);
            this.loadFileBtn.Name = "loadFileBtn";
            this.loadFileBtn.Size = new System.Drawing.Size(75, 23);
            this.loadFileBtn.TabIndex = 8;
            this.loadFileBtn.Text = "Load File";
            this.loadFileBtn.UseVisualStyleBackColor = true;
            this.loadFileBtn.Click += new System.EventHandler(this.loadFileBtn_Click);
            // 
            // trackDisplayPanel
            // 
            this.trackDisplayPanel.BackColor = System.Drawing.SystemColors.Window;
            this.trackDisplayPanel.Location = new System.Drawing.Point(336, 3);
            this.trackDisplayPanel.Name = "trackDisplayPanel";
            this.trackDisplayPanel.Size = new System.Drawing.Size(385, 317);
            this.trackDisplayPanel.TabIndex = 9;
            // 
            // valBlockID
            // 
            this.valBlockID.AutoSize = true;
            this.valBlockID.Location = new System.Drawing.Point(166, 192);
            this.valBlockID.Name = "valBlockID";
            this.valBlockID.Size = new System.Drawing.Size(90, 13);
            this.valBlockID.TabIndex = 10;
            this.valBlockID.Text = "NoBlockSelected";
            // 
            // valState
            // 
            this.valState.AutoSize = true;
            this.valState.Location = new System.Drawing.Point(166, 214);
            this.valState.Name = "valState";
            this.valState.Size = new System.Drawing.Size(90, 13);
            this.valState.TabIndex = 11;
            this.valState.Text = "NoBlockSelected";
            // 
            // valHeater
            // 
            this.valHeater.AutoSize = true;
            this.valHeater.Location = new System.Drawing.Point(166, 236);
            this.valHeater.Name = "valHeater";
            this.valHeater.Size = new System.Drawing.Size(90, 13);
            this.valHeater.TabIndex = 12;
            this.valHeater.Text = "NoBlockSelected";
            // 
            // valCircuit
            // 
            this.valCircuit.AutoSize = true;
            this.valCircuit.Location = new System.Drawing.Point(166, 258);
            this.valCircuit.Name = "valCircuit";
            this.valCircuit.Size = new System.Drawing.Size(90, 13);
            this.valCircuit.TabIndex = 13;
            this.valCircuit.Text = "NoBlockSelected";
            // 
            // valSwitch
            // 
            this.valSwitch.AutoSize = true;
            this.valSwitch.Location = new System.Drawing.Point(167, 281);
            this.valSwitch.Name = "valSwitch";
            this.valSwitch.Size = new System.Drawing.Size(90, 13);
            this.valSwitch.TabIndex = 14;
            this.valSwitch.Text = "NoBlockSelected";
            // 
            // valTunnel
            // 
            this.valTunnel.AutoSize = true;
            this.valTunnel.Location = new System.Drawing.Point(167, 307);
            this.valTunnel.Name = "valTunnel";
            this.valTunnel.Size = new System.Drawing.Size(90, 13);
            this.valTunnel.TabIndex = 15;
            this.valTunnel.Text = "NoBlockSelected";
            // 
            // valLine
            // 
            this.valLine.AutoSize = true;
            this.valLine.Location = new System.Drawing.Point(167, 330);
            this.valLine.Name = "valLine";
            this.valLine.Size = new System.Drawing.Size(90, 13);
            this.valLine.TabIndex = 16;
            this.valLine.Text = "NoBlockSelected";
            // 
            // titleBar
            // 
            this.titleBar.AutoSize = true;
            this.titleBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar.Location = new System.Drawing.Point(93, 21);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(222, 20);
            this.titleBar.TabIndex = 17;
            this.titleBar.Text = "Terminal Velocity: Track Model";
            // 
            // TrackModelGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.valLine);
            this.Controls.Add(this.valTunnel);
            this.Controls.Add(this.valSwitch);
            this.Controls.Add(this.valCircuit);
            this.Controls.Add(this.valHeater);
            this.Controls.Add(this.valState);
            this.Controls.Add(this.valBlockID);
            this.Controls.Add(this.trackDisplayPanel);
            this.Controls.Add(this.loadFileBtn);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.lblTunnel);
            this.Controls.Add(this.lblSwitch);
            this.Controls.Add(this.lblCircuit);
            this.Controls.Add(this.lblHeater);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblBlockID);
            this.Controls.Add(this.BlockInfoTitle);
            this.Name = "TrackModelGUI";
            this.Size = new System.Drawing.Size(724, 389);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BlockInfoTitle;
        private System.Windows.Forms.Label lblBlockID;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblHeater;
        private System.Windows.Forms.Label lblCircuit;
        private System.Windows.Forms.Label lblSwitch;
        private System.Windows.Forms.Label lblTunnel;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Button loadFileBtn;
        private System.Windows.Forms.Panel trackDisplayPanel;
        private System.Windows.Forms.Label valBlockID;
        private System.Windows.Forms.Label valState;
        private System.Windows.Forms.Label valHeater;
        private System.Windows.Forms.Label valCircuit;
        private System.Windows.Forms.Label valSwitch;
        private System.Windows.Forms.Label valTunnel;
        private System.Windows.Forms.Label valLine;
        private System.Windows.Forms.Label titleBar;
    }
}
