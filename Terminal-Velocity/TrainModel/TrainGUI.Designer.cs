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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.outputVariableTextBox = new System.Windows.Forms.TextBox();
            this.outputValueTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trainInfoTextBox
            // 
            this.trainInfoTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trainInfoTextBox.Location = new System.Drawing.Point(19, 75);
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
            this.allTrainComboBox.Location = new System.Drawing.Point(501, 48);
            this.allTrainComboBox.Name = "allTrainComboBox";
            this.allTrainComboBox.Size = new System.Drawing.Size(121, 21);
            this.allTrainComboBox.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.outputVariableTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.outputValueTextBox, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(450, 75);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(222, 385);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // outputVariableTextBox
            // 
            this.outputVariableTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputVariableTextBox.Location = new System.Drawing.Point(3, 3);
            this.outputVariableTextBox.Multiline = true;
            this.outputVariableTextBox.Name = "outputVariableTextBox";
            this.outputVariableTextBox.ReadOnly = true;
            this.outputVariableTextBox.Size = new System.Drawing.Size(105, 32);
            this.outputVariableTextBox.TabIndex = 0;
            this.outputVariableTextBox.Text = "Output Variables";
            this.outputVariableTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // outputValueTextBox
            // 
            this.outputValueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputValueTextBox.Location = new System.Drawing.Point(114, 3);
            this.outputValueTextBox.Multiline = true;
            this.outputValueTextBox.Name = "outputValueTextBox";
            this.outputValueTextBox.ReadOnly = true;
            this.outputValueTextBox.Size = new System.Drawing.Size(105, 32);
            this.outputValueTextBox.TabIndex = 1;
            this.outputValueTextBox.Text = "Output Values";
            this.outputValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TrainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.allTrainComboBox);
            this.Controls.Add(this.trainInfoTextBox);
            this.Name = "TrainGUI";
            this.Size = new System.Drawing.Size(701, 484);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox trainInfoTextBox;
        private System.Windows.Forms.ComboBox allTrainComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox outputVariableTextBox;
        private System.Windows.Forms.TextBox outputValueTextBox;

    }
}
