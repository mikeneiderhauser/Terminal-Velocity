using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IBlock
    {
        int BlockID { get; }
        //StateEnum State { get; set; }
        int PrevBlockID { get; set; }
        double StartingElev { get; }
        double Grade { get; }
        int[] Location { get; }
        double BlockSize { get; set; }
        //DirEnum Direction { get; }
        int SwitchDest1 { get; }
        int SwitchDest2 { get; }

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

public enum DirEnum 
	{
		North,
		South,
		East,
		West,
		Northeast,
		Northwest,
		Southeast,
		Southwest,
		North_AND_South,
		East_AND_West,
		Northeast_AND_Southwest,
		Northwest_AND_Southeast
	}

public enum StateEnum
	{
		PowerFailure,
		BrokenTrackFailure,
		CircuitFailure,
		Healthy
	}
}
