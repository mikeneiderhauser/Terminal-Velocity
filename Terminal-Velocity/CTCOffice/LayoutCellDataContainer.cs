using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public class LayoutCellDataContainer
    {
        private ITrainModel _train;
        private IBlock _block;
        private Image _tile;

        public LayoutCellDataContainer()
        {
            _block = null;
            _tile = null;
            _train = null;
        }

        public ITrainModel Train
        {
            get { return _train; }
            set { _train = value; }
        }

        public IBlock Block
        {
            get { return _block; }
            set { _block = value; }
        }

        public Image Tile
        {
            get { return _tile; }
            set { _tile = value; }
        }
    }
}
