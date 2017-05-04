using System;
using System.Configuration;
using System.Drawing;

namespace SkylineProblemWinforms
{
    public class SkylineSettings : ApplicationSettingsBase
    {
        private static SkylineSettings _settings = null;
        public static SkylineSettings GetInstance()
        {
            if (_settings == null)
            {
                _settings = new SkylineSettings();
            }
            return _settings;
        }

        private SkylineSettings()
        {
        }

        [UserScopedSetting]
        [DefaultSettingValue("skyline_data3.txt")]
        public string DefaultDataFile
        {
            get
            {
                return ((String)this["DefaultDataFile"]);
            }

            set
            {
                this["DefaultDataFile"] = (String)value;
            }
        }

        /// <summary>
        /// Show or Hide the X Axis
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("True")]
        public bool ShowXAxis
        {
            get
            {
                return ((bool)this["ShowXAxis"]);
            }

            set
            {
                this["ShowXAxis"] = (bool)value;
            }
        }

        /// <summary>
        /// Show or Hide the X Axis
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("True")]
        public bool ShowYAxis
        {
            get
            {
                return ((bool)this["ShowYAxis"]);
            }

            set
            {
                this["ShowYAxis"] = (bool)value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("False")]
        public bool ShowCoordinates
        {
            get
            {
                return ((bool)this["ShowCoordinates"]);
            }

            set
            {
                this["ShowCoordinates"] = (bool)value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("True")]
        public bool HighlightSkyline
        {
            get
            {
                return ((bool)this["HighlightSkyline"]);
            }

            set
            {
                this["HighlightSkyline"] = (bool)value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int CanvasMarginInPixels
        {
            get
            {
                return ((int)this["CanvasMarginInPixels"]);
            }

            set
            {
                this["CanvasMarginInPixels"] = (int)value;
            }
        }


        [UserScopedSetting]
        [DefaultSettingValue("True")]
        public bool ShowDataPointWindow
        {
            get
            {
                return ((bool)this["ShowDataPointWindow"]);
            }

            set
            {
                this["ShowDataPointWindow"] = (bool)value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("True")]
        public bool ShowGrid
        {
            get
            {
                return ((bool)this["ShowGrid"]);
            }

            set
            {
                this["ShowGrid"] = (bool)value;
            }
        }



        /// <summary>
        /// Skyline border color 
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("FF0000")]
        public string SkylineBorderColor
        {
            get
            {
                return (string)this["SkylineBorderColor"];
            }

            set
            {
                this["SkylineBorderColor"] = value;
            }
        }

        /// <summary>
        /// Grid Color
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("303030")]
        public string GridColor
        {
            get
            {
                return (string)this["GridColor"];
            }

            set
            {
                this["GridColor"] = value;
            }
        }

        /// <summary>
        /// Canvas Background Color
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("000000")]
        public string CanvasBackgroundColor
        {
            get
            {
                return (string)this["CanvasBackgroundColor"];
            }

            set
            {
                this["CanvasBackgroundColor"] = value;
            }
        }

    }
}
