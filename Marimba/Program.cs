namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    static class Program
    {
        public static Marimba.Forms.MainMenu home;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The arguments that marimba is run with</param>
        [STAThread]
        public static void Main(string[] args)
        {
            // This fixes when one is trying to open a .mrb file and the file name doesn't get passed in
            // Comment out the line if debugging as it throws an Argument Null Exception
            if (args == null || args.Length == 0)
            {
                //args = AppDomain.CurrentDomain.SetupInformation.ActivationArguments‌​.ActivationData.Select(s => new Uri(s).LocalPath).ToArray();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            home = new Marimba.Forms.MainMenu(args);
            Application.Run(home);
        }
    }
}
