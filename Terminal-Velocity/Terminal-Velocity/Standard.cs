using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerminalVelocity
{
    public class Standard
    {
        /// <summary>
        /// Externally accessible member 
        /// </summary>
        public object _param2;

        // A private global parameter
        private object _param1;

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Standard()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param1">Parameter 1</param>
        /// <param name="param2">Parameter 2</param>
        public Standard(object param1, object param2)
        {
            // Assignment of a global parameter
            _param1 = param1;
        }

        #endregion 

        #region Properties

        public object Parameter1
        {
            get { return _param1; }
            set { _param1 = value; }
        }

        public object Parameter2
        {
            get 
            {
                if (_param2 != null)
                    return _param2;
                else
                    return (-1 as object);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to do something
        /// </summary>
        /// <param name="argument">Input argument</param>
        /// <returns>Bool if true</returns>
        public bool StandardMethodTemplate(object argument)
        {
            long one = 12345678987654321;
            long two = 11111111111111111;

            for (int i = 0; i < 100; i++)
            {
                if (one > two)
                    return true;
            }
            return false;
        }

        #endregion

        #region Private Methods and Properties

        private void StandardPrivateMethod()
        {
        }

        private bool IsThisReal
        {
            get
            {
                // Maximum line length of 100 characters
                if (this.GetType().Assembly.FullName.Equals("TerminalVelocity.Standard") ||
                    this.GetType().Assembly.IsDynamic)
                    return true;
                return false;
            }
        }

        #endregion
    }
}
