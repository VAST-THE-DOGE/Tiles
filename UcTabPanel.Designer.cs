namespace Tiles
{
    partial class UcTabPanel
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
            TabsTable = new MyTableLayoutPanel();
            ButtonMapSettings = new StandardButton();
            ButtonGovernment = new StandardButton();
            ButtonMisc = new StandardButton();
            ButtonOverview = new StandardButton();
            ButtonBasic = new StandardButton();
            myTableLayoutPanel3 = new MyTableLayoutPanel();
            panelLine = new Panel();
            BorderLeft = new StandardBorder() { PanelSide = BorderType.Left};
            BorderBottomLeft = new StandardBorder() { PanelSide = BorderType.BottomLeft};
            BorderBottom = new StandardBorder() { PanelSide = BorderType.Bottom};
            BorderBottomRight = new StandardBorder() { PanelSide = BorderType.BottomRight};
            BorderRight = new StandardBorder() { PanelSide = BorderType.Right};
            TabsTable.SuspendLayout();
            myTableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // TabsTable
            // 
            TabsTable.ColumnCount = 5;
            TabsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            TabsTable.Controls.Add(ButtonMapSettings, 1, 0);
            TabsTable.Controls.Add(ButtonGovernment, 2, 0);
            TabsTable.Controls.Add(ButtonMisc, 3, 0);
            TabsTable.Controls.Add(ButtonOverview, 4, 0);
            TabsTable.Controls.Add(ButtonBasic, 0, 0);
            TabsTable.Dock = DockStyle.Fill;
            TabsTable.Location = new Point(61, 0);
            TabsTable.Margin = new Padding(0);
            TabsTable.Name = "TabsTable";
            TabsTable.RowCount = 1;
            myTableLayoutPanel3.SetRowSpan(TabsTable, 2);
            TabsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            TabsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            TabsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            TabsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            TabsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            TabsTable.Size = new Size(1506, 148);
            TabsTable.TabIndex = 0;
            // 
            // ButtonMapSettings
            // 
            ButtonMapSettings.BackColor = SystemColors.ControlDark;
            ButtonMapSettings.Dock = DockStyle.Fill;
            ButtonMapSettings.FlatAppearance.BorderColor = Color.Yellow;
            ButtonMapSettings.FlatAppearance.BorderSize = 3;
            ButtonMapSettings.FlatStyle = FlatStyle.Flat;
            ButtonMapSettings.Font = new Font("Segoe UI", 11.7063208F);
            ButtonMapSettings.Location = new Point(313, 14);
            ButtonMapSettings.Margin = new Padding(12, 14, 12, 0);
            ButtonMapSettings.Name = "ButtonMapSettings";
            ButtonMapSettings.Size = new Size(277, 134);
            ButtonMapSettings.TabIndex = 1;
            ButtonMapSettings.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            ButtonMapSettings.Text = "Map Settings";
            ButtonMapSettings.UseVisualStyleBackColor = false;
            // 
            // ButtonGovernment
            // 
            ButtonGovernment.BackColor = SystemColors.ControlDark;
            ButtonGovernment.Dock = DockStyle.Fill;
            ButtonGovernment.FlatAppearance.BorderColor = Color.Yellow;
            ButtonGovernment.FlatAppearance.BorderSize = 3;
            ButtonGovernment.FlatStyle = FlatStyle.Flat;
            ButtonGovernment.Font = new Font("Segoe UI", 13.7452612F);
            ButtonGovernment.Location = new Point(614, 14);
            ButtonGovernment.Margin = new Padding(12, 14, 12, 0);
            ButtonGovernment.Name = "ButtonGovernment";
            ButtonGovernment.Size = new Size(277, 134);
            ButtonGovernment.TabIndex = 2;
            ButtonGovernment.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            ButtonGovernment.Text = "Goverment";
            ButtonGovernment.UseVisualStyleBackColor = false;
            // 
            // ButtonMisc
            // 
            ButtonMisc.BackColor = SystemColors.ControlDark;
            ButtonMisc.Dock = DockStyle.Fill;
            ButtonMisc.FlatAppearance.BorderColor = Color.Yellow;
            ButtonMisc.FlatAppearance.BorderSize = 3;
            ButtonMisc.FlatStyle = FlatStyle.Flat;
            ButtonMisc.Font = new Font("Segoe UI", 11.64613F);
            ButtonMisc.Location = new Point(915, 14);
            ButtonMisc.Margin = new Padding(12, 14, 12, 0);
            ButtonMisc.Name = "ButtonMisc";
            ButtonMisc.Size = new Size(277, 134);
            ButtonMisc.TabIndex = 3;
            ButtonMisc.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            ButtonMisc.Text = "Misc Settings";
            ButtonMisc.UseVisualStyleBackColor = false;
            // 
            // ButtonOverview
            // 
            ButtonOverview.BackColor = SystemColors.ControlDark;
            ButtonOverview.Dock = DockStyle.Fill;
            ButtonOverview.FlatAppearance.BorderColor = Color.Yellow;
            ButtonOverview.FlatAppearance.BorderSize = 3;
            ButtonOverview.FlatStyle = FlatStyle.Flat;
            ButtonOverview.Font = new Font("Segoe UI", 16.44294F);
            ButtonOverview.Location = new Point(1216, 14);
            ButtonOverview.Margin = new Padding(12, 14, 12, 0);
            ButtonOverview.Name = "ButtonOverview";
            ButtonOverview.Size = new Size(278, 134);
            ButtonOverview.TabIndex = 4;
            ButtonOverview.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            ButtonOverview.Text = "Overview";
            ButtonOverview.UseVisualStyleBackColor = false;
            // 
            // ButtonBasic
            // 
            ButtonBasic.BackColor = SystemColors.ControlDark;
            ButtonBasic.Dock = DockStyle.Fill;
            ButtonBasic.FlatAppearance.BorderColor = Color.Yellow;
            ButtonBasic.FlatAppearance.BorderSize = 3;
            ButtonBasic.FlatStyle = FlatStyle.Flat;
            ButtonBasic.Font = new Font("Segoe UI", 15.8514853F);
            ButtonBasic.Location = new Point(12, 14);
            ButtonBasic.Margin = new Padding(12, 14, 12, 0);
            ButtonBasic.Name = "ButtonBasic";
            ButtonBasic.Size = new Size(277, 134);
            ButtonBasic.TabIndex = 0;
            ButtonBasic.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            ButtonBasic.Text = "Basic Info";
            ButtonBasic.UseVisualStyleBackColor = false;
            // 
            // myTableLayoutPanel3
            // 
            myTableLayoutPanel3.BackColor = Color.Transparent;
            myTableLayoutPanel3.ColumnCount = 3;
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 61F));
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            myTableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 61F));
            myTableLayoutPanel3.Controls.Add(panelLine, 0, 2);
            myTableLayoutPanel3.Controls.Add(TabsTable, 1, 0);
            myTableLayoutPanel3.Controls.Add(BorderLeft, 0, 3);
            myTableLayoutPanel3.Controls.Add(BorderBottomLeft, 0, 4);
            myTableLayoutPanel3.Controls.Add(BorderBottom, 1, 4);
            myTableLayoutPanel3.Controls.Add(BorderBottomRight, 2, 4);
            myTableLayoutPanel3.Controls.Add(BorderRight, 2, 3);
            myTableLayoutPanel3.Dock = DockStyle.Fill;
            myTableLayoutPanel3.Location = new Point(0, 0);
            myTableLayoutPanel3.Margin = new Padding(7, 8, 7, 8);
            myTableLayoutPanel3.Name = "myTableLayoutPanel3";
            myTableLayoutPanel3.RowCount = 5;
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 5F));
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            myTableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
            myTableLayoutPanel3.Size = new Size(1628, 946);
            myTableLayoutPanel3.TabIndex = 1;
            // 
            // panelLine
            // 
            panelLine.BackColor = Color.Yellow;
            myTableLayoutPanel3.SetColumnSpan(panelLine, 3);
            panelLine.Dock = DockStyle.Fill;
            panelLine.Location = new Point(0, 148);
            panelLine.Margin = new Padding(0);
            panelLine.Name = "panelLine";
            panelLine.Size = new Size(1628, 10);
            panelLine.TabIndex = 1;
            // 
            // BorderLeft
            // 
            BorderLeft.Dock = DockStyle.Fill;
            BorderLeft.Location = new Point(0, 158);
            BorderLeft.Margin = new Padding(0);
            BorderLeft.Name = "BorderLeft";
            BorderLeft.Size = new Size(61, 720);
            BorderLeft.TabIndex = 2;
            // 
            // BorderBottomLeft
            // 
            BorderBottomLeft.Dock = DockStyle.Fill;
            BorderBottomLeft.Location = new Point(0, 878);
            BorderBottomLeft.Margin = new Padding(0);
            BorderBottomLeft.Name = "BorderBottomLeft";
            BorderBottomLeft.Size = new Size(61, 68);
            BorderBottomLeft.TabIndex = 3;
            // 
            // BorderBottom
            // 
            BorderBottom.Dock = DockStyle.Fill;
            BorderBottom.Location = new Point(61, 878);
            BorderBottom.Margin = new Padding(0);
            BorderBottom.Name = "BorderBottom";
            BorderBottom.Size = new Size(1506, 68);
            BorderBottom.TabIndex = 4;
            // 
            // BorderBottomRight
            // 
            BorderBottomRight.Dock = DockStyle.Fill;
            BorderBottomRight.Location = new Point(1567, 878);
            BorderBottomRight.Margin = new Padding(0);
            BorderBottomRight.Name = "BorderBottomRight";
            BorderBottomRight.Size = new Size(61, 68);
            BorderBottomRight.TabIndex = 5;
            // 
            // BorderRight
            // 
            BorderRight.Dock = DockStyle.Fill;
            BorderRight.Location = new Point(1567, 158);
            BorderRight.Margin = new Padding(0);
            BorderRight.Name = "BorderRight";
            BorderRight.Size = new Size(61, 720);
            BorderRight.TabIndex = 6;
            // 
            // UcNewWorldPanel
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(myTableLayoutPanel3);
            Margin = new Padding(7, 8, 7, 8);
            Name = "UcNewWorldPanel";
            Size = new Size(1628, 946);
            TabsTable.ResumeLayout(false);
            myTableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private MyTableLayoutPanel TabsTable;
        private MyTableLayoutPanel myTableLayoutPanel3;
        private StandardButton ButtonMapSettings;
        private StandardButton ButtonGovernment;
        private StandardButton ButtonMisc;
        private StandardButton ButtonOverview;
        private StandardButton ButtonBasic;
        private Panel panelLine;
        private StandardBorder BorderLeft;
        private StandardBorder BorderBottomLeft;
        private StandardBorder BorderBottom;
        private StandardBorder BorderBottomRight;
        private StandardBorder BorderRight;
    }
}
