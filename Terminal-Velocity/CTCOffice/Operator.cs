﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCOffice
{
    class Operator
    {
        #region Private Class Variables

        /// <summary>
        /// Holds the user entered username
        /// </summary>
        private string _username;

        /// <summary>
        /// Holds the user entered password
        /// </summary>
        private string _password;

        /// <summary>
        /// Holds the correct username
        /// </summary>
        private string _authUsername;

        /// <summary>
        /// Holds the correct password
        /// </summary>
        private string _authPassword;

        #endregion

        #region Constructors

        /// <summary>
        /// Defualt constructor for the Operator
        /// </summary>
        public Operator()
        {
            _username = null;
            _password = null;
            _authPassword = null;
            _authUsername = null;
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// submits the user entered credentials
        /// </summary>
        /// <param name="username">user entered username</param>
        /// <param name="password">user entered password</param>
        public void login(string username, string password)
        {
            _username = new string(username.ToCharArray());
            _password = new string(password.ToCharArray());
        }

        /// <summary>
        /// clears the user entered credentials
        /// </summary>
        public void logout()
        {
            _username = null;
            _password = null;
        }

        /// <summary>
        /// Checks to see if the user is logged in
        /// </summary>
        /// <returns></returns>
        public Boolean isAuth()
        {
            Boolean status = false;

            if (_username == null || _password == null)
            {
                //handle null case
            }
            else
            {
                if (_username.Equals(_authUsername) && _password.Equals(_authPassword))
                {
                    status = true;
                }
            }
            return status;
        }

        #endregion

        #region Protected Functions
        /// <summary>
        /// Sets the correct credentials
        /// </summary>
        /// <param name="username">correct username</param>
        /// <param name="password">correct password</param>
        public void setAuth(string username, string password)
        {
            _authUsername = new string(username.ToCharArray());
            _authPassword = new string(password.ToCharArray());
        }

        #endregion

    }
}
