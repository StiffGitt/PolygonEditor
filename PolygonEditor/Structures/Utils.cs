using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace PolygonEditor.Structures
{
    public static class Utils
    {
        private const int offsetErr = 5;
        public static bool IsInCircle(Point p, Point center, int radius)
        {
            return (p.X - center.X) * (p.X - center.X) + (p.Y - center.Y) * (p.Y - center.Y) <= radius * radius;
        }

        public static bool IsNearEdge(Point p, Point start, Point end)
        {
            float a = Dist(start, end);
            float b = Dist(start, p);
            float c = Dist(end, p);
            float s = (a + b + c) / 2;
            double distance = 2 * Math.Sqrt(s * (s - a) * (s - b) * (s - c)) / a;
            return distance <= offsetErr;
        }
        public static float Dist(Point a, Point b)
        {
            return (float)Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }
        public static int Product(int x1, int y1, int x2, int y2)
        {
            return x1 * y2 - x2 * y1;
        }
        public static bool DoEdgesIntersect(Segment s1, Segment s2)
        {
            return DoEdgesIntersect(s1.a, s1.b, s2.a, s2.b);
        }
        public static bool DoEdgesIntersect(Point a1, Point a2, Point b1, Point b2)
        {
            int d1 = Product(b2.X - b1.X, b2.Y - b1.Y, a1.X - b1.X, a1.Y - b1.Y);
            int d2 = Product(b2.X - b1.X, b2.Y - b1.Y, a2.X - b1.X, a2.Y - b1.Y);
            int d3 = Product(a2.X - a1.X, a2.Y - a1.Y, b1.X - a1.X, b1.Y - a1.Y);
            int d4 = Product(a2.X - a1.X, a2.Y - a1.Y, b2.X - a1.X, b2.Y - a1.Y);

            int d12 = d1 * d2;
            int d34 = d3 * d4;

            if (d12 > 0 || d34 > 0)
                return false;
            if (d12 < 0 && d34 < 0)
                return true;
            if (a1 == b1 || a1 == b2 || a2 == b1 || a2 == b2)
                return true;
            if (Math.Max(a1.X, a2.X) < Math.Min(b1.X, b2.X) ||
                Math.Max(b1.X, b2.X) < Math.Min(a1.X, a2.X) ||
                Math.Max(a1.Y, a2.Y) < Math.Min(b1.Y, b2.Y) ||
                Math.Max(b1.Y, b2.Y) < Math.Min(a1.Y, a2.Y))
                return false;
            return true;
        }
        public static List<Segment> GetSegmentsFromPoints(List<Point> points)
        {
            List<Segment> segments = new List<Segment>();
            for (int i = 0, j; i < points.Count; i++)
            {
                j = (i + 1) % points.Count;
                var a = new Point(points[i].X, points[i].Y);
                var b = new Point(points[j].X, points[j].Y);
                segments.Add(new Segment(a, b));
            }
            return segments;
        }
        public static (double, double, double) ExtendSegmentToLine(Segment s)
        {
            double A = s.a.Y - s.b.Y;
            double B = s.b.X - s.a.X;
            double C = (s.a.X - s.b.X) * s.a.Y + (s.b.Y - s.a.Y) * s.a.X;
            return (A, B, C);
        }
        public static Point LinesIntersectionPoint((double A, double B, double C) l1, (double A, double B, double C) l2)
        {
            double x = (l1.B * l2.C - l2.B * l1.C) / (l1.A * l2.B - l2.A * l1.B);
            double y = (l1.C * l2.A - l2.C * l1.A) / (l1.A * l2.B - l2.A * l1.B);
            return new Point((int)x, (int)y);
        }
        // dla współliniowych punktów
        public static bool IsPointOnRay(Point start, Point dir, Point p)
        {
            if (start.X < dir.X)
                return p.X > start.X;
            else
                return p.X < start.X;
        }
        public static void DrawArcByPoints(Graphics g, Pen pen, Point s, Point ai, Point bi)
        {
            //PointF a = new PointF(ai.X, ai.Y);
            //PointF b = new PointF(bi.X, bi.Y);
            //float radius = Dist(s, ai);
            //double x = b.X - a.X, y = b.Y - a.Y;
            //// get orientation angle
            //var theta = Math.Atan2(y, x);
            //// length between A and B
            //var l = Math.Sqrt(x * x + y * y);
            //if (2 * radius >= l)
            //{
            //    // find the sweep angle (actually half the sweep angle)
            //    var phi = Math.Asin(l / (2 * radius));
            //    // triangle height from the chord to the center
            //    var h = radius * Math.Cos(phi);
            //    // get center point. 
            //    // Use sin(theta)=y/l and cos(theta)=x/l
            //    PointF C = new PointF(
            //        (float)(a.X + x / 2 - h * (y / l)),
            //        (float)(a.Y + y / 2 + h * (x / l)));


            //    // Conversion factor between radians and degrees
            //    const double to_deg = 180 / Math.PI;

            //    // Draw arc based on square around center and start/sweep angles
            //    g.DrawArc(pen, C.X - radius, C.Y - radius, 2 * radius, 2 * radius,
            //        (float)((theta - phi) * to_deg) - 90, (float)(2 * phi * to_deg));
            //}
            //int vertexRadius = 6;
            //g.FillEllipse(Brushes.Blue, (ai.X - vertexRadius / 2), (ai.Y - vertexRadius / 2), vertexRadius, vertexRadius);
            //g.FillEllipse(Brushes.Green, (bi.X - vertexRadius / 2), (bi.Y - vertexRadius / 2), vertexRadius, vertexRadius);
            //g.FillEllipse(Brushes.Yellow, (s.X - vertexRadius / 2), (s.Y - vertexRadius / 2), vertexRadius, vertexRadius);
            float r = (float)Math.Sqrt((ai.X - s.X) * (ai.X - s.X) + (ai.Y - s.Y) * (ai.Y - s.Y));
            float x = s.X - r;
            float y = s.Y - r;
            float width = 2 * r;
            float height = 2 * r;
            //float startAngle = (float)(180 / Math.PI * Math.Atan2(ai.Y - s.Y, ai.X - s.X));
            //float endAngle = (float)(180 / Math.PI * Math.Atan2(bi.Y - s.Y, bi.X - s.X));
            //float startAngle = (1 - (Math.Atan2(ai.Y - s.Y, ai.X - s.X) / 2 / Math.PI + 1.25) % 1);
            //float endAngle = (float) (1 - (Math.Atan2(bi.Y - s.Y, bi.X - s.X) / 2 / Math.PI + 1.25) % 1);
            //float startAngle = (float)(Math.Atan2(ai.Y - s.Y, ai.X - s.X) * 180 / Math.PI);
            //float endAngle = (float)(Math.Atan2(bi.Y - s.Y, bi.X - s.X) * 180 / Math.PI);
            float startAngle = (float)(Math.Atan2(ai.Y - s.Y, ai.X - s.X) * 180 / Math.PI);
            float endAngle = (float)(Math.Atan2(bi.Y - s.Y, bi.X - s.X) * 180 / Math.PI);
            g.DrawArc(pen, x, y, width, height, startAngle, endAngle - startAngle);
            //g.DrawEllipse(pen, x, y, width, height);
            //g.DrawArc(pen, x, y, width, height, 0, -90);
        }
    }
}
