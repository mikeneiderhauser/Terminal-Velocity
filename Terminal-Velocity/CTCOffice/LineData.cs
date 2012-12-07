using System.Collections.Generic;
using System.Drawing;
using Interfaces;

namespace CTCOffice
{
    public class LineData
    {
        private List<IBlock> _blocks;
        private ISimulationEnvironment _env;
        private LayoutCellDataContainer[,] _layout;
        private List<ITrainModel> _trains;
        private ResourceWrapper _res;

        public LineData(IBlock[,] layout, ISimulationEnvironment env, ResourceWrapper res)
        {
            _env = env;
            _res = res;
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
                        container.BaseTile = _res.Unpopulated;
                        container.Tile = container.BaseTile;
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
                            container.BaseTile = GetBlockType(container.Block);
                            container.Tile = container.BaseTile;
                        }
                        else if (layout[i, j].Line.CompareTo("Green") == 0 || layout[i, j].Line.CompareTo("green") == 0 ||
                                 layout[i, j].Line.CompareTo("G") == 0 || layout[i, j].Line.CompareTo("g") == 0)
                        {
                            //green line
                            container.BaseTile = GetBlockType(container.Block);
                            container.Tile = container.BaseTile;
                        }
                        else
                        {
                            container.BaseTile = _res.TrackError;
                            container.Tile = container.BaseTile;
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
                if (train.TrainID == t.TrainID)
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

        public Bitmap GetBlockType(IBlock block)
        {

            if (block.Line.CompareTo("Red") != 0)
            {
                //red
                if (block.State != StateEnum.Healthy) { return _res.RedTrackClosed; }
                if (block.hasSwitch()) { return _res.RedTrackSwitch; }
                if (block.hasTunnel()) { return _res.RedTrackTunnel; }
                if (block.hasHeater()) { return _res.RedTrackHeater; }
                if (block.hasCrossing()) { return _res.RedTrackCrossing; }
                if (block.hasStation()) { return _res.RedTrackStation; }
                return _res.RedTrack;
            }
            else if (block.Line.CompareTo("Green") != 0)
            {
                //green
                if (block.State != StateEnum.Healthy) { return _res.GreenTrackClosed; }
                if (block.hasSwitch()) { return _res.GreenTrackSwitch; }
                if (block.hasTunnel()) { return _res.GreenTrackTunnel; }
                if (block.hasHeater()) { return _res.GreenTrackHeater; }
                if (block.hasCrossing()) { return _res.GreenTrackCrossing; }
                if (block.hasStation()) { return _res.GreenTrackStation; }
                return _res.GreenTrack;
            }
            else
            {
                return _res.TrackError;
            }
        }

        public LayoutCellDataContainer TriangulateContainer(IBlock block)
        {
            return null;
        }
    }
}