using System;
using System.Linq;
using System.Windows.Forms;

namespace SimBioseTasks
{
    /// <summary>
    /// Representa o controlador da aplicação.
    /// É responsável por ligar a View ao Model, tratar os eventos da interface
    /// e coordenar a atualização dos dados apresentados.
    /// O arranque da aplicação é iniciado a partir do próprio controlador.
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
        /// Inicia o fluxo principal da aplicação.
        /// Carrega o estado inicial na vista e inicia o ciclo de execução WinForms.
        /// </summary>
        public void Start()
        {
            RefreshView();
            Application.Run(_view);
        }

        /// <summary>
        /// Atualiza os dados apresentados na vista com o estado atual do modelo.
        /// </summary>
        private void RefreshView()
        {
            _view.LoadTasks(_model.Tasks.ToList());
        }

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
            RefreshView();
        }
    }
}