﻿using System;
using System.Collections.Generic;
using Utility;

namespace Interfaces
{
    public interface ISimulationEnvironment
    {
        /// <summary>
        ///     A reference to the CTCOffice
        /// </summary>
        ICTCOffice CTCOffice { get; set; }

        /// <summary>
        ///     A reference to System Scheduler Module
        /// </summary>
        ISystemScheduler SystemScheduler { get; set; }

        /// <summary>
        ///     A reference to the first, or primary, track controller (Red)
        /// </summary>
        ITrackController PrimaryTrackControllerRed { get; set; }

        /// <summary>
        ///     A reference to the first, or primary, track controller (Green)
        /// </summary>
        ITrackController PrimaryTrackControllerGreen { get; set; }

        /// <summary>
        ///     A reference to the Track Model
        /// </summary>
        ITrackModel TrackModel { get; set; }

        /// <summary>
        ///     A List of all trains in the system (Red and Green)
        /// </summary>
        List<ITrainModel> AllTrains { get; }

        /// <summary>
        ///     Event that generates clock
        /// </summary>
        event EventHandler<TickEventArgs> Tick;

        /// <summary>
        ///     Function to add train to AllTrains
        /// </summary>
        /// <param name="train">train to add</param>
        void AddTrain(ITrainModel train);

        /// <summary>
        ///     Function to remove train from AllTrains
        /// </summary>
        /// <param name="train">train to remove</param>
        void RemoveTrain(ITrainModel train);

        /// <summary>
        ///     Send message to environment to log
        /// </summary>
        /// <param name="msg"></param>
        void SendLogEntry(string msg);

        /// <summary>
        ///     Set speed to timer
        /// </summary>
        /// <param name="speed"></param>
        void SetInterval(long interval);

        /// <summary>
        ///     get speed of timer
        /// </summary>
        /// <returns></returns>
        long GetInterval();

        /// <summary>
        ///     Function to stop the environment timer
        /// </summary>
        /// <param name="sender">ref to caller</param>
        void StopTick();

        /// <summary>
        ///     Function to start environment timer
        /// </summary>
        /// <param name="sender">ref to caller</param>
        void StartTick();

        /// <summary>
        ///     Function to dispatch train (sim yard)
        /// </summary>
        /// <param name="request"></param>
        void Dispatch(IRequest request);
    }
}