using System;
using System.Threading;
using System.Windows.Forms;
using SkylineProblemWinforms.Controllers;
using SkylineProblemWinforms.Interfaces;
using SkylineProblemWinforms.Utilities;

namespace SkylineProblemWinforms
{
    static class Program
    {
        private static IFormController controller;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //controller = MainFormController.GetInstance();//new MainFormController();
                controller = new MainFormController();
                var form = new MainForm(controller);

                Application.Run(form);
            }
            catch (Exception e)
            {
                LogHelper.Log(e.Message);
                MessageBox.Show(e.Message, "An exception occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    //public class ThreadExceptionHandler
    //{
    //    public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
    //    {
    //        LogHelper.Log(e.Exception.Message);
    //        MessageBox.Show(e.Exception.Message, "An exception occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    }
    //}
}


