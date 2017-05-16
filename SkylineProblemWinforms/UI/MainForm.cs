using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KohdAndArt.Toolkit;
using MetroFramework.Forms;
using SkylineProblemWinforms.UI;
using SkylineProblemWinforms.Utilities;

namespace SkylineProblemWinforms
{
    public partial class MainForm : MetroForm
    {
        #region External Imports
        [DllImport(@"gdiplus.dll")]
        public static extern int GdipWindingModeOutline(HandleRef path, IntPtr matrix, float flatness);
        #endregion

        #region Public Properties
        public SkylineSettings              Settings { get; set; }
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
            Initialize();
            //TestBuildGridRectangle();
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            try
            {
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

                // Create and optionally display the info panel
                InfoPanel = new InfoPanel(this);
                InfoPanel.SetData(DataManager.Data);
                if (Settings.ShowInfoPanel)
                {
                    InfoPanel.Show();
                }

                UpdateConfigurationSettingsUI();

                // Ensure that the primary canvas doesn't flicker when refreshed
                panelCanvas.SetDoubleBuffered();

                // Setup keyboard handler
                this.KeyPreview = true;
                this.KeyDown += new KeyEventHandler(MainForm_KeyDown);

                // Menu Item States
                InitializeMenuItemStates();

                var g = panelCanvas.CreateGraphics();
                var matrix = new Matrix(1, 0, 0, -1, 0, 0);
                g.Transform = matrix;
                g.TranslateTransform(0, -panelCanvas.Height);

                menuStrip1.Renderer = new ToolStripProfessionalRenderer(new TestColorTable());
            }
            catch (Exception e)
            {
                throw new ApplicationException("Oops.  Something very very bad has happened :)");
            }
        }

        private void InitializeMenuItemStates()
        {
            menuItemSkylineBorder.Checked = Settings.HighlightSkyline;
            menuItemSkylineFill.Checked = Settings.SkylineFillFlag; 
            menuItemSkylineFill.Enabled = Settings.HighlightSkyline;
            menuItemGrid.Checked = Settings.ShowGrid;
            menuItemXAxis.Checked = Settings.ShowXAxis;
            menuItemYAxis.Checked = Settings.ShowYAxis;
            menuItemShowDataPoints.Checked = Settings.ShowDataCoordinates;

            metroToggleInfoPanel.Checked = Settings.ShowInfoPanel;
        }

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
            InfoPanel.SetData(DataManager.Data);
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

            //DrawCheckerboard(g);
            //return;

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


        private void DrawCheckerboard(Graphics g)
        {
            Color backgroundColor = Color.FromArgb(200, 50, 50, 50);
            Color borderColor = Color.FromArgb(60, 255, 255, 255);
            Color textColor = Color.FromArgb(100, 255, 255, 255);
            float borderWidth = 1.0f;

            //g.Clear(backgroundColor);

            IEnumerable<Rectangle> list = BuildGridRectangles(_canvasWidth: panelCanvas.Width,
                                                       _canvasHeight: panelCanvas.Height,
                                                       _blockCountHorizontal: 8,
                                                       _blockCountVertical: 8,
                                                       _gapWidth: 20,
                                                       _gapHeight: 20);


            var stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            int temp = 0;
            using (var p = new Pen(borderColor, borderWidth))
            {
                foreach (var rect in list)
                {
                    g.DrawRectangle(p, rect);
                    if (temp % 2 == 0)
                    {
                        g.FillRectangle(new SolidBrush(borderColor), rect);
                    }
                    temp++;

                    string coordinateText = $"{rect.X} x {rect.Y}";
                    g.DrawString(coordinateText, 
                                 new Font(FontFamily.GenericSansSerif, 8.0f, FontStyle.Regular), new SolidBrush(textColor), rect, stringFormat);
                }
            }
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

            //GraphicsState state = gr.Save();

            //gr.ResetTransform();
            //gr.ScaleTransform(10.0f, 10.0f, MatrixOrder.Prepend);
            //gr.TranslateTransform(-1.0f, -10f, MatrixOrder.Prepend);


            using (var drawFont = new System.Drawing.Font("Arial", fontSize))
            using (var drawBrush = new System.Drawing.SolidBrush(textColor))
            {
                bool nextOneIsTerminator = false;

                int arrayLength = pathData.Points.Length;
                for (int i = arrayLength - 1; i >= 0; i--)
                {
                    var x = pathData.Points[i].X;
                    var y = pathData.Points[i].Y;

                    // Since were enumerating the array backwards,
                    // we want to see if the next Y value is zero.
                    // If the next Y value is zero, then we can stop
                    // rendering values (after we complete the current one)
                    if (y == 0)
                    {
                        nextOneIsTerminator = true;
                    }

                    //var nextIndex = i - 1;
                    //if (nextIndex >= 0)
                    //{
                    //    if (pathData.Points[nextIndex].Y == 0)
                    //    {
                    //        nextOneIsTerminator = true;
                    //    }
                    //}

                    var y2 = y;
                    var drawString = $"{x}x{y}";
                    var drawFormat = new System.Drawing.StringFormat();
                    //gr.DrawString(drawString, drawFont, drawBrush, x, y2, drawFormat);

                    var rect = new RectangleF(new PointF(x, y2), new Size(2, 4));
                    var temp = new RectangleWithText(rect, drawString);
                    temp.Draw(gr);

                    if (nextOneIsTerminator == true)
                    {
                        break;
                    }
                }
            }

            //gr.Restore(state);
        }

        private void WritePathDataToFile(PathData data)
        {
            // Build an output filename based on the source filename
            FileInfo fi = new FileInfo(Settings.DefaultDataFile);
            string targetFilename = $"outputdata_{fi.Name}";

            // Sort the data array 
            List<PointF> pointList = new List<PointF>();
            pointList.AddRange(data.Points);
            pointList = pointList.OrderBy(p => p.X).ToList();
            PointF[] dataPoints = pointList.ToArray<PointF>();

            // Save the path data points to a file
            using (var sw = File.CreateText(targetFilename))
            {
                for (int j = 0; j < dataPoints.Length; ++j)
                {
                    sw.WriteLine(BuildLine(dataPoints[j]));
                }
                sw.Close();
            }

            // Only added this to demonstrate use of inner methods
            // In this particular instance it isn't really necessary :)
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

        private void metroToggleInfoPanel_CheckedChanged(object sender, EventArgs e)
        {
            var isChecked = metroToggleInfoPanel.Checked;
            if (isChecked == true) { InfoPanel.Show(); }
            else { InfoPanel.Hide(); }
            Settings.ShowInfoPanel = isChecked;
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

        private void menuItemSkylineBorder_Click(object sender, EventArgs e)
        {
            var flag = menuItemSkylineBorder.Checked = !menuItemSkylineBorder.Checked;
            Settings.HighlightSkyline = flag;

            // Need to ensure that the Skyline Fill flag is disabled if border is turned off
            menuItemSkylineFill.Enabled = flag;

            OptionsUpdated();
        }

        private void menuItemSkylineFill_Click(object sender, EventArgs e)
        {
            var flag = menuItemSkylineFill.Checked = !menuItemSkylineFill.Checked;
            Settings.SkylineFillFlag = flag;
            OptionsUpdated();
        }

        private void menuItemGrid_Click(object sender, EventArgs e)
        {
            var flag = menuItemGrid.Checked = !menuItemGrid.Checked;
            Settings.ShowGrid = flag;
            OptionsUpdated();
        }

        private void menuItemXAxis_Click(object sender, EventArgs e)
        {
            var flag = menuItemXAxis.Checked = !menuItemXAxis.Checked;
            Settings.ShowXAxis = flag;
            OptionsUpdated();
        }

        private void menuItemYAxis_Click(object sender, EventArgs e)
        {
            var flag = menuItemYAxis.Checked = !menuItemYAxis.Checked;
            Settings.ShowYAxis = flag;
            OptionsUpdated();
        }

        private void menuItemShowDataPoints_Click(object sender, EventArgs e)
        {
            var flag = menuItemShowDataPoints.Checked = !menuItemShowDataPoints.Checked;
            Settings.ShowDataCoordinates = flag;
            OptionsUpdated();
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

            // Show/Hide the Info Panel
            if (Settings.ShowInfoPanel) { InfoPanel.Show(); }
            else { InfoPanel.Hide(); }
        }
        #endregion

        private void TestBuildGridRectangle()
        {
            List<Rectangle> list = BuildGridRectangles(_canvasWidth:             panelCanvas.Width,
                                                       _canvasHeight:            panelCanvas.Height,
                                                       _blockCountHorizontal:    6,
                                                       _blockCountVertical:      5,
                                                       _gapWidth:                20,
                                                       _gapHeight:               20);
        }

        private List<Rectangle> BuildGridRectangles(int _canvasWidth,
                                                    int _canvasHeight,
                                                    int _blockCountHorizontal,
                                                    int _blockCountVertical,
                                                    int _gapWidth = 0,
                                                    int _gapHeight = 0,
                                                    int _blockWidth = 0, 
                                                    int _blockHeight = 0)
        {
            List<Rectangle> rectList = new List<Rectangle>();

            int blockWidth = (_canvasWidth - (_blockCountHorizontal + 1) * _gapWidth) / _blockCountHorizontal;
            int blockHeight = (_canvasHeight - (_blockCountVertical + 1) * _gapHeight) / _blockCountVertical;

            for (int x = 0; x < _blockCountHorizontal; x++)
            {
                for (int y = 0; y < _blockCountVertical; y++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = blockWidth;
                    r.Height = blockHeight;
                    r.X = ((x+1) * _gapWidth) + (x * blockWidth);
                    r.Y = ((y+1) * _gapHeight) + (y * blockHeight);
                    rectList.Add(r);
                }
            }

            return rectList;
        }
    }

    class RectangleWithText
    {
        RectangleF m_extent = new RectangleF();
        string m_text = "";

        Font m_textFont = null;
        RectangleF m_textRect = new RectangleF();

        public RectangleWithText(RectangleF extent, string text)
        {
            m_extent = extent;
            m_text = text;
        }

        public void Draw(Graphics g)
        {
            //var dashedGrayPen = new Pen(Color.Gray, 1.0f / g.DpiX) { DashStyle = DashStyle.Dash };
            //var brownPen = new Pen(Color.White, 1.0f / g.DpiX);

            //// Draw rectangle itself
            //g.DrawRectangle(dashedGrayPen, m_extent.X, m_extent.Y, m_extent.Width, m_extent.Height);

            // Draw text on it
            var extentCenter = new PointF((m_extent.Left + m_extent.Right) / 2, (m_extent.Bottom + m_extent.Top) / 2);
            DrawText(g, m_text, extentCenter, m_extent);
        }

        private void DrawText(Graphics g, string text, PointF ptStart, RectangleF extent)
        {
            var gs = g.Save();

            // Inverse Y axis again - now it grow down;
            // if we don't do this, text will be drawn inverted
            g.ScaleTransform(1.0f, -1.0f, MatrixOrder.Prepend);

            if (m_textFont == null)
            {
                //m_textFont = new Font("Arial", 150.0f/g.DpiX, FontStyle.Regular, GraphicsUnit.Pixel);


                // Find the maximum appropriate text size to fix the extent
                float fontSize = 100.0f;
                Font fnt;
                SizeF textSize;
                do
                {
                    fnt = new Font("Arial", fontSize / g.DpiX, FontStyle.Regular, GraphicsUnit.Pixel);
                    textSize = g.MeasureString(text, fnt);
                    m_textRect = new RectangleF(new PointF(ptStart.X - textSize.Width / 2.0f, -ptStart.Y - textSize.Height / 2.0f), textSize);

                    var textRectInv = new RectangleF(m_textRect.X, -m_textRect.Y, m_textRect.Width, m_textRect.Height);
                    if (extent.Contains(textRectInv))
                        break;

                    fontSize -= 1.0f;
                    if (fontSize <= 0)
                    {
                        fontSize = 1.0f;
                        break;
                    }
                } while (true);

                m_textFont = fnt;
            }

            // Create a StringFormat object with the each line of text, and the block of text centered on the page
            var stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            //m_textFont = new Font(FontFamily.GenericSansSerif, 2.0f, FontStyle.Regular);
            g.DrawString(text, m_textFont, Brushes.White, m_textRect, stringFormat);

            g.Restore(gs);
        }
    }
}
