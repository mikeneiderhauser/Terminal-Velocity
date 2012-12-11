using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CTCOffice
{
    public class MyPictureBox : PictureBox
    {
        private Panel _master;
        private UserControl _container;
        private TileContainerStats _attribs;

        public event EventHandler<MyPictureBoxEventArgs> ForceRedraw;

        public MyPictureBox(Panel linePanel, UserControl ctcOfficeGuiControl)
        {
            _master = linePanel;
            _container = ctcOfficeGuiControl;
            _attribs = null;
        }

        public void ReDrawMe()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(this.ReDrawMe));
                return;
            }

            if (ForceRedraw != null && _attribs != null)
            {
                ForceRedraw(this,new MyPictureBoxEventArgs(_attribs));
            }
        }

        public TileContainerStats Attributes
        {
            get { return _attribs; }
            set { _attribs = value; }
        }

    }
}
