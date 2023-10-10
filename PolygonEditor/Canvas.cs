using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using PolygonEditor.Structures;

namespace PolygonEditor
{
    public class Canvas
    {
        //public bool IsPainting { get; set; } = false;
        public Bitmap picture;
        private List<Shape> shapes;
        private Dictionary<ShapeTypes, Func<Color, Color, Color, Shape>> shapeCtors;
        private Color edgeColor = Color.DarkBlue;
        private Color vertexColor = Color.OrangeRed;
        private Color fillColor = Color.Aqua;
        private Color backGroundColor;

        public Canvas(Bitmap bitmap, Color backGroundColor)
        {
            shapes = new List<Shape>();
            this.picture = bitmap;
            this.backGroundColor = backGroundColor;
            InitializeDicts();

        }
        public bool AddPoint(Point p)
        {
            bool isFinished = shapes.Last().AddPoint(p);
            Draw();
            return isFinished;
        }
        public void StartPainting(Point p, ShapeTypes shape)
        {
            shapes.Add(shapeCtors[shape](edgeColor, vertexColor, fillColor));
            shapes.Last().AddPoint(p);
            Draw();
        }
        public void Draw(Point? p = null)
        {
            Clear();
            foreach (Shape shape in shapes)
            {
                shape.Draw(picture, p);
            }
        }
        private void Clear()
        {
            using (Graphics g = Graphics.FromImage(picture))
            {
                g.Clear(backGroundColor);
            }
        }
        private void InitializeDicts()
        {
            shapeCtors = new Dictionary<ShapeTypes, Func<Color, Color, Color, Shape>>();
            var l = (Color c1, Color c2, Color c3) => new Polygon(c1, c2, c3);
            shapeCtors.Add(ShapeTypes.Polygon, (Color c1, Color c2, Color c3) => new Polygon(c1, c2, c3));
        }
        public void TestDrawEllipse(int x, int y)
        {
            SolidBrush sb = new SolidBrush(Color.Blue);
            using (Graphics g = Graphics.FromImage(picture))
            {
                g.FillEllipse(sb, x, y, 50, 50);
            }
        }
    }
}
