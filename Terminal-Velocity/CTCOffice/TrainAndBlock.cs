using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public class TrainAndBlock
    {
        ITrainModel _train;
        IBlock _block;

        public TrainAndBlock()
        {
            _train = null;
            _block = null;
        }

        public ITrainModel Train
        {
            get { return _train; }
            set { _train = value; }
        }

        public IBlock Block
        {
            get { return _block; }
            set { _block = value; }
        }      
    }
}
