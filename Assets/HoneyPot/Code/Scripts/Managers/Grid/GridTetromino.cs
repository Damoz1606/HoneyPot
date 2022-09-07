using System.Collections;
using UnityEngine;

public class GridTetromino : Grid
{

    public void PlaceTetrominoInGrid()
    {
        GameplayManagers.SpawnManager.TetrominoSpawnManager.Spawn();
    }

    public bool IsValidGridPosition(Transform tetromino)
    {
        foreach (Transform child in tetromino)
        {
            if (child.gameObject.CompareTag("Block"))
            {
                Vector2 pos = VectorRound.Vector2Round(child.position);
                if (!this.IsInsideBounds(pos))
                {
                    return false;
                }
                if (this._grid[(int)pos.x, (int)pos.y] != null &&
                    this._grid[(int)pos.x, (int)pos.y].parent != tetromino)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void UpdateGrid(Transform tetromino)
    {
        for (int y = 0; y < Constants.GRID_HEIGHT; y++)
        {
            for (int x = 0; x < Constants.GRID_WIDTH; x++)
            {
                if (this._grid[x, y] != null)
                {
                    if (this._grid[x, y].parent == tetromino)
                    {
                        this._grid[x, y] = null;
                    }

                }
            }
        }

        foreach (Transform child in tetromino)
        {
            if (child.gameObject.CompareTag("Block"))
            {
                Vector2 position = VectorRound.Vector2Round(child.position);
                this._grid[(int)position.x, (int)position.y] = child;
            }
        }
    }

    public override void ClearGrid()
    {
        throw new System.NotImplementedException();
    }

    public override void DecreaseTile(Vector2 tile)
    {
        int x = (int)tile.x;
        int y = (int)tile.y;
        if (this._grid[x, y] == null) return;
        if (this._grid[x, y].GetComponent<Block>() == null || !this._grid[x, y].GetComponent<Block>().CanDecrease) return;
        this._grid[x, y - 1] = this._grid[x, y];
        this._grid[x, y] = null;
        this._grid[x, y - 1].position += new Vector3(0, -1, 0);
    }

    public override void RemoveTile(Vector2 tile)
    {
        int x = (int)tile.x;
        int y = (int)tile.y;
        if (this._grid[x, y] == null) return;
        Destroy(this._grid[x, y].gameObject);
        this._grid[x, y] = null;
    }
}