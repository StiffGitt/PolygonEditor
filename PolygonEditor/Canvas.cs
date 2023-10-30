using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using PolygonEditor.Structures;

namespace PolygonEditor
{
    public class Canvas
    {
        public Bitmap picture;
        private PictureBox pictureBox;
        private List<Shape> shapes;
        private Dictionary<ShapeType, Func<Color, Color, Color, Color, Shape>> shapeCtors;
        private Color edgeColor = Color.DarkBlue;
        private Color vertexColor = Color.OrangeRed;
        private Color fillColor = Color.Aqua;
        private Color hullColor = Color.Green;
        private Color backGroundColor;
        private (Shape, int) curMovedVertex = (null, -1);
        private (Shape, int, Point) curMovedEdge = (null, -1, new Point());
        private (Shape, Point) curMovedShape = (null, new Point());
        private Shape lastOffsettedShape = null;
        public LineAlgorithm lineAlgorithm = LineAlgorithm.Library;

        public Canvas(Bitmap bitmap, PictureBox pictureBox, bool withPredifined = true)
        {
            shapes = new List<Shape>();
            this.picture = bitmap;
            this.backGroundColor = pictureBox.BackColor;
            this.pictureBox = pictureBox;
            InitializeDicts();
            if(withPredifined)
                Utils.AddPredefinedShapes(shapes, picture, edgeColor, vertexColor, fillColor, hullColor);
            Draw();
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
            if (curMovedShape.Item1 != null)
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
        public bool StartPainting(Point p, ShapeType shape)
        {
            var e = GetEdge(p);
            bool isNew = false;
            if (e.Item2 >= 0 && e.Item1 is Polygon)
                ((Polygon)e.Item1).AddPointAfter(p, e.Item2);
            else
            {
                shapes.Add(shapeCtors[shape](edgeColor, vertexColor, fillColor, hullColor));
                shapes.Last().AddPoint(p);
                isNew = true;
            }
            Draw();
            return isNew;
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
        public void RemoveVertex(Point p)
        {
            var v = GetVertex(p);
            if (v.Item2 >= 0)
            {
                bool isGone = v.Item1.RemoveVertex(v.Item2);
                if (isGone)
                    shapes.Remove(v.Item1);
            }
            Draw();
        }
        public void AddRelation(Point p, Relation r)
        {
            var e = GetEdge(p);
            if (e.Item2 >= 0 && e.Item1 is Polygon)
                ((Polygon)e.Item1).AddRelation(e.Item2, r);
            Draw();
        }
        public void RemoveRelation(Point p)
        {
            var e = GetEdge(p);
            if (e.Item2 >= 0 && e.Item1 is Polygon)
                ((Polygon)e.Item1).RemoveRelation(e.Item2);
            Draw();
        }
        public void OffSetPolygon(Point p, int offset)
        {
            Shape s;
            s = GetShape((Point)p).Item1;
            if (s != null)
                s.Inflate(offset);
            Draw();
            lastOffsettedShape = s;
        }
        public void OffSetLastPolygon(int offset)
        {
            var s = lastOffsettedShape;
            if (s != null)
                s.Inflate(offset);
            Draw();
        }
        public void Draw(Point? p = null)
        {
            Clear();
            picture = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = picture;
            foreach (Shape shape in shapes)
            {
                shape.Draw(picture, lineAlgorithm, p);
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
            shapeCtors = new Dictionary<ShapeType, Func<Color, Color, Color, Color, Shape>>();
            var l = (Color c1, Color c2, Color c3, Color c4) => new Polygon(c1, c2, c3, c4);
            shapeCtors.Add(ShapeType.Polygon, (Color c1, Color c2, Color c3, Color c4) => new Polygon(c1, c2, c3, c4));
        }
    }
}
