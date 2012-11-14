using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace CTCOffice
{
    class Request : IRequest
    {
        #region Private Class Variables
        /// <summary>
        /// Holds the type of request
        /// </summary>
        private RequestTypes _requestType;

        /// <summary>
        /// Holds the track controller id number that the request is meant for
        /// </summary>
        private int _trackControllerID;

        /// <summary>
        /// Holds the train ID the request is meant for
        /// </summary>
        private int _trainID;

        /// <summary>
        /// Holds the Authority limit (set authority) or the speed limit (setSpeed)
        /// </summary>
        private int _trainAuthority;

        /// <summary>
        /// Holds train routing information
        /// </summary>
        private IRoute _trainRoute;

        /// <summary>
        /// Holds the time of the request
        /// </summary>
        private DateTime _issueDateTime; // automatically populated

        /// <summary>
        /// holds the status information from request
        /// </summary>
        private IStatus _info; // added by TC

        /// <summary>
        /// Holds the Block information that the request is meant for
        /// </summary>
        private IBlock _block;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for Request
        /// </summary>
        /// <param name="request">request type (enum)</param>
        /// <param name="trackControllerID">track controller id</param>
        /// <param name="trainID">train id</param>
        /// <param name="authority">authority/speed limit</param>
        /// <param name="route">route information</param>
        /// <param name="block">block information</param>
        public Request(RequestTypes request, int trackControllerID,
            int trainID, int authority, IRoute route,
            IBlock block)
        {
            _requestType = request;
            _trackControllerID = trackControllerID;
            _trainID = trainID;
            _trainAuthority = authority;
            _trainRoute = route;
            _block = block;
            _issueDateTime = DateTime.Now;
            _info = null;
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Property for _trackControllerID
        /// </summary>
        public int TrackControllerID
        {
            get { return _trackControllerID; }
        }

        /// <summary>
        /// Property for _trainID
        /// </summary>
        public int TrainID
        {
            get { return _trainID; }
        }

        /// <summary>
        /// Property for _trainAuthority
        /// </summary>
        public int TrainAuthority
        {
            get { return _trainAuthority; }
        }

        /// <summary>
        /// Property for _trainRoute
        /// </summary>
        public IRoute TrainRoute
        {
            get { return _trainRoute; }
        }

        /// <summary>
        /// Property for _issueDateTime
        /// </summary>
        public DateTime IssueDateTime
        {
            get { return _issueDateTime; }
        }

        /// <summary>
        /// Property for _block
        /// </summary>
        public IBlock Block
        {
            get { return _block; }
        }

        /// <summary>
        /// property for _info
        /// </summary>
        public IStatus Info
        {
            get { return _info; }
            set { _info = value; }
        }

        /// <summary>
        /// Property for _requestType
        /// </summary>
        public RequestTypes RequestType
        {
            get { return _requestType; }
        }
        #endregion
    }
}
