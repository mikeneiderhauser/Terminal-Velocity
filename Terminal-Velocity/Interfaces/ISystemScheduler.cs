﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface ISystemScheduler
    {
        IRequest GetRoute { get; }
    }
}
