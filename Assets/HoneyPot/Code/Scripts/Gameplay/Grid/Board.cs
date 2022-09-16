using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridComponent))]
public class Board : MonoBehaviour
{
    [SerializeField] float tweeningTime = 0.25f;
    private GridComponent _gridComponent;

    public Column<Block>[] Grid { get { return this._gridComponent.Grid; } }

    private void Awake()
    {
        this._gridComponent = this.GetComponent<GridComponent>();
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
            this._gridComponent.SetBlockAt(new Vector2(position.x, position.y), block);
        }
    }

    public void PlaceNewTetromino()
    {
        this.TryPop();
        GameplayManagers.SpawnManager.TetrominoSpawnManager.Spawn();
    }

    public void SwapBlock(Block currentBlock, Block nextBlock)
    {
        if (!currentBlock.CanSwipe || !nextBlock.CanSwipe) return;
        SwapUtils.Swap<Block, Tile>(currentBlock, nextBlock, this.Grid, this.tweeningTime);
        this.TryPop();
    }

    private void TryPop()
    {
        if (PopUtils.CanPop<Block>(this.Grid))
        {
            Vector2[] targets = PopUtils.Pop<Block>(this.Grid, tweeningTime);
            DestroyUtils.DecreaseAllAbove<Block>(targets, this.Grid, tweeningTime);
            this.TryPop();
        }
    }
}
