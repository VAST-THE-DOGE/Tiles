//not used I think:

namespace Tiles;

internal class LegacyWorldGroup
{
	public static World[] TransferWorld(Object[] legacyWorld)
	{
		switch (legacyWorld)
		{
			case null: throw new NullReferenceException();
			case World[]: return legacyWorld as World[];
			default:
				throw new Exception(
					"Could not find legacy world type! Most likely a corrupt or invalid save file.");
		}
	}

	public class ver_0_3_1
	{
		public int[] Time { get; set; }
		public long[] Resources { get; set; }
		public int[] Research { get; set; }
		public int Difficulty { get; set; }
		public int Weather { get; set; }
		public int Leader { get; set; }
		public bool Sandbox { get; set; }
		public bool EditedMap { get; set; }
		public int[][] Map { get; set; }

		public World CopyWorld()
		{
			var New = new World();
			New.Time = this.Time;
			New.Resources = this.Resources;
			New.Research = this.Research;
			New.Difficulty = this.Difficulty;
			New.Weather = this.Weather;
			New.Leader = this.Leader;
			New.Sandbox = this.Sandbox;
			New.Map = this.Map;
			New.EditedMap = this.EditedMap;
			return New;
		}
	}
}