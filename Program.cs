using System.Runtime.InteropServices;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;

namespace Tiles;

internal static class Program
{
    // random thing that somehow removes the console!
    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    private static extern bool FreeConsole();

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    public static string GameState = "Loading";

    //1 is worst. 8 is in the middle. 20 or so is the best. Default: 16
    //aka, default image size. (images are resized on load to prevent blurriness)
    private static readonly int ImageQuality = 6; // DO NOT CHANGE!!!
    public static readonly bool DEBUG = true;
    public static readonly int LoaderFontSize = 36;
    public static readonly string VERSION = "0.4.0";
    private static Tile[] tiles;
    private static Bitmap[] tileIcons;
    private static Bitmap[] menuIcons = new Bitmap[30];
    private static Bitmap NO_IMAGE_ICON;
    private static World[] Worlds;
    public static Form frame;

    public static Settings settings;

    //load json files and do some setup
    //then, call the function to create the first panel (main menu).
    [STAThread]
    public static void Main(string[] args)
    {
        AllocConsole();

        Console.WriteLine(
            "////////////////////////////////////////////\n//  Tiles (C# PORT) by William Herbert   //\n//  Version  "
            + VERSION + "                      //\n/////////////////////////////////////////");

        Console.WriteLine(
            "- For bug reporting, updates, and useful info,\n- go to https://github.com/VAST-THE-DOGE/Tiles");

        Console.WriteLine("- SETUP:");

        Console.WriteLine("- Creating Error Reporting:");
        Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
        AppDomain.CurrentDomain.UnhandledException +=
            new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        Console.WriteLine("- Getting file path...");

        //get the path to the parent folder
        string FolderPath = Directory.GetCurrentDirectory();

        Console.WriteLine("- PATH: " + FolderPath);

        Console.WriteLine("- Loading Data...");

        //create discord rich presence.
        Console.WriteLine("- Setting up Discord Rich Presence");
        RichPresenceHelper.Initialize();
        RichPresenceHelper.UpdateActivity("Loading...", "Tiles");

        //check for updates
        //will run async sadly
        GithubHelper.CheckForUpdate();

        //TODO: move all to using the bug report system


        //load the tile info
        try
        {
            string Json = File.ReadAllText(FolderPath + @"\Data\TileData.json");
            tiles = JsonSerializer.Deserialize<Tile[]>(Json);
            Console.WriteLine("- TileData Loaded");
        }
        catch
        {
            Console.WriteLine("[!] ERROR: Loading " + FolderPath + @"\Data\TileData.json");
            Console.WriteLine
                ("Press Enter to exit. Please verify that the file is in the correct location!");
            Console.ReadLine();
            Environment.Exit(1);
        }

        //load saved worlds
        try
        {
            string Json = File.ReadAllText(FolderPath + @"\Data\SavedWorlds.json");
            Worlds = JsonSerializer.Deserialize<World[]>(Json);
            //Worlds = LegacyWorldGroup.TransferWorld(JsonSerializer.Deserialize<Object[]>(Json));
            Console.WriteLine("- SavedWorlds Loaded");
        }
        catch
        {
            Console.WriteLine("[!] ERROR: Loading " + FolderPath + @"\Data\SavedWorlds.json");
            Console.WriteLine
                ("Press Enter to exit. Please verify that the file is in the correct location!");
            Console.ReadLine();
            Environment.Exit(1);
        }

        // load settings
        try
        {
            string Json = File.ReadAllText(FolderPath + @"\Data\Settings.json");
            settings = JsonSerializer.Deserialize<Settings>(Json);
            Console.WriteLine("- Settings Loaded");
        }
        catch
        {
            Console.WriteLine("[!] ERROR: Loading " + FolderPath + @"\Data\Settings.json");
            Console.WriteLine
                ("Press Enter to exit. Please verify that the file is in the correct location!");
            Console.ReadLine();
            Environment.Exit(1);
        }

        Console.WriteLine("- Loading Images...");

        // load the no image icon!
        try
        {
            var stream = new Bitmap(FolderPath + @"\Data\ImageData\NoImageIcon.png");
            NO_IMAGE_ICON = HelperStuff.ResizeImage(new Bitmap(stream), 16 * ImageQuality, 16 * ImageQuality);
            HelperStuff.NO_IMAGE_ICON = NO_IMAGE_ICON;
            Console.WriteLine("- NO_IMAGE_ICON Loaded.");
        }
        catch
        {
            Console.Error.WriteLine("[!] ERROR: NO_IMAGE_ICON has not been loaded!");
            Console.WriteLine
                ("Press Enter to exit. Please verify that the file is in the correct location!");
            Console.ReadLine();
            Environment.Exit(1);
        }

        //load tile images (to be remade to allow for different tile skins).
        try
        {
            Console.WriteLine("- Loading Tile Images...");
            //load other images
            tileIcons = new Bitmap[tiles.Length];

            for (int i = 0; i < tileIcons.Length; i++)
            {
                tileIcons[i] = HelperStuff.ResizeImage(HelperStuff.LoadImage("Tile" + i), 16 * ImageQuality,
                    16 * ImageQuality);
            }

            Console.WriteLine("- Loading Menu Images...");
            for (int i = 0; i < menuIcons.Length; i++)
            {
                menuIcons[i] = HelperStuff.ResizeImage(HelperStuff.LoadImage("Menu" + i), 16 * ImageQuality,
                    16 * ImageQuality);
            }
        }
        catch
        {
            Console.Error.WriteLine("[!] ERROR: Unknown Image Load/Set Error.");
            Console.WriteLine
                ("Press Enter to exit. Please verify files and send a bug report!");
            Console.ReadLine();
            Environment.Exit(1);
        }

        //set the data up for Game. (to be redone later)
        Console.WriteLine("- Setting Game Data...");
        try
        {
            Game.settings = settings;
            Game.menuIcons = menuIcons;
            Game.tiles = tiles;
            Game.Worlds = Worlds;
            Game.tileIcons = tileIcons;
            Game.NO_IMAGE_ICON = NO_IMAGE_ICON;
            Game.frame = frame;
        }
        catch
        {
            Console.WriteLine
                ("[!] ERROR: Unknown Error While Setting Game Data.");
            Console.WriteLine
                ("Press Enter to exit. Please send a bug report!");
            Console.ReadLine();
            Environment.Exit(1);
        }

        //setup the window and stuff.
        Console.WriteLine("- Creating Frame...");
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frame = new MyForm();
            frame.Text = "Tiles " + VERSION;
            frame.FormClosing += (sender, e) => { Application.Exit(); };

            //setup cursor stuff
            for (int i = 0; i < HelperStuff.cursors.Length; i++)
            {
                HelperStuff.cursors[i] = new Cursor(
                    HelperStuff.LoadImage("CursorImage" + i + "").GetHicon());
            }

            Cursor cursor = HelperStuff.cursors[0];

            Game.frame = frame;

            // load the icon
            Bitmap imgIcon = HelperStuff.LoadImage("TilesLogoV2");
            frame.Icon = Icon.FromHandle(imgIcon.GetHicon());

            frame.BackColor = Color.SandyBrown;
            frame.Controls.Add(LoaderGUIStuff.LoaderPanelSetup
                (menuIcons, LoaderFontSize, ref Worlds, ref frame, ref settings));

            //frame.FormBorderStyle = FormBorderStyle.FixedDialog;
            frame.FormBorderStyle = FormBorderStyle.FixedSingle;
            frame.Size = new Size(1200, 590);
            frame.MaximizeBox = false;
        }
        catch
        {
            Console.WriteLine
                ("[!] ERROR: Unknown Error While Creating Frame.");
            Console.WriteLine
                ("Press Enter to exit. Please send a bug report!");
            Console.ReadLine();
            Environment.Exit(1);
        }

        Console.WriteLine("- Closing Console...");
        try
        {
            FreeConsole();
        }
        catch
        {
            Console.WriteLine("[!] ERROR: Console Close.");
            Console.WriteLine
                ("This Error Can Be Ignored. Please send a bug report!");
        }

        //Application.SetCompatibleTextRenderingDefault(false);
        frame = new MyForm();
        frame.Text = "Tiles " + VERSION;
        frame.FormClosing += (sender, e) => { Application.Exit(); };

        Game.frame = frame;

        // load the icon
        var img = HelperStuff.LoadImage("TilesLogoV2");
        frame.Icon = Icon.FromHandle(img.GetHicon());

        frame.BackColor = Color.SandyBrown;
        frame.Controls.Add(LoaderGUIStuff.LoaderPanelSetup
            (menuIcons, LoaderFontSize, ref Worlds, ref frame, ref settings));

        //frame.FormBorderStyle = FormBorderStyle.FixedDialog;
        frame.FormBorderStyle = FormBorderStyle.FixedSingle;
        frame.Size = new Size(1200, 590);
        frame.MaximizeBox = false;

        //Application.SetCompatibleTextRenderingDefault(false);
        //ApplicationConfiguration.Initialize();
        //Application.Run(new Loader(Properties.Settings.Default.Fullscreen));

        Application.Run(frame);

        RichPresenceHelper.DisposeClient();
    }

    private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
        GithubHelper.ShowError(e.Exception, GetErrorLevel(e.Exception), GameState);
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        GithubHelper.ShowError(e.ExceptionObject as Exception, GetErrorLevel(e.ExceptionObject as Exception),
            GameState);
    }

    private static string GetErrorLevel(Exception? exception)
    {
        return exception switch
        {
            _ => "Normal (NA)"
        };
    }
}