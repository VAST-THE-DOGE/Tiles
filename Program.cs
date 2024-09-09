using System.Runtime.InteropServices;
using System.Text.Json;

namespace Tiles;

public static class Program
{
	public static string GameState = "Loading";
	public static readonly bool DEBUG = true;
	public static readonly int LoaderFontSize = 36;
	private static Tile[] tiles;
	private static Bitmap[] tileIcons;
	private static Bitmap[] menuIcons = new Bitmap[30];
	private static Bitmap NO_IMAGE_ICON;
	private static World[] Worlds;
	public static Form frame;

	public static Settings settings;

	public static string VERSION => GlobalVariableManager.VERSION;

	// random thing that somehow removes the console!
	[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
	private static extern bool FreeConsole();

	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool AllocConsole();

	//load json files and do some setup
	//then, call the function to create the first panel (main menu).
	[STAThread]
	public static void Main(string[] args)
	{
		//TODO: verify that this (entire game) works offline.

		AllocConsole();

		Console.WriteLine(
			"////////////////////////////////////////////\n//  Tiles (C# PORT) by William Herbert   //\n//  Version  "
			+ GlobalVariableManager.VERSION + "                      //\n/////////////////////////////////////////");

		Console.WriteLine(
			"- For bug reporting, updates, and useful info,\n- go to https://github.com/VAST-THE-DOGE/Tiles");

		Console.WriteLine("- SETUP:");

#if !DEBUG
		Console.WriteLine("- Creating Error Reporting:");
		Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
		AppDomain.CurrentDomain.UnhandledException +=
			new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
#endif

		Console.WriteLine("- Getting file path...");

		//get the path to the parent folder
		var FolderPath = Directory.GetCurrentDirectory();

		Console.WriteLine("- PATH: " + FolderPath);

		Console.WriteLine("- Loading Data...");

		//create discord rich presence.
		Console.WriteLine("- Setting up Discord Rich Presence");
		RichPresenceHelper.Initialize();
		RichPresenceHelper.UpdateActivity("Loading...", "Tiles");

		// check for updates
		// will run async sadly
		GithubHelper.CheckForUpdate();

		//load the tile info
		{
			var Json = File.ReadAllText(FolderPath + @"\Data\TileData.json");
			tiles = JsonSerializer.Deserialize<Tile[]>(Json);
			Console.WriteLine("- TileData Loaded");
		}

		//load saved worlds
		{
			var Json = File.ReadAllText(FolderPath + @"\Data\SavedWorlds.json");
			Worlds = JsonSerializer.Deserialize<World[]>(Json);
			//Worlds = LegacyWorldGroup.TransferWorld(JsonSerializer.Deserialize<Object[]>(Json));
			Console.WriteLine("- SavedWorlds Loaded");
		}

		// load settings
		{
			var Json = File.ReadAllText(FolderPath + @"\Data\Settings.json");
			settings = JsonSerializer.Deserialize<Settings>(Json);
			Console.WriteLine("- Settings Loaded");
		}

		Console.WriteLine("- Loading Images...");

		// load the no image icon!
		var stream = new Bitmap(FolderPath + @"\Data\ImageData\NoImageIcon.png");
		NO_IMAGE_ICON = HelperStuff.ResizeImage(new Bitmap(stream), 16 * GlobalVariableManager.MenuImageQuality,
			16 * GlobalVariableManager.MenuImageQuality);
		HelperStuff.NO_IMAGE_ICON = NO_IMAGE_ICON;
		Console.WriteLine("- NO_IMAGE_ICON Loaded.");

		//load tile images (to be remade to allow for different tile skins).
		Console.WriteLine("- Loading Tile Images...");
		//load other images
		tileIcons = new Bitmap[tiles.Length];

		for (var i = 0; i < tileIcons.Length; i++)
		{
			tileIcons[i] = HelperStuff.ResizeImage(HelperStuff.LoadImage("Tile" + i),
				16 * GlobalVariableManager.TileImageQuality,
				16 * GlobalVariableManager.TileImageQuality);
		}

		Console.WriteLine("- Loading Menu Images...");
		for (var i = 0; i < menuIcons.Length; i++)
		{
			menuIcons[i] = HelperStuff.ResizeImage(HelperStuff.LoadImage("Menu" + i),
				16 * GlobalVariableManager.MenuImageQuality,
				16 * GlobalVariableManager.MenuImageQuality);
		}

		//set the data up for Game. (to be redone later)
		Console.WriteLine("- Setting Game Data...");
		GlobalVariableManager.settings = settings;
		GlobalVariableManager.tileInfo = tiles;
		BasicGuiManager.NO_IMAGE_ICON = NO_IMAGE_ICON;
		BasicGuiManager.ExtraEffects = settings.ExtraEffects;
		BasicGuiManager.TileIcons = tileIcons;
		BasicGuiManager.MenuIcons = menuIcons;

		//setup the window and stuff.
		Console.WriteLine("- Creating Frame...");

		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		//frame = new MyForm();
		//frame.Text = "Tiles " + VERSION;
		//frame.FormClosing += (sender, e) => { Application.Exit(); };

		//setup cursor stuff
		var bounds = Screen.PrimaryScreen.Bounds;

		for (var i = 0; i < HelperStuff.cursors.Length; i++)
		{
			var cursorImg = HelperStuff.LoadImage("CursorImage" + i + "");

			HelperStuff.cursors[i] = new Cursor(cursorImg.GetHicon());
			//TODO: very small cursor on high dpi screen (aka 4k)
			/*new Cursor(
				HelperStuff.ResizeImage( cursorImg,
						cursorImg.Width * (int)Math.Ceiling(bounds.Height / 500f),
						cursorImg.Height * (int)Math.Ceiling(bounds.Height / 500f))
					.GetHicon());*/
		}

		//var cursor = HelperStuff.cursors[0];


		// load the icon
		//var imgIcon = HelperStuff.LoadImage("TilesLogoV2");
		//frame.Icon = Icon.FromHandle(imgIcon.GetHicon());

		//frame.BackColor = Color.SandyBrown;
		//frame.Controls.Add(LoaderGUIStuff.LoaderPanelSetup
		//	(menuIcons, LoaderFontSize, ref Worlds, ref frame, ref settings));

		//frame.FormBorderStyle = FormBorderStyle.FixedDialog;
		//frame.FormBorderStyle = FormBorderStyle.FixedSingle;
		//frame.Size = new Size(1200, 590);
		//frame.MaximizeBox = false;

		if (GithubHelper.UpdateCheckStatus < 2)
		{
			Console.WriteLine("Waiting for Github Update Checker ...");

			var waitTime = 0;

			while ((GithubHelper.UpdateCheckStatus == 1) || (GithubHelper.UpdateCheckStatus == 0 && waitTime++ < 50))
			{
				Thread.Sleep(100);
			}

			if (waitTime >= 50)
			{
				Console.WriteLine("[!] ERROR: Github Update Check Failed.");
			}
		}

		if (GithubHelper.UpdateCheckStatus == 3)
		{
			Console.WriteLine("Current version is up to date.");
		}

		Console.WriteLine("- Closing Console...");
		FreeConsole();

		//Application.SetCompatibleTextRenderingDefault(false);
		//frame = new MyForm();
		//frame.Text = "Tiles " + VERSION;
		//frame.FormClosing += (sender, e) => { Application.Exit(); };


		// load the icon
		//var img = HelperStuff.LoadImage("TilesLogoV2");
		//frame.Icon = Icon.FromHandle(img.GetHicon());

		//frame.Controls.Add(LoaderGUIStuff.LoaderPanelSetup
		//	(menuIcons, LoaderFontSize, ref Worlds, ref frame, ref settings));

		//frame.FormBorderStyle = FormBorderStyle.FixedDialog;
		//frame.FormBorderStyle = FormBorderStyle.Sizable;
		//frame.Size = new Size(1200, 590);
		//frame.StartPosition = FormStartPosition.CenterScreen;
		//frame.MaximizeBox = true;

		//TODO: resizes very weird on high DPI monitors (use 4k laptop monitor to test)

		Application.EnableVisualStyles();
		//Application.SetHighDpiMode(HighDpiMode.PerMonitorV2); // use for no scaling of fonts???
		Application.SetCompatibleTextRenderingDefault(false);
		//ApplicationConfiguration.Initialize();
		Application.Run(new Loader(Properties.Settings.Default.Fullscreen));

		//GlobalVariableManager.frame = frame;

		//Application.Run(frame);

		RichPresenceHelper.DisposeClient();
	}

	private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
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