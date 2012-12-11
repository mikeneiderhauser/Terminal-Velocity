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
        //[DllImport("user32.dll")]
        //public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11; 

        private Panel _master;
        private UserControl _container;
        public MyPictureBox(Panel linePanel, UserControl ctcOfficeGuiControl)
        {
            _master = linePanel;
            _container = ctcOfficeGuiControl;
        }


        public void ReDrawMe()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(this.ReDrawMe));
            }

            this.CreateGraphics();

            //SendMessage(Parent.Handle, WM_SETREDRAW, true, 0);
           //Parent.Refresh();
        }

        /*
        public override void Refresh()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(this.Refresh));
                return;
            }


            
            
        }*/
    }
}
