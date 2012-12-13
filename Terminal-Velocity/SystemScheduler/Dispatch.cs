# region Header

/*
 * Kent W. Nixon
 * Software Engineering
 * December 13, 2012
 */

# endregion

using System;
using System.Collections.Generic;
using Interfaces;
using SimulationEnvironment;

namespace SystemScheduler
{
    //This is a class representing a single dispatch order in the database
    public class Dispatch
    {
        # region Private Variables

        //The color of the line this dispatch is targeting
        private readonly string _color;

        //This dispatch's ID number
        private readonly int _dispatchID;

        //The route containing all of the custom waypoints of this dispatch
        private readonly IRoute _dispatchRoute;

        //The time this dispatch is to occur
        private readonly DateTime _dispatchTime;

        //The ever present environment
        private readonly ISimulationEnvironment _environment;

        //An integer representing the type of this route
        private readonly int _routeType;

        # endregion

        # region Constructors

        //The constructor for the dispatch
        public Dispatch(ISimulationEnvironment env, string id, string time, string type, string color, string route)
        {
            //Store all incoming data to our global variables
            _environment = env;
            _dispatchTime = DateTime.Parse(time);
            _dispatchID = int.Parse(id);
            _routeType = int.Parse(type);
            _color = color;

            //Create our route object based on whether or not this is a custom route
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

        //Accessor for our color
        public string Color
        {
            get { return _color; }
        }

        //Accessor for our time
        public DateTime DispatchTime
        {
            get { return _dispatchTime; }
        }

        //Accessor for our ID
        public int DispatchID
        {
            get { return _dispatchID; }
        }

        //Accessor for our type
        public int DispatchRouteType
        {
            get { return _routeType; }
        }

        //Accessor for our route
        public IRoute DispatchRoute
        {
            get { return _dispatchRoute; }
        }

        # endregion
    }
}