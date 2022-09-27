using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class BlockSpawner : _SpawnerBase<Block>
{
    [SerializeField] private List<KeyPair<TileTypes, TileSpawner>> _tileSpawnerKeyPair;
    [SerializeField] private List<KeyPair<ComboTypes, TileSpawner>> _comboSpawnerKeyPair;
    private Dictionary<TileTypes, TileSpawner> _tileSpawnerDictionary = new Dictionary<TileTypes, TileSpawner>();
    private Dictionary<ComboTypes, TileSpawner> _comboSpawnerDictionary = new Dictionary<ComboTypes, TileSpawner>();

    private void Awake()
    {
        this._tileSpawnerKeyPair.ForEach(item => _tileSpawnerDictionary.Add(item.key, item.value));
        this._comboSpawnerKeyPair.ForEach(item => _comboSpawnerDictionary.Add(item.key, item.value));
    }

    public void ReattachTile(Block shape, Tile tile)
    {
        _tileSpawnerDictionary[shape.Child.Type].OnKill(shape.Child);
        shape.DeattachChild();
        shape.AttachChild(tile);
    }

    public TileSpawner GetSpawner(int index)
    {
        switch (index)
        {
            case 0:
                return this._tileSpawnerDictionary[TileTypes.FLOWER_1];
            case 1:
                return this._tileSpawnerDictionary[TileTypes.FLOWER_2];
            case 2:
                return this._tileSpawnerDictionary[TileTypes.FLOWER_3];
            case 3:
                return this._tileSpawnerDictionary[TileTypes.FLOWER_4];
            default:
                return null;
        }
    }

    public TileSpawner GetSpawner(ComboTypes type)
    {
        switch (type)
        {
            case ComboTypes.BOMB:
                return this._comboSpawnerDictionary[ComboTypes.BOMB];
            case ComboTypes.HONEYPOT:
                return this._comboSpawnerDictionary[ComboTypes.HONEYPOT];
            default:
                return null;
        }
    }

    public override Block OnSpawn()
    {
        int randomIndex = UnityEngine.Random.Range(0, this._tileSpawnerDictionary.Count);
        Block block = (this._usePool) ? this.Pool.Get() : this.OnCreate();
        Tile tile = this.GetSpawner(randomIndex).OnSpawn();
        block.AttachChild(tile);
        return block;
    }

    public override void OnKill(Block shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    protected override Block OnCreate()
    {
        return Instantiate(this._objectPrefab, Vector3.zero, Quaternion.identity);
    }

    protected override void OnGet(Block shape)
    {
        shape.OnActivate();
        shape.gameObject.SetActive(true);
    }

    protected override void OnReleased(Block shape)
    {
        shape.OnDeactivate();
        if (shape.Child.Type == TileTypes.COMBO)
            this._comboSpawnerDictionary[((ComboTile)shape.Child).ComboType].OnKill(shape.Child);
        else
            this._tileSpawnerDictionary[shape.Child.Type].OnKill(shape.Child);
        shape.DeattachChild();
        shape.transform.SetParent(this._poolContainer);
        shape.gameObject.SetActive(false);
    }

    protected override void OnRemove(Block shape)
    {
        Destroy(shape.gameObject);
    }
}