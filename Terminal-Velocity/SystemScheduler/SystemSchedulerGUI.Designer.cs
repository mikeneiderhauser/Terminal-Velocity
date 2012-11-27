namespace SystemScheduler
{
    partial class SystemSchedulerGUI
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilepath = new System.Windows.Forms.TextBox();
            this.grdDispatches = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.DispatchID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispatchTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispatchRouteType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispatchRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdDispatches)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(284, 22);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(103, 26);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilepath
            // 
            this.txtFilepath.Location = new System.Drawing.Point(15, 22);
            this.txtFilepath.Name = "txtFilepath";
            this.txtFilepath.ReadOnly = true;
            this.txtFilepath.Size = new System.Drawing.Size(263, 26);
            this.txtFilepath.TabIndex = 1;
            // 
            // grdDispatches
            // 
            this.grdDispatches.AllowUserToAddRows = false;
            this.grdDispatches.AllowUserToDeleteRows = false;
            this.grdDispatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDispatches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DispatchID,
            this.DispatchTime,
            this.DispatchRouteType,
            this.DispatchRoute});
            this.grdDispatches.Location = new System.Drawing.Point(15, 66);
            this.grdDispatches.Name = "grdDispatches";
            this.grdDispatches.ReadOnly = true;
            this.grdDispatches.RowTemplate.Height = 28;
            this.grdDispatches.Size = new System.Drawing.Size(372, 150);
            this.grdDispatches.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(15, 223);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 26);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(97, 222);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 27);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit...";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(178, 223);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 26);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add...";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "CSV|*.csv";
            // 
            // DispatchID
            // 
            this.DispatchID.HeaderText = "Dispatch ID";
            this.DispatchID.Name = "DispatchID";
            this.DispatchID.ReadOnly = true;
            // 
            // DispatchTime
            // 
            this.DispatchTime.HeaderText = "Dispatch Time";
            this.DispatchTime.Name = "DispatchTime";
            this.DispatchTime.ReadOnly = true;
            // 
            // DispatchRouteType
            // 
            this.DispatchRouteType.HeaderText = "DispatchRouteType";
            this.DispatchRouteType.Name = "DispatchRouteType";
            this.DispatchRouteType.ReadOnly = true;
            this.DispatchRouteType.Visible = false;
            // 
            // DispatchRoute
            // 
            this.DispatchRoute.HeaderText = "Dispatch Route";
            this.DispatchRoute.Name = "DispatchRoute";
            this.DispatchRoute.ReadOnly = true;
            // 
            // SystemSchedulerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grdDispatches);
            this.Controls.Add(this.txtFilepath);
            this.Controls.Add(this.btnBrowse);
            this.Name = "SystemSchedulerGUI";
            this.Size = new System.Drawing.Size(406, 281);
            ((System.ComponentModel.ISupportInitialize)(this.grdDispatches)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFilepath;
        private System.Windows.Forms.DataGridView grdDispatches;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispatchID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispatchTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispatchRouteType;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispatchRoute;
    }
}
