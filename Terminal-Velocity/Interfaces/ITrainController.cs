using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
   public interface ITrainController
    {
        
        
        int AuthorityLimit { get; set; }
        double SpeedLimit { get; set; }
        int Announcement { set; }
        double SpeedInput { get; set; }

        void addPassengers();
        void removePassengers();
        void checkLightsOn();
        void returnFeedback(String Feedback);
        void doorOpen();
        void doorClose();
        void sendPower(double Power);

 





    }
}
