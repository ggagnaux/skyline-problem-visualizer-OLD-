using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkylineProblemWinforms.Utilities;
using KohdAndArt.Toolkit;

namespace SkylineProblemWinforms
{
    public partial class MainForm : Form
    {   
        public SkylineSettings              Settings { get; set; }
        public FormDataPoints               FormDataPoints { get; set; }
        public ChartCanvasManager           CanvasManager { get; set; }
        public List<BuildingCoordinates>    DataList { get; set; }
        public DataManager                  DataManager { get; set; }

        //List<BuildingCoordinates> _dataList;

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

        Pen _axisPen = new System.Drawing.Pen(Color.AliceBlue, 2.0F);
        Pen pen1 = new Pen(Color.White, 1F);
        Pen pen2 = new Pen(Color.Red, 1F);
        Brush brush1 = new SolidBrush(Color.FromArgb(99, 255, 0, 0));
        Brush brush2 = new SolidBrush(Color.FromArgb(99, 0, 0, 255));
        int _XMax = 0;
        int _YMax = 0;
        int _panelWidth, _panelHeight;
        Point _centerPoint = new Point();


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
            CanvasManager = new ChartCanvasManager(_panelWidth, _panelHeight);

            // Create the Data Manager and Load the data
            DataManager = new DataManager(Settings.DefaultDataFile);

            FormDataPoints = new FormDataPoints(this);
            if (Settings.ShowDataPointWindow)
            {
                FormDataPoints.Show();
            }

            UpdateConfigurationSettingsUI();
            LoadAndParseDataFile();
        }

        private void UpdateConfigurationSettingsUI()
        {
            textBoxFilename.Text = Settings.DefaultDataFile;
        }

        private void LoadConfigurationSettings()
        {
            Settings = SkylineSettings.GetInstance();
        }

        private void LoadAndParseDataFile()
        {
            // Load data from file and send to 
            // data points window for display 
            DataList = LoadData();
            FormDataPoints.SetData(_dataList);
        }

        private void DrawRawData()
        {
            panelCanvas.Invalidate();
        }

        //private List<BuildingCoordinates> LoadData()
        //{
        //    string filename = Settings.DefaultDataFile;
        //    //string filename = Properties.Settings.Default.DefaultDataFile;
        //    const char itemSeparator = ' ';

        //    if (filename.Length == 0 || File.Exists(filename) == false)
        //        throw new ArgumentException($"Filename not specified or file '{filename}' not found.");

        //    var line = string.Empty;
        //    var list = new List<BuildingCoordinates>();

        //    using (var file = new StreamReader(filename))
        //    {
        //        while ((line = file.ReadLine()) != null && (line.Length > 0))
        //        {
        //            string[] temp = line.Split(itemSeparator);
        //            var l = Convert.ToInt32(temp[0]);
        //            var h = Convert.ToInt32(temp[1]);
        //            var r = Convert.ToInt32(temp[2]);
        //            BuildingCoordinates d = new BuildingCoordinates(l, h, r);
        //            list.Add(d);

        //            // Determine the maximum X and Y for all of the data
        //            if (_XMax < r) { _XMax = r; }
        //            if (_YMax < h) { _YMax = h; }
        //        }
        //    }
        //    return list;
        //}

        [DllImport(@"gdiplus.dll")]
        public static extern int GdipWindingModeOutline(HandleRef path, IntPtr matrix, float flatness);

        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Set the canvas background color
            panelCanvas.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.CanvasBackgroundColor);

            // Set the coordinate transformation (Flip the Y Axis)
            TransformCanvas(g);

            // Ensure the canvas dimensions are correct and optionally
            // display the X and/or Y Axis.
            CanvasManager.SetCanvasDimensions(_panelWidth, _panelHeight);
            CanvasManager.ShowXAxis = Settings.ShowXAxis;
            CanvasManager.ShowYAxis = Settings.ShowYAxis;
            CanvasManager.RenderXAndYAxis();


            GraphicsPath gp = new GraphicsPath();
            //gp.SetMarkers();

            int penIndex = 0;
            foreach (var b in _dataList)
            {
                // Scale the data to fit nicely in the panel
                var a = b.GetScaledCoordinates(_panelWidth, _panelHeight, _XMax, _YMax);

                // Get the data
                var l = a.Left;
                var h = a.Height;
                var r = a.Right;
                var w = a.Width;

                Rectangle rect1 = b.GetScaledRectangle(_panelWidth, _panelHeight, _XMax, _YMax);

                gp.AddRectangle(rect1);

                HandleRef handle = new HandleRef(gp, (IntPtr)gp.GetType().GetField("nativePath", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(gp));
                GdipWindingModeOutline(handle, IntPtr.Zero, 0.25F);


                if (!Settings.HighlightSkyline)
                {
                    // Draw the building outline rectangle
                    g.DrawRectangle(_pens[penIndex], l, 0, w, h);
                }

                penIndex++;
                if (penIndex >= _pens.Length)
                {
                    penIndex = 0;
                }
            }

            if (Settings.HighlightSkyline)
            {
                Color skylineBorderColor = ColorUtilities.GetColorFromHexRGBString(Settings.SkylineBorderColor);

                g.DrawPath(new Pen(skylineBorderColor, 5.0F), gp);

                Brush theBrush = new HatchBrush(HatchStyle.LargeGrid, 
                                                Color.FromArgb(50, 255, 255, 255), 
                                                Color.FromArgb(10, 255, 255, 255));

                g.FillPath(theBrush, gp);
            }

            if (Settings.ShowCoordinates)
            {
                var drawString = "[10,15]";

                using (var drawFont = new System.Drawing.Font("Arial", 10))
                using (var drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
                {
                    var x = 150.0F;
                    var y = 0F;
                    var drawFormat = new System.Drawing.StringFormat();
                    g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                }



                //System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
                //System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                //float x = 150.0F;
                //float y = 50.0F;
                //System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                //g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                //drawFont.Dispose();
                //drawBrush.Dispose();
                //g.Dispose();
            }


            PointF[] pathPoints = gp.PathData.Points;
        }

        private void TransformCanvas(Graphics g)
        {
            Matrix myMatrix = new Matrix(1, 0, 0, -1, 0, 0);
            g.Transform = myMatrix;
            g.TranslateTransform(0, _panelHeight, MatrixOrder.Append);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ReinitializeWindow();
        }

        private void panelCanvas_Resize(object sender, EventArgs e)
        {
            ReinitializeWindow();
        }

        private void ReinitializeValues()
        {
            _panelWidth = panelCanvas.Width - Settings.CanvasMarginInPixels;
            _panelHeight = panelCanvas.Height - Settings.CanvasMarginInPixels;
            _centerPoint.X = _panelWidth / 2;
            _centerPoint.Y = _panelHeight / 2;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save();
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            new FormManageSkylineSettings(this, Settings).ShowDialog();
        }

        public void OptionsUpdated()
        {
            UpdateConfigurationSettingsUI();
            LoadAndParseDataFile();
            DrawRawData();
            ReinitializeWindow();

            if (Settings.ShowDataPointWindow)
            {
                FormDataPoints.Show();
            }
            else
            {
                FormDataPoints.Hide();
            }
        }

        private void ReinitializeWindow()
        {
            ReinitializeValues();
            panelCanvas.Invalidate();
        }
    }
}
