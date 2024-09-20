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
            FlowPanel = new CustomFlowLayoutPanel();
            myTableLayoutPanel1 = new MyTableLayoutPanel();
            ButtonRefresh = new StandardButton();
            label1 = new Label();
            myTableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // FlowPanel
            // 
            FlowPanel.BackColor = Color.Transparent;
            FlowPanel.Dock = DockStyle.Fill;
            FlowPanel.FlowDirection = FlowDirection.TopDown;
            FlowPanel.Location = new Point(0, 35);
            FlowPanel.Margin = new Padding(0);
            FlowPanel.Name = "FlowPanel";
            FlowPanel.Size = new Size(1072, 694);
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
            ButtonRefresh.Font = new Font("Arial", 14F, FontStyle.Bold);
            ButtonRefresh.Location = new Point(1072, 0);
            ButtonRefresh.Margin = new Padding(0);
            ButtonRefresh.Name = "ButtonRefresh";
            ButtonRefresh.Size = new Size(35, 35);
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
            label1.Font = new Font("Arial", 26.25F, FontStyle.Bold);
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(1072, 35);
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

        private CustomFlowLayoutPanel FlowPanel;
        private MyTableLayoutPanel myTableLayoutPanel1;
        private StandardButton ButtonRefresh;
        private Label label1;
    }
}
