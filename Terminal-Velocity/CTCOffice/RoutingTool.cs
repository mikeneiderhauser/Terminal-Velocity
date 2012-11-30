using System;
using System.Linq;
using System.Windows.Forms;
using Interfaces;
using SimulationEnvironment;

namespace CTCOffice
{
    public partial class RoutingTool : UserControl
    {
        private readonly CTCOfficeGUI _ctcGui;
        private readonly ISimulationEnvironment _env;
        private IBlock _block;
        private CTCOffice _ctc;

        public RoutingTool(CTCOfficeGUI ctcgui, CTCOffice ctc, ISimulationEnvironment env, IBlock sBlock)
        {
            InitializeComponent();
            _ctc = ctc;
            _ctcGui = ctcgui;
            _env = env;
            Start = sBlock;

            _ctcGui.RoutingToolResponse += _ctcGui_RoutingToolResponse;

            _block = null;
        }

        public IBlock EndBlock
        {
            get { return _block; }
            set { _block = value; }
        }

        public IBlock Start { get; set; }
        public event EventHandler<EventArgs> EnablePointSelection;
        public event EventHandler<RoutingToolEventArgs> SubmitRoute;

        /// <summary>
        ///     handle response send by ctc gui with end block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ctcGui_RoutingToolResponse(object sender, EventArgs e)
        {
            if (SubmitRoute != null)
            {
                //TODO - populate list of block inbetween current and dest
                IRoute r = new Route(RouteTypes.PointRoute, _block, -1, null);
                SubmitRoute(this, new RoutingToolEventArgs(r));
            }
        }

        /// <summary>
        ///     create route objuct for Red Route
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnRed_Click(object sender, EventArgs e)
        {
            if (SubmitRoute != null)
            {
                IRouteInfo routeInfo = _env.TrackModel.requestRouteInfo(0);
                IBlock endBlock = _env.TrackModel.requestBlockInfo(routeInfo.EndBlock, routeInfo.RouteName);
                IRoute r = new Route(RouteTypes.DefinedRoute, endBlock, routeInfo.RouteID, routeInfo.BlockList.ToList());
                SubmitRoute(this, new RoutingToolEventArgs(r));
            }
        }

        /// <summary>
        ///     create route objuct for Green Route
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnGreen_Click(object sender, EventArgs e)
        {
            if (SubmitRoute != null)
            {
                IRouteInfo routeInfo = _env.TrackModel.requestRouteInfo(1);
                IBlock endBlock = _env.TrackModel.requestBlockInfo(routeInfo.EndBlock, routeInfo.RouteName);
                IRoute r = new Route(RouteTypes.DefinedRoute, endBlock, routeInfo.RouteID, routeInfo.BlockList.ToList());
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
            ParentForm.Hide();
            //show ctc gui
            _ctcGui.Show();
        }
    }
}