using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkylineProblemWinforms.UI
{
    public partial class InfoPanel : Form
    {
        public string MouseCoordinates
        {
            set
            {
                labelMousePosition.Text = value;
            }
        }

        public string ZoomLevel
        {
            set
            {
                labelZoomLevel.Text = value;
            }
        }


        #region Constructor(s)
        public InfoPanel(Form parent)
        {
            InitializeComponent();
            this.Owner = parent;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            labelMousePosition.Text = $"1322 x 985";
            labelZoomLevel.Text = $"100%";
            base.OnLoad(e);
        }

        public void SetMouseCoordinates(Point p)
        {
            string s = $"{p.X} x {p.Y}";
            s = string.Format("{0,20}", s);
            MouseCoordinates = s;
        }

        public void SetZoomLevel(float zoomLevel)
        {
            string s = $"{zoomLevel}%";
            s = string.Format("{0,20}", s);
            ZoomLevel = s;
        }
    }
}
