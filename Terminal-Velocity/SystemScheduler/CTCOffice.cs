using System;
using System.Collections.Generic;
using Interfaces;
using Utility;
using Utility.Properties;

namespace SystemScheduler
{
    public class CTCOffice : ICTCOffice
    {
        int _numRequests;

        # region Constructor(s)

        public CTCOffice()
        {
            _numRequests = 0;
        }

        # endregion

        # region Public Methods

        public event EventHandler<EventArgs> StartAutomation;

        public event EventHandler<EventArgs> StopAutomation;

        public void passRequest(IRequest request)
        {
            if (request != null) {
                _numRequests = _numRequests + 1;
            }
        }

        public void handleResponse(IRequest request)
        {

        }

        public void StartScheduling()
        {
            if (StartAutomation != null)
            {
                StartAutomation(this, EventArgs.Empty);
            }
        }

        public void StopScheduling()
        {
            if (StopAutomation != null)
            {
                StopAutomation(this, EventArgs.Empty);
            }
        }

        # endregion

        # region Properties

        public int NumRequests
        {
            get { return _numRequests; }
        }

        # endregion

    }
}
