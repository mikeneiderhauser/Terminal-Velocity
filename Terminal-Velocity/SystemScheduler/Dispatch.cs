using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using TerminalVelocity;


namespace SystemScheduler
{
    class Dispatch : IDispatch
    {
        private ISimulationEnvironment _environment;
        private DateTime _dispatchTime;
        private int _dispatchID;
        private int _routeType;
        private Interfaces.IRoute _dispatchRoute;
        

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

        public Interfaces.IRoute DispatchRoute
        {
            get { return _dispatchRoute; }
        }

        public Dispatch(ISimulationEnvironment env, string time, string id, string type, string route)
        {
            _environment = env;
            _dispatchTime = DateTime.Parse(time);
            _dispatchID = int.Parse(id);
            _routeType = int.Parse(type);
            if (_routeType == 0)
            {
                List<IBlock> stops = null;
                IBlock last = null;
                foreach (string blockID in route.Split('|'))
                {
                    stops.Add(_environment.TrackModel.requestBlockInfo(int.Parse(blockID)));
                    last = _environment.TrackModel.requestBlockInfo(int.Parse(blockID));
                }
                _dispatchRoute = new SimulationEnvironment.Route(RouteTypes.PointRoute, last, 0, stops);
            }
            else
            {
                _dispatchRoute = new SimulationEnvironment.Route(RouteTypes.DefinedRoute, null, int.Parse(route), null);
            }
        }
    }
}
