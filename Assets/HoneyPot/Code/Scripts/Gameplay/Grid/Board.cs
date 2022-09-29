using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GridComponent))]
public class Board : MonoBehaviour, IDecrease, IFusion<IBlock>, IPop<IBlock>, ISwap<IBlock>
{
    // [SerializeField] float tweeningTime = 0.25f;
    private GridComponent _gridComponent;

    private void Awake()
    {
        this._gridComponent = this.GetComponent<GridComponent>();
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

    public void SwapBlock(IBlock currentBlock, IBlock nextBlock)
    {
        if (nextBlock == default) return;
        if (currentBlock == default) return;
        if (!currentBlock.CanSwap || !nextBlock.CanSwap) return;
        currentBlock.IsSwapping = true;
        nextBlock.IsSwapping = true;

        this.Swap(currentBlock, nextBlock);

        bool currentCanPop = this.CanPop(currentBlock);
        bool nextCanPop = this.CanPop(nextBlock);

        if (currentCanPop)
            this.Pop(currentBlock);
        if (nextCanPop)
            this.Pop(nextBlock);
        if (!nextCanPop && !currentCanPop)
            this.Swap(currentBlock, nextBlock);

        currentBlock.IsSwapping = false;
        nextBlock.IsSwapping = false;
        this.TryPop();
    }

    private void TryPop()
    {
        if (this.CanPop())
            this.Pop();
    }

    public void LookForCombos(IBlock block, List<IBlock> blocks)
    {
        if (blocks.Count <= Constants.COMBO_NORMAL) return;
        else
        {
            if (blocks.Count > Constants.COMBO_HONEYPOT)
            {
                TileCombo tile = GameplayManagers.ComboManager.InstanceCombo(ComboTypes.HONEYPOT);
                this.FusionCellsTo(block, blocks);
                block.AttachTile(tile);
            }
            else if (blocks.Count > Constants.COMBO_BEE_POLLEN)
            {
                /* Deattach and Attach tiles */
                this.FusionCellsTo(block, blocks);
            }
            else
            {
                List<Vector3Int> vectors = new List<Vector3Int>();
                blocks.ForEach(item =>
                {
                    vectors.Add(item.Position);
                    this.DestroyBlock(item.Position);
                });
                this.DecreaseAllAbove(vectors);
            }
        }
    }

    public void Swap(IBlock currentBlock, IBlock nextBlock)
    {
        if (currentBlock == null || nextBlock == null) return;
        ITile currentTile = currentBlock.tile;
        ITile nextTile = nextBlock.tile;

        #region Sequence
        var sequence = DOTween.Sequence();
        sequence.Join(currentTile.transform.DOMove(nextTile.transform.position, 0.75f).SetEase(Ease.OutBack))
        .Join(nextTile.transform.DOMove(currentTile.transform.position, 0.75f).SetEase(Ease.OutBack));

        sequence.Play()
        .OnComplete(() =>
        {
            currentBlock.AttachTile(nextTile);
            nextBlock.AttachTile(currentTile);
        });
        #endregion

        this.ComboSwap(currentBlock);
        this.ComboSwap(nextBlock);
    }

    public void ComboSwap(IBlock block)
    {
        if (block == null) return;
        if (block.tile.type.Equals(TileTypes.COMBO))
            block.tile.OnEffect(block);
    }

    public bool CanPop()
    {
        int width = this._gridComponent.Width;
        int height = this._gridComponent.Height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (this._gridComponent.GetAt(x, y) == default) continue;
                if (this.CanPop(this._gridComponent.GetAt(x, y))) return true;
            }
        }
        return false;
    }

    public bool CanPop(IBlock block)
    {
        var horizontalConnections = block.GetConnections(AxisTypes.HORIZONTAL);
        var verticalConnections = block.GetConnections(AxisTypes.VERTICAL);
        return (horizontalConnections.Count > Constants.COMBO_NORMAL ||
        verticalConnections.Count > Constants.COMBO_NORMAL);
    }

    public void Pop(IBlock block)
    {
        List<IBlock> horizontalConnections = block.GetConnections(AxisTypes.HORIZONTAL);
        List<IBlock> verticalConnections = block.GetConnections(AxisTypes.VERTICAL);
        if (horizontalConnections.Count > verticalConnections.Count)
            this.LookForCombos(block, horizontalConnections);
        else
            this.LookForCombos(block, horizontalConnections);
    }

    public void Pop()
    {
        int width = this._gridComponent.Width;
        int height = this._gridComponent.Height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (this._gridComponent.GetAt(x, y) == null) continue;
                if (this._gridComponent.GetAt(x, y).CanPop) continue;
                this.Pop(this._gridComponent.GetAt(x, y));
            }
        }
    }

    public void FusionCellsTo(IBlock block, List<IBlock> blocks)
    {
        List<Vector3Int> vectors = new List<Vector3Int>();
        blocks.ForEach(item =>
        {
            Vector3Int vector = item.Position;
            vectors.Add(vector);
            item.transform.DOMove(block.Position, 0.5f).Play()
            .OnComplete(() => this.DestroyBlock(vector));
        });
        this.DecreaseAllAbove(vectors);

    }

    public void DecreaseAbove(Vector3Int target)
    {
        int width = this._gridComponent.Width;
        int height = this._gridComponent.Height;

        int x = (int)target.x;
        for (int y = (int)target.y + 1; y < height; y++)
        {
            if (this._gridComponent.GetAt(x, y) == null) continue;
            if (!this._gridComponent.GetAt(x, y).CanDecrease) continue;
            this._gridComponent.SetAt(x, y - 1, this._gridComponent.GetAt(x, y));
            this._gridComponent.SetAt(x, y, default);
            IBlock block = this._gridComponent.GetAt(x, y - 1);
            block.transform
            .DOMove(block.transform.position + Vector3.down, 0.5f)
            .SetEase(Ease.OutBounce)
            .Play();
        }
    }

    public void DecreaseAllAbove(List<Vector3Int> targets)
    {
        targets = targets.OrderByDescending(o => o.y).ToList();
        foreach (Vector3Int target in targets)
            this.DecreaseAbove(target);
    }

    public void DestroyBlock(Vector3Int vector)
    {
        if (this._gridComponent.GetAt(vector.x, vector.y) == default) return;
        if (this._gridComponent.GetAt(vector.x, vector.y).CanPop) return;
        IBlock aux = this._gridComponent.GetAt(vector.x, vector.y);
        this._gridComponent.SetAt(aux.Position.x, aux.Position.y, default);
        aux.OnEffect();
        /* Pool Release */
    }
}
