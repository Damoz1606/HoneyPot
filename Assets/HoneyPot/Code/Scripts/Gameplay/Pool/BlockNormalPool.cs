using System.Collections.Generic;
using UnityEngine;

public class BlockNormalPool : APoolFactory<BlockNormal>
{
    [SerializeField] private List<KeyPair<TileTypes, TileNormalPool>> _tileNormalPools;
    [SerializeField] private List<KeyPair<ComboTypes, TileComboPool>> _tileComboPools;
    private Dictionary<TileTypes, TileNormalPool> _tileNormalPoolDictionary = new Dictionary<TileTypes, TileNormalPool>();
    private Dictionary<ComboTypes, TileComboPool> _tileComboPoolDictionary = new Dictionary<ComboTypes, TileComboPool>();

    public Dictionary<TileTypes, TileNormalPool> TileNormalPoolDictionary => this._tileNormalPoolDictionary;
    public Dictionary<ComboTypes, TileComboPool> TileComboPoolDictionary => this._tileComboPoolDictionary;

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
        if (block.tile.type.Equals(TileTypes.COMBO))
            this._tileComboPoolDictionary[((TileCombo)block.tile).comboType].OnKill((TileCombo)block.tile);
        else
            this._tileNormalPoolDictionary[((TileNormal)block.tile).type].OnKill((TileNormal)block.tile);
        block.DeattachTile();
        block.AttachTile(tile);
    }

    private TileTypes GetType(int index)
    {
        switch (index)
        {
            case 0:
                return TileTypes.FLOWER_1;
            case 1:
                return TileTypes.FLOWER_2;
            case 2:
                return TileTypes.FLOWER_3;
            case 3:
                return TileTypes.FLOWER_4;
            default:
                return TileTypes.FLOWER_1;
        }
    }

    public override void OnKill(BlockNormal shape)
    {
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