using System;
using System.Collections.Generic;
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
    }
}
