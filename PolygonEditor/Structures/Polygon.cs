using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PolygonEditor.Structures
{
    public class Polygon : Shape
    {
        public List<Point> points { get; private set; }
        private Dictionary<int, Relation> relDict;
        private Color edgeColor;
        private Color vertexColor;
        private Color brushColor;
        private Color hullColor;
        private bool isFinished = false;
        private string resourcesPath;
        private Bitmap relIconImg;


        public Polygon(Color edgeColor, Color vertexColor, Color brushColor, Color hullColor) 
        {
            this.edgeColor = edgeColor;
            this.vertexColor = vertexColor;
            this.brushColor = brushColor;
            this.hullColor = hullColor;
            this.points = new List<Point>();
            this.relDict = new Dictionary<int, Relation>();
            
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
                DrawPolygon(g, points, relDict);
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
            int prevIdx = (idx == 0)? points.Count - 1 : idx - 1, nextIdx = (idx + 1) % points.Count;
            if (relDict.ContainsKey(prevIdx))
                MoveWithRelation(idx, prevIdx, p, relDict[prevIdx]);
            if (relDict.ContainsKey(idx))
                MoveWithRelation(idx, nextIdx, p, relDict[idx]);
            points[idx] = p;
        }
        private void MoveWithRelation(int i, int j, Point p,Relation rel)
        {
            if (rel == Relation.Horizontal)
            {
                points[j] = new Point(points[j].X, p.Y);
                points[i] = new Point(p.X, p.Y);
            }
            if (rel == Relation.Vertical)
            {
                points[j] = new Point(p.X, points[j].Y);
                points[i] = new Point(p.X, p.Y);
            }
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
            Point lineEnd = new Point(0, p.Y);
            int intersects = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if(Utils.DoEdgesIntersect(p, lineEnd, points[i], points[(i + 1) % points.Count]))
                    intersects++;
            }
            return intersects % 2 == 1;
        }
        public void AddRelation(int idx, Relation r)
        {
            Point p = points[idx];
            int nextIdx = (idx + 1) % points.Count, prevIdx = (idx == 0) ? points.Count - 1 : idx - 1;
            if ((relDict.ContainsKey(prevIdx) && relDict[prevIdx] == r) || (relDict.ContainsKey(nextIdx) && relDict[nextIdx] == r))
                return;
            if (relDict.ContainsKey(idx))
                relDict.Remove(idx);
            relDict.Add(idx, r);
            if (r == Relation.Horizontal)
                points[nextIdx] = new Point(points[nextIdx].X, p.Y);
            if (r == Relation.Vertical)
                points[nextIdx] = new Point(p.X, points[nextIdx].Y);
        }
        public void RemoveRelation(int idx)
        {
            if(relDict.ContainsKey(idx))
                relDict.Remove(idx);
        }
        private void DrawPolygon(Graphics g, List<Point> pointsToDraw, Dictionary<int, Relation>? dict)
        {
            Pen edgePen = new Pen(edgeColor);
            for (int i = 0, j = 1; i < pointsToDraw.Count; i++, j = (j + 1) % pointsToDraw.Count)
            {
                Utils.DrawLine(g, edgePen, pointsToDraw[i], pointsToDraw[j], dict != null && dict.ContainsKey(i));
            }
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
