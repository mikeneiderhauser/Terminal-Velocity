using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using Utility;

namespace TrackModel
{

    public class DBManager
    {
        //Private parameters
		private int _blockID;

        public DBManager(int bID)
        {
		_blockID=bID;
        }
		
		
	//Public methods
        public String createQueryString(string qType, int ID)
        {
		return null;
	}
	
	public String createUpdate(string updateType, Block bToUpdate)
	{
		return null;
	}

	public String createInsert(Block b)
	{
		return null;
	}


	//Return type should be changed into some sort of
	//SQLResults object after I examine the libraries
	//and classes used for this type of thing in C#
	public void runQuery(string sqlQuery)
	{

	}


	public bool runUpdate(string sqlUpdate)
	{
		return false;
	}

	public bool runInsert(string sqlInsert)
	{
		return false;
	}


	//Argument to this function shouldbe changed
	//into the SQLResults object returned from
	//runQuery() above
	public Block formatQueryResults(void)
	{
		return null;
	}

	//Argument to this function should be changed
	//into the SQLResults object returned from
	//runQuery above (and used in fQR above)
	public Route formatQueryResults(void)
	{
		return null;
	}
		
		
		
        #region Properties
	#endregion

    }
}
