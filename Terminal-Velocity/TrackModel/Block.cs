using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using Utility;

namespace TrackModel
{

    public class Block : IBlock
    {
        //Private parameters
		private int _blockID;
		private StateEnum _stateAttributes;
        	private int _prevBlockID;
		private double _startingElev;
		private double _grade;
		private int[] _location;
		private int _bSize;
		private DirEnum _direction;
		private string[] _attributes;
		private int _switchDest1;
		private int _switchDest2;

        public Block(int bID, StateEnum state,int pBID,double sElev, double g, int[] loc, int bS, DirEnum dir, string[] atts,int d1, int d2)
        {
		_blockID=bID;
		_stateAttributes=state;
		_prevBlockID=pBID;
		_startingElev=sElev;
		_grade=g;
		_location=loc;
		_bSize=bS;
		_direction=dir;
		_attributes=atts;
		_switchDest1=d1;
		_switchDest2=d2;
        }
		
		
	//Public methods
        public bool hasSwitch()
        {
			return false;
        }
		
		public bool hasTunnel()
		{
			return false;
		}
		
		public bool hasHeater()
		{
			return false;
		}
		
		public bool hasCrossing()
		{
			return false;
		}
		
		public bool hasStation()
		{
			return false;
		}

	public bool runsNorth()
	{
		return false;
	}

	public bool runsSouth()
	{
		return false;
	}

	public bool runsEast()
	{
		return false;
	}

	public bool runsWest()
	{
		return false;
	}

	public bool runsNorthEast()
	{
		return false;
	}

	public bool runsNorthWest()
	{
		return false;
	}

	public bool runsSouthEast()
	{
		return false;
	}

	public bool runsSouthWest()
	{
		return false;
	}
		
		
		
        #region Properties
        public int BlockID
        {
            get { return _trainID; }
        }
        
		
        public StateEnum State
		{
			get {return _stateAttributes;}

			set
			{
				_stateAttributes=value;
			}	
		}
		
		public int PrevBlockID
		{
			get { return _prevBlockID; }	
		}
		
		public double StartingElev
		{
			get { return _startingElev;}	
		}
		
		public double Grade
		{
			get {return _grade;}	
		}
		
		public int[] Location
		{
			get {return _location;}	
		}
		
		public int BlockSize
		{
			get {return _bSize;}
			set
			{
				_bSize=value;
			}
		}
		
		public DirEnum Direction
		{
			get {return _direction;}	
		}
		
		public int SwitchDest1
		{
			get {return _switchDest1;}
			set
			{
				_switchDest1=value;
			}	
		}
		
		public int SwitchDest2
		{
			get {return _switchDest2;}
			set
			{
				_switchDest2=value;
			}	
		}
		#endregion
    }
}
