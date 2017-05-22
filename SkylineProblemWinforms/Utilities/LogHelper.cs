using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SkylineProblemWinforms.Interfaces;
using SkylineProblemWinforms.UI;

namespace SkylineProblemWinforms.Utilities
{
    public static class LogHelper
    {
        public static List<ILogTarget> _extraLoggingTargets = new List<ILogTarget>();

        private static readonly ILog _Logger;
        private static InfoPanel _InfoPanel = null;

        private static ILog GetLogger(string logName)
        {
            ILog log = LogManager.GetLogger(logName);
            return log;
        }

        private static ILog GetLogger(string logName, InfoPanel _infoPanel)
        {
            _InfoPanel = _infoPanel;
            ILog log = LogManager.GetLogger(logName);
            return log;
        }

        static LogHelper()
        {
            _Logger = GetLogger("FileLogger");
        }

        public static void Log(string msg)
        {
            _Logger.DebugFormat(msg);
            LogToExtraTargets(msg);
        }

        public static void LogMessage(string msg)
        {
            _Logger.InfoFormat(msg);
            LogToExtraTargets(msg);
        }

        private static void LogToExtraTargets(string msg)
        {
            foreach (var logTarget in _extraLoggingTargets)
            {
                ((ILogTarget)logTarget).LogToControl(msg);
            }
        }

        public static void AddLogTarget(ILogTarget _target)
        {
            _extraLoggingTargets.Add(_target);
        }
    }
}
