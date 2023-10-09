using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace PolygonEditor.Structures
{
    public class Polygon : Shape
    {
        public List<Point> points { get; private set; }
        public List<Edges> edges { get; private set; }
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
            this.edges = new List<Edges>();
        }
        public void Draw(Bitmap picture)
        {
            Graphics g = Graphics.FromImage(picture);
            Pen edgePen = new Pen(edgeColor);
            SolidBrush fillBrush = new SolidBrush(brushColor);
            g.DrawLines(edgePen, points.ToArray());
        }
        public bool AddPoint(int x, int y)
        {
            Point p = new Point(x, y);
            if (p.X == points.First().X && p.Y == points.First().Y)
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
