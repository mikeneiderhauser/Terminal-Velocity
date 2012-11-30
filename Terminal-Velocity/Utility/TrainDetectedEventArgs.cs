using System;

namespace Utility
{
    public class TrainDetectedEventArgs : EventArgs
    {
        public TrainDetectedEventArgs(int trainID)
        {
            TrainID = trainID;
        }

        public int TrainID { get; private set; }
    }
}