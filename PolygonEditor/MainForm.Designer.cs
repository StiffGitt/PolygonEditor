namespace PolygonEditor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBox = new PictureBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            clearButton = new Button();
            groupBox3 = new GroupBox();
            bresenhamRadioButton = new RadioButton();
            libraryRadioButton = new RadioButton();
            groupBox2 = new GroupBox();
            removeRelationButton = new Button();
            verticalButton = new Button();
            horizontalButton = new Button();
            resetActionButton = new Button();
            numericUpDown = new NumericUpDown();
            offsetPolygonButton = new Button();
            deleteButton = new RadioButton();
            moveButton = new RadioButton();
            addButtom = new RadioButton();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = SystemColors.Control;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(pictureBox, 0, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(782, 553);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.Control;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(3, 106);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(776, 452);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Location = new Point(3, 100);
            label1.Name = "label1";
            label1.Size = new Size(776, 3);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(clearButton);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(resetActionButton);
            groupBox1.Controls.Add(numericUpDown);
            groupBox1.Controls.Add(offsetPolygonButton);
            groupBox1.Controls.Add(deleteButton);
            groupBox1.Controls.Add(moveButton);
            groupBox1.Controls.Add(addButtom);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 94);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            // 
            // clearButton
            // 
            clearButton.Location = new Point(661, 19);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(109, 30);
            clearButton.TabIndex = 9;
            clearButton.Text = "Clear Canvas";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(bresenhamRadioButton);
            groupBox3.Controls.Add(libraryRadioButton);
            groupBox3.Location = new Point(519, 0);
            groupBox3.Margin = new Padding(3, 4, 3, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 4, 3, 4);
            groupBox3.Size = new Size(135, 93);
            groupBox3.TabIndex = 8;
            groupBox3.TabStop = false;
            groupBox3.Text = "Line algorithm";
            // 
            // bresenhamRadioButton
            // 
            bresenhamRadioButton.AutoSize = true;
            bresenhamRadioButton.Location = new Point(3, 59);
            bresenhamRadioButton.Margin = new Padding(3, 4, 3, 4);
            bresenhamRadioButton.Name = "bresenhamRadioButton";
            bresenhamRadioButton.Size = new Size(103, 24);
            bresenhamRadioButton.TabIndex = 1;
            bresenhamRadioButton.TabStop = true;
            bresenhamRadioButton.Text = "Bresenham";
            bresenhamRadioButton.UseVisualStyleBackColor = true;
            bresenhamRadioButton.CheckedChanged += bresenhamRadioButton_CheckedChanged;
            // 
            // libraryRadioButton
            // 
            libraryRadioButton.AutoSize = true;
            libraryRadioButton.Checked = true;
            libraryRadioButton.Location = new Point(3, 27);
            libraryRadioButton.Margin = new Padding(3, 4, 3, 4);
            libraryRadioButton.Name = "libraryRadioButton";
            libraryRadioButton.Size = new Size(75, 24);
            libraryRadioButton.TabIndex = 0;
            libraryRadioButton.TabStop = true;
            libraryRadioButton.Text = "Library";
            libraryRadioButton.UseVisualStyleBackColor = true;
            libraryRadioButton.CheckedChanged += libraryRadioButton_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(removeRelationButton);
            groupBox2.Controls.Add(verticalButton);
            groupBox2.Controls.Add(horizontalButton);
            groupBox2.Location = new Point(312, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(207, 93);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Relations";
            // 
            // removeRelationButton
            // 
            removeRelationButton.Location = new Point(123, 59);
            removeRelationButton.Name = "removeRelationButton";
            removeRelationButton.Size = new Size(77, 29);
            removeRelationButton.TabIndex = 2;
            removeRelationButton.Text = "remove";
            removeRelationButton.UseVisualStyleBackColor = true;
            removeRelationButton.Click += removeRelationButton_Click;
            // 
            // verticalButton
            // 
            verticalButton.Location = new Point(6, 59);
            verticalButton.Name = "verticalButton";
            verticalButton.Size = new Size(112, 29);
            verticalButton.TabIndex = 1;
            verticalButton.Text = "Vertical";
            verticalButton.UseVisualStyleBackColor = true;
            verticalButton.Click += verticalButton_Click;
            // 
            // horizontalButton
            // 
            horizontalButton.Location = new Point(6, 27);
            horizontalButton.Name = "horizontalButton";
            horizontalButton.Size = new Size(112, 29);
            horizontalButton.TabIndex = 0;
            horizontalButton.Text = "Horizontal";
            horizontalButton.UseVisualStyleBackColor = true;
            horizontalButton.Click += horizontalButton_Click;
            // 
            // resetActionButton
            // 
            resetActionButton.Location = new Point(660, 55);
            resetActionButton.Name = "resetActionButton";
            resetActionButton.Size = new Size(109, 30);
            resetActionButton.TabIndex = 6;
            resetActionButton.Text = "Reset Action";
            resetActionButton.UseVisualStyleBackColor = true;
            resetActionButton.Click += resetActionButton_Click;
            // 
            // numericUpDown
            // 
            numericUpDown.Location = new Point(137, 53);
            numericUpDown.Margin = new Padding(3, 4, 3, 4);
            numericUpDown.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numericUpDown.Name = "numericUpDown";
            numericUpDown.Size = new Size(66, 27);
            numericUpDown.TabIndex = 5;
            numericUpDown.TextAlign = HorizontalAlignment.Right;
            numericUpDown.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown.ValueChanged += numericUpDown_ValueChanged;
            // 
            // offsetPolygonButton
            // 
            offsetPolygonButton.Location = new Point(209, 48);
            offsetPolygonButton.Margin = new Padding(3, 4, 3, 4);
            offsetPolygonButton.Name = "offsetPolygonButton";
            offsetPolygonButton.Size = new Size(66, 37);
            offsetPolygonButton.TabIndex = 3;
            offsetPolygonButton.Text = "Offset";
            offsetPolygonButton.UseVisualStyleBackColor = true;
            offsetPolygonButton.Click += offsetPolygonButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.AutoSize = true;
            deleteButton.Location = new Point(21, 57);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(74, 24);
            deleteButton.TabIndex = 2;
            deleteButton.TabStop = true;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            // 
            // moveButton
            // 
            moveButton.AutoSize = true;
            moveButton.Location = new Point(21, 33);
            moveButton.Name = "moveButton";
            moveButton.Size = new Size(67, 24);
            moveButton.TabIndex = 1;
            moveButton.TabStop = true;
            moveButton.Text = "Move";
            moveButton.UseVisualStyleBackColor = true;
            // 
            // addButtom
            // 
            addButtom.AutoSize = true;
            addButtom.Location = new Point(21, 9);
            addButtom.Name = "addButtom";
            addButtom.Size = new Size(58, 24);
            addButtom.TabIndex = 0;
            addButtom.TabStop = true;
            addButtom.Text = "Add";
            addButtom.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 553);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Draw";
            Resize += MainForm_Resize;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox;
        private Label label1;
        private GroupBox groupBox1;
        private RadioButton deleteButton;
        private RadioButton moveButton;
        private RadioButton addButtom;
        private Button offsetPolygonButton;
        private NumericUpDown numericUpDown;
        private Button resetActionButton;
        private GroupBox groupBox2;
        private Button verticalButton;
        private Button horizontalButton;
        private Button removeRelationButton;
        private GroupBox groupBox3;
        private RadioButton bresenhamRadioButton;
        private RadioButton libraryRadioButton;
        private Button clearButton;
    }
}