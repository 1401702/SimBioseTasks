using System.Drawing;
using System.Windows.Forms;

namespace SimBioseTasks
{
    partial class View
    {
        /// <summary>
        /// Variável necessária para o suporte ao Designer.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Panel panelDetail;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private CheckBox chkCompleted;
        private Button btNew;
        private Button btnUpdate;
        private Button btnDelete;
        private SplitContainer split;
        private Label lblId;

        /// <summary>
        /// Liberta os recursos que estiverem a ser utilizados.
        /// </summary>
        /// <param name="disposing">
        /// true se os recursos geridos devem ser eliminados; caso contrário, false.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Método necessário para suporte ao Designer.
        /// </summary>
        private void InitializeComponent()
        {
            split = new SplitContainer();
            panelTop = new Panel();
            dgvTasks = new DataGridView();
            panelDetail = new Panel();
            panel3 = new Panel();
            btnDelete = new Button();
            btnUpdate = new Button();
            btNew = new Button();
            panel2 = new Panel();
            txtDescription = new TextBox();
            panelup = new Panel();
            txtTitle = new TextBox();
            chkCompleted = new CheckBox();
            lblId = new Label();
            ((System.ComponentModel.ISupportInitialize)split).BeginInit();
            split.Panel1.SuspendLayout();
            split.Panel2.SuspendLayout();
            split.SuspendLayout();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            panelDetail.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panelup.SuspendLayout();
            SuspendLayout();
            // 
            // split
            // 
            split.Dock = DockStyle.Fill;
            split.Location = new Point(0, 0);
            split.Margin = new Padding(3, 2, 3, 2);
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
            split.Size = new Size(1373, 974);
            split.SplitterDistance = 687;
            split.SplitterWidth = 3;
            split.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.AutoSize = true;
            panelTop.Controls.Add(dgvTasks);
            panelTop.Dock = DockStyle.Fill;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(3, 2, 3, 2);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1373, 687);
            panelTop.TabIndex = 0;
            // 
            // dgvTasks
            // 
            dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTasks.Dock = DockStyle.Fill;
            dgvTasks.Location = new Point(0, 0);
            dgvTasks.Margin = new Padding(3, 2, 3, 2);
            dgvTasks.MultiSelect = false;
            dgvTasks.Name = "dgvTasks";
            dgvTasks.RowHeadersWidth = 51;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.Size = new Size(1373, 687);
            dgvTasks.TabIndex = 0;
            dgvTasks.CellValueChanged += dgvTasks_CellValueChanged;
            dgvTasks.SelectionChanged += dgvTasks_SelectionChanged;
            // 
            // panelDetail
            // 
            panelDetail.Controls.Add(panel3);
            panelDetail.Controls.Add(panel2);
            panelDetail.Controls.Add(panelup);
            panelDetail.Controls.Add(lblId);
            panelDetail.Dock = DockStyle.Fill;
            panelDetail.Location = new Point(0, 0);
            panelDetail.Margin = new Padding(3, 2, 3, 2);
            panelDetail.Name = "panelDetail";
            panelDetail.Size = new Size(1373, 284);
            panelDetail.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnDelete);
            panel3.Controls.Add(btnUpdate);
            panel3.Controls.Add(btNew);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 246);
            panel3.Name = "panel3";
            panel3.Size = new Size(1373, 38);
            panel3.TabIndex = 7;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(175, 2);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(89, 2);
            btnUpdate.Margin = new Padding(3, 2, 3, 2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 30);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btNew
            // 
            btNew.Location = new Point(3, 2);
            btNew.Margin = new Padding(3, 2, 3, 2);
            btNew.Name = "btNew";
            btNew.Size = new Size(80, 30);
            btNew.TabIndex = 1;
            btNew.Text = "New";
            btNew.UseVisualStyleBackColor = true;
            btNew.Click += btnNew_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(txtDescription);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 24);
            panel2.Name = "panel2";
            panel2.Size = new Size(1373, 260);
            panel2.TabIndex = 6;
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Location = new Point(0, 0);
            txtDescription.Margin = new Padding(3, 2, 3, 2);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(1373, 260);
            txtDescription.TabIndex = 1;
            // 
            // panelup
            // 
            panelup.Controls.Add(txtTitle);
            panelup.Controls.Add(chkCompleted);
            panelup.Dock = DockStyle.Top;
            panelup.Location = new Point(0, 0);
            panelup.Name = "panelup";
            panelup.Size = new Size(1373, 24);
            panelup.TabIndex = 5;
            // 
            // txtTitle
            // 
            txtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTitle.Location = new Point(0, 0);
            txtTitle.Margin = new Padding(3, 200, 100, 2);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(1273, 23);
            txtTitle.TabIndex = 0;
            // 
            // chkCompleted
            // 
            chkCompleted.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkCompleted.Location = new Point(1288, 0);
            chkCompleted.Margin = new Padding(3, 2, 3, 2);
            chkCompleted.Name = "chkCompleted";
            chkCompleted.Size = new Size(85, 24);
            chkCompleted.TabIndex = 2;
            chkCompleted.Text = "Completed";
            chkCompleted.UseVisualStyleBackColor = true;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(12, 9);
            lblId.Name = "lblId";
            lblId.Size = new Size(0, 15);
            lblId.TabIndex = 3;
            // 
            // View
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1373, 974);
            Controls.Add(split);
            Margin = new Padding(3, 2, 3, 2);
            Name = "View";
            Text = "SimBiose Tasks";
            split.Panel1.ResumeLayout(false);
            split.Panel1.PerformLayout();
            split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)split).EndInit();
            split.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            panelDetail.ResumeLayout(false);
            panelDetail.PerformLayout();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panelup.ResumeLayout(false);
            panelup.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private DataGridView dgvTasks;
        private Panel panelup;
        private Panel panel3;
        private Panel panel2;
    }
}