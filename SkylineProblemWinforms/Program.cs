using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkylineProblemWinforms.UI;

namespace SkylineProblemWinforms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm form = new MainForm();
            Application.Run(form);
        }
    }


    public class ThreadExceptionHandler
    {
        public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "An exception occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}


