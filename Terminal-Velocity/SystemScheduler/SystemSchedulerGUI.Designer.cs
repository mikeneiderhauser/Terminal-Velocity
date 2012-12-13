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
            ((System.ComponentModel.ISupportInitialize)(this.grdDispatches)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(576, 18);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(103, 35);
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
            this.txtFilepath.Size = new System.Drawing.Size(555, 26);
            this.txtFilepath.TabIndex = 1;
            // 
            // grdDispatches
            // 
            this.grdDispatches.AllowUserToAddRows = false;
            this.grdDispatches.AllowUserToDeleteRows = false;
            this.grdDispatches.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdDispatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDispatches.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdDispatches.Location = new System.Drawing.Point(15, 66);
            this.grdDispatches.MultiSelect = false;
            this.grdDispatches.Name = "grdDispatches";
            this.grdDispatches.ReadOnly = true;
            this.grdDispatches.RowHeadersVisible = false;
            this.grdDispatches.RowTemplate.Height = 28;
            this.grdDispatches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDispatches.Size = new System.Drawing.Size(664, 208);
            this.grdDispatches.TabIndex = 2;
            this.grdDispatches.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDispatches_CellClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(15, 291);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(210, 35);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(242, 291);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(210, 35);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit...";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(469, 291);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(210, 35);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "CSV|*.csv";
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
            this.Size = new System.Drawing.Size(693, 342);
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
    }
}
