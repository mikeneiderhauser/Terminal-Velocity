﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IStatus
    {
        /// <summary>
        /// Property to operate on _trains
        /// </summary>
        List<ITrain> Trains { get; set; }

        /// <summary>
        /// Property to operate on _blocks
        /// </summary>
        List<IBlock> Blocks { get; set; }
    }
}