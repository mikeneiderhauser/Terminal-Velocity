using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class TrainDetectedEventArgs : EventArgs
    {
        public int TrainID { get; private set; }

        public TrainDetectedEventArgs(int trainID)
        {
            TrainID = trainID;
        }
    }
}
