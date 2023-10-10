using PolygonEditor.Structures;
namespace PolygonEditor
{
    public partial class MainForm : Form
    {
        private Canvas canvas;
        private ActionType curAction = ActionType.AddNewShape;

        public MainForm()
        {
            InitializeComponent();
            Bitmap bitmap = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            pictureBox.Image = bitmap;
            this.canvas = new Canvas(bitmap, pictureBox.BackColor);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            switch (curAction)
            {
                case ActionType.Painting:
                    canvas.Draw(p);
                    break;
                case ActionType.MovingVertex:
                    canvas.MovePoint(p);
                    break;
                default:
                    break;
            }
            pictureBox.Refresh();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            if (curAction == ActionType.Default)
            {
                if (addButtom.Checked)
                    curAction = ActionType.AddNewShape;
                if (moveButton.Checked)
                    curAction = canvas.GetActionOnMove(p);
            }
            
            switch (curAction)
            {
                case ActionType.AddNewShape:
                    canvas.StartPainting(p, ShapeType.Polygon);
                    curAction = ActionType.Painting;
                    break;
                case ActionType.Painting:
                    curAction = canvas.AddPoint(p) ? ActionType.Default : ActionType.Painting;
                    break;
                case ActionType.MovingVertex:
                    canvas.MovePoint(p);
                    break;
                default:
                    break;
            }
            pictureBox.Refresh();
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            switch (curAction)
            {
                case ActionType.MovingVertex:
                    curAction = ActionType.Default;
                    break;
                default:
                    break;
            }
        }

    }
}