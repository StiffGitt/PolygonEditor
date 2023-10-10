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
        public abstract void Draw(Bitmap picture, Point? p = null);
        public abstract bool AddPoint(Point p); // returns true if drawing is finished

    }
}
