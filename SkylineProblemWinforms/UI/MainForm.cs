using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KohdAndArt.Toolkit;
using SkylineProblemWinforms.UI;
using SkylineProblemWinforms.Utilities;

namespace SkylineProblemWinforms
{
    public partial class MainForm : Form
    {
        #region External Imports
        [DllImport(@"gdiplus.dll")]
        public static extern int GdipWindingModeOutline(HandleRef path, IntPtr matrix, float flatness);
        #endregion

        #region Public Properties
        public SkylineSettings              Settings { get; set; }
        public FormDataPoints               FormDataPoints { get; set; }
        public ChartCanvasManager           CanvasManager { get; set; }
        public IList<BuildingCoordinates>   DataList { get; set; }
        public DataManager                  DataManager { get; set; }
        public InfoPanel                    InfoPanel { get; set; }
        #endregion

        #region Private Variables
        // Setup our pens and brushes
        Pen[] _pens = new Pen[] {
                new Pen(Color.LightSeaGreen, 1f),
                new Pen(Color.Blue, 1f),
                new Pen(Color.Red, 1f),
                new Pen(Color.Green, 1f),
                new Pen(Color.CadetBlue, 1f),
                new Pen(Color.LightCoral, 1f),
                new Pen(Color.Gray, 1f),
                new Pen(Color.Yellow, 1f),
                new Pen(Color.Orange, 1f),
                new Pen(Color.Purple, 1f),
            };

        Point _centerPoint = new Point();

        float _canvasZoomFactor = 1.0f;
        bool _ctrlPressed = false;
        #endregion

        #region Constructor(s)
        public MainForm()
        {
            LoadConfigurationSettings();

            InitializeComponent();

            // Use double buffering to reduce flicker.
            this.SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
            this.UpdateStyles();

            // Create the canvas manager
            CanvasManager = new ChartCanvasManager(panelCanvas.Width, panelCanvas.Height);

            // Create the Data Manager and Load the data
            DataManager = new DataManager(Settings.DefaultDataFile);

            // Create and optionally display the data window
            FormDataPoints = new FormDataPoints(this);
            FormDataPoints.SetData(DataManager.Data);
            if (Settings.ShowDataPointWindow)
            {
                FormDataPoints.Show();
            }

            // Create and optionally display the info panel
            InfoPanel = new InfoPanel(this);
            if (Settings.ShowInfoPanel)
            {
                InfoPanel.Show();
            }


            UpdateConfigurationSettingsUI();

            buttonToggleData.BackColor = Color.FromArgb(100, 0, 0, 0);
            buttonToggleData.ForeColor = Color.FromArgb(100, 255, 0, 0);

            // Ensure that the primary canvas doesn't flicker when refreshed
            panelCanvas.SetDoubleBuffered();

            // Setup keyboard handler
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(MainForm_KeyDown);

            infoPanelToolStripMenuItem.Checked = Settings.ShowInfoPanel;
        }
        #endregion

        #region Private Methods
        private void UpdateConfigurationSettingsUI()
        {
            labelDefaultDataFile.Text = Settings.DefaultDataFile;
        }

        private void LoadConfigurationSettings()
        {
            Settings = SkylineSettings.GetInstance();
        }

        private void ReloadData()
        {
            DataManager.Filename = Settings.DefaultDataFile;
            FormDataPoints.SetData(DataManager.Data);
        }

        private void DrawRawData()
        {
            panelCanvas.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReinitializeValues()
        {
            var w = panelCanvas.Width;
            var h = panelCanvas.Height;
            _centerPoint.X = w / 2;
            _centerPoint.Y = h / 2;
            CanvasManager?.SetCanvasDimensions(w, h);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReinitializeWindow()
        {
            ReinitializeValues();
            panelCanvas.Invalidate();
        }
        #endregion

        #region Event Handlers
        private void panelCanvas_Paint1(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            CanvasManager.Graphics = g;

            // Set the canvas background color
            panelCanvas.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.CanvasBackgroundColor);


            // Renable this later!

            // Get rid of jaggy graphics
            g.SmoothingMode = SmoothingMode.HighQuality;

            //// Set the coordinate transformation (Flip the Y Axis)
            ////CanvasManager.TransformCanvas();
            CanvasManager.FlipYAxis();

            //// Set the Zoom factor
            CanvasManager.Zoom(_canvasZoomFactor);

            //// Move the origin a bit to the right and up so
            //// we have a bit more space
            g.TranslateTransform(Settings.CanvasMarginLeft,
                                    Settings.CanvasMarginBottom);




            // Ensure the canvas dimensions are correct and optionally
            // display the X and/or Y Axis.
            //CanvasManager.SetCanvasDimensions(panelCanvas.Width, panelCanvas.Height);
            CanvasManager.ShowXAxis = Settings.ShowXAxis;
            CanvasManager.ShowYAxis = Settings.ShowYAxis;
            CanvasManager.XAxisColor = ColorUtilities.GetColorFromHexRGBString(Settings.XAxisColor);
            CanvasManager.YAxisColor = ColorUtilities.GetColorFromHexRGBString(Settings.YAxisColor);
            CanvasManager.XAxisWidth = Settings.XAxisWidth;
            CanvasManager.YAxisWidth = Settings.YAxisWidth;
            CanvasManager.RenderXAndYAxis();

            // Show the optional grid
            CanvasManager.ShowGrid = Settings.ShowGrid;
            CanvasManager.GridColor = ColorUtilities.GetColorFromHexRGBString(Settings.GridColor);
            CanvasManager.RenderGrid();

            GraphicsPath gpUnscaledData = new GraphicsPath();
            GraphicsPath gpScaledData = new GraphicsPath();

            var penIndex = 0;
            foreach (BuildingCoordinates buildingData in DataManager.Data)
            {
                // Scale the data to fit nicely in the panel
                BuildingCoordinates a = buildingData.GetScaledCoordinates(CanvasManager.Width,
                                                            CanvasManager.Height,
                                                            DataManager.MaximumX,
                                                            DataManager.MaximumY);

                // Get the data
                var l = a.Left;
                var h = a.Height;
                var r = a.Right;
                var w = a.Width;

                Rectangle rect1 = buildingData.GetScaledRectangle(CanvasManager.Width,
                                                                    CanvasManager.Height,
                                                                    DataManager.MaximumX,
                                                                    DataManager.MaximumY);

                gpScaledData.AddRectangle(rect1);

                HandleRef handle = new HandleRef(gpScaledData,
                                                    (IntPtr)gpScaledData.
                                                    GetType().
                                                    GetField("nativePath", BindingFlags.NonPublic |
                                                                            BindingFlags.Instance).
                                                    GetValue(gpScaledData));

                GdipWindingModeOutline(handle, IntPtr.Zero, 0.25F);


                if (!Settings.HighlightSkyline)
                {
                    // Draw the building outline rectangle
                    g.DrawRectangle(_pens[penIndex], l, 0, w, h);
                }

                // Get the next pen in the list of pens
                // for the next building rectangle
                penIndex++;
                if (penIndex >= _pens.Length)
                {
                    penIndex = 0;
                }
            }


            PointF[] pathPoints = gpScaledData.PathData.Points;


            if (Settings.HighlightSkyline)
            {
                var color = ColorUtilities.GetColorFromHexRGBString(Settings.SkylineBorderColor);
                var penWidth = Settings.SkylineBorderWidth;

                using (var p = new Pen(color, penWidth))
                {
                    g.DrawPath(p, gpScaledData);
                }


                using (var brush = new HatchBrush(HatchStyle.LargeGrid,
                                                Color.FromArgb(20, 255, 255, 255),
                                                Color.FromArgb(10, 255, 255, 255)))
                {
                    g.FillPath(brush, gpScaledData);
                }

            }

            if (Settings.ShowDataCoordinates)
            {
                g.ResetTransform();
                var drawString = "[10,15]";

                using (var drawFont = new System.Drawing.Font("Arial", 8))
                using (var drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
                {
                    var x = 150.0F;
                    var y = 0F;
                    var drawFormat = new System.Drawing.StringFormat();
                    g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                }
            }

            //PointF[] pathPoints = gpScaledData.PathData.Points;
        }




        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            CanvasManager.Graphics = g;

            // Set the canvas background color
            panelCanvas.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.CanvasBackgroundColor);

            // Ensure the canvas dimensions are correct and optionally
            // display the X and/or Y Axis.
            //CanvasManager.SetCanvasDimensions(panelCanvas.Width, panelCanvas.Height);
            CanvasManager.ShowXAxis = Settings.ShowXAxis;
            CanvasManager.ShowYAxis = Settings.ShowYAxis;
            CanvasManager.XAxisColor = ColorUtilities.GetColorFromHexRGBString(Settings.XAxisColor);
            CanvasManager.YAxisColor = ColorUtilities.GetColorFromHexRGBString(Settings.YAxisColor);
            CanvasManager.XAxisWidth = Settings.XAxisWidth;
            CanvasManager.YAxisWidth = Settings.YAxisWidth;
            CanvasManager.RenderXAndYAxis();

            // Show the optional grid
            CanvasManager.ShowGrid = Settings.ShowGrid;
            CanvasManager.GridColor = ColorUtilities.GetColorFromHexRGBString(Settings.GridColor);
            CanvasManager.RenderGrid();

            GraphicsPath gpUnscaledData = new GraphicsPath();
            GraphicsPath gpScaledData = new GraphicsPath();

            float scaleAspect = Math.Min(CanvasManager.Width / DataManager.MaximumX, 
                                         CanvasManager.Height / DataManager.MaximumY);


            //PointF[] allPoints = new PointF[DataManager.Data.Count * 4];
            List<PointF> allPointsList = new List<PointF>();


            var penIndex = 0;
            foreach (BuildingCoordinates buildingData in DataManager.Data)
            {
                // Scale the data to fit nicely in the panel
                BuildingCoordinates a = buildingData.GetScaledCoordinates(CanvasManager.Width,
                                                            CanvasManager.Height,
                                                            DataManager.MaximumX,
                                                            DataManager.MaximumY);

                // Get the data
                //var l = a.Left;
                //var h = a.Height;
                //var r = a.Right;
                //var w = a.Width;

                var l = buildingData.Left;
                var h = buildingData.Height;
                var r = buildingData.Right;
                var w = buildingData.Width;

                //Rectangle rect1 = buildingData.GetScaledRectangle(CanvasManager.Width,
                //                                                    CanvasManager.Height,
                //                                                    DataManager.MaximumX,
                //                                                    DataManager.MaximumY);

                allPointsList.Add(new PointF(l, 0f));
                allPointsList.Add(new PointF(l, h));
                allPointsList.Add(new PointF(r, h));
                allPointsList.Add(new PointF(r, 0f));


                //Rectangle rect1 = new Rectangle(l, 0, w, h);
                //gpScaledData.AddRectangle(rect1);
                //HandleRef handle = new HandleRef(gpScaledData,
                //                                    (IntPtr)gpScaledData.
                //                                    GetType().
                //                                    GetField("nativePath", BindingFlags.NonPublic |
                //                                                            BindingFlags.Instance).
                //                                    GetValue(gpScaledData));

                //GdipWindingModeOutline(handle, IntPtr.Zero, 0.25F);

                //if (!Settings.HighlightSkyline)
                //{
                //    // Draw the building outline rectangle
                //    g.DrawRectangle(_pens[penIndex], l, 0, w, h);
                //}

                //// Get the next pen in the list of pens
                //// for the next building rectangle
                //penIndex++;
                //if (penIndex >= _pens.Length)
                //{
                //    penIndex = 0;
                //}
            }


            //Matrix matrix = new Matrix();
            //matrix.Scale(scaleAspect, -scaleAspect);
            //matrix.Translate(DataManager.MaximumX / 2, -DataManager.MaximumY / 2);

            PointF[] points = allPointsList.ToArray();
            //matrix.TransformPoints(points);

            RectangleF[] rectangles = ConvertPointsToRectangles(points);


            g.TranslateTransform(Settings.CanvasMarginLeft, Settings.CanvasMarginBottom);
            g.DrawRectangles(Pens.Red, rectangles);

            //g.DrawLines(Pens.Green, points);

            //if (Settings.HighlightSkyline)
            //{
            //    var color = ColorUtilities.GetColorFromHexRGBString(Settings.SkylineBorderColor);
            //    var penWidth = Settings.SkylineBorderWidth;

            //    using (var p = new Pen(color, penWidth))
            //    {
            //        g.DrawPath(p, gpScaledData);
            //    }


            //    using (var brush = new HatchBrush(HatchStyle.LargeGrid,
            //                                    Color.FromArgb(20, 255, 255, 255),
            //                                    Color.FromArgb(10, 255, 255, 255)))
            //    {
            //        g.FillPath(brush, gpScaledData);
            //    }

            //}

            //if (Settings.ShowDataCoordinates)
            //{
            //    g.ResetTransform();
            //    var drawString = "[10,15]";

            //    using (var drawFont = new System.Drawing.Font("Arial", 8))
            //    using (var drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
            //    {
            //        var x = 150.0F;
            //        var y = 0F;
            //        var drawFormat = new System.Drawing.StringFormat();
            //        g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            //    }
            //}

            //PointF[] pathPoints = gpScaledData.PathData.Points;
        }


        private RectangleF[] ConvertPointsToRectangles(PointF[] points)
        {
            List<RectangleF> rects = new List<RectangleF>();

            for(int i = 0; i<points.Length; i+=4)
            {
                PointF upperLeft = points[i + 1];
                float width = points[i + 3].X - points[i].X;
                float height = upperLeft.Y - points[i].Y;
                RectangleF r = new RectangleF(upperLeft, new SizeF(width, Math.Abs(height)));
                rects.Add(r);
            }

            return rects.ToArray();
        }










        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            ReinitializeWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCanvas_Resize(object sender, EventArgs e)
        {
            ReinitializeWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOptions_Click(object sender, EventArgs e)
        {
            new FormManageSkylineSettings(this, Settings).ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormManageSkylineSettings(this, Settings).ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Skyline Dataset File...";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                if (filename.Length > 0 && File.Exists(filename))
                {
                    labelDefaultDataFile.Text = filename;
                    Settings.DefaultDataFile = filename;
                    OptionsUpdated();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonToggleData_Click(object sender, EventArgs e)
        {
            if (Settings.ShowDataPointWindow == true)
            {
                FormDataPoints.Hide();
                Settings.ShowDataPointWindow = false;
            }
            else
            {
                FormDataPoints.Show();
                Settings.ShowDataPointWindow = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (Settings.ShowInfoPanel)
            {
                InfoPanel.SetMouseCoordinates(e.Location);
            }
            //if (!Settings.ShowMouseCoordinates)
            //{
            //    labelMouseCoordinates.Text = string.Empty;
            //    return;
            //}

            //// Get mouse coordinates
            //var x = e.X - Settings.CanvasMarginLeft;
            //var y = e.Y;

            //// We have to flip the Y-Axis to match the actual chart
            //var height = panelCanvas.Height;
            //y = height - y;

            //y -= Settings.CanvasMarginBottom;

            //// Detect if the mouse coordinates are 'out of bounds'
            //string text = string.Empty;
            //if (x >=0 && y >= 0)
            //{
            //    text = $"{x} : {y}";
            //}
            //labelMouseCoordinates.Text = text;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            float minZoom = 0.1f;
            float maxZoom = 10.0f;
            float zoomIncrement = 0.1f;
            if (_ctrlPressed == true)
            {
                bool increasing = (e.Delta > 0) ? true : false;
                if (increasing)
                    _canvasZoomFactor += zoomIncrement;
                else
                    _canvasZoomFactor -= zoomIncrement;

                if (_canvasZoomFactor <= 0f)
                    _canvasZoomFactor = minZoom;

                if (_canvasZoomFactor > maxZoom)
                    _canvasZoomFactor = maxZoom;

                ReinitializeWindow();
            }

            float z = _canvasZoomFactor * 100;
            InfoPanel.SetZoomLevel(z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        public void OptionsUpdated()
        {
            UpdateConfigurationSettingsUI();
            //UpdateInfoPanelVisiblity();

            ReloadData();
            DrawRawData();
            ReinitializeWindow();

            // Show/Hide the Data Points Window
            if (Settings.ShowDataPointWindow) { FormDataPoints.Show(); }
            else { FormDataPoints.Hide(); }

            // Show/Hide the Info Panel
            if (Settings.ShowInfoPanel) { InfoPanel.Show(); }
            else { InfoPanel.Hide(); }
        }
        #endregion


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            _ctrlPressed = e.Control;
        }


        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            _ctrlPressed = e.Control;
        }

        private void infoPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the current Info Panel visibility status
            var isVisible = InfoPanel.Visible;

            // Flip Status
            isVisible = !isVisible;

            // Show/Hide the panel
            InfoPanel.Visible = isVisible;

            // Update Settings property
            Settings.ShowInfoPanel = isVisible;

            // Update the menuitem checkbox
            infoPanelToolStripMenuItem.Checked = isVisible;
        }

        private void UpdateInfoPanelVisiblity()
        {
            // Get the current Info Panel visibility status
            var isVisible = InfoPanel.Visible;

            // Flip Status
            isVisible = !isVisible;

            // Show/Hide the panel
            InfoPanel.Visible = isVisible;

            // Update Settings property
            Settings.ShowInfoPanel = isVisible;

            // Update the menuitem checkbox
            //infoPanelToolStripMenuItem.Checked = isVisible;
        }

    }
}
