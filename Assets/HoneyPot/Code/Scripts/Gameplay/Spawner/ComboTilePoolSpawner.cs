using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTilePoolSpawner : _SpawnerBase
{
    [SerializeField] private GameObject _poolContainer;
    [SerializeField] private Pool _bombTilesPool;
    [SerializeField] private Pool _honeypotTilesPool;
    public ComboTile CurrentTile { private set; get; }
    public ComboTypes ComboType { set; get; }

    private void Start()
    {
        this.ComboType = ComboTypes.BOMB;
    }

    public override void Spawn()
    {
        switch (this.ComboType)
        {
            case ComboTypes.BOMB:
                this.CurrentTile = _bombTilesPool.GetPooledObject().GetComponent<ComboTile>();
                this.CurrentTile.OnActivate();
                break;
            case ComboTypes.HONEYPOT:
                this.CurrentTile = _honeypotTilesPool.GetPooledObject().GetComponent<ComboTile>();
                this.CurrentTile.OnActivate();
                break;
            default: break;
        }
        this.ComboType = ComboTypes.BOMB;
    }

    public void SetOnPool(GameObject _pool)
    {
        _pool.transform.SetParent(this._poolContainer.transform);
    }
}
