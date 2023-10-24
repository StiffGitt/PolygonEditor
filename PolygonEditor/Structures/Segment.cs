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
            double ax1 = 0;
            double ax2 = 0;
            double ay1 = 0;
            double ay2 = 0;
            double bx1 = 0;
            double bx2 = 0;
            double by1 = 0;
            double by2 = 0;
            if (day == dby)
            {
                ax1 = ax2 = dax;
                ay1 = day - offset;
                ay2 = day + offset;
                bx1 = bx2 = dbx;
                by1 = dby - offset;
                by2 = dby + offset;
            }
            else if (dax == dbx)
            {
                ax1 = dax - offset;
                ax2 = dax + offset;
                ay1 = ay2 = day;
                bx1 = dbx - offset;
                bx2 = dbx + offset;
                by1 = by2 = dby;
            }
            else
            {
                double m = (dby - day) / (dbx - dax);
                ax1 = dax - (offset * m) / (Math.Sqrt(m * m + 1));
                ax2 = dax + (offset * m) / (Math.Sqrt(m * m + 1));
                ay1 = day + (dax - ax1) / m;
                ay2 = day + (dax - ax2) / m;

                bx1 = dbx - (offset * m) / (Math.Sqrt(m * m + 1));
                bx2 = dbx + (offset * m) / (Math.Sqrt(m * m + 1));
                by1 = dby + (dbx - bx1) / m;
                by2 = dby + (dbx - bx2) / m;
            }
            var a1 = new Point((int)ax1, (int)ay1);
            var b1 = new Point((int)bx1, (int)by1);
            var a2 = new Point((int)ax2, (int)ay2);
            var b2 = new Point((int)bx2, (int)by2);
            return (new Segment(a1, b1), new Segment(a2, b2));
        }
    }
}
