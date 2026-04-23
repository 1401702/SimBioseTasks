namespace SimBioseTasks
{
    public partial class View : Form
    {
        public event Action<OperTask> OnViewEvent;
        public View()
        {
            InitializeComponent();
            LoadTasks();

            if (dgvTasks.Rows.Count > 0)
            {
                dgvTasks.Rows[0].Selected = true;
            }
        }
        public void LoadTasks()
        {
            dgvTasks.DataSource = null;
            dgvTasks.DataSource = Controller._model.GetTasks().ToList();

            // se não houver nada, detalhe limpo
            if (dgvTasks.Rows.Count == 0)
            {
                ClearDetail();

            }
            else
            {
                // faz select da primeira linha
                dgvTasks.Rows[0].Selected = true;
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Criamos o comando
            OperTask op = new OperTask();
            op.Operation = "Update";
            op.Task = new BaseTask
            {
                Id = string.IsNullOrEmpty(lblId.Text) ? (int?)null : int.Parse(lblId.Text),
                Title = txtTitle.Text,
                Description = txtDescription.Text
            };

            // Enviamos para o Controller
            OnViewEvent?.Invoke(op);
        }
        private void ClearDetail()
        {
            tmpTask = null;

            lblId.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            chkCompleted.Checked = false;
        }
        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                //ClearDetail();
                return;
            }
            var row = dgvTasks.SelectedRows[0];
            selectedTask = (BaseTask)row.DataBoundItem;

            ShowDetail(selectedTask);
        }
        private void ShowDetail(BaseTask task)
        {
            lblId.Text = task.Id.ToString();
            txtTitle.Text = task.Title ?? string.Empty;
            txtDescription.Text = task.Description ?? string.Empty;
            chkCompleted.Checked = task.IsCompleted;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            BaseTask newTask = new BaseTask();
            // isto não deveria estar no controller?
            // Id será atribuído internamente em AddTask
            ClearDetail();
            txtTitle.Select();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblId.Text != "")
            {
                int tmpId = int.Parse(lblId.Text);
                Controller.TaskView_btnDelete_Click(tmpId);
                LoadTasks();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            // Criamos o comando
            OperTask op = new OperTask();
            op.Operation = "Create";
            op.Task = new BaseTask
            {
                Id = null,
                Title = txtTitle.Text,
                Description = txtDescription.Text
            };

            // Enviamos para o Controller
            OnViewEvent?.Invoke(op);
        }

        private void dgvTasks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvTasks.Rows[e.RowIndex];

            // Criamos o comando
            OperTask op = new OperTask();
            op.Operation = "Update";
            op.Task = (BaseTask)row.DataBoundItem;

            // Enviamos para o Controller
            OnViewEvent?.Invoke(op);

        }
    }
}
