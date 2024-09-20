namespace Tiles
{
    partial class UcWorldHeader
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            standardBackground1 = new StandardBackground();
            myTableLayoutPanel2 = new MyTableLayoutPanel();
            ButtonPlay = new StandardButton();
            ButtonCopy = new StandardButton();
            ButtonDelete = new StandardButton();
            myTableLayoutPanel3 = new MyTableLayoutPanel();
            LabelTime = new StandardLabel();
            LabelName = new StandardLabel();
            myTableLayoutPanel4 = new MyTableLayoutPanel();
            WorldImg = new PictureBox();
            myTableLayoutPanel1 = new MyTableLayoutPanel();
            standardBackground1.SuspendLayout();
            myTableLayoutPanel2.SuspendLayout();
            myTableLayoutPanel3.SuspendLayout();
            myTableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WorldImg).BeginInit();
            myTableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // standardBackground1
            // 
            standardBackground1.BackColor = Color.SaddleBrown;
            standardBackground1.Controls.Add(myTableLayoutPanel2);
            standardBackground1.Dock = DockStyle.Fill;
            standardBackground1.Location = new Point(3, 3);
            standardBackground1.Name = "standardBackground1";
            standardBackground1.Size = new Size(1049, 121);
            standardBackground1.TabIndex = 0;
            // 
            // myTableLayoutPanel2
            // 
            myTableLayoutPanel2.BackColor = Color.Transparent;
            myTableLayoutPanel2.ColumnCount = 5;
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            myTableLayoutPanel2.Controls.Add(ButtonPlay, 2, 0);
            myTableLayoutPanel2.Controls.Add(ButtonCopy, 3, 0);
            myTableLayoutPanel2.Controls.Add(ButtonDelete, 4, 0);
            myTableLayoutPanel2.Controls.Add(myTableLayoutPanel3, 1, 0);
            myTableLayoutPanel2.Controls.Add(myTableLayoutPanel4, 0, 0);
            myTableLayoutPanel2.Dock = DockStyle.Fill;
            myTableLayoutPanel2.Location = new Point(0, 0);
            myTableLayoutPanel2.Margin = new Padding(4);
            myTableLayoutPanel2.Name = "myTableLayoutPanel2";
            myTableLayoutPanel2.RowCount = 2;
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            myTableLayoutPanel2.Size = new Size(1049, 121);
            myTableLayoutPanel2.TabIndex = 0;
            // 
            // ButtonPlay
            // 
            ButtonPlay.BackColor = Color.Gray;
            ButtonPlay.Dock = DockStyle.Fill;
            ButtonPlay.FlatAppearance.BorderColor = Color.Yellow;
            ButtonPlay.FlatAppearance.BorderSize = 3;
            ButtonPlay.FlatStyle = FlatStyle.Flat;
            ButtonPlay.Font = new Font("Arial", 30.9532757F);
            ButtonPlay.ForeColor = Color.Lime;
            ButtonPlay.Location = new Point(722, 10);
            ButtonPlay.Margin = new Padding(10, 10, 0, 10);
            ButtonPlay.Name = "ButtonPlay";
            myTableLayoutPanel2.SetRowSpan(ButtonPlay, 2);
            ButtonPlay.Size = new Size(99, 101);
            ButtonPlay.TabIndex = 3;
            ButtonPlay.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            ButtonPlay.Text = "▶️";
            ButtonPlay.UseVisualStyleBackColor = false;
            // 
            // ButtonCopy
            // 
            ButtonCopy.BackColor = Color.Gray;
            ButtonCopy.Dock = DockStyle.Fill;
            ButtonCopy.FlatAppearance.BorderColor = Color.Yellow;
            ButtonCopy.FlatAppearance.BorderSize = 3;
            ButtonCopy.FlatStyle = FlatStyle.Flat;
            ButtonCopy.Font = new Font("Arial", 41.7679138F);
            ButtonCopy.ForeColor = Color.Orange;
            ButtonCopy.Location = new Point(831, 10);
            ButtonCopy.Margin = new Padding(10, 10, 0, 10);
            ButtonCopy.Name = "ButtonCopy";
            myTableLayoutPanel2.SetRowSpan(ButtonCopy, 2);
            ButtonCopy.Size = new Size(99, 101);
            ButtonCopy.TabIndex = 4;
            ButtonCopy.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            ButtonCopy.Text = "📄";
            ButtonCopy.UseVisualStyleBackColor = false;
            // 
            // ButtonDelete
            // 
            ButtonDelete.BackColor = Color.Gray;
            ButtonDelete.Dock = DockStyle.Fill;
            ButtonDelete.FlatAppearance.BorderColor = Color.Yellow;
            ButtonDelete.FlatAppearance.BorderSize = 3;
            ButtonDelete.FlatStyle = FlatStyle.Flat;
            ButtonDelete.Font = new Font("Arial", 41.7679138F);
            ButtonDelete.ForeColor = Color.Red;
            ButtonDelete.Location = new Point(940, 10);
            ButtonDelete.Margin = new Padding(10);
            ButtonDelete.Name = "ButtonDelete";
            myTableLayoutPanel2.SetRowSpan(ButtonDelete, 2);
            ButtonDelete.Size = new Size(99, 101);
            ButtonDelete.TabIndex = 5;
            ButtonDelete.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            ButtonDelete.Text = "🗑";
            ButtonDelete.UseVisualStyleBackColor = false;
            // 
            // myTableLayoutPanel3
            // 
            myTableLayoutPanel3.BackColor = Color.Yellow;
            myTableLayoutPanel3.ColumnCount = 1;
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.Controls.Add(LabelTime, 0, 1);
            myTableLayoutPanel3.Controls.Add(LabelName, 0, 0);
            myTableLayoutPanel3.Dock = DockStyle.Fill;
            myTableLayoutPanel3.Location = new Point(180, 10);
            myTableLayoutPanel3.Margin = new Padding(0, 10, 0, 10);
            myTableLayoutPanel3.Name = "myTableLayoutPanel3";
            myTableLayoutPanel3.RowCount = 2;
            myTableLayoutPanel2.SetRowSpan(myTableLayoutPanel3, 2);
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.Size = new Size(532, 101);
            myTableLayoutPanel3.TabIndex = 8;
            // 
            // LabelTime
            // 
            LabelTime.AutoSize = true;
            LabelTime.BackColor = Color.Wheat;
            LabelTime.Dock = DockStyle.Fill;
            LabelTime.Font = new Font("Segoe UI", 24.7409439F);
            LabelTime.Location = new Point(3, 50);
            LabelTime.Margin = new Padding(3, 0, 3, 3);
            LabelTime.Name = "LabelTime";
            LabelTime.Size = new Size(526, 48);
            LabelTime.TabIndex = 7;
            LabelTime.Text = "Day 0 - 10am";
            LabelTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelName
            // 
            LabelName.AutoSize = true;
            LabelName.BackColor = Color.Wheat;
            LabelName.Dock = DockStyle.Fill;
            LabelName.Font = new Font("Segoe UI", 24.2255077F);
            LabelName.Location = new Point(3, 3);
            LabelName.Margin = new Padding(3, 3, 3, 0);
            LabelName.Name = "LabelName";
            LabelName.Size = new Size(526, 47);
            LabelName.TabIndex = 6;
            LabelName.Text = "World -1";
            LabelName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // myTableLayoutPanel4
            // 
            myTableLayoutPanel4.BackColor = Color.Yellow;
            myTableLayoutPanel4.ColumnCount = 1;
            myTableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel4.Controls.Add(WorldImg, 0, 0);
            myTableLayoutPanel4.Dock = DockStyle.Fill;
            myTableLayoutPanel4.Location = new Point(10, 10);
            myTableLayoutPanel4.Margin = new Padding(10);
            myTableLayoutPanel4.Name = "myTableLayoutPanel4";
            myTableLayoutPanel4.RowCount = 1;
            myTableLayoutPanel2.SetRowSpan(myTableLayoutPanel4, 2);
            myTableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel4.Size = new Size(160, 101);
            myTableLayoutPanel4.TabIndex = 9;
            // 
            // WorldImg
            // 
            WorldImg.BackColor = Color.Black;
            WorldImg.Dock = DockStyle.Fill;
            WorldImg.Location = new Point(3, 3);
            WorldImg.Name = "WorldImg";
            myTableLayoutPanel4.SetRowSpan(WorldImg, 2);
            WorldImg.Size = new Size(154, 95);
            WorldImg.TabIndex = 0;
            WorldImg.TabStop = false;
            // 
            // myTableLayoutPanel1
            // 
            myTableLayoutPanel1.ColumnCount = 1;
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel1.Controls.Add(standardBackground1, 0, 0);
            myTableLayoutPanel1.Dock = DockStyle.Fill;
            myTableLayoutPanel1.Location = new Point(0, 0);
            myTableLayoutPanel1.Margin = new Padding(4);
            myTableLayoutPanel1.Name = "myTableLayoutPanel1";
            myTableLayoutPanel1.RowCount = 1;
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel1.Size = new Size(1055, 127);
            myTableLayoutPanel1.TabIndex = 0;
            // 
            // UcWorldHeader
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Yellow;
            Controls.Add(myTableLayoutPanel1);
            Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "UcWorldHeader";
            Size = new Size(1055, 127);
            standardBackground1.ResumeLayout(false);
            myTableLayoutPanel2.ResumeLayout(false);
            myTableLayoutPanel3.ResumeLayout(false);
            myTableLayoutPanel3.PerformLayout();
            myTableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)WorldImg).EndInit();
            myTableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private StandardBackground standardBackground1;
        private MyTableLayoutPanel myTableLayoutPanel2;
        private PictureBox WorldImg;
        private MyTableLayoutPanel myTableLayoutPanel1;
        private StandardButton ButtonPlay;
        private StandardButton ButtonCopy;
        private StandardButton ButtonDelete;
        private StandardLabel LabelName;
        private StandardLabel LabelTime;
        private MyTableLayoutPanel myTableLayoutPanel3;
        private MyTableLayoutPanel myTableLayoutPanel4;
    }
}
