namespace Tiles;

/// <summary>The class for managing Worlds. Includes many functions for World loading, saving, copying, etc.</summary>
internal class WorldManager
{
	//TODO: create all these

	public static async Task SaveWorld(World world)
	{
		/*try
		{
		    File.WriteAllText(Directory.GetCurrentDirectory() + @"\Data\" + Name + ".json", JsonSerializer.Serialize(Info));
		}
		catch
		{

		}*/
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
		throw new NotImplementedException();
	}

	public static async Task<World> CreateWorld( /*TODO: need params*/)
	{
		throw new NotImplementedException();
	}

	public static async Task LoadWorldImage(World world)
	{
		throw new NotImplementedException();
	}

	public static async Task LoadWorldImage(WorldHeader world)
	{
		throw new NotImplementedException();
	}

	public static async Task LoadWorldImage(string worldName)
	{
		throw new NotImplementedException();
	}

	public static async Task LoadWorld(World world)
	{
		await LoadWorld(world.Name);
	}

	public static async Task LoadWorld(WorldHeader world)
	{
		await LoadWorld(world.Name);
	}

	public static async Task LoadWorld(string worldName)
	{
		throw new NotImplementedException();
	}

	public static async Task LoadAllWorlds()
	{
		throw new NotImplementedException();
	}

	public static async Task GetWorldHeaders()
	{
		throw new NotImplementedException();
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
		throw new NotImplementedException();
	}

	public static async Task RenameWorld(World world, string newName)
	{
		throw new NotImplementedException();
	}
}

/// <summary>A simple representation of a World</summary>
internal record WorldHeader
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
	public string Name { get; set; }

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