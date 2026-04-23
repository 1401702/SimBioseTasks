using System;
using System.Windows.Forms;

namespace SimBioseTasks
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Controller controller = new Controller();
            controller.Start();
        }
    }
}