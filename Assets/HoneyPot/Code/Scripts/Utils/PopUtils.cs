using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PopUtils
{
    public static void OnComplete(UtilsCallback action) => action();

    public static bool CanPop<T>(T cell) where T : Block
    {
        var (horizontalConnections, verticalConnections) = cell.GetConnections();
        if (horizontalConnections.Skip(1).Count() >= Constants.MIN_MATCH_COUNT ||
        verticalConnections.Skip(1).Count() >= Constants.MIN_MATCH_COUNT)
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
                var (horizontalConnections, verticalConnections) = grid[x].row[y].GetConnections();
                if (horizontalConnections.Skip(1).Count() >= Constants.MIN_MATCH_COUNT ||
                verticalConnections.Skip(1).Count() >= Constants.MIN_MATCH_COUNT)
                    return true;
            }
        }

        return false;
    }

    public static Vector2[] Pop<T>(T cell, Column<T>[] grid, float tweeningTime = 0.25f) where T : Block
    {
        List<Vector2> targets = new List<Vector2>();
        var (horizontalConnections, verticalConnections) = cell.GetConnections();
        if (horizontalConnections.Count > verticalConnections.Count)
        {
            if (horizontalConnections.Skip(1).Count() < Constants.MIN_MATCH_COUNT) return targets.ToArray();
            foreach (T child in horizontalConnections)
            {
                if (!child.CanPop) continue;
                targets.Add(child.transform.position);
                child.Destroy();
                grid[(int)child.transform.position.x].row[(int)child.transform.position.y] = null;
            }
        }
        else
        {
            if (verticalConnections.Skip(1).Count() < Constants.MIN_MATCH_COUNT) return targets.ToArray();
            foreach (T child in verticalConnections)
            {
                if (!child.CanPop) continue;
                targets.Add(child.transform.position);
                child.Destroy();
                grid[(int)child.transform.position.x].row[(int)child.transform.position.y] = null;
            }
        }

        return targets.OrderBy(o => o.y).ToArray();
    }

    public static Vector2[] Pop<T>(Column<T>[] grid, float tweeningTime = 0.25f) where T : Block
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
}