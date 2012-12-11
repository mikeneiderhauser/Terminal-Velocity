using System.Drawing;
using Interfaces;
using System.Windows.Forms;

namespace CTCOffice
{
    public class LayoutCellDataContainer
    {
        public LayoutCellDataContainer()
        {
            Block = null;
            Tile = null;
            Train = null;
            BaseTile = null;
            Panel = null;
        }

        public ITrainModel Train { get; set; }

        public IBlock Block { get; set; }

        public Image Tile { get; set; }

        public Image BaseTile { get; set; }

        public MyPictureBox Panel { get; set; }
    }
}