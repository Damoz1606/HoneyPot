using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PopBlockComponent : MonoBehaviour
{
    [SerializeField] private float tweeningTime = 0.25f;

    public bool CanPop(Column<Block>[] grid)
    {
        int width = grid.Length;
        int height = grid[0].row.Length;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x].row[y] == null) continue;
                var (horizontal, vertical) = grid[x].row[y].GetConnections();
                if (horizontal.Count - 1 >= Constants.MIN_MATCH_COUNT || vertical.Count - 1 >= Constants.MIN_MATCH_COUNT) return true;
            }
        }

        return false;
    }

    public void Pop(Column<Block>[] grid)
    {
        int width = grid.Length;
        int height = grid[0].row.Length;

        List<Vector2> positions = new List<Vector2>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x].row[y] == null) continue;
                var (horizontal, vertical) = grid[x].row[y].GetConnections();
                if (horizontal.Count >= vertical.Count)
                {
                    if (horizontal.Count <= Constants.MIN_MATCH_COUNT) continue;
                    foreach (Block block in horizontal)
                    {
                        if (!block.CanDecrease) continue;
                        positions.Add(new Vector2(block.Position.x, block.Position.y));
                        block.DestroyWithParticles.Destroy();
                        grid[block.Position.x].row[block.Position.y] = null;
                    }
                }
                else
                {
                    if (vertical.Count <= Constants.MIN_MATCH_COUNT) continue;
                    foreach (Block block in vertical)
                    {
                        if (!block.CanDecrease) continue;
                        positions.Add(new Vector2(block.Position.x, block.Position.y));
                        block.DestroyWithParticles.Destroy();
                        grid[block.Position.x].row[block.Position.y] = null;
                    }
                }
            }
        }
        this.DecreaseAbove(positions.ToArray(), grid);
    }

    private void DecreaseAbove(Vector2[] positions, Column<Block>[] grid)
    {
        int width = grid.Length;
        int height = grid[0].row.Length;
        var sequence = DOTween.Sequence();
        foreach (Vector2 position in positions)
        {
            int x = (int)position.x;
            for (int y = (int)position.y + 1; y < height; y++)
            {
                if (grid[x].row[y] != null)
                {
                    if (!grid[x].row[y].CanDecrease) continue;
                    grid[x].row[y - 1] = grid[x].row[y];
                    grid[x].row[y] = null;
                    // grid[x].row[y - 1].transform.position += Vector3.down;

                    sequence.Join(grid[x].row[y - 1].transform.DOMove(grid[x].row[y - 1].transform.position + Vector3.down, tweeningTime)).SetEase(Ease.OutBack);
                    sequence.Play();
                }
            }
        }
    }

}
