using PolygonEditor.Structures;
namespace PolygonEditor
{
    public partial class MainForm : Form
    {
        private Canvas canvas;
        private ActionType curAction = ActionType.Default;

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
                    curAction = canvas.StartPainting(p, ShapeType.Polygon) ? ActionType.Painting : ActionType.Default;
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
                case ActionType.SettingHorizontal:
                    canvas.AddRelation(p, Relation.Horizontal);
                    curAction = ActionType.Default;
                    Cursor = System.Windows.Forms.Cursors.Default;
                    break;
                case ActionType.SettingVertical:
                    canvas.AddRelation(p, Relation.Vertical);
                    curAction = ActionType.Default;
                    Cursor = System.Windows.Forms.Cursors.Default;
                    break;
                case ActionType.RemovingRelation:
                    canvas.RemoveRelation(p);
                    curAction = ActionType.Default;
                    Cursor = System.Windows.Forms.Cursors.Default;
                    break;
                case ActionType.AddingCircle:
                    canvas.AddCircle(p, (Double.TryParse(radiusTextBox.Text, out double d) ? d : 50));
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
            if (curAction != ActionType.Painting)
            {
                curAction = ActionType.OffsettingPolygon;
                Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (canvas != null)
                canvas.Draw();
            pictureBox.Refresh();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            canvas.OffSetLastPolygon((int)numericUpDown.Value);
            pictureBox.Refresh();
        }

        private void resetActionButton_Click(object sender, EventArgs e)
        {
            curAction = ActionType.Default;
            Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void horizontalButton_Click(object sender, EventArgs e)
        {
            if (curAction != ActionType.Painting)
            {
                curAction = ActionType.SettingHorizontal;
                Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }

        private void verticalButton_Click(object sender, EventArgs e)
        {
            if (curAction != ActionType.Painting)
            {
                curAction = ActionType.SettingVertical;
                Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }

        private void removeRelationButton_Click(object sender, EventArgs e)
        {
            if (curAction != ActionType.Painting)
            {
                curAction = ActionType.RemovingRelation;
                Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }

        private void libraryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            canvas.lineAlgorithm = LineAlgorithm.Library;
            canvas.Draw();
            pictureBox.Refresh();
        }

        private void bresenhamRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            canvas.lineAlgorithm = LineAlgorithm.Brensenham;
            canvas.Draw();
            pictureBox.Refresh();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            pictureBox.Image = bitmap;
            this.canvas = new Canvas(bitmap, pictureBox, false);
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            if (curAction != ActionType.Painting)
            {
                curAction = ActionType.AddingCircle;
                Cursor = System.Windows.Forms.Cursors.Cross;
            }
        }

        private void radiusTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(radiusTextBox.Text, out double d))
                canvas.ChangeCircleRadius(d);
            canvas.Draw();
        }
    }
}