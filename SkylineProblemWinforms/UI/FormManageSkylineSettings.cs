using System;
using System.Drawing;
using System.IO;
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
            EnableSkylineSettings(Settings.HighlightSkyline);
            EnableGridColorPicker(Settings.ShowGrid);
            EnableXAxisSettings(Settings.ShowXAxis);
            EnableYAxisSettings(Settings.ShowYAxis);

            panelSkylineBorderColorSwatch.BackColor =  ColorUtilities.GetColorFromHexRGBString(Settings.SkylineBorderColor);
            panelCanvasGridColorSwatch.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.GridColor);
            panelCanvasBackgroundColorSwatch.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.CanvasBackgroundColor);
            panelXAxisColorSwatch.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.XAxisColor);
            panelYAxisColorSwatch.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.YAxisColor);

            //SkylineBorderColorPicker.CurrentColor = ColorUtilities.GetColorFromHexRGBString(Settings.SkylineBorderColor); ;

            panelSkylineFillForegroundColorSwatch.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.SkylineFillForegroundColor);
            panelSkylineFillBackgroundColorSwatch.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.SkylineFillBackgroundColor);

            base.OnLoad(e);
        }

        private void SetBindings()
        {
            this.textBoxDefaultDataFile.DataBindings.Add("Text", Settings, "DefaultDataFile", false, DataSourceUpdateMode.OnPropertyChanged);

            // Skyline Border Settings
            this.checkBoxHighlightSkyline.DataBindings.Add("Checked", Settings, "HighlightSkyline", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxSkylineBorderColor.DataBindings.Add("Text", Settings, "SkylineBorderColor", false, DataSourceUpdateMode.OnPropertyChanged);
            //this.SkylineBorderColorPicker.DataBindings.Add("Text", Settings, "SkylineBorderColor", false, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
            this.textBoxSkylineBorderWidth.DataBindings.Add("Text", Settings, "SkylineBorderWidth", false, DataSourceUpdateMode.OnPropertyChanged);

            this.checkBoxEnableSkylineFill.DataBindings.Add("Checked", Settings, "SkylineFillFlag", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxSkylineFillForegroundColor.DataBindings.Add("Text", Settings, "SkylineFillForegroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxSkylineFillBackgroundColor.DataBindings.Add("Text", Settings, "SkylineFillBackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);

            // Axis Settings
            this.checkBoxShowXAxis.DataBindings.Add("Checked", Settings, "ShowXAxis", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxXAxisColor.DataBindings.Add("Text", Settings, "XAxisColor", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxXAxisWidth.DataBindings.Add("Text", Settings, "XAxisWidth", false, DataSourceUpdateMode.OnPropertyChanged);

            this.checkBoxShowYAxis.DataBindings.Add("Checked", Settings, "ShowYAxis", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxYAxisColor.DataBindings.Add("Text", Settings, "YAxisColor", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxYAxisWidth.DataBindings.Add("Text", Settings, "YAxisWidth", false, DataSourceUpdateMode.OnPropertyChanged);

            // Grid Settings
            this.checkBoxShowGrid.DataBindings.Add("Checked", Settings, "ShowGrid", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxCanvasGridColor.DataBindings.Add("Text", Settings, "GridColor", false, DataSourceUpdateMode.OnPropertyChanged);

            // Canvas Settings
            this.textBoxCanvasBackgroundColor.DataBindings.Add("Text", Settings, "CanvasBackgroundColor", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxMarginLeft.DataBindings.Add("Text", Settings, "CanvasMarginLeft", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxMarginRight.DataBindings.Add("Text", Settings, "CanvasMarginRight", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxMarginTop.DataBindings.Add("Text", Settings, "CanvasMarginTop", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxMarginBottom.DataBindings.Add("Text", Settings, "CanvasMarginBottom", false, DataSourceUpdateMode.OnPropertyChanged);

            // Other Settings
            this.checkBoxShowDataCoordinates.DataBindings.Add("Checked", Settings, "ShowDataCoordinates", false, DataSourceUpdateMode.OnPropertyChanged);
            this.checkBoxShowInfoPanel.DataBindings.Add("Checked", Settings, "ShowInfoPanel", false, DataSourceUpdateMode.OnPropertyChanged);
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

        private void EnableSkylineSettings(bool enable)
        {
            EnableColorPickerComponents(enable, 
                                        labelSkylineBorderColor, 
                                        panelSkylineBorderColorSwatch,
                                        textBoxSkylineBorderColor);
            labelSkylineBorderWidth.Enabled = enable;
            textBoxSkylineBorderWidth.Enabled = enable;

            checkBoxEnableSkylineFill.Enabled = enable;
            EnableSkylineFillForegroundSettings(enable);
            EnableSkylineFillBackgroundSettings(enable);
        }

        private void EnableSkylineFillForegroundSettings(bool enable)
        {
            EnableColorPickerComponents(enable,
                                        labelSkylineForegroundFillColor,
                                        panelSkylineFillForegroundColorSwatch,
                                        textBoxSkylineFillForegroundColor);

            labelSkylineForegroundFillColor.Enabled = enable;
        }

        private void EnableSkylineFillBackgroundSettings(bool enable)
        {
            EnableColorPickerComponents(enable,
                                        labelSkylineBackgroundFillColor,
                                        panelSkylineFillBackgroundColorSwatch,
                                        textBoxSkylineFillBackgroundColor);

            labelSkylineBackgroundFillColor.Enabled = enable;
        }

        private void EnableXAxisSettings(bool enable)
        {
            EnableColorPickerComponents(enable,
                                        labelXAxisColor,
                                        panelXAxisColorSwatch,
                                        textBoxXAxisColor);
            labelXAxisWidth.Enabled = enable;
            textBoxXAxisWidth.Enabled = enable;
        }

        private void EnableYAxisSettings(bool enable)
        {
            EnableColorPickerComponents(enable,
                                        labelYAxisColor,
                                        panelYAxisColorSwatch,
                                        textBoxYAxisColor);
            labelYAxisWidth.Enabled = enable;
            textBoxYAxisWidth.Enabled = enable;
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
            // Note - This ensures that this method isn't run
            // during application startup
            if (!checkBoxHighlightSkyline.IsHandleCreated)
                return;

            bool flag = checkBoxHighlightSkyline.Checked;
            EnableSkylineSettings(flag);
            ParentForm.OptionsUpdated();
        }

        private void checkBoxShowXAxis_CheckedChanged(object sender, EventArgs e)
        {
            // Note - This ensures that this method isn't run
            // during application startup
            if (!checkBoxShowXAxis.IsHandleCreated)
                return;

            bool flag = checkBoxShowXAxis.Checked;
            EnableXAxisSettings(flag);
            ParentForm.OptionsUpdated();
        }

        private void checkBoxShowYAxis_CheckedChanged(object sender, EventArgs e)
        {
            // Note - This ensures that this method isn't run
            // during application startup
            if (!checkBoxShowYAxis.IsHandleCreated)
                return;

            bool flag = checkBoxShowYAxis.Checked;
            EnableYAxisSettings(flag);
            ParentForm.OptionsUpdated();
        }

        private void checkBoxShowCoordinates_CheckedChanged(object sender, EventArgs e)
        {
            // Note - This ensures that this method isn't run
            // during application startup
            if (!checkBoxShowDataCoordinates.IsHandleCreated)
                return;

            bool flag = checkBoxShowDataCoordinates.Checked;
            Settings.ShowDataCoordinates = flag;
            ParentSettings.ShowDataCoordinates = flag;
            ParentForm.OptionsUpdated();
        }

        private void checkBoxShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            // Note - This ensures that this method isn't run
            // during application startup
            if (!checkBoxShowGrid.IsHandleCreated)
                return;

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

        private void textBoxSkylineBorderWidth_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBoxSkylineBorderWidth.Text, out float f))
            {
                Settings.SkylineBorderWidth = f;
                ParentSettings.SkylineBorderWidth = f;
                ParentForm.OptionsUpdated();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxXAxisWidth_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBoxXAxisWidth.Text, out float f))
            {
                Settings.XAxisWidth = f;
                ParentSettings.XAxisWidth = f;
                ParentForm.OptionsUpdated();
            }
        }

        private void textBoxYAxisWidth_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(textBoxYAxisWidth.Text, out float f))
            {
                Settings.YAxisWidth = f;
                ParentSettings.YAxisWidth = f;
                ParentForm.OptionsUpdated();
            }
        }

        private void panelXAxisColorSwatch_Click(object sender, EventArgs e)
        {
            SetColorSwatch(panelXAxisColorSwatch.BackColor,
                           panelXAxisColorSwatch,
                           textBoxXAxisColor);
        }

        private void panelYAxisColorSwatch_Click(object sender, EventArgs e)
        {
            SetColorSwatch(panelYAxisColorSwatch.BackColor,
                           panelYAxisColorSwatch,
                           textBoxYAxisColor);
        }

        private void textBoxMarginLeft_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxMarginLeft.Text, out int i))
            {
                Settings.CanvasMarginLeft = i;
                ParentSettings.CanvasMarginLeft = i;
                ParentForm.OptionsUpdated();
            }
        }

        private void textBoxMarginRight_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxMarginRight.Text, out int i))
            {
                Settings.CanvasMarginRight = i;
                ParentSettings.CanvasMarginRight = i;
                ParentForm.OptionsUpdated();
            }
        }

        private void textBoxMarginTop_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxMarginTop.Text, out int i))
            {
                Settings.CanvasMarginTop = i;
                ParentSettings.CanvasMarginTop = i;
                ParentForm.OptionsUpdated();
            }
        }

        private void textBoxMarginBottom_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxMarginBottom.Text, out int i))
            {
                Settings.CanvasMarginBottom = i;
                ParentSettings.CanvasMarginBottom = i;
                ParentForm.OptionsUpdated();
            }
        }
        #endregion

        private void checkBoxShowInfoPanel_CheckedChanged(object sender, EventArgs e)
        {
            // Note - This ensures that this method isn't run
            // during application startup
            if (!checkBoxShowInfoPanel.IsHandleCreated)
                return;

            bool flag = checkBoxShowInfoPanel.Checked;
            Settings.ShowInfoPanel = flag;
            ParentSettings.ShowInfoPanel = flag;
            ParentForm.OptionsUpdated();
        }

        private void panelSkylineFillForegroundColorSwatch_Click(object sender, EventArgs e)
        {
            SetColorSwatch(panelSkylineFillForegroundColorSwatch.BackColor,
                           panelSkylineFillForegroundColorSwatch,
                           textBoxSkylineFillForegroundColor);
        }

        private void panelSkylineFillBackgroundColorSwatch_Click(object sender, EventArgs e)
        {
            SetColorSwatch(panelSkylineFillBackgroundColorSwatch.BackColor,
                           panelSkylineFillBackgroundColorSwatch,
                           textBoxSkylineFillBackgroundColor);
        }

        private void checkBoxEnableSkylineFill_CheckedChanged(object sender, EventArgs e)
        {
            // Note - This ensures that this method isn't run
            // during application startup
            if (!checkBoxEnableSkylineFill.IsHandleCreated)
                return;

            bool flag = checkBoxEnableSkylineFill.Checked;
            EnableSkylineFillForegroundSettings(flag);
            EnableSkylineFillBackgroundSettings(flag);
            ParentForm.OptionsUpdated();
        }
    }
}
