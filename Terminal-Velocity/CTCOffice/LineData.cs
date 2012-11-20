﻿using System;
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

        public LineData(IBlock[][] layout, int line)
        {
            _trains = new List<ITrainModel>();
            _blocks = new List<IBlock>();
            _layout = new LayoutCellDataContainer[layout.GetUpperBound(0), layout.GetUpperBound(1)];

            for (int i = 0; i < layout.GetUpperBound(0); i++)
            {
                for(int j=0; j < layout.GetUpperBound(1); j++)
                {
                    LayoutCellDataContainer conatiner = new LayoutCellDataContainer();

                    //determine tile
                    if (layout[i][j] == null)
                    {
                        conatiner.Tile = Properties.Resources.Unpopulated;
                        //conatiner.Block = null;
                    }
                    else
                    {
                        _blocks.Add(layout[i][j]);
                        conatiner.Block = layout[i][j];

                        if (line == 0)
                        {
                            conatiner.Tile = Properties.Resources.RedTrack;
                        }
                        else
                        {
                            conatiner.Tile = Properties.Resources.GreenTrack;
                        }
                    }//end determine tile

                    _layout[i,j] = conatiner;
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
