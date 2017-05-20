using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkylineProblemWinforms.Interfaces;
using SkylineProblemWinforms.Utilities;

namespace SkylineProblemWinforms.Controllers
{
    public class MainFormController : FormController
    {
        public UserSettings UserSettings { get; set; }
        public DataManager DataManager { get; set; }

        public MainFormController()
        {
            // Load the user configurable settings
            UserSettings = UserSettings.GetInstance();

            // Create the Data Manager and Load the data
            DataManager = DataManager.GetInstance(UserSettings.DefaultDataFile);
        }
    }
}

