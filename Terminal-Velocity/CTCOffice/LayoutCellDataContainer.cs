﻿using System.Drawing;
using Interfaces;

namespace CTCOffice
{
    public class LayoutCellDataContainer
    {
        public LayoutCellDataContainer()
        {
            Block = null;
            Tile = null;
            Train = null;
        }

        public ITrainModel Train { get; set; }

        public IBlock Block { get; set; }

        public Image Tile { get; set; }
    }
}