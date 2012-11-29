using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public class ShowTrainEventArgs : EventArgs
    {
        public ITrainModel _train;

        public ShowTrainEventArgs(ITrainModel train)
        {
            _train = train;
        }

        public ITrainModel TrainModel
        {
            get { return _train; }
            set { _train = value; }
        }
    }
}
