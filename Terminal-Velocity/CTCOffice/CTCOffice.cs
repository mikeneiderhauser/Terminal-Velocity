using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public class CTCOffice : ICTCOffice
    {
        public event EventHandler<EventArgs> StartAutomation;

        public event EventHandler<EventArgs> StopAutomation;

        public void passRequest(IRequest request)
        {
            throw new NotImplementedException();
        }

        public void handleResponse(IRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
