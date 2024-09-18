namespace Tiles
{
    partial class UcNewWorldPanel
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
            myTableLayoutPanel3 = new MyTableLayoutPanel();
            panelLine = new Panel();
            Button1 = new StandardButton();
            Button2 = new StandardButton();
            Button3 = new StandardButton();
            Button4 = new StandardButton();
            Button5 = new StandardButton();
            myTableLayoutPanel1.SuspendLayout();
            myTableLayoutPanel2.SuspendLayout();
            myTableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // myTableLayoutPanel1
            // 
            myTableLayoutPanel1.ColumnCount = 3;
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            myTableLayoutPanel1.Controls.Add(myTableLayoutPanel2, 0, 0);
            myTableLayoutPanel1.Controls.Add(panelLine, 1, 0);
            myTableLayoutPanel1.Dock = DockStyle.Fill;
            myTableLayoutPanel1.Location = new Point(25, 25);
            myTableLayoutPanel1.Margin = new Padding(0);
            myTableLayoutPanel1.Name = "myTableLayoutPanel1";
            myTableLayoutPanel1.RowCount = 1;
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            myTableLayoutPanel1.Size = new Size(1085, 516);
            myTableLayoutPanel1.TabIndex = 0;
            // 
            // myTableLayoutPanel2
            // 
            myTableLayoutPanel2.ColumnCount = 1;
            myTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            myTableLayoutPanel2.Controls.Add(Button2, 0, 1);
            myTableLayoutPanel2.Controls.Add(Button3, 0, 2);
            myTableLayoutPanel2.Controls.Add(Button4, 0, 3);
            myTableLayoutPanel2.Controls.Add(Button5, 0, 4);
            myTableLayoutPanel2.Controls.Add(Button1, 0, 0);
            myTableLayoutPanel2.Dock = DockStyle.Fill;
            myTableLayoutPanel2.Location = new Point(0, 0);
            myTableLayoutPanel2.Margin = new Padding(0);
            myTableLayoutPanel2.Name = "myTableLayoutPanel2";
            myTableLayoutPanel2.RowCount = 5;
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            myTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            myTableLayoutPanel2.Size = new Size(270, 516);
            myTableLayoutPanel2.TabIndex = 0;
            // 
            // myTableLayoutPanel3
            // 
            myTableLayoutPanel3.ColumnCount = 3;
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 25F));
            myTableLayoutPanel3.Controls.Add(myTableLayoutPanel1, 1, 1);
            myTableLayoutPanel3.Dock = DockStyle.Fill;
            myTableLayoutPanel3.Location = new Point(0, 0);
            myTableLayoutPanel3.Name = "myTableLayoutPanel3";
            myTableLayoutPanel3.RowCount = 3;
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            myTableLayoutPanel3.Size = new Size(1135, 566);
            myTableLayoutPanel3.TabIndex = 1;
            // 
            // panelLine
            // 
            panelLine.BackColor = Color.Yellow;
            panelLine.Dock = DockStyle.Fill;
            panelLine.Location = new Point(270, 0);
            panelLine.Margin = new Padding(0);
            panelLine.Name = "panelLine";
            panelLine.Size = new Size(5, 516);
            panelLine.TabIndex = 1;
            // 
            // Button1
            // 
            Button1.BackColor = SystemColors.ControlDark;
            Button1.Dock = DockStyle.Fill;
            Button1.FlatAppearance.BorderColor = Color.Yellow;
            Button1.FlatAppearance.BorderSize = 3;
            Button1.FlatStyle = FlatStyle.Flat;
            Button1.Font = new Font("Segoe UI", 30.0774841F);
            Button1.Location = new Point(5, 5);
            Button1.Margin = new Padding(5);
            Button1.Name = "Button1";
            Button1.Size = new Size(260, 93);
            Button1.TabIndex = 0;
            Button1.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            Button1.Text = "1. Basic Info";
            Button1.UseVisualStyleBackColor = false;
            // 
            // Button2
            // 
            Button2.BackColor = SystemColors.ControlDark;
            Button2.Dock = DockStyle.Fill;
            Button2.FlatAppearance.BorderColor = Color.Yellow;
            Button2.FlatAppearance.BorderSize = 3;
            Button2.FlatStyle = FlatStyle.Flat;
            Button2.Font = new Font("Segoe UI", 23.3408985F);
            Button2.Location = new Point(5, 108);
            Button2.Margin = new Padding(5);
            Button2.Name = "Button2";
            Button2.Size = new Size(260, 93);
            Button2.TabIndex = 1;
            Button2.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            Button2.Text = "2. Map Settings";
            Button2.UseVisualStyleBackColor = false;
            // 
            // Button3
            // 
            Button3.BackColor = SystemColors.ControlDark;
            Button3.Dock = DockStyle.Fill;
            Button3.FlatAppearance.BorderColor = Color.Yellow;
            Button3.FlatAppearance.BorderSize = 3;
            Button3.FlatStyle = FlatStyle.Flat;
            Button3.Font = new Font("Segoe UI", 26.7379837F);
            Button3.Location = new Point(5, 211);
            Button3.Margin = new Padding(5);
            Button3.Name = "Button3";
            Button3.Size = new Size(260, 93);
            Button3.TabIndex = 2;
            Button3.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            Button3.Text = "3. Goverment";
            Button3.UseVisualStyleBackColor = false;
            // 
            // Button4
            // 
            Button4.BackColor = SystemColors.ControlDark;
            Button4.Dock = DockStyle.Fill;
            Button4.FlatAppearance.BorderColor = Color.Yellow;
            Button4.FlatAppearance.BorderSize = 3;
            Button4.FlatStyle = FlatStyle.Flat;
            Button4.Font = new Font("Segoe UI", 23.2380333F);
            Button4.Location = new Point(5, 314);
            Button4.Margin = new Padding(5);
            Button4.Name = "Button4";
            Button4.Size = new Size(260, 93);
            Button4.TabIndex = 3;
            Button4.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            Button4.Text = "4. Misc Settings";
            Button4.UseVisualStyleBackColor = false;
            // 
            // Button5
            // 
            Button5.BackColor = SystemColors.ControlDark;
            Button5.Dock = DockStyle.Fill;
            Button5.FlatAppearance.BorderColor = Color.Yellow;
            Button5.FlatAppearance.BorderSize = 3;
            Button5.FlatStyle = FlatStyle.Flat;
            Button5.Font = new Font("Segoe UI", 30.8847256F);
            Button5.Location = new Point(5, 417);
            Button5.Margin = new Padding(5);
            Button5.Name = "Button5";
            Button5.Size = new Size(260, 94);
            Button5.TabIndex = 4;
            Button5.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            Button5.Text = "5. Overview";
            Button5.UseVisualStyleBackColor = false;
            // 
            // UcNewWorldPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(myTableLayoutPanel3);
            Name = "UcNewWorldPanel";
            Size = new Size(1135, 566);
            myTableLayoutPanel1.ResumeLayout(false);
            myTableLayoutPanel2.ResumeLayout(false);
            myTableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MyTableLayoutPanel myTableLayoutPanel1;
        private MyTableLayoutPanel myTableLayoutPanel2;
        private MyTableLayoutPanel myTableLayoutPanel3;
        private Panel panelLine;
        private StandardButton Button2;
        private StandardButton Button3;
        private StandardButton Button4;
        private StandardButton Button5;
        private StandardButton Button1;
    }
}
