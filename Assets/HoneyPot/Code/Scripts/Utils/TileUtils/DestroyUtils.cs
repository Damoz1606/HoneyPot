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
            if (!DestroyBlock(child.transform.position, grid, type)) continue;
            targets.Add(child.transform.position);
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
            if (!DestroyBlock(child, grid, type)) continue;
            targets.Add(child);
        }
        return targets;
    }

    public static bool DestroyBlock<T>(Vector2 connection, Column<T>[] grid, ParticlesTypes type = ParticlesTypes.DEFAULT)
    where T : Block => DestroyBlock(VectorRound.Vector2Round(connection), grid, type);
    public static bool DestroyBlock<T>(Vector2Int connection, Column<T>[] grid, ParticlesTypes type = ParticlesTypes.DEFAULT)
    where T : Block
    {
        if (grid[connection.x].row[connection.y] == null) return false;
        if (!grid[connection.x].row[connection.y].CanPop) return false;
        T cell = (T)grid[connection.x].row[connection.y];
        grid[connection.x].row[connection.y] = null;
        GameplayManagers.SpawnManager.BlockSpawner.OnKill(cell);
        return true;
    }
}