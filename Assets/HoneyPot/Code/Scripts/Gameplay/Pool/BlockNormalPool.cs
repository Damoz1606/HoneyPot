using System.Collections.Generic;
using UnityEngine;

public class BlockNormalPool : APoolFactory<BlockNormal>
{
    [SerializeField] private List<KeyPair<TileNormalType, TileNormalPool>> _tileNormalPools;
    [SerializeField] private List<KeyPair<TileComboType, TileComboPool>> _tileComboPools;
    private Dictionary<TileNormalType, TileNormalPool> _tileNormalPoolDictionary = new Dictionary<TileNormalType, TileNormalPool>();
    private Dictionary<TileComboType, TileComboPool> _tileComboPoolDictionary = new Dictionary<TileComboType, TileComboPool>();

    public Dictionary<TileNormalType, TileNormalPool> TileNormalPoolDictionary => this._tileNormalPoolDictionary;
    public Dictionary<TileComboType, TileComboPool> TileComboPoolDictionary => this._tileComboPoolDictionary;

    public override bool UsePool
    {
        set
        {
            this._usePool = value;
            this._tileNormalPools.ForEach(item => item.value.UsePool = value);
            this._tileComboPools.ForEach(item => item.value.UsePool = value);
        }
        get { return this._usePool; }
    }

    private void Awake()
    {
        this._tileNormalPools.ForEach(item => _tileNormalPoolDictionary.Add(item.key, item.value));
        this._tileComboPools.ForEach(item => _tileComboPoolDictionary.Add(item.key, item.value));
    }

    public void ChangeTile(IBlock block, ITile tile)
    {
        if (block.tile.Equals(TileNormalType.COMBO))
            this._tileComboPoolDictionary[((TileCombo)block.tile).comboType].OnKill((TileCombo)block.tile);
        else
            this._tileNormalPoolDictionary[((TileNormal)block.tile).type].OnKill((TileNormal)block.tile);
        block.DeattachTile();
        block.AttachTile(tile);
    }

    private TileNormalType GetType(int index)
    {
        switch (index)
        {
            case 0:
                return TileNormalType.FLOWER_1;
            case 1:
                return TileNormalType.FLOWER_2;
            case 2:
                return TileNormalType.FLOWER_3;
            case 3:
                return TileNormalType.FLOWER_4;
            default:
                return TileNormalType.FLOWER_1;
        }
    }

    public override void OnKill(BlockNormal shape)
    {
        ITile tile = shape.tile;
        shape.DeattachTile();
        if (tile.type.Equals(TileNormalType.COMBO))
            this._tileComboPoolDictionary[((TileCombo)tile).comboType].OnKill(((TileCombo)tile));
        else
            this._tileNormalPoolDictionary[((TileNormal)tile).type].OnKill(((TileNormal)tile));
        shape.transform.SetParent(this._poolContainer);
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override BlockNormal OnSpawn()
    {
        int randomIndex = UnityEngine.Random.Range(0, this._tileNormalPoolDictionary.Count);
        BlockNormal shape = (this._usePool) ? this.Pool.Get() : this.OnCreate();
        ITile tile = this._tileNormalPoolDictionary[this.GetType(randomIndex)].OnSpawn();
        shape.AttachTile(tile);
        return shape;
    }
}