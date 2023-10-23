﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace PolygonEditor.Structures
{
    public static class Utils
    {
        private const int offsetErr = 5;
        private const double eps = 0.0001;
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
        public static int Product(Point a, Point b)
        {
            return Product(a.X, a.Y, b.X, b.Y);
        }
        static int GetOrientation(Point p, Point q, Point r)
        {
            int val = Product(r.Substract(q), q.Substract(p));

            if (val == 0) return 0;

            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }
        public static bool DoEdgesIntersect(Segment s1, Segment s2)
        {
            return DoEdgesIntersect(s1.a, s1.b, s2.a, s2.b);
        }
        public static bool IsOnSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                    q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;
            return false;
        }
        //public static bool DoEdgesIntersect(Point a1, Point a2, Point b1, Point b2)
        //{
        //    //int d1 = Product(b2.Substract(b1), a1.Substract(b1));
        //    //int d2 = Product(b2.Substract(b1), a2.Substract(b1));
        //    //int d3 = Product(a2.Substract(a1), b1.Substract(a1));
        //    //int d4 = Product(a2.Substract(a1), b2.Substract(a1));

        //    //int d12 = d1 * d2;
        //    //int d34 = d3 * d4;

        //    //if (d12 > 0 || d34 > 0)
        //    //    return false;
        //    //if (d12 < 0 && d34 < 0)
        //    //    return true;
        //    //if (a1 == b1 || a1 == b2 || a2 == b1 || a2 == b2)
        //    //    return true;
        //    //if (Math.Max(a1.X, a2.X) < Math.Min(b1.X, b2.X) ||
        //    //    Math.Max(b1.X, b2.X) < Math.Min(a1.X, a2.X) ||
        //    //    Math.Max(a1.Y, a2.Y) < Math.Min(b1.Y, b2.Y) ||
        //    //    Math.Max(b1.Y, b2.Y) < Math.Min(a1.Y, a2.Y))
        //    //    return false;
        //    //return true;
        //    return doIntersect(a1, a2, b1, b2);
        //}
        public static bool DoEdgesIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            int o1 = GetOrientation(p1, q1, p2);
            int o2 = GetOrientation(p1, q1, q2);
            int o3 = GetOrientation(p2, q2, p1);
            int o4 = GetOrientation(p2, q2, q1);

            if (o1 != o2 && o3 != o4)
                return true;

            if (o1 == 0 && IsOnSegment(p1, p2, q1)) return true;

            if (o2 == 0 && IsOnSegment(p1, q2, q1)) return true;

            if (o3 == 0 && IsOnSegment(p2, p1, q2)) return true;

            if (o4 == 0 && IsOnSegment(p2, q1, q2)) return true;

            return false;
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

        public static bool IsPointOnRay(Point start, Point dir, Point p)
        {
            if (start.X != p.X)
            {
                if (start.X < dir.X)
                    return p.X > start.X;
                else
                    return p.X < start.X;
            }
            else
            {
                if (start.Y < dir.Y)
                    return p.Y > start.Y;
                else
                    return p.Y < start.Y;
            }
        }
        public static void DrawArcByPoints(Graphics g, Pen pen, Point s, Point ai, Point bi)
        {
            //int vertexRadius = 6;
            //g.FillEllipse(Brushes.Blue, (ai.X - vertexRadius / 2), (ai.Y - vertexRadius / 2), vertexRadius, vertexRadius);
            //g.FillEllipse(Brushes.Green, (bi.X - vertexRadius / 2), (bi.Y - vertexRadius / 2), vertexRadius, vertexRadius);
            //g.FillEllipse(Brushes.Yellow, (s.X - vertexRadius / 2), (s.Y - vertexRadius / 2), vertexRadius, vertexRadius);
            float r = (float)Math.Sqrt((ai.X - s.X) * (ai.X - s.X) + (ai.Y - s.Y) * (ai.Y - s.Y));
            float x = s.X - r;
            float y = s.Y - r;
            float width = 2 * r;
            float height = 2 * r;
            float startAngle = (float)(Math.Atan2(ai.Y - s.Y, ai.X - s.X) * 180 / Math.PI);
            float endAngle = (float)(Math.Atan2(bi.Y - s.Y, bi.X - s.X) * 180 / Math.PI);
            
            startAngle = FromAtanToArcAngle(startAngle);
            endAngle = FromAtanToArcAngle(endAngle);

            float sweepAngle = Math.Abs(endAngle - startAngle);
            float startArcAngle = Math.Min(startAngle, endAngle);

            g.DrawArc(pen, x, y, width, height, startArcAngle , sweepAngle < 180 ? sweepAngle : -(360 - sweepAngle));
        }
        public static float FromAtanToArcAngle(float angle)
        {
            if (angle >= 0)
                return angle;
            else
                return 360 + angle;
        }
        public static List<Point>SortCounterClockwise(List<Point> inPoints)
        {
            var points = new List<Point>(inPoints);
            int idx = 0;
            for (int i = 1; i < points.Count; i++)
            {
                if (points[idx].Y < points[i].Y || (points[idx].Y == points[i].Y && points[idx].X < points[i].X))
                    idx = i;
            }
            int prev = (idx == 0) ? points.Count - 1 : idx - 1;
            int next = (idx + 1) % points.Count;
            if (Utils.Product(points[prev].Substract(points[idx]), points[next].Substract(points[idx])) >= 0)
                return points;
            Point temp;
            for (int i = 0; i < (points.Count - 1) / 2; i++)
            {
                temp = points[next];
                points[next] = points[prev];
                points[prev] = temp;

                prev = (prev == 0) ? points.Count - 1 : prev - 1;
                next = (next + 1) % points.Count;
            }
            return points;
        }
    }
}
