using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonEditor.Structures
{
    public static class Utils
    {
        public static bool IsInCircle(Point p, Point center, int radius)
        {
            return (p.X - center.X) * (p.X - center.X) + (p.Y - center.Y) * (p.Y - center.Y) <= radius * radius;
        }
    }
}
