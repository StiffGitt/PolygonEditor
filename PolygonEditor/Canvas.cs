using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolygonEditor.Structures;

namespace PolygonEditor
{
    public class Canvas
    {
        public bool IsPainting { get; set; }
        public Bitmap picture;
        private List<Shape> shapes;

        public Canvas(Bitmap bitmap)
        {
            shapes = new List<Shape>();
            this.picture = bitmap;
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
