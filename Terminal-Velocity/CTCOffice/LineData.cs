using System.Collections.Generic;
using Interfaces;
using Utility.Properties;

namespace CTCOffice
{
    public class LineData
    {
        private List<IBlock> _blocks;
        private ISimulationEnvironment _env;
        private LayoutCellDataContainer[,] _layout;
        private List<ITrainModel> _trains;

        public LineData(IBlock[,] layout, ISimulationEnvironment env)
        {
            _env = env;
            _trains = new List<ITrainModel>();
            _blocks = new List<IBlock>();
            _layout = new LayoutCellDataContainer[layout.GetUpperBound(0) + 1,layout.GetUpperBound(1) + 1];

            //for each item in the 1st dimension (row)
            for (int i = 0; i <= layout.GetUpperBound(0); i++)
            {
                //for each item in the 2nd dimension (col)
                for (int j = 0; j <= layout.GetUpperBound(1); j++)
                {
                    //make a new container
                    var container = new LayoutCellDataContainer();

                    //determine tile
                    if (layout[i, j] == null)
                    {
                        //null container
                        container.Tile = Resources.Unpopulated;
                        container.Block = null;
                        container.Train = null;
                    }
                    else
                    {
                        container.Train = null;
                        _blocks.Add(layout[i, j]);
                        container.Block = layout[i, j];

                        //expand after prototype
                        if (layout[i, j].Line.CompareTo("Red") == 0 || layout[i, j].Line.CompareTo("red") == 0 ||
                            layout[i, j].Line.CompareTo("R") == 0 || layout[i, j].Line.CompareTo("r") == 0)
                        {
                            //red line
                            container.Tile = Resources.RedTrack;
                        }
                        else if (layout[i, j].Line.CompareTo("Green") == 0 || layout[i, j].Line.CompareTo("green") == 0 ||
                                 layout[i, j].Line.CompareTo("G") == 0 || layout[i, j].Line.CompareTo("g") == 0)
                        {
                            //green line
                            container.Tile = Resources.GreenTrack;
                        }
                        else
                        {
                            container.Tile = Resources.TrackError;
                            env.sendLogEntry("CTC Office: Line Data - IBlock.Line is invalid");
                        }
                    } //end determine tile


                    //add the container to the layout panel
                    _layout[i, j] = container;
                } //end for 2nd dimension
            } //end for 1st dimentsion
        }

//end constructor

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
                if (toRemove.TrainID == t.TrainID)
                {
                    toRemove = t;
                    break;
                }
            }

            return toRemove;
        }

        public void AddBlock(IBlock block)
        {
            _blocks.Add(block);
        }

        public void RemoveBlock(IBlock block)
        {
            if (block != null)
            {
                if (_blocks.Contains(block))
                {
                    _blocks.Remove(block);
                }
            }
        }

        public int[] TriangulateBlock(IBlock block)
        {
            int[] index = new int[2]{-1,-1};

            int i = 0;
            int j = 0;
            for (i = 0; i <= _layout.GetUpperBound(0); i++)
            {
                for (j = 0; j <= _layout.GetUpperBound(1); j++)
                {
                    if (_layout[i, j] != null)
                    {
                        if (block.BlockID == _layout[i, j].Block.BlockID)
                        {
                            index[0] = i;
                            index[1] = j;
                            return index;
                        }
                    }
                }
            }

            return null;
        }//end triangulate block

        public void UpdateBlock(int listIndex, int layoutX, int layoutY, StateEnum state)
        {

        }
    }
}