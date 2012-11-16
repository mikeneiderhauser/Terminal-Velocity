using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace CTCOffice
{
    class LineData
    {
        private List<ITrain> _trains;
        private List<IBlock> _blocks;
        private IBlock[][] _layout;

        public LineData(){}

        public void AddTrain(ITrain train)
        {
            _trains.Add(train);
        }

        public ITrain RemoveTrian(ITrain train)
        {
            ITrain toRemove = null;

            foreach (ITrain t in _trains)
            {
                //check if train ID's match.. need ITrain class to implement
            }

            return toRemove;
        }

        public void AddBlock(IBlock block)
        {
            _blocks.Add(block);
        }

        public List<ITrain> Trains 
        {
            get { return _trains; }
            set { _trains = value; }
        }

        public List<IBlock> Blocks
        {
            get { return _blocks; }
            set { _blocks = value; }
        }

        public IBlock[][] Layout
        {
            get { return _layout; }
            set { _layout = value; }
        }
    }
}
