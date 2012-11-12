using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCOffice
{
    class Operator
    {
        #region Private Class Variables

        private string _username;
        private string _password;
        private string _authUsername;
        private string _authPassword;

        #endregion

        #region Constructors
        public Operator()
        {
            _username = null;
            _password = null;
            _authPassword = null;
            _authUsername = null;
        }
        #endregion

        #region Public Functions
        public void login(string username, string password)
        {
            _username = new string(username.ToCharArray());
            _password = new string(password.ToCharArray());
        }

        public void logout()
        {
            _username = null;
            _password = null;
        }

        public Boolean isAuth()
        {
            Boolean status = false;

            if (_username.Equals(_authUsername) && _password.Equals(_authPassword))
            {
                status = true;
            }
            return status;
        }

        #endregion

        #region Protected Functions
        public void setAuth(string username, string password)
        {
            _authUsername = new string(username.ToCharArray());
            _authPassword = new string(password.ToCharArray());
        }

        #endregion

    }
}
