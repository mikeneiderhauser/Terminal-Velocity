using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CTCOffice
{
    public class MyPictureBox : PictureBox
    {
        private Panel _master;
        private UserControl _container;
        public MyPictureBox(Panel master, UserControl par)
        {
            _master = master;
            _container = par;
        }

        public override void Refresh()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(this.Refresh));
                return;
            }

            this.Update();
            /*
            _master.Refresh();
            _container.Refresh();
            base.Refresh();
            base.Invalidate();
            */
        }
    }
}
