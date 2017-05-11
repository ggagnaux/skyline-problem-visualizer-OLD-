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
    public partial class MainForm : MetroFramework.Forms.MetroForm
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
        static float penWidth = 0;
        Pen[] _pens = new Pen[] {
                new Pen(Color.LightSeaGreen, penWidth),
                new Pen(Color.Blue, penWidth),
                new Pen(Color.Red, penWidth),
                new Pen(Color.Green, penWidth),
                new Pen(Color.CadetBlue, penWidth),
                new Pen(Color.LightCoral, penWidth),
                new Pen(Color.Gray, penWidth),
                new Pen(Color.Yellow, penWidth),
                new Pen(Color.Orange, penWidth),
                new Pen(Color.Purple, penWidth),
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

            var g = panelCanvas.CreateGraphics();
            var matrix = new Matrix(1, 0, 0, -1, 0, 0);
            g.Transform = matrix;
            g.TranslateTransform(0, -panelCanvas.Height);
        }
        #endregion

        #region Private Methods
        private void UpdateConfigurationSettingsUI()
        {
            labelDefaultDataFile.Text = new FileInfo(Settings.DefaultDataFile).Name;
        }

        private void LoadConfigurationSettings()
        {
            Settings = SkylineSettings.GetInstance();
        }

        private void UpdateInfoPanelVisiblity()
        {
            // Get the current Info Panel visibility status
            var isVisible = !InfoPanel.Visible;
            InfoPanel.Visible = isVisible;
            Settings.ShowInfoPanel = isVisible;
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

        private void ReinitializeValues()
        {
            var w = panelCanvas.Width;
            var h = panelCanvas.Height;
            _centerPoint.X = w / 2;
            _centerPoint.Y = h / 2;
            CanvasManager?.SetCanvasDimensions(w, h);
        }

        private void ReinitializeWindow()
        {
            ReinitializeValues();
            panelCanvas.Invalidate();
        }
        #endregion

        #region Event Handlers
        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            var allPointsList = new List<PointF>();
            var graphicsPath = new GraphicsPath();
            var penIndex = 0;
            var g = e.Graphics;


            g.SmoothingMode = SmoothingMode.AntiAlias;
            CanvasManager.Graphics = g;

            // Set the canvas background color
            panelCanvas.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.CanvasBackgroundColor);

            // Set up the coordinate mapping
            MapRectangles(g,
                          0, DataManager.MaximumX, 0, DataManager.MaximumY, // World Coordinates
                          0 + Settings.CanvasMarginLeft,                    // Device Coordinates
                          CanvasManager.Width - Settings.CanvasMarginRight,
                          0 + Settings.CanvasMarginTop,
                          CanvasManager.Height - Settings.CanvasMarginBottom,
                          false);

            // Set the Zoom factor
            CanvasManager.Zoom(_canvasZoomFactor);

            foreach (BuildingCoordinates buildingData in DataManager.Data)
            {
                var left = buildingData.Left;
                var height = buildingData.Height;
                var right = buildingData.Right;
                var width = buildingData.Width;
                var bottom = 0;

                graphicsPath.AddRectangle(new Rectangle(left, bottom, width, height));

                HandleRef handle = new HandleRef(graphicsPath,
                                                    (IntPtr)graphicsPath.
                                                    GetType().
                                                    GetField("nativePath", BindingFlags.NonPublic |
                                                                            BindingFlags.Instance).
                                                    GetValue(graphicsPath));

                GdipWindingModeOutline(handle, IntPtr.Zero, 0.25F);

                if (!Settings.HighlightSkyline)
                {
                    // Draw the building outline rectangle
                    DrawBuildingOutline(g, _pens[penIndex], left, bottom, right, height);
                }

                penIndex = GetNextPenArrayIndex(penIndex, _pens.Length);
            }

            if (Settings.HighlightSkyline)
            {
                DrawSkyline(g, graphicsPath, 
                            Settings.SkylineBorderWidth, 
                            Settings.SkylineBorderColor, 
                            Settings.SkylineFillFlag, 
                            Settings.SkylineFillForegroundColor,
                            Settings.SkylineFillBackgroundColor);
            }

            if (Settings.ShowDataCoordinates)
            {
                //g.ResetTransform();

                // Set up the coordinate mapping
                //MapRectangles(g,
                //              0, DataManager.MaximumX, 0, DataManager.MaximumY, // World Coordinates
                //              0 + Settings.CanvasMarginLeft,                    // Device Coordinates
                //              CanvasManager.Width - Settings.CanvasMarginRight,
                //              0 + Settings.CanvasMarginTop,
                //              CanvasManager.Height - Settings.CanvasMarginBottom,
                //              false);

                DrawDataCoordinates(g, graphicsPath.PathData);
            }

            // Save the path data points to a file
            WritePathDataToFile(graphicsPath.PathData);

            // Clear out all of the transforms on the graphics object
            g.ResetTransform();

            // Ensure the canvas dimensions are correct and optionally
            // display the X and/ or Y Axis.
            DrawAxis();

            // Render the grid
            DrawGrid();
        }

        private int GetNextPenArrayIndex(int currentIndex, int arrayLength)
        {
            var newIndex = ++currentIndex;
            if (newIndex >= arrayLength)
            {
                newIndex = 0;
            }
            return newIndex;
        }

        private void DrawBuildingOutline(Graphics gr, Pen pen, int left, int bottom, int right, int height)
        {
            gr.DrawLine(pen, left, bottom, left, height);
            gr.DrawLine(pen, left, height, right, height);
            gr.DrawLine(pen, right, height, right, bottom);
        }

        private void DrawAxis()
        {
            CanvasManager.SetCanvasDimensions(panelCanvas.Width, panelCanvas.Height);
            CanvasManager.SetMargins(Settings.CanvasMarginLeft,
                                     Settings.CanvasMarginRight,
                                     Settings.CanvasMarginRight,
                                     Settings.CanvasMarginBottom);
            CanvasManager.ShowXAxis = Settings.ShowXAxis;
            CanvasManager.ShowYAxis = Settings.ShowYAxis;
            CanvasManager.XAxisColor = ColorUtilities.GetColorFromHexRGBString(Settings.XAxisColor);
            CanvasManager.YAxisColor = ColorUtilities.GetColorFromHexRGBString(Settings.YAxisColor);
            CanvasManager.XAxisWidth = Settings.XAxisWidth;
            CanvasManager.YAxisWidth = Settings.YAxisWidth;
            CanvasManager.RenderXAndYAxis();
        }

        private void DrawGrid()
        {
            CanvasManager.ShowGrid = Settings.ShowGrid;
            CanvasManager.GridColor = ColorUtilities.GetColorFromHexRGBString(Settings.GridColor);
            CanvasManager.GridOpacity = 15;
            CanvasManager.GridPenWidth = 0;
            CanvasManager.GridSpacingHorizontal = 30;
            CanvasManager.GridSpacingVertical = 30;
            CanvasManager.RenderGrid();
        }

        private void DrawSkyline(Graphics gr, 
                                 GraphicsPath graphicsPath, 
                                 float _penWidth = 1.0f, 
                                 string _borderColor = "FF0000", 
                                 bool _fillIn = true, 
                                 string _fillForegroundColor = "FFFFFF",
                                 string _fillBackgroundColor = "FFFFFF")
        {
            // Draw the border
            DrawSkylineBorder(gr, graphicsPath, _penWidth, _borderColor);

            // Fill it in
            if (_fillIn)
            {
                FillSkyline(gr, graphicsPath, _fillForegroundColor, _fillBackgroundColor);
            }
        }

        private void DrawSkylineBorder(Graphics gr,
                                       GraphicsPath graphicsPath,
                                       float _penWidth = 1.0f,
                                       string _borderColor = "FF0000")
        {
            var borderColor = ColorUtilities.GetColorFromHexRGBString(_borderColor);
            using (var p = new Pen(borderColor, _penWidth))
            {
                // Ensure that the pen doesn't scale upward in size
                p.ScaleTransform(.1f, .1f);
                gr.DrawPath(p, graphicsPath);
            }
        }

        private void FillSkyline(Graphics gr, 
                                 GraphicsPath graphicsPath, 
                                 string _fillForegroundColor, 
                                 string _fillBackgroundColor)
        {
            ColorUtilities.ConvertRGBHexStringToBase10(_fillForegroundColor, out byte rF, out byte gF, out byte bF);
            ColorUtilities.ConvertRGBHexStringToBase10(_fillBackgroundColor, out byte rB, out byte gB, out byte bB);
            var foreColor = Color.FromArgb(50, rF, gF, bF);
            var backColor = Color.FromArgb(40, rB, gB, bB);

            // TODO - Put HatchStyle somewhere else as a constant (or make it user configurable)
            HatchStyle style = HatchStyle.SmallCheckerBoard;
            using (var brush = new HatchBrush(style, foreColor, backColor))
            {
                gr.FillPath(brush, graphicsPath);
            }
        }

        private void DrawDataCoordinates(Graphics gr, PathData pathData)
        {
            Color textColor = Color.White;
            var fontSize = 1.0f;

            GraphicsState state = gr.Save();
            //gr.ScaleTransform(1.0f, -1.0f, MatrixOrder.Prepend);


            using (var drawFont = new System.Drawing.Font("Arial", fontSize))
            using (var drawBrush = new System.Drawing.SolidBrush(textColor))
            {
                int arrayLength = pathData.Points.Length;
                for (int i = arrayLength - 1; i >= 0; i--)
                {
                    var x = pathData.Points[i].X;
                    var y = pathData.Points[i].Y;
                    var y2 = y;
                    var drawString = $"{x} {y}";
                    var drawFormat = new System.Drawing.StringFormat();
                    gr.DrawString(drawString, drawFont, drawBrush, x, y2, drawFormat);
                }
            }

            gr.Restore(state);
        }

        private void WritePathDataToFile(PathData data, string filename = "pathdata.txt")
        {
            // Save the path data points to a file
            using (var sw = File.CreateText(filename))
            {
                for (int j = 0; j < data.Points.Length; ++j)
                {
                    sw.WriteLine(BuildLine(data.Points[j]));
                }
                sw.Close();
            }

            string BuildLine(PointF point)
            {
                return $"{point.X} {point.Y}";
            }
        }

        // Transform the Graphics object to 
        // world coordinates wxmin <= X <= wxmax, wymin <= Y <= wymax are mapped to 
        // device coordinates dxmin	<= X <=	dxmax, dymin <= Y <= dymax. 
        private void MapRectangles(Graphics gr,
                                   float wxmin, float wxmax, float wymin, float wymax,
                                   float dxmin, float dxmax, float dymin, float dymax,
                                   bool yAxisStartsAtTopLeft = false)
        {
            RectangleF worldRectangle;
            PointF[] devicePoints;
            PointF upperLeft;
            PointF upperRight;
            PointF lowerRight;

            // Make a world coordinate rectangle.
            worldRectangle = new RectangleF(wxmin, wymin, wxmax - wxmin, wymax - wymin);

            // Make PointF objects represeting the upper left, upper right,
            // and lower right corners of the device coordinates.

            if (yAxisStartsAtTopLeft)
            {
                // Origin = Top-Left
                upperLeft = new PointF(dxmin, dymin); 
                upperRight = new PointF(dxmax, dymin);
                lowerRight = new PointF(dxmin, dymax);
            }
            else
            {
                // Origin = Bottom-Left
                upperLeft = new PointF(dxmin, dymax);
                upperRight = new PointF(dxmax, dymax);
                lowerRight = new PointF(dxmin, dymin);
            }

            devicePoints = new PointF[] {
                upperLeft,
                upperRight,
                lowerRight
            };

            // If these two points are equal, don't do the transform
            // An exception will be thrown otherwise.
            if (upperLeft != lowerRight)
            {
                // Map the rectangle to the points.
                gr.Transform = new Matrix(worldRectangle, devicePoints);
            }
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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ReinitializeWindow();
        }

        private void panelCanvas_Resize(object sender, EventArgs e)
        {
            ReinitializeWindow();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save();

            // Clean up resources
            foreach (var p in _pens)
            {
                p.Dispose();
            }
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            new FormManageSkylineSettings(this, Settings).ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormManageSkylineSettings(this, Settings).ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Skyline Dataset File...";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                if (filename.Length > 0 && File.Exists(filename))
                {
                    var fi = new FileInfo(filename);
                    labelDefaultDataFile.Text = fi.Name;
                    Settings.DefaultDataFile = filename;
                    OptionsUpdated();
                }
            }
        }

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

        private void panelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (Settings.ShowInfoPanel)
            {
                InfoPanel.SetMouseCoordinates(e.Location);
            }
        }

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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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
            var isVisible = !InfoPanel.Visible;
            InfoPanel.Visible = isVisible;
            Settings.ShowInfoPanel = isVisible;
            infoPanelToolStripMenuItem.Checked = isVisible;
        }
        #endregion

        #region Public Methods
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
    }
}
