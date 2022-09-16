using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public static class DestroyUtils
{
    public static async void DecreaseAllAbove<T>(Vector2[] targets, Column<T>[] grid, float tweeningTime = 0.25f) where T : Block
    {
        int width = grid.Length;
        int height = grid[0].row.Length;

        targets = targets.OrderBy(o => o.y).ToArray();

        foreach (Vector2 target in targets)
        {
            await DecreaseAboveAsync(target, grid, tweeningTime);
        }
    }

    public static async void DecreaseAbove<T>(Vector2 target, Column<T>[] grid, float tweeningTime = 0.25f) where T : Block
    {
        await DecreaseAboveAsync(target, grid, tweeningTime);
    }

    private static async Task DecreaseAboveAsync<T>(Vector2 target, Column<T>[] grid, float tweeningTime = 0.05f) where T : Block
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
            sequence.Join(grid[x].row[y - 1].transform.DOMove(grid[x].row[y - 1].transform.position + Vector3.down, tweeningTime)).SetEase(Ease.OutBack);
            await sequence.Play().AsyncWaitForCompletion();
        }
    }
}