using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    interface ICTCOffice
    {
        /// <summary>
        /// Event to start automation
        /// </summary>
        event EventHandler<EventArgs> StartAutomation;

        /// <summary>
        /// Event to stop automation
        /// </summary>
        event EventHandler<EventArgs> StopAutomation;

        /// <summary>
        /// function for System Scheduler to CTC to send request through CTC to Track Controller
        /// </summary>
        /// <param name="request">request sent to the track controller</param>
        void passRequest(IRequest request);

        /// <summary>
        /// function for Track Controller to send response to CTC
        /// </summary>
        /// <param name="request">request sent to the ctc office</param>
        void handleResponse(IRequest request);
    }
}