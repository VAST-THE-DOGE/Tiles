using System.Drawing.Imaging;
using System.Text.Json;

namespace Tiles;

/// <summary>The class for managing Worlds. Includes many functions for World loading, saving, copying, etc.</summary>
internal static class WorldManager
{
	//TODO: create all these

	public static async Task<string> SaveWorld(World world, bool secondTry = false)
	{
		try
		{
			var directory = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SavedWorlds");

			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			var name = world.Name;

			if (name is not { Length: > 0 }) //TEMP (for merging old worlds)
			{
				name = "World - 1";

				if (File.Exists(Path.Combine(directory, $@"{name}.json")))
				{
					var increment = 1;
					while (File.Exists($@"{directory}\{name} - {increment}.json"))
					{
						increment++;
					}

					name = $"{name} - {increment}";
				}
			}

			await File.WriteAllTextAsync(Path.Combine(directory, $@"{name}.json"), JsonSerializer.Serialize(world));

			return name;
		}
		catch (Exception e)
		{
			if (secondTry)
			{
				throw;
			}
			else
			{
				return await SaveWorld(world, true);
			}
		}
	}

	public static async Task SaveWorldImage(World world, Bitmap image)
	{
		await SaveWorldImage(world.Name, image);
	}

	public static async Task SaveWorldImage(WorldHeader world, Bitmap image)
	{
		await SaveWorldImage(world.Name, image);
	}

	public static async Task SaveWorldImage(string worldName, Bitmap image)
	{
		try
		{
			image.Save(Directory.GetCurrentDirectory() + @$"\Data\ImageData\WorldScreenshots\{worldName}.png", ImageFormat.Png);
		}
		catch (Exception e)
		{
			
		}
	}

	public static async Task<World> CreateWorld(string name, int diff, (int,int) size, bool editing)
	{
		throw new NotImplementedException();
	}

	public static async Task<Bitmap> LoadWorldImage(World world)
	{
		return await LoadWorldImage(world.Name);
	}

	public static async Task<Bitmap> LoadWorldImage(WorldHeader world)
	{
		return await LoadWorldImage(world.Name);
	}

	public static async Task<Bitmap> LoadWorldImage(string worldName)
	{
		try
		{
			using (var image = new Bitmap(Directory.GetCurrentDirectory() + @$"\Data\ImageData\WorldScreenshots\{worldName}.png"))
			{
				return new Bitmap(image);
			}
		}
		catch
		{
			return BasicGuiManager.NO_IMAGE_ICON;
		}
	}

	public static async Task<World> LoadWorld(World world)
	{
		return await LoadWorld(world.Name);
	}

	public static async Task<World> LoadWorld(WorldHeader world)
	{
		return await LoadWorld(world.Name);
	}

	public static async Task<World> LoadWorld(string worldName, bool secondTry = false)
	{
		try
		{
			var directory = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SavedWorlds");
			var fileName = Path.Combine(directory, $"{worldName}.json");


			var json = await File.ReadAllTextAsync(fileName);
			var world = JsonSerializer.Deserialize<World>(json)
			            ?? throw new Exception(
				            $"World with name: {fileName} could be corrupted. Please move this world out of the SavedWorlds folder.");

			world.Name = world.Name is null || world.Name.Length == 0 ? worldName : world.Name;
			
			return world;
		}
		catch (Exception e)
		{
			if (secondTry)
			{
				throw;
			}
			else
			{
				return await LoadWorld(worldName, true);
			}
		}
	}

	public static async Task<IEnumerable<World>> LoadAllWorlds(bool secondTry = false)
	{
		try
		{
			var directory = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SavedWorlds");
			var fileNames = Directory.EnumerateFiles(directory);

			HashSet<World> worlds = [];

			foreach (var fileName in fileNames)
			{
				var json = await File.ReadAllTextAsync(fileName);

				var world = JsonSerializer.Deserialize<World>(json) ?? throw new Exception(
					$"World with name: {fileName} could may be corrupted. Please move this world out of the SavedWorlds folder.");

				world.Name ??= string.Join("", new FileInfo(fileName).Name.Split(".").Where(s => s != "json"));

				worlds.Add(world);
			}

			return worlds;
		}
		catch (Exception e)
		{
			if (secondTry)
			{
				throw;
			}
			else
			{
				return await LoadAllWorlds(true);
			}
		}
	}

	public static async Task<IEnumerable<WorldHeader>> GetWorldHeaders(bool secondTry = false)
	{
		try
		{
			var directory = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SavedWorlds");
			var fileNames = Directory.EnumerateFiles(directory);

			var worlds = await LoadAllWorlds();

			return worlds.Select(w => new WorldHeader()
			{
				Name = w.Name,
				Difficulty = w.Difficulty,
				Time = w.Time,
				Sandbox = w.Sandbox,
				EditedMap = w.EditedMap
			});
		}
		catch (Exception e)
		{
			if (secondTry)
			{
				throw;
			}
			else
			{
				return await GetWorldHeaders(true);
			}
		}
	}

	public static async Task DeleteWorld(World world)
	{
		await DeleteWorld(world.Name);
	}

	public static async Task DeleteWorld(WorldHeader world)
	{
		await DeleteWorld(world.Name);
	}

	public static async Task DeleteWorld(string worldName)
	{
		throw new NotImplementedException();
	}

	public static async Task CopyWorld(World world, string? newName)
	{
		await SaveWorld(new World
		{
			Time = world.Time,
			Name = newName,
			Resources = world.Resources,
			Research = world.Research,
			Difficulty = world.Difficulty,
			Weather = world.Weather,
			Leader = world.Leader,
			Sandbox = world.Sandbox,
			Map = world.Map,
			TileStatus = world.TileStatus,
			TileTimers = world.TileTimers,
			EditedMap = world.EditedMap
		});
	}

	public static async Task RenameWorld(World world, string newName)
	{
		throw new NotImplementedException();
	}

	private static async Task VerifyFileIntegrity() //TODO: is this needed? If so, make it.
	{
		throw new NotImplementedException();
	}
}

/// <summary>A simple representation of a World</summary>
public record WorldHeader
{
	public string Name { get; set; }
	public int[] Time { get; set; }
	public int Difficulty { get; set; }
	public bool Sandbox { get; set; }
	public bool EditedMap { get; set; }
}

/// <summary>This is a World, aka the entire map + all gameplay data.</summary>
public record World
{
	//new
	public string? Name { get; set; }

	public int[] Time { get; set; }
	public long[] Resources { get; set; }
	public int[] Research { get; set; }
	public int Difficulty { get; set; }
	public int Weather { get; set; }
	public int WeatherTimer { get; set; }
	public int Leader { get; set; }
	public bool Sandbox { get; set; }
	public bool EditedMap { get; set; }
	public int[][] Map { get; set; }
	public int[][] TileStatus { get; set; }
	public int[][] TileTimers { get; set; }
	public int[] SellPrice { get; set; }
	public int[] BuyPrice { get; set; }

	//TODO: move this to world manager
	public World CopyWorld()
	{
		return new World
		{
			Time = this.Time,
			Resources = this.Resources,
			Research = this.Research,
			Difficulty = this.Difficulty,
			Weather = this.Weather,
			Leader = this.Leader,
			Sandbox = this.Sandbox,
			Map = this.Map,
			TileStatus = this.TileStatus,
			TileTimers = this.TileTimers,
			EditedMap = this.EditedMap
		};
	}
}