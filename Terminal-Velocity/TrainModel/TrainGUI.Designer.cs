namespace TrainModel
{
    partial class TrainGUI
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
            this.trainInfoTextBox = new System.Windows.Forms.TextBox();
            this.allTrainComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // trainInfoTextBox
            // 
            this.trainInfoTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trainInfoTextBox.Location = new System.Drawing.Point(25, 52);
            this.trainInfoTextBox.Multiline = true;
            this.trainInfoTextBox.Name = "trainInfoTextBox";
            this.trainInfoTextBox.ReadOnly = true;
            this.trainInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.trainInfoTextBox.Size = new System.Drawing.Size(399, 311);
            this.trainInfoTextBox.TabIndex = 0;
            // 
            // allTrainComboBox
            // 
            this.allTrainComboBox.FormattingEnabled = true;
            this.allTrainComboBox.Location = new System.Drawing.Point(521, 25);
            this.allTrainComboBox.Name = "allTrainComboBox";
            this.allTrainComboBox.Size = new System.Drawing.Size(121, 21);
            this.allTrainComboBox.TabIndex = 1;
            // 
            // TrainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.allTrainComboBox);
            this.Controls.Add(this.trainInfoTextBox);
            this.Name = "TrainGUI";
            this.Size = new System.Drawing.Size(701, 484);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void DisplayError(string error)
        {
            System.Windows.Forms.MessageBox.Show(error, "Critical Error with Train", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        private System.Windows.Forms.TextBox trainInfoTextBox;
        private System.Windows.Forms.ComboBox allTrainComboBox;

    }
}
