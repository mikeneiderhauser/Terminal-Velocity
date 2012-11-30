using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Interfaces;

namespace SystemScheduler
{
    public class DispatchDatabase
    {
        # region Private Variables

        private readonly List<Dispatch> _dispatchlist = new List<Dispatch>();
        private readonly ISimulationEnvironment _environment;
        private readonly string _filename;
        private List<string[]> _dispatchDataSource;

        # endregion

        # region Constructor(s)

        public DispatchDatabase(ISimulationEnvironment env, string filename)
        {
            _environment = env;
            _filename = filename;
            ParseFile(_filename);
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
            foreach (var singleDispatch in fileData)
            {
                _dispatchlist.Add(new Dispatch(_environment, singleDispatch[0], singleDispatch[1], singleDispatch[2],
                                               singleDispatch[3], singleDispatch[4]));
            }
        }

        private List<string[]> ParseCSV(string path)
        {
            var parsedData = new List<string[]>();

            try
            {
                using (var readFile = new StreamReader(path))
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
            var file = new StreamWriter(_filename);
            foreach (var dispatchRecord in _dispatchDataSource)
            {
                file.WriteLine(dispatchRecord[0] + "," + dispatchRecord[1] + "," + dispatchRecord[2] + "," +
                               dispatchRecord[3] + "," + dispatchRecord[4]);
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
            foreach (var singleDispatch in _dispatchDataSource)
            {
                if (int.Parse(singleDispatch[0]) == dispatchID)
                {
                    _dispatchDataSource.Remove(singleDispatch);
                    break;
                }
            }
            UpdateDatabase();
        }

        public void AddDispatch(string id, string time, string type, string color, string route)
        {
            if (int.Parse(id) == -1)
            {
                id = getFreeID();
            }
            _dispatchlist.Add(new Dispatch(_environment, id, time, type, color, route));
            _dispatchDataSource.Add(new[] {id, time, type, color, route});
            UpdateDatabase();
        }

        # endregion
    }
}