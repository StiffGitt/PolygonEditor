using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace PolygonEditor.Structures
{
    public class Polygon : Shape
    {
        public List<Point> points { get; private set; }
        private Color edgeColor;
        private Color vertexColor;
        private Color brushColor;
        private bool isFinished = false;

        public Polygon(Color edgeColor, Color vertexColor, Color brushColor) 
        {
            this.edgeColor = edgeColor;
            this.vertexColor = vertexColor;
            this.brushColor = brushColor;
            this.points = new List<Point>();
        }
        public override void Draw(Bitmap picture, Point? p = null)
        {

            Graphics g = Graphics.FromImage(picture);
            Pen edgePen = new Pen(edgeColor);
            SolidBrush vertexBrush = new SolidBrush(vertexColor);
            SolidBrush fillBrush = new SolidBrush(brushColor);
            if (isFinished)
            {
                g.FillPolygon(fillBrush, points.ToArray());
                g.DrawPolygon(edgePen, points.ToArray());
            }
            else 
            {
                if (points.Count > 1)
                    g.DrawLines(edgePen, points.ToArray());
                if (p != null)
                    g.DrawLine(edgePen, points.Last(), (Point)p);
            }
            foreach (Point v in points)
            {
                g.FillEllipse(vertexBrush, (v.X - vertexRadius / 2), (v.Y - vertexRadius / 2), vertexRadius, vertexRadius);
            }
            g.Dispose();
        }
        public override bool AddPoint(Point p)
        {
            if (points.Count > 0 && Utils.IsInCircle(p, points.First(), vertexRadius))
            {
                isFinished = true;
            }
            else
            {
                points.Add(p);
            }
            return isFinished;
        }
    }
}
