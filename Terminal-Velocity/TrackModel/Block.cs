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
		private int _trackConID;

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
		for(int i=0;i<_attributes.Length;i++)
		{
			if(_attributes[i].Equals("SWITCH",StringComparison.Ordinal))
			{
				return true;
			}
		}

		return false;
		
		//Alternate implementation involves checking if switchDest2=-1


        }
		
	public bool hasTunnel()
	{
		for(int i=0;i<_attributes.Length;i++)
		{
			if(_attributes[i].Equals("TUNNEL",StringComparison.Ordinal))
			{
				return true;
			}
		}

		return false;

	}
		
	public bool hasHeater()
	{
		for(int i=0;i<_attributes.Length;i++)
		{
			if(_attributes[i].Equals("HEATER",StringComparison.Ordinal))
			{
				return true;
			}
		}

		return false;
	}
		
	public bool hasCrossing()
	{
		for(int i=0;i<_attributes.Length;i++)
		{
			if(_attributes[i].Equals("CROSSING",StringComparison.Ordinal))
			{
				return true;
			}
		}

		return false;
	}
		
	public bool hasStation()
	{
		for(int i=0;i<_attributes.Length;i++)
		{
			if(_attributes[i].Equals("STATION",StringComparison.Ordinal))
			{
				return true;
			}
		}

		return false;
	}

	public bool runsNorth()
	{
		if(_direction==DirEnum.North || _direction==DirEnum.North_AND_South)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool runsSouth()
	{
		if(_direction==DirEnum.South || _direction==DirEnum.North_AND_South)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool runsEast()
	{
		if(_direction==DirEnum.East || _direction==DirEnum.East_AND_West)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool runsWest()
	{
		if(_direction==DirEnum.West || _direction==DirEnum.East_AND_West)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool runsNorthEast()
	{
		if(_direction==DirEnum.Northeast || _direction==DirEnum.Northeast_AND_Southwest)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool runsNorthWest()
	{
		if(_direction==DirEnum.Northwest || _direction==DirEnum.Northwest_AND_Southeast)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool runsSouthEast()
	{
		if(_direction==DirEnum.Southeast || _direction==DirEnum.Northwest_AND_Southeast)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool runsSouthWest()
	{
		if(_direction==DirEnum.Southwest || _direction==DirEnum.Northeast_AND_Southwest)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
		
		
		
        #region Properties
        public int BlockID
        {
            get { return _blockID; }
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

		public int TrackConID
		{
			get {return _trackConID;}
			set 
			{
				_trackConID=value;
			}
		
		}
		#endregion
    }
}
