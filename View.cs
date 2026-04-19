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
        public void ActivateInterface()
        {
            // Temos que conectar o objetos do Model e do Controller
            // Desenhar janelas e botões ocorre no código automático da API WinForms
            // A animação do clique do botão é gerada pelo código da API WinForms

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
            op.Operation = "update";
            op.Task = new BaseTask
            {
                Id = string.IsNullOrEmpty(lblId.Text) ? (int?)null : int.Parse(lblId.Text),
                Title = txtTitle.Text,
                Description = txtDescription.Text
            };

            // Enviamos para o Controller
            OnViewEvent?.Invoke(op);
        }
        //private void btnUpdate_Click(object sender, EventArgs e)
        //{

        //    if (tmpTask == null) tmpTask = new BaseTask();

        //    // criar tmpTask para passar para o TaskController
        //    if (lblId.Text != "")
        //    {
        //        tmpTask.Id = null;
        //    }
        //    else
        //    {
        //        tmpTask.Id = int.Parse(lblId.Text);
        //    }
        //    tmpTask.Title = txtTitle.Text.Trim();
        //    tmpTask.Description = txtDescription.Text.Trim();
        //    tmpTask.IsCompleted = chkCompleted.Checked;

        //    Controller.TaskView_btnUpdate_Click(tmpTask);

        //    // ??este codigo tem de ser passado para o controller???

        //    LoadTasks();
        //}
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

    }
}
