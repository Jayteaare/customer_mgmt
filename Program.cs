using System.Globalization;

namespace C969_WGU_TallisJordan
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
            ApplicationConfiguration.Initialize();
            /*
            CultureInfo japaneseCulture = new CultureInfo("ja-JP");
            Thread.CurrentThread.CurrentCulture = japaneseCulture;
            Thread.CurrentThread.CurrentUICulture = japaneseCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            */
            Application.Run(new Login());
        }
    }
}