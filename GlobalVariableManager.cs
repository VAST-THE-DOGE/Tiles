
namespace Tiles;

internal record GlobalVariableManager
{
    internal static readonly string VERSION = "0.4.0";
    internal static string GameStateMain = "Loading";
    internal static string GameStateSecondary = "Loading";

    internal static Settings settings;

    //TODO: move this to json:
    internal static readonly string[] ResourceNames = { "Gold", "Iron", "Stone", "Wood", "Water", "Food", "Workers", "Research" };
    internal static readonly Color[] ResourceColors = { Color.Yellow, Color.Red, Color.Gray, Color.Green, Color.Blue, Color.Orange, Color.Chocolate, Color.Magenta };

	internal static World MenuIcons { get; set; }
	internal static Form frame;
	internal static Bitmap[] tileIcons;
	internal static Bitmap[] menuIcons;

    //1 is worst. 8 is in the middle. 20 or so is the best. Default: 16
    //aka, default image size. (images are resized on load to prevent blurriness)
    //more ram used, less storage used.
    internal static readonly int ImageQuality = 6; // DO NOT CHANGE!!!

	internal static readonly int LoaderFontSize = 36;
	internal static Tile[] tiles;
}