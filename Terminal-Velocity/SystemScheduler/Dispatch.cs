using System;
using System.Collections.Generic;
using Interfaces;
using SimulationEnvironment;

namespace SystemScheduler
{
    public class Dispatch
    {
        # region Private Variables

        private readonly string _color;
        private readonly int _dispatchID;
        private readonly IRoute _dispatchRoute;
        private readonly DateTime _dispatchTime;
        private readonly ISimulationEnvironment _environment;
        private readonly int _routeType;

        # endregion

        # region Constructors

        public Dispatch(ISimulationEnvironment env, string id, string time, string type, string color, string route)
        {
            _environment = env;
            _dispatchTime = DateTime.Parse(time);
            _dispatchID = int.Parse(id);
            _routeType = int.Parse(type);
            _color = color;
            if (_routeType == 0)
            {
                var stops = new List<IBlock>();
                IBlock last = null;
                foreach (string blockID in route.Split('|'))
                {
                    stops.Add(_environment.TrackModel.requestBlockInfo(int.Parse(blockID), _color));
                    last = _environment.TrackModel.requestBlockInfo(int.Parse(blockID), _color);
                }
                _dispatchRoute = new Route(RouteTypes.PointRoute, last, -1, stops);
            }
            else
            {
                _dispatchRoute = new Route(RouteTypes.DefinedRoute, null, int.Parse(route), null);
            }
        }

        # endregion

        # region Properties

        public string Color
        {
            get { return _color; }
        }

        public DateTime DispatchTime
        {
            get { return _dispatchTime; }
        }

        public int DispatchID
        {
            get { return _dispatchID; }
        }

        public int DispatchRouteType
        {
            get { return _routeType; }
        }

        public IRoute DispatchRoute
        {
            get { return _dispatchRoute; }
        }

        # endregion
    }
}