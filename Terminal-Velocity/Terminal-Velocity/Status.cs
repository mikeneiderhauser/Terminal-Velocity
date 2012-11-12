using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace Terminal_Velocity
{
    class Status
    {
        #region Private Class Variables
        private List<ITrain> _trains;
        private List<IBlock> _blocks;
        #endregion

        #region Constructor
        public Status(List<ITrain> trains, List<IBlock> blocks)
        {
            _trains = trains;
            _blocks = blocks;
        }
        #endregion

        #region Public Properties
        public List<ITrain> Trains
        {
            get { return _trains; }
        }

        public List<IBlock> Blocks
        {
            get { return _blocks; }
        }
        #endregion
    }
}
