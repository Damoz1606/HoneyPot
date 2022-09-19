using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public static class DecreaseUtils
{
    public static void DecreaseAllInstant<T>(List<Vector2[]> targets, Column<T>[] grid, float tweeningTime = Constants.TWEENING_DECREASE_TIME)
        where T : Block
    {
        foreach (Vector2[] vectors in targets)
        {
            if (vectors.Length == 0) continue;
            DecreaseInstant(vectors, grid, tweeningTime);
        }
    }

    public static void DecreaseAllInstant<T>(List<Vector2Int[]> targets, Column<T>[] grid, float tweeningTime = Constants.TWEENING_DECREASE_TIME)
    where T : Block
    {
        foreach (Vector2Int[] vectors in targets)
        {
            if (vectors.Length == 0) continue;
            DecreaseInstant(vectors, grid, tweeningTime);
        }
    }

    public static void DecreaseInstant<T>(Vector2[] targets, Column<T>[] grid, float tweeningTime = Constants.TWEENING_DECREASE_TIME)
    where T : Block => DecreaseInstant(VectorRound.Vectors2Round(targets), grid, tweeningTime);
    public static async void DecreaseInstant<T>(Vector2Int[] targets, Column<T>[] grid, float tweeningTime = Constants.TWEENING_DECREASE_TIME) where T : Block
    {
        int max = targets.Max(o => o.y);
        int min = targets.Min(o => o.y);
        int x = targets[0].x;
        await DecreaseInstantAsync(new Vector2Int(x, min), new Vector2Int(x, max), grid, tweeningTime);
    }

    private static async Task DecreaseInstantAsync<T>(Vector2Int inferior, Vector2Int superior, Column<T>[] grid, float tweeningTime) where T : Block
    {
        int height = grid[0].row.Length;
        int min = inferior.y;
        int max = superior.y;
        int x = inferior.x;

        for (int y = (int)max + 1; y < height; y++)
        {
            int newY = min + (y - (max + 1));
            if (grid[x].row[y] == null) continue;
            if (!grid[x].row[y].CanDecrease) continue;
            grid[x].row[newY] = grid[x].row[y];
            Debug.Log($"Vector: {x}, {newY}");
            grid[x].row[y] = null;

            var sequence = DOTween.Sequence();
            sequence.Join(grid[x].row[newY].transform
            .DOMove(new Vector3(x, newY, 0), tweeningTime))
            .SetEase(Ease.OutBack);
            await sequence.Play().AsyncWaitForCompletion();
        }
    }

    public static void DecreaseAllAbove<T>(Vector2[] targets, Column<T>[] grid, float tweeningTime = Constants.TWEENING_DECREASE_TIME)
    where T : Block => DecreaseAllAbove<T>(VectorRound.Vectors2Round(targets), grid, tweeningTime);
    public static async void DecreaseAllAbove<T>(Vector2Int[] targets, Column<T>[] grid, float tweeningTime = Constants.TWEENING_DECREASE_TIME) where T : Block
    {
        foreach (Vector2Int target in targets)
        {
            await DecreaseAboveAsync(target, grid, tweeningTime);
        }
    }

    public static void DecreaseAbove<T>(Vector2 target, Column<T>[] grid, float tweeningTime = Constants.TWEENING_DECREASE_TIME)
    where T : Block => DecreaseAbove<T>(VectorRound.Vector2Round(target), grid, tweeningTime);
    public static async void DecreaseAbove<T>(Vector2Int target, Column<T>[] grid, float tweeningTime = Constants.TWEENING_DECREASE_TIME) where T : Block
    {
        await DecreaseAboveAsync(target, grid, tweeningTime);
    }

    private static async Task DecreaseAboveAsync<T>(Vector2Int target, Column<T>[] grid, float tweeningTime) where T : Block
    {
        int width = grid.Length;
        int height = grid[0].row.Length;

        int x = (int)target.x;
        for (int y = (int)target.y + 1; y < height; y++)
        {
            if (grid[x].row[y] == null) continue;
            if (!grid[x].row[y].CanDecrease) continue;
            grid[x].row[y - 1] = grid[x].row[y];
            grid[x].row[y] = null;

            var sequence = DOTween.Sequence();
            sequence.Join(grid[x].row[y - 1].transform
            .DOMove(grid[x].row[y - 1].transform.position + Vector3.down, tweeningTime))
            .SetEase(Ease.InBounce);
            await sequence.Play().AsyncWaitForCompletion();
        }
    }
}