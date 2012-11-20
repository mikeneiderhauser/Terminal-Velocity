using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Interfaces;
using Utility;

namespace SimulationEnvironment
{
    class SystemLog
    {
        #region Private Variables
        /// <summary>
        /// Holds the path to the log file
        /// </summary>
        private string _currentLogFile;

        /// <summary>
        /// Object to write to file
        /// </summary>
        private StreamWriter log;
        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SystemLog()
        {
            _currentLogFile = "Terminal_Velocity.log";
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Formats the message to be written to the log
        /// </summary>
        /// <param name="msg">original message to be written</param>
        public void writeLog(string msg)
        {
            appendSystemLog(DateTime.Now + " --> " + msg);
        }

        /// <summary>
        /// Writes to log file
        /// </summary>
        /// <param name="msg">exact message to write to the log file</param>
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

        /// <summary>
        /// changes the log file path (NOT YET IMPLEMENTED)
        /// </summary>
        /// <param name="filename">string path to new file</param>
        public void changeLogFile(string filename)
        {
            //not implemented
        }

        #endregion

    }
}
