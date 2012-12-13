using System;
using System.IO;
using System.Collections;

using Interfaces;

namespace SimulationEnvironment
{
    internal class SystemLog
    {
        #region Private Variables

        /// <summary>
        ///     Holds the path to the log file
        /// </summary>
        private readonly string _currentLogFile;

        /// <summary>
        ///     Object to write to file
        /// </summary>
        private StreamWriter log;

        private Queue _messages;

        private ISimulationEnvironment _env;

        #endregion

        #region Constructor

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public SystemLog(ISimulationEnvironment e)
        {
            _currentLogFile = "Terminal_Velocity.log";
            _env = e;
            _env.Tick += new EventHandler<Utility.TickEventArgs>(_env_Tick);
            _messages = new Queue();
        }

        void _env_Tick(object sender, Utility.TickEventArgs e)
        {
            if (_messages.Count > 0)
            {
                if (_messages.Peek() != null)
                {
                    appendSystemLog(_messages.Dequeue().ToString());
                }
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        ///     Formats the message to be written to the log
        /// </summary>
        /// <param name="msg">original message to be written</param>
        public void writeLog(string msg)
        {
            _messages.Enqueue(DateTime.Now + " --> " + msg);
            //appendSystemLog(DateTime.Now + " --> " + msg);
        }

        /// <summary>
        ///     Writes to log file
        /// </summary>
        /// <param name="msg">exact message to write to the log file</param>
        private void appendSystemLog(string msg)
        {
            return;
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
        ///     changes the log file path (NOT YET IMPLEMENTED)
        /// </summary>
        /// <param name="filename">string path to new file</param>
        public void changeLogFile(string filename)
        {
            //not implemented
        }

        #endregion
    }
}