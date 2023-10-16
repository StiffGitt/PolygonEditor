using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PolygonEditor.Structures
{
    public struct Segment
    {
        public Point a;
        public Point b;

        public Segment(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }
        public (Segment, Segment) GetParallelsBy(int offset)
        {
            double dax = (double) a.X;
            double day = (double) a.Y;
            double dbx = (double) b.X;
            double dby = (double) b.Y;
            double m = (dby - day) / (dbx - dax);
            //double ax1 = (dax*(1 + m) + Math.Sqrt(dax*dax*(1+m)*(1+m) - (1+m)*(dax*dax*(1+m) - offset*offset*m*m))) / (m + 1);
            //double ax2 = (dax*(1 + m) - Math.Sqrt(dax*dax*(1+m)*(1+m) - (1+m)*(dax*dax*(1+m) - offset*offset*m*m))) / (m + 1);
            double ax1 = dax - (offset*m) / (Math.Sqrt(m*m + 1));
            double ax2 = dax + (offset*m) / (Math.Sqrt(m*m + 1));
            double ay1 = day + (dax - ax1) / m;
            double ay2 = day + (dax - ax2) / m;

            double bx1 = dbx - (offset * m) / (Math.Sqrt(m * m + 1));
            double bx2 = dbx + (offset * m) / (Math.Sqrt(m * m + 1));
            double by1 = dby + (dbx - ax1) / m;
            double by2 = dby + (dbx - ax2) / m;

            var a1 = new Point((int)ax1, (int)ay1);
            var b1 = new Point((int)bx1, (int)by1);
            var a2 = new Point((int)ax2, (int)ay2);
            var b2 = new Point((int)bx2, (int)by2);
            return (new Segment(a1, b1), new Segment(a2, b2));
        }
    }
}
