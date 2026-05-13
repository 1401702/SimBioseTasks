using System;
using System.Windows.Forms;

namespace SimBioseTasks
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal da aplicação.
        /// Inicializa a configuração base do WinForms e arranca o controller principal.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Controller controller = new Controller();
            controller.Start();
        }
    }
}