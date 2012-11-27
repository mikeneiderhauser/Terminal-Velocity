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

        
        void addPassengers();
        void removePassengers();
        void checkLightsOn();
        void returnFeedback(String Feedback);
        void doorOpen();
        void doorClose();
        void sendPower(double Power);

 





    }
}
