namespace SimBioseTasks
{
    partial class View
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvTasks;

        private Panel panelDetail;

        private TextBox txtTitle;
        private TextBox txtDescription;
        private CheckBox chkCompleted;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        private SplitContainer split;
        private Panel panelTop;
        private Label lblId;

        private void InitializeComponent()
        {
            split = new SplitContainer();
            panelTop = new Panel();
            btnConfirm = new Button();
            dgvTasks = new DataGridView();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            panelDetail = new Panel();
            lblId = new Label();
            txtTitle = new TextBox();
            txtDescription = new TextBox();
            chkCompleted = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)split).BeginInit();
            split.Panel1.SuspendLayout();
            split.Panel2.SuspendLayout();
            split.SuspendLayout();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            panelDetail.SuspendLayout();
            SuspendLayout();
            // 
            // split
            // 
            split.Dock = DockStyle.Fill;
            split.Location = new Point(0, 0);
            split.Margin = new Padding(3, 4, 3, 4);
            split.Name = "split";
            split.Orientation = Orientation.Horizontal;
            // 
            // split.Panel1
            // 
            split.Panel1.Controls.Add(panelTop);
            // 
            // split.Panel2
            // 
            split.Panel2.Controls.Add(panelDetail);
            split.Size = new Size(1008, 761);
            split.SplitterDistance = 537;
            split.SplitterWidth = 5;
            split.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnConfirm);
            panelTop.Controls.Add(dgvTasks);
            panelTop.Controls.Add(btnAdd);
            panelTop.Controls.Add(btnUpdate);
            panelTop.Controls.Add(btnDelete);
            panelTop.Dock = DockStyle.Fill;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(3, 4, 3, 4);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1008, 537);
            panelTop.TabIndex = 0;
            // 
            // btnConfirm
            // 
            btnConfirm.Enabled = false;
            btnConfirm.Location = new Point(307, 16);
            btnConfirm.Margin = new Padding(3, 4, 3, 4);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(141, 40);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = "Confirm New Task";
            btnConfirm.Click += btnConfirm_Click;
            // 
            // dgvTasks
            // 
            dgvTasks.ColumnHeadersHeight = 29;
            dgvTasks.Dock = DockStyle.Bottom;
            dgvTasks.Location = new Point(0, 70);
            dgvTasks.Margin = new Padding(3, 4, 3, 4);
            dgvTasks.Name = "dgvTasks";
            dgvTasks.RowHeadersWidth = 51;
            dgvTasks.Size = new Size(1008, 467);
            dgvTasks.TabIndex = 0;
            dgvTasks.CellValueChanged += dgvTasks_CellValueChanged;
            dgvTasks.SelectionChanged += dgvTasks_SelectionChanged;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.MultiSelect = false;

            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(14, 16);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(91, 40);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add New";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(112, 16);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(91, 40);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(210, 16);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(91, 40);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // panelDetail
            // 
            panelDetail.Controls.Add(lblId);
            panelDetail.Controls.Add(txtTitle);
            panelDetail.Controls.Add(txtDescription);
            panelDetail.Controls.Add(chkCompleted);
            panelDetail.Dock = DockStyle.Fill;
            panelDetail.Location = new Point(0, 0);
            panelDetail.Margin = new Padding(3, 4, 3, 4);
            panelDetail.Name = "panelDetail";
            panelDetail.Size = new Size(1008, 219);
            panelDetail.TabIndex = 0;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(14, 12);
            lblId.Name = "lblId";
            lblId.Size = new Size(39, 20);
            lblId.TabIndex = 3;
            lblId.Text = "lblId";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(14, 39);
            txtTitle.Margin = new Padding(3, 4, 3, 4);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(878, 27);
            txtTitle.TabIndex = 0;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(14, 77);
            txtDescription.Margin = new Padding(3, 4, 3, 4);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(982, 116);
            txtDescription.TabIndex = 1;
            // 
            // chkCompleted
            // 
            chkCompleted.Location = new Point(899, 39);
            chkCompleted.Margin = new Padding(3, 4, 3, 4);
            chkCompleted.Name = "chkCompleted";
            chkCompleted.Size = new Size(97, 32);
            chkCompleted.TabIndex = 2;
            chkCompleted.Text = "Completed";
            // 
            // View
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 761);
            Controls.Add(split);
            Margin = new Padding(3, 4, 3, 4);
            Name = "View";
            Text = "SimBiose Tasks";
            split.Panel1.ResumeLayout(false);
            split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)split).EndInit();
            split.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            panelDetail.ResumeLayout(false);
            panelDetail.PerformLayout();
            ResumeLayout(false);
        }
        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code



        #endregion

        private Button btnConfirm;
    }
}
