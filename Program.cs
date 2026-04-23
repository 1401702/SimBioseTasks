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
            // passa-se o run para o controller


            // Não sei se existe uma forma melhor de passar o controlo para o controller do que o while
            //ApplicationConfiguration.Initialize();
            //Application.Run(new View());
            //ThreadContext.FromCurrent().RunMessageLoop(msoloop.Main, new ApplicationContext(mainForm));
            //controller.ProgInit();  // coloca-se o loop while no construtor do controller


        }
    }
}