namespace TrackController
{
    partial class TrackControllerUi
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
            this.okButton = new System.Windows.Forms.Button();
            this.tcComboBox = new System.Windows.Forms.ComboBox();
            this.trainInformation = new System.Windows.Forms.GroupBox();
            this.blockGrid = new System.Windows.Forms.DataGridView();
            this.StateBlockID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SwitchDest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trainGrid = new System.Windows.Forms.DataGridView();
            this.tcListBoxInfo = new System.Windows.Forms.ListBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.tcCountBox = new System.Windows.Forms.TextBox();
            this.selectTCLabel = new System.Windows.Forms.Label();
            this.TrainID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RouteBlocks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Authority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trainInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(525, 66);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // tcComboBox
            // 
            this.tcComboBox.FormattingEnabled = true;
            this.tcComboBox.Location = new System.Drawing.Point(476, 39);
            this.tcComboBox.Name = "tcComboBox";
            this.tcComboBox.Size = new System.Drawing.Size(121, 21);
            this.tcComboBox.TabIndex = 2;
            // 
            // trainInformation
            // 
            this.trainInformation.Controls.Add(this.blockGrid);
            this.trainInformation.Controls.Add(this.trainGrid);
            this.trainInformation.Controls.Add(this.tcListBoxInfo);
            this.trainInformation.Controls.Add(this.nextButton);
            this.trainInformation.Controls.Add(this.prevButton);
            this.trainInformation.Location = new System.Drawing.Point(4, 4);
            this.trainInformation.Name = "trainInformation";
            this.trainInformation.Size = new System.Drawing.Size(466, 593);
            this.trainInformation.TabIndex = 3;
            this.trainInformation.TabStop = false;
            this.trainInformation.Text = "Track Controller Information";
            // 
            // blockGrid
            // 
            this.blockGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.blockGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.blockGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StateBlockID,
            this.State,
            this.SwitchDest});
            this.blockGrid.Location = new System.Drawing.Point(6, 437);
            this.blockGrid.Name = "blockGrid";
            this.blockGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.blockGrid.Size = new System.Drawing.Size(453, 150);
            this.blockGrid.TabIndex = 11;
            // 
            // StateBlockID
            // 
            this.StateBlockID.HeaderText = "Block ID";
            this.StateBlockID.Name = "StateBlockID";
            this.StateBlockID.ReadOnly = true;
            // 
            // State
            // 
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // SwitchDest
            // 
            this.SwitchDest.HeaderText = "Switch Destination";
            this.SwitchDest.Name = "SwitchDest";
            this.SwitchDest.ReadOnly = true;
            // 
            // trainGrid
            // 
            this.trainGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.trainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trainGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TrainID,
            this.RouteBlocks,
            this.Authority,
            this.Speed});
            this.trainGrid.Location = new System.Drawing.Point(6, 292);
            this.trainGrid.Name = "trainGrid";
            this.trainGrid.ReadOnly = true;
            this.trainGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.trainGrid.Size = new System.Drawing.Size(453, 139);
            this.trainGrid.TabIndex = 10;
            // 
            // tcListBoxInfo
            // 
            this.tcListBoxInfo.FormattingEnabled = true;
            this.tcListBoxInfo.Location = new System.Drawing.Point(87, 22);
            this.tcListBoxInfo.Name = "tcListBoxInfo";
            this.tcListBoxInfo.Size = new System.Drawing.Size(292, 121);
            this.tcListBoxInfo.TabIndex = 9;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(385, 62);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 8;
            this.nextButton.Text = "Next TC";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButtonClick);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(6, 62);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(75, 23);
            this.prevButton.TabIndex = 7;
            this.prevButton.Text = "Prev TC";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.PrevButtonClick);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(476, 166);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(121, 431);
            this.messageTextBox.TabIndex = 4;
            this.messageTextBox.Text = "";
            // 
            // tcCountBox
            // 
            this.tcCountBox.Location = new System.Drawing.Point(476, 134);
            this.tcCountBox.Name = "tcCountBox";
            this.tcCountBox.ReadOnly = true;
            this.tcCountBox.Size = new System.Drawing.Size(121, 20);
            this.tcCountBox.TabIndex = 5;
            // 
            // selectTCLabel
            // 
            this.selectTCLabel.AutoSize = true;
            this.selectTCLabel.Location = new System.Drawing.Point(476, 14);
            this.selectTCLabel.Name = "selectTCLabel";
            this.selectTCLabel.Size = new System.Drawing.Size(115, 13);
            this.selectTCLabel.TabIndex = 6;
            this.selectTCLabel.Text = "Select Track Controller";
            // 
            // TrainID
            // 
            this.TrainID.HeaderText = "Train ID";
            this.TrainID.Name = "TrainID";
            this.TrainID.ReadOnly = true;
            // 
            // RouteBlocks
            // 
            this.RouteBlocks.HeaderText = "Route Blocks";
            this.RouteBlocks.Name = "RouteBlocks";
            this.RouteBlocks.ReadOnly = true;
            // 
            // Authority
            // 
            this.Authority.HeaderText = "Authority";
            this.Authority.Name = "Authority";
            this.Authority.ReadOnly = true;
            // 
            // Speed
            // 
            this.Speed.HeaderText = "Speed";
            this.Speed.Name = "Speed";
            this.Speed.ReadOnly = true;
            // 
            // TrackControllerUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectTCLabel);
            this.Controls.Add(this.tcCountBox);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.trainInformation);
            this.Controls.Add(this.tcComboBox);
            this.Controls.Add(this.okButton);
            this.Name = "TrackControllerUi";
            this.Size = new System.Drawing.Size(600, 600);
            this.trainInformation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blockGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox tcComboBox;
        private System.Windows.Forms.GroupBox trainInformation;
        private System.Windows.Forms.DataGridView blockGrid;
        private System.Windows.Forms.DataGridView trainGrid;
        private System.Windows.Forms.ListBox tcListBoxInfo;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.RichTextBox messageTextBox;
        private System.Windows.Forms.TextBox tcCountBox;
        private System.Windows.Forms.Label selectTCLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateBlockID;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn SwitchDest;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrainID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RouteBlocks;
        private System.Windows.Forms.DataGridViewTextBoxColumn Authority;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
    }
}
