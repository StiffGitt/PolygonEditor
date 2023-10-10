using PolygonEditor.Structures;
namespace PolygonEditor
{
    public partial class MainForm : Form
    {
        private Canvas canvas;
        private Actions curAction = Actions.Default;
        public MainForm()
        {
            InitializeComponent();
            Bitmap bitmap = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            pictureBox.Image = bitmap;
            this.canvas = new Canvas(bitmap, pictureBox.BackColor);
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            switch (curAction)
            {
                case Actions.Default:
                    canvas.StartPainting(p, ShapeTypes.Polygon);
                    curAction = Actions.Painting;
                    break;
                case Actions.Painting:
                    curAction = canvas.AddPoint(p) ? Actions.Default : Actions.Painting;
                    break;
            }
            pictureBox.Refresh();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (curAction == Actions.Painting)
            {
                canvas.Draw(new Point(e.X, e.Y));
                pictureBox.Refresh();
            }
        }
    }
}