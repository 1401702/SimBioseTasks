using System;
using System.Linq;
using System.Windows.Forms;

namespace SimBioseTasks
{
    /// <summary>
    /// Representa o controlador da aplicação.
    /// É responsável por ligar a View ao Model, tratar os eventos da interface
    /// e coordenar a atualização dos dados apresentados.
    /// </summary>
    public class Controller
    {
        private readonly ITaskModel _model;
        private readonly View _view;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Controller"/>.
        /// Cria o modelo e a vista, e estabelece as subscrições aos eventos
        /// necessários para a comunicação entre ambos.
        /// </summary>
        public Controller()
        {
            _model = new Model();
            _view = new View();

            _view.OnViewEvent += EventOnView;
            _model.TasksChanged += Model_TasksChanged;
        }

        /// <summary>
        /// Inicia a aplicação carregando as tarefas atuais na vista
        /// e mostrando o formulário principal.
        /// </summary>
        public void Start()
        {
            _view.LoadTasks(_model.Tasks.ToList());
            _view.Show();
        }

        /// <summary>
        /// Obtém o formulário principal da aplicação.
        /// </summary>
        public Form MainForm => _view;

        /// <summary>
        /// Trata os eventos enviados pela vista.
        /// Recebe a operação solicitada pelo utilizador e encaminha-a para o modelo.
        /// </summary>
        /// <param name="args">
        /// Objeto que contém a operação a executar e a tarefa associada.
        /// </param>
        private void EventOnView(OperTask args)
        {
            if (args == null || args.Task == null)
                return;

            try
            {
                _model.Execute(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// Trata o evento de alteração de tarefas emitido pelo modelo.
        /// Atualiza a lista de tarefas apresentada na vista.
        /// </summary>
        /// <param name="sender">Objeto que originou o evento.</param>
        /// <param name="e">Dados associados ao evento de alteração.</param>
        private void Model_TasksChanged(object? sender, OperTaskEventArgs e)
        {
            _view.LoadTasks(_model.Tasks.ToList());
        }
    }
}