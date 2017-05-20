using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkylineProblemWinforms.Interfaces;
using SkylineProblemWinforms.Utilities;

namespace SkylineProblemWinforms.Contollers
{
    public class FormController : IFormController
    {
        public FormController()
        {
        }

        //
        // ILoggable Implementation
        //
        public void Log(string msg) { LogHelper.Log(msg); }
        public void LogError(string msg) { LogHelper.Log(msg); }
        public void LogException(string msg, Exception ex) { LogHelper.Log(msg); }
        public void LogMessage(string msg) { LogHelper.Log(msg); }
    }
}
