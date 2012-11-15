using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public class CTCOffice : ICTCOffice
    {
        #region Private Variables
        private IEnvironment _env;
        private ITrackController _primaryTrackControllerRed;
        private ITrackController _primaryTrackControllerGreen;
        private Queue<IRequest> _requestsOut;
        private Queue<IRequest> _requestsIn;
        private Operator _op;
        #endregion

        #region Constructor
        public CTCOffice(IEnvironment env, ITrackController redTC, ITrackController greenTC)
        {
            _env = env;
            _primaryTrackControllerGreen = greenTC;
            _primaryTrackControllerRed = redTC;

            //subscribe to Environment Tick
            _env.Tick += new EventHandler<TickEventArgs>(_env_Tick);

            //create new operator object
            _op = new Operator();
            //set credentials
            _op.setAuth("mike", "42");

            /* Unit Test of operator login - PASS
            //test login
            _op.login("mike", "42");
            if (_op.isAuth())
            {
                _op.logout();
            }
            */
        }

        public bool Login(string username, string password)
        {
            _op.login(username, password);
            return _op.isAuth();
        }

        public bool Logout()
        {
            if (_op.isAuth())
            {
                _op.logout();
            }

            return true;
        }

        public void sendRequest(IRequest request)
        {

        }

        //handle Environment Tick
        void _env_Tick(object sender, TickEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Interface
        public event EventHandler<EventArgs> StartAutomation;

        public event EventHandler<EventArgs> StopAutomation;

        /// <summary>
        /// Pass request from System Scheduler to Track Controller via send request
        /// </summary>
        /// <param name="request"></param>
        public void passRequest(IRequest request)
        {
            throw new NotImplementedException();
        }

        public void handleResponse(IRequest request)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
