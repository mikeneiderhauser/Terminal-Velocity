﻿namespace TrainModel
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
            this.trainLabel = new System.Windows.Forms.Label();
            this.buttonBrakeFailure = new System.Windows.Forms.Button();
            this.buttonEngineFailure = new System.Windows.Forms.Button();
            this.buttonSignalPickupFailure = new System.Windows.Forms.Button();
            this.buttonEmergencyBrake = new System.Windows.Forms.Button();
            this.signalPickupFailureLabel = new System.Windows.Forms.Label();
            this.engineFailureLabel = new System.Windows.Forms.Label();
            this.brakeFailureLabel = new System.Windows.Forms.Label();
            this.emergencyBrakeLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trainInfoTextBox
            // 
            this.trainInfoTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trainInfoTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.trainInfoTextBox.Location = new System.Drawing.Point(19, 75);
            this.trainInfoTextBox.Multiline = true;
            this.trainInfoTextBox.Name = "trainInfoTextBox";
            this.trainInfoTextBox.ReadOnly = true;
            this.trainInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.trainInfoTextBox.Size = new System.Drawing.Size(417, 301);
            this.trainInfoTextBox.TabIndex = 0;
            // 
            // allTrainComboBox
            // 
            this.allTrainComboBox.FormattingEnabled = true;
            this.allTrainComboBox.Location = new System.Drawing.Point(454, 48);
            this.allTrainComboBox.Name = "allTrainComboBox";
            this.allTrainComboBox.Size = new System.Drawing.Size(103, 21);
            this.allTrainComboBox.TabIndex = 1;
            this.allTrainComboBox.DropDown += new System.EventHandler(this.allTrainComboBox_DropDown);
            this.allTrainComboBox.SelectedIndexChanged += new System.EventHandler(this.allTrainComboBox_SelectedIndexChanged);
            this.allTrainComboBox.DropDownClosed += new System.EventHandler(this.allTrainComboBox_DropDownClosed);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(222, 385);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // doorsValueText
            // 
            this.doorsValueText.Location = new System.Drawing.Point(114, 346);
            this.doorsValueText.Multiline = true;
            this.doorsValueText.Name = "doorsValueText";
            this.doorsValueText.ReadOnly = true;
            this.doorsValueText.Size = new System.Drawing.Size(104, 32);
            this.doorsValueText.TabIndex = 19;
            this.doorsValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // doorsTextBox
            // 
            this.doorsTextBox.Location = new System.Drawing.Point(4, 346);
            this.doorsTextBox.Multiline = true;
            this.doorsTextBox.Name = "doorsTextBox";
            this.doorsTextBox.ReadOnly = true;
            this.doorsTextBox.Size = new System.Drawing.Size(103, 32);
            this.doorsTextBox.TabIndex = 18;
            this.doorsTextBox.Text = "Doors";
            this.doorsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lightsValueText
            // 
            this.lightsValueText.Location = new System.Drawing.Point(114, 308);
            this.lightsValueText.Multiline = true;
            this.lightsValueText.Name = "lightsValueText";
            this.lightsValueText.ReadOnly = true;
            this.lightsValueText.Size = new System.Drawing.Size(104, 31);
            this.lightsValueText.TabIndex = 17;
            this.lightsValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lightsTextBox
            // 
            this.lightsTextBox.Location = new System.Drawing.Point(4, 308);
            this.lightsTextBox.Multiline = true;
            this.lightsTextBox.Name = "lightsTextBox";
            this.lightsTextBox.ReadOnly = true;
            this.lightsTextBox.Size = new System.Drawing.Size(103, 31);
            this.lightsTextBox.TabIndex = 16;
            this.lightsTextBox.Text = "Lights";
            this.lightsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numCrewValueText
            // 
            this.numCrewValueText.Location = new System.Drawing.Point(114, 270);
            this.numCrewValueText.Multiline = true;
            this.numCrewValueText.Name = "numCrewValueText";
            this.numCrewValueText.ReadOnly = true;
            this.numCrewValueText.Size = new System.Drawing.Size(104, 31);
            this.numCrewValueText.TabIndex = 15;
            this.numCrewValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numCrewTextBox
            // 
            this.numCrewTextBox.Location = new System.Drawing.Point(4, 270);
            this.numCrewTextBox.Multiline = true;
            this.numCrewTextBox.Name = "numCrewTextBox";
            this.numCrewTextBox.ReadOnly = true;
            this.numCrewTextBox.Size = new System.Drawing.Size(103, 31);
            this.numCrewTextBox.TabIndex = 14;
            this.numCrewTextBox.Text = "Crew";
            this.numCrewTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numPassengersValueText
            // 
            this.numPassengersValueText.Location = new System.Drawing.Point(114, 232);
            this.numPassengersValueText.Multiline = true;
            this.numPassengersValueText.Name = "numPassengersValueText";
            this.numPassengersValueText.ReadOnly = true;
            this.numPassengersValueText.Size = new System.Drawing.Size(104, 31);
            this.numPassengersValueText.TabIndex = 13;
            this.numPassengersValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numPassengersTextBox
            // 
            this.numPassengersTextBox.Location = new System.Drawing.Point(4, 232);
            this.numPassengersTextBox.Multiline = true;
            this.numPassengersTextBox.Name = "numPassengersTextBox";
            this.numPassengersTextBox.ReadOnly = true;
            this.numPassengersTextBox.Size = new System.Drawing.Size(103, 31);
            this.numPassengersTextBox.TabIndex = 12;
            this.numPassengersTextBox.Text = "Passengers";
            this.numPassengersTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // massValueText
            // 
            this.massValueText.Location = new System.Drawing.Point(114, 194);
            this.massValueText.Multiline = true;
            this.massValueText.Name = "massValueText";
            this.massValueText.ReadOnly = true;
            this.massValueText.Size = new System.Drawing.Size(104, 31);
            this.massValueText.TabIndex = 11;
            this.massValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // massTextBox
            // 
            this.massTextBox.Location = new System.Drawing.Point(4, 194);
            this.massTextBox.Multiline = true;
            this.massTextBox.Name = "massTextBox";
            this.massTextBox.ReadOnly = true;
            this.massTextBox.Size = new System.Drawing.Size(103, 31);
            this.massTextBox.TabIndex = 10;
            this.massTextBox.Text = "Mass (kg)";
            this.massTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradeValueText
            // 
            this.gradeValueText.Location = new System.Drawing.Point(114, 156);
            this.gradeValueText.Multiline = true;
            this.gradeValueText.Name = "gradeValueText";
            this.gradeValueText.ReadOnly = true;
            this.gradeValueText.Size = new System.Drawing.Size(104, 31);
            this.gradeValueText.TabIndex = 9;
            this.gradeValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradeTextBox
            // 
            this.gradeTextBox.Location = new System.Drawing.Point(4, 156);
            this.gradeTextBox.Multiline = true;
            this.gradeTextBox.Name = "gradeTextBox";
            this.gradeTextBox.ReadOnly = true;
            this.gradeTextBox.Size = new System.Drawing.Size(103, 31);
            this.gradeTextBox.TabIndex = 8;
            this.gradeTextBox.Text = "Grade (%)";
            this.gradeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // accelerationValueText
            // 
            this.accelerationValueText.Location = new System.Drawing.Point(114, 118);
            this.accelerationValueText.Multiline = true;
            this.accelerationValueText.Name = "accelerationValueText";
            this.accelerationValueText.ReadOnly = true;
            this.accelerationValueText.Size = new System.Drawing.Size(104, 31);
            this.accelerationValueText.TabIndex = 7;
            this.accelerationValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // accelerationTextBox
            // 
            this.accelerationTextBox.Location = new System.Drawing.Point(4, 118);
            this.accelerationTextBox.Multiline = true;
            this.accelerationTextBox.Name = "accelerationTextBox";
            this.accelerationTextBox.ReadOnly = true;
            this.accelerationTextBox.Size = new System.Drawing.Size(103, 31);
            this.accelerationTextBox.TabIndex = 6;
            this.accelerationTextBox.Text = "Acceleration(m/s^2)";
            this.accelerationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // velocityValueText
            // 
            this.velocityValueText.Location = new System.Drawing.Point(114, 80);
            this.velocityValueText.Multiline = true;
            this.velocityValueText.Name = "velocityValueText";
            this.velocityValueText.ReadOnly = true;
            this.velocityValueText.Size = new System.Drawing.Size(104, 31);
            this.velocityValueText.TabIndex = 5;
            this.velocityValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // velocityTextBox
            // 
            this.velocityTextBox.Location = new System.Drawing.Point(4, 80);
            this.velocityTextBox.Multiline = true;
            this.velocityTextBox.Name = "velocityTextBox";
            this.velocityTextBox.ReadOnly = true;
            this.velocityTextBox.Size = new System.Drawing.Size(103, 31);
            this.velocityTextBox.TabIndex = 4;
            this.velocityTextBox.Text = "Velocity (m/s)";
            this.velocityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // positionValueText
            // 
            this.positionValueText.Location = new System.Drawing.Point(114, 42);
            this.positionValueText.Multiline = true;
            this.positionValueText.Name = "positionValueText";
            this.positionValueText.ReadOnly = true;
            this.positionValueText.Size = new System.Drawing.Size(104, 31);
            this.positionValueText.TabIndex = 3;
            this.positionValueText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // outputVariableTextBox
            // 
            this.outputVariableTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputVariableTextBox.Location = new System.Drawing.Point(4, 4);
            this.outputVariableTextBox.Multiline = true;
            this.outputVariableTextBox.Name = "outputVariableTextBox";
            this.outputVariableTextBox.ReadOnly = true;
            this.outputVariableTextBox.Size = new System.Drawing.Size(103, 31);
            this.outputVariableTextBox.TabIndex = 0;
            this.outputVariableTextBox.Text = "Output Variables";
            this.outputVariableTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // outputValueTextBox
            // 
            this.outputValueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputValueTextBox.Location = new System.Drawing.Point(114, 4);
            this.outputValueTextBox.Multiline = true;
            this.outputValueTextBox.Name = "outputValueTextBox";
            this.outputValueTextBox.ReadOnly = true;
            this.outputValueTextBox.Size = new System.Drawing.Size(104, 31);
            this.outputValueTextBox.TabIndex = 1;
            this.outputValueTextBox.Text = "Output Values";
            this.outputValueTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // positionTextBox
            // 
            this.positionTextBox.Location = new System.Drawing.Point(4, 42);
            this.positionTextBox.Multiline = true;
            this.positionTextBox.Name = "positionTextBox";
            this.positionTextBox.ReadOnly = true;
            this.positionTextBox.Size = new System.Drawing.Size(103, 31);
            this.positionTextBox.TabIndex = 2;
            this.positionTextBox.Text = "Position (m)";
            this.positionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trainLabel
            // 
            this.trainLabel.AutoSize = true;
            this.trainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trainLabel.Location = new System.Drawing.Point(161, 36);
            this.trainLabel.Name = "trainLabel";
            this.trainLabel.Size = new System.Drawing.Size(87, 33);
            this.trainLabel.TabIndex = 3;
            this.trainLabel.Text = "Train";
            // 
            // buttonBrakeFailure
            // 
            this.buttonBrakeFailure.Location = new System.Drawing.Point(19, 392);
            this.buttonBrakeFailure.Name = "buttonBrakeFailure";
            this.buttonBrakeFailure.Size = new System.Drawing.Size(91, 35);
            this.buttonBrakeFailure.TabIndex = 4;
            this.buttonBrakeFailure.Text = "Toggle Brake Failure";
            this.buttonBrakeFailure.UseVisualStyleBackColor = true;
            this.buttonBrakeFailure.Click += new System.EventHandler(this.buttonBrakeFailure_Click);
            // 
            // buttonEngineFailure
            // 
            this.buttonEngineFailure.Location = new System.Drawing.Point(116, 392);
            this.buttonEngineFailure.Name = "buttonEngineFailure";
            this.buttonEngineFailure.Size = new System.Drawing.Size(91, 35);
            this.buttonEngineFailure.TabIndex = 5;
            this.buttonEngineFailure.Text = "Toggle Engine Failure";
            this.buttonEngineFailure.UseVisualStyleBackColor = true;
            this.buttonEngineFailure.Click += new System.EventHandler(this.buttonEngineFailure_Click);
            // 
            // buttonSignalPickupFailure
            // 
            this.buttonSignalPickupFailure.Location = new System.Drawing.Point(214, 392);
            this.buttonSignalPickupFailure.Name = "buttonSignalPickupFailure";
            this.buttonSignalPickupFailure.Size = new System.Drawing.Size(91, 35);
            this.buttonSignalPickupFailure.TabIndex = 6;
            this.buttonSignalPickupFailure.Text = "Toggle Signal Pickup Failure";
            this.buttonSignalPickupFailure.UseVisualStyleBackColor = true;
            this.buttonSignalPickupFailure.Click += new System.EventHandler(this.buttonSignalPickupFailure_Click);
            // 
            // buttonEmergencyBrake
            // 
            this.buttonEmergencyBrake.Location = new System.Drawing.Point(320, 392);
            this.buttonEmergencyBrake.Name = "buttonEmergencyBrake";
            this.buttonEmergencyBrake.Size = new System.Drawing.Size(116, 35);
            this.buttonEmergencyBrake.TabIndex = 7;
            this.buttonEmergencyBrake.Text = "Apply Emergency Brake";
            this.buttonEmergencyBrake.UseVisualStyleBackColor = true;
            this.buttonEmergencyBrake.Click += new System.EventHandler(this.buttonEmergencyBrake_Click);
            // 
            // signalPickupFailureLabel
            // 
            this.signalPickupFailureLabel.AutoSize = true;
            this.signalPickupFailureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signalPickupFailureLabel.Location = new System.Drawing.Point(239, 430);
            this.signalPickupFailureLabel.Name = "signalPickupFailureLabel";
            this.signalPickupFailureLabel.Size = new System.Drawing.Size(37, 16);
            this.signalPickupFailureLabel.TabIndex = 14;
            this.signalPickupFailureLabel.Text = "false";
            // 
            // engineFailureLabel
            // 
            this.engineFailureLabel.AutoSize = true;
            this.engineFailureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.engineFailureLabel.Location = new System.Drawing.Point(142, 430);
            this.engineFailureLabel.Name = "engineFailureLabel";
            this.engineFailureLabel.Size = new System.Drawing.Size(37, 16);
            this.engineFailureLabel.TabIndex = 15;
            this.engineFailureLabel.Text = "false";
            // 
            // brakeFailureLabel
            // 
            this.brakeFailureLabel.AutoSize = true;
            this.brakeFailureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brakeFailureLabel.Location = new System.Drawing.Point(45, 430);
            this.brakeFailureLabel.Name = "brakeFailureLabel";
            this.brakeFailureLabel.Size = new System.Drawing.Size(37, 16);
            this.brakeFailureLabel.TabIndex = 16;
            this.brakeFailureLabel.Text = "false";
            // 
            // emergencyBrakeLabel
            // 
            this.emergencyBrakeLabel.AutoSize = true;
            this.emergencyBrakeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emergencyBrakeLabel.Location = new System.Drawing.Point(339, 430);
            this.emergencyBrakeLabel.Name = "emergencyBrakeLabel";
            this.emergencyBrakeLabel.Size = new System.Drawing.Size(71, 16);
            this.emergencyBrakeLabel.TabIndex = 17;
            this.emergencyBrakeLabel.Text = "toggled off";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(701, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userManualToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // userManualToolStripMenuItem
            // 
            this.userManualToolStripMenuItem.Name = "userManualToolStripMenuItem";
            this.userManualToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.userManualToolStripMenuItem.Text = "User Manual";
            this.userManualToolStripMenuItem.Click += new System.EventHandler(this.userManualToolStripMenuItem_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(580, 48);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(71, 21);
            this.pauseButton.TabIndex = 19;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // TrainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.emergencyBrakeLabel);
            this.Controls.Add(this.brakeFailureLabel);
            this.Controls.Add(this.engineFailureLabel);
            this.Controls.Add(this.signalPickupFailureLabel);
            this.Controls.Add(this.buttonEmergencyBrake);
            this.Controls.Add(this.buttonSignalPickupFailure);
            this.Controls.Add(this.buttonEngineFailure);
            this.Controls.Add(this.buttonBrakeFailure);
            this.Controls.Add(this.trainLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.allTrainComboBox);
            this.Controls.Add(this.trainInfoTextBox);
            this.Controls.Add(this.menuStrip1);
            this.Name = "TrainGUI";
            this.Size = new System.Drawing.Size(701, 484);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.Label trainLabel;
        private System.Windows.Forms.Button buttonBrakeFailure;
        private System.Windows.Forms.Button buttonEngineFailure;
        private System.Windows.Forms.Button buttonSignalPickupFailure;
        private System.Windows.Forms.Button buttonEmergencyBrake;
        private System.Windows.Forms.Label signalPickupFailureLabel;
        private System.Windows.Forms.Label engineFailureLabel;
        private System.Windows.Forms.Label brakeFailureLabel;
        private System.Windows.Forms.Label emergencyBrakeLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userManualToolStripMenuItem;
        private System.Windows.Forms.Button pauseButton;

    }
}
