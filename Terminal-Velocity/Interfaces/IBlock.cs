using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IBlock
    {
	int BlockID{ get; }
	StateEnum State { get; set; }
	int PrevBlockID { get; set; }
	double StartingElev{ get; }
	double Grade { get; }
	int[] Location { get; }
	double BlockSize { get; set;}
	DirEnum Direction { get;}
	int SwitchDest1{ get; }
	int SwitchDest2{ get; }

	bool hasSwitch();
	bool hasTunnel();
	bool hasHeater();
	bool hasCrossing();
	bool runsNorth();
	bool runsSouth();
	bool runsEast();
	bool runsWest();
	bool runsNorthEast();
	bool runsNorthWest();
	bool runsSouthEast();
	bool runsSouthWest();
    }	
}
