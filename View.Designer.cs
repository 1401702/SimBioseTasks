namespace SimBioseTasks
{
    partial class View
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Model _model;

        private DataGridView dgvTasks;

        private Panel panelDetail;

        private TextBox txtTitle;
        private TextBox txtDescription;
        private CheckBox chkCompleted;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        private BaseTask tmpTask;
        private BaseTask selectedTask;

        private SplitContainer split;
        private Panel panelTop;
        private Label lblId;

        private void InitializeComponent()
        {
            split = new SplitContainer();
            panelTop = new Panel();
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
            split.Size = new Size(884, 716);
            split.SplitterDistance = 507;
            split.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(dgvTasks);
            panelTop.Controls.Add(btnAdd);
            panelTop.Controls.Add(btnUpdate);
            panelTop.Controls.Add(btnDelete);
            panelTop.Dock = DockStyle.Fill;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(884, 507);
            panelTop.TabIndex = 0;
            // 
            // dgvTasks
            // 
            dgvTasks.Dock = DockStyle.Bottom;
            dgvTasks.Location = new Point(0, 157);
            dgvTasks.Name = "dgvTasks";
            dgvTasks.Size = new Size(884, 350);
            dgvTasks.TabIndex = 0;
            dgvTasks.SelectionChanged += dgvTasks_SelectionChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 12);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 30);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(98, 12);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 30);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(184, 12);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
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
            panelDetail.Name = "panelDetail";
            panelDetail.Size = new Size(884, 205);
            panelDetail.TabIndex = 0;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(12, 9);
            lblId.Name = "lblId";
            lblId.Size = new Size(30, 15);
            lblId.TabIndex = 3;
            lblId.Text = "lblId";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(12, 29);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(769, 23);
            txtTitle.TabIndex = 0;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(12, 58);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(860, 88);
            txtDescription.TabIndex = 1;
            // 
            // chkCompleted
            // 
            chkCompleted.Location = new Point(787, 29);
            chkCompleted.Name = "chkCompleted";
            chkCompleted.Size = new Size(85, 24);
            chkCompleted.TabIndex = 2;
            chkCompleted.Text = "Completed";
            // 
            // TaskView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 716);
            Controls.Add(split);
            Name = "TaskView";
            Text = "Task Manager - List + Detail";
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
    }
}
