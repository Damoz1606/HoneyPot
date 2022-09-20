using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridComponent))]
public class Board : MonoBehaviour
{
    [SerializeField] float tweeningTime = 0.25f;
    private GridComponent _gridComponent;

    private void Awake()
    {
        this._gridComponent = this.GetComponent<GridComponent>();
    }

    private void Update()
    {
        // this.TryPop();
    }

    public void InitGrid(int width, int height)
    {
        this._gridComponent.InitGrid(width, height);
    }

    public Block GetBlockAt(int x, int y) => this.GetBlockAt(new Vector2Int(x, y));

    public Block GetBlockAt(Vector2 position) => this.GetBlockAt(VectorRound.Vector2Round(position));


    public Block GetBlockAt(Vector2Int position)
    {
        try
        {
            return this._gridComponent.GetBlockAt(position);
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    public bool IsValidPosition(Tetromino tetromino)
    {
        foreach (Block block in tetromino.Blocks)
        {
            if (!this._gridComponent.IsInsideBounds(block.IntegerPosition))
                return false;
            if (this._gridComponent.GetBlockAt(block.IntegerPosition) != null &&
            this._gridComponent.GetBlockAt(block.IntegerPosition).transform.parent != tetromino.transform)
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
                if (this._gridComponent.GetBlockAt(x, y) != null &&
                this._gridComponent.GetBlockAt(x, y).transform.parent == tetromino.transform)
                {
                    this._gridComponent.SetBlockAt(x, y, null);
                }
            }
        }

        foreach (Block block in tetromino.Blocks)
        {
            this._gridComponent.SetBlockAt(block.IntegerPosition, block);
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
        SwapUtils.Swap<Block, Tile>(currentBlock, nextBlock, this._gridComponent.Grid, this.tweeningTime);
        this.TryPop();
    }

    private void TryPop()
    {
        if (PopUtils.CanPop<Block>(this._gridComponent.Grid))
        {
            Vector2[] targets = PopUtils.Pop<Block>(this._gridComponent.Grid, tweeningTime);
            DecreaseUtils.DecreaseAllAbove<Block>(targets, this._gridComponent.Grid);
        }
    }

    public Board PopAll()
    {
        PopUtils.PopAll<Block>(this._gridComponent.Grid, tweeningTime);
        return this;
    }

    public void PopExplosion(Block block)
    {
        Vector2[] targets = PopUtils.PopExplosion<Block>(block, this._gridComponent.Grid);
        DecreaseUtils.DecreaseAllAbove<Block>(targets, this._gridComponent.Grid);
    }
}
