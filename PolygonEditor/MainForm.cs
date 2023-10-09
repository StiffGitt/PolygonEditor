namespace PolygonEditor
{
    public partial class MainForm : Form
    {
        private Canvas canvas;
        public MainForm()
        {
            InitializeComponent();
            Bitmap bitmap =  new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            pictureBox.Image = bitmap;
            this.canvas = new Canvas(bitmap);
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            canvas.TestDrawEllipse(e.X, e.Y);
            pictureBox.Refresh();
        }
    }
}