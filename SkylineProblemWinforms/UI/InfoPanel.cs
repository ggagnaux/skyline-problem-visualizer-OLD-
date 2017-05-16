using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace SkylineProblemWinforms.UI
{
    public partial class InfoPanel : MetroForm //Form
    {
        #region Properties
        public IList<BuildingCoordinates> Data { get; set; }

        private string MouseCoordinates
        {
            set
            {
                labelMousePosition.Text = value;
            }
        }

        private string ZoomLevel
        {
            set
            {
                labelZoomLevel.Text = value;
            }
        }
        #endregion

        #region Constructor(s)
        public InfoPanel(MainForm parent)
        {
            InitializeComponent();
            this.Owner = parent;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            SetMouseCoordinates(new Point(500, 500));
            SetZoomLevel(100);
            base.OnLoad(e);
        }


        #region Public Methods
        public void SetData(IList<BuildingCoordinates> data)
        {
            this.Data = data;

            // Clear old data and load new data
            listViewData.Clear();

            // Create the columns
            listViewData.Columns.Add("Left");
            listViewData.Columns.Add("Height");
            listViewData.Columns.Add("Right");

            // Populate Listbox
            foreach (var a in Data)
            {
                string[] output = {
                                    a.Left.ToString(),
                                    a.Height.ToString(),
                                    a.Right.ToString()
                                  };
                listViewData.Items.Add(new ListViewItem(output));
            }
        }

        public void SetMouseCoordinates(Point p)
        {
            var all = string.Format("{0,22}", p.X.ToString() + " x " + p.Y.ToString());
            MouseCoordinates = all;
        }

        public void SetZoomLevel(float _zoomLevel)
        {
            var zoomLevel = string.Format("{0}", (int)_zoomLevel);
            string s = $"{zoomLevel}%";
            s = string.Format("{0,11}", s);
            ZoomLevel = s;
        }
        #endregion
    }
}
