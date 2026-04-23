using System;
using System.Linq;
using System.Windows.Forms;

namespace SimBioseTasks
{
    /// <summary>
    /// Representa o controlador da aplicação.
    /// </summary>
    public class Controller
    {
        private readonly Model _model;
        private readonly View _view;

        /// <summary>
        /// Inicializa o controller.
        /// </summary>
        public Controller()
        {
            _model = new Model();
            _view = new View();

            _view.OnViewEvent += EventOnView;
            _model.OnModelEvent += EventOnModel;
        }

        /// <summary>
        /// Inicia a aplicação.
        /// </summary>
        public void Start()
        {
            RefreshView();
            Application.Run(_view);
        }

        /// <summary>
        /// Atualiza a view com os dados atuais do model.
        /// </summary>
        private void RefreshView()
        {
            _view.LoadTasks(_model.Tasks.ToList());
        }

        /// <summary>
        /// Trata eventos enviados pela view.
        /// </summary>
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
        /// Trata eventos enviados pelo model.
        /// </summary>
        private void EventOnModel(OperTask args)
        {
            RefreshView();
        }
    }
}