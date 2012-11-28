using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public partial class RoutingTool : UserControl
    {
        private CTCOffice _ctc;
        private CTCOfficeGUI _ctcGui;
        private IBlock _block;

        public event EventHandler<EventArgs> EnablePointSelection;
        public event EventHandler<RoutingToolEventArgs> SubmitRoute;

        public RoutingTool(CTCOfficeGUI ctcgui, CTCOffice ctc)
        {
            InitializeComponent();
            _ctc = ctc;
            _ctcGui = ctcgui;
            _block = null;
        }

        public IBlock EndBlock
        {
            get { return _block; }
            set { _block = value; }
        }

        private void _btnRed_Click(object sender, EventArgs e)
        {
            if (SubmitRoute != null)
            {
                SubmitRoute(this, new RoutingToolEventArgs(null, 0));
            }
        }

        private void _btnGreen_Click(object sender, EventArgs e)
        {
            if (SubmitRoute != null)
            {
                SubmitRoute(this, new RoutingToolEventArgs(null, 1));
            }
        }

        private void _btnPoint_Click(object sender, EventArgs e)
        {
            if (EnablePointSelection != null)
            {
                EnablePointSelection(this, EventArgs.Empty);
            }
            this.Hide();
            _ctcGui.Show();
        }
    }
}
