using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SimBioseTasks
{
    /// <summary>
    /// Representa a View principal da aplicação.
    /// Implementa o contrato <see cref="ITaskView"/> e trata da interface gráfica
    /// e das ações do utilizador.
    /// </summary>
    public partial class View : Form, ITaskView
    {
        /// <summary>
        /// Indica se a grelha está a ser atualizada programaticamente.
        /// </summary>
        private bool _isRefreshingGrid = false;

        /// <summary>
        /// Tarefa atualmente selecionada.
        /// </summary>
        private BaseTask? _selectedTask = null;

        /// <summary>
        /// Ocorre quando o utilizador pede a save de uma nova tarefa.
        /// </summary>
        public event Action<BaseTask>? OnSaveRequest;

        /// <summary>
        /// Ocorre quando o utilizador pede a remoção de uma tarefa existente.
        /// </summary>
        public event Action<int>? OnDeleteRequest;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="View"/>.
        /// </summary>
        public View()
        {
            InitializeComponent();

            dgvTasks.AutoGenerateColumns = true;
        }

        /// <summary>
        /// Devolve a instância concreta do formulário.
        /// </summary>
        /// <returns>A instância atual do formulário.</returns>
        public Form GetForm()
        {
            return this;
        }

        /// <summary>
        /// Carrega na interface a coleção de tarefas recebida do controller.
        /// </summary>
        /// <param name="tasks">Coleção de tarefas a apresentar.</param>
        public void LoadTasks(IReadOnlyList<BaseTask> tasks)
        {
            _isRefreshingGrid = true;

            try
            {
                dgvTasks.DataSource = tasks.ToList();

                if (dgvTasks.Columns.Count > 0)
                    dgvTasks.Columns[0].Visible = false;

                if (dgvTasks.Rows.Count == 0)
                {
                    ClearDetail();
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                    return;
                }

                dgvTasks.ClearSelection();

                if (dgvTasks.Rows.Count > 0)
                {
                    dgvTasks.Rows[0].Selected = true;
                    _selectedTask = dgvTasks.Rows[0].DataBoundItem as BaseTask;

                    if (_selectedTask != null)
                        ShowDetail(_selectedTask);
                    // No auto fill da datagrid coloca  a coluna IsCompleted com 15
                    if (dgvTasks.Columns["IsCompleted"] != null)
                        dgvTasks.Columns["IsCompleted"].FillWeight = 15;
                    if (dgvTasks.Columns["Title"] != null)
                        dgvTasks.Columns["Title"].FillWeight = 30;
                }
            }
            finally
            {
                _isRefreshingGrid = false;
            }
        }

        /// <summary>
        /// Mostra uma mensagem de erro ao utilizador.
        /// </summary>
        /// <param name="message">Mensagem de erro a apresentar.</param>
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Limpa os campos de detalhe da tarefa.
        /// </summary>
        private void ClearDetail()
        {
            _selectedTask = null;

            lblId.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            chkCompleted.Checked = false;
        }

        /// <summary>
        /// Mostra os detalhes de uma tarefa selecionada.
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
        /// Constrói uma tarefa a partir dos valores atuais do formulário.
        /// </summary>
        /// <returns>Instância de <see cref="BaseTask"/> preenchida com os dados da interface.</returns>
        private BaseTask BuildTaskFromForm()
        {
            int? id = null;

            if (int.TryParse(lblId.Text, out int parsedId))
                id = parsedId;

            return new BaseTask
            {
                Id = id,
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                IsCompleted = chkCompleted.Checked
            };
        }

        /// <summary>
        /// Trata a mudança de seleção na grelha de tarefas.
        /// </summary>
        /// <param name="sender">Origem do evento.</param>
        /// <param name="e">Dados do evento.</param>
        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                ClearDetail();
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            DataGridViewRow row = dgvTasks.SelectedRows[0];

            if (row.DataBoundItem is BaseTask task)
            {
                _selectedTask = task;
                ShowDetail(task);
            }
            else
            {
                ClearDetail();
            }
        }

        /// <summary>
        /// Trata o clique no botão para preparar a criação de uma nova tarefa.
        /// </summary>
        /// <param name="sender">Origem do evento.</param>
        /// <param name="e">Dados do evento.</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearDetail();

            txtTitle.Focus();
        }

        /// <summary>
        /// Trata o clique no botão de confirmação de criação.
        /// </summary>
        /// <param name="sender">Origem do evento.</param>
        /// <param name="e">Dados do evento.</param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            BaseTask task = BuildTaskFromForm();
            OnSaveRequest?.Invoke(task);
        }

        /// <summary>
        /// Trata o clique no botão de atualização.
        /// </summary>
        /// <param name="sender">Origem do evento.</param>
        /// <param name="e">Dados do evento.</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            BaseTask task = BuildTaskFromForm();
            OnSaveRequest?.Invoke(task);
        }

        /// <summary>
        /// Trata o clique no botão de remoção.
        /// </summary>
        /// <param name="sender">Origem do evento.</param>
        /// <param name="e">Dados do evento.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedTask?.Id == null)
                return;

            OnDeleteRequest?.Invoke(_selectedTask.Id.Value);
        }

        /// <summary>
        /// Trata alterações de células na grelha.
        /// Quando uma tarefa é alterada diretamente na tabela, emite pedido de atualização.
        /// </summary>
        /// <param name="sender">Origem do evento.</param>
        /// <param name="e">Dados do evento.</param>
        private void dgvTasks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isRefreshingGrid)
                return;

            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvTasks.Rows[e.RowIndex];
            BaseTask? task = row.DataBoundItem as BaseTask;

            if (task == null)
                return;

            OnSaveRequest?.Invoke(task);
        }
    }
}