namespace Tiles
{
    partial class UcWorldHeaderViewer
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
            FlowPanel = new MyFlowPanel();
            myTableLayoutPanel1 = new MyTableLayoutPanel();
            ButtonRefresh = new StandardButton();
            label1 = new Label();
            myTableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // FlowPanel
            // 
            FlowPanel.BackColor = Color.Transparent;
            myTableLayoutPanel1.SetColumnSpan(FlowPanel, 2);
            FlowPanel.Dock = DockStyle.Fill;
            FlowPanel.FlowDirection = FlowDirection.TopDown;
            FlowPanel.Location = new Point(3, 38);
            FlowPanel.Name = "FlowPanel";
            FlowPanel.Size = new Size(1101, 688);
            FlowPanel.TabIndex = 0;
            // 
            // myTableLayoutPanel1
            // 
            myTableLayoutPanel1.ColumnCount = 2;
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            myTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 35F));
            myTableLayoutPanel1.Controls.Add(FlowPanel, 0, 1);
            myTableLayoutPanel1.Controls.Add(ButtonRefresh, 1, 0);
            myTableLayoutPanel1.Controls.Add(label1, 0, 0);
            myTableLayoutPanel1.Dock = DockStyle.Fill;
            myTableLayoutPanel1.Location = new Point(0, 0);
            myTableLayoutPanel1.Name = "myTableLayoutPanel1";
            myTableLayoutPanel1.RowCount = 2;
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            myTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            myTableLayoutPanel1.Size = new Size(1107, 729);
            myTableLayoutPanel1.TabIndex = 0;
            // 
            // ButtonRefresh
            // 
            ButtonRefresh.BackColor = Color.Gray;
            ButtonRefresh.Dock = DockStyle.Fill;
            ButtonRefresh.FlatAppearance.BorderColor = Color.Yellow;
            ButtonRefresh.FlatAppearance.BorderSize = 3;
            ButtonRefresh.FlatStyle = FlatStyle.Flat;
            ButtonRefresh.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ButtonRefresh.Location = new Point(1075, 3);
            ButtonRefresh.Name = "ButtonRefresh";
            ButtonRefresh.Size = new Size(29, 29);
            ButtonRefresh.TabIndex = 1;
            ButtonRefresh.Tag = new int[]
    {
    3,
    5,
    0,
    0,
    0
    };
            ButtonRefresh.Text = "↻";
            ButtonRefresh.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(1066, 35);
            label1.TabIndex = 2;
            label1.Text = "Worlds";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UcWorldHeaderViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(myTableLayoutPanel1);
            Name = "UcWorldHeaderViewer";
            Size = new Size(1107, 729);
            myTableLayoutPanel1.ResumeLayout(false);
            myTableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private MyFlowPanel FlowPanel;
        private MyTableLayoutPanel myTableLayoutPanel1;
        private StandardButton ButtonRefresh;
        private Label label1;
    }
}
