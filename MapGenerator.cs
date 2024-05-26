class MapGenerator {
    public static int[][] Generate(int width, int height)
    {
        int[][] map = new int[height][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new int[width];
        }
        return map;
    }
}