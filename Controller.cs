using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimBioseTasks
{
    /// <summary>
    /// Representa o Controller principal da aplicação.
    /// É responsável por criar o Model e a View, subscrever os eventos da View,
    /// encaminhar operações para o Model e atualizar a View quando o estado muda.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Referência para o model da aplicação.
        /// </summary>
        private readonly ITaskBase _model;

        /// <summary>
        /// Referência para a view principal da aplicação.
        /// </summary>
        private readonly ITaskView _view;

        /// <summary>
        /// Ocorre quando o controller pretende comunicar um erro à view.
        /// </summary>
        public event Action<string>? OnError;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Controller"/>.
        /// Cria o model e a view e estabelece as ligações entre os respetivos eventos.
        /// </summary>
        public Controller()
        {
            _model = new Model();
            _view = new View();

            _view.OnSaveRequest += SaveTask;
            _view.OnDeleteRequest += DeleteTask;

            _model.OnTasksChanged += LoadTasksIntoView;

            OnError += _view.ShowError;
        }

        /// <summary>
        /// Inicia a aplicação.
        /// Inicializa o model com os dados persistidos e arranca o ciclo principal do WinForms.
        /// </summary>
        public void Start()
        {
            _model.Initialize();
            Application.Run(_view.GetForm());
        }

        /// <summary>
        /// Trata o pedido de criação de uma nova tarefa recebido da view.
        /// </summary>
        /// <param name="task">Tarefa a criar.</param>
        private void SaveTask(BaseTask task)
        {
            try
            {
                _model.Save(task);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        /// <summary>
        /// Trata o pedido de remoção de uma tarefa recebido da view.
        /// </summary>
        /// <param name="id">Identificador da tarefa a remover.</param>
        private void DeleteTask(int id)
        {
            try
            {
                _model.Delete(id);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza a view com a lista atual de tarefas recebida do model.
        /// </summary>
        /// <param name="tasks">Coleção atual de tarefas.</param>
        private void LoadTasksIntoView(IReadOnlyList<BaseTask> tasks)
        {
            _view.LoadTasks(tasks);
        }
    }
}