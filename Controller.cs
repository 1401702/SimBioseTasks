using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SimBioseTasks
{
    /// <summary>
    /// Representa o controlador da aplicação no padrão MVC.
    /// É responsável por coordenar o fluxo entre a View e o Model,
    /// utilizando delegates e eventos para reduzir o acoplamento direto
    /// entre as camadas.
    /// </summary>
    public class Controller
    {
        private readonly Model _model;
        private readonly View _view;

        public event Action<string>? OnError;


        /// <summary>
        /// Evento emitido pelo Controller para encaminhar pedidos da View para o Model.
        /// Desta forma, o Controller não invoca diretamente métodos do Model,
        /// limitando-se a publicar uma operação que o Model poderá tratar.
        /// </summary>
        public event Action<OperTask>? OnControllerToModel;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Controller"/>.
        /// Cria o Model e a View, e estabelece as ligações por eventos entre as camadas.
        /// </summary>
        public Controller()
        {
            _model = new Model();   // Inicializa o Model
            _view = new View();     // Inicializa a View

            _view.OnViewEvent += EventOnView;                   // Subscreve eventos da View
            //_model.OnModelEvent += EventOnModel;                // Subscreve eventos do Model
            _model.OnTasksChanged += tasks => _view.LoadTasks(tasks);
            OnControllerToModel += _model.EventOnController;    // Encaminha eventos do Controller para o Model
            OnError += msg => _view.ShowError(msg);             // Tratamento de erros com encaminhamento para a view
            _model.PublishInitialState();                       // Carrega a lista de tarefas do ficheiro
        }

        /// <summary>
        /// Inicia a aplicação.
        /// Atualiza o estado inicial da View e arranca o ciclo principal do WinForms.
        /// </summary>
        public void Start()
        {
            //RefreshView();
            Application.Run(_view);
        }

        /// <summary>
        /// Trata os eventos emitidos pela View.
        /// Recebe a operação pedida pelo utilizador e reencaminha-a para o Model
        /// através do evento do Controller.
        /// </summary>
        /// <param name="args">
        /// Objeto que contém a operação pedida e a tarefa associada.
        /// </param>
        private void EventOnView(OperTask args)
        {
            if (args == null || args.Task == null)
                return;

            try
            {
                OnControllerToModel?.Invoke(args);
            }
            catch (UnauthorizedAccessException ex)
            {
                OnError?.Invoke(ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                OnError?.Invoke(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                OnError?.Invoke(ex.Message);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }
    }
}