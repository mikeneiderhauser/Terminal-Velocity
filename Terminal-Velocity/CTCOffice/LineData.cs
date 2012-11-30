using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace CTCOffice
{
    public class LineData
    {
        private List<ITrainModel> _trains;
        private List<IBlock> _blocks;
        private LayoutCellDataContainer[,] _layout;
        private ISimulationEnvironment _env;

        public LineData(IBlock[,] layout, ISimulationEnvironment env)
        {
            _env = env;
            _trains = new List<ITrainModel>();
            _blocks = new List<IBlock>();
            _layout = new LayoutCellDataContainer[layout.GetUpperBound(0)+1, layout.GetUpperBound(1)+1];

            //for each item in the 1st dimension (row)
            for (int i = 0; i <= layout.GetUpperBound(0); i++)
            {
                //for each item in the 2nd dimension (col)
                for(int j=0; j <= layout.GetUpperBound(1); j++)
                {
                    //make a new container
                    LayoutCellDataContainer container = new LayoutCellDataContainer();

                    //determine tile
                    if (layout[i,j] == null)
                    {
                        //null container
                        container.Tile = Utility.Properties.Resources.Unpopulated;
                        container.Block = null;
                        container.Train = null;
                    }
                    else
                    {
                        container.Train = null;
                        _blocks.Add(layout[i,j]);
                        container.Block = layout[i,j];

                        //expand after prototype
                        if (layout[i, j].Line.CompareTo("Red")==0 || layout[i, j].Line.CompareTo("red")==0 || layout[i, j].Line.CompareTo("R")==0 || layout[i, j].Line.CompareTo("r")==0)
                        {
                            //red line
                            container.Tile = Utility.Properties.Resources.RedTrack;
                        }
                        else if (layout[i, j].Line.CompareTo("Green") == 0 || layout[i, j].Line.CompareTo("green") == 0 || layout[i, j].Line.CompareTo("G") == 0 || layout[i, j].Line.CompareTo("g") == 0)
                        {
                            //green line
                            container.Tile = Utility.Properties.Resources.GreenTrack;
                        }
                        else
                        {
                            container.Tile = Utility.Properties.Resources.TrackError;
                            env.sendLogEntry("CTC Office: Line Data - IBlock.Line is invalid");
                        }
                    }//end determine tile


                    //add the container to the layout panel
                    _layout[i,j] = container;
                }//end for 2nd dimension
            }//end for 1st dimentsion
        }//end constructor

        public void AddTrain(ITrainModel train)
        {
            _trains.Add(train);
        }

        public ITrainModel RemoveTrian(ITrainModel train)
        {
            ITrainModel toRemove = null;

            foreach (ITrainModel t in _trains)
            {
                //check if train ID's match.. need ITrain class to implement
            }

            return toRemove;
        }

        public void AddBlock(IBlock block)
        {
            _blocks.Add(block);
        }

        public List<ITrainModel> Trains 
        {
            get { return _trains; }
            set { _trains = value; }
        }

        public List<IBlock> Blocks
        {
            get { return _blocks; }
            set { _blocks = value; }
        }

        public LayoutCellDataContainer[,] Layout
        {
            get { return _layout; }
            set { _layout = value; }
        }
    }
}
