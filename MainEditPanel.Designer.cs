namespace Tiles
{
    partial class MainEditPanel
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
            MapAreaPanel = new StandardBackground();
            ucRightPanel1 = new UcRightPanel();
            ucBottomPanel1 = new UcEditPanel();
            myTableLayoutPanel1 = new MyTableLayoutPanel();
            myTableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // MapAreaPanel
            // 
            MapAreaPanel.BackColor = Color.DodgerBlue;
            MapAreaPanel.Dock = DockStyle.Fill;
            MapAreaPanel.Location = new Point(0, 0);
            MapAreaPanel.Margin = new Padding(0);
            MapAreaPanel.Name = "MapAreaPanel";
            MapAreaPanel.Size = new Size(1536, 900);
            MapAreaPanel.TabIndex = 0;
            // 
            // ucRightPanel1
            // 
            ucRightPanel1.BackColor = Color.SaddleBrown;
            ucRightPanel1.Dock = DockStyle.Fill;
            ucRightPanel1.Location = new Point(1536, 0);
            ucRightPanel1.Margin = new Padding(0);
            ucRightPanel1.Name = "ucRightPanel1";
            ucRightPanel1.Size = new Size(384, 900);
            ucRightPanel1.TabIndex = 0;
            // 
            // ucBottomPanel1
            // 
            ucBottomPanel1.BackColor = Color.SaddleBrown;
            myTableLayoutPanel1.SetColumnSpan(ucBottomPanel1, 2);
            ucBottomPanel1.Dock = DockStyle.Fill;
            ucBottomPanel1.Location = new Point(0, 900);
            ucBottomPanel1.Margin = new Padding(0);
            ucBottomPanel1.Name = "ucBottomPanel1";
            ucBottomPanel1.Size = new Size(1920, 180);
            ucBottomPanel1.TabIndex = 0;
            // 
            // myTableLayoutPanel1
            // 
            myTableLayoutPanel1.ColumnCount = 2;
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            myTableLayoutPanel1.Controls.Add(ucRightPanel1, 1, 0);
            myTableLayoutPanel1.Controls.Add(ucBottomPanel1, 0, 1);
            myTableLayoutPanel1.Controls.Add(MapAreaPanel, 0, 0);
            myTableLayoutPanel1.Dock = DockStyle.Fill;
            myTableLayoutPanel1.Location = new Point(0, 0);
            myTableLayoutPanel1.Name = "myTableLayoutPanel1";
            myTableLayoutPanel1.RowCount = 2;
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 83.3333359F));
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            myTableLayoutPanel1.Size = new Size(1920, 1080);
            myTableLayoutPanel1.TabIndex = 1;
            // 
            // MainEditPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(myTableLayoutPanel1);
            Name = "MainEditPanel";
            Size = new Size(1920, 1080);
            myTableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private UcEditPanel ucBottomPanel1;
        private UcRightPanel ucRightPanel1;
        private StandardBackground MapAreaPanel;
        private MyTableLayoutPanel myTableLayoutPanel1;
    }
}