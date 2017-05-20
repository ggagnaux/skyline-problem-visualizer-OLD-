using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using SkylineProblemWinforms.Interfaces;
using SkylineProblemWinforms.Utilities;

namespace SkylineProblemWinforms.UI
{
    public partial class FormBase : MetroForm, ILoggable
    {
        public IFormController Controller { get; set; }
        public FormBase(IFormController _controller)
        {
            if (_controller == null)
            {
                throw new ArgumentNullException("The FormBase() constructor requires an IFormController object.");
            } 

            this.Controller = _controller;
        }

        // For Designer Only
        private FormBase() {}

        //
        // ILoggable Implementation
        //
        public void AddLogTarget(ILogTarget target) { LogHelper.AddLogTarget(target); }
        public void Log(string msg) { LogHelper.Log(msg); }
        public void LogError(string msg) { LogHelper.Log(msg); }
        public void LogException(string msg, Exception ex) { LogHelper.Log(msg); }
        public void LogMessage(string msg) { LogHelper.Log(msg); }
    }
}
