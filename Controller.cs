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
            _model.LoadTasksFromJson();
            _view = new View();

            // no modo ciclo de run custom
            //_view.ActivateInterface();
            _view.Show();
            // A tecnica é enquanto houver forms abertos o loop continua
            while (Application.OpenForms.Count > 0)
            {
                Application.DoEvents();

                // aqui temos de controlar o tempo para poupar cpu, mas o codigo tem de ser responsivo
                // este loop tem de ser bem pensado, pois ele vai carregar o sistema
                System.Threading.Thread.Sleep(200);
            }
        }
        public void ProgInit()
        {
            //Implementar....

            _view.ActivateInterface();
            // A tecnica é enquanto houver forms abertos o loop continua
            while (Application.OpenForms.Count > 0)
            {
                Application.DoEvents();

                // aqui temos de controlar o tempo para poupar cpu, mas o codigo tem de ser responsivo
                // este loop tem de ser bem pensado, pois ele vai carregar o sistema
                System.Threading.Thread.Sleep(1);
            }
        }
        public static void OnCreateTask(BaseTask task)
        {
            _model.AddTask(task);
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
                    _model.AddTask(detailTask);
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
