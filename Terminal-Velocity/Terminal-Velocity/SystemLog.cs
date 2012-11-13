using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Interfaces;
using Utility;

namespace TerminalVelocity
{
    class SystemLog
    {
        #region Private Variables
        private string _currentLogFile;
        private StreamWriter log;
        #endregion

        #region Constructor
        public SystemLog()
        {
            _currentLogFile = "Terminal_Velocity.log";
        }
        #endregion

        #region Public Functions
        public void writeLog(string msg)
        {
            appendSystemLog(DateTime.Now + " --> " + msg);
        }

        public void appendSystemLog(string msg)
        {
            
            if (!File.Exists(_currentLogFile))
            {
                log = new StreamWriter(_currentLogFile);
            }
            else
            {
                log = File.AppendText(_currentLogFile);
            }
            log.WriteLine(msg);
            log.Close();
        }

        public void changeLogFile(string filename)
        {

        }

        #endregion

    }
}
