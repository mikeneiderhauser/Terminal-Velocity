using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using Utility;

namespace SystemScheduler
{
    public class CTC_Dummy : ICTCOffice
    {
        private ISimulationEnvironment _env;
        private bool _start;
        private bool _stop;
        private int _requestCount;

        public CTC_Dummy(ISimulationEnvironment env)
        {
            _env = env;
            _env.Tick += new EventHandler<TickEventArgs>(_env_Tick);
            _start = false;
            _stop = false;
            _requestCount = 0;
        }

        void _env_Tick(object sender, TickEventArgs e)
        {
            //
        }

        public void StartScheduling()
        {
            if (!_start)
            {
                if (StartAutomation != null)
                {
                    StartAutomation(this, EventArgs.Empty);
                }
            }
        }

        public void StopScheduling()
        {
            if (!_stop)
            {
                if (StopAutomation != null)
                {
                    StopAutomation(this, EventArgs.Empty);
                }
            }
        }


        public event EventHandler<EventArgs> StartAutomation;

        public event EventHandler<EventArgs> StopAutomation;

        public void ExternalRefresh()
        {
            //throw new NotImplementedException();
        }

        public void passRequest(IRequest request)
        {
            if (request != null)
            {
                if (request.RequestType != null)
                {
                    _requestCount++;
                }
            }
        }

        public void handleResponse(IRequest request)
        {
            //throw new NotImplementedException();
        }

        public int RequestCount
        {
            get { return _requestCount; }
        }
    }
}
