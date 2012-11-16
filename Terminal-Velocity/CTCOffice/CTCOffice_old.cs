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
    public partial class CTCOffice_old : Form
    {
        /// <summary>
        /// Holds a reference to the environement
        /// </summary>
        private IEnvironment _env;

        /// <summary>
        /// The first track controller closest to the CTC Office (Red Line)
        /// </summary>
        private ITrackController _primaryTCRed;

        /// <summary>
        /// The first track controller closest to the CTC Office (Green Line)
        /// </summary>
        private ITrackController _primaryTCGreen;

        /// <summary>
        /// Holds a reference to the Operator Credentials / Login status
        /// </summary>
        private Operator _op;

        /// <summary>
        /// Declare StartAutomation Event
        /// </summary>
        public event EventHandler<EventArgs> StartAutomation;

        /// <summary>
        /// Declare StopAutomation Event
        /// </summary>
        public event EventHandler<EventArgs> StopAutomation;

        /// <summary>
        /// Constructor for the CTC Office
        /// </summary>
        /// <param name="env">Takes in the Environment as a param</param>
        public CTCOffice_old(IEnvironment env, ITrackController primaryTCRed, ITrackController primaryTCGreen)
        {
            _env = env;
            _env.Tick += _env_Tick;

            /* Test even on checkbox
            this.StartAutomation += _handleEvent1;
            this.StopAutomation += _handleEvent2;
            */

            _primaryTCRed = primaryTCRed;
            _primaryTCGreen = primaryTCGreen;

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
        /// <summary>
        /// CTC Office form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CTCOffice_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Login / Logut button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnLoginLogout_Click(object sender, EventArgs e)
        {
            processLogin();
        }

        /// <summary>
        /// Function to handle login procedure
        /// </summary>
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
                _txtUsername.Focus();

                disableUserControl();
            }
        }//end btnLogin Logout

        /// <summary>
        /// function to enable user controls (calls setUserControlState)
        /// </summary>
        private void enableUserControl()
        {
            setUserControlState(true);
        }//end enable User Control

        /// <summary>
        /// function to disable user controls (calls setUserControlState)
        /// </summary>
        private void disableUserControl()
        {
            setUserControlState(false);
        }//end disable User Control

        /// <summary>
        /// sets the accessibility state of controls
        /// </summary>
        /// <param name="state"></param>
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
            txtGlobalTimeArea.Enabled = state;
        }

        /// <summary>
        /// Handles text changing in password field (handles enter)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles text changing in password field (handles enter)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //enter was pressed, click login button
                _btnLoginLogout_Click(_btnLoginLogout, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Function the sets up a request and sends request to primary track controller to dispatch a train
        /// </summary>
        private void dispatchTrain()
        {
            IRoute myRoute = promptForRoute();
            IRequest request = new Request(RequestTypes.DispatchTrain, 0, -1, 1, myRoute, null);
            sendRequest(request);
        }

        /// <summary>
        /// Function to prompt user to enter route details
        /// </summary>
        /// <returns></returns>
        private IRoute promptForRoute()
        {
            return null;
        }

        /// <summary>
        /// Sends any request to the primary track controller
        /// </summary>
        /// <param name="request"></param>
        private void sendRequest(IRequest request)
        {

        }

        /// <summary>
        /// Handles system scheudler automation check box being checked/unchecked -> fires events accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _checkAutomatedScheduling_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkAutomatedScheduling.Checked)
            {
                if (StartAutomation != null)
                {
                    StartAutomation(this, EventArgs.Empty);
                }
            }
            else
            {
                if (StopAutomation != null)
                {
                    StopAutomation(this, EventArgs.Empty);
                }
            }
        }//end _checkAutomatedScheduling_CheckedChanged

        /// <summary>
        /// Handles the Show Schedule Button(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnSchedule_1_Click(object sender, EventArgs e)
        {
            openScheduler();
        }

        /// <summary>
        /// Handles the Show Schedule Button(2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnSchedule_2_Click(object sender, EventArgs e)
        {
            openScheduler();
        }

        /// <summary>
        /// Shows the Scheduler Window
        /// </summary>
        private void openScheduler()
        {

        }
        #endregion
        

        //Make handleResponse implement a queue and check if Request.Info is null before processing


        #region Event Handlers
        /// <summary>
        /// Handles the environments tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _env_Tick(object sender, TickEventArgs e)
        {
           // int x = 0;
        }

        /*test even on check box
        private void _handleEvent1(object sender, EventArgs e)
        {
            int x = 0;
        }

        private void _handleEvent2(object sender, EventArgs e)
        {
            int x = 0;
        }
        */
        #endregion // Events
    }//end class
}//end namespace
