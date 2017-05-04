using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KohdAndArt.Toolkit;

namespace SkylineProblemWinforms
{
    public partial class FormManageSkylineSettings : Form
    {
        public SkylineSettings Settings { get; set; }

        public SkylineSettings ParentSettings
        {
            get
            {
                return ((MainForm)Owner).Settings;
            }
        }

        public new MainForm ParentForm
        {
            get
            {
                return (MainForm)Owner;
            }
        }


        public FormManageSkylineSettings(MainForm parent, SkylineSettings s)
        {
            InitializeComponent();
            this.Owner = parent;
            this.Settings = s;
            SetBindings();
        }

        protected override void OnLoad(EventArgs e)
        {
            EnableSkylineBorderColorPicker(Settings.HighlightSkyline);
            EnableGridColorPicker(Settings.ShowGrid);

            panelSkylineBorderColorSwatch.BackColor =  ColorUtilities.GetColorFromHexRGBString(Settings.SkylineBorderColor);
            panelCanvasGridColorSwatch.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.GridColor);
            panelCanvasBackgroundColorSwatch.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.CanvasBackgroundColor);

            base.OnLoad(e);
        }

        private void SetBindings()
        {
            this.textBoxDefaultDataFile.DataBindings.Add("Text", Settings, "DefaultDataFile", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBoxHighlightSkyline.DataBindings.Add("Checked", Settings, "HighlightSkyline", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxSkylineBorderColor.DataBindings.Add("Text", Settings, "SkylineBorderColor", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBoxShowCoordinates.DataBindings.Add("Checked", Settings, "ShowCoordinates", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBoxShowXAxis.DataBindings.Add("Checked", Settings, "ShowXAxis", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBoxShowYAxis.DataBindings.Add("Checked", Settings, "ShowYAxis", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxCanvasBackgroundColor.DataBindings.Add("Text", Settings, "CanvasBackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBoxShowDataPointWindow.DataBindings.Add("Checked", Settings, "ShowDataPointWindow", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxCanvasMarginInPixels.DataBindings.Add("Text", Settings, "CanvasMarginInPixels", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBoxShowGrid.DataBindings.Add("Checked", Settings, "ShowGrid", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxCanvasGridColor.DataBindings.Add("Text", Settings, "GridColor", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetColorSwatch(Color currentColor, Panel swatchPanel, TextBox textBoxHexRGB)
        {
            Color c = SelectColor(currentColor);
            swatchPanel.BackColor = c;
            textBoxHexRGB.Text = KohdAndArt.Toolkit.ColorUtilities.ConvertColorToHexRGBString(c);
            ParentForm.OptionsUpdated();
        }

        private Color SelectColor(Color currentColor)
        {
            Color c = currentColor;
            var dlg = new ColorDialogEx();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                c = dlg.Color;
            }
            return c;
        }

        private void EnableSkylineBorderColorPicker(bool enable)
        {
            EnableColorPickerComponents(enable, 
                                        labelSkylineBorderColor, 
                                        panelSkylineBorderColorSwatch,
                                        textBoxSkylineBorderColor);
        }

        private void EnableGridColorPicker(bool enable)
        {
            EnableColorPickerComponents(enable, 
                                        labelCanvasGridColor, 
                                        panelCanvasGridColorSwatch,
                                        textBoxCanvasGridColor);
        }

        private void EnableColorPickerComponents(bool enable, Label l, Panel p, TextBox t)
        {
            l.Enabled = enable;
            p.Enabled = enable;
            t.Enabled = enable;
        }

        #region Event Handlers
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                if (filename.Length > 0 && File.Exists(filename))
                {
                    textBoxDefaultDataFile.Text = filename;
                }
            }
        }

        private void checkBoxHighlightSkyline_CheckedChanged(object sender, EventArgs e)
        {
            EnableSkylineBorderColorPicker(checkBoxHighlightSkyline.Checked);
            ParentForm.OptionsUpdated();
        }

        private void checkBoxShowDataPointWindow_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = checkBoxShowDataPointWindow.Checked;
            Settings.ShowDataPointWindow = flag;
            ParentSettings.ShowDataPointWindow = flag;
            ParentForm.OptionsUpdated();
        }

        private void checkBoxShowXAxis_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = checkBoxShowXAxis.Checked;
            Settings.ShowXAxis = flag;
            ParentSettings.ShowXAxis = flag;
            ParentForm.OptionsUpdated();
        }

        private void checkBoxShowYAxis_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = checkBoxShowYAxis.Checked;
            Settings.ShowYAxis = flag;
            ParentSettings.ShowYAxis = flag;
            ParentForm.OptionsUpdated();
        }

        private void checkBoxShowCoordinates_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = checkBoxShowCoordinates.Checked;
            Settings.ShowCoordinates = flag;
            ParentSettings.ShowCoordinates = flag;
            ParentForm.OptionsUpdated();
        }

        private void checkBoxShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            var flag = checkBoxShowGrid.Checked;
            Settings.ShowGrid = flag;
            ParentSettings.ShowGrid = flag;
            EnableGridColorPicker(flag);
            ParentForm.OptionsUpdated();
        }

        private void panelSkylineBorderColorSwatch_Click(object sender, EventArgs e)
        {
            SetColorSwatch(panelSkylineBorderColorSwatch.BackColor, 
                           panelSkylineBorderColorSwatch, 
                           textBoxSkylineBorderColor);
        }

        private void panelCanvasBackgroundColorSwatch_Click(object sender, EventArgs e)
        {
            SetColorSwatch(panelCanvasBackgroundColorSwatch.BackColor,
                           panelCanvasBackgroundColorSwatch, 
                           textBoxCanvasBackgroundColor);
        }

        private void panelCanvasGridColorSwatch_Click(object sender, EventArgs e)
        {
            SetColorSwatch(panelCanvasGridColorSwatch.BackColor,
                           panelCanvasGridColorSwatch,
                           textBoxCanvasGridColor);
        }
        

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
