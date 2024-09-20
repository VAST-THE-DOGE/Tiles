using Timer = System.Threading.Timer;

namespace Tiles;

using static RichPresenceHelper;
using static HelperStuff;

public class Game
{
	//edit mode only:
	public static bool EditorMode;
	public static bool UpdateOnClick;
	public static int UpgradeID;
	private readonly MainGamePanel MainGui;
	private Timer mainTimer;

	public int popDebuff = 0; //wip

	private int[] BaseResourceChange = [0, 0, 0, 0, 0, 0, 0, 0];
	private bool Saved;
	private int[] selected = [0, 0];

	// game speed
	private int speed;
	private int tick;
	private World World;

	public Game(World world)
	{
		World = world;

		//verify that there are the new int values in the world
		if (World.TileStatus is null)
		{
			World.TileStatus = new int[World.Map.Length][];
			for (var i = 0; i < World.Map.Length; i++)
			{
				World.TileStatus[i] = new int[World.Map[i].Length];
			}
		}

		if (World.TileTimers is null)
		{
			World.TileTimers = new int[World.Map.Length][];
			for (var i = 0; i < World.Map.Length; i++)
			{
				World.TileTimers[i] = new int[World.Map[i].Length];
			}
		}

		World.SellPrice ??= [0, 20, 50, 500, 1300, 1200];
		World.BuyPrice ??= [0, 2, 5, 50, 130, 120];

		World = world;
		BaseResourceChange = GetMapResourceChange(World.Map);

		//create the main gui
		MainGui = new MainGamePanel();
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

		MainGui.Initialize(ref RefreshResources, ref RefreshTime, ref RefreshSaved, ref SetTileId, ref RefreshMap,
			ref SetTileState, ref setWeather, ref setSpeed, ref UpdateSelectedTile, ref FreezeTime, World);

		GlobalVariableManager.frame.Controls.Clear();
		GlobalVariableManager.frame.Controls.Add(MainGui);

		//EditorMode = World.EditedMap;

		RefreshMap.Invoke(World.Map, World.TileStatus);
		RefreshResources.Invoke(World.Resources, BaseResourceChange);

		RefreshTime.Invoke(World.Time);
		setSpeed.Invoke(speed);
		setWeather.Invoke(World.Weather);

		//end loading
		UpdateStartTime();
		UpdateActivity((EditorMode ? "Editing" : "Playing") + " a World", "Day " + World.Time[0]);

		//setup timers
		mainTimer = new Timer(MainTimerTick, null, 0, 100);
	}

	public static Bitmap[] menuIcons => BasicGuiManager.MenuIcons;

	/////////////////
	private static Tile[] tiles => GlobalVariableManager.tileInfo;

	private static Settings settings => GlobalVariableManager.settings;

	//new:
	private event Action<long[], int[]> RefreshResources;
	private event Action<int[]> RefreshTime;
	private event Action<bool> RefreshSaved;
	private event Action<int, int, int> SetTileId;
	private event Action<int, int, int> SetTileState;
	private event Action<int> setWeather;
	private event Action<int[][], int[][]> RefreshMap;
	private event Action<int> setSpeed;
	private event Action<bool> FreezeTime;
	private event Action<int, int?, int?> UpdateSelectedTile;

	private void MainTimerTick(object state)
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
			World.Time[1]++;
			//update the
			if (World.Time[1] >= 24)
			{
				World.Time[0] += 1;
				World.Time[1] = 0;
				if (settings.AutoSave)
				{
					// SaveGame(MainGui.mapArea, ID);
				}

				MainGui.Invoke(() => { RefreshSaved.Invoke(false); });

				//discord activity:
				UpdateActivity((World.EditedMap ? "Editing" : "Playing") + " a World",
					"Day " + World.Time[0]);
			}

			//add/remove player resources:
			//TODO: update
			for (var i = 0; i < World.Resources.Length; i++)
			{
				World.Resources[i] += BaseResourceChange[i];
			}

			//any and all gui updates:
			MainGui?.Invoke(() =>
			{
				RefreshResources?.Invoke(World.Resources, BaseResourceChange);
				RefreshTime?.Invoke(World.Time);
			});

			#region WeatherRandomizer

			World.Weather = (int)(((MapPanel.Weather)World.Weather) switch
			{
				MapPanel.Weather.Clear => (Random.Shared.Next(0, 128)) switch
				{
					0 or 1 or 2 => MapPanel.Weather.Sprinkle,
					_ => MapPanel.Weather.Clear
				},
				MapPanel.Weather.Sprinkle => (Random.Shared.Next(0, 48)) switch
				{
					0 or 1 or 2 => MapPanel.Weather.Clear,
					3 or 4 => MapPanel.Weather.Rainy,
					_ => MapPanel.Weather.Sprinkle
				},
				MapPanel.Weather.Rainy => (Random.Shared.Next(0, 48)) switch
				{
					0 or 1 or 2 => MapPanel.Weather.Sprinkle,
					3 => MapPanel.Weather.Stormy,
					_ => MapPanel.Weather.Rainy
				},
				MapPanel.Weather.Stormy => (Random.Shared.Next(0, 48)) switch
				{
					0 or 1 => MapPanel.Weather.Rainy,
					_ => MapPanel.Weather.Stormy
				}
			});
			setWeather?.Invoke(World.Weather);

			#endregion

			//Manage tile status:
			for (var y = 0; y < World.TileTimers.Length; y++)
			{
				for (var x = 0; x < World.TileTimers[0].Length; x++)
				{
					if (World.TileTimers[y][x] == 0) continue;

					World.TileTimers[y][x]--;

					if (World.TileTimers[y][x] != 0) continue;

					World.TileStatus[y][x] = (World.TileStatus[y][x]) switch
					{
						_ => 0
					};
					SetTileState.Invoke(x, y, World.TileStatus[y][x]);
				}
			}
		}
		//tick is not reached
		else
		{
			tick++;
		}
	}

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

	private async void SaveGame()
	{
		// save the world info
		World.Name = await WorldManager.SaveWorld(World);
		await WorldManager.SaveWorldImage(World.Name, await MainGui.ScreenshotMap());

		RefreshSaved.Invoke(true);

		// try
		// {
		// 	// save the image
		// 	var image = new Bitmap(MapPanel.Width, MapPanel.Height);
		// 	MapPanel.DrawToBitmap(image, new Rectangle(new Point(0, 0), MapPanel.Size));
		// 	image.Save(Directory.GetCurrentDirectory() + @"\Data\ImageData\WorldScreenshots\World" + ID + ".png",
		// 		ImageFormat.Png);
		// }
		// catch (Exception)
		// {
		// }

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
		int[] cost;
		var OldID = World.Map[row][column];
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
		if (ResourceCheck(cost) || World.Sandbox || World.EditedMap)
		{
			Saved = false;
			// remove resources
			if (!World.EditedMap)
			{
				World.Resources =
					AddLongArrays(World.Resources, cost.Select(i => (long)i).ToArray(), true);
			}

			//update tile and resource change:
			var oldTileResource = GetTileResourceChange(column, row, World.Map);
			World.Map[row][column] = newID;
			World.TileStatus[row][column] = 1;
			World.TileTimers[row][column] = GlobalVariableManager.tileInfo[newID].BuildHours;
			var newTileResource = GetTileResourceChange(column, row, World.Map);
			BaseResourceChange = AddIntArrays(BaseResourceChange, AddIntArrays(newTileResource, oldTileResource, true),
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
			SetTileId.Invoke(column, row, newID);
			SetTileState.Invoke(column, row, 1);
			RefreshResources.Invoke(World.Resources, BaseResourceChange);
			UpdateSelectedTile.Invoke(
				World.Map[row][column],
				World.TileStatus[row][column],
				World.TileTimers[row][column]);

			RefreshSaved.Invoke(false);
			RefreshResources.Invoke(World.Resources, BaseResourceChange);

			//
			if (settings.AutoSave)
			{
				SaveGame();
			}

			// MainGui.right.UpdateInfo(newID);
			// MainGui.bottom.UpdateInfo(World);
		}
	}

	public void SetTile(int id)
	{
		var row = selected[0];
		var column = selected[1];

		if (World.Map[row][column] != id)
		{
			//update tile and resource change:
			//TODO: check nearby tiles as well when upgrading (new nearby tile effects)
			var oldTileResource = GetTileResourceChange(column, row, World.Map);
			World.Map[row][column] = id;
			var newTileResource = GetTileResourceChange(column, row, World.Map);
			for (var i = 0; i < oldTileResource.Length; i++)
			{
				BaseResourceChange[i] -= oldTileResource[i];
			}

			for (var i = 0; i < newTileResource.Length; i++)
			{
				BaseResourceChange[i] += newTileResource[i];
			}

			//update the GUIs and save
			SetTileId.Invoke(column, row, id);

			// TODO use world manager
			Saved = false;
			if (settings.AutoSave)
			{
				SaveGame();
			}
		}
	}
}