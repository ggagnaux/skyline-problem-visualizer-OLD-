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

        [UserScopedSetting]
        [DefaultSettingValue("2.0")]
        public float XAxisWidth
        {
            get
            {
                return ((float)this["XAxisWidth"]);
            }

            set
            {
                this["XAxisWidth"] = (float)value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("ff0000")]
        public string XAxisColor
        {
            get
            {
                return (string)this["XAxisColor"];
            }

            set
            {
                this["XAxisColor"] = value;
            }
        }





        /// <summary>
        /// Show or Hide the Y Axis
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
        [DefaultSettingValue("2.0")]
        public float YAxisWidth
        {
            get
            {
                return ((float)this["YAxisWidth"]);
            }

            set
            {
                this["YAxisWidth"] = (float)value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("ff0000")]
        public string YAxisColor
        {
            get
            {
                return (string)this["YAxisColor"];
            }

            set
            {
                this["YAxisColor"] = value;
            }
        }


        [UserScopedSetting]
        [DefaultSettingValue("False")]
        public bool ShowDataCoordinates
        {
            get
            {
                return ((bool)this["ShowDataCoordinates"]);
            }

            set
            {
                this["ShowDataCoordinates"] = (bool)value;
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
        [DefaultSettingValue("5.0")]
        public float SkylineBorderWidth
        {
            get
            {
                return ((float)this["SkylineBorderWidth"]);
            }

            set
            {
                this["SkylineBorderWidth"] = (float)value;
            }
        }



        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int CanvasMarginLeft
        {
            get { return ((int)this["CanvasMarginLeft"]); }
            set { this["CanvasMarginLeft"] = (int)value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int CanvasMarginRight
        {
            get { return ((int)this["CanvasMarginRight"]); }
            set { this["CanvasMarginRight"] = (int)value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int CanvasMarginTop
        {
            get { return ((int)this["CanvasMarginTop"]); }
            set { this["CanvasMarginTop"] = (int)value; }
        }

        [UserScopedSetting]
        [DefaultSettingValue("0")]
        public int CanvasMarginBottom
        {
            get { return ((int)this["CanvasMarginBottom"]); }
            set { this["CanvasMarginBottom"] = (int)value; }
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
        /// Skyline foreground fill color 
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("FFFFFF")]
        public string SkylineFillForegroundColor
        {
            get
            {
                return (string)this["SkylineFillForegroundColor"];
            }

            set
            {
                this["SkylineFillForegroundColor"] = value;
            }
        }

        /// <summary>
        /// Skyline background fill color 
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("FFFFFF")]
        public string SkylineFillBackgroundColor
        {
            get
            {
                return (string)this["SkylineFillBackgroundColor"];
            }

            set
            {
                this["SkylineFillBackgroundColor"] = value;
            }
        }

        [UserScopedSetting]
        [DefaultSettingValue("True")]
        public bool SkylineFillFlag
        {
            get
            {
                return ((bool)this["SkylineFillFlag"]);
            }

            set
            {
                this["SkylineFillFlag"] = (bool)value;
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
        [DefaultSettingValue("000010")]
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

        /// <summary>
        /// Show the mouse coordinates?
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("True")]
        public bool ShowMouseCoordinates
        {
            get
            {
                return (bool)this["ShowMouseCoordinates"];
            }

            set
            {
                this["ShowMouseCoordinates"] = value;
            }
        }

        /// <summary>
        /// Show the information panel?
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("True")]
        public bool ShowInfoPanel
        {
            get
            {
                return (bool)this["ShowInfoPanel"];
            }

            set
            {
                this["ShowInfoPanel"] = value;
            }
        }
    }
}
