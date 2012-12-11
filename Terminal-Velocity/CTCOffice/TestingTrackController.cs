using System;
using System.Collections.Generic;
using Interfaces;

namespace CTCOffice
{
    public class TestingTrackController : ITrackController
    {
        private int _id;
        private List<IBlock> _blocks;
        private List<ITrainModel> _trains;
        private TestingTrackModel _tm;
        private ISimulationEnvironment _env;

        public TestingTrackController(int id, TestingTrackModel tm, ISimulationEnvironment env)
        {
            _id = id;
            _blocks = new List<IBlock>();
            _trains = new List<ITrainModel>();
            _tm = tm;
            _env = env;
        }

        public IRequest Request
        {
            set 
            {
                if (TransferRequest != null)
                {
                    Request r = (Request)value;
                    bool safetyCheck = true;
                    if (r.RequestType == RequestTypes.TrackMaintenanceOpen)
                    {
                        IBlock b = r.Block;
                        foreach (ITrainModel t in _env.AllTrains)
                        {
                            if (b.BlockID == t.CurrentBlock.BlockID)
                            {
                                safetyCheck = false;
                            }
                        }

                        if (safetyCheck)
                        {
                            r.Block.State = StateEnum.Healthy;
                        }
                    }
                    else if (r.RequestType == RequestTypes.TrackMaintenanceClose)
                    {
                        IBlock b = r.Block;
                        foreach (ITrainModel t in _env.AllTrains)
                        {
                            if (b.BlockID == t.CurrentBlock.BlockID)
                            {
                                safetyCheck = false;
                            }
                        }

                        if (safetyCheck)
                        {
                            r.Block.State = StateEnum.PowerFailure;
                        }
                    }
                    TransferRequest(this, new RequestEventArgs(value));
                }
            }
        }

        public int ID
        {
            get { return _id; }
        }

        public ITrackController Previous
        {
            get
            {
                return null;
            }
            set
            {
                //do nothing for ctc simulation
            }
        }

        public ITrackController Next
        {
            get
            {
                return null;
            }
            set
            {
                //do nothing for ctc simulation
            }
        }

        public List<ITrainModel> Trains
        {
            get { return _trains; }
        }

        public List<IBlock> Blocks
        {
            get { return _blocks; }
        }

        public List<IRoute> Routes
        {
            get { return null; }
        }

        public void LoadPLCProgram(string filename)
        {
            //
        }

        public event EventHandler<EventArgs> TransferRequest;
    }
}