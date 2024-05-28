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
        int[] xPoints = new int[(int) Math.Ceiling(Math.Sqrt(mapSize/50))];
        int[] yPoints = new int[xPoints.Length];
        int[] PointHeight = new int[xPoints.Length];
        for (int i = 0; i < xPoints.Length; i++)
        {
            xPoints[i] = rnd.Next(0, map[0].Length);
            yPoints[i] = rnd.Next(0, map.Length);
            PointHeight[i] = rnd.Next(1, Math.Clamp(1 + (int) Math.Ceiling(Math.Sqrt(mapSize/150)), 1, 4));
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

        map = createTiles(map);

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
    private static int[][] createTiles(int[][] heightMap)
    {
        //smooth height, do 3 times to make sure everything is good.
        SmoothHeight(heightMap);
        SmoothHeight(heightMap);
        SmoothHeight(heightMap);

        // create the empty map.
        // and add the tiles in one sweep.
        int[][] map = new int[heightMap.Length][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new int[heightMap[0].Length];
            for (int j = 0; j < map[i].Length; j++)
            {
                // at a tile
                map = SetTile(map, heightMap, i, j);
            }
        }   
        return map; 
    }
    private static int[][] SmoothHeight(int[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] == 0 && NearbyCheck(map, i, j) == 0)
                {
                    map[i][j] = 1;
                } else if (map[i][j] == 1 && NearbyCheck(map, i, j) > 2)
                {
                    map[i][j] = 0;
                }
                else if (map[i][j] == 1 && 
                (
                    (i != 0 && i < map.Length - 1 
                    && map[i + 1][j] == 0 && map[i - 1][j] == 0) 
                    ||
                    (j != 0 && j < map[0].Length - 1 
                    && map[i][j + 1] == 0 && map[i][j - 1] == 0) 
                ))
                {
                    map[i][j] = 0;
                }
            }
        }
        return map;
    }
    private static int[][] SetTile(int[][] map, int[][] heightMap, int i, int j)
    {
        //int nearbySeaCount = NearbyCheck(heightMap, i, j);
        //return if it is the sea.
        if (heightMap[i][j] == 0)
        {
            return map;
        }
        //check for higher land (can't be a beach).
        if (heightMap[i][j] > 1) {
            map[i][j] = 13;
            return map;
        }
        // height == 1! check for beaches and set.
        if (i != 0 && heightMap[i - 1][j] == 0)
        { //water on top
            if (j < heightMap[0].Length - 1 && heightMap[i][j + 1] == 0) 
            { //water to the right, corner beach.
                map[i][j] = 6;
            } else if (j != 0 && heightMap[i][j - 1] == 0)
            { //water to the left, corner beach.
                map[i][j] = 7;
            } else 
            { //flat beach.
                map[i][j] = 1;
            }
            return map;
        } else if (i != heightMap.Length - 1 && heightMap[i + 1][j] == 0)
        { //water on bottom
            if (j < heightMap[0].Length - 1 && heightMap[i][j + 1] == 0) 
            { //water to the right, corner beach.
                map[i][j] = 5;
            } else if (j != 0 && heightMap[i][j - 1] == 0)
            { //water to the left, corner beach.
                map[i][j] = 8;
            } else 
            { //flat beach.
                map[i][j] = 3;
            }
            return map;
        } else if (j != 0 && heightMap[i][j - 1] == 0)
        { //water on left
            map[i][j] = 2;
            return map;
        } else if (j < heightMap[0].Length - 1 && heightMap[i][j + 1] == 0)
        { //water on right
            map[i][j] = 4;
            return map;
        } else if (j != 0 && i != 0 && heightMap[i - 1][j - 1] == 0)
        { //water on top left
            map[i][j] = 12;
            return map;
        } else if (j < heightMap[0].Length - 1 && i != 0 && heightMap[i - 1][j + 1] == 0)
        { //water on top right
            map[i][j] = 11;
            return map;
        } else if (j != 0 && i < heightMap.Length - 1 && heightMap[i + 1][j - 1] == 0)
        { //water on bottom left
            map[i][j] = 9;
            return map;
        } else if (j < heightMap[0].Length - 1 && i < heightMap.Length - 1 && heightMap[i + 1][j + 1] == 0)
        { //water on bottom right
            map[i][j] = 10;
            return map;
        }

        map[i][j] = 13;
        return map;
    }
    private static int NearbyCheck(int[][] map, int i, int j)
    {
        int count = 0; // amount of sea tiles.
        if (i != 0 && map[i - 1][j] == 0)
        {
            count++;
        }
        if (j != 0 && map[i][j - 1] == 0)
        {
            count++;
        }
        if (i < map.Length - 1 && map[i + 1][j] == 0) 
        {
            count++;
        }
        if (j < map[0].Length - 1 && map[i][j + 1] == 0) 
        {
            count++;
        } 
        return count;
    }
}
