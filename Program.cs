namespace SimBioseTasks
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new View());

            Controller controller = new Controller();
            // passo o run para o controller
            controller.ProgInit();
            
        }
    }
}