using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
   public interface ITrainController
    {
        ITrain Train { get; }
        List<IBlock> AuthorityBlocks {get; set;}
        int AuthorityLimit { get; set; }
        double SpeedLimit { get; set; }
        IBlock CurrentBlock { get; set; }
        int Announcement { set; }

        
        public void addPassengers();
        public void removePassengers();
        public void checkLightsOn();
        public void returnFeedback(String Feedback);
        public void doorOpen();
        public void doorClose();
        void sendPower(double Power);

 





    }
}
