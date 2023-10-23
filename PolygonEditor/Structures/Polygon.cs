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
        private Color hullColor;
        private bool isFinished = false;

        public Polygon(Color edgeColor, Color vertexColor, Color brushColor, Color hullColor) 
        {
            this.edgeColor = edgeColor;
            this.vertexColor = vertexColor;
            this.brushColor = brushColor;
            this.hullColor = hullColor;
            this.points = new List<Point>();
        }
        public override void Draw(Bitmap picture, Point? p = null)
        {
            Graphics g = Graphics.FromImage(picture);
            Pen edgePen = new Pen(edgeColor);
            SolidBrush vertexBrush = new SolidBrush(vertexColor);
            SolidBrush fillBrush = new SolidBrush(brushColor);
            if (isFinished && points.Count > 1)
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
            if (isInflated && points.Count >= 3)
                DrawInflated(g);
            g.Dispose();
        }
        public override bool AddPoint(Point p)
        {
            if (points.Count > 1 && Utils.IsInCircle(p, points.First(), vertexRadius))
            {
                isFinished = true;
            }
            else
            {
                points.Add(p);
            }
            return isFinished;
        }
        public override void MovePoint(int idx, Point p)
        {
            points[idx] = p;
        }
        public override void MoveEdge(int idx, Point p, Point prevP)
        {
            int nextIdx = (idx + 1) % points.Count;
            int distX = p.X - prevP.X;
            int distY = p.Y - prevP.Y;
            points[idx] = new Point(points[idx].X + distX, points[idx].Y + distY);
            points[nextIdx] = new Point(points[nextIdx].X + distX, points[nextIdx].Y + distY);
        }
        public override void MoveShape(Point p, Point prevP)
        {
            int distX = p.X - prevP.X;
            int distY = p.Y - prevP.Y;
            List<Point> newPoints = new List<Point>();
            foreach (Point point in points)
            {
                newPoints.Add(new Point(point.X + distX, point.Y + distY));
            }
            points = newPoints;
        }
        public override bool RemoveVertex(int idx)
        {
            points.RemoveAt(idx);
            return points.Count == 0;
        }
        public override int IsOnVertex(Point p)
        {
            for(int i = 0; i < points.Count; i++)
            {
                if (Utils.IsInCircle(p, points[i], vertexRadius))
                    return i;
            }
            return -1;
        }

        public override int IsOnEdge(Point p)
        {
            for (int i = 0, j = 1; i < points.Count; i++, j = (i + 1) % points.Count)
            {
                if (Utils.IsNearEdge(p, points[i], points[j]))
                    return i;
            }
            return -1;
        }

        public override bool IsInside(Point p)
        {
            //int minY = points.First().Y;
            //foreach (Point point in points)
            //    if (point.Y < minY)
            //        minY = point.Y;
            Point lineEnd = new Point(0, p.Y);
            int intersects = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if(Utils.DoEdgesIntersect(p, lineEnd, points[i], points[(i + 1) % points.Count]))
                    intersects++;
            }
            return intersects % 2 == 1;
        }
        private void DrawInflated(Graphics g)
        {
            var hullPen = new Pen(hullColor);
            
            var segments = Utils.GetSegmentsFromPoints(Utils.SortCounterClockwise(points));
            int j;
            List<(Segment, bool)> hullSegments = new List<(Segment, bool)>();
            for (int i = 0; i < segments.Count; i++)
            {
                j = (i == 0) ? segments.Count - 1 : i - 1;
                var prevS = segments[j];
                var s = segments[i];
                var parS = s.GetParallelsBy(inflationOffset);
                var p = Utils.LinesIntersectionPoint(Utils.ExtendSegmentToLine(prevS), Utils.ExtendSegmentToLine(parS.Item1));
                //SolidBrush vertexBrush = new SolidBrush(vertexColor);
                //g.FillEllipse(vertexBrush, (p.X - vertexRadius / 2), (p.Y - vertexRadius / 2), vertexRadius, vertexRadius);
                int crossProduct = Utils.Product(prevS.a.Substract(s.a), s.b.Substract(s.a));
                bool isConvex = crossProduct >= 0;
                if (!Utils.IsPointOnRay(prevS.b, prevS.a, p) && crossProduct >= 0 || Utils.IsPointOnRay(prevS.b, prevS.a, p) && crossProduct < 0)
                {
                    hullSegments.Add((parS.Item1, isConvex));
                }
                else
                {
                    hullSegments.Add((parS.Item2, isConvex));
                }
            }
            for (int i = 0, k; i < hullSegments.Count; i++)
            {
                j = (i == 0) ? hullSegments.Count - 1 : i - 1;
                k = (i + 1) % hullSegments.Count;
                var prevS = hullSegments[j].Item1;
                var s = hullSegments[i].Item1;
                if (hullSegments[i].Item2)
                {
                    if (hullSegments[k].Item2)
                        g.DrawLine(hullPen, s.a, s.b);
                    Utils.DrawArcByPoints(g, hullPen, segments[j].b, s.a, prevS.b);
                }
                else if (hullSegments[k].Item2)
                {
                    var p = Utils.LinesIntersectionPoint(Utils.ExtendSegmentToLine(prevS), Utils.ExtendSegmentToLine(s));
                    g.DrawLine(hullPen, hullSegments[i].Item1.b, p);
                    if (hullSegments[j].Item2)
                    {
                        g.DrawLine(hullPen, hullSegments[j].Item1.a, p);
                    }
                    else
                    {
                        var prevPrevS = hullSegments[(j == 0) ? hullSegments.Count - 1 : j - 1].Item1;
                        var pprev = Utils.LinesIntersectionPoint(Utils.ExtendSegmentToLine(prevPrevS), Utils.ExtendSegmentToLine(prevS));
                        g.DrawLine(hullPen, pprev, p);
                    }
                }
                else
                {
                    var p = Utils.LinesIntersectionPoint(Utils.ExtendSegmentToLine(prevS), Utils.ExtendSegmentToLine(s));
                    g.DrawLine(hullPen, hullSegments[j].Item1.a, p);
                }

            }
        }
    }
}
