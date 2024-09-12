using Timer = System.Threading.Timer;

namespace Tiles;

using static RichPresenceHelper;
using static HelperStuff;

public class Editor //TODO: make an IWorldViewer that Game and Editor can inherit from to have shared functions in one file.
{
	//edit mode only:
	private readonly MainEditPanel MainGui;
	private Timer mainTimer;

	public int popDebuff = 0; //wip

	private int[] resourceChange = [0, 0, 0, 0, 0, 0, 0, 0];
	private bool Saved;
	private int[] selected = [0, 0];

	// game speed
	private int speed;
	private int tick;
	private World World;

	public Editor(World world)
	{
		World = world;

		//create the main gui
		MainGui = new MainEditPanel();
		MainGui.Dock = DockStyle.Fill;

		MainGui.MenuRequest += () =>
		{
			mainTimer.Dispose();
			MainGui.Dispose();
			GlobalVariableManager.frame.Controls.Clear();
			GlobalVariableManager.frame.Controls.Add(GlobalVariableManager.Loader);
		};
		MainGui.SaveRequest += SaveGame;
		MainGui.TileClicked += Clicked;
		MainGui.ToggleTileRequest += () => { };
		MainGui.GameSpeedUpdate += (s) =>
		{
			speed = s;
			setSpeed.Invoke(s);
		};
		MainGui.RequestFreezeUpdate += (yn) => { };
		MainGui.UpgradeTileRequest += UpgradeTile;
		MainGui.SetSelectedToIdRequest += SetTile;

		MainGui.Initialize(ref RefreshResources, ref RefreshTime, ref RefreshSaved, ref SetTileId, ref RefreshMap,
			ref SetTileState, ref setWeather, ref setSpeed, ref UpdateSelectedTile, ref FreezeTime, World);

		GlobalVariableManager.frame.Controls.Clear();
		GlobalVariableManager.frame.Controls.Add(MainGui);

		//EditorMode = World.EditedMap;

		RefreshMap.Invoke(World.Map, World.TileStatus);
		RefreshResources.Invoke(World.Resources, resourceChange);

		RefreshTime.Invoke(World.Time);
		setSpeed.Invoke(0);

		//end loading
		UpdateStartTime();
		UpdateActivity("Editing a World", $"World Name: {World.Name}");
	}

	public static Bitmap[] menuIcons => BasicGuiManager.MenuIcons;

	/////////////////
	private static Tile[] tiles => GlobalVariableManager.tileInfo;

	private static Settings settings => GlobalVariableManager.settings;

	//new:
	private event Action<long[], int[]> RefreshResources;
	private event Action<int[]> RefreshTime;
	private event Action<bool, bool> RefreshSaved;
	private event Action<int, int, int> SetTileId;
	private event Action<int, int, int> SetTileState;
	private event Action<int> setWeather;
	private event Action<int[][], int[][]> RefreshMap;
	private event Action<int> setSpeed;
	private event Action<bool> FreezeTime;
	private event Action<int, int?, int?> UpdateSelectedTile;

	private void Clicked(int row, int column)
	{
		//if the click is the same tile, ignore.
		if (selected[1] == column && selected[0] == row)
		{
			return;
		}

		//update the selected button.
		selected[0] = row;
		selected[1] = column;

		//call a function to update the tile upgrade menu.
		UpdateSelectedTile.Invoke(
			World.Map[selected[0]][selected[1]],
			World.TileStatus[selected[0]][selected[1]],
			World.TileTimers[selected[0]][selected[1]]);
	}

	private int[] GetMapResourceChange(int[][] Map)
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
		switch (World.Difficulty)
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

	private static int[] GetTileResourceChange(int column, int row, int[][] Map)
	{
		var TileNetGain = new int[8];
		TileNetGain = AddIntArrays(TileNetGain, tiles[Map[row][column]].ResourceChange, false);

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

		return NearIDs.Where(ID => ID != -1 && tiles[Map[row][column]].NearTileEffects.ContainsKey(ID)).Aggregate(
			TileNetGain,
			(current, ID) => AddIntArrays(current, tiles[Map[row][column]].NearTileEffects[ID], false));
	}

	private async void SaveGame(bool playing)
	{
		// save the world info
		World.EditedMap = !playing;
		
		World.Name = await WorldManager.SaveWorld(World);
		await WorldManager.SaveWorldImage(World.Name, MainGui.ScreenshotMap());
		
		RefreshSaved.Invoke(true, !World.EditedMap);

		Saved = true;
	}

	private bool ResourceCheck(int[] other)
	{
		return !other.Where((t, i) => World.Resources[i] - t < 0).Any();
	}

	private void UpgradeTile(int selection)
	{
		//
		var row = selected[0];
		var column = selected[1];

		// get tile data and find the into 1, 2, or 3 cost and tile.
		var OldID = World.Map[row][column];
		int newID;
		switch (selection)
		{
			case 1:
				newID = tiles[OldID].Upgrades[0];
				break;

			case 2:
				newID = tiles[OldID].Upgrades[1];
				break;

			case 3:
				newID = tiles[OldID].Upgrades[2];
				break;

			default:
				return;
		}

		if (newID == -1)
		{
			return;
		}
		
		Saved = false;
		
		//update tile:
		World.Map[row][column] = newID;
		
		//update the GUIs and save
		SetTileId.Invoke(column, row, newID);
		RefreshResources.Invoke(World.Resources, resourceChange);
		UpdateSelectedTile.Invoke(
			World.Map[row][column],
			World.TileStatus[row][column],
			World.TileTimers[row][column]);
		RefreshSaved.Invoke(false, World.EditedMap);
		RefreshResources.Invoke(World.Resources, resourceChange);
		
		if (settings.AutoSave)
		{
			SaveGame(World.EditedMap);
		}
	}

	private void SetTile(int row, int column, int id)
	{
		selected[0] = row;
		selected[1] = column;

		if (World.Map[row][column] == id) return;
		
		//update tile and resource change:
		World.Map[row][column] = id;

		//update the GUIs and save
		SetTileId.Invoke(column, row, id);
			
		Saved = false;
		if (settings.AutoSave)
		{
			SaveGame(World.EditedMap);
		}
	}
}