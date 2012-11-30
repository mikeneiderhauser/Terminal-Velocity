using System;
using Interfaces;

namespace CTCOffice
{
    public class Request : IRequest
    {
        #region Private Class Variables

        /// <summary>
        ///     Holds the Block information that the request is meant for
        /// </summary>
        private readonly IBlock _block;

        /// <summary>
        ///     Holds the time of the request
        /// </summary>
        private readonly DateTime _issueDateTime; // automatically populated

        /// <summary>
        ///     Holds the type of request
        /// </summary>
        private readonly RequestTypes _requestType;

        /// <summary>
        ///     Holds the track controller id number that the request is meant for
        /// </summary>
        private readonly int _trackControllerID;

        /// <summary>
        ///     Holds the Authority limit (set authority) or the speed limit (setSpeed)
        /// </summary>
        private readonly int _trainAuthority;

        /// <summary>
        ///     Holds the train ID the request is meant for
        /// </summary>
        private readonly int _trainID;

        /// <summary>
        ///     Holds train routing information
        /// </summary>
        private readonly IRoute _trainRoute;

        /// <summary>
        ///     Holds the speed to set to the train in a request
        /// </summary>
        private readonly double _trainSpeed;

        #endregion

        #region Constructor

        /// <summary>
        ///     Constructor for Request
        /// </summary>
        /// <param name="request">request type (enum)</param>
        /// <param name="trackControllerID">track controller id</param>
        /// <param name="trainID">train id</param>
        /// <param name="authority">authority/speed limit</param>
        /// <param name="route">route information</param>
        /// <param name="block">block information</param>
        public Request(RequestTypes request, int trackControllerID,
                       int trainID, int authority, double speed, IRoute route,
                       IBlock block)
        {
            _requestType = request;
            _trackControllerID = trackControllerID;
            _trainID = trainID;
            _trainAuthority = authority;
            _trainRoute = route;
            _block = block;
            _issueDateTime = DateTime.Now;
            Info = null;
            _trainSpeed = speed;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Property for _trackControllerID
        /// </summary>
        public int TrackControllerID
        {
            get { return _trackControllerID; }
        }

        /// <summary>
        ///     Property for _trainID
        /// </summary>
        public int TrainID
        {
            get { return _trainID; }
        }

        /// <summary>
        ///     Property for _trainAuthority
        /// </summary>
        public int TrainAuthority
        {
            get { return _trainAuthority; }
        }

        /// <summary>
        ///     Property for _trainSpeed
        /// </summary>
        public double TrainSpeed
        {
            get { return _trainSpeed; }
        }

        /// <summary>
        ///     Property for _trainRoute
        /// </summary>
        public IRoute TrainRoute
        {
            get { return _trainRoute; }
        }

        /// <summary>
        ///     Property for _issueDateTime
        /// </summary>
        public DateTime IssueDateTime
        {
            get { return _issueDateTime; }
        }

        /// <summary>
        ///     Property for _block
        /// </summary>
        public IBlock Block
        {
            get { return _block; }
        }

        /// <summary>
        ///     property for _info
        /// </summary>
        public IStatus Info { get; set; }

        /// <summary>
        ///     Property for _requestType
        /// </summary>
        public RequestTypes RequestType
        {
            get { return _requestType; }
        }

        #endregion
    }
}