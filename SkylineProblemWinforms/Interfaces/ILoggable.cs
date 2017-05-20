using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylineProblemWinforms.Interfaces
{
    public interface ILoggable
    {
        void Log(string msg);
        void LogMessage(string msg);
        void LogError(string msg);
        void LogException(string msg, Exception ex = null);
    }
}
