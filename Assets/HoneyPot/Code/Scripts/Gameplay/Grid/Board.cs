using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GridComponent))]
[RequireComponent(typeof(PopComponent))]
[RequireComponent(typeof(SwapComponent))]

public class Board : MonoBehaviour
{
    private GridComponent _gridComponent;
    private IPop<IBlock> _popComponent;
    private ISwap<IBlock> _swapComponent;

    private void Awake()
    {
        this._gridComponent = this.GetComponent<GridComponent>();
        this._popComponent = this.GetComponent<IPop<IBlock>>();
        this._swapComponent = this.GetComponent<ISwap<IBlock>>();
    }

    public void InitGrid(int width, int height)
    {
        this._gridComponent.InitGrid(width, height);
    }

    public IBlock GetAt(int x, int y) => this.GetAt(new Vector2Int(x, y));
    public IBlock GetAt(Vector2 position) => this.GetAt(VectorRound.Vector2Round(position));
    public IBlock GetAt(Vector2Int position)
    {
        try
        {
            return this._gridComponent.GetAt(position);
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    public bool IsValidPosition(ITetrominoe tetrominoe)
    {
        foreach (IBlock block in tetrominoe.Blocks)
        {
            if (!this._gridComponent.IsInsideBounds(block.Position.x, block.Position.y))
                return false;
            if (this._gridComponent.GetAt(block.Position.x, block.Position.y) != null &&
            this._gridComponent.GetAt(block.Position.x, block.Position.y).transform.parent != tetrominoe.transform)
                return false;
        }
        return true;
    }

    public void UpdateTetromino(ITetrominoe tetrominoe)
    {
        for (int y = 0; y < this._gridComponent.Height; y++)
        {
            for (int x = 0; x < this._gridComponent.Width; x++)
            {
                if (this._gridComponent.GetAt(x, y) != null &&
                this._gridComponent.GetAt(x, y).transform.parent == tetrominoe.transform)
                {
                    this._gridComponent.SetAt(x, y, null);
                }
            }
        }

        foreach (IBlock block in tetrominoe.Blocks)
        {
            this._gridComponent.SetAt(block.Position.x, block.Position.y, block);
        }
    }

    public void PlaceNewTetromino()
    {
        this.TryPop();
        GameplayManagers.SpawnManager.TetrominoeNormalSpawner.OnSpawn();
    }

    public async void SwapBlock(IBlock currentBlock, IBlock nextBlock)
    {
        if (!currentBlock.CanSwap || !nextBlock.CanSwap) return;
        currentBlock.IsSwapping = true;
        nextBlock.IsSwapping = true;

        await this._swapComponent.Swap(currentBlock, nextBlock);

        bool currentCanPop = this._popComponent.CanPop(currentBlock);
        bool nextCanPop = this._popComponent.CanPop(nextBlock);

        if (currentCanPop)
            this._popComponent.Pop(currentBlock);
        if (nextCanPop)
            this._popComponent.Pop(nextBlock);
        if ((!nextCanPop && !currentCanPop) && (!currentBlock.tile.type.Equals(TileTypes.COMBO) && !nextBlock.tile.type.Equals(TileTypes.COMBO)))
            await this._swapComponent.Swap(currentBlock, nextBlock);

        currentBlock.IsSwapping = false;
        nextBlock.IsSwapping = false;
        this.TryPop();
    }

    private void TryPop()
    {
        if (this._popComponent.CanPop())
            this._popComponent.Pop();
    }
}
