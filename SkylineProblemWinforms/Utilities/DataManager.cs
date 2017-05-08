using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylineProblemWinforms.Utilities
{
    public class DataManager
    {
        public IList<BuildingCoordinates>   Data { get; set; }

        private string _filename;
        public string Filename
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
                if (_filename.Length > 0)
                {
                    Load();
                }
            }
        }

        public int MaximumX { get; set; }
        public int MaximumY { get; set; }

        public DataManager(string filename)
        {
            this.Filename = filename;
            Load();
        }

        private void Load()
        {
            const char itemSeparator = ' ';

            if (Filename.Length == 0 || File.Exists(Filename) == false)
                throw new ArgumentException($"Filename not specified or file '{Filename}' not found.");

            var line = string.Empty;
            var list = new List<BuildingCoordinates>();

            using (var file = new StreamReader(Filename))
            {
                while ((line = file.ReadLine()) != null && (line.Length > 0))
                {
                    string[] temp = line.Split(itemSeparator);
                    var l = Convert.ToInt32(temp[0]);
                    var h = Convert.ToInt32(temp[1]);
                    var r = Convert.ToInt32(temp[2]);
                    list.Add(new BuildingCoordinates(l, h, r));
                }
            }

            // Determine the maximum X and Y for all of the data
            MaximumX = list.Max(a => a.Right);
            MaximumY = list.Max(a => a.Height);

            Data = list;
        }
    }
}
