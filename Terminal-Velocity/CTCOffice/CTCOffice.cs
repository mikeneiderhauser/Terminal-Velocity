using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public partial class CTCOffice : Form
    {
        private Environment _env;
        private Operator _op;

        public CTCOffice()
        {
            InitializeComponent();
            disableUserControl();
            txtGlobalTimeArea.Text = "1";
            loginStatusImage.Image = Properties.Resources.red;


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

        #region Private Functions
        private void CTCOffice_Load(object sender, EventArgs e)
        {

        }

        private void _btnLoginLogout_Click(object sender, EventArgs e)
        {
            processLogin();
        }

        private void processLogin()
        {
            if (_btnLoginLogout.Text.Equals("Login"))
            {
                _op.login(_txtUsername.Text.ToString(), _txtPassword.Text.ToString());
                if (_op.isAuth())
                {
                    _txtPassword.Text = "";
                    loginStatusImage.Image = Properties.Resources.green;
                    _btnLoginLogout.Text = "Logout";

                    enableUserControl();
                }
                else
                {
                    //Tell Op that the credentials are invalid & clear
                    MessageBox.Show("Operators entered credentials are invalid!", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _txtUsername.Text = "";
                    _txtPassword.Text = "";
                }
            }
            else
            {
                //button says logout
                if (_op.isAuth())
                {
                    _op.logout();
                }

                _txtUsername.Text = "";
                _txtPassword.Text = "";
                loginStatusImage.Image = Properties.Resources.red;
                _btnLoginLogout.Text = "Login";

                disableUserControl();
            }
        }//end btnLogin Logout

        private void enableUserControl()
        {
            setUserControlState(true);
        }//end enable User Control

        private void disableUserControl()
        {
            setUserControlState(false);
        }//end disable User Control

        private void setUserControlState(bool state)
        {
            _btnDispatchTrain.Enabled = state;
            _btnSchedule_1.Enabled = state;
            _btnSchedule_2.Enabled = state;
            _btnRefreshView.Enabled = state;
            _btnRefreshMetrics.Enabled = state;
            _btnSpeed.Enabled = state;
            _checkAutomatedScheduling.Enabled = state;
            dataGridTrackLayout.Enabled = state;
        }

        private void _txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        private void _txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        private void dispatchTrain()
        {
            IRoute myRoute = promptForRoute();
            IRequest request = new Request(RequestTypes.DispatchTrain, 0, -1, 1, myRoute, null);
            sendRequest(request);
        }

        private IRoute promptForRoute()
        {
            return null;
        }

        private void sendRequest(IRequest request)
        {

        }

        #endregion
    }//end class
}//end namespace
