namespace Tiles
{
    partial class Loader
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loader));
            MainTable = new MyTableLayoutPanel();
            Title = new MyTableLayoutPanel();
            Background = new MyTableLayoutPanel();
            pictureBox = new PictureBox();
            LeftControls = new MyTableLayoutPanel();
            Main = new MyTableLayoutPanel();
            BInfo = new StandardButton();
            BSettings = new StandardButton();
            BNewMapEdit = new StandardButton();
            BNewWorld = new StandardButton();
            BWorlds = new StandardButton();
            MiddlePanel = new MyTableLayoutPanel();
            SettingsTable = new MyTableLayoutPanel();
            button9 = new StandardButton();
            button2 = new StandardButton();
            button3 = new StandardButton();
            button4 = new StandardButton();
            button5 = new StandardButton();
            InfoTable = new MyTableLayoutPanel();
            button1 = new StandardButton();
            button6 = new StandardButton();
            button7 = new StandardButton();
            button8 = new StandardButton();
            toolTip = new ToolTip(components);
            standardBackground1 = new StandardBackground();
            MainTable.SuspendLayout();
            Title.SuspendLayout();
            Background.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            LeftControls.SuspendLayout();
            Main.SuspendLayout();
            MiddlePanel.SuspendLayout();
            SettingsTable.SuspendLayout();
            InfoTable.SuspendLayout();
            standardBackground1.SuspendLayout();
            SuspendLayout();
            // 
            // MainTable
            // 
            MainTable.BackColor = Color.Transparent;
            MainTable.ColumnCount = 3;
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            MainTable.Controls.Add(Title, 0, 0);
            MainTable.Controls.Add(LeftControls, 0, 1);
            MainTable.Dock = DockStyle.Fill;
            MainTable.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            MainTable.Location = new Point(0, 0);
            MainTable.Name = "MainTable";
            MainTable.RowCount = 3;
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 65F));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            MainTable.Size = new Size(944, 501);
            MainTable.TabIndex = 0;
            // 
            // Title
            // 
            Title.BackColor = Color.Transparent;
            Title.ColumnCount = 1;
            Title.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Title.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Title.Controls.Add(Background, 0, 0);
            Title.Dock = DockStyle.Fill;
            Title.Location = new Point(15, 5);
            Title.Margin = new Padding(15, 5, 15, 5);
            Title.Name = "Title";
            Title.RowCount = 1;
            Title.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Title.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Title.Size = new Size(253, 90);
            Title.TabIndex = 3;
            // 
            // Background
            // 
            Background.ColumnCount = 1;
            Background.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Background.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Background.Controls.Add(pictureBox, 0, 0);
            Background.Dock = DockStyle.Fill;
            Background.Location = new Point(4, 4);
            Background.Margin = new Padding(4);
            Background.Name = "Background";
            Background.RowCount = 1;
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Background.Size = new Size(245, 82);
            Background.TabIndex = 1;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = (Image)resources.GetObject("pictureBox.Image");
            pictureBox.Location = new Point(3, 6);
            pictureBox.Margin = new Padding(3, 6, 3, 3);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(239, 73);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // LeftControls
            // 
            LeftControls.BackColor = Color.Yellow;
            LeftControls.ColumnCount = 1;
            LeftControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LeftControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LeftControls.Controls.Add(standardBackground1, 0, 0);
            LeftControls.Dock = DockStyle.Fill;
            LeftControls.Location = new Point(15, 105);
            LeftControls.Margin = new Padding(15, 5, 15, 5);
            LeftControls.Name = "LeftControls";
            LeftControls.RowCount = 1;
            MainTable.SetRowSpan(LeftControls, 2);
            LeftControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            LeftControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            LeftControls.Size = new Size(253, 391);
            LeftControls.TabIndex = 2;
            // 
            // Main
            // 
            Main.BackColor = Color.Transparent;
            Main.ColumnCount = 1;
            Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Main.Controls.Add(BInfo, 0, 4);
            Main.Controls.Add(BSettings, 0, 3);
            Main.Controls.Add(BNewMapEdit, 0, 2);
            Main.Controls.Add(BNewWorld, 0, 1);
            Main.Controls.Add(BWorlds, 0, 0);
            Main.Dock = DockStyle.Fill;
            Main.Location = new Point(0, 0);
            Main.Margin = new Padding(4);
            Main.Name = "Main";
            Main.RowCount = 5;
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Main.Size = new Size(247, 385);
            Main.TabIndex = 2;
            // 
            // BInfo
            // 
            BInfo.BackColor = Color.Gray;
            BInfo.Dock = DockStyle.Fill;
            BInfo.FlatAppearance.BorderColor = Color.Yellow;
            BInfo.FlatAppearance.BorderSize = 3;
            BInfo.FlatAppearance.MouseDownBackColor = Color.Red;
            BInfo.FlatAppearance.MouseOverBackColor = Color.Yellow;
            BInfo.FlatStyle = FlatStyle.Flat;
            BInfo.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            BInfo.Location = new Point(3, 311);
            BInfo.Name = "BInfo";
            BInfo.Size = new Size(241, 71);
            BInfo.TabIndex = 4;
            BInfo.Text = "Info";
            BInfo.UseVisualStyleBackColor = false;
            BInfo.Click += BInfo_Click;
            // 
            // BSettings
            // 
            BSettings.BackColor = Color.Gray;
            BSettings.Dock = DockStyle.Fill;
            BSettings.FlatAppearance.BorderColor = Color.Yellow;
            BSettings.FlatAppearance.BorderSize = 3;
            BSettings.FlatAppearance.MouseDownBackColor = Color.Red;
            BSettings.FlatAppearance.MouseOverBackColor = Color.Yellow;
            BSettings.FlatStyle = FlatStyle.Flat;
            BSettings.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            BSettings.Location = new Point(3, 234);
            BSettings.Name = "BSettings";
            BSettings.Size = new Size(241, 71);
            BSettings.TabIndex = 3;
            BSettings.Text = "Settings";
            BSettings.UseVisualStyleBackColor = false;
            BSettings.Click += BSettings_Click;
            // 
            // BNewMapEdit
            // 
            BNewMapEdit.BackColor = Color.Gray;
            BNewMapEdit.Dock = DockStyle.Fill;
            BNewMapEdit.FlatAppearance.BorderColor = Color.Yellow;
            BNewMapEdit.FlatAppearance.BorderSize = 3;
            BNewMapEdit.FlatAppearance.MouseDownBackColor = Color.Red;
            BNewMapEdit.FlatAppearance.MouseOverBackColor = Color.Yellow;
            BNewMapEdit.FlatStyle = FlatStyle.Flat;
            BNewMapEdit.Font = new Font("Arial Black", 20F, FontStyle.Bold);
            BNewMapEdit.Location = new Point(3, 157);
            BNewMapEdit.Name = "BNewMapEdit";
            BNewMapEdit.Size = new Size(241, 71);
            BNewMapEdit.TabIndex = 2;
            BNewMapEdit.Text = "New Map Editor";
            BNewMapEdit.UseVisualStyleBackColor = false;
            BNewMapEdit.Click += BNewMapEdit_Click;
            // 
            // BNewWorld
            // 
            BNewWorld.BackColor = Color.Gray;
            BNewWorld.Dock = DockStyle.Fill;
            BNewWorld.FlatAppearance.BorderColor = Color.Yellow;
            BNewWorld.FlatAppearance.BorderSize = 3;
            BNewWorld.FlatAppearance.MouseDownBackColor = Color.Red;
            BNewWorld.FlatAppearance.MouseOverBackColor = Color.Yellow;
            BNewWorld.FlatStyle = FlatStyle.Flat;
            BNewWorld.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            BNewWorld.Location = new Point(3, 80);
            BNewWorld.Name = "BNewWorld";
            BNewWorld.Size = new Size(241, 71);
            BNewWorld.TabIndex = 1;
            BNewWorld.Text = "New World";
            BNewWorld.UseVisualStyleBackColor = false;
            BNewWorld.Click += BNewWorld_Click;
            // 
            // BWorlds
            // 
            BWorlds.BackColor = Color.Gray;
            BWorlds.Dock = DockStyle.Fill;
            BWorlds.FlatAppearance.BorderColor = Color.Yellow;
            BWorlds.FlatAppearance.BorderSize = 3;
            BWorlds.FlatAppearance.MouseDownBackColor = Color.Red;
            BWorlds.FlatAppearance.MouseOverBackColor = Color.Yellow;
            BWorlds.FlatStyle = FlatStyle.Flat;
            BWorlds.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            BWorlds.Location = new Point(3, 3);
            BWorlds.Name = "BWorlds";
            BWorlds.Size = new Size(241, 71);
            BWorlds.TabIndex = 0;
            BWorlds.Text = "Worlds";
            BWorlds.UseVisualStyleBackColor = false;
            BWorlds.Click += BWorlds_Click;
            // 
            // MiddlePanel
            // 
            MiddlePanel.BackColor = Color.Yellow;
            MiddlePanel.ColumnCount = 1;
            MiddlePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MiddlePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            MiddlePanel.Controls.Add(SettingsTable, 0, 0);
            MiddlePanel.Dock = DockStyle.Fill;
            MiddlePanel.Location = new Point(293, 105);
            MiddlePanel.Margin = new Padding(10, 5, 10, 5);
            MiddlePanel.Name = "MiddlePanel";
            MiddlePanel.RowCount = 1;
            MiddlePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            MiddlePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            MiddlePanel.Size = new Size(216, 315);
            MiddlePanel.TabIndex = 4;
            // 
            // SettingsTable
            // 
            SettingsTable.BackgroundImage = (Image)resources.GetObject("SettingsTable.BackgroundImage");
            SettingsTable.ColumnCount = 1;
            SettingsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            SettingsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            SettingsTable.Controls.Add(button9, 0, 4);
            SettingsTable.Controls.Add(button2, 0, 3);
            SettingsTable.Controls.Add(button3, 0, 2);
            SettingsTable.Controls.Add(button4, 0, 1);
            SettingsTable.Controls.Add(button5, 0, 0);
            SettingsTable.Dock = DockStyle.Fill;
            SettingsTable.Location = new Point(4, 4);
            SettingsTable.Margin = new Padding(4);
            SettingsTable.Name = "SettingsTable";
            SettingsTable.RowCount = 5;
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            SettingsTable.Size = new Size(208, 307);
            SettingsTable.TabIndex = 2;
            // 
            // button9
            // 
            button9.BackgroundImage = (Image)resources.GetObject("button9.BackgroundImage");
            button9.Dock = DockStyle.Fill;
            button9.FlatAppearance.BorderColor = Color.Yellow;
            button9.FlatAppearance.BorderSize = 3;
            button9.FlatAppearance.MouseDownBackColor = Color.Red;
            button9.FlatAppearance.MouseOverBackColor = Color.Yellow;
            button9.FlatStyle = FlatStyle.Flat;
            button9.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            button9.ForeColor = Color.Red;
            button9.Location = new Point(3, 247);
            button9.Name = "button9";
            button9.Size = new Size(202, 57);
            button9.TabIndex = 4;
            button9.Text = "Music Packs";
            button9.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.BackgroundImage = (Image)resources.GetObject("button2.BackgroundImage");
            button2.Dock = DockStyle.Fill;
            button2.FlatAppearance.BorderColor = Color.Yellow;
            button2.FlatAppearance.BorderSize = 3;
            button2.FlatAppearance.MouseDownBackColor = Color.Red;
            button2.FlatAppearance.MouseOverBackColor = Color.Yellow;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            button2.ForeColor = Color.Red;
            button2.Location = new Point(3, 186);
            button2.Name = "button2";
            button2.Size = new Size(202, 55);
            button2.TabIndex = 3;
            button2.Text = "Skin Packs";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.BackgroundImage = (Image)resources.GetObject("button3.BackgroundImage");
            button3.Dock = DockStyle.Fill;
            button3.FlatAppearance.BorderColor = Color.Yellow;
            button3.FlatAppearance.BorderSize = 3;
            button3.FlatAppearance.MouseDownBackColor = Color.Red;
            button3.FlatAppearance.MouseOverBackColor = Color.Yellow;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Arial Black", 20F, FontStyle.Bold);
            button3.ForeColor = Color.Red;
            button3.Location = new Point(3, 125);
            button3.Name = "button3";
            button3.Size = new Size(202, 55);
            button3.TabIndex = 2;
            button3.Text = "Gameplay";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.BackgroundImage = (Image)resources.GetObject("button4.BackgroundImage");
            button4.Dock = DockStyle.Fill;
            button4.FlatAppearance.BorderColor = Color.Yellow;
            button4.FlatAppearance.BorderSize = 3;
            button4.FlatAppearance.MouseDownBackColor = Color.Red;
            button4.FlatAppearance.MouseOverBackColor = Color.Yellow;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            button4.ForeColor = Color.Red;
            button4.Location = new Point(3, 64);
            button4.Name = "button4";
            button4.Size = new Size(202, 55);
            button4.TabIndex = 1;
            button4.Text = "Graphics";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.BackgroundImage = (Image)resources.GetObject("button5.BackgroundImage");
            button5.Dock = DockStyle.Fill;
            button5.FlatAppearance.BorderColor = Color.Yellow;
            button5.FlatAppearance.BorderSize = 3;
            button5.FlatAppearance.MouseDownBackColor = Color.Red;
            button5.FlatAppearance.MouseOverBackColor = Color.Yellow;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            button5.ForeColor = Color.Red;
            button5.Location = new Point(3, 3);
            button5.Name = "button5";
            button5.Size = new Size(202, 55);
            button5.TabIndex = 0;
            button5.Text = "Sound";
            button5.UseVisualStyleBackColor = true;
            // 
            // InfoTable
            // 
            InfoTable.BackgroundImage = (Image)resources.GetObject("InfoTable.BackgroundImage");
            InfoTable.ColumnCount = 1;
            InfoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InfoTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InfoTable.Controls.Add(button1, 0, 3);
            InfoTable.Controls.Add(button6, 0, 2);
            InfoTable.Controls.Add(button7, 0, 1);
            InfoTable.Controls.Add(button8, 0, 0);
            InfoTable.Dock = DockStyle.Fill;
            InfoTable.Location = new Point(523, 4);
            InfoTable.Margin = new Padding(4);
            InfoTable.Name = "InfoTable";
            InfoTable.RowCount = 4;
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            InfoTable.Size = new Size(417, 92);
            InfoTable.TabIndex = 3;
            // 
            // button1
            // 
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.Dock = DockStyle.Fill;
            button1.FlatAppearance.BorderColor = Color.Yellow;
            button1.FlatAppearance.BorderSize = 3;
            button1.FlatAppearance.MouseDownBackColor = Color.Red;
            button1.FlatAppearance.MouseOverBackColor = Color.Yellow;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            button1.ForeColor = Color.Red;
            button1.Location = new Point(3, 72);
            button1.Name = "button1";
            button1.Size = new Size(411, 17);
            button1.TabIndex = 3;
            button1.Text = "Tile Data";
            button1.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.BackgroundImage = (Image)resources.GetObject("button6.BackgroundImage");
            button6.Dock = DockStyle.Fill;
            button6.FlatAppearance.BorderColor = Color.Yellow;
            button6.FlatAppearance.BorderSize = 3;
            button6.FlatAppearance.MouseDownBackColor = Color.Red;
            button6.FlatAppearance.MouseOverBackColor = Color.Yellow;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Arial Black", 20F, FontStyle.Bold);
            button6.ForeColor = Color.Red;
            button6.Location = new Point(3, 49);
            button6.Name = "button6";
            button6.Size = new Size(411, 17);
            button6.TabIndex = 2;
            button6.Text = "Gameplay";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.BackgroundImage = (Image)resources.GetObject("button7.BackgroundImage");
            button7.Dock = DockStyle.Fill;
            button7.FlatAppearance.BorderColor = Color.Yellow;
            button7.FlatAppearance.BorderSize = 3;
            button7.FlatAppearance.MouseDownBackColor = Color.Red;
            button7.FlatAppearance.MouseOverBackColor = Color.Yellow;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Arial Black", 18F, FontStyle.Bold);
            button7.ForeColor = Color.Red;
            button7.Location = new Point(3, 26);
            button7.Name = "button7";
            button7.Size = new Size(411, 17);
            button7.TabIndex = 1;
            button7.Text = "Change Log";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.BackgroundImage = (Image)resources.GetObject("button8.BackgroundImage");
            button8.Dock = DockStyle.Fill;
            button8.FlatAppearance.BorderColor = Color.Yellow;
            button8.FlatAppearance.BorderSize = 3;
            button8.FlatAppearance.MouseDownBackColor = Color.Red;
            button8.FlatAppearance.MouseOverBackColor = Color.Yellow;
            button8.FlatStyle = FlatStyle.Flat;
            button8.Font = new Font("Arial Black", 24F, FontStyle.Bold);
            button8.ForeColor = Color.Red;
            button8.Location = new Point(3, 3);
            button8.Name = "button8";
            button8.Size = new Size(411, 17);
            button8.TabIndex = 0;
            button8.Text = "Github";
            button8.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            toolTip.AutomaticDelay = 200;
            toolTip.BackColor = Color.PaleGoldenrod;
            // 
            // standardBackground1
            // 
            standardBackground1.BackColor = Color.SaddleBrown;
            standardBackground1.Controls.Add(Main);
            standardBackground1.Dock = DockStyle.Fill;
            standardBackground1.Location = new Point(3, 3);
            standardBackground1.Name = "standardBackground1";
            standardBackground1.Size = new Size(247, 385);
            standardBackground1.TabIndex = 4;
            // 
            // Loader
            // 
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(944, 501);
            Controls.Add(MainTable);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(300, 150);
            Name = "Loader";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tiles";
            WindowState = FormWindowState.Maximized;
            SizeChanged += Loader_SizeChanged;
            MainTable.ResumeLayout(false);
            Title.ResumeLayout(false);
            Background.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            LeftControls.ResumeLayout(false);
            Main.ResumeLayout(false);
            MiddlePanel.ResumeLayout(false);
            SettingsTable.ResumeLayout(false);
            InfoTable.ResumeLayout(false);
            standardBackground1.ResumeLayout(false);
            ResumeLayout(false);
        }

        //
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion
        private MyTableLayoutPanel MainTable;
        private PictureBox pictureBox;
        private MyTableLayoutPanel LeftControls;
        private MyTableLayoutPanel Title;
        private MyTableLayoutPanel Background;
        private MyTableLayoutPanel Main;
        private StandardButton BWorlds;
        private StandardButton BInfo;
        private StandardButton BSettings;
        private StandardButton BNewMapEdit;
        private StandardButton BNewWorld;
        private MyTableLayoutPanel MiddlePanel;
        private MyTableLayoutPanel SettingsTable;
        private StandardButton button2;
        private StandardButton button3;
        private StandardButton button4;
        private StandardButton button5;
        private MyTableLayoutPanel InfoTable;
        private StandardButton button1;
        private StandardButton button6;
        private StandardButton button7;
        private StandardButton button8;
        private StandardButton button9;
        private ToolTip toolTip;
        private StandardBackground standardBackground1;
    }
}
