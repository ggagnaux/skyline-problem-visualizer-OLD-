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
        public enum Axis { Y, X } 


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
        public float ZoomFactor { get; set; } = 1.0f;

        private Color _XAxisColor;
        public Color XAxisColor
        {
            get
            {
                return _XAxisColor;
            }
            set
            {
                _XAxisColor = value;
                if (_penXAxis != null)
                    _penXAxis.Dispose();
                _penXAxis = new Pen(XAxisColor, XAxisWidth);
            }
        }

        private Color _YAxisColor;
        public Color YAxisColor
        {
            get
            {
                return _YAxisColor;
            }
            set
            {
                _YAxisColor = value;
                if (_penYAxis != null)
                    _penYAxis.Dispose();
                _penYAxis = new Pen(YAxisColor, YAxisWidth);
            }
        }

        private float _XAxisWidth;
        public float XAxisWidth
        {
            get
            {
                return _XAxisWidth;
            }
            set
            {
                _XAxisWidth = value;
                if (_penXAxis != null)
                    _penXAxis.Dispose();
                _penXAxis = new Pen(XAxisColor, _XAxisWidth);
            }
        }

        private float _YAxisWidth;
        public float YAxisWidth
        {
            get
            {
                return _YAxisWidth;
            }
            set
            {
                _YAxisWidth = value;
                if (_penYAxis != null)
                    _penYAxis.Dispose();
                _penYAxis = new Pen(YAxisColor, _YAxisWidth);
            }
        }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public void SetCanvasDimensions(int w, int h)
        {
            this.Width = w;
            this.Height = h;
        }

        /// <summary>
        /// 
        /// </summary>
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
            specs.Origin = new Point(0, 0);
            specs.Finish = new Point(Width, Height);
            return specs;
        }

        /// <summary>
        /// 
        /// </summary>
        public void TransformCanvas(Axis a)
        {
            // 
            // |-      -|
            // | 11  12 |
            // | 21  22 |
            // | 31  32 |
            // |-      -|
            var matrix = new Matrix(11, 12, 21, 22, 31, 32);

            if (a == Axis.X)
            {
                var m = new Matrix(1, 0, 0, -1, 0, 0);
                Graphics.Transform = m;
                Graphics?.TranslateTransform(Width, 0, MatrixOrder.Append);
            }
            else if (a == Axis.Y)
            {
                var m = new Matrix(1, 0, 0, -1, 0, 0);
                Graphics.Transform = m;
                Graphics?.TranslateTransform(0, Height, MatrixOrder.Append);
            }
        }

        [Obsolete]
        public void TransformCanvas()
        {
            // Flip Y Axis
            Matrix myMatrix = new Matrix(1, 0, 0, -1, 0, 0);
            Graphics.Transform = myMatrix;
            Graphics?.TranslateTransform(0, Height, MatrixOrder.Append);
        }

        [Obsolete]
        public void TransformCanvas(Graphics g)
        {
            // Flip Y Axis
            Matrix myMatrix = new Matrix(1, 0, 0, -1, 0, 0);
            g.Transform = myMatrix;
            g.TranslateTransform(0, Height, MatrixOrder.Append);
        }

        /// <summary>
        /// 
        /// </summary>
        public void FlipYAxis()
        {
            TransformCanvas(Axis.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        public void FlipXAxis()
        {
            TransformCanvas(Axis.X);
        }


        public void Zoom(float zFactor)
        {
            ZoomFactor = zFactor;
            Graphics.ScaleTransform(ZoomFactor, ZoomFactor);
        }

    }
}
