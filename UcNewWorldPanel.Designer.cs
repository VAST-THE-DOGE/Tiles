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
            TabsTable = new MyTableLayoutPanel();
            myTableLayoutPanel3 = new MyTableLayoutPanel();
            panelLine = new Panel();
            BorderLeft = new StandardBorder() { PanelSide = BorderType.Left};
            BorderBottomLeft = new StandardBorder() { PanelSide = BorderType.BottomLeft};
            BorderBottom = new StandardBorder() { PanelSide = BorderType.Bottom};
            BorderBottomRight = new StandardBorder() { PanelSide = BorderType.BottomRight};
            BorderRight = new StandardBorder() { PanelSide = BorderType.Right};
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
            panelLine.Size = new Size(1628, 5);
            panelLine.TabIndex = 1;
            // 
            // BorderLeft
            // 
            BorderLeft.Dock = DockStyle.Fill;
            BorderLeft.Location = new Point(0, 153);
            BorderLeft.Margin = new Padding(0);
            BorderLeft.Name = "BorderLeft";
            BorderLeft.Size = new Size(61, 724);
            BorderLeft.TabIndex = 2;
            // 
            // BorderBottomLeft
            // 
            BorderBottomLeft.Dock = DockStyle.Fill;
            BorderBottomLeft.Location = new Point(0, 877);
            BorderBottomLeft.Margin = new Padding(0);
            BorderBottomLeft.Name = "BorderBottomLeft";
            BorderBottomLeft.Size = new Size(61, 69);
            BorderBottomLeft.TabIndex = 3;
            // 
            // BorderBottom
            // 
            BorderBottom.Dock = DockStyle.Fill;
            BorderBottom.Location = new Point(61, 877);
            BorderBottom.Margin = new Padding(0);
            BorderBottom.Name = "BorderBottom";
            BorderBottom.Size = new Size(1506, 69);
            BorderBottom.TabIndex = 4;
            // 
            // BorderBottomRight
            // 
            BorderBottomRight.Dock = DockStyle.Fill;
            BorderBottomRight.Location = new Point(1567, 877);
            BorderBottomRight.Margin = new Padding(0);
            BorderBottomRight.Name = "BorderBottomRight";
            BorderBottomRight.Size = new Size(61, 69);
            BorderBottomRight.TabIndex = 5;
            // 
            // BorderRight
            // 
            BorderRight.Dock = DockStyle.Fill;
            BorderRight.Location = new Point(1567, 153);
            BorderRight.Margin = new Padding(0);
            BorderRight.Name = "BorderRight";
            BorderRight.Size = new Size(61, 724);
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
            myTableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private MyTableLayoutPanel TabsTable;
        private MyTableLayoutPanel myTableLayoutPanel3;
        private Panel panelLine;
        private StandardBorder BorderLeft;
        private StandardBorder BorderBottomLeft;
        private StandardBorder BorderBottom;
        private StandardBorder BorderBottomRight;
        private StandardBorder BorderRight;
    }
}
