class MapGenerator {
    private static Random rnd;
    public static int[][] Generate(int width, int height)
    {
        int[][] map = new int[height][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new int[width];
        }


        return GenerateHeightMap(map);
    }
    private static int[][] GenerateHeightMap(int[][] map)
    {
        rnd = new Random();
        int mapSize = map.Length * map[0].Length;

        //create starting points:
        int[] xPoints = new int[(int) Math.Ceiling((double) mapSize/100)];
        int[] yPoints = new int[xPoints.Length];
        int[] PointHeight = new int[xPoints.Length];
        for (int i = 0; i < xPoints.Length; i++)
        {
            xPoints[i] = rnd.Next(0, map[0].Length);
            yPoints[i] = rnd.Next(0, map.Length);
            PointHeight[i] = rnd.Next(1, 1 + (int) Math.Ceiling(Math.Sqrt(mapSize/150)));
        }

        //set map height at points:
        for (int i = 0; i < xPoints.Length; i++)
        {
            map[yPoints[i]][xPoints[i]] = PointHeight[i];
        }

        //call the height spreader functions:
        int[] location = new int[2];
        for (int i = 0; i < xPoints.Length; i++)
        {
            location[0] = yPoints[i];
            location[1] = xPoints[i];
            map = SpreadHeight(map, location, false);
        }


        return map;
    }
      //////////////////////////
     // height map generator //
    //////////////////////////
    private static int[][] SpreadHeight(int[][] map, int[] location, bool sameHeight)
    {
        //recursive calls:
        int[] newLocation = new int[2];
        int decrease;
        if (location[0] + 1 < map.Length)
        {
            newLocation[0] = location[0] + 1;
            newLocation[1] = location[1];
            map = SHHelper(map, location, newLocation, sameHeight);
        }
        if (location[1] + 1 < map[0].Length)
        {
            newLocation[0] = location[0];
            newLocation[1] = location[1] + 1;
            map = SHHelper(map, location, newLocation, sameHeight);
        }
        if (location[0] - 1 >= 0)
        {
            newLocation[0] = location[0] - 1;
            newLocation[1] = location[1];
            map = SHHelper(map, location, newLocation, sameHeight);
        }
        if (location[1] - 1 >= 0)
        {
            newLocation[0] = location[0];
            newLocation[1] = location[1] - 1;
            map = SHHelper(map, location, newLocation, sameHeight);
        }
        return map;
    }
    private static int[][] SHHelper(int[][] map, int[] location, int[] newLocation, bool sameHeight)
    {
        int decrease;
        if (map[newLocation[0]][newLocation[1]] < map[location[0]][location[1]]) 
        {
            if (sameHeight) {
                map[newLocation[0]][newLocation[1]] = 
                    map[location[0]][location[1]] - 1;
                    return SpreadHeight(map, newLocation, false);
            } else {
                decrease = rnd.Next(0,2);
                map[newLocation[0]][newLocation[1]] = 
                    map[location[0]][location[1]];
                    return SpreadHeight(map, newLocation, decrease == 0);
            }
        } else {
            return map;
        }
    }
      ////////////////////////
     // tile map generator //
    ////////////////////////
    
}
