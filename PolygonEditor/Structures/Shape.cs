using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonEditor.Structures
{
    public abstract class Shape
    {
        protected const int vertexRadius = 10;
        protected bool isInflated = false;
        protected int inflationOffset = 0;
        public abstract void Draw(Bitmap picture, LineAlgorithm lineAlgorithm, Point? p = null);
        public abstract bool AddPoint(Point p); // returns true if drawing is finished
        public abstract void MovePoint(int idx, Point p);
        public abstract void MoveEdge(int idx, Point p, Point prevP);
        public abstract void MoveShape(Point p, Point prevP);
        public abstract bool RemoveVertex(int idx);
        public abstract int IsOnVertex(Point p);
        public abstract int IsOnEdge(Point p); // returns smaller edge idx
        public abstract bool IsInside(Point p);
        public void Inflate(int offset)
        {
            isInflated = true;
            inflationOffset = offset;
        }
    }
}
