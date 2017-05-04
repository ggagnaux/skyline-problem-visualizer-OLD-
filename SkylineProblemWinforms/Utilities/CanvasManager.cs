using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylineProblemWinforms
{
    public class ChartCanvasManager
    {
        public bool ShowXAxis { get; set; }
        public bool ShowYAxis { get; set; }
        public Graphics Graphics { get; set; }
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }


        #region Private Variables
        private Pen _penYAxis;
        private Pen _penXAxis;
        private float _XAxisWidth = 3.0f;
        private float _YAxisWidth = 3.0f;
        private Color _XAxisColor = Color.Red;
        private Color _YAxisColor = Color.Red;
        #endregion


        #region Constructor(s)
        public ChartCanvasManager(int w, int h)
        {
            this.CanvasWidth = w;
            this.CanvasHeight = h;

            this._penXAxis = new Pen(new SolidBrush(_XAxisColor), _XAxisWidth);
            this._penYAxis = new Pen(new SolidBrush(_YAxisColor), _YAxisWidth);
        }
        #endregion

        public void SetCanvasDimensions(int w, int h)
        {
            this.CanvasWidth = w;
            this.CanvasHeight = h;
        }

        public void RenderXAndYAxis()
        {
            if (ShowXAxis) RenderXAxis();
            if (ShowYAxis) RenderYAxis();
        }

        public void RenderYAxis()
        {
            Graphics?.DrawLine(_penYAxis, 0, 0, 0, CanvasHeight);
        }

        public void RenderXAxis()
        {
            Graphics?.DrawLine(_penXAxis, 0, 0, CanvasWidth, 0);
        }

        public void TransformCanvas()
        {
            Matrix myMatrix = new Matrix(1, 0, 0, -1, 0, 0);
            Graphics.Transform = myMatrix;
            Graphics?.TranslateTransform(0, CanvasHeight, MatrixOrder.Append);
        }
    }
}
