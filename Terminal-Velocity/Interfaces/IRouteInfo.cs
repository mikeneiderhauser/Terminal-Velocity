using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IBlock
    {
	int RouteID{ get; }
	String RouteName { get;}
	int NumBlocks { get; }
	IBlock[] BlockList{ get; }
	int StartBlock{ get;}
	int EndBlock{ get;}
	
    }

}
