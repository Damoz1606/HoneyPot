using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PopUtils
{
    public static bool CanPop<T>(T cell) where T : Block
    {
        var horizontalConnections = cell.GetConnectionsHorizontal();
        var verticalConnections = cell.GetConnectionsVertical();
        if (horizontalConnections.Count > Constants.COMBO_NORMAL ||
        verticalConnections.Count > Constants.COMBO_NORMAL)
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
        var horizontalConnections = cell.GetConnectionsHorizontal().OfType<T>().ToList();
        var verticalConnections = cell.GetConnectionsVertical().OfType<T>().ToList();
        if (horizontalConnections.Count == verticalConnections.Count && horizontalConnections.Count > Constants.COMBO_STAR)
        {
            verticalConnections.Remove(cell);
            horizontalConnections.AddRange(verticalConnections);
            GameplayManagers.ComboManager.InstanceCombo(ComboTypes.STAR, cell);
        }
        else if (horizontalConnections.Count > verticalConnections.Count)
        {
            targets.AddRange(LookForCombos(cell, horizontalConnections, grid));
        }
        else
        {
            targets.AddRange(LookForCombos(cell, verticalConnections, grid));
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
                if (!grid[x].row[y].CanPop) continue;
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
                if (!grid[x].row[y].CanPop) continue;
                grid[x].row[y].OnDeactivate();
                grid[x].row[y] = null;
            }
        }
    }

    private static List<Vector2> LookForCombos<T>(T cell, List<T> connections, Column<T>[] grid)
    where T : Block
    {
        if (connections.Count <= Constants.COMBO_NORMAL) return new List<Vector2>();
        else
        {
            if (connections.Count > Constants.COMBO_HONEYPOT)
            {
                List<Vector2Int> vectors = new List<Vector2Int>();
                connections.Remove(cell);
                vectors.AddRange(connections.Select(o => o.IntegerPosition).ToArray());
                GameplayManagers.ComboManager.InstanceCombo(ComboTypes.HONEYPOT, cell);
                FusionUtils.FusionCells(cell, connections, grid);
                return DestroyUtils.DestroyBlocks(vectors.ToArray(), grid);
            }
            else if (connections.Count > Constants.COMBO_BEE_POLLEN)
            {
                List<Vector2Int> vectors = new List<Vector2Int>();
                connections.Remove(cell);
                vectors.AddRange(connections.Select(o => o.IntegerPosition).ToArray());
                GameplayManagers.ComboManager.InstanceCombo(ComboTypes.BOMB, cell);
                FusionUtils.FusionCells(cell, connections, grid);
                return DestroyUtils.DestroyBlocks(vectors.ToArray(), grid);
            }
            else return DestroyUtils.DestroyBlocks(connections, grid);
        }
    }

    public static Vector2[] PopExplosion<T>(T cell, Column<T>[] grid)
    where T : Block
    {
        List<T> connections = new List<T> {
            cell,
            (T) cell.Top,
            (T) cell.Bottom,
            (T) cell.Right,
            (cell.Right != null) ? (T)cell.Right.Top : null,
            (cell.Right != null) ? (T)cell.Right.Bottom : null,
            (T) cell.Left,
            (cell.Left != null) ? (T)cell.Left.Top : null,
            (cell.Left != null) ? (T)cell.Left.Bottom : null,
        };
        return DestroyUtils.DestroyBlocks(connections, grid, ParticlesTypes.BEES).ToArray();
    }

    public static Vector2[] PopHorizontalAxis<T>(T cell, Column<T>[] grid)
    where T : Block
    {
        return DestroyUtils.DestroyBlocks(cell.GetConnectionsHorizontal().OfType<T>().ToList(), grid).ToArray();
    }

    public static Vector2[] PopVerticalAxis<T>(T cell, Column<T>[] grid)
    where T : Block
    {
        return DestroyUtils.DestroyBlocks(cell.GetConnectionsVertical().OfType<T>().ToList(), grid).ToArray();
    }


}