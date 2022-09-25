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

    public bool IsValidPosition(Tetrominoe tetrominoe)
    {
        foreach (Block block in tetrominoe.Blocks)
        {
            if (!this._gridComponent.IsInsideBounds(block.IntegerPosition))
                return false;
            if (this._gridComponent.GetBlockAt(block.IntegerPosition) != null &&
            this._gridComponent.GetBlockAt(block.IntegerPosition).transform.parent != tetrominoe.transform)
                return false;
        }
        return true;
    }
    
    public void UpdateTetromino(Tetrominoe tetrominoe)
    {
        for (int y = 0; y < this._gridComponent.Height; y++)
        {
            for (int x = 0; x < this._gridComponent.Width; x++)
            {
                if (this._gridComponent.GetBlockAt(x, y) != null &&
                this._gridComponent.GetBlockAt(x, y).transform.parent == tetrominoe.transform)
                {
                    this._gridComponent.SetBlockAt(x, y, null);
                }
            }
        }

        foreach (Block block in tetrominoe.Blocks)
        {
            this._gridComponent.SetBlockAt(block.IntegerPosition, block);
        }
    }

    public void PlaceNewTetromino()
    {
        this.TryPop();
        GameplayManagers.SpawnManager.TetrominoePoolSpawner.Spawn();
    }

    public async void SwapBlock(Block currentBlock, Block nextBlock)
    {
        if (!currentBlock.CanSwipe || !nextBlock.CanSwipe) return;
        currentBlock.IsSwapping = true;
        nextBlock.IsSwapping = true;

        await SwapUtils.SwapAsync<Block, Tile>(currentBlock, nextBlock, tweeningTime);

        bool currentCanPop = PopUtils.CanPop<Block>(currentBlock);
        bool nextCanPop = PopUtils.CanPop<Block>(nextBlock);

        if (currentCanPop)
        {
            Vector2[] targets = PopUtils.Pop<Block>(currentBlock, this._gridComponent.Grid, tweeningTime);
            DecreaseUtils.DecreaseAllAbove<Block>(targets, this._gridComponent.Grid);
        }
        if (nextCanPop)
        {
            Vector2[] targets = PopUtils.Pop<Block>(nextBlock, this._gridComponent.Grid, tweeningTime);
            DecreaseUtils.DecreaseAllAbove<Block>(targets, this._gridComponent.Grid);
        }
        if (!nextCanPop && !currentCanPop)
        {
            await SwapUtils.SwapAsync<Block, Tile>(currentBlock, nextBlock, tweeningTime);
        }

        currentBlock.IsSwapping = false;
        nextBlock.IsSwapping = false;
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

    public void PopAll()
    {
        PopUtils.PopAll<Block>(this._gridComponent.Grid, tweeningTime);
    }

    public void PopVerticalAxis(Block block)
    {
        PopUtils.PopVerticalAxis<Block>(block, this._gridComponent.Grid);
    }

    public void PopExplosion(Block block)
    {
        Vector2[] targets = PopUtils.PopExplosion<Block>(block, this._gridComponent.Grid);
        DecreaseUtils.DecreaseAllAbove<Block>(targets, this._gridComponent.Grid);
    }
}
