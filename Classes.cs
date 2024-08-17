//classes that are used to load from Json files.
namespace Tiles
{
	public class Tile
	{
		public string Name { get; set; }
		public int[] ResourceChange { get; set; }
		public int[] Upgrades { get; set; }
		public int[] Cost1 { get; set; }
		public int[] Cost2 { get; set; }
		public int[] Cost3 { get; set; }
		public Dictionary<int, int[]> NearTileEffects { get; set; }

		//unused stuff. Will be used later!

		//used for animating tiles. Currently it should be set to 1 always.
		public int Frames { get; set; }
		//used to play a sound when clicked
		public int SoundID { get; set; }
		//used to check what research items are needed for the tile.
		public int[] ResearchIDs { get; set; }

		//new - wip
		public int BuildHours { get; set; }
	}

	public class Settings
	{
		public int MusicVolume { get; set; }
		public int SFXVolume { get; set; }
		public bool Grid { get; set; }
		public bool AutoSave { get; set; }
		public bool ExtraEffects { get; set; }
		public string SkinPack { get; set; }
		public string MusicPack { get; set; }
	}
}