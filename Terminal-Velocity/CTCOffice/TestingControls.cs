using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CTCOffice
{
    public partial class TestingControls : UserControl
    {
        private TestingTrackModel _tm;
        public TestingControls(TestingTrackModel tm)
        {
            InitializeComponent();
            _tm = tm;
        }

        private void _btnCloseGreenBlock_Click(object sender, EventArgs e)
        {
            _tm.CloseBlock(1, "Green");
        }

        private void _btnCloseRedBlock_Click(object sender, EventArgs e)
        {
            _tm.CloseBlock(1, "Red");
        }

        private void _btnThrowChange_Click(object sender, EventArgs e)
        {
            _tm.ThrowTrackChanged();
        }
    }
}
