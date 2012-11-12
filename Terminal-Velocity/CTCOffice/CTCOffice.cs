using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CTCOffice
{
    public partial class CTCOffice : Form
    {
        private Operator _op;
        public CTCOffice()
        {
            InitializeComponent();

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

        private void CTCOffice_Load(object sender, EventArgs e)
        {

        }

        private void _btnLoginLogout_Click(object sender, EventArgs e)
        {
            if (_btnLoginLogout.Text.Equals("Login"))
            {
                _op.login(_txtUsername.Text.ToString(), _txtPassword.Text.ToString());
                if (_op.isAuth())
                {
                    _txtPassword.Text = "";
                    loginStatusImage.Image = Properties.Resources.green;
                    _btnLoginLogout.Text = "Logout";
                }
                else
                {
                    //Tell Op that the credentials are invalid & clear
                    MessageBox.Show("Operators entered credentials are invalid!", "Login Failed!", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    _txtUsername.Text = "";
                    _txtPassword.Text = "";
                }
            }
            else
            {
                //button says logout
                if(_op.isAuth())
                {
                    //call logout
                }

                _txtUsername.Text = "";
                _txtPassword.Text = "";
                loginStatusImage.Image = Properties.Resources.red;
                _btnLoginLogout.Text = "Login";
            }
        }//end btnLogin Logout

    }
}
