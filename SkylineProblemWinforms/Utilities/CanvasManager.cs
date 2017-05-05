using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KohdAndArt.Toolkit.Graphics;

namespace SkylineProblemWinforms
{
    public class ChartCanvasManager
    {
        #region Public Properties
        public Graphics Graphics { get; set; }
        public bool ShowXAxis { get; set; }
        public bool ShowYAxis { get; set; }
        public bool ShowGrid { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int GridSpacingX { get; set; } = 50;
        public int GridSpacingY { get; set; } = 50;
        public Color GridColor { get; set; } = Color.FromArgb(100, 100, 100, 100);
        public float GridPenWidth { get; set; } = 1.0f;
        public Color XAxisColor { get; set; } = Color.Red;
        public Color YAxisColor { get; set; } = Color.Red;
        public float XAxisWidth { get; set; } = 3.0f;
        public float YAxisWidth { get; set; } = 3.0f;
        #endregion


        #region Private Variables
        private Pen _penXAxis;
        private Pen _penYAxis;
        #endregion

        #region Constructor(s)
        public ChartCanvasManager(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this._penXAxis = new Pen(XAxisColor, XAxisWidth);
            this._penYAxis = new Pen(YAxisColor, YAxisWidth);
        }
        #endregion

        public void SetCanvasDimensions(int w, int h)
        {
            this.Width = w;
            this.Height = h;
        }

        public void RenderXAndYAxis()
        {
            if (ShowXAxis) RenderXAxis();
            if (ShowYAxis) RenderYAxis();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RenderYAxis()
        {
            Graphics?.DrawLine(_penYAxis, 0, 0, 0, Height);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RenderXAxis()
        {
            Graphics?.DrawLine(_penXAxis, 0, 0, Width, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RenderGrid()
        {
            if (ShowGrid)
            {
                var w = new WinformGraphics();
                var details = new DrawingDetails
                {
                    Canvas = Graphics,
                    GridSpacingHorizontal = this.GridSpacingX,
                    GridSpacingVertical = this.GridSpacingY,
                    PenColor = this.GridColor,
                    PenWidth = (int)this.GridPenWidth
                };
                w.ViewportSpecs = GetViewportSpecs();
                w.DrawGrid(details);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ViewportSpecifications GetViewportSpecs()
        {
            var specs = new ViewportSpecifications();
            int width = Width;
            int height = Height;
            specs.Origin = new Point(0, 0);
            specs.Finish = new Point(width, height);
            return specs;
        }

        /// <summary>
        /// 
        /// </summary>
        public void TransformCanvas()
        {
            Matrix myMatrix = new Matrix(1, 0, 0, -1, 0, 0);
            Graphics.Transform = myMatrix;
            Graphics?.TranslateTransform(0, Height, MatrixOrder.Append);
        }

        public void TransformCanvas(Graphics g)
        {
            Matrix myMatrix = new Matrix(1, 0, 0, -1, 0, 0);
            g.Transform = myMatrix;
            g.TranslateTransform(0, Height, MatrixOrder.Append);
        }
    }
}
