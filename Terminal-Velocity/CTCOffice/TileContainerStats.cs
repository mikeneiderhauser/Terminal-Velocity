using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public class TileContainerStats
    {
        private int _gridI;
        private int _gridJ;
        private int _gridX;
        private int _gridY;
        private LayoutCellDataContainer _container;
        private Panel _drawingPanel;

        public TileContainerStats(int i, int j, int x, int y, LayoutCellDataContainer c, Panel drawingPanel)
        {
            _gridI = i;
            _gridJ = j;
            _gridX = x;
            _gridY = y;
            _container = c;
            _drawingPanel = drawingPanel;
            /*
            MyPictureBox pane = new MyPictureBox(_panelRedLine,this);
            _panelRedLine.Controls.Add(pane);
            pane.Name = "_imgGridRed_" + i + "_" + j;
            pane.SizeMode = PictureBoxSizeMode.CenterImage;
            pane.Size = new Size(20, 20);
            pane.Location = new Point(x, y);
            pane.Image = _redLineData.Layout[i, j].Tile;
            pane.Tag = _redLineData.Layout[i, j];
            _redLineData.Layout[i, j].Panel = pane;
            pane.MouseClick += _layoutPiece_MouseClick;
            //pane.MouseHover += new EventHandler(this._layoutPiece_MouseHover);
            x += 20; 
             */
        }

        public int X
        {
            get { return _gridX; }
            set { _gridX = value; }
        }

        public int Y
        {
            get { return _gridY; }
            set { _gridY = value; }
        }

        public int I
        {
            get { return _gridI; }
            set { _gridI = value; }
        }

        public int J
        {
            get { return _gridJ; }
            set { _gridJ = value; }
        }

        public LayoutCellDataContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        public Panel LayoutPanel
        {
            get { return _drawingPanel; }
            set { _drawingPanel = value; }
        }
    }
}
