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
            this.components = new System.ComponentModel.Container();
            this.trainInfoTextBox = new System.Windows.Forms.TextBox();
            this.allTrainComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.doorsValueText = new System.Windows.Forms.TextBox();
            this.doorsTextBox = new System.Windows.Forms.TextBox();
            this.lightsValueText = new System.Windows.Forms.TextBox();
            this.lightsTextBox = new System.Windows.Forms.TextBox();
            this.numCrewValueText = new System.Windows.Forms.TextBox();
            this.numCrewTextBox = new System.Windows.Forms.TextBox();
            this.numPassengersValueText = new System.Windows.Forms.TextBox();
            this.numPassengersTextBox = new System.Windows.Forms.TextBox();
            this.massValueText = new System.Windows.Forms.TextBox();
            this.massTextBox = new System.Windows.Forms.TextBox();
            this.gradeValueText = new System.Windows.Forms.TextBox();
            this.gradeTextBox = new System.Windows.Forms.TextBox();
            this.accelerationValueText = new System.Windows.Forms.TextBox();
            this.accelerationTextBox = new System.Windows.Forms.TextBox();
            this.velocityValueText = new System.Windows.Forms.TextBox();
            this.velocityTextBox = new System.Windows.Forms.TextBox();
            this.positionValueText = new System.Windows.Forms.TextBox();
            this.outputVariableTextBox = new System.Windows.Forms.TextBox();
            this.outputValueTextBox = new System.Windows.Forms.TextBox();
            this.positionTextBox = new System.Windows.Forms.TextBox();
            this.simulationEnvironmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.trainLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationEnvironmentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // trainInfoTextBox
            // 
            this.trainInfoTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trainInfoTextBox.Location = new System.Drawing.Point(25, 92);
            this.trainInfoTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trainInfoTextBox.Multiline = true;
            this.trainInfoTextBox.Name = "trainInfoTextBox";
            this.trainInfoTextBox.ReadOnly = true;
            this.trainInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.trainInfoTextBox.Size = new System.Drawing.Size(531, 382);
            this.trainInfoTextBox.TabIndex = 0;
            this.trainInfoTextBox.TextChanged += new System.EventHandler(this.trainInfoTextBox_TextChanged);
            // 
            // allTrainComboBox
            // 
            this.allTrainComboBox.FormattingEnabled = true;
            this.allTrainComboBox.Location = new System.Drawing.Point(668, 59);
            this.allTrainComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.allTrainComboBox.Name = "allTrainComboBox";
            this.allTrainComboBox.Size = new System.Drawing.Size(160, 24);
            this.allTrainComboBox.TabIndex = 1;
            this.allTrainComboBox.SelectedIndexChanged += new System.EventHandler(this.allTrainComboBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.doorsValueText, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.doorsTextBox, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.lightsValueText, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lightsTextBox, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.numCrewValueText, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.numCrewTextBox, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.numPassengersValueText, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.numPassengersTextBox, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.massValueText, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.massTextBox, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.gradeValueText, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.gradeTextBox, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.accelerationValueText, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.accelerationTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.velocityValueText, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.velocityTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.positionValueText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.outputVariableTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.outputValueTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.positionTextBox, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(600, 92);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(296, 474);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // doorsValueText
            // 
            this.doorsValueText.Location = new System.Drawing.Point(152, 428);
            this.doorsValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.doorsValueText.Multiline = true;
            this.doorsValueText.Name = "doorsValueText";
            this.doorsValueText.ReadOnly = true;
            this.doorsValueText.Size = new System.Drawing.Size(137, 38);
            this.doorsValueText.TabIndex = 19;
            this.doorsValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // doorsTextBox
            // 
            this.doorsTextBox.Location = new System.Drawing.Point(5, 428);
            this.doorsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.doorsTextBox.Multiline = true;
            this.doorsTextBox.Name = "doorsTextBox";
            this.doorsTextBox.ReadOnly = true;
            this.doorsTextBox.Size = new System.Drawing.Size(136, 38);
            this.doorsTextBox.TabIndex = 18;
            this.doorsTextBox.Text = "Doors";
            this.doorsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lightsValueText
            // 
            this.lightsValueText.Location = new System.Drawing.Point(152, 381);
            this.lightsValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lightsValueText.Multiline = true;
            this.lightsValueText.Name = "lightsValueText";
            this.lightsValueText.ReadOnly = true;
            this.lightsValueText.Size = new System.Drawing.Size(137, 37);
            this.lightsValueText.TabIndex = 17;
            this.lightsValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lightsTextBox
            // 
            this.lightsTextBox.Location = new System.Drawing.Point(5, 381);
            this.lightsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lightsTextBox.Multiline = true;
            this.lightsTextBox.Name = "lightsTextBox";
            this.lightsTextBox.ReadOnly = true;
            this.lightsTextBox.Size = new System.Drawing.Size(136, 37);
            this.lightsTextBox.TabIndex = 16;
            this.lightsTextBox.Text = "Lights";
            this.lightsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numCrewValueText
            // 
            this.numCrewValueText.Location = new System.Drawing.Point(152, 334);
            this.numCrewValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numCrewValueText.Multiline = true;
            this.numCrewValueText.Name = "numCrewValueText";
            this.numCrewValueText.ReadOnly = true;
            this.numCrewValueText.Size = new System.Drawing.Size(137, 37);
            this.numCrewValueText.TabIndex = 15;
            this.numCrewValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numCrewTextBox
            // 
            this.numCrewTextBox.Location = new System.Drawing.Point(5, 334);
            this.numCrewTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numCrewTextBox.Multiline = true;
            this.numCrewTextBox.Name = "numCrewTextBox";
            this.numCrewTextBox.ReadOnly = true;
            this.numCrewTextBox.Size = new System.Drawing.Size(136, 37);
            this.numCrewTextBox.TabIndex = 14;
            this.numCrewTextBox.Text = "Crew";
            this.numCrewTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numPassengersValueText
            // 
            this.numPassengersValueText.Location = new System.Drawing.Point(152, 287);
            this.numPassengersValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numPassengersValueText.Multiline = true;
            this.numPassengersValueText.Name = "numPassengersValueText";
            this.numPassengersValueText.ReadOnly = true;
            this.numPassengersValueText.Size = new System.Drawing.Size(137, 37);
            this.numPassengersValueText.TabIndex = 13;
            this.numPassengersValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numPassengersTextBox
            // 
            this.numPassengersTextBox.Location = new System.Drawing.Point(5, 287);
            this.numPassengersTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numPassengersTextBox.Multiline = true;
            this.numPassengersTextBox.Name = "numPassengersTextBox";
            this.numPassengersTextBox.ReadOnly = true;
            this.numPassengersTextBox.Size = new System.Drawing.Size(136, 37);
            this.numPassengersTextBox.TabIndex = 12;
            this.numPassengersTextBox.Text = "Passengers";
            this.numPassengersTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // massValueText
            // 
            this.massValueText.Location = new System.Drawing.Point(152, 240);
            this.massValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.massValueText.Multiline = true;
            this.massValueText.Name = "massValueText";
            this.massValueText.ReadOnly = true;
            this.massValueText.Size = new System.Drawing.Size(137, 37);
            this.massValueText.TabIndex = 11;
            this.massValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // massTextBox
            // 
            this.massTextBox.Location = new System.Drawing.Point(5, 240);
            this.massTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.massTextBox.Multiline = true;
            this.massTextBox.Name = "massTextBox";
            this.massTextBox.ReadOnly = true;
            this.massTextBox.Size = new System.Drawing.Size(136, 37);
            this.massTextBox.TabIndex = 10;
            this.massTextBox.Text = "Current Mass";
            this.massTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradeValueText
            // 
            this.gradeValueText.Location = new System.Drawing.Point(152, 193);
            this.gradeValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gradeValueText.Multiline = true;
            this.gradeValueText.Name = "gradeValueText";
            this.gradeValueText.ReadOnly = true;
            this.gradeValueText.Size = new System.Drawing.Size(137, 37);
            this.gradeValueText.TabIndex = 9;
            this.gradeValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradeTextBox
            // 
            this.gradeTextBox.Location = new System.Drawing.Point(5, 193);
            this.gradeTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gradeTextBox.Multiline = true;
            this.gradeTextBox.Name = "gradeTextBox";
            this.gradeTextBox.ReadOnly = true;
            this.gradeTextBox.Size = new System.Drawing.Size(136, 37);
            this.gradeTextBox.TabIndex = 8;
            this.gradeTextBox.Text = "Current Grade";
            this.gradeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // accelerationValueText
            // 
            this.accelerationValueText.Location = new System.Drawing.Point(152, 146);
            this.accelerationValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.accelerationValueText.Multiline = true;
            this.accelerationValueText.Name = "accelerationValueText";
            this.accelerationValueText.ReadOnly = true;
            this.accelerationValueText.Size = new System.Drawing.Size(137, 37);
            this.accelerationValueText.TabIndex = 7;
            this.accelerationValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // accelerationTextBox
            // 
            this.accelerationTextBox.Location = new System.Drawing.Point(5, 146);
            this.accelerationTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.accelerationTextBox.Multiline = true;
            this.accelerationTextBox.Name = "accelerationTextBox";
            this.accelerationTextBox.ReadOnly = true;
            this.accelerationTextBox.Size = new System.Drawing.Size(136, 37);
            this.accelerationTextBox.TabIndex = 6;
            this.accelerationTextBox.Text = "Current Acceleration";
            this.accelerationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // velocityValueText
            // 
            this.velocityValueText.Location = new System.Drawing.Point(152, 99);
            this.velocityValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.velocityValueText.Multiline = true;
            this.velocityValueText.Name = "velocityValueText";
            this.velocityValueText.ReadOnly = true;
            this.velocityValueText.Size = new System.Drawing.Size(137, 37);
            this.velocityValueText.TabIndex = 5;
            this.velocityValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // velocityTextBox
            // 
            this.velocityTextBox.Location = new System.Drawing.Point(5, 99);
            this.velocityTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.velocityTextBox.Multiline = true;
            this.velocityTextBox.Name = "velocityTextBox";
            this.velocityTextBox.ReadOnly = true;
            this.velocityTextBox.Size = new System.Drawing.Size(136, 37);
            this.velocityTextBox.TabIndex = 4;
            this.velocityTextBox.Text = "Current Velocity";
            this.velocityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // positionValueText
            // 
            this.positionValueText.Location = new System.Drawing.Point(152, 52);
            this.positionValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.positionValueText.Multiline = true;
            this.positionValueText.Name = "positionValueText";
            this.positionValueText.ReadOnly = true;
            this.positionValueText.Size = new System.Drawing.Size(137, 37);
            this.positionValueText.TabIndex = 3;
            this.positionValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // outputVariableTextBox
            // 
            this.outputVariableTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputVariableTextBox.Location = new System.Drawing.Point(5, 5);
            this.outputVariableTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.outputVariableTextBox.Multiline = true;
            this.outputVariableTextBox.Name = "outputVariableTextBox";
            this.outputVariableTextBox.ReadOnly = true;
            this.outputVariableTextBox.Size = new System.Drawing.Size(136, 37);
            this.outputVariableTextBox.TabIndex = 0;
            this.outputVariableTextBox.Text = "Output Variables";
            this.outputVariableTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // outputValueTextBox
            // 
            this.outputValueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputValueTextBox.Location = new System.Drawing.Point(152, 5);
            this.outputValueTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.outputValueTextBox.Multiline = true;
            this.outputValueTextBox.Name = "outputValueTextBox";
            this.outputValueTextBox.ReadOnly = true;
            this.outputValueTextBox.Size = new System.Drawing.Size(137, 37);
            this.outputValueTextBox.TabIndex = 1;
            this.outputValueTextBox.Text = "Output Values";
            this.outputValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // positionTextBox
            // 
            this.positionTextBox.Location = new System.Drawing.Point(5, 52);
            this.positionTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.positionTextBox.Multiline = true;
            this.positionTextBox.Name = "positionTextBox";
            this.positionTextBox.ReadOnly = true;
            this.positionTextBox.Size = new System.Drawing.Size(136, 37);
            this.positionTextBox.TabIndex = 2;
            this.positionTextBox.Text = "Current Position";
            this.positionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // simulationEnvironmentBindingSource
            // 
            this.simulationEnvironmentBindingSource.DataSource = typeof(SimulationEnvironment.SimulationEnvironment);
            // 
            // trainLabel
            // 
            this.trainLabel.AutoSize = true;
            this.trainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trainLabel.Location = new System.Drawing.Point(407, 22);
            this.trainLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.trainLabel.Name = "trainLabel";
            this.trainLabel.Size = new System.Drawing.Size(0, 42);
            this.trainLabel.TabIndex = 3;
            // 
            // TrainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trainLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.allTrainComboBox);
            this.Controls.Add(this.trainInfoTextBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TrainGUI";
            this.Size = new System.Drawing.Size(935, 596);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationEnvironmentBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox trainInfoTextBox;
        private System.Windows.Forms.ComboBox allTrainComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox outputVariableTextBox;
        private System.Windows.Forms.TextBox outputValueTextBox;
        private System.Windows.Forms.TextBox doorsValueText;
        private System.Windows.Forms.TextBox doorsTextBox;
        private System.Windows.Forms.TextBox lightsValueText;
        private System.Windows.Forms.TextBox lightsTextBox;
        private System.Windows.Forms.TextBox numCrewValueText;
        private System.Windows.Forms.TextBox numCrewTextBox;
        private System.Windows.Forms.TextBox numPassengersValueText;
        private System.Windows.Forms.TextBox numPassengersTextBox;
        private System.Windows.Forms.TextBox massValueText;
        private System.Windows.Forms.TextBox massTextBox;
        private System.Windows.Forms.TextBox gradeValueText;
        private System.Windows.Forms.TextBox gradeTextBox;
        private System.Windows.Forms.TextBox accelerationValueText;
        private System.Windows.Forms.TextBox accelerationTextBox;
        private System.Windows.Forms.TextBox velocityValueText;
        private System.Windows.Forms.TextBox velocityTextBox;
        private System.Windows.Forms.TextBox positionValueText;
        private System.Windows.Forms.TextBox positionTextBox;
        private System.Windows.Forms.BindingSource simulationEnvironmentBindingSource;
        private System.Windows.Forms.Label trainLabel;

    }
}
