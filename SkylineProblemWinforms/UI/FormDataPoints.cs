using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkylineProblemWinforms
{
    public partial class FormDataPoints : Form
    {
        public IList<BuildingCoordinates> _data;

        public FormDataPoints(MainForm parent)
        {
            InitializeComponent();
            this.Owner = parent;
        }

        public void SetData(IList<BuildingCoordinates> data)
        {
            this._data = data;

            int[] columnWidths = new int[] { 5, 7, 7 };

            // Clear old data and load new data
            listBoxDataPoints.Items.Clear();

            // Populate Listbox
            string title = string.Format("{0,-5}{1,-7}{2,-7}", "Left", "Height", "Right");
            listBoxDataPoints.Items.Add(title);

            string divider = "-------------------";
            listBoxDataPoints.Items.Add(divider);
            foreach (var a in _data)
            {
                string output = $"{a.Left,-5}{a.Height,-7}{a.Right,-7}";
                listBoxDataPoints.Items.Add(output);
            }
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            ((MainForm)this.Owner).Settings.ShowDataPointWindow = false;
            this.Visible = false;
        }
    }
}
