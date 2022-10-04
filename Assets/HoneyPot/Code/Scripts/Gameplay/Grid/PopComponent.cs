using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(GridComponent))]
public class PopComponent : MonoBehaviour, IFusion<IBlock>, IPop<IBlock>, IComboSearch<IBlock>
{
    [SerializeField] private TweeningModel _tweening;

    [SerializeField] private ChannelTile _challengeTile;

    private GridComponent _gridComponent;

    private void Awake()
    {
        this._gridComponent = this.GetComponent<GridComponent>();
    }

    private void OnDisable()
    {
        this._challengeTile.ListenBomb -= this.PopAround;
        this._challengeTile.ListenHoneypot -= this.PopAllBlocks;
    }

    private void OnEnable()
    {
        this._challengeTile.ListenBomb += this.PopAround;
        this._challengeTile.ListenHoneypot += this.PopAllBlocks;
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

    public void FusionCellsTo(IBlock block, List<IBlock> blocks)
    {
        List<Vector3Int> vectors = new List<Vector3Int>();
        blocks.ForEach(item =>
        {
            Vector3Int vector = item.Position;
            item.MoveTo(block.Position);
            this._gridComponent.RemoveAt(vector.x, vector.y);
            this._gridComponent.DecreaseAt(vector.x, vector.y + 1);
        });
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
                foreach (IBlock item in blocks)
                {
                    if (!item.CanPop) return;
                    if (!item.CanDecrease) return;
                    item.OnEffect();
                    this._gridComponent.RemoveAt(item.Position.x, item.Position.y);
                    this._gridComponent.DecreaseAt(item.Position.x, item.Position.y + 1);
                };
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
                this.Pop(block);
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
                this._gridComponent.RemoveAt(x, y);
            }
        }
    }

    public void PopAround(IBlock block)
    {
        List<IBlock> blocks = new List<IBlock> {
            (block.Left != null) ? block.Left.Top : null,
            block.Left,
            (block.Left != null) ? block.Left.Bottom : null,
            block.Top,
            block.Bottom,
            (block.Right != null) ? block.Right.Top : null,
            block.Right,
            (block.Right != null) ? block.Right.Bottom : null,
        };

        foreach (var item in blocks)
        {
            if (item == null) continue;
            this._gridComponent.RemoveAt(item.Position.x, item.Position.y);
            this._gridComponent.DecreaseAt(item.Position.x, item.Position.y + 1);
        }
    }
}