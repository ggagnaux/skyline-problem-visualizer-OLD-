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
        #region Properties
        public IList<BuildingCoordinates> Data { get; set; }
        #endregion

        #region Constructor(s)
        public FormDataPoints(MainForm parent)
        {
            InitializeComponent();
            this.Owner = parent;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #region Public Methods
        /// <summary>
        /// Populate the listview
        /// </summary>
        /// <param name="data"></param>
        public void SetData(IList<BuildingCoordinates> data)
        {
            this.Data = data;

            //int[] columnWidths = new int[] { 5, 7, 7 };

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
        #endregion

        #region Event Handlers
        private void buttonHide_Click(object sender, EventArgs e)
        {
            ((MainForm)this.Owner).Settings.ShowDataPointWindow = false;
            this.Visible = false;
        }
        #endregion
    }
}
