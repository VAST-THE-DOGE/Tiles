//Gui stuff for the loader.
//super messy, but the gui looks good.
//settings and the delete world button need to be finished!
class LoaderGUIStuff
{
    static World[] Worlds;
    static int LoaderFontSize;
    static Bitmap[] menuIcons;
    public static Form frame;
    public static Settings settings;
    private static MyTableLayoutPanel[] RightPanels = new MyTableLayoutPanel[6];
    private static bool[] setupFlags = new bool[8];
    private static MyTableLayoutPanel leftPanel;
    private static FlowLayoutPanel midPanel;

    //LoaderPanel:

    // Primary Panel Setup:
    public static Panel LoaderPanelSetup(Bitmap[] menuIcons, int LoaderFontSize, ref World[] Worlds, ref Form frame, ref Settings settings) 
    {
        Game.menuIcons = menuIcons;

        LoaderGUIStuff.menuIcons = menuIcons;
        LoaderGUIStuff.LoaderFontSize = LoaderFontSize;
        LoaderGUIStuff.Worlds = Worlds;
        LoaderGUIStuff.frame = frame;
        LoaderGUIStuff.settings = settings;
        

        MyTableLayoutPanel MainPanel = new MyTableLayoutPanel();

        //call the setup stuff and continue
        SetupSettingsPanel();
        SetupGithubPanel();
        SetupMapEditorPanel();
        SetupNewWorldPanel();
        SetupDeletePanel(MainPanel);
        SetupWorldPreviewPanel(MainPanel);
        SetupLeft(MainPanel);
        SetupMid(MainPanel);

        //wait for panel setup to be done.
        HelperStuff.WaitForFlags(setupFlags, 25);

        //setup the main
        MainPanel.Controls.Add(leftPanel, 0, 0);
        MainPanel.Controls.Add(midPanel, 1, 0);
        if (Worlds.Length > 0)
        {
            UpdateWorldPreviewPanel(0, RightPanels[4]);
            MainPanel.Controls.Add(RightPanels[4], 2, 0);
        }
        else 
        {
            MainPanel.Controls.Add(RightPanels[3], 2, 0);
        }
        MainPanel.Dock = DockStyle.Fill;
        MainPanel.ColumnCount = 3;
        MainPanel.RowCount = 1;
        RichPresenceHelper.UpdateStartTime();
        RichPresenceHelper.UpdateActivity("In the Main Menu", "");
        return MainPanel;
    }

    //extra functions for the loader panel.
    public static void UpdateWorldPreviewPanel(int ID, MyTableLayoutPanel MainPanel)
    {
        Control WorldImage = MainPanel.GetControlFromPosition(0, 0);
        Color borderC;
        switch (Worlds[ID].Difficulty)
        {
            case 0: borderC = Color.Lime;  break;
            case 1: borderC = Color.Yellow; break;
            case 2: borderC = Color.Red;    break;
            default: borderC = Color.Black; break;
        }
        WorldImage.BackgroundImage = 
        HelperStuff.OutlineImage(new Bitmap(HelperStuff.LoadImage(@"WorldScreenshots\World" + ID), WorldImage.Width, WorldImage.Height), borderC, 4);
        WorldImage.Text = "" + ID;
        MainPanel.GetControlFromPosition(1, 1).Text = (Worlds[ID].EditedMap ? "Edit" : "Play");
        MainPanel.GetControlFromPosition(0, 1).Text = "World "+(ID+1)+"\nDay: "+ Worlds[ID].Time[0];
    }
    public static void BasicButtonSetup(Button Button, int FontSize, int Width, int Height)
    {
        Button.Font = new Font(new FontFamily("Arial"), FontSize, FontStyle.Bold);
        Button.FlatStyle = FlatStyle.Flat;
        Button.FlatAppearance.BorderColor = Color.Yellow;
        Button.FlatAppearance.BorderSize = 4;
        Button.BackgroundImage = menuIcons[28];
        Button.Size = new Size(Width, Height);
        Button.Anchor = AnchorStyles.Top;

        //click delay
        Button.Click += (sender, e) => 
        {
            Button.Enabled = false;
            System.Timers.Timer clickDelayTimer = new System.Timers.Timer(10);
            clickDelayTimer.Elapsed += (s, args) => 
            {
                Button.Invoke(new Action(() => 
                {
                    Button.Enabled = true;
                }));
                clickDelayTimer.Stop();
            };
            clickDelayTimer.Start();
        };
        if (settings.ExtraEffects)
        {
            HelperStuff.SetupMouseEffects(Button, true, true, true);
        }

    }
    public static MyTableLayoutPanel SetupBasicRightPanel(int Rows, int Columns)
    {
        MyTableLayoutPanel MainPanel = new MyTableLayoutPanel();
        MainPanel.BackgroundImage = menuIcons[8];
        MainPanel.Size = new Size(570, 550);
        MainPanel.Dock = DockStyle.Fill;
        MainPanel.ColumnCount = Columns;
        MainPanel.RowCount = Rows;
        MainPanel.Margin = new Padding(0);
        return MainPanel;
    }
    public static void  AddWorldToTable(MyTableLayoutPanel MainPanel, int ID, MyTableLayoutPanel WorldPanel)
    {
            Button B = new Button();
            BasicButtonSetup(B, LoaderFontSize, 292, 100);
            B.Click += (sender, e) =>
            {
                UpdateWorldPreviewPanel(ID, WorldPanel);
                MainPanel.Controls.RemoveAt(2);
                MainPanel.Controls.Add(WorldPanel);
            };
            B.Text = "World " + (ID + 1);
            MainPanel.GetControlFromPosition(1, 0).Controls.Add(B);
    }
    //extra setup for the loader menu.
    async public static void SetupWorldPreviewPanel(MyTableLayoutPanel ParentPanel)
    {
        MyTableLayoutPanel MainPanel = SetupBasicRightPanel(1, 2);
        //Buttons[0] = Load, Buttons[1] = Copy, Buttons[2] = Delete
        Button[] Buttons = {new Button(), new Button(), new Button()};
        Label WorldImage = new Label();
        WorldImage.Size = new Size(500, 300);
        WorldImage.Margin = new Padding(18);
        WorldImage.BackgroundImage = null;
        WorldImage.BackColor = Color.Black;
        WorldImage.Anchor = AnchorStyles.Top;
        WorldImage.Font = new Font(new FontFamily("Arial"), 1, FontStyle.Bold);
        WorldImage.Text = "" + 0;
        MainPanel.Controls.Add(WorldImage, 0, 0);
        MainPanel.SetColumnSpan(WorldImage, 2);

        Label DayInfo = new Label();
        DayInfo.Anchor = AnchorStyles.Top;
        DayInfo.Font = new Font(new FontFamily("Arial"), LoaderFontSize - 5, FontStyle.Bold);
        DayInfo.Size = new Size(270, 100);
        DayInfo.Text = "Day: " + "N/A";
        DayInfo.BackgroundImage = HelperStuff.GetLabelBackground(270, 100, 29, Color.Yellow, 4);
        DayInfo.Margin = new Padding(0);
        DayInfo.TextAlign = ContentAlignment.TopCenter;

        MainPanel.Controls.Add(DayInfo, 0, 1);

        foreach (Button B in Buttons)
        {
            BasicButtonSetup(B, LoaderFontSize, 270, 100);
        }

        Buttons[0].Text = "Play";
        Buttons[1].Text = "Copy";
        Buttons[2].Text = "Delete";

        Buttons[0].Click += (sender, e) => 
        {
            //load the game
            RichPresenceHelper.UpdateActivity((Worlds[int.Parse(WorldImage.Text)].EditedMap ? "Editing" : "Playing") + " a World", "Day 0");
            new Game(int.Parse(WorldImage.Text), Worlds[int.Parse(WorldImage.Text)].EditedMap, ref frame, Worlds, settings);
        };
        Buttons[1].Click += (sender, e) => 
        {
            //copy the world
            HelperStuff.AppendToArray(ref Worlds, Worlds[int.Parse(WorldImage.Text)].CopyWorld());
            AddWorldToTable(ParentPanel, Worlds.Length - 1, MainPanel);
            HelperStuff.SaveToJson("SavedWorlds", Worlds);
            try 
            {
                // save the image
                HelperStuff.LoadImage(@"WorldScreenshots\World" + int.Parse(WorldImage.Text)).Save(Directory.GetCurrentDirectory()+@"\Data\ImageData\WorldScreenshots\World"+(Worlds.Length - 1)+".png", System.Drawing.Imaging.ImageFormat.Png);
            } 
            catch (Exception) 
            {}
        };
        Buttons[2].Click += (sender, e) => 
        {
            //delete the world after confirming
            UpdateDeletePanel(int.Parse(WorldImage.Text), ref RightPanels[5], WorldImage.BackgroundImage);
            ParentPanel.Controls.RemoveAt(2);
            ParentPanel.Controls.Add(RightPanels[5]);
        };

        MainPanel.Controls.Add(Buttons[0], 1, 1);
        MainPanel.Controls.Add(Buttons[1], 0, 2);
        MainPanel.Controls.Add(Buttons[2], 1, 2);


        RightPanels[4] = MainPanel;
        setupFlags[4] = true;
    }
    async public static void SetupSettingsPanel()
    {
        Button[] Buttons = {};

        MyTableLayoutPanel MainPanel = SetupBasicRightPanel(5, 5);
        Label WIPLabel = new Label();
        WIPLabel.Anchor = AnchorStyles.Top;
        WIPLabel.Font = new Font(new FontFamily("Arial"), LoaderFontSize - 18, FontStyle.Bold);
        WIPLabel.Size = new Size(400, 60);
        WIPLabel.Text = "This panel is W.I.P\nPanel: Settings";
        WIPLabel.BackgroundImage = menuIcons[29];
        WIPLabel.Margin = new Padding(75, 250, 250, 75);
        WIPLabel.TextAlign = ContentAlignment.TopCenter;
        MainPanel.Controls.Add(WIPLabel);

        RightPanels[0] = MainPanel;
        setupFlags[0] = true;
    }
    async public static void SetupGithubPanel()
    {
        MyTableLayoutPanel MainPanel = SetupBasicRightPanel(5, 5);
        
        Label WebsiteQR = new Label();
        WebsiteQR.Size = new Size(300, 300);
        WebsiteQR.BackgroundImage = HelperStuff.OutlineImage(new Bitmap(HelperStuff.LoadImage("GithubQR"), WebsiteQR.Width, WebsiteQR.Height), Color.SlateGray, 4);
        WebsiteQR.Anchor = AnchorStyles.Top;
        WebsiteQR.Margin = new Padding(15);

        Label WebsiteUrl = new Label();
        WebsiteUrl.Anchor = AnchorStyles.Top;
        WebsiteUrl.Font = new Font(new FontFamily("Arial"), LoaderFontSize - 18, FontStyle.Bold);
        WebsiteUrl.Size = new Size(500, 33);
        WebsiteUrl.Text = "https://github.com/VAST-THE-DOGE/Tiles";
        WebsiteUrl.BackgroundImage = HelperStuff.GetLabelBackground(WebsiteUrl.Size.Width, WebsiteUrl.Size.Height, 29, Color.Yellow, 4);
        WebsiteUrl.Margin = new Padding(30);
        WebsiteUrl.TextAlign = ContentAlignment.TopCenter;

        Button WebsiteButton = new Button();
        BasicButtonSetup(WebsiteButton, LoaderFontSize, 350, 100);
        WebsiteButton.Text = "Go to Github";
        WebsiteButton.Click += (sender, e) =>
        {
            try {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://github.com/VAST-THE-DOGE/Tiles",
                    UseShellExecute = true
                });
            }
            catch
            {
                WebsiteButton.FlatAppearance.BorderColor = Color.Red;
            }
        };
        
        MainPanel.Controls.Add(WebsiteQR, 3, 2);
        MainPanel.Controls.Add(WebsiteUrl, 3, 3);
        MainPanel.Controls.Add(WebsiteButton, 3, 4);

        RightPanels[1] = MainPanel;
        setupFlags[1] = true;
    }
    async public static void SetupMapEditorPanel()
    {
        // Copy and paste new world setup (With small edits!).
        MyTableLayoutPanel MainPanel = SetupBasicRightPanel(5, 4);
        // width -1, width +1, height -1, height +1, difficulty change, creative mode change, create world / edit new world.
        Button[] buttons = {new Button(), new Button(), new Button(), new Button(), new Button(), new Button(), new Button()};
        Label[] labels = {new Label(), new Label(), new Label(), new Label(), new Label(), new Label()};
        
        //setup buttons
        foreach (Button B in buttons)
        {
            BasicButtonSetup(B, LoaderFontSize, 100, 100);
        } 
        buttons[0].Text = "◀";
        buttons[1].Text = "▶";
        buttons[2].Text = "◀";
        buttons[3].Text = "▶";
        buttons[4].Text = "Normal";
        buttons[5].Text = "False";
        buttons[6].Text = "Edit New Map";

        //setup labels
        foreach (Label L in labels)
        {
            L.Anchor = AnchorStyles.Top;
            L.Font = new Font(new FontFamily("Arial"), LoaderFontSize + 2, FontStyle.Bold);
            L.Size = new Size(142, 80);
            L.Dock = DockStyle.Fill;
            L.Margin = new Padding(5, 5, 5, 5);
            L.TextAlign = ContentAlignment.TopCenter;
        }

        labels[0].Text = "30";
        labels[1].Text = "15";
        labels[2].Text = "Width:";
        labels[3].Text = "Height:";
        labels[4].Text = "Difficulty:";
        labels[5].Text = "Creative:";

        labels[2].Size = new Size(200, 90);
        labels[3].Size = new Size(200, 90);
        labels[4].Size = new Size(270, 90);
        labels[5].Size = new Size(270, 90);

        buttons[4].Size = new Size(230, 90);
        buttons[5].Size = new Size(230, 90);
        buttons[6].Size = new Size(552, 100);

        labels[0].Margin = new Padding(0, 5, 0, 5);
        labels[1].Margin = new Padding(0, 5, 0, 5);

        buttons[0].Margin = new Padding(5, 5, 0, 5);
        buttons[1].Margin = new Padding(0, 5, 12, 5);
        buttons[2].Margin = new Padding(5, 5, 0, 5);
        buttons[3].Margin = new Padding(0, 5, 12, 5);

        buttons[6].Margin = new Padding(5, 5, 5, 5);
        buttons[6].Anchor = AnchorStyles.Left;

        bool CreateMode = false;
        int Difficulty = 1;
        string[] DiffNames = {"Easy", "Normal", "Hard"};

        buttons[0].Click += (sender, e) => { if (int.Parse(labels[0].Text) > 1) labels[0].Text = "" + (int.Parse(labels[0].Text) - 1); };

        buttons[1].Click += (sender, e) => { labels[0].Text = "" + (int.Parse(labels[0].Text) + 1); };

        buttons[2].Click += (sender, e) => { if (int.Parse(labels[1].Text) > 1) labels[1].Text = "" + (int.Parse(labels[1].Text) - 1); };

        buttons[3].Click += (sender, e) => { labels[1].Text = "" + (int.Parse(labels[1].Text) + 1); };

        buttons[4].Click += (sender, e) => 
        {
            if (Difficulty < 2)
            {buttons[4].Text = DiffNames[++Difficulty];}
            else {buttons[4].Text = DiffNames[0]; Difficulty = 0;}
        };

        buttons[5].Click += (sender, e) => { CreateMode = !CreateMode; buttons[5].Text = ""+CreateMode;};
        buttons[6].Click += (sender, e) => 
        {
            buttons[6].Text = "Please Wait..."; 
            buttons[6].FlatAppearance.BorderColor = Color.Red;
            World newWorld = new World();
            newWorld.Time = new int[2];
            newWorld.Resources = new long[8];
            newWorld.Resources[7] = 25;
            newWorld.Resources[6] = 500;
            newWorld.Resources[5] = 250;
            newWorld.Resources[4] = 250;
            newWorld.Research = new int[1];
            newWorld.Difficulty = Difficulty;
            newWorld.Weather = 0;
            newWorld.Leader = 0;
            newWorld.Sandbox = bool.Parse(buttons[5].Text);
            newWorld.EditedMap = true;
            newWorld.Map = new int[int.Parse(labels[1].Text)][];
            for (int i = 0; i < newWorld.Map.Length; i++)
            {
                newWorld.Map[i] = new int[int.Parse(labels[0].Text)];
            }
            HelperStuff.AppendToArray(ref Worlds, newWorld);
            new Game(Worlds.Length - 1, true, ref frame, Worlds, settings);
        };

        MainPanel.Controls.Add(buttons[0], 1, 0);
        MainPanel.Controls.Add(buttons[1], 3, 0);
        MainPanel.Controls.Add(buttons[2], 1, 1);
        MainPanel.Controls.Add(buttons[3], 3, 1);
        MainPanel.Controls.Add(buttons[4], 2, 2);
        MainPanel.Controls.Add(buttons[5], 2, 3);
        MainPanel.Controls.Add(buttons[6], 0, 4);

        MainPanel.Controls.Add(labels[0], 2, 0);
        MainPanel.Controls.Add(labels[1], 2, 1);
        MainPanel.Controls.Add(labels[2], 0, 0);
        MainPanel.Controls.Add(labels[3], 0, 1);
        MainPanel.Controls.Add(labels[4], 0, 2);
        MainPanel.Controls.Add(labels[5], 0, 3);

        MainPanel.SetColumnSpan(labels[4], 2);
        MainPanel.SetColumnSpan(labels[5], 2);
        MainPanel.SetColumnSpan(buttons[4], 2);
        MainPanel.SetColumnSpan(buttons[5], 2);
        MainPanel.SetColumnSpan(buttons[6], 4);

        foreach (Label L in labels)
        {
            L.BackgroundImage = HelperStuff.GetLabelBackground(L.Size.Width, L.Size.Height, 29, Color.Yellow, 4);
        }

        RightPanels[2] = MainPanel;
        setupFlags[2] = true;
    }
    async public static void SetupNewWorldPanel()

    {
        // Copy and paste new world setup (With small edits!).
        MyTableLayoutPanel MainPanel = SetupBasicRightPanel(5, 4);
        // width -1, width +1, height -1, height +1, difficulty change, creative mode change, create world / edit new world.
        Button[] buttons = {new Button(), new Button(), new Button(), new Button(), new Button(), new Button(), new Button()};
        Label[] labels = {new Label(), new Label(), new Label(), new Label(), new Label(), new Label()};
        
        //setup buttons
        foreach (Button B in buttons)
        {
            BasicButtonSetup(B, LoaderFontSize, 100, 100);
        }
        buttons[0].Text = "◀";
        buttons[1].Text = "▶";
        buttons[2].Text = "◀";
        buttons[3].Text = "▶";
        buttons[4].Text = "Normal";
        buttons[5].Text = "False";
        buttons[6].Text = "Create New World";

        //setup labels
        foreach (Label L in labels)
        {
            L.Anchor = AnchorStyles.Top;
            L.Font = new Font(new FontFamily("Arial"), LoaderFontSize + 2, FontStyle.Bold);
            L.Size = new Size(142, 80);
            L.Dock = DockStyle.Fill;
            L.Margin = new Padding(5, 5, 5, 5);
            L.BackgroundImage = menuIcons[29];
            L.TextAlign = ContentAlignment.TopCenter;
        }

        labels[0].Text = "30";
        labels[1].Text = "15";
        labels[2].Text = "Width:";
        labels[3].Text = "Height:";
        labels[4].Text = "Difficulty:";
        labels[5].Text = "Creative:";

        labels[2].Size = new Size(200, 90);
        labels[3].Size = new Size(200, 90);
        labels[4].Size = new Size(270, 90);
        labels[5].Size = new Size(270, 90);

        buttons[4].Size = new Size(230, 90);
        buttons[5].Size = new Size(230, 90);
        buttons[6].Size = new Size(552, 100);

        labels[0].Margin = new Padding(0, 5, 0, 5);
        labels[1].Margin = new Padding(0, 5, 0, 5);

        buttons[0].Margin = new Padding(5, 5, 0, 5);
        buttons[1].Margin = new Padding(0, 5, 12, 5);
        buttons[2].Margin = new Padding(5, 5, 0, 5);
        buttons[3].Margin = new Padding(0, 5, 12, 5);

        buttons[6].Margin = new Padding(5, 5, 5, 5);
        buttons[6].Anchor = AnchorStyles.Left;

        bool CreateMode = false;
        int Difficulty = 1;
        string[] DiffNames = {"Easy", "Normal", "Hard"};

        buttons[0].Click += (sender, e) => { if (int.Parse(labels[0].Text) > 1) labels[0].Text = "" + (int.Parse(labels[0].Text) - 1); };

        buttons[1].Click += (sender, e) => { labels[0].Text = "" + (int.Parse(labels[0].Text) + 1); };

        buttons[2].Click += (sender, e) => { if (int.Parse(labels[1].Text) > 1) labels[1].Text = "" + (int.Parse(labels[1].Text) - 1); };

        buttons[3].Click += (sender, e) => { labels[1].Text = "" + (int.Parse(labels[1].Text) + 1); };

        buttons[4].Click += (sender, e) => 
        {
            if (Difficulty < 2)
            {buttons[4].Text = DiffNames[++Difficulty];}
            else {buttons[4].Text = DiffNames[0]; Difficulty = 0;}
        };

        buttons[5].Click += (sender, e) => { CreateMode = !CreateMode; buttons[5].Text = ""+CreateMode;};
        buttons[6].Click += (sender, e) => { 
            buttons[6].Text = "Please Wait..."; 
            buttons[6].FlatAppearance.BorderColor = Color.Red;
            World newWorld = new World();
            newWorld.Time = new int[2];
            newWorld.Resources = new long[8];
            newWorld.Resources[7] = 25;
            newWorld.Resources[6] = 500;
            newWorld.Resources[5] = 250;
            newWorld.Resources[4] = 250;
            newWorld.Research = new int[1];
            newWorld.Difficulty = Difficulty;
            newWorld.Weather = 0;
            newWorld.Leader = 0;
            newWorld.Sandbox = bool.Parse(buttons[5].Text);
            newWorld.EditedMap = false;
            newWorld.Map = MapGenerator.Generate(int.Parse(labels[0].Text), int.Parse(labels[1].Text));
            HelperStuff.AppendToArray(ref Worlds, newWorld);
            new Game(Worlds.Length - 1, true, ref frame, Worlds, settings);
            };

        MainPanel.Controls.Add(buttons[0], 1, 0);
        MainPanel.Controls.Add(buttons[1], 3, 0);
        MainPanel.Controls.Add(buttons[2], 1, 1);
        MainPanel.Controls.Add(buttons[3], 3, 1);
        MainPanel.Controls.Add(buttons[4], 2, 2);
        MainPanel.Controls.Add(buttons[5], 2, 3);
        MainPanel.Controls.Add(buttons[6], 0, 4);

        MainPanel.Controls.Add(labels[0], 2, 0);
        MainPanel.Controls.Add(labels[1], 2, 1);
        MainPanel.Controls.Add(labels[2], 0, 0);
        MainPanel.Controls.Add(labels[3], 0, 1);
        MainPanel.Controls.Add(labels[4], 0, 2);
        MainPanel.Controls.Add(labels[5], 0, 3);

        MainPanel.SetColumnSpan(labels[4], 2);
        MainPanel.SetColumnSpan(labels[5], 2);
        MainPanel.SetColumnSpan(buttons[4], 2);
        MainPanel.SetColumnSpan(buttons[5], 2);
        MainPanel.SetColumnSpan(buttons[6], 4);

        foreach (Label L in labels)
        {
            L.BackgroundImage = HelperStuff.GetLabelBackground(L.Size.Width, L.Size.Height, 29, Color.Yellow, 4);
        }

        RightPanels[3] = MainPanel;
        setupFlags[3] = true;
    }
    async private static void SetupLeft(MyTableLayoutPanel MainPanel)
    {
        // left menu
        // setup the buttons

        // don't use "= new Button[#]" makes an error
        Button[] LeftMenuButtons = {new Button(), new Button(), new Button(), new Button()};

        Label label1 = new Label();
        label1.Text = "Tiles";
        label1.Font = new Font(new FontFamily("Arial"), LoaderFontSize*2 + 5, FontStyle.Bold);
        label1.Margin = new Padding(0);
        label1.BackColor = Color.Transparent;
        label1.Size = new Size(300,100);
        Label label2 = new Label();
        label2.Text = "        By William Herbert";
        label2.Font = new Font(new FontFamily("Arial"), LoaderFontSize/4 + 5, FontStyle.Bold);
        label2.Margin = new Padding(0);
        label2.BackColor = Color.Transparent;
        label2.Size = new Size(300,25);
        
        //manual sets for the buttons
        LeftMenuButtons[0].Click += (sender, e) =>
        {
            MainPanel.Controls.RemoveAt(2);
            MainPanel.Controls.Add(RightPanels[0]);
        };
        LeftMenuButtons[0].Text = "Settings";

        LeftMenuButtons[1].Click += (sender, e) =>
        {
            MainPanel.Controls.RemoveAt(2);
            MainPanel.Controls.Add(RightPanels[1]);
        };
        LeftMenuButtons[1].Text = "Github";

        LeftMenuButtons[2].Click += (sender, e) =>
        {
            MainPanel.Controls.RemoveAt(2);
            MainPanel.Controls.Add(RightPanels[2]);
        };
        LeftMenuButtons[2].Text = "Map Editor";

        LeftMenuButtons[3].Click += (sender, e) =>
        {
            MainPanel.Controls.RemoveAt(2);
            MainPanel.Controls.Add(RightPanels[3]);
        };
        LeftMenuButtons[3].Text = "New World";
        
        foreach (Button B in LeftMenuButtons)
        {
            BasicButtonSetup(B, LoaderFontSize, 302, 100);
        }

        MyTableLayoutPanel LeftPanel = new MyTableLayoutPanel();

        LeftPanel.Dock = DockStyle.Fill;
        LeftPanel.ColumnCount = 1;
        LeftPanel.RowCount = LeftMenuButtons.Length + 2;
        LeftPanel.Size = new Size(308, 400);
        LeftPanel.BackgroundImage = menuIcons[8];
        LeftPanel.Margin = new Padding(0);

        LeftPanel.Controls.Add(label1, 0, 0);
        LeftPanel.Controls.Add(label2, 0, 1);
        for (int i = 2; i < LeftPanel.RowCount; i++)
        {
            LeftPanel.Controls.Add(LeftMenuButtons[i - 2], 0, i);
        }

        leftPanel = LeftPanel;
        setupFlags[5] = true;
    }
    async private static void SetupMid(MyTableLayoutPanel MainPanel)
    {
        //set up the middle panel

        FlowLayoutPanel MiddlePanel = new FlowLayoutPanel();
        MiddlePanel.BackgroundImage = menuIcons[29];
        MiddlePanel.Size = new Size(315, 205);
        MiddlePanel.Dock = DockStyle.Fill;
        MiddlePanel.FlowDirection = FlowDirection.TopDown;
        MiddlePanel.WrapContents = false;
        MiddlePanel.AutoScroll = true;
        MiddlePanel.Margin = new Padding(0);
        
        // Add buttons to the panel
        for (int i = 0; i < Worlds.Length; i++)
        {
            Button B = new Button();
            BasicButtonSetup(B, LoaderFontSize, 292, 100);
            int ID = i;
            B.Click += (sender, e) =>
            {
                UpdateWorldPreviewPanel(ID, RightPanels[4]);
                MainPanel.Controls.RemoveAt(2);
                MainPanel.Controls.Add(RightPanels[4]);
            };
            B.Text = "World " + (i + 1);
            MiddlePanel.Controls.Add(B);
        }

        midPanel = MiddlePanel;
        setupFlags[6] = true;
    }
    async private static void SetupDeletePanel(MyTableLayoutPanel ParentPanel)
    {
        MyTableLayoutPanel MainPanel = SetupBasicRightPanel(1, 2);
        Button[] Buttons = {new Button(), new Button()};
        Label WorldImage = new Label();
        WorldImage.Size = new Size(500, 300);
        WorldImage.Margin = new Padding(18);
        WorldImage.BackgroundImage = null;
        WorldImage.BackColor = Color.Black;
        WorldImage.Anchor = AnchorStyles.Top;
        WorldImage.Font = new Font(new FontFamily("Arial"), 1, FontStyle.Bold);
        WorldImage.Text = "" + 0;
        MainPanel.Controls.Add(WorldImage, 0, 0);
        MainPanel.SetColumnSpan(WorldImage, 2);

        Label label = new Label();
        label.Anchor = AnchorStyles.Top;
        label.Font = new Font(new FontFamily("Arial"), LoaderFontSize - 5, FontStyle.Bold);
        label.Size = new Size(510, 100);
        label.Text = "Are you sure? This cannot be undone!";
        label.BackgroundImage = HelperStuff.GetLabelBackground(510, 100, 29, Color.Red, 4);
        label.Margin = new Padding(0);
        label.TextAlign = ContentAlignment.TopCenter;

        MainPanel.Controls.Add(label, 0, 1);
        MainPanel.SetColumnSpan(label, 2);

        foreach (Button B in Buttons)
        {
            BasicButtonSetup(B, LoaderFontSize, 270, 100);
        }

        Buttons[0].Text = "Go Back";
        Buttons[1].Text = "Delete World";

        Buttons[0].Click += (sender, e) => 
        {
            ParentPanel.Controls.RemoveAt(2);
            ParentPanel.Controls.Add(RightPanels[4]);
        };
        Buttons[1].Click += (sender, e) => 
        {
            //start at the index id of the removed world
            for (int i = int.Parse(WorldImage.Text); i < Worlds.Length; i++)
            {

                
                // save the image
                Bitmap curImg = HelperStuff.LoadImage(@"WorldScreenshots\World" + (i + 1));
                curImg.Save(Directory.GetCurrentDirectory()+@"\Data\ImageData\WorldScreenshots\World" + (i) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                
            }

            HelperStuff.RemoveAtIndex(ref Worlds, int.Parse(WorldImage.Text));
            HelperStuff.SaveToJson("SavedWorlds", Worlds);

            ParentPanel.Controls.RemoveAt(2);
            ParentPanel.Controls.RemoveAt(1);
            setupFlags[6] = false;
            SetupMid(ParentPanel);
            HelperStuff.WaitForFlags(setupFlags, 10);
            ParentPanel.Controls.Add(midPanel);
            if (Worlds.Length > 0)
            {
                UpdateWorldPreviewPanel(0, RightPanels[4]);
                ParentPanel.Controls.Add(RightPanels[4]);
            }
            else
            {
                ParentPanel.Controls.Add(RightPanels[3]);  
            }
        };

        MainPanel.Controls.Add(Buttons[0], 1, 1);
        MainPanel.Controls.Add(Buttons[1], 0, 2);

        RightPanels[5] = MainPanel;
        setupFlags[7] = true;
    }
    public static void UpdateDeletePanel(int ID, ref MyTableLayoutPanel MainPanel, Image image)
    {
        Label WorldImage = MainPanel.GetControlFromPosition(0, 0) as Label;
        WorldImage.BackgroundImage = image;
        WorldImage.Text = "" + ID;
    }
}
