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

    }
}
