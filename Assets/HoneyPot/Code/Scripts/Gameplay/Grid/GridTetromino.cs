using System.Collections;
using UnityEngine;

public class GridTetromino : Grid
{

    public void PlaceTetrominoInGrid()
    {
        foreach (Transform child in GameplayManagers.GameManager.BlockHolder)
            if (child.childCount <= 1)
                Destroy(child.gameObject);
        GameplayManagers.SpawnManager.TetrominoSpawnManager.Spawn();
    }

    public bool IsValidGridPosition(Transform tetromino)
    {
        foreach (Transform child in tetromino)
        {
            if (child.gameObject.CompareTag("Block"))
            {
                Vector2 block = VectorRound.Vector2Round(child.position);
                if (!this.IsInsideBounds(block))
                    return false;

                if (this._gameGridColumn[(int)block.x].row[(int)block.y] != null &&
                    this._gameGridColumn[(int)block.x].row[(int)block.y].parent != tetromino)
                    return false;
            }
        }
        return true;
    }

    public void UpdateGrid(Transform tetromino)
    {
        for (int y = 0; y < this._gridHeight; ++y)
            for (int x = 0; x < this._gridWidth; ++x)
                if (this._gameGridColumn[x].row[y] != null &&
                this._gameGridColumn[x].row[y].parent == tetromino)
                    this._gameGridColumn[x].row[y] = null;

        foreach (Transform child in tetromino)
        {
            if (child.gameObject.CompareTag("Block"))
            {
                Vector2 position = VectorRound.Vector2Round(child.position);
                this._gameGridColumn[(int)position.x].row[(int)position.y] = child;
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
        if (this._gameGridColumn[x].row[y] != null)
        {
            if (this._gameGridColumn[x].row[y].GetComponent<Block>() == null || !this._gameGridColumn[x].row[y].GetComponent<Block>().CanDecrease) return;
            this._gameGridColumn[x].row[y - 1] = this._gameGridColumn[x].row[y];
            this._gameGridColumn[x].row[y] = null;

            this._gameGridColumn[x].row[y - 1].position += Vector3.down;
        }
    }

    public override void RemoveTile(Vector2 tile)
    {
        int x = (int)tile.x;
        int y = (int)tile.y;
        if (this._gameGridColumn[x].row[y].gameObject.GetComponent<Block>() != null)
        {
            this._gameGridColumn[x].row[y].gameObject.GetComponent<Block>().DestroyWithParticles.Destroy();
        }
        else
        {
            Destroy(this._gameGridColumn[x].row[y].gameObject);
        }
        this._gameGridColumn[x].row[y] = null;
    }
}