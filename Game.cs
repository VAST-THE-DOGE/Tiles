using System.Drawing.Imaging;
using Timer = System.Threading.Timer;

namespace Tiles;

using static RichPresenceHelper;
using static HelperStuff;

public class Game : Form
{
	public static bool EditorMode;

	//edit mode only:
	public static bool UpdateOnClick = false;

	public static int UpgradeID = 0;

	/////////////////
	public static Tile[] tiles;
	public static int[] selected = { 0, 0 };
	public static Bitmap[] tileIcons;
	public static Bitmap[] menuIcons = new Bitmap[30];
	public static Bitmap NO_IMAGE_ICON;
	public static bool inMainMenu = true;
	public static int WorldID;

	public static int popDebuff = 0;

	// game speed
	public static int speed = 0;
	public static World[] Worlds;
	public static Form frame;
	public static MainPanel MainGui;
	public static int ID;
	public static bool Saved = true;

	public static readonly string[] ResourceNames =
		{ "Gold", "Iron", "Stone", "Wood", "Water", "Food", "Workers", "Research" }; //move to json

	public static readonly Color[] ResourceColors =
	{
		Color.Yellow, Color.Red, Color.Gray, Color.Green, Color.Blue, Color.Orange, Color.Chocolate, Color.Magenta
	};

	public static Settings settings;
	private static int tick = 0;
	public static Timer mainTimer;
	public static int[] resourceChange = { 0, 0, 0, 0, 0, 0, 0, 0 };

	public Game(int ID, bool EditorMode, ref Form frame, World[] Worlds, Settings settings)
	{
		//verify that there are the new int values in the world
		if (Worlds[ID].TileStatus is null)
		{
			Worlds[ID].TileStatus = new int[Worlds[ID].Map.Length][];
			for (var i = 0; i < Worlds[ID].Map.Length; i++)
			{
				Worlds[ID].TileStatus[i] = new int[Worlds[ID].Map[i].Length];
			}
		}

		if (Worlds[ID].TileTimers is null)
		{
			Worlds[ID].TileTimers = new int[Worlds[ID].Map.Length][];
			for (var i = 0; i < Worlds[ID].Map.Length; i++)
			{
				Worlds[ID].TileTimers[i] = new int[Worlds[ID].Map[i].Length];
			}
		}

		if (Worlds[ID].SellPrice is null)
		{
			Worlds[ID].SellPrice = [0, 20, 50, 500, 1300, 1200];
		}

		if (Worlds[ID].BuyPrice is null)
		{
			Worlds[ID].SellPrice = [0, 2, 5, 50, 130, 120];
		}

		Game.settings = settings;
		Game.Worlds = Worlds;

		//create the main gui
		Game.ID = ID;
		Game.frame = frame;
		MainGui = new MainPanel();
		resourceChange = GetMapResourceChange(Worlds[ID].Map);
		MainGui.bottom.UpdateInfo(Worlds[ID]);
		MainGui.right.UpdateInfo(0);

		Game.EditorMode = Worlds[ID].EditedMap;

		//reset any values
		selected[0] = 0;
		selected[1] = 0;

		//NEW - random new code to make this mess work somehow...
		MainGui.mapArea.mapPanel.MapButtonClicked += Clicked;

		//end loading
		UpdateStartTime();
		UpdateActivity((EditorMode ? "Editing" : "Playing") + " a World", "Day " + Worlds[ID].Time[0]);

		//setup timers
		mainTimer = new Timer(MainTimerTick, null, 0, 100);
	}

	private static void MainTimerTick(object state)
	{
		//play/edit mode:


		//play mode only:
		if (speed == 0 || EditorMode)
		{
			return;
		}
		//if the tick is reached.
		else if (tick >= 10 / speed)
		{
			//reset tick
			tick = 0;
			//not saved, 1 more hour.
			Saved = false;
			//add 1 hour to game time
			Worlds[ID].Time[1]++;
			//update the
			if (Worlds[ID].Time[1] >= 24)
			{
				Worlds[ID].Time[0] += 1;
				Worlds[ID].Time[1] = 0;
				if (settings.AutoSave)
				{
					SaveGame(MainGui.mapArea, ID);
				}

				//discord activity:
				UpdateActivity((Worlds[ID].EditedMap ? "Editing" : "Playing") + " a World",
					"Day " + Worlds[ID].Time[0]);
			}

			//add/remove player resources:
			for (var i = 0; i < Worlds[ID].Resources.Length; i++)
			{
				Worlds[ID].Resources[i] += resourceChange[i];
			}

			//any and all gui updates:
			MainGui.Invoke((MethodInvoker)delegate
			{
				MainGui.right.CheckUpgrades();
				MainGui.bottom.UpdateInfo(Worlds[ID]);
			});
		}
		//tick is not reached
		else
		{
			tick++;
		}
	}

	public static void Clicked(MapPanel MapPanel, int row, int column)
	{
		//if the click is the same tile, ignore.
		if (selected[1] == column && selected[0] == row)
		{
			return;
		}

		//remove old border
		var button = MapPanel.buttons[selected[0]][selected[1]];
		if (button != null)
		{
			button.FlatAppearance.BorderColor = Color.SteelBlue;
			if (!settings.Grid)
			{
				button.FlatAppearance.BorderSize = 0;
			}
		}

		//update the selected button.
		selected[0] = row;
		selected[1] = column;

		//add new border
		button = MapPanel.buttons[row][column];
		if (button != null)
		{
			button.FlatAppearance.BorderColor = Color.Red;
			if (!settings.Grid)
			{
				button.FlatAppearance.BorderSize = 1;
			}
		}

		//call a function to update the tile upgrade menu.
		MainGui.right.UpdateInfo(Worlds[ID].Map[row][column]);

		//if edit mode, do stuff.
		if (Worlds[ID].EditedMap && UpdateOnClick)
		{
			SetTile(UpgradeID);
		}
	}

	public static int[] GetMapResourceChange(int[][] Map)
	{
		//set the default
		var NetGain = new int[8];
		int[] TileChange;

		//loop through the entire map.
		for (var j = 0; j < Map[0].Length; j++)
		{
			for (var i = 0; i < Map.Length; i++)
			{
				//at a tile ID of the map:
				TileChange = GetTileResourceChange(j, i, Map);
				//add the arrays. should be moved to HelperStuff.cs to be honest.
				NetGain = AddIntArrays(NetGain, TileChange, false);
			}
		}

		//make any edits (global effects) to NetGain here:
		switch (Worlds[ID].Difficulty)
		{
			case 0:
				;
				NetGain[7] += 3;
				NetGain[6] += 3;
				break; // easy
			case 1:
				;
				NetGain[7] += 2;
				NetGain[6] += 1;
				break; // normal
			case 2:
				;
				NetGain[7] += 1;
				NetGain[6] -= 1;
				break; // hard
		}

		//done
		return NetGain;
	}

	public static int[] GetTileResourceChange(int column, int row, int[][] Map)
	{
		var TileNetGain = new int[8];
		TileNetGain = AddIntArrays(TileNetGain, tiles[Map[row][column]].ResourceChange, false);
		//for (int k = 0; k < tiles[Map[row][column]].ResourceChange.Length; k++) 
		//{
		//    TileNetGain[k] += tiles[Map[row][column]].ResourceChange[k];
		//}

		//check the nearby tiles for any near tile effects
		//get an array of the near tile IDs
		var NearIDs = new int[4];

		if (Map.Length > 1)
		{
			if (row == 0)
			{
				NearIDs[0] = Map[row + 1][column];
				NearIDs[1] = -1;
			}
			else if (row == Map.Length - 1)
			{
				NearIDs[0] = -1;
				NearIDs[1] = Map[row - 1][column];
			}
			else
			{
				NearIDs[0] = Map[row + 1][column];
				NearIDs[1] = Map[row - 1][column];
			}
		}

		if (Map[0].Length > 1)
		{
			if (column == 0)
			{
				NearIDs[2] = Map[row][column + 1];
				NearIDs[3] = -1;
			}
			else if (column == Map[0].Length - 1)
			{
				NearIDs[2] = -1;
				NearIDs[3] = Map[row][column - 1];
			}
			else
			{
				NearIDs[2] = Map[row][column + 1];
				NearIDs[3] = Map[row][column - 1];
			}
		}

		//loop through
		foreach (var ID in NearIDs)
		{
			if (ID != -1 && tiles[Map[row][column]].NearTileEffects.ContainsKey(ID))
			{
				// add the effects to the net gain.
				TileNetGain = AddIntArrays(TileNetGain, tiles[Map[row][column]].NearTileEffects[ID], false);
				//for (int k = 0; k < Effects.Length; k++) 
				//{
				//    TileNetGain[k] += Effects[k];
				//}
			}
		}

		return TileNetGain;
	}

	public static void SaveGame(MyTableLayoutPanel MapPanel, int ID)
	{
		// save the world info
		SaveToJson("SavedWorlds", Worlds);

		try
		{
			// save the image
			var image = new Bitmap(MapPanel.Width, MapPanel.Height);
			MapPanel.DrawToBitmap(image, new Rectangle(new Point(0, 0), MapPanel.Size));
			image.Save(Directory.GetCurrentDirectory() + @"\Data\ImageData\WorldScreenshots\World" + ID + ".png",
				ImageFormat.Png);
		}
		catch (Exception)
		{
		}

		Saved = true;
	}

	public static int[][] GenerateMap(int Width, int Height)
	{
		//create the empty map
		int[][] map = new int[Height][];
		for (var i = 0; i < map.Length; i++)
		{
			map[i] = new int[Width];
		}

		//generate world here:
		//wip

		return map;
	}

	public static bool ResourceCheck(int[] other)
	{
		//if (other.Length != Worlds[ID].Resources.Length)
		//{
		//    return false;
		//}

		for (var i = 0; i < other.Length; i++)
		{
			if (Worlds[ID].Resources[i] - other[i] < 0)
			{
				return false;
			}
		}

		return true;
	}

	public static void UpgradeTile(int selection)
	{
		//
		var row = selected[0];
		var column = selected[1];

		// get tile data and find the into 1, 2, or 3 cost and tile.
		int[] cost;
		var OldID = Worlds[ID].Map[row][column];
		int newID;
		switch (selection)
		{
			case 1:
				cost = tiles[OldID].Cost1;
				newID = tiles[OldID].Upgrades[0];
				break;

			case 2:
				cost = tiles[OldID].Cost2;
				newID = tiles[OldID].Upgrades[1];
				break;

			case 3:
				cost = tiles[OldID].Cost3;
				newID = tiles[OldID].Upgrades[2];
				break;

			default:
				return;
		}

		if (newID == -1)
		{
			return;
		}

		// check resources
		if (ResourceCheck(cost) || Worlds[ID].Sandbox || Worlds[ID].EditedMap)
		{
			Saved = false;
			// remove resources
			if (!Worlds[ID].EditedMap)
			{
				Worlds[ID].Resources =
					AddLongArrays(Worlds[ID].Resources, cost.Select(i => (long)i).ToArray(), true);
			}

			//update tile and resource change:
			var oldTileResource = GetTileResourceChange(column, row, Worlds[ID].Map);
			Worlds[ID].Map[row][column] = newID;
			var newTileResource = GetTileResourceChange(column, row, Worlds[ID].Map);
			resourceChange = AddIntArrays(resourceChange, AddIntArrays(newTileResource, oldTileResource, true),
				false);
			//for (int i = 0; i < oldTileResource.Length; i++)
			//{
			//    resourceChange[i] -= oldTileResource[i];
			//}
			//for (int i = 0; i < newTileResource.Length; i++)
			//{
			//    resourceChange[i] += newTileResource[i];
			//}

			//update the GUIs and save

			//temp:
			(MainGui.mapArea.mapPanel.GetControlFromPosition(column, row) as Button).BackgroundImage =
				tileIcons[newID];

			//
			if (settings.AutoSave)
			{
				SaveGame((MainGui.mapArea as MyTableLayoutPanel), ID);
			}

			MainGui.right.UpdateInfo(newID);
			MainGui.bottom.UpdateInfo(Worlds[ID]);
		}
	}

	public static void SetTile(int id)
	{
		//
		var row = selected[0];
		var column = selected[1];

		if (Worlds[ID].Map[row][column] != id)
		{
			//update tile and resource change:
			var oldTileResource = GetTileResourceChange(column, row, Worlds[ID].Map);
			Worlds[ID].Map[row][column] = id;
			var newTileResource = GetTileResourceChange(column, row, Worlds[ID].Map);
			for (var i = 0; i < oldTileResource.Length; i++)
			{
				resourceChange[i] -= oldTileResource[i];
			}

			for (var i = 0; i < newTileResource.Length; i++)
			{
				resourceChange[i] += newTileResource[i];
			}

			//update the GUIs and save
			//temp:
			(MainGui.mapArea.mapPanel.GetControlFromPosition(column, row) as Button).BackgroundImage =
				tileIcons[id];
			//
			Saved = false;
			if (settings.AutoSave)
			{
				SaveGame((MainGui.mapArea as MyTableLayoutPanel), id);
			}

			MainGui.right.UpdateInfo(id);
			MainGui.bottom.UpdateInfo(Worlds[ID]);
		}
	}
}