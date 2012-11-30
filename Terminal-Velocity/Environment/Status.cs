using System.Collections.Generic;
using Interfaces;

namespace SimulationEnvironment
{
    internal class Status : IStatus
    {
        #region Private Class Variables

        #endregion

        #region Constructor

        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="trains">list of trains per track controller</param>
        /// <param name="blocks">list of blocks per track controller</param>
        public Status(List<ITrainModel> trains, List<IBlock> blocks)
        {
            Trains = trains;
            Blocks = blocks;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Property to operate on _trains
        /// </summary>
        public List<ITrainModel> Trains { get; set; }

        /// <summary>
        ///     Property to operate on _blocks
        /// </summary>
        public List<IBlock> Blocks { get; set; }

        #endregion
    }
}