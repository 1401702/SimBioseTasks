using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SimBioseTasks
{
    /// <summary>
    /// representa a View do MVC
    /// </summary>
    public partial class View : Form
    {

        private bool _isRefreshingGrid = false;

        /// <summary>
        /// Ocorre quando o utilizador executa uma ação na interface,
        /// como criar, atualizar ou eliminar uma tarefa.
        /// </summary>
        public event Action<OperTask>? OnViewEvent;

        private readonly BindingSource _tasksSource = new BindingSource();

        //private BaseTask? tmpTask = null;
        private BaseTask? selectedTask = null;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="View"/>.
        /// Configura os componentes da interface e associa a grelha
        /// ao objeto de binding das tarefas.
        /// </summary>
        public View()
        {
            InitializeComponent();
            //LoadTasks();

            dgvTasks.AutoGenerateColumns = true;
            dgvTasks.DataSource = _tasksSource;
        }

        /// <summary>
        /// Carrega a lista de tarefas na grelha e atualiza o painel de detalhe.
        /// </summary>
        /// <param name="tasks">Lista de tarefas a apresentar na interface.</param>
        public void LoadTasks(IReadOnlyList<BaseTask> tasks)
        {
            _isRefreshingGrid = true;
            try
            {
                _tasksSource.DataSource = new BindingList<BaseTask>(tasks.ToList());
                dgvTasks.DataSource = _tasksSource;


                if (dgvTasks.Columns.Count > 0)
                    dgvTasks.Columns[0].Visible = false;


                if (dgvTasks.Rows.Count == 0)
                {
                    ClearDetail();
                }
                else
                {
                    dgvTasks.ClearSelection();
                    selectedTask = dgvTasks.Rows[0].DataBoundItem as BaseTask;
                    if (selectedTask != null)
                        ShowDetail(selectedTask);
                }
            }
            finally
            {
                _isRefreshingGrid = false;
            }
        }

        /// <summary>
        /// Exibe uma mensagem de erro para o utilizador.
        /// </summary>
        /// <param name="message"></param>
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /// <summary>
        /// Limpa os campos do painel de detalhe e remove a seleção atual.
        /// </summary>
        private void ClearDetail()
        {
            //tmpTask = null;
            selectedTask = null;

            lblId.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            chkCompleted.Checked = false;
        }

        /// <summary>
        /// Trata o evento de mudança de seleção na grelha de tarefas.
        /// Atualiza o painel de detalhe com a tarefa atualmente selecionada.
        /// </summary>
        /// <param name="sender">Objeto que originou o evento.</param>
        /// <param name="e">Dados associados ao evento.</param>
        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                ClearDetail();
                return;
            }

            var row = dgvTasks.SelectedRows[0];

            if (row.DataBoundItem is BaseTask task)
            {
                selectedTask = task;
                ShowDetail(task);
            }
            else
            {
                ClearDetail();
            }
            btnConfirm.Enabled = false;
            btnAdd.Enabled = true;

        }

        /// <summary>
        /// Mostra no painel de detalhe os dados de uma tarefa.
        /// </summary>
        /// <param name="task">Tarefa a apresentar.</param>
        private void ShowDetail(BaseTask task)
        {
            lblId.Text = task.Id?.ToString() ?? "";
            txtTitle.Text = task.Title ?? string.Empty;
            txtDescription.Text = task.Description ?? string.Empty;
            chkCompleted.Checked = task.IsCompleted;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        /// <summary>
        /// Trata o clique no botão de adicionar nova tarefa.
        /// Limpa os campos do detalhe e prepara a interface para inserção.
        /// </summary>
        /// <param name="sender">Objeto que originou o evento.</param>
        /// <param name="e">Dados associados ao evento.</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //tmpTask = null;
            ClearDetail();
            btnConfirm.Enabled = true;
            txtTitle.Select();
            btnUpdate.Enabled = false;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// Trata o clique no botão de confirmação de criação de tarefa.
        /// Cria um comando de criação e envia-o para o controller.
        /// </summary>
        /// <param name="sender">Objeto que originou o evento.</param>
        /// <param name="e">Dados associados ao evento.</param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var op = new OperTask
            {
                Operation = TaskOp.Create,
                Task = new BaseTask
                {
                    Id = null,
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    IsCompleted = chkCompleted.Checked
                }
            };

            OnViewEvent?.Invoke(op);
            btnConfirm.Enabled = false;
        }

        /// <summary>
        /// Trata o clique no botão de atualização de tarefa.
        /// Cria um comando de atualização com os dados atuais do formulário
        /// e envia-o para o controller.
        /// </summary>
        /// <param name="sender">Objeto que originou o evento.</param>
        /// <param name="e">Dados associados ao evento.</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int? id = null;
            _ = int.TryParse(lblId.Text, out var parsedId);
            if (!string.IsNullOrWhiteSpace(lblId.Text)) id = parsedId;

            var op = new OperTask
            {
                Operation = TaskOp.Update,
                Task = new BaseTask
                {
                    Id = id,
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    IsCompleted = chkCompleted.Checked
                }
            };

            OnViewEvent?.Invoke(op);

        }

        /// <summary>
        /// Trata o clique no botão de eliminação de tarefa.
        /// Envia para o controller um comando para remover a tarefa selecionada.
        /// </summary>
        /// <param name="sender">Objeto que originou o evento.</param>
        /// <param name="e">Dados associados ao evento.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
                return;

            var task = dgvTasks.SelectedRows[0].DataBoundItem as BaseTask;
            if (task == null)
                return;

            var op = new OperTask
            {
                Operation = TaskOp.Delete,
                Task = task
            };

            OnViewEvent?.Invoke(op);
        }

        /// <summary>
        /// Trata a alteração do valor de uma célula na grelha.
        /// Quando uma tarefa é editada diretamente na grelha,
        /// envia um comando de atualização para o controller.
        /// </summary>
        /// <param name="sender">Objeto que originou o evento.</param>
        /// <param name="e">Dados associados ao evento da célula alterada.</param>
        private void dgvTasks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (_isRefreshingGrid) return;

            if (e.RowIndex < 0) return;

            var row = dgvTasks.Rows[e.RowIndex];
            var task = row.DataBoundItem as BaseTask;

            if (task == null) return;

            var op = new OperTask
            {
                Operation = TaskOp.Update,
                Task = task
            };

            OnViewEvent?.Invoke(op);
        }

    }
}