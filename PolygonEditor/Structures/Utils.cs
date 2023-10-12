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
        public static bool DoEdgesIntersect(Point a1, Point a2, Point b1, Point b2)
        {

        }
    }
}
