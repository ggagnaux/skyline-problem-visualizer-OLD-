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
        public string                       Filename { get; set; }
        public int                          MaximumX { get; set; }
        public int                          MaximumY { get; set; }

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
                    BuildingCoordinates d = new BuildingCoordinates(l, h, r);
                    list.Add(d);

                    // Determine the maximum X and Y for all of the data
                    if (MaximumX < r) { MaximumX = r; }
                    if (MaximumY < h) { MaximumY = h; }
                }
            }

            Data = list;
        }

    }
}
