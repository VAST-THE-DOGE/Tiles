    //classes that are used to load from Json files.
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
    }
    public class World
    {
        public int[] Time { get; set; }
        public long[] Resources { get; set; }
        public int[] Research { get; set; }
        public int Difficulty { get; set; }
        public int Weather { get; set;}
        public int Leader { get; set; }
        public bool Sandbox { get; set;}
        public bool EditedMap { get; set;}
        public int[][] Map { get; set; }
        public World CopyWorld()
        {
            World New = new World();
            New.Time = this.Time;
            New.Resources = this.Resources;
            New.Research = this.Research ;
            New.Difficulty = this.Difficulty;
            New.Weather = this.Weather;
            New.Leader = this.Leader;
            New.Sandbox = this.Sandbox;
            New.Map = this.Map;
            New.EditedMap = this.EditedMap;
            return New;
        }
    }

    public class Settings
    {
    public int AudioVolume { get; set;}
    public bool Grid { get; set;}
    public bool AutoSave { get; set;}
    public string SkinPack { get; set;}
    public string MusicPack { get; set;}

    }