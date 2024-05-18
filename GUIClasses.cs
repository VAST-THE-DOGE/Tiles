/*
symbol storage:

    time stuff:
        0: ⏸
        1: |▶
        2: ▶
        3: ▶▶
        4: ▶▶▶

    Weather:
        night:      ✦
        very hot:   ☼🌡
        sun:        ☀
        clouds 1:   🌤
        clouds 2:   ☁
        rain:       🌧
        thunder:    🌩
        tornado:    🌪
        snow:       🌨
        freeze:     ❆
        acid rain:  ☢
        muggy:      ♨
        plague:     ☣
        
    info:   ℹ️

    save:   💾

    side menu icons: 🔬, 📰, 📜

    other:
        📤📥✦☀🌤☁🌧🌩🌪🌨❆░▒▓█▶◀▼▲◂▸▾▴◸◹◺◿⫷⫸┃✔✘♨📜☢☼🌡☣ℹ️☠💾⚗️🔬📰🪙⚠️⚠
*/
using static Game;
//GENERAL GUI Classes:
public class MyForm : Form
{
    public MyForm()
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer| ControlStyles.UserPaint, true);
        UpdateStyles();
    }
}
public class MyTableLayoutPanel : TableLayoutPanel
{
    public MyTableLayoutPanel()
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer| ControlStyles.UserPaint, true);
        UpdateStyles();
    }
}
public class OutlinedTableLayoutPanel : MyTableLayoutPanel
{
    public Panel[] lines;
    public Bitmap[] BackgroundIcons;
    public OutlinedTableLayoutPanel(int rows, int columns, Bitmap[] BackgroundIcons)
    {
        //check the input and change if needed.
        if (rows < 3)
        {
            rows = 3;
        }
        if (columns < 3)
        {
            columns = 3;
        }
        //set the rows
        ColumnCount = columns;
        RowCount = rows;
        //save the icons
        this.BackgroundIcons = BackgroundIcons;

        //setup the cool border lines
        lines = new Panel[8];
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = new Panel();
            lines[i].BackgroundImage = BackgroundIcons[i];
            lines[i].Margin = new Padding(0);
        }
        lines[0].Anchor = AnchorStyles.Left;    // left
        lines[1].Anchor = AnchorStyles.Bottom;  // bottom
        lines[2].Anchor = AnchorStyles.Right;   // right
        lines[3].Anchor = AnchorStyles.Top;     // top
        lines[4].Anchor = AnchorStyles.Left;    // top left corner
        lines[5].Anchor = AnchorStyles.Left;    // bottom left corner
        lines[6].Anchor = AnchorStyles.Right;    // bottom right corner
        lines[7].Anchor = AnchorStyles.Right;    // top right corner
        Controls.Add(lines[0], 0, 1); // left
        Controls.Add(lines[1], 1, rows - 1); // bottom
        Controls.Add(lines[2], columns - 1, 1); // right
        Controls.Add(lines[3], 1, 0); // top
        Controls.Add(lines[4], 0, 0); // top left corner
        Controls.Add(lines[5], 0, rows - 1); // bottom left corner
        Controls.Add(lines[6], columns - 1, rows - 1); // bottom right corner
        Controls.Add(lines[7], columns - 1, 0); // top right corner
        SetRowSpan(lines[0], rows - 2);      // left
        SetColumnSpan(lines[1], columns - 2);   // bottom
        SetRowSpan(lines[2], rows - 2);      // right
        SetColumnSpan(lines[3], columns - 2);   // top
    }
    public void ResizeLines()
    {
        Size cellSize = new Size(Width / ColumnCount, Height / RowCount);
        //resize the icons.
        Bitmap[] newMenuIcons = new Bitmap[8];
        Size imageSize = new Size(cellSize.Height, cellSize.Height);
        for (int i = 0; i < newMenuIcons.Length; i++)
        {
            newMenuIcons[i] = new Bitmap(BackgroundIcons[i], imageSize);
        }
        //grab the control and update the size and icon.
        lines[0].Size = new Size(cellSize.Height, Size.Height - 2 * cellSize.Height);   // left
        lines[1].Size = new Size(Size.Width - 2 * cellSize.Height, cellSize.Height);    // bottom
        lines[2].Size = new Size(cellSize.Height, Size.Height - 2 * cellSize.Height);   // right
        lines[3].Size = new Size(Size.Width - 2 * cellSize.Height, cellSize.Height);    // top
        for (int i = 4; i < lines.Length; i++) //corners
        {
            lines[i].Size = new Size(cellSize.Height, cellSize.Height);
        }
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].BackgroundImage = newMenuIcons[i];
        }
        //update the background image
        BackgroundImage = new Bitmap(menuIcons[8], new Size(cellSize.Height, cellSize.Height));
    }
    public void resizeControl(Control control)
    {
        if (control == null)
        {return;}
        else if (control.Size.Height == 0 || control.Size.Width == 0 || Size.Height == 0 || Size.Width == 0)
        {return;}
        else 
        {
            control.Size = new Size((lines[3].Size.Width / (ColumnCount - 2)) * GetColumnSpan(control) - control.Margin.Horizontal
            , (Size.Height / RowCount) * GetRowSpan(control) - control.Margin.Vertical);
        }
    }
}
public class TransparentMenuPanel : MyTableLayoutPanel
{
    public TransparentMenuPanel(int rows, int columns, Padding margins)
    {
        ColumnCount = columns;
        RowCount = rows;
        Margin = margins;
        BackColor = Color.Transparent;
        Resize += (sender, e) =>
        {
            foreach (Control control in Controls)
            {
                ResizeControl(control);
            }
        };
    }
    public void ResizeControl(Control control)
    {
        if (control == null)
        {return;}
        else if (control.Size.Height == 0 || control.Size.Width == 0 || Size.Height == 0 || Size.Width == 0)
        {return;}
        else 
        {
            control.Size = new Size
            ((Size.Width / (ColumnCount)) * GetColumnSpan(control) - control.Margin.Horizontal
            , (Size.Height / RowCount) * GetRowSpan(control) - control.Margin.Vertical);
        }
    }
}
//GAME GUIs
public class BottomPanel
{
    private BottomEditPanel EditPanel;
    private BottomInfoPanel InfoPanel;
    private bool UsingEdit;
    public BottomPanel(bool UsingEdit)
    {
        this.UsingEdit = UsingEdit;
        if (UsingEdit)
        {
            EditPanel = new BottomEditPanel();
        }
        else
        {
            InfoPanel = new BottomInfoPanel();
        }
    }
    public void UpdateInfo(World world)
    {
        if (UsingEdit)
            {
                EditPanel.UpdateInfo(world);
            }
            else
            {
                InfoPanel.UpdateInfo(world);
            }
    }
    public void ResizeBottomPanel(int height, int width)
    {
        if (UsingEdit)
            {
                EditPanel.ResizeBottomPanel(height, width);
            }
            else
            {
                InfoPanel.ResizeBottomPanel(height, width);
            }
    }
    public OutlinedTableLayoutPanel GetMain()
        {
            if (UsingEdit)
                {
                    return EditPanel.MainPanel;
                }
                else
                {
                    return InfoPanel.MainPanel;
                }
        }
}
//the main panel that stores the other panels
class MainPanel : MyTableLayoutPanel
{
    public MapAreaPanel mapArea;
    public BottomPanel bottom;
    public RightPanel right;
    public Bitmap tempIcon;
    //public MapAreaPanel MapArea; //redo. make a class with panels for dealing with each menu that can be open.
    public MainPanel()
    {
        MyTableLayoutPanel LoadingPanel = SetupLoadingPanel();
        frame.BackgroundImage = menuIcons[28];
        frame.Controls.Clear();
        frame.Controls.Add(LoadingPanel);
        frame.MinimumSize = new Size(500, 300);
        frame.MaximizeBox = true;
        frame.FormBorderStyle = FormBorderStyle.Sizable;
        frame.WindowState = FormWindowState.Maximized;
        frame.BackgroundImageLayout = ImageLayout.Stretch;
        LoadingPanel.Size = new Size(frame.Width, frame.Height);
        frame.Invalidate();
        frame.Update();
        mapArea = new MapAreaPanel(Worlds[ID].Map);
        bottom = new BottomPanel(Worlds[ID].EditedMap);
        right = new RightPanel(Worlds[ID].EditedMap);
        bool Resizing = false;
        tempIcon = new Bitmap(menuIcons[28], frame.Size);
        Size OldSize = frame.Size;
        frame.ResizeBegin += (sender, e) =>
        {
            if (!Resizing && Visible == true)
            {
                    frame.BackgroundImage  = HelperStuff.CaptureControlBitmap(this);
                    Visible = false;
            }
        };
        frame.ResizeEnd += (sender, e) =>
        {
            if (!Resizing && !frame.Size.Equals(OldSize))
            {
                Resizing = true;
                ResizeMainPanel(this);
                OldSize = frame.Size;
                Resizing = false;
            } else {
                Visible = true;
            }
        };  
        frame.Resize += (sender, e) =>
        {
            if (!Resizing && frame.WindowState == FormWindowState.Maximized)
            {
                Resizing = true;
                Visible = false;
                ResizeMainPanel(this);
                OldSize = frame.Size;
                Resizing = false;
            }
        };     
        RowCount = 4;
        ColumnCount = 3;
        Controls.Add(mapArea, 0, 0);
        SetRowSpan(mapArea, 2);
        SetColumnSpan(mapArea, 2);
        Controls.Add(right, 3, 0);
        SetRowSpan(right, 3);
        Controls.Add(bottom.GetMain(), 0, 3);
        SetColumnSpan(bottom.GetMain(), 4);
        BackgroundImage = menuIcons[8];
        ResizeMainPanel(this);
        DrawToBitmap(tempIcon, new Rectangle(new Point(0,0), Size));
        Visible = false;
        frame.BackgroundImage = new Bitmap(tempIcon, frame.Size);
        EndLoading(LoadingPanel);

        frame.Controls.Add(this);
        Visible = true;
    }

    public static MyTableLayoutPanel SetupLoadingPanel()
    {
        MyTableLayoutPanel MainPanel = new MyTableLayoutPanel();
        MainPanel.BackgroundImage = menuIcons[28];
        return MainPanel;
    }
    public static async void EndLoading(MyTableLayoutPanel MainPanel)
    {

        System.Windows.Forms.Timer resizeTimer = new System.Windows.Forms.Timer();

        bool isTimerRunning = true;


        MainPanel.SuspendLayout();

        resizeTimer.Interval = 20;

        resizeTimer.Tick += (sender, e)  =>
        {
            if (MainPanel.Size.Height < 10)
            {
                resizeTimer.Stop();
                MainPanel.Visible = false;
                isTimerRunning = false;
            }
            else
            {
                MainPanel.Size = new Size(MainPanel.Size.Width, MainPanel.Size.Height - 20);
                frame.Invalidate();
                frame.Update();
            }
        };

        // Start the Timer.
        resizeTimer.Start();

        await WaitForTimerAsync();

        async Task WaitForTimerAsync()
        {
            while (isTimerRunning)
            {
                await Task.Delay(20);
            }
        }
        frame.Controls.Remove(MainPanel);
    }
    public void ResizeMainPanel(MyTableLayoutPanel MainPanel)
    {
        MainPanel.Size = new Size(frame.Width - 15, frame.Height - 40);
        mapArea.ResizeAll();
        bottom.ResizeBottomPanel((MainPanel.Height - mapArea.Size.Height), MainPanel.Width);
        right.ResizePanel(mapArea.Size.Height, MainPanel.Width - mapArea.Size.Width);
        frame.Invalidate();
        frame.Update();
        MainPanel.Visible = true;
        frame.BackgroundImage = tempIcon;
    }
}

class MapAreaPanel : MyTableLayoutPanel
{
    public MapPanel mapPanel;
    public MapAreaPanel(int[][] map)
    {
        Margin = new Padding(0);
        mapPanel = new MapPanel(map);
        Controls.Add(mapPanel, 0, 0);
    }
    public void ResizeAll()
    {
        Size = new Size((int)(frame.Width / 1.3), (int)(frame.Height / 1.3));
        mapPanel.ResizeMap(Size);
        Size = mapPanel.Size;
        //wip
    }
}
class MapPanel : MyTableLayoutPanel
{
    public Button[][] buttons;
    private bool[] resizeFlags;
    public MapPanel(int[][] Map)
    {
        resizeFlags = new bool[Map.Length];
        buttons = new Button[Map.Length][];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = new Button[Map[0].Length];
        }

        ColumnCount = Map[0].Length;
        RowCount = Map.Length;
        BackColor = Color.SteelBlue;
        Margin = new Padding(0);

        SuspendLayout();

        for (int c = 0; c < ColumnCount; c++) 
        {
            for (int r = 0; r < RowCount; r++)
            {
                buttons[r][c] = new Button();

                buttons[r][c].FlatStyle = FlatStyle.Flat;
                buttons[r][c].FlatAppearance.BorderColor = BackColor;
                buttons[r][c].BackgroundImage = tileIcons[Map[r][c]];
                buttons[r][c].Margin = new Padding(0);
                buttons[r][c].BackgroundImageLayout = ImageLayout.Stretch;
                int column = c;
                int row = r;
                buttons[r][c].Click += (sender, e) =>
                {
                    Clicked(this, row, column);
                };
                Controls.Add(buttons[r][c], column, row);
            }
        }
        ResumeLayout();
        PerformLayout();
    }
    public void ResizeMap(Size size)
    {

        Size = size;
        SuspendLayout();
        Size cellSize = new Size(Size.Width / ColumnCount, Size.Height / RowCount);
        for (int r = 0; r < RowCount; r++)
        {
            resizeFlags[r] = false;
            ResizeRow(cellSize, r);
        }
        Size = new Size(cellSize.Width * ColumnCount, cellSize.Height * RowCount);
        HelperStuff.WaitForFlags(resizeFlags, 25);
        //PerformLayout();
        ResumeLayout();
        //Refresh();
    }
    async private void ResizeRow(Size cellSize, int row)
    {
        for (int c = 0; c < ColumnCount; c++)
        {
            buttons[row][c].Size = cellSize;
        }
        resizeFlags[row] = true;
    }
}
class ResourcePanel : MyTableLayoutPanel
{
    public static int PANEL_RATIO = 42;
    public Label Total;
    public Label Gain;
    public int ResourceID;
    public ResourcePanel(int id)
    {
        ColumnCount = 2;
        BackColor = Color.Transparent;
        Margin = new Padding(10,1,0,1);
        Total = new Label();
        Gain = new Label();
        ResourceID = id;

        Total.Margin = new Padding(0);
        Gain.Margin = new Padding(0);
        Total.BackgroundImage = menuIcons[29];
        Gain.BackgroundImage  = menuIcons[29];
        Total.TextAlign = ContentAlignment.TopCenter;
        Gain.TextAlign = ContentAlignment.TopCenter;
        Total.ForeColor = ResourceColors[id];

        Controls.Add(Total, 0, 0);
        Controls.Add(Gain, 1, 0);
    }
    public void UpdateInfo(World world)
    {
        //update text
        Total.Text = ResourceNames[ResourceID]+": " + world.Resources[ResourceID];
        Gain.Text = "" + resourceChange[ResourceID];

        //update font size
        HelperStuff.UpdateFont(Total);
        HelperStuff.UpdateFont(Gain);

        //update resource gain colors
        switch (resourceChange[ResourceID])
        {
            case > 0: Gain.ForeColor = Color.Green; Gain.Text = "+" + Gain.Text; break;
            case < 0: Gain.ForeColor = Color.Red; break;
            default: Gain.ForeColor = Color.Yellow; break;
        }
    }
    public void ResizePanel()
    {
        Width = Size.Width;
        Height = Size.Height;

        Size = new Size(Width, Height);
        Total.Size = new Size((Width / PANEL_RATIO) * 30, Height);
        Gain.Size = new Size(Width - Total.Size.Width, Height);
        Total.BackgroundImage = HelperStuff.GetLabelBackground((Width / PANEL_RATIO) * 30, Height, 29, ResourceColors[ResourceID], 2);
        Gain.BackgroundImage = HelperStuff.GetLabelBackground(Width - (Width / PANEL_RATIO) * 30, Height, 29, ResourceColors[ResourceID], 2);
    }

}
class BottomInfoPanel
{
    public OutlinedTableLayoutPanel MainPanel;
    private ResourcePanel[] ResourcePanels;
    private Button[] Buttons;
    private Label TimeDisplay;
    private TransparentMenuPanel OtherInfoPanel;
    private Label[] OtherLabels;
    public BottomInfoPanel()
    {
        MainPanel = new OutlinedTableLayoutPanel(4, 8, menuIcons);
        ResourcePanels = new ResourcePanel[8];
        Buttons = new Button[2];
        TimeDisplay = new Label();
        TimeDisplay.TextAlign = ContentAlignment.TopCenter;
        MainPanel.SuspendLayout();
        MainPanel.Margin = new Padding(0);
        MainPanel.BackgroundImage = menuIcons[8];

        //very wip. Icons next to time.
        OtherInfoPanel = new TransparentMenuPanel(1, 2, new Padding(3,1,3,1));
        OtherLabels = new Label[OtherInfoPanel.ColumnCount];
        for (int i = 0; i < OtherLabels.Length; i++)
        {
            OtherLabels[i] = new Label();
            OtherLabels[i].BackgroundImage = menuIcons[29];
            OtherLabels[i].Text = "E";
            OtherLabels[i].Margin = new Padding(0);
            OtherLabels[i].FlatStyle = FlatStyle.Flat;
            OtherLabels[i].TextAlign = ContentAlignment.TopCenter;
            OtherInfoPanel.Controls.Add(OtherLabels[i], i, 0);
        }
        MainPanel.Controls.Add(OtherInfoPanel, 2, 1);

        //setup the resource display panels
        for (int i = 0; i < ResourcePanels.Length; i++)
        {
            ResourcePanels[i] = new ResourcePanel(i);
        }

        MainPanel.Controls.Add(ResourcePanels[0], 3, 1);
        MainPanel.Controls.Add(ResourcePanels[1], 4, 1);
        MainPanel.Controls.Add(ResourcePanels[2], 5, 1);
        MainPanel.Controls.Add(ResourcePanels[3], 6, 1);
        MainPanel.Controls.Add(ResourcePanels[4], 3, 2);
        MainPanel.Controls.Add(ResourcePanels[5], 4, 2);
        MainPanel.Controls.Add(ResourcePanels[6], 5, 2);
        MainPanel.Controls.Add(ResourcePanels[7], 6, 2);

        TimeDisplay.BackgroundImage = menuIcons[29];
        TimeDisplay.Text = "Loading Info...";
        TimeDisplay.Margin = new Padding(3,1,3,1);
        TimeDisplay.FlatStyle = FlatStyle.Flat;
        MainPanel.Controls.Add(TimeDisplay, 1, 1);

        //setup the back to menu button
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i] = new Button();
            Buttons[i].BackgroundImage = menuIcons[28];
            Buttons[i].FlatStyle = FlatStyle.Flat;
            Buttons[i].FlatAppearance.BorderColor = Color.Yellow;
            Buttons[i].FlatAppearance.BorderSize = 3;
            Buttons[i].Margin = new Padding(3,1,3,1);
        }
        Buttons[0].Text = "Back To Menu";
        Buttons[1].Text = "Save Game";

        //menu button action:
        Buttons[0].Click += (sender, e) => 
        {
            mainTimer.Dispose();
            if (settings.AutoSave)
            {
                SaveGame((MainGui.GetControlFromPosition(0, 0) as MyTableLayoutPanel), ID);
            }
            frame.Visible = false;
            frame.Controls.Clear();
            frame.BackgroundImage = menuIcons[28];
            frame.MaximizeBox = false;
            frame.WindowState = FormWindowState.Minimized;
            frame.Size = new Size(1200, 590);
            frame.Controls.Add(LoaderGUIStuff.LoaderPanelSetup(menuIcons, Program.LoaderFontSize, ref Worlds, ref frame, ref settings));
            frame.FormBorderStyle = FormBorderStyle.FixedSingle;
            frame.Visible = true;
            frame.WindowState = FormWindowState.Normal;
            frame.Size = new Size(1200, 590);
        };

        //save button action:
        Buttons[1].Click += (sender, e) =>
        {
            ///save:
            SaveGame((MainGui.GetControlFromPosition(0, 0) as MyTableLayoutPanel), ID);
            //update bottom panel info:
            UpdateInfo(Worlds[ID]);
        };

        //

        MainPanel.Controls.Add(Buttons[0], 1, 2);
        MainPanel.Controls.Add(Buttons[1], 2, 2);

        MainPanel.ResumeLayout();
        MainPanel.PerformLayout();
    }
    async public void ResizeBottomPanel(int height, int width)
    {
        if (height <= 0 || width <= 0)
        {
            return;
        }
        //resize the entire panel
        MainPanel.SuspendLayout();
        MainPanel.Size = new Size(width, height);

        MainPanel.ResizeLines();

        //resize everything
        for (int i = 0; i < Buttons.Length; i++)
        {
            MainPanel.resizeControl(Buttons[i]);
            HelperStuff.UpdateFont(Buttons[i]);
        }
        for (int i = 0; i < ResourcePanels.Length; i++)
        {
            MainPanel.resizeControl(ResourcePanels[i]);
            ResourcePanels[i].ResizePanel();
            ResourcePanels[i].UpdateInfo(Worlds[ID]);
        }

        MainPanel.resizeControl(OtherInfoPanel);
        for (int i = 0; i < OtherLabels.Length; i++)
        {
            OtherLabels[i].Image = HelperStuff.GetLabelBackground
            (OtherLabels[i].Width, OtherLabels[i].Height, 29, Color.Yellow, 4);
            HelperStuff.UpdateFont(OtherLabels[i]);
        }

        MainPanel.resizeControl(TimeDisplay);
        TimeDisplay.Image = HelperStuff.GetLabelBackground
        (TimeDisplay.Width, TimeDisplay.Height, 29, Color.Yellow, 4);
        HelperStuff.UpdateFont(TimeDisplay);

        MainPanel.ResumeLayout();
        MainPanel.PerformLayout();
        MainPanel.Refresh();
    }
    public void UpdateInfo(World world)
    {
        TimeDisplay.Text = world.Time[0]+" days and "+world.Time[1]+" hours. "/*+(Saved ? "Saved." : "NOT SAVED!")+" (VERY WIP!)"*/;
        HelperStuff.UpdateFont(TimeDisplay);
        foreach (ResourcePanel panel in ResourcePanels)
        {
            panel.UpdateInfo(world);
        }

        OtherLabels[0].Text = "💾: "+ (Saved ? "✔" : "✘");
        OtherLabels[0].ForeColor = (Saved ? Color.Green : Color.Red);

        //wip
        OtherLabels[1].Text = "WIP";

        for (int i = 0; i < OtherLabels.Length; i++)
        {
            HelperStuff.UpdateFont(OtherLabels[i]);
        }
    }
}
class BottomEditPanel
{
    public OutlinedTableLayoutPanel MainPanel;
    private Button[] Buttons;
    private TransparentMenuPanel OtherInfoPanel;
    private Label[] OtherLabels;
    private TransparentMenuPanel temp;
    private TransparentMenuPanel TileSelect;
    private Label[] TSLabels;
    private Button[] TSButtons;

    public BottomEditPanel()
    {
        MainPanel = new OutlinedTableLayoutPanel(4, 8, menuIcons);
        Buttons = new Button[5];
        TSButtons = new Button[4];
        TSLabels = new Label[2];
        temp = new TransparentMenuPanel(1,1, new Padding(5));
        TileSelect = new TransparentMenuPanel(1, TSLabels.Length + TSButtons.Length, new Padding(5));
        MainPanel.SuspendLayout();
        MainPanel.Margin = new Padding(0);
        MainPanel.BackgroundImage = menuIcons[8];

        //very wip. Icons next to time.
        OtherInfoPanel = new TransparentMenuPanel(1, 2, new Padding(3,1,3,1));
        OtherLabels = new Label[OtherInfoPanel.ColumnCount];
        for (int i = 0; i < OtherLabels.Length; i++)
        {
            OtherLabels[i] = new Label();
            OtherLabels[i].BackgroundImage = menuIcons[29];
            OtherLabels[i].Margin = new Padding(0);
            OtherLabels[i].FlatStyle = FlatStyle.Flat;
            OtherInfoPanel.Controls.Add(OtherLabels[i], i, 0);
        }
        MainPanel.Controls.Add(OtherInfoPanel, 2, 1);

        for (int i = 0; i < TSLabels.Length; i++)
        {
            TSLabels[i] = new Label();
            TSLabels[i].BackgroundImage = menuIcons[29];
            TSLabels[i].Margin = new Padding(0);
            TSLabels[i].FlatStyle = FlatStyle.Flat;
            TSLabels[i].BackgroundImageLayout = ImageLayout.Stretch;
            TileSelect.Controls.Add(TSLabels[i], i + 2, 0);
        }
        for (int i = 0; i < TSButtons.Length; i++)
        {
            TSButtons[i] = new Button();
            TSButtons[i].BackgroundImage = menuIcons[28];
            TSButtons[i].FlatStyle = FlatStyle.Flat;
            TSButtons[i].FlatAppearance.BorderColor = Color.Yellow;
            TSButtons[i].FlatAppearance.BorderSize = 3;
            TSButtons[i].Margin = new Padding(0);
            if (i < 2)
            {
                TileSelect.Controls.Add(TSButtons[i], i, 0);
            }
            else
            {
                TileSelect.Controls.Add(TSButtons[i], i + 2, 0);
            }
        }
        TSButtons[0].Text = "◀◀";
        TSButtons[0].Click += (sender, e) =>
        {
            int newID = UpgradeID - 10;
            if (newID < 0)
            {
                newID = tiles.Length + newID;
            }
            UpgradeID = newID;
            UpdateInfo(Worlds[ID]);
        };
        TSButtons[1].Text = "◀";
        TSButtons[1].Click += (sender, e) =>
        {
            int newID = UpgradeID - 1;
            if (newID < 0)
            {
                newID = tiles.Length + newID;
            }
            UpgradeID = newID;
            UpdateInfo(Worlds[ID]);
        };
        TSButtons[2].Text = "▶";
        TSButtons[2].Click += (sender, e) =>
        {
            int newID = UpgradeID + 1;
            if (newID >= tiles.Length)
            {
                newID -= tiles.Length;
            }
            UpgradeID = newID;
            UpdateInfo(Worlds[ID]);
        };
        TSButtons[3].Text = "▶▶";
        TSButtons[3].Click += (sender, e) =>
        {
            int newID = UpgradeID + 10;
            if (newID >= tiles.Length)
            {
                newID -= tiles.Length;
            }
            UpgradeID = newID;
            UpdateInfo(Worlds[ID]);
        };

        MainPanel.Controls.Add(TileSelect, 3, 1);

        //setup the resource display panels

        //MainPanel.Controls.Add(ResourcePanels[0], 3, 1);
        //MainPanel.Controls.Add(ResourcePanels[1], 4, 1);
        //MainPanel.Controls.Add(ResourcePanels[2], 5, 1);
        //MainPanel.Controls.Add(ResourcePanels[3], 6, 1);
        //MainPanel.Controls.Add(ResourcePanels[4], 3, 2);
        //MainPanel.Controls.Add(ResourcePanels[5], 4, 2);
        //MainPanel.Controls.Add(ResourcePanels[6], 5, 2);
        //MainPanel.Controls.Add(ResourcePanels[7], 6, 2);

        //setup the back to menu button
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i] = new Button();
            Buttons[i].BackgroundImage = menuIcons[28];
            Buttons[i].FlatStyle = FlatStyle.Flat;
            Buttons[i].FlatAppearance.BorderColor = Color.Yellow;
            Buttons[i].FlatAppearance.BorderSize = 3;
            Buttons[i].Margin = new Padding(3,1,3,1);
        }
        Buttons[0].Text = "Back To Menu";
        Buttons[1].Text = "Save Game as Editing";
        Buttons[2].Text = "Save Game as Playing";
        Buttons[3].Text = "Set on Click: false";
        Buttons[4].Text = "Set Tile";

        //menu button action:
        Buttons[0].Click += (sender, e) => 
        {
            if (settings.AutoSave)
            {
                SaveGame((MainGui.GetControlFromPosition(0, 0) as MyTableLayoutPanel), ID);
            }
            frame.Visible = false;
            frame.Controls.Clear();
            frame.BackgroundImage = menuIcons[28];
            frame.MaximizeBox = false;
            frame.WindowState = FormWindowState.Minimized;
            frame.Size = new Size(1200, 590);
            frame.Controls.Add(LoaderGUIStuff.LoaderPanelSetup(menuIcons, Program.LoaderFontSize, ref Worlds, ref frame, ref settings));
            frame.FormBorderStyle = FormBorderStyle.FixedSingle;
            frame.Visible = true;
            frame.WindowState = FormWindowState.Normal;
            frame.Size = new Size(1200, 590);
        };

        //save button action:
        Buttons[1].Click += (sender, e) =>
        {
            Worlds[ID].EditedMap = true;
            ///save:
            SaveGame((MainGui.GetControlFromPosition(0, 0) as MyTableLayoutPanel), ID);
            //update bottom panel info:
            UpdateInfo(Worlds[ID]);
        };
        Buttons[2].Click += (sender, e) =>
        {
            Worlds[ID].EditedMap = false;
            ///save:
            SaveGame((MainGui.GetControlFromPosition(0, 0) as MyTableLayoutPanel), ID);
            //update bottom panel info:
            UpdateInfo(Worlds[ID]);
        };
        Buttons[4].Click += (sender, e) =>
        {
            SetTile(UpgradeID);
        };
        Buttons[3].Click += (sender, e) =>
        {
            UpdateOnClick = !UpdateOnClick;
            UpdateInfo(Worlds[ID]);
        };

        //

        MainPanel.Controls.Add(Buttons[0], 1, 1);
        MainPanel.Controls.Add(Buttons[1], 1, 2);
        MainPanel.Controls.Add(Buttons[2], 2, 2);
        MainPanel.Controls.Add(Buttons[3], 3, 2);
        MainPanel.Controls.Add(Buttons[4], 4, 2);

        MainPanel.ResumeLayout();
        MainPanel.PerformLayout();
    }
    async public void ResizeBottomPanel(int height, int width)
    {
        if (height <= 0 || width <= 0)
        {
            return;
        }
        //resize the entire panel
        MainPanel.SuspendLayout();
        MainPanel.Size = new Size(width, height);

        MainPanel.ResizeLines();

        //resize everything
        for (int i = 0; i < Buttons.Length; i++)
        {
            MainPanel.resizeControl(Buttons[i]);
            HelperStuff.UpdateFont(Buttons[i]);
        }

        MainPanel.resizeControl(OtherInfoPanel);
        for (int i = 0; i < OtherLabels.Length; i++)
        {
            OtherLabels[i].Image = HelperStuff.GetLabelBackground
            (OtherLabels[i].Width, OtherLabels[i].Height, 29, Color.Yellow, 4);
            HelperStuff.UpdateFont(OtherLabels[i]);
        }

        MainPanel.resizeControl(TileSelect);
        for (int i = 0; i < TSButtons.Length; i++)
        {
            HelperStuff.UpdateFont(TSButtons[i]);
        }
        TSLabels[1].Image = HelperStuff.GetLabelBackground
        (TSLabels[1].Width, TSLabels[1].Height, 29, Color.Yellow, 4);
        HelperStuff.UpdateFont(TSLabels[1]);

        TSLabels[0].Image = HelperStuff.OutlineImage(tileIcons[UpgradeID], Color.Yellow, 4);

        MainPanel.ResumeLayout();
        MainPanel.PerformLayout();
        MainPanel.Refresh();
    }
    public void UpdateInfo(World world)
    {
        OtherLabels[0].Text = "💾: "+ (Saved ? "✔" : "✘");
        OtherLabels[0].ForeColor = (Saved ? Color.Green : Color.Red);

        TSLabels[1].Text = "" + UpgradeID;
        HelperStuff.UpdateFont(TSLabels[1]);

        Buttons[3].Text = "Set on Click: " + UpdateOnClick;

        TSLabels[0].Image = HelperStuff.OutlineImage( new Bitmap(tileIcons[UpgradeID], TSLabels[0].Width,TSLabels[0].Height), Color.Yellow, 4);

        //wip
        OtherLabels[1].Text = "WIP";

        for (int i = 0; i < OtherLabels.Length; i++)
        {
            HelperStuff.UpdateFont(OtherLabels[i]);
        }
    }
}
public class RightPanel : MyTableLayoutPanel
{
    private bool EditModeSetup;
    //top panel stuff
    private OutlinedTableLayoutPanel TopPanel;
    private Label[] Icons = new Label[5]; // 0 = selected tile icon. 1,2,3 = upgrade 1,2,3 icon. 4 = tile name.
    private Button[] TopButtons = new Button[4]; //1,2,3 = upgrade 1,2,3 button. 0 = tile info.
    //bottom panel stuff
    private OutlinedTableLayoutPanel BottomPanel;
    private Button[] BottomButtons;
    private TransparentMenuPanel TimePanel;
    private Button[] TimeButtons;
    private Label TempBottom;
    private int ID = 0;
    public RightPanel(bool EditMode)
    {
        EditModeSetup = EditMode;
        // setup top
        TopPanel = new OutlinedTableLayoutPanel(5, 6, menuIcons);
        TopPanel.Margin = new Padding(0);
        for (int i = 0; i < Icons.Length; i++)
        {
            Icons[i] = new Label();
            Icons[i].Margin = new Padding(0, 0, 0, 5);
            Icons[i].BackgroundImageLayout = ImageLayout.Stretch;
        }
        TopPanel.Controls.Add(Icons[0], 1, 1);
        TopPanel.Controls.Add(Icons[1], 3, 2);
        TopPanel.Controls.Add(Icons[2], 1, 3);
        TopPanel.Controls.Add(Icons[3], 3, 3);
        TopPanel.Controls.Add(Icons[4], 2, 1);
        TopPanel.SetColumnSpan(Icons[4], 3);
        Icons[4].Text = "Sea";
        Icons[4].BackgroundImageLayout = ImageLayout.None;

        for (int i = 0; i < TopButtons.Length; i++)
        {
            TopButtons[i] = new Button();
            TopButtons[i].Margin = new Padding(0, 0, 5, 5);
            TopButtons[i].BackgroundImage = menuIcons[28];
            TopButtons[i].FlatStyle = FlatStyle.Flat;
            TopButtons[i].FlatAppearance.BorderColor = Color.Yellow;
            TopButtons[i].FlatAppearance.BorderSize = 3;
            TopButtons[i].Text = "🢁";
        }
        TopButtons[0].Click += (sender, e) =>
        {
            TopButtons[0].FlatAppearance.BorderColor = Color.Red;
            TopButtons[0].Text = "WIP";
        };

        for (int i = 1; i < TopButtons.Length; i++)
        {
            int sendNum = i;
            TopButtons[i].Click += (sender, e) =>
            {
                UpgradeTile(sendNum);
            };
        }


        TopPanel.Controls.Add(TopButtons[0], 1, 2);
        TopPanel.SetColumnSpan(TopButtons[0], 2);
        TopButtons[0].Text = "Tile Info";

        TopPanel.Controls.Add(TopButtons[1], 4, 2);
        TopPanel.Controls.Add(TopButtons[2], 2, 3);
        TopPanel.Controls.Add(TopButtons[3], 4, 3);

        // setup bottom (Normal)
        if(!EditModeSetup)
        {
            BottomPanel = new OutlinedTableLayoutPanel( 5, 4, menuIcons);
            BottomPanel.Margin = new Padding(0);

            BottomButtons = new Button[6];
            string[] ButtonLabels = {"💰","👑","🤝","⚖️","📢","🔬"};
            for (int i = 0; i < BottomButtons.Length; i++)
            {
                BottomButtons[i] = new Button();
                BottomButtons[i].Margin = new Padding(5, 5, 5, 5);
                BottomButtons[i].BackgroundImage = menuIcons[28];
                BottomButtons[i].FlatStyle = FlatStyle.Flat;
                BottomButtons[i].FlatAppearance.BorderColor = Color.Red;
                BottomButtons[i].FlatAppearance.BorderSize = 3;
                BottomButtons[i].Text = ButtonLabels[i];
                BottomPanel.Controls.Add(BottomButtons[i], 1 + (i >= 3 ? 1 : 0), 1 + i % 3);
            }
            //wip button
            BottomButtons[0].Click += (sender, e) => 
            {};
            //wip button
            BottomButtons[1].Click += (sender, e) => 
            {};
            //wip button
            BottomButtons[2].Click += (sender, e) => 
            {};
            //wip button
            BottomButtons[3].Click += (sender, e) => 
            {};
            //wip button
            BottomButtons[4].Click += (sender, e) => 
            {};
            //wip button
            BottomButtons[5].Click += (sender, e) => 
            {};

            TimeButtons = new Button[5];
            string[] TimeLabels = {"┃┃","┃▶","▶","▶▶","▶▶▶"};
            TimePanel = new TransparentMenuPanel(1, TimeButtons.Length, new Padding(0,10,0,0));
            for (int i = 0; i < TimeButtons.Length; i++)
            {
                TimeButtons[i] = new Button();
                TimeButtons[i].Margin = new Padding(0);
                TimeButtons[i].BackgroundImage = menuIcons[28];
                TimeButtons[i].FlatStyle = FlatStyle.Flat;
                TimeButtons[i].FlatAppearance.BorderColor = Color.Yellow;
                TimeButtons[i].FlatAppearance.BorderSize = 3;
                TimeButtons[i].Text = TimeLabels[i];

                int buttonSpeed = i;
                TimeButtons[i].Click += (sender,e) => {
                    Game.speed = buttonSpeed;
                    UpdateTime();
                };

                TimePanel.Controls.Add(TimeButtons[i], i, 0);
            }
            TimePanel.Anchor = AnchorStyles.Bottom;
            BottomPanel.Controls.Remove(BottomPanel.lines[1]);
            //BottomPanel.lines[1] = TimePanel;
            BottomPanel.Controls.Add(TimePanel, 1, BottomPanel.RowCount - 1);
            BottomPanel.SetColumnSpan(TimePanel, 2);
            Controls.Add(BottomPanel, 0, 1);


        } else {
            // setup bottom (editMode)
            TempBottom = new Label();
            TempBottom.Margin = new Padding(0);
            Controls.Add(TempBottom, 0, 1);
        }

        // setup main
        Margin = new Padding(0);
        RowCount = 2;
        Controls.Add(TopPanel, 0, 0);
        UpdateTime();
    }
    public void ResizePanel(int height, int width)
    {
        //resize entire panel
        Size = new Size(width, height);

        //resize top
        TopPanel.Size = new Size(width, height / 2);
        TopPanel.ResizeLines();
        for (int i = 0; i < Icons.Length; i++)
        {
            TopPanel.resizeControl(Icons[i]);
        }
        for (int i = 0; i < 3; i++)
        {
            Icons[i + 1].BackgroundImage = HelperStuff.OutlineImage
            (tiles[ID].Upgrades[i] == -1 ? menuIcons[29] : tileIcons[tiles[ID].Upgrades[i]], Color.Yellow, 4);
        }
        HelperStuff.UpdateFont(Icons[4]);
        Icons[4].BackgroundImage = HelperStuff.GetLabelBackground(Icons[4].Width, Icons[4].Height, 29, Color.Yellow, 3);
        Icons[0].BackgroundImage = HelperStuff.OutlineImage(tileIcons[ID], Color.Yellow, 4);
        for (int i = 0; i < TopButtons.Length; i++)
        {
            TopPanel.resizeControl(TopButtons[i]);
            HelperStuff.UpdateFont(TopButtons[i]);
        }

        // resize bottom (Normal)
        if(!EditModeSetup)
        {
            BottomPanel.Size = new Size(width, height / 2);
            BottomPanel.ResizeLines();
            BottomPanel.resizeControl(TimePanel);
            for (int i = 0; i < BottomButtons.Length; i++)
            {
                BottomPanel.resizeControl(BottomButtons[i]);
                HelperStuff.UpdateFont(BottomButtons[i]);
            }
            for (int i = 0; i < TimeButtons.Length; i++)
            {
                TimePanel.ResizeControl(TimeButtons[i]);
                HelperStuff.UpdateFont(TimeButtons[i]);
            }
            
        } else {
            // resize bottom (editMode)
            TempBottom.Size = new Size(width, height / 2);
            TempBottom.BackgroundImage = HelperStuff.GetLabelBackground
            (TempBottom.Size.Width, TempBottom.Size.Height, 28, Color.Red, 4);
        }
        
    }
    public void UpdateInfo(int ID)
    {
        for (int i = 0; i < 3; i++)
        {
            Icons[i + 1].BackgroundImage = HelperStuff.OutlineImage
            (tiles[ID].Upgrades[i] == -1 ? menuIcons[29] : tileIcons[tiles[ID].Upgrades[i]], Color.Yellow, 4);
        }
        Icons[4].Text = tiles[ID].Name;
        HelperStuff.UpdateFont(Icons[4]);
        Icons[4].BackgroundImage = HelperStuff.GetLabelBackground
        (Icons[4].Width, Icons[4].Height, 29, Color.Yellow, 3);
        Icons[0].BackgroundImage = HelperStuff.OutlineImage
        (tileIcons[ID], Color.Yellow, 4);
        this.ID = ID;
        CheckUpgrades();
    }
    public void CheckUpgrades()
    {
        int[][] Cost = new int[3][];
        Cost[0] = tiles[ID].Cost1;
        Cost[1] = tiles[ID].Cost2;
        Cost[2] = tiles[ID].Cost3;
        int tileID = ID;

        for (int i = 0; i < tiles[ID].Upgrades.Length; i++)
        {
            if (tiles[ID].Upgrades[i] == -1)
            {
                TopButtons[i + 1].ForeColor = Color.SlateGray;
            }
            else if (ResourceCheck(Cost[i]))
            {
                TopButtons[i + 1].ForeColor = Color.Yellow;
            }
            else
            {
                TopButtons[i + 1].ForeColor = Color.Black;
            }
        }
    }
    public void UpdateTime() {
        for (int i = 0; i < TimeButtons.Length; i++)
        {
            TimeButtons[i].ForeColor = Color.Black;
        }
        TimeButtons[speed].ForeColor = Color.Yellow;
    }
}