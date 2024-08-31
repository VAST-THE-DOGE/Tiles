namespace Tiles
{
    partial class UcBottomPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcBottomPanel));
            tableLayoutPanel1 = new MyTableLayoutPanel();
            tableLayoutPanel3 = new MyTableLayoutPanel();
            LabelSaved = new StandardLabel();
            ButtonSave = new StandardButton();
            tableLayoutPanel2 = new MyTableLayoutPanel();
            LabelHour = new StandardLabel();
            LabelDays = new StandardLabel();
            panel1 = new Panel();
            ucResourcePanel1 = new UcResourcePanel();
            ucResourcePanel2 = new UcResourcePanel();
            ucResourcePanel3 = new UcResourcePanel();
            ucResourcePanel4 = new UcResourcePanel();
            ucResourcePanel6 = new UcResourcePanel();
            ucResourcePanel7 = new UcResourcePanel();
            ucResourcePanel8 = new UcResourcePanel();
            ucResourcePanel5 = new UcResourcePanel();
            MainTable = new TableLayoutPanel();
            LineBottomRight = new StandardBorder() { PanelSide = BorderType.BottomRight};
            LineBottom = new StandardBorder() { PanelSide = BorderType.Bottom};
            LineBottomLeft = new StandardBorder() { PanelSide = BorderType.BottomLeft};
            LineRight = new StandardBorder() { PanelSide = BorderType.Right};
            LineLeft = new StandardBorder() { PanelSide = BorderType.Left};
            LineTopRight = new StandardBorder() { PanelSide = BorderType.TopRight};
            LineTopLeft = new StandardBorder() { PanelSide = BorderType.TopLeft};
            LineTop = new StandardBorder() { PanelSide = BorderType.Top};
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            MainTable.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Controls.Add(ucResourcePanel5, 2, 0);
            tableLayoutPanel1.Controls.Add(ucResourcePanel6, 3, 0);
            tableLayoutPanel1.Controls.Add(ucResourcePanel7, 4, 0);
            tableLayoutPanel1.Controls.Add(ucResourcePanel8, 5, 0);
            tableLayoutPanel1.Controls.Add(ucResourcePanel1, 2, 1);
            tableLayoutPanel1.Controls.Add(ucResourcePanel2, 3, 1);
            tableLayoutPanel1.Controls.Add(ucResourcePanel3, 4, 1);
            tableLayoutPanel1.Controls.Add(ucResourcePanel4, 5, 1);
            
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(20, 20);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1117, 141);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = Color.Yellow;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(LabelSaved, 0, 0);
            tableLayoutPanel3.Controls.Add(ButtonSave, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 73);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(216, 65);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // LabelSaved
            // 
            LabelSaved.AutoSize = true;
            LabelSaved.BackColor = Color.Wheat;
            LabelSaved.Dock = DockStyle.Fill;
            LabelSaved.Location = new Point(3, 3);
            LabelSaved.Margin = new Padding(3);
            LabelSaved.Name = "LabelSaved";
            LabelSaved.Size = new Size(102, 59);
            LabelSaved.TabIndex = 2;
            LabelSaved.Text = "💾: ✘";
            LabelSaved.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ButtonSave
            // 
            ButtonSave.BackColor = Color.SlateGray;
            ButtonSave.BackgroundImage = (Image)resources.GetObject("ButtonSave.BackgroundImage");
            ButtonSave.Dock = DockStyle.Fill;
            ButtonSave.FlatAppearance.BorderColor = Color.Yellow;
            ButtonSave.FlatAppearance.BorderSize = 3;
            ButtonSave.FlatStyle = FlatStyle.Flat;
            ButtonSave.Font = new Font("Segoe UI", 16.2065144F);
            ButtonSave.Location = new Point(108, 0);
            ButtonSave.Margin = new Padding(0);
            ButtonSave.Name = "ButtonSave";
            ButtonSave.Size = new Size(108, 65);
            ButtonSave.TabIndex = 3;
            ButtonSave.Text = "Save";
            ButtonSave.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.Yellow;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(LabelHour, 1, 0);
            tableLayoutPanel2.Controls.Add(LabelDays, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(216, 64);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // LabelHour
            // 
            LabelHour.AutoSize = true;
            LabelHour.BackColor = Color.Wheat;
            LabelHour.Dock = DockStyle.Fill;
            LabelHour.Location = new Point(111, 3);
            LabelHour.Margin = new Padding(3);
            LabelHour.Name = "LabelHour";
            LabelHour.Size = new Size(102, 58);
            LabelHour.TabIndex = 1;
            LabelHour.Text = "11:00 am";
            LabelHour.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LabelDays
            // 
            LabelDays.AutoSize = true;
            LabelDays.BackColor = Color.Wheat;
            LabelDays.Dock = DockStyle.Fill;
            LabelDays.Location = new Point(3, 3);
            LabelDays.Margin = new Padding(3);
            LabelDays.Name = "LabelDays";
            LabelDays.Size = new Size(102, 58);
            LabelDays.TabIndex = 0;
            LabelDays.Text = "Day 412";
            LabelDays.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Yellow;
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(223, 3);
            panel1.Margin = new Padding(1, 3, 1, 3);
            panel1.Name = "panel1";
            tableLayoutPanel1.SetRowSpan(panel1, 2);
            panel1.Size = new Size(3, 135);
            panel1.TabIndex = 0;
            // 
            // ucResourcePanel1
            // 
            ucResourcePanel1.BackColor = Color.Blue;
            ucResourcePanel1.Dock = DockStyle.Fill;
            ucResourcePanel1.Location = new Point(230, 73);
            ucResourcePanel1.Name = "ucResourcePanel1";
            ucResourcePanel1.Size = new Size(216, 65);
            ucResourcePanel1.TabIndex = 2;
            // 
            // ucResourcePanel2
            // 
            ucResourcePanel2.BackColor = Color.Gold;
            ucResourcePanel2.Dock = DockStyle.Fill;
            ucResourcePanel2.Location = new Point(452, 73);
            ucResourcePanel2.Name = "ucResourcePanel2";
            ucResourcePanel2.Size = new Size(216, 65);
            ucResourcePanel2.TabIndex = 3;
            // 
            // ucResourcePanel3
            // 
            ucResourcePanel3.BackColor = Color.DarkGoldenrod;
            ucResourcePanel3.Dock = DockStyle.Fill;
            ucResourcePanel3.Location = new Point(674, 73);
            ucResourcePanel3.Name = "ucResourcePanel3";
            ucResourcePanel3.Size = new Size(216, 65);
            ucResourcePanel3.TabIndex = 4;
            // 
            // ucResourcePanel4
            // 
            ucResourcePanel4.BackColor = Color.Purple;
            ucResourcePanel4.Dock = DockStyle.Fill;
            ucResourcePanel4.Location = new Point(896, 73);
            ucResourcePanel4.Name = "ucResourcePanel4";
            ucResourcePanel4.Size = new Size(218, 65);
            ucResourcePanel4.TabIndex = 5;
            // 
            // ucResourcePanel6
            // 
            ucResourcePanel6.BackColor = Color.Red;
            ucResourcePanel6.Dock = DockStyle.Fill;
            ucResourcePanel6.Location = new Point(452, 3);
            ucResourcePanel6.Name = "ucResourcePanel6";
            ucResourcePanel6.Size = new Size(216, 64);
            ucResourcePanel6.TabIndex = 7;
            // 
            // ucResourcePanel7
            // 
            ucResourcePanel7.BackColor = Color.Silver;
            ucResourcePanel7.Dock = DockStyle.Fill;
            ucResourcePanel7.Location = new Point(674, 3);
            ucResourcePanel7.Name = "ucResourcePanel7";
            ucResourcePanel7.Size = new Size(216, 64);
            ucResourcePanel7.TabIndex = 8;
            // 
            // ucResourcePanel8
            // 
            ucResourcePanel8.BackColor = Color.Green;
            ucResourcePanel8.Dock = DockStyle.Fill;
            ucResourcePanel8.Location = new Point(896, 3);
            ucResourcePanel8.Name = "ucResourcePanel8";
            ucResourcePanel8.Size = new Size(218, 64);
            ucResourcePanel8.TabIndex = 9;
            // 
            // ucResourcePanel5
            // 
            ucResourcePanel5.BackColor = Color.Yellow;
            ucResourcePanel5.Dock = DockStyle.Fill;
            ucResourcePanel5.Location = new Point(230, 3);
            ucResourcePanel5.Name = "ucResourcePanel5";
            ucResourcePanel5.Size = new Size(216, 64);
            ucResourcePanel5.TabIndex = 6;
            // 
            // MainTable
            // 
            MainTable.ColumnCount = 3;
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, BasicGuiManager.LineSize));
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, BasicGuiManager.LineSize));
            MainTable.Controls.Add(LineBottomRight, 2, 2);
            MainTable.Controls.Add(LineBottom, 1, 2);
            MainTable.Controls.Add(LineBottomLeft, 0, 2);
            MainTable.Controls.Add(LineRight, 2, 1);
            MainTable.Controls.Add(LineLeft, 0, 1);
            MainTable.Controls.Add(LineTopRight, 2, 0);
            MainTable.Controls.Add(LineTopLeft, 0, 0);
            MainTable.Controls.Add(tableLayoutPanel1, 1, 1);
            MainTable.Controls.Add(LineTop, 1, 0);
            MainTable.Dock = DockStyle.Fill;
            MainTable.Location = new Point(0, 0);
            MainTable.Name = "MainTable";
            MainTable.RowCount = 3;
            MainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, BasicGuiManager.LineSize));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, BasicGuiManager.LineSize));
            MainTable.Size = new Size(1157, 181);
            MainTable.TabIndex = 2;
            MainTable.BackColor = Color.Transparent;
            // 
            // LineBottomRight
            // 
            LineBottomRight.Dock = DockStyle.Fill;
            LineBottomRight.Location = new Point(1137, 161);
            LineBottomRight.Margin = new Padding(0);
            LineBottomRight.Name = "LineBottomRight";
            LineBottomRight.Size = new Size(20, 20);
            LineBottomRight.TabIndex = 6;
            // 
            // LineBottom
            // 
            LineBottom.Dock = DockStyle.Fill;
            LineBottom.Location = new Point(20, 161);
            LineBottom.Margin = new Padding(0);
            LineBottom.Name = "LineBottom";
            LineBottom.Size = new Size(1117, 20);
            LineBottom.TabIndex = 5;
            // 
            // LineBottomLeft
            // 
            LineBottomLeft.Dock = DockStyle.Fill;
            LineBottomLeft.Location = new Point(0, 161);
            LineBottomLeft.Margin = new Padding(0);
            LineBottomLeft.Name = "LineBottomLeft";
            LineBottomLeft.Size = new Size(20, 20);
            LineBottomLeft.TabIndex = 4;
            // 
            // LineRight
            // 
            LineRight.Dock = DockStyle.Fill;
            LineRight.Location = new Point(1137, 20);
            LineRight.Margin = new Padding(0);
            LineRight.Name = "LineRight";
            LineRight.Size = new Size(20, 141);
            LineRight.TabIndex = 3;
            // 
            // LineLeft
            // 
            LineLeft.Dock = DockStyle.Fill;
            LineLeft.Location = new Point(0, 20);
            LineLeft.Margin = new Padding(0);
            LineLeft.Name = "LineLeft";
            LineLeft.Size = new Size(20, 141);
            LineLeft.TabIndex = 3;
            // 
            // LineTopRight
            // 
            LineTopRight.Dock = DockStyle.Fill;
            LineTopRight.Location = new Point(1137, 0);
            LineTopRight.Margin = new Padding(0);
            LineTopRight.Name = "LineTopRight";
            LineTopRight.Size = new Size(20, 20);
            LineTopRight.TabIndex = 3;
            // 
            // LineTopLeft
            // 
            LineTopLeft.Dock = DockStyle.Fill;
            LineTopLeft.Location = new Point(0, 0);
            LineTopLeft.Margin = new Padding(0);
            LineTopLeft.Name = "LineTopLeft";
            LineTopLeft.Size = new Size(20, 20);
            LineTopLeft.TabIndex = 3;
            // 
            // LineTop
            // 
            LineTop.Dock = DockStyle.Fill;
            LineTop.Location = new Point(20, 0);
            LineTop.Margin = new Padding(0);
            LineTop.Name = "LineTop";
            LineTop.Size = new Size(1117, 20);
            LineTop.TabIndex = 2;
            // 
            // UcBottomPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(MainTable);
            DoubleBuffered = true;
            Name = "UcBottomPanel";
            Size = new Size(1157, 181);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            MainTable.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MyTableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private MyTableLayoutPanel tableLayoutPanel2;
        private Panel panel1;
        private TableLayoutPanel MainTable;
        private UcResourcePanel ucResourcePanel1;
        private UcResourcePanel ucResourcePanel2;
        private UcResourcePanel ucResourcePanel3;
        private UcResourcePanel ucResourcePanel4;
        private UcResourcePanel ucResourcePanel5;
        private UcResourcePanel ucResourcePanel6;
        private UcResourcePanel ucResourcePanel7;
        private UcResourcePanel ucResourcePanel8;
        private StandardBorder LineBottomRight;
        private StandardBorder LineBottom;
        private StandardBorder LineBottomLeft;
        private StandardBorder LineRight;
        private StandardBorder LineLeft;
        private StandardBorder LineTopRight;
        private StandardBorder LineTopLeft;
        private StandardBorder LineTop;
        private StandardLabel LabelDays;
        private StandardLabel LabelSaved;
        private StandardLabel LabelHour;
        private StandardButton ButtonSave;
    }
}
