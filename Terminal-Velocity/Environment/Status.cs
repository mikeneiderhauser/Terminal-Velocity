using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace SimulationEnvironment
{
    class Status : IStatus
    {
        #region Private Class Variables
        /// <summary>
        /// Holds the list of trains per track controller
        /// </summary>
        private List<ITrainModel> _trains;

        /// <summary>
        /// Holds the list of blocks per track controller
        /// </summary>
        private List<IBlock> _blocks;
        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="trains">list of trains per track controller</param>
        /// <param name="blocks">list of blocks per track controller</param>
        public Status(List<ITrainModel> trains, List<IBlock> blocks)
        {
            _trains = trains;
            _blocks = blocks;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Property to operate on _trains
        /// </summary>
        public List<ITrainModel> Trains
        {
            get { return _trains; }
            set { _trains = value; }
        }

        /// <summary>
        /// Property to operate on _blocks
        /// </summary>
        public List<IBlock> Blocks
        {
            get { return _blocks; }
            set { _blocks = value; }
        }
        #endregion
    }
}
