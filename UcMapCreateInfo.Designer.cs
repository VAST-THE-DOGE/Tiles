namespace Tiles
{
    partial class UcMapCreateInfo
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
            myTableLayoutPanel1 = new MyTableLayoutPanel();
            myTableLayoutPanel3 = new MyTableLayoutPanel();
            LabelDifficulty = new StandardLabel();
            standardLabel2 = new StandardLabel();
            myTableLayoutPanel2 = new MyTableLayoutPanel();
            standardLabel1 = new StandardLabel();
            richTextBox1 = new RichTextBox();
            myTableLayoutPanel1.SuspendLayout();
            myTableLayoutPanel3.SuspendLayout();
            myTableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // myTableLayoutPanel1
            // 
            myTableLayoutPanel1.ColumnCount = 3;
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            myTableLayoutPanel1.Controls.Add(myTableLayoutPanel3, 0, 1);
            myTableLayoutPanel1.Controls.Add(myTableLayoutPanel2, 0, 0);
            myTableLayoutPanel1.Controls.Add(richTextBox1, 1, 0);
            myTableLayoutPanel1.Dock = DockStyle.Fill;
            myTableLayoutPanel1.Location = new Point(0, 0);
            myTableLayoutPanel1.Name = "myTableLayoutPanel1";
            myTableLayoutPanel1.RowCount = 4;
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            myTableLayoutPanel1.Size = new Size(1000, 500);
            myTableLayoutPanel1.TabIndex = 0;
            // 
            // myTableLayoutPanel3
            // 
            myTableLayoutPanel3.BackColor = Color.Yellow;
            myTableLayoutPanel3.ColumnCount = 2;
            myTableLayoutPanel1.SetColumnSpan(myTableLayoutPanel3, 2);
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.Controls.Add(LabelDifficulty, 1, 0);
            myTableLayoutPanel3.Controls.Add(standardLabel2, 0, 0);
            myTableLayoutPanel3.Dock = DockStyle.Fill;
            myTableLayoutPanel3.Location = new Point(10, 145);
            myTableLayoutPanel3.Margin = new Padding(10, 20, 5, 20);
            myTableLayoutPanel3.Name = "myTableLayoutPanel3";
            myTableLayoutPanel3.RowCount = 1;
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.Size = new Size(651, 85);
            myTableLayoutPanel3.TabIndex = 1;
            // 
            // LabelDifficulty
            // 
            LabelDifficulty.AutoSize = true;
            LabelDifficulty.BackColor = Color.Wheat;
            LabelDifficulty.Dock = DockStyle.Fill;
            LabelDifficulty.Font = new Font("Segoe UI", 38.6577263F);
            LabelDifficulty.ForeColor = Color.Lime;
            LabelDifficulty.Location = new Point(325, 5);
            LabelDifficulty.Margin = new Padding(0, 5, 5, 5);
            LabelDifficulty.Name = "LabelDifficulty";
            LabelDifficulty.Size = new Size(321, 75);
            LabelDifficulty.TabIndex = 1;
            LabelDifficulty.Text = "Easy";
            LabelDifficulty.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // standardLabel2
            // 
            standardLabel2.AutoSize = true;
            standardLabel2.BackColor = Color.Wheat;
            standardLabel2.Dock = DockStyle.Fill;
            standardLabel2.Font = new Font("Segoe UI", 38.6577263F);
            standardLabel2.Location = new Point(5, 5);
            standardLabel2.Margin = new Padding(5, 5, 0, 5);
            standardLabel2.Name = "standardLabel2";
            standardLabel2.Size = new Size(320, 75);
            standardLabel2.TabIndex = 0;
            standardLabel2.Text = "Difficulty";
            standardLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myTableLayoutPanel2
            // 
            myTableLayoutPanel2.BackColor = Color.Yellow;
            myTableLayoutPanel2.ColumnCount = 1;
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.Controls.Add(standardLabel1, 0, 0);
            myTableLayoutPanel2.Dock = DockStyle.Fill;
            myTableLayoutPanel2.Location = new Point(10, 20);
            myTableLayoutPanel2.Margin = new Padding(10, 20, 5, 20);
            myTableLayoutPanel2.Name = "myTableLayoutPanel2";
            myTableLayoutPanel2.RowCount = 1;
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.Size = new Size(318, 85);
            myTableLayoutPanel2.TabIndex = 0;
            // 
            // standardLabel1
            // 
            standardLabel1.AutoSize = true;
            standardLabel1.BackColor = Color.Wheat;
            standardLabel1.Dock = DockStyle.Fill;
            standardLabel1.Font = new Font("Segoe UI", 37.775528F);
            standardLabel1.Location = new Point(5, 5);
            standardLabel1.Margin = new Padding(5);
            standardLabel1.Name = "standardLabel1";
            standardLabel1.Size = new Size(308, 75);
            standardLabel1.TabIndex = 0;
            standardLabel1.Text = "World Name";
            standardLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.Wheat;
            richTextBox1.BorderStyle = BorderStyle.None;
            myTableLayoutPanel1.SetColumnSpan(richTextBox1, 2);
            richTextBox1.DetectUrls = false;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox1.Location = new Point(343, 40);
            richTextBox1.Margin = new Padding(10, 40, 10, 40);
            richTextBox1.Multiline = false;
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ScrollBars = RichTextBoxScrollBars.None;
            richTextBox1.Size = new Size(647, 45);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            richTextBox1.WordWrap = false;
            // 
            // UcMapCreateInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(myTableLayoutPanel1);
            Name = "UcMapCreateInfo";
            Size = new Size(1000, 500);
            myTableLayoutPanel1.ResumeLayout(false);
            myTableLayoutPanel3.ResumeLayout(false);
            myTableLayoutPanel3.PerformLayout();
            myTableLayoutPanel2.ResumeLayout(false);
            myTableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MyTableLayoutPanel myTableLayoutPanel1;
        private MyTableLayoutPanel myTableLayoutPanel3;
        private StandardLabel standardLabel2;
        private MyTableLayoutPanel myTableLayoutPanel2;
        private StandardLabel standardLabel1;
        private StandardLabel LabelDifficulty;
        private RichTextBox richTextBox1;
    }
}
