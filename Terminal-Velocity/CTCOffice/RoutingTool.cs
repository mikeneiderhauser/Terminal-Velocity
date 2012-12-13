using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Interfaces;
using SimulationEnvironment;

namespace CTCOffice
{
    public partial class RoutingTool : UserControl
    {
        private readonly CTCOfficeGUI _ctcGui;
        private readonly ISimulationEnvironment _env;
        private IBlock _endBlock;
        private CTCOffice _ctc;
        private IBlock _startBlock;

        public RoutingTool(CTCOfficeGUI ctcgui, CTCOffice ctc, ISimulationEnvironment env, IBlock sBlock, bool mode)
        {
            InitializeComponent();

            //mode = true for dispatch, mode = flase for update
            _btnPoint.Enabled = !mode;

            _ctc = ctc;
            _ctcGui = ctcgui;
            _env = env;
            _startBlock = sBlock;

            _ctcGui.RoutingToolResponse += _ctcGui_RoutingToolResponse;

            _endBlock = null;
        }

        public IBlock EndBlock
        {
            get { return _endBlock; }
            set { _endBlock = value; }
        }

        public IBlock Start { get; set; }
        public event EventHandler<EventArgs> EnablePointSelection;
        public event EventHandler<RoutingToolEventArgs> SubmitRoute;

        /// <summary>
        ///     handle response send by ctc gui with end block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ctcGui_RoutingToolResponse(object sender, RoutingToolEventArgs e)
        {
            if (e.Block != null)
            {
                _endBlock = e.Block;

                if (SubmitRoute != null)
                {
                    //TODO - populate list of block inbetween current and dest
                    string line;
                    if (_startBlock.Line.CompareTo("Red") == 0)
                    {
                        line = "Red";
                    }
                    else
                    {
                        line = "Green";
                    }

                    IBlock[] b = _env.TrackModel.requestPath(_startBlock.BlockID, _endBlock.BlockID, line);
                    List<IBlock> routeBlocks = b.ToList<IBlock>();
                    IRoute r = new Route(RouteTypes.PointRoute, _endBlock, -1, routeBlocks);
                    SubmitRoute(this, new RoutingToolEventArgs(r,null));
                }
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
                SubmitRoute(this, new RoutingToolEventArgs(r,null));
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
                SubmitRoute(this, new RoutingToolEventArgs(r,null));
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