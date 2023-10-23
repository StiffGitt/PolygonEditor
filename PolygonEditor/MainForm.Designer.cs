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
            numericUpDown = new NumericUpDown();
            offsetPolygonButton = new Button();
            deleteButton = new RadioButton();
            moveButton = new RadioButton();
            addButtom = new RadioButton();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            groupBox1.SuspendLayout();
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
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(684, 415);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.Control;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(3, 79);
            pictureBox.Margin = new Padding(3, 2, 3, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(678, 339);
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
            label1.Location = new Point(3, 75);
            label1.Name = "label1";
            label1.Size = new Size(678, 2);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numericUpDown);
            groupBox1.Controls.Add(offsetPolygonButton);
            groupBox1.Controls.Add(deleteButton);
            groupBox1.Controls.Add(moveButton);
            groupBox1.Controls.Add(addButtom);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 2);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(678, 71);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            // 
            // numericUpDown
            // 
            numericUpDown.Location = new Point(98, 37);
            numericUpDown.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numericUpDown.Name = "numericUpDown";
            numericUpDown.Size = new Size(47, 23);
            numericUpDown.TabIndex = 5;
            numericUpDown.TextAlign = HorizontalAlignment.Right;
            numericUpDown.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // offsetPolygonButton
            // 
            offsetPolygonButton.Location = new Point(151, 32);
            offsetPolygonButton.Name = "offsetPolygonButton";
            offsetPolygonButton.Size = new Size(58, 28);
            offsetPolygonButton.TabIndex = 3;
            offsetPolygonButton.Text = "Offset";
            offsetPolygonButton.UseVisualStyleBackColor = true;
            offsetPolygonButton.Click += offsetPolygonButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.AutoSize = true;
            deleteButton.Location = new Point(18, 43);
            deleteButton.Margin = new Padding(3, 2, 3, 2);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(58, 19);
            deleteButton.TabIndex = 2;
            deleteButton.TabStop = true;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            // 
            // moveButton
            // 
            moveButton.AutoSize = true;
            moveButton.Location = new Point(18, 25);
            moveButton.Margin = new Padding(3, 2, 3, 2);
            moveButton.Name = "moveButton";
            moveButton.Size = new Size(55, 19);
            moveButton.TabIndex = 1;
            moveButton.TabStop = true;
            moveButton.Text = "Move";
            moveButton.UseVisualStyleBackColor = true;
            // 
            // addButtom
            // 
            addButtom.AutoSize = true;
            addButtom.Location = new Point(18, 7);
            addButtom.Margin = new Padding(3, 2, 3, 2);
            addButtom.Name = "addButtom";
            addButtom.Size = new Size(47, 19);
            addButtom.TabIndex = 0;
            addButtom.TabStop = true;
            addButtom.Text = "Add";
            addButtom.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 415);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
    }
}