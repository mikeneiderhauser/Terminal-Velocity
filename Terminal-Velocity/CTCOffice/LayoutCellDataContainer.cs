using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Interfaces;
using Utility;

namespace CTCOffice
{
    private class LayoutCellDataContainer
    {
        private ITrainModel _train;
        private IBlock _block;
        private Image _tile;

        public LayoutCellDataContainer(IBlock block, Image tile)
        {
            _block = block;
            _tile = tile;
            _train = null;
        }

        private ITrainModel Train
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
