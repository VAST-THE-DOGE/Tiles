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
            myTableLayoutPanel2 = new MyTableLayoutPanel();
            standardLabel1 = new StandardLabel();
            myTableLayoutPanel3 = new MyTableLayoutPanel();
            standardLabel2 = new StandardLabel();
            standardButton1 = new StandardButton();
            standardButton2 = new StandardButton();
            standardButton3 = new StandardButton();
            NameInput = new TextBox();
            myTableLayoutPanel1.SuspendLayout();
            myTableLayoutPanel2.SuspendLayout();
            myTableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // myTableLayoutPanel1
            // 
            myTableLayoutPanel1.ColumnCount = 3;
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            myTableLayoutPanel1.Controls.Add(standardButton3, 1, 3);
            myTableLayoutPanel1.Controls.Add(standardButton2, 2, 3);
            myTableLayoutPanel1.Controls.Add(standardButton1, 0, 3);
            myTableLayoutPanel1.Controls.Add(myTableLayoutPanel3, 1, 2);
            myTableLayoutPanel1.Controls.Add(myTableLayoutPanel2, 1, 0);
            myTableLayoutPanel1.Controls.Add(NameInput, 0, 1);
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
            // myTableLayoutPanel2
            // 
            myTableLayoutPanel2.BackColor = Color.Yellow;
            myTableLayoutPanel2.ColumnCount = 1;
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.Controls.Add(standardLabel1, 0, 0);
            myTableLayoutPanel2.Dock = DockStyle.Fill;
            myTableLayoutPanel2.Location = new Point(333, 40);
            myTableLayoutPanel2.Margin = new Padding(0, 40, 0, 5);
            myTableLayoutPanel2.Name = "myTableLayoutPanel2";
            myTableLayoutPanel2.RowCount = 1;
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel2.Size = new Size(333, 80);
            myTableLayoutPanel2.TabIndex = 0;
            // 
            // standardLabel1
            // 
            standardLabel1.AutoSize = true;
            standardLabel1.BackColor = Color.Wheat;
            standardLabel1.Dock = DockStyle.Fill;
            standardLabel1.Font = new Font("Segoe UI", 36.0805435F);
            standardLabel1.Location = new Point(5, 5);
            standardLabel1.Margin = new Padding(5);
            standardLabel1.Name = "standardLabel1";
            standardLabel1.Size = new Size(323, 70);
            standardLabel1.TabIndex = 0;
            standardLabel1.Text = "World Name";
            standardLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myTableLayoutPanel3
            // 
            myTableLayoutPanel3.BackColor = Color.Yellow;
            myTableLayoutPanel3.ColumnCount = 1;
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.Controls.Add(standardLabel2, 0, 0);
            myTableLayoutPanel3.Dock = DockStyle.Fill;
            myTableLayoutPanel3.Location = new Point(333, 270);
            myTableLayoutPanel3.Margin = new Padding(0, 20, 0, 15);
            myTableLayoutPanel3.Name = "myTableLayoutPanel3";
            myTableLayoutPanel3.RowCount = 1;
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            myTableLayoutPanel3.Size = new Size(333, 90);
            myTableLayoutPanel3.TabIndex = 1;
            // 
            // standardLabel2
            // 
            standardLabel2.AutoSize = true;
            standardLabel2.BackColor = Color.Wheat;
            standardLabel2.Dock = DockStyle.Fill;
            standardLabel2.Font = new Font("Segoe UI", 41.23491F);
            standardLabel2.Location = new Point(5, 5);
            standardLabel2.Margin = new Padding(5);
            standardLabel2.Name = "standardLabel2";
            standardLabel2.Size = new Size(323, 80);
            standardLabel2.TabIndex = 0;
            standardLabel2.Text = "Difficulty";
            standardLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // standardButton1
            // 
            standardButton1.BackColor = SystemColors.ButtonShadow;
            standardButton1.Dock = DockStyle.Fill;
            standardButton1.FlatAppearance.BorderColor = Color.Yellow;
            standardButton1.FlatAppearance.BorderSize = 3;
            standardButton1.FlatStyle = FlatStyle.Flat;
            standardButton1.Font = new Font("Segoe UI", 36.0805435F);
            standardButton1.Location = new Point(20, 380);
            standardButton1.Margin = new Padding(20, 5, 0, 20);
            standardButton1.Name = "standardButton1";
            standardButton1.Size = new Size(313, 100);
            standardButton1.TabIndex = 2;
            standardButton1.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            standardButton1.Text = "Easy";
            standardButton1.UseVisualStyleBackColor = false;
            // 
            // standardButton2
            // 
            standardButton2.BackColor = SystemColors.ButtonShadow;
            standardButton2.Dock = DockStyle.Fill;
            standardButton2.FlatAppearance.BorderColor = Color.Yellow;
            standardButton2.FlatAppearance.BorderSize = 3;
            standardButton2.FlatStyle = FlatStyle.Flat;
            standardButton2.Font = new Font("Segoe UI", 36.0805435F);
            standardButton2.Location = new Point(666, 380);
            standardButton2.Margin = new Padding(0, 5, 20, 20);
            standardButton2.Name = "standardButton2";
            standardButton2.Size = new Size(314, 100);
            standardButton2.TabIndex = 3;
            standardButton2.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            standardButton2.Text = "Hard";
            standardButton2.UseVisualStyleBackColor = false;
            // 
            // standardButton3
            // 
            standardButton3.BackColor = SystemColors.ButtonShadow;
            standardButton3.Dock = DockStyle.Fill;
            standardButton3.FlatAppearance.BorderColor = Color.Yellow;
            standardButton3.FlatAppearance.BorderSize = 3;
            standardButton3.FlatStyle = FlatStyle.Flat;
            standardButton3.Font = new Font("Segoe UI", 36.0805435F);
            standardButton3.Location = new Point(343, 380);
            standardButton3.Margin = new Padding(10, 5, 10, 20);
            standardButton3.Name = "standardButton3";
            standardButton3.Size = new Size(313, 100);
            standardButton3.TabIndex = 4;
            standardButton3.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            standardButton3.Text = "Normal";
            standardButton3.UseVisualStyleBackColor = false;
            // 
            // NameInput
            // 
            NameInput.BackColor = Color.White;
            NameInput.BorderStyle = BorderStyle.None;
            myTableLayoutPanel1.SetColumnSpan(NameInput, 3);
            NameInput.Dock = DockStyle.Fill;
            NameInput.Font = new Font("Arial", 24F);
            NameInput.Location = new Point(50, 135);
            NameInput.Margin = new Padding(50, 10, 50, 5);
            NameInput.MaxLength = 200;
            NameInput.Name = "NameInput";
            NameInput.PlaceholderText = "Type Here";
            NameInput.Size = new Size(900, 37);
            NameInput.TabIndex = 5;
            NameInput.TextAlign = HorizontalAlignment.Center;
            NameInput.WordWrap = false;
            // 
            // UcMapCreateInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(myTableLayoutPanel1);
            Name = "UcMapCreateInfo";
            Size = new Size(1000, 500);
            myTableLayoutPanel1.ResumeLayout(false);
            myTableLayoutPanel1.PerformLayout();
            myTableLayoutPanel2.ResumeLayout(false);
            myTableLayoutPanel2.PerformLayout();
            myTableLayoutPanel3.ResumeLayout(false);
            myTableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MyTableLayoutPanel myTableLayoutPanel1;
        private MyTableLayoutPanel myTableLayoutPanel3;
        private StandardLabel standardLabel2;
        private MyTableLayoutPanel myTableLayoutPanel2;
        private StandardLabel standardLabel1;
        private StandardButton standardButton3;
        private StandardButton standardButton2;
        private StandardButton standardButton1;
        private TextBox NameInput;
    }
}
