using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCOffice
{
    public class MyPictureBoxEventArgs : EventArgs
    {
        private TileContainerStats _stats;
        public MyPictureBoxEventArgs(TileContainerStats stats)
        {
            _stats = stats;
        }

        public TileContainerStats Attributes
        {
            get { return _stats; }
        }
    }
}
