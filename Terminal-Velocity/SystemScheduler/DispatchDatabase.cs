# region Header

/*
 * Kent W. Nixon
 * Software Engineering
 * December 13, 2012
 */

# endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Interfaces;

namespace SystemScheduler
{
    //This is the class representing the entire collection of dispatches stored in a database
    //as well as the logical operations that can be performed on them
    public class DispatchDatabase
    {
        # region Private Variables

        //A list of all of the dispatches we contain
        private readonly List<Dispatch> _dispatchlist = new List<Dispatch>();

        //The ever-present environment
        private readonly ISimulationEnvironment _environment;

        //The path to our database on disk
        private readonly string _filename;

        //Whether or not we were able to successfully parse the database we were given
        private readonly bool _successfulParse;

        //List of string arrays representing the dispatches in textual form
        //This is used to populate our DataGridView in the main GUI
        private List<string[]> _dispatchDataSource;

        # endregion

        # region Constructor(s)

        //The contructor for our dispatch database
        public DispatchDatabase(ISimulationEnvironment env, string filename)
        {
            //Store all incoming data to our globals
            _environment = env;
            _filename = filename;
            _successfulParse = ParseFile(_filename);
        }

        # endregion

        # region Properties

        public string DispatchDatabaseFilename
        {
            get { return _filename; }
        }

        public List<Dispatch> DispatchList
        {
            get { return _dispatchlist; }
        }

        public List<string[]> DispatchDatabaseDataSource
        {
            get { return _dispatchDataSource; }
        }

        public bool SuccessfulParse
        {
            get { return _successfulParse; }
        }

        # endregion

        # region Private Methods

        //Method to parse the database file we were given
        private bool ParseFile(string filename)
        {
            //Parse the .csv file into string arrays
            List<string[]> fileData = ParseCSV(filename);

            //Store this information to our global
            _dispatchDataSource = fileData;

            //For each string array in the list
            foreach (var singleDispatch in fileData)
            {
                //Make sure everything is formatted correctly so we can react gracefully if not
                if (InputCheckOK(singleDispatch))
                {
                        //Create a new dispatch object from the incoming data
                        _dispatchlist.Add(new Dispatch(_environment, singleDispatch[0], singleDispatch[1], singleDispatch[2],
                                                       singleDispatch[3], singleDispatch[4]));
                }

                //If the data in the incoming database is improperly formatted
                else
                {
                    //Show an error to the user and get the hell out of here
                    MessageBox.Show("The selected database is not formatted correctly.");
                    return false;
                }
            }

            //If everything managed to go well, return true
            return true;
        }

        //Method to check that incoming data is in the expected format
        private bool InputCheckOK(string[] input) {

            //Create some temporaries to pass to some function calls we use later
            int tempInt;
            DateTime tempTime;

            //Check to make sure that:
            //a. There are 5 fields in the input
            //b. The id is an integer
            //c. The time is actually a time
            //d. Dispatch type is either 0 or 1
            //e. The line is either the green or the red line
            //f. If present, the waypoints are delimited correctly
            if (!(input.Length == 5) || !(int.TryParse(input[0], out tempInt)) || !(DateTime.TryParse(input[1], out tempTime)) || !(input[2].Equals("0") || input[2].Equals("1")) || !(input[3].Equals("Red") || input[3].Equals("Green")) || !(System.Text.RegularExpressions.Regex.IsMatch(input[4], @"[0-9](\|[0-9])*")))
            {
                //Return false if any of these assumptions are wrong
                return false;
            }

            //If everything checks out
            else
            {
                //Return true
                return true;
            }
        }

        //Method used to parse required data out of a .csv database
        private List<string[]> ParseCSV(string path)
        {
            //Create a list of string arrays to hold our information
            var parsedData = new List<string[]>();

            //If anything goes wrong catch the problem and drop out instead of crashing
            try
            {
                //Open the database file we were given
                using (var readFile = new StreamReader(path))
                {
                    //Create temporaries to hold the information being read in while we parse it
                    string line;
                    string[] row;

                    //While there are still lines in the database
                    while ((line = readFile.ReadLine()) != null)
                    {
                        //Split the line and commas to created an array of strings and then add that to the list
                        row = line.Split(',');
                        parsedData.Add(row);
                    }
                }
            }

            //If there was a problem
            catch (Exception e)
            {
                //Tell the user
                MessageBox.Show(e.Message);
            }

            //Give back all of the now parsed and formatted data
            return parsedData;
        }

        //Method used to write updated information to the .csv database on disk
        private void UpdateDatabase()
        {
            //Delete the old bastard so we don't have much to worry about
            File.Delete(_filename);

            //Create a new file in the same place
            var file = new StreamWriter(_filename);
            
            //And write each of our dispatches in memory back to it in the proper form
            foreach (var dispatchRecord in _dispatchDataSource)
            {
                file.WriteLine(dispatchRecord[0] + "," + dispatchRecord[1] + "," + dispatchRecord[2] + "," +
                               dispatchRecord[3] + "," + dispatchRecord[4]);
            }

            //Then close the file
            file.Close();
        }

        //Method to determine the lower number available to act as a dispatch ID
        private string getFreeID()
        {
            //Create two temporary variables to help us through this
            int i = 0;
            int f = -1;

            //While we still haven't found a good value for the ID
            while (i == 0)
            {
                //Increment f and set i = f
                f = f + 1;
                i = f;

                //The run through the list of dispatches we have checking if any have the same number as i
                foreach (Dispatch funtime in _dispatchlist)
                {

                    //If they do
                    if (funtime.DispatchID == i)
                    {

                        //Set i equal to zero and start over
                        i = 0;
                        break;
                    }
                }
            }

            //By the time we are done, we now have the lowest available value to assign as the ID of a new dispatch
            return i.ToString();
        }

        # endregion

        # region Public Methods

        //Method to remove an existing dispatch from our list
        //Given a dispatch ID
        public void RemoveDispatch(int dispatchID)
        {
            //Iterate through our dispatch database
            foreach (Dispatch singleDispatch in _dispatchlist)
            {
                //Until we find the dispatch with the matching ID
                if (singleDispatch.DispatchID == dispatchID)
                {
                    //Then delete it
                    _dispatchlist.Remove(singleDispatch);
                    break;
                }
            }

            //The also iterate through our display database (of string-ified dispatches)
            foreach (var singleDispatch in _dispatchDataSource)
            {
                //Until we find the same dispatch
                if (int.Parse(singleDispatch[0]) == dispatchID)
                {
                    //And remove it from here as well
                    _dispatchDataSource.Remove(singleDispatch);
                    break;
                }
            }

            //Now update our database on disk
            UpdateDatabase();
        }

        //Method to add a new dispatch to our dispatch database
        public void AddDispatch(string id, string time, string type, string color, string route)
        {
            //If it doesn't yet have an ID, give it one
            if (int.Parse(id) == -1)
            {
                id = getFreeID();
            }

            //Add the dispatch to our list of dispatches
            _dispatchlist.Add(new Dispatch(_environment, id, time, type, color, route));

            //Also add it to our list of display information
            _dispatchDataSource.Add(new[] { id, time, type, color, route });

            //Also update the database on disk
            UpdateDatabase();
        }

        # endregion
    }
}