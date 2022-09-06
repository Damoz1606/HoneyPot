using UnityEngine;

public static class Constants
{
    public static int GRID_WIDTH = 10;
    public static int GRID_HEIGHT = 16;
    private static int GRID_GREACE_HEIGHT = 4;
    public static Vector3 GRID_TETROMINO_SPAWN = new Vector3(GRID_WIDTH / 2, GRID_HEIGHT - GRID_GREACE_HEIGHT, 0);
    public static int MIN_MATCH_COUNT = 2;
}