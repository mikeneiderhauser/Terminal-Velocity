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
        private RequestTypes _requestType;
        private int _trackControllerID;
        private int _trainID;
        private int _trainAuthority;
        private IRoute _trainRoute;
        private DateTime _issueDateTime; // automatically populated
        private IStatus _info; // added by TC
        private IBlock _block;
        #endregion

        #region Constructor
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
        public int TrackControllerID
        {
            get { return _trackControllerID; }
        }

        public int TrainID
        {
            get { return _trainID; }
        }

        public int TrainAuthority
        {
            get { return _trainAuthority; }
        }

        public IRoute TrainRoute
        {
            get { return _trainRoute; }
        }

        public DateTime IssueDateTime
        {
            get { return _issueDateTime; }
        }

        public IBlock Block
        {
            get { return _block; }
        }

        public IStatus Info
        {
            get { return _info; }
            set { _info = value; }
        }

        public RequestTypes RequestType
        {
            get { return _requestType; }
        }
        #endregion
    }
}
