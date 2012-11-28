﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interfaces;
using Utility;
using System.IO;
using System.Windows.Forms;

namespace SystemScheduler
{

    public class DispatchDatabase
    {

        # region Private Variables

        private ISimulationEnvironment _environment;
        private string _filename;
        private List<Dispatch> _dispatchlist = new List<Dispatch>();
        private List<string[]> _dispatchDataSource;

        # endregion

        # region Constructor(s)

        public DispatchDatabase(ISimulationEnvironment env, string filename)
        {
            _environment = env;
            _filename = filename;
            this.ParseFile(_filename);
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

        # endregion

        # region Private Methods

        private void ParseFile(string filename)
        {
            List<string[]> fileData = ParseCSV(filename);
            _dispatchDataSource = fileData;
            foreach (string[] singleDispatch in fileData)
            {
                _dispatchlist.Add(new Dispatch(_environment, singleDispatch[0], singleDispatch[1], singleDispatch[2], singleDispatch[3]));
            }
        }

        private List<string[]> ParseCSV(string path)
        {
            List<string[]> parsedData = new List<string[]>();

            try
            {
                using (StreamReader readFile = new StreamReader(path))
                {
                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        parsedData.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return parsedData;
        }

        private void UpdateDatabase()
        {
            File.Delete(_filename);
            //File.Create(_filename);
            System.IO.StreamWriter file = new System.IO.StreamWriter(_filename);
            foreach (string[] dispatchRecord in _dispatchDataSource)
            {
                file.WriteLine(dispatchRecord[0] + "," + dispatchRecord[1] + "," + dispatchRecord[2] + "," + dispatchRecord[3]);
            }
            file.Close();
        }

        private string getFreeID()
        {
            int i = 0;
            int f = -1;
            while (i == 0)
            {
                f = f + 1;
                i = f;
                foreach (Dispatch funtime in _dispatchlist)
                {
                    if (funtime.DispatchID == i)
                    {
                        i = 0;
                        break;
                    }
                }
            }
            return i.ToString();
        }

        # endregion

        # region Public Methods

        public void RemoveDispatch(int dispatchID)
        {
            foreach (Dispatch singleDispatch in _dispatchlist)
            {
                if (singleDispatch.DispatchID == dispatchID)
                {
                    _dispatchlist.Remove(singleDispatch);
                    break;
                }
            }
            foreach (string[] singleDispatch in _dispatchDataSource)
            {
                if (int.Parse(singleDispatch[0]) == dispatchID)
                {
                    _dispatchDataSource.Remove(singleDispatch);
                    break;
                }
            }
            UpdateDatabase();
        }

        public void AddDispatch(string id, string time, string type, string route)
        {
            if (int.Parse(id) == -1)
            {
                id = getFreeID();
            }
            _dispatchlist.Add(new Dispatch(_environment, id, time, type, route));
            _dispatchDataSource.Add(new string[] {id, time, type, route});
            UpdateDatabase();
        }

        # endregion

    }

}