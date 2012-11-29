using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Utility;

namespace Interfaces
{
    public interface ISimulationEnvironment
    {
        /// <summary>
        /// Event that generates clock
        /// </summary>
        event EventHandler<TickEventArgs> Tick;

        /// <summary>
        /// A reference to the CTCOffice
        /// </summary>
        ICTCOffice CTCOffice { get; set; }

        /// <summary>
        /// A reference to System Scheduler Module
        /// </summary>
        ISystemScheduler SystemScheduler { get; set; }

        /// <summary>
        /// A reference to the first, or primary, track controller (Red)
        /// </summary>
        ITrackController PrimaryTrackControllerRed { get; set; }

        /// <summary>
        /// A reference to the first, or primary, track controller (Green)
        /// </summary>
        ITrackController PrimaryTrackControllerGreen { get; set; }

        /// <summary>
        /// A reference to the Track Model
        /// </summary>
        ITrackModel TrackModel { get; set; }

        /// <summary>
        /// A List of all trains in the system (Red and Green)
        /// </summary>
        List<ITrainModel> AllTrains { get; }

        /// <summary>
        /// Function to add train to AllTrains
        /// </summary>
        /// <param name="train">train to add</param>
        void addTrain(ITrainModel train);

        /// <summary>
        /// Function to remove train from AllTrains
        /// </summary>
        /// <param name="train">train to remove</param>
        void removeTrain(ITrainModel train);

        /// <summary>
        /// Send message to environment to log
        /// </summary>
        /// <param name="msg"></param>
        void sendLogEntry(string msg);

        /// <summary>
        /// Set speed to timer
        /// </summary>
        /// <param name="speed"></param>
        void setInterval(long interval);

        /// <summary>
        /// get speed of timer
        /// </summary>
        /// <returns></returns>
        long getInterval();

        /// <summary>
        /// Function to stop the environment timer -> CTC Access Only
        /// </summary>
        /// <param name="sender">ref to caller</param>
        void stopTick(object sender);

        /// <summary>
        /// Function to start environment timer -> CTC Access Only
        /// </summary>
        /// <param name="sender">ref to caller</param>
        void startTick(object sender);

        void Dispatch(IRequest request);
    }
}
