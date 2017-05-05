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

            UpdateConfigurationSettingsUI();

            buttonToggleData.BackColor = Color.FromArgb(100, 0, 0, 0);
            buttonToggleData.ForeColor = Color.FromArgb(100, 255, 0, 0);
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
            var w = panelCanvas.Width - Settings.CanvasMarginInPixels;
            var h = panelCanvas.Height - Settings.CanvasMarginInPixels;
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
        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            CanvasManager.Graphics = g;

            // Set the canvas background color
            panelCanvas.BackColor = ColorUtilities.GetColorFromHexRGBString(Settings.CanvasBackgroundColor);

            // Set the coordinate transformation (Flip the Y Axis)
            CanvasManager.TransformCanvas();

            // Ensure the canvas dimensions are correct and optionally
            // display the X and/or Y Axis.
            CanvasManager.SetCanvasDimensions(panelCanvas.Width, panelCanvas.Height);
            CanvasManager.ShowXAxis = Settings.ShowXAxis;
            CanvasManager.ShowYAxis = Settings.ShowYAxis;
            CanvasManager.RenderXAndYAxis();

            // Show the optional grid
            CanvasManager.ShowGrid = Settings.ShowGrid;
            CanvasManager.GridColor = ColorUtilities.GetColorFromHexRGBString(Settings.GridColor);
            CanvasManager.RenderGrid();


            GraphicsPath gp = new GraphicsPath();
            //gp.SetMarkers();

            var penIndex = 0;
            foreach (var b in DataManager.Data)
            {
                // Scale the data to fit nicely in the panel
                var a = b.GetScaledCoordinates(CanvasManager.Width,
                                               CanvasManager.Height,
                                               DataManager.MaximumX,
                                               DataManager.MaximumY);

                // Get the data
                var l = a.Left;
                var h = a.Height;
                var r = a.Right;
                var w = a.Width;

                Rectangle rect1 = b.GetScaledRectangle(CanvasManager.Width,
                                                       CanvasManager.Height,
                                                       DataManager.MaximumX,
                                                       DataManager.MaximumY);

                gp.AddRectangle(rect1);

                HandleRef handle = new HandleRef(gp, (IntPtr)gp.GetType().GetField("nativePath", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(gp));
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

            if (Settings.HighlightSkyline)
            {
                var color = ColorUtilities.GetColorFromHexRGBString(Settings.SkylineBorderColor);
                var penWidth = Settings.SkylineBorderWidth;
                g.DrawPath(new Pen(color, penWidth), gp);

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
            // Get mouse coordinates
            var x = e.X;
            var y = e.Y;

            // We have to flip the Y-Axis to match the actual chart
            var height = panelCanvas.Height;
            y = height - y;

            string text = $"{x} : {y}";
            labelMouseCoordinates.Text = text;
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
            ReloadData();
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
        #endregion
    }
}
