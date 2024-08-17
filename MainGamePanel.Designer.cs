namespace Tiles
{
    partial class MainGamePanel
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
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            ucRightPanel1 = new UcRightPanel();
            ucBottomPanel1 = new UcBottomPanel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.Yellow;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.Yellow;
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.SaddleBrown;
            splitContainer1.Panel2.Controls.Add(ucBottomPanel1);
            splitContainer1.Size = new Size(1175, 643);
            splitContainer1.SplitterDistance = 499;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.BackColor = Color.Yellow;
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.BackColor = Color.CornflowerBlue;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.BackColor = Color.SaddleBrown;
            splitContainer2.Panel2.Controls.Add(ucRightPanel1);
            splitContainer2.Size = new Size(1175, 499);
            splitContainer2.SplitterDistance = 923;
            splitContainer2.TabIndex = 0;
            splitContainer2.SplitterMoved += splitContainer2_SplitterMoved;
            // 
            // ucRightPanel1
            // 
            ucRightPanel1.BackColor = Color.SaddleBrown;
            ucRightPanel1.Dock = DockStyle.Fill;
            ucRightPanel1.Location = new Point(0, 0);
            ucRightPanel1.Name = "ucRightPanel1";
            ucRightPanel1.Size = new Size(248, 499);
            ucRightPanel1.TabIndex = 0;
            // 
            // ucBottomPanel1
            // 
            ucBottomPanel1.BackColor = Color.SaddleBrown;
            ucBottomPanel1.Dock = DockStyle.Fill;
            ucBottomPanel1.Location = new Point(0, 0);
            ucBottomPanel1.Margin = new Padding(0);
            ucBottomPanel1.Name = "ucBottomPanel1";
            ucBottomPanel1.Size = new Size(1175, 140);
            ucBottomPanel1.TabIndex = 0;
            // 
            // MainGamePanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Name = "MainGamePanel";
            Size = new Size(1175, 643);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private UcBottomPanel ucBottomPanel1;
        private UcRightPanel ucRightPanel1;
    }
}
