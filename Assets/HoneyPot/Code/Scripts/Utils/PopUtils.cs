using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PopUtils
{
    public static bool CanPop<T>(T cell) where T : Block
    {
        var horizontalConnections = cell.GetConnectionsHorizontal();
        var verticalConnections = cell.GetConnectionsVertical();
        if (horizontalConnections.Count > Constants.MIN_MATCH_COUNT ||
        verticalConnections.Count > Constants.MIN_MATCH_COUNT)
            return true;

        return false;
    }

    public static bool CanPop<T>(Column<T>[] grid) where T : Block
    {
        int width = grid.Length;
        int height = grid[0].row.Length;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x].row[y] == null) continue;
                if (CanPop(grid[x].row[y])) return true;
            }
        }

        return false;
    }

    public static Vector2[] Pop<T>(T cell, Column<T>[] grid, float tweeningTime = Constants.TWEENING_POP_TIME) where T : Block
    {
        List<Vector2> targets = new List<Vector2>();
        var horizontalConnections = cell.GetConnectionsHorizontal();
        var verticalConnections = cell.GetConnectionsVertical();
        if (horizontalConnections.Count > verticalConnections.Count)
        {
            if (horizontalConnections.Count <= Constants.MIN_MATCH_COUNT) return targets.ToArray();
            foreach (T child in horizontalConnections)
            {
                if (!child.CanPop) continue;
                targets.Add(child.transform.position);
                grid[child.IntegerPosition.x].row[child.IntegerPosition.y] = null;
                child.Destroy();
            }
        }
        else
        {
            if (verticalConnections.Count <= Constants.MIN_MATCH_COUNT) return targets.ToArray();
            foreach (T child in verticalConnections)
            {
                if (!child.CanPop) continue;
                targets.Add(child.transform.position);
                grid[child.IntegerPosition.x].row[child.IntegerPosition.y] = null;
                child.Destroy();
            }
        }

        return targets.OrderByDescending(o => o.y).ToArray();
    }

    public static Vector2[] Pop<T>(Column<T>[] grid, float tweeningTime = Constants.TWEENING_POP_TIME) where T : Block
    {
        int width = grid.Length;
        int height = grid[0].row.Length;
        List<Vector2> targets = new List<Vector2>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x].row[y] == null) continue;
                targets.AddRange(Pop<T>(grid[x].row[y], grid));
            }
        }

        return targets.ToArray();
    }

    public static void PopAll<T>(Column<T>[] grid, float tweeningTime = Constants.TWEENING_POP_TIME) where T : Block
    {
        int width = grid.Length;
        int height = grid[0].row.Length;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x].row[y] == null) continue;
                grid[x].row[y].Destroy();
                grid[x].row[y] = null;
            }
        }

    }
}