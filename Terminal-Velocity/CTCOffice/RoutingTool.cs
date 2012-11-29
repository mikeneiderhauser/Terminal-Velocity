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
        private IBlock _sBlock;
        private ISimulationEnvironment _env;

        public event EventHandler<EventArgs> EnablePointSelection;
        public event EventHandler<RoutingToolEventArgs> SubmitRoute;

        public RoutingTool(CTCOfficeGUI ctcgui, CTCOffice ctc, ISimulationEnvironment env, IBlock sBlock)
        {
            InitializeComponent();
            _ctc = ctc;
            _ctcGui = ctcgui;
            _env = env;
            _sBlock = sBlock;

            _ctcGui.RoutingToolResponse += new EventHandler<EventArgs>(_ctcGui_RoutingToolResponse);
            
            _block = null;
        }

        /// <summary>
        /// handle response send by ctc gui with end block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _ctcGui_RoutingToolResponse(object sender, EventArgs e)
        {
            if (SubmitRoute != null)
            {
                //TODO - populate list of block inbetween current and dest
                IRoute r = new SimulationEnvironment.Route(RouteTypes.PointRoute, _block, -1, null);
                SubmitRoute(this, new RoutingToolEventArgs(r));
            }
        }

        public IBlock EndBlock
        {
            get { return _block; }
            set { _block = value; }
        }

        public IBlock Start
        {
            get { return _sBlock; }
            set { _sBlock = value; }
        }

        /// <summary>
        /// create route objuct for Red Route
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnRed_Click(object sender, EventArgs e)
        {
            if (SubmitRoute != null)
            {
                IRouteInfo routeInfo = _env.TrackModel.requestRouteInfo(0);
                IBlock endBlock = _env.TrackModel.requestBlockInfo(routeInfo.EndBlock, routeInfo.RouteName);
                IRoute r = new SimulationEnvironment.Route(RouteTypes.DefinedRoute, endBlock, routeInfo.RouteID, routeInfo.BlockList.ToList());
                SubmitRoute(this, new RoutingToolEventArgs(r));
            }
        }

        /// <summary>
        /// create route objuct for Green Route
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnGreen_Click(object sender, EventArgs e)
        {
            if (SubmitRoute != null)
            {
                IRouteInfo routeInfo = _env.TrackModel.requestRouteInfo(1);
                IBlock endBlock = _env.TrackModel.requestBlockInfo(routeInfo.EndBlock, routeInfo.RouteName);
                IRoute r = new SimulationEnvironment.Route(RouteTypes.DefinedRoute, endBlock, routeInfo.RouteID, routeInfo.BlockList.ToList());
                SubmitRoute(this, new RoutingToolEventArgs(r));
            }
        }

        private void _btnPoint_Click(object sender, EventArgs e)
        {
            if (EnablePointSelection != null)
            {
                //send event to allow interfacing to CTCGui
                EnablePointSelection(this, EventArgs.Empty);
            }

            //hide this gui
            this.ParentForm.Hide();
            //show ctc gui
            _ctcGui.Show();
        }
    }
}
