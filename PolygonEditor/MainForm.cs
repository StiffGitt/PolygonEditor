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
            this.canvas = new Canvas(bitmap, pictureBox);
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
                if (deleteButton.Checked)
                    curAction = ActionType.RemovingVertex;
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
                case ActionType.MovingEdge:
                    canvas.MoveEdge(p);
                    break;
                case ActionType.MovingShape:
                    canvas.MoveShape(p);
                    break;
                case ActionType.RemovingVertex:
                    canvas.RemoveVertex(p);
                    curAction = ActionType.Default;
                    break;
                case ActionType.OffsettingPolygon:
                    canvas.OffSetPolygon(p, (int)numericUpDown.Value);
                    curAction = ActionType.Default;
                    Cursor = System.Windows.Forms.Cursors.Default;
                    break;
                default:
                    break;
            }
            pictureBox.Refresh();
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
                case ActionType.MovingEdge:
                    canvas.MoveEdge(p);
                    break;
                case ActionType.MovingShape:
                    canvas.MoveShape(p);
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
                case ActionType.MovingEdge:
                    curAction = ActionType.Default;
                    break;
                case ActionType.MovingShape:
                    curAction = ActionType.Default;
                    break;
                default:
                    break;
            }
        }
        private void offsetPolygonButton_Click(object sender, EventArgs e)
        {
            if (curAction == ActionType.Default)
            {
                curAction = ActionType.OffsettingPolygon;
                Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }
    }
}