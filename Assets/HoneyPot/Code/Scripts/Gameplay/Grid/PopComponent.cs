using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(DecreaseComponent))]
[RequireComponent(typeof(GridComponent))]
public class PopComponent : MonoBehaviour, IFusion<IBlock>, IPop<IBlock>, IComboSearch<IBlock>
{
    [SerializeField] private TweeningModel _tweening;

    [SerializeField] private ChannelTile _challengeTile;

    private GridComponent _gridComponent;
    private IDecrease _decreaseComponent;

    private void Awake()
    {
        this._gridComponent = this.GetComponent<GridComponent>();
        this._decreaseComponent = this.GetComponent<IDecrease>();
    }

    private void OnDisable()
    {
        this._challengeTile.ListenBomb += this.PopAround;
        this._challengeTile.ListenHoneypot += this.PopAllBlocks;
    }

    private void OnEnable()
    {

    }

    public bool CanPop()
    {
        int width = this._gridComponent.Width;
        int height = this._gridComponent.Height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                IBlock block = this._gridComponent.GetAt(x, y);
                if (block == null) continue;
                if (this.CanPop(block)) return true;
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

    public void DestroyBlock(Vector3Int vector)
    {
        if (this._gridComponent.GetAt(vector.x, vector.y) == null) return;
        if (!this._gridComponent.GetAt(vector.x, vector.y).CanPop) return;
        IBlock aux = this._gridComponent.GetAt(vector.x, vector.y);
        aux.OnEffect();
        this._gridComponent.SetAt(aux.Position.x, aux.Position.y, default);
        GameplayManagers.SpawnManager.BlockNormalSpawner.OnKill((BlockNormal)aux);
    }

    public void FusionCellsTo(IBlock block, List<IBlock> blocks)
    {
        // List<Vector3Int> vectors = new List<Vector3Int>();
        blocks.ForEach(async item =>
        {
            Vector3Int vector = item.Position;
            // vectors.Add(vector);
            await item.transform.DOMove(block.Position, this._tweening.tweeningTime).Play().AsyncWaitForCompletion();
            this.DestroyBlock(vector);
            await this._decreaseComponent.DecreaseAbove(vector);
        });
        // this._decreaseComponent.DecreaseAllAbove(vectors);
    }

    public void LookCombos(IBlock block, List<IBlock> blocks)
    {
        if (blocks.Count <= Constants.COMBO_NORMAL) return;
        else
        {
            if (blocks.Count > Constants.COMBO_HONEYPOT)
            {
                blocks.Remove(block);
                TileCombo tile = GameplayManagers.ComboManager.InstanceCombo(ComboTypes.HONEYPOT);
                GameplayManagers.SpawnManager.BlockNormalSpawner.ChangeTile(block, tile);
                this.FusionCellsTo(block, blocks);
            }
            else if (blocks.Count > Constants.COMBO_BEE_POLLEN)
            {
                blocks.Remove(block);
                TileCombo tile = GameplayManagers.ComboManager.InstanceCombo(ComboTypes.BOMB);
                GameplayManagers.SpawnManager.BlockNormalSpawner.ChangeTile(block, tile);
                this.FusionCellsTo(block, blocks);
            }
            else
            {
                // List<Vector3Int> vectors = new List<Vector3Int>();
                blocks.ForEach(async item =>
                {
                    Vector3Int vector = item.Position;
                    this.DestroyBlock(item.Position);
                    await this._decreaseComponent.DecreaseAbove(vector);
                });
                // this._decreaseComponent.DecreaseAllAbove(vectors);
            }
        }
    }

    public void Pop(IBlock block)
    {
        List<IBlock> horizontalConnections = block.GetConnections(AxisTypes.HORIZONTAL);
        List<IBlock> verticalConnections = block.GetConnections(AxisTypes.VERTICAL);
        if (horizontalConnections.Count > verticalConnections.Count)
            this.LookCombos(block, horizontalConnections);
        else
            this.LookCombos(block, verticalConnections);
    }

    public void Pop()
    {
        int width = this._gridComponent.Width;
        int height = this._gridComponent.Height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                IBlock block = this._gridComponent.GetAt(x, y);
                if (block == null) continue;
                if (!block.CanPop) continue;
                this.Pop(this._gridComponent.GetAt(x, y));
            }
        }
    }

    public void PopAllBlocks()
    {
        int width = this._gridComponent.Width;
        int height = this._gridComponent.Height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                IBlock block = this._gridComponent.GetAt(x, y);
                if (block == null) continue;
                if (!block.CanPop) continue;
                GameplayManagers.SpawnManager.BlockNormalSpawner.OnKill((BlockNormal)block);
                block = null;
            }
        }
    }

    public void PopAround(IBlock block)
    {
        List<IBlock> blocks = new List<IBlock> {
            block.Left != null ? block.Left.Top : null,
            block.Left,
            block.Left != null ? block.Left.Bottom : null,
            block.Top,
            block,
            block.Bottom,
            block.Right != null ? block.Right.Top : null,
            block.Right,
            block.Right != null ? block.Right.Bottom : null
        };

        blocks.ForEach(block =>
        {
            Vector3Int auxPosition = block.Position;
            this.DestroyBlock(block.Position);
            this._decreaseComponent.DecreaseAbove(block.Position);
        });


    }
}