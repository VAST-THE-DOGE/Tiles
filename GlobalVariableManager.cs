namespace Tiles;

public record GlobalVariableManager
{
	public static readonly string VERSION = "0.4.0";
	public static string GameStateMain = "Loading";
	public static string GameStateSecondary = "Loading";

	public static Settings settings;

	//TODO: move this to json:
	public static readonly string[] ResourceNames =
		["Gold", "Iron", "Stone", "Wood", "Water", "Food", "Workers", "Research"];

	public static readonly Color[] ResourceColors =
		[Color.Yellow, Color.Red, Color.Gray, Color.Green, Color.Blue, Color.Orange, Color.Chocolate, Color.Magenta];

	public static Form frame;

	//1 is worst. 8 is in the middle. 20 or so is the best. Default: 16
	//aka, default image size. (images are resized on load to prevent blurriness)
	//more ram used, less storage used.
	public static readonly int TileImageQuality = 16; // DO NOT CHANGE!!!
	public static readonly int MenuImageQuality = 4; // DO NOT CHANGE!!!
	
	public static Tile[] tileInfo;

	public static Game Game;
	public static Control Loader;
}