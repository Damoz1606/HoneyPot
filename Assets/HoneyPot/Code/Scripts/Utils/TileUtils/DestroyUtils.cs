using System.Collections.Generic;
using UnityEngine;

public static class DestroyUtils
{

    public static List<Vector2> DestroyBlocks<T>(List<T> connections, Column<T>[] grid, ParticlesTypes type = ParticlesTypes.DEFAULT)
    where T : Block
    {
        List<Vector2> targets = new List<Vector2>();
        foreach (T child in connections)
        {
            if (child == null) continue;
            if (!child.CanPop) continue;
            targets.Add(child.transform.position);
            grid[child.IntegerPosition.x].row[child.IntegerPosition.y] = null;
            // child.Destroy(type);
            child.OnDeactivate();
        }
        return targets;
    }

    public static List<Vector2> DestroyBlocks<T>(Vector2[] connections, Column<T>[] grid, ParticlesTypes type = ParticlesTypes.DEFAULT)
    where T : Block => DestroyBlocks<T>(VectorRound.Vectors2Round(connections), grid, type);
    public static List<Vector2> DestroyBlocks<T>(Vector2Int[] connections, Column<T>[] grid, ParticlesTypes type = ParticlesTypes.DEFAULT)
    where T : Block
    {
        List<Vector2> targets = new List<Vector2>();
        foreach (Vector2Int child in connections)
        {
            if (grid[child.x].row[child.y] == null) continue;
            if (!grid[child.x].row[child.y].CanPop) continue;
            targets.Add(child);
            T cell = (T)grid[child.x].row[child.y];
            grid[child.x].row[child.y] = null;
            // cell.Destroy(type);
            cell.OnDeactivate();
        }
        return targets;
    }
}