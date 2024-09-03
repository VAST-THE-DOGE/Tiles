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
            myTableLayoutPanel1 = new MyTableLayoutPanel();
            myTableLayoutPanel2 = new MyTableLayoutPanel();
            WorldImg = new PictureBox();
            ButtonPlay = new StandardButton();
            ButtonCopy = new StandardButton();
            ButtonDelete = new StandardButton();
            LabelName = new StandardLabel();
            LabelTime = new StandardLabel();
            standardBackground1.SuspendLayout();
            myTableLayoutPanel1.SuspendLayout();
            myTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WorldImg).BeginInit();
            SuspendLayout();
            // 
            // standardBackground1
            // 
            standardBackground1.BackColor = Color.SaddleBrown;
            standardBackground1.Controls.Add(myTableLayoutPanel2);
            standardBackground1.Dock = DockStyle.Fill;
            standardBackground1.Location = new Point(4, 4);
            standardBackground1.Margin = new Padding(4, 4, 4, 4);
            standardBackground1.Name = "standardBackground1";
            standardBackground1.Size = new Size(1047, 106);
            standardBackground1.TabIndex = 0;
            // 
            // myTableLayoutPanel1
            // 
            myTableLayoutPanel1.ColumnCount = 1;
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel1.Controls.Add(standardBackground1, 0, 0);
            myTableLayoutPanel1.Dock = DockStyle.Fill;
            myTableLayoutPanel1.Location = new Point(0, 0);
            myTableLayoutPanel1.Margin = new Padding(4, 4, 4, 4);
            myTableLayoutPanel1.Name = "myTableLayoutPanel1";
            myTableLayoutPanel1.RowCount = 1;
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel1.Size = new Size(1055, 114);
            myTableLayoutPanel1.TabIndex = 0;
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
            myTableLayoutPanel2.Controls.Add(LabelTime, 1, 1);
            myTableLayoutPanel2.Controls.Add(WorldImg, 0, 0);
            myTableLayoutPanel2.Controls.Add(ButtonPlay, 2, 0);
            myTableLayoutPanel2.Controls.Add(ButtonCopy, 3, 0);
            myTableLayoutPanel2.Controls.Add(ButtonDelete, 4, 0);
            myTableLayoutPanel2.Controls.Add(LabelName, 1, 0);
            myTableLayoutPanel2.Dock = DockStyle.Fill;
            myTableLayoutPanel2.Location = new Point(0, 0);
            myTableLayoutPanel2.Margin = new Padding(4, 4, 4, 4);
            myTableLayoutPanel2.Name = "myTableLayoutPanel2";
            myTableLayoutPanel2.RowCount = 2;
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.Size = new Size(1047, 106);
            myTableLayoutPanel2.TabIndex = 0;
            // 
            // WorldImg
            // 
            WorldImg.BackColor = Color.Black;
            WorldImg.BorderStyle = BorderStyle.FixedSingle;
            WorldImg.Dock = DockStyle.Fill;
            WorldImg.Location = new Point(10, 10);
            WorldImg.Margin = new Padding(10);
            WorldImg.Name = "WorldImg";
            myTableLayoutPanel2.SetRowSpan(WorldImg, 2);
            WorldImg.Size = new Size(160, 86);
            WorldImg.TabIndex = 0;
            WorldImg.TabStop = false;
            // 
            // ButtonPlay
            // 
            ButtonPlay.BackColor = Color.Gray;
            ButtonPlay.Dock = DockStyle.Fill;
            ButtonPlay.FlatAppearance.BorderColor = Color.Yellow;
            ButtonPlay.FlatAppearance.BorderSize = 3;
            ButtonPlay.FlatStyle = FlatStyle.Flat;
            ButtonPlay.Font = new Font("Arial", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ButtonPlay.ForeColor = Color.Lime;
            ButtonPlay.Location = new Point(759, 10);
            ButtonPlay.Margin = new Padding(10, 10, 0, 10);
            ButtonPlay.Name = "ButtonPlay";
            myTableLayoutPanel2.SetRowSpan(ButtonPlay, 2);
            ButtonPlay.Size = new Size(86, 86);
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
            ButtonCopy.Font = new Font("Arial", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ButtonCopy.ForeColor = Color.Orange;
            ButtonCopy.Location = new Point(855, 10);
            ButtonCopy.Margin = new Padding(10, 10, 0, 10);
            ButtonCopy.Name = "ButtonCopy";
            myTableLayoutPanel2.SetRowSpan(ButtonCopy, 2);
            ButtonCopy.Size = new Size(86, 86);
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
            ButtonDelete.Font = new Font("Arial", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ButtonDelete.ForeColor = Color.Red;
            ButtonDelete.Location = new Point(951, 10);
            ButtonDelete.Margin = new Padding(10);
            ButtonDelete.Name = "ButtonDelete";
            myTableLayoutPanel2.SetRowSpan(ButtonDelete, 2);
            ButtonDelete.Size = new Size(86, 86);
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
            // LabelName
            // 
            LabelName.AutoSize = true;
            LabelName.BackColor = Color.Wheat;
            LabelName.Dock = DockStyle.Fill;
            LabelName.Font = new Font("Segoe UI", 22.1637611F);
            LabelName.Location = new Point(180, 10);
            LabelName.Margin = new Padding(0, 10, 0, 0);
            LabelName.Name = "LabelName";
            LabelName.Size = new Size(569, 43);
            LabelName.TabIndex = 6;
            LabelName.Text = "World -1";
            LabelName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelTime
            // 
            LabelTime.AutoSize = true;
            LabelTime.BackColor = Color.Wheat;
            LabelTime.Dock = DockStyle.Fill;
            LabelTime.Font = new Font("Segoe UI", 22.1637611F);
            LabelTime.Location = new Point(180, 53);
            LabelTime.Margin = new Padding(0, 0, 0, 10);
            LabelTime.Name = "LabelTime";
            LabelTime.Size = new Size(569, 43);
            LabelTime.TabIndex = 7;
            LabelTime.Text = "Day 0 - 10am";
            LabelTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // UcWorldHeader
            // 
            AutoScaleDimensions = new SizeF(9F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Yellow;
            Controls.Add(myTableLayoutPanel1);
            Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 4, 4, 4);
            Name = "UcWorldHeader";
            Size = new Size(1055, 114);
            standardBackground1.ResumeLayout(false);
            myTableLayoutPanel1.ResumeLayout(false);
            myTableLayoutPanel2.ResumeLayout(false);
            myTableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)WorldImg).EndInit();
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
    }
}
