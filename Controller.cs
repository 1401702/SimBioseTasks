using System.Collections.Generic;

namespace SimBioseTasks
{
    public class Controller
    {
        // O controller
        // controla os inputs do user
        // controla o fluxo

        public static Model _model;
        public static View _view;

        public Controller()
        {
            _model = new Model();
            _view = new View();


            // conceito event delegate 
            _view.OnViewEvent += EventOnView;

            _view.Show();

            // não deixa fechar o programa se não existir o while o programa abre e fecha
            // A tecnica é enquanto houver forms abertos o loop continua

            while (Application.OpenForms.Count > 0)
            {
                Application.DoEvents();

                // aqui temos de controlar o tempo para poupar cpu, mas o codigo tem de ser responsivo
                // este loop tem de ser bem pensado, pois ele vai carregar o sistema
                System.Threading.Thread.Sleep(200);
            }
        }
        private void EventOnView(OperTask args)
        {
            // Aqui o Controller decide o fluxo baseado na string
            // As operations pode ser um enum
            if (args.Operation == "Update")
            {
                // Validação simples antes de passar ao Model
                if (!string.IsNullOrEmpty(args.Task.Title))
                {
                    _model.UpdateTask(args.Task);
                }
            }
            else if (args.Operation == "Delete")
            {
                _model.DeleteTask(args.Task.Id.Value);
            }
            else if (args.Operation == "Create")
            {
                _model.CreateTask(args.Task);
            }

            // Após o Model processar, o Controller manda a View atualizar a lista
            _view.LoadTasks();
        }
        public static void OnCreateTask(BaseTask task)
        {
            _model.CreateTask(task);
            RefreshView();
        }

        public static void OnEditTask(BaseTask task)
        {
            // o controller é quem abre o form e decide o que fazer

        }
        public static void TaskView_btnUpdate_Click(BaseTask detailTask)
        {
            // apos o user carregar no btnUpdate
            // a detail task tras a info que estava no detail panel
            // todas as regras de validação de dados devem ser colocadas aqui
            // mas regras especificas de negocio é no model

            if (detailTask != null)
            {
                if (detailTask.Id != null)
                {
                    _model.UpdateTask(detailTask);
                }
                else
                {
                    _model.CreateTask(detailTask);
                }
            }
            else
            {
                // tem de existir Controlo de erros
                return;
            }
        }
        public static void TaskView_btnDelete_Click(int id)
        {
            _model.DeleteTask(id);
            RefreshView();
        }
        private static void RefreshView()
        {

        }
    }
}
