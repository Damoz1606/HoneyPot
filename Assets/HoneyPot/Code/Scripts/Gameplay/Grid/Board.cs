using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridComponent))]
[RequireComponent(typeof(SwapTileComponent))]
[RequireComponent(typeof(PopBlockComponent))]
public class Board : MonoBehaviour
{
    private GridComponent _gridComponent;
    private SwapTileComponent _swapTileComponent;
    private PopBlockComponent _popBlockComponent;

    public Column<Block>[] Grid { get { return this._gridComponent.Grid; } }

    private void Awake()
    {
        this._gridComponent = this.GetComponent<GridComponent>();
        this._swapTileComponent = this.GetComponent<SwapTileComponent>();
        this._popBlockComponent = this.GetComponent<PopBlockComponent>();
    }

    public void InitGrid(int width, int height)
    {
        this._gridComponent.InitGrid(width, height);
    }

    public Block GetBlockAt(Vector2 position)
    {
        return this._gridComponent.GetBlockAt(position);
    }

    public bool IsValidPosition(Tetromino tetromino)
    {
        foreach (Block block in tetromino.Blocks)
        {
            Vector2 position = VectorRound.Vector2Round(block.transform.position);
            if (!this._gridComponent.IsInsideBounds(position))
                return false;
            if (this._gridComponent.Grid[(int)position.x].row[(int)position.y] != null &&
            this._gridComponent.Grid[(int)position.x].row[(int)position.y].transform.parent != tetromino.transform)
                return false;
        }
        return true;
    }

    public void UpdateTetromino(Tetromino tetromino)
    {
        for (int y = 0; y < this._gridComponent.Height; y++)
        {
            for (int x = 0; x < this._gridComponent.Width; x++)
            {
                if (this._gridComponent.GetBlockAt(new Vector2(x, y)) != null &&
                this._gridComponent.GetBlockAt(new Vector2(x, y)).transform.parent == tetromino.transform)
                {
                    this._gridComponent.SetBlockAt(new Vector2(x, y), null);
                }
            }
        }

        foreach (Block block in tetromino.Blocks)
        {
            Vector2 position = VectorRound.Vector2Round(block.transform.position);
            block.Position = new Vector2Int((int)position.x, (int)position.y);
            this._gridComponent.SetBlockAt(new Vector2(position.x, position.y), block);
        }
    }

    public void PlaceNewTetromino()
    {
        this.PopController();
        GameplayManagers.SpawnManager.TetrominoSpawnManager.Spawn();
    }

    public void SwapBlock(Block currentBlock, Block nextBlock)
    {
        if (!currentBlock.CanSwipe || !nextBlock.CanSwipe) return;
        this._swapTileComponent.SwapTiles(currentBlock, nextBlock, this._gridComponent.Grid);

        if (this._popBlockComponent.CanPop(_gridComponent.Grid))
        {
            Debug.Log("Poping");
            this._popBlockComponent.Pop(_gridComponent.Grid);
            this.PopController();
        }
        else
        {
            Debug.Log("Swapping");
            this._swapTileComponent.SwapTiles(currentBlock, nextBlock, this._gridComponent.Grid);
        }
    }

    private void PopController()
    {
        if (this._popBlockComponent.CanPop(_gridComponent.Grid))
        {
            Debug.Log("Poping 2");
            this._popBlockComponent.Pop(_gridComponent.Grid);
            // this.PopController();
        }
    }
}
