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
        public Bitmap picture;
        private List<Shape> shapes;
        private Dictionary<ShapeType, Func<Color, Color, Color, Shape>> shapeCtors;
        private Color edgeColor = Color.DarkBlue;
        private Color vertexColor = Color.OrangeRed;
        private Color fillColor = Color.Aqua;
        private Color backGroundColor;
        private (Shape, int) curMovedVertex = (null, -1);
        private (Shape, int, Point) curMovedEdge = (null, -1, new Point());
        private (Shape, Point) curMovedShape = (null, new Point());

        public Canvas(Bitmap bitmap, Color backGroundColor)
        {
            shapes = new List<Shape>();
            this.picture = bitmap;
            this.backGroundColor = backGroundColor;
            InitializeDicts();
        }
        public ActionType GetActionOnMove(Point p)
        {
            curMovedVertex = GetVertex(p);
            if (curMovedVertex.Item2 >= 0)
                return ActionType.MovingVertex;
            curMovedEdge = GetEdge(p);
            if (curMovedEdge.Item2 >= 0)
                return ActionType.MovingEdge;
            curMovedShape = GetShape(p);
            if (curMovedEdge.Item2 >= 0)
                return ActionType.MovingShape;

            return ActionType.Default;
        }
        private (Shape, int) GetVertex(Point p)
        {
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                Shape shape = shapes[i];
                int idx = shape.IsOnVertex(p);
                if (idx >= 0)
                    return (shape, idx);
            }
            return (null, -1);
        }
        private (Shape, int, Point) GetEdge(Point p)
        {
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                Shape shape = shapes[i];
                int idx = shape.IsOnEdge(p);
                if(idx >= 0)
                    return (shape, idx, p);
            }
            return (null, -1, new Point());
        }
        private (Shape, Point) GetShape(Point p)
        {
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                Shape shape = shapes[i];
                if (shape.IsInside(p))
                    return (shape, p);
            }
            return (null, new Point());
        }
        public void StartPainting(Point p, ShapeType shape)
        {
            shapes.Add(shapeCtors[shape](edgeColor, vertexColor, fillColor));
            shapes.Last().AddPoint(p);
            Draw();
        }
        public bool AddPoint(Point p)
        {
            bool isFinished = shapes.Last().AddPoint(p);
            Draw();
            return isFinished;
        }
        public void MovePoint(Point p)
        {
            curMovedVertex.Item1.MovePoint(curMovedVertex.Item2 ,p);
            Draw();
        }
        public void MoveEdge(Point p)
        {
            curMovedEdge.Item1.MoveEdge(curMovedEdge.Item2, p, curMovedEdge.Item3);
            curMovedEdge.Item3 = p;
            Draw();
        }
        public void MoveShape(Point p)
        {
            curMovedShape.Item1.MoveShape(p, curMovedShape.Item2);
            curMovedShape.Item2 = p;
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
            shapeCtors = new Dictionary<ShapeType, Func<Color, Color, Color, Shape>>();
            var l = (Color c1, Color c2, Color c3) => new Polygon(c1, c2, c3);
            shapeCtors.Add(ShapeType.Polygon, (Color c1, Color c2, Color c3) => new Polygon(c1, c2, c3));
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
