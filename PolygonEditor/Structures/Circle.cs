using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonEditor.Structures
{
    public class Circle : Shape
    {
        private Color edgeColor;
        private Point center;
        private double r;

        public Circle(Color color, Point p, double r) 
        {
            edgeColor = color;
            center = p;
            this.r = r;
        }
        public void SetRadius(double r)
        {
            this.r = r;
        }

        public override bool AddPoint(Point p)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Bitmap picture, LineAlgorithm lineAlgorithm, Point? p = null)
        {
            DrawAntyAliasingCircle(picture);
        }
        private void DrawCircle(Bitmap bitmap)
        {
            double deltaE = 3;
            double deltaSE = 5 - 2 * r;
            double d = 1 - r;
            int x = 0;
            int y = (int)r;
            PaintPixels(bitmap, x, y, edgeColor);
            while (y > x)
            {
                if (d < 0)
                {
                    d += deltaE;
                    deltaE += 2;
                    deltaSE += 2;
                }
                else
                {
                    d += deltaSE;
                    deltaE += 2;
                    deltaSE += 4;
                    y--;
                }
                x++;
                PaintPixels(bitmap, x, y, edgeColor);
            }
        }
        private void PaintPixels(Bitmap bitmap, int x, int y, Color color)
        {
            Utils.PaintPixel(bitmap, center.X + x, center.Y + y, edgeColor);
            Utils.PaintPixel(bitmap, center.X - x, center.Y + y, edgeColor);
            Utils.PaintPixel(bitmap, center.X + x, center.Y - y, edgeColor);
            Utils.PaintPixel(bitmap, center.X - x, center.Y - y, edgeColor);
            Utils.PaintPixel(bitmap, center.X + y, center.Y + x, edgeColor);
            Utils.PaintPixel(bitmap, center.X - y, center.Y + x, edgeColor);
            Utils.PaintPixel(bitmap, center.X + y, center.Y - x, edgeColor);
            Utils.PaintPixel(bitmap, center.X - y, center.Y - x, edgeColor);
        }
        private void DrawAntyAliasingCircle(Bitmap bitmap)
        {
            int x = (int) r;
            int y = 0;
            double T = 0;
            int I = 1;
            PaintPixelsInt(bitmap, x, y, edgeColor, I);
            while (x > y)
            {
                y++;
                double DRy = Math.Ceiling(Math.Sqrt(r*r - y*y)) - Math.Sqrt(r*r - y* y);
                if (DRy < T)
                    x--;
                PaintPixelsInt(bitmap, x, y, edgeColor, I * (1 - DRy));
                PaintPixelsInt(bitmap, x - 1, y, edgeColor, I * (1 - DRy));
                T = DRy;
            }
        }
        private void PaintPixelsInt(Bitmap bitmap, int x, int y, Color color, double intensity)
        {
            int newR = (int)(color.R * intensity);
            int newG = (int)(color.G * intensity);
            int newB = (int)(color.B * intensity);
            Utils.PaintPixel(bitmap, center.X + x, center.Y + y, Color.FromArgb(newR, newG, newB));
            Utils.PaintPixel(bitmap, center.X - x, center.Y + y, Color.FromArgb(newR, newG, newB));
            Utils.PaintPixel(bitmap, center.X + x, center.Y - y, Color.FromArgb(newR, newG, newB));
            Utils.PaintPixel(bitmap, center.X - x, center.Y - y, Color.FromArgb(newR, newG, newB));
            Utils.PaintPixel(bitmap, center.X + y, center.Y + x, Color.FromArgb(newR, newG, newB));
            Utils.PaintPixel(bitmap, center.X - y, center.Y + x, Color.FromArgb(newR, newG, newB));
            Utils.PaintPixel(bitmap, center.X + y, center.Y - x, Color.FromArgb(newR, newG, newB));
            Utils.PaintPixel(bitmap, center.X - y, center.Y - x, Color.FromArgb(newR, newG, newB));
        }


        public override bool IsInside(Point p)
        {
            return Utils.Dist(center, p) < r;
        }

        public override int IsOnEdge(Point p)
        {
            return -1;
        }

        public override int IsOnVertex(Point p)
        {
             return -1;
        }

        public override void MoveEdge(int idx, Point p, Point prevP)
        {
            throw new NotImplementedException();
        }

        public override void MovePoint(int idx, Point p)
        {
            throw new NotImplementedException();
        }

        public override void MoveShape(Point p, Point prevP)
        {
            center = p;
        }

        public override bool RemoveVertex(int idx)
        {
            throw new NotImplementedException();
        }
    }
}
