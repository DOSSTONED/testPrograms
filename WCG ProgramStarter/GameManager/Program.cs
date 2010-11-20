using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace GameManager
{
    static class Program
    {
        private static GameManager mainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ensure only a single instance of this app runs.
            
            SingleInstanceApplication app = new SingleInstanceApplication();

            app.StartupNextInstance += new StartupNextInstanceEventHandler(OnAppStartupNextInstance);



            mainForm = new GameManager();

            app.Run(mainForm);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
        }

        /// <summary>

        /// Event handler for processing when the another application instance tries

        /// to startup. Bring the previous instance of the app to the front and

        /// process any command-line that's needed.

        /// </summary>

        /// <param name="sender">Object sending this message.</param>

        /// <param name="e">Event argument for this message.</param>

        static void OnAppStartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {

            // if the window is currently minimized, then restore it.

            if (mainForm.WindowState == FormWindowState.Minimized)
            {

                mainForm.WindowState = FormWindowState.Normal;

            }

            // activate the current instance of the app, so that it's shown.

            
            mainForm.Show();
            mainForm.Activate();


            // todo: implement command-line processing as needed...

        }


    }
}
