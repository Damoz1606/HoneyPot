using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTilePoolSpawner : _SpawnerBase
{
    [SerializeField] private GameObject _poolContainer;
    [SerializeField] private List<Pool> _normalTilesPool;
    public NormalTile CurrentTile { private set; get; }

    public override void Spawn()
    {
        int randomIndex = Random.Range(0, this._normalTilesPool.Count);
        this.CurrentTile = _normalTilesPool[randomIndex].GetPooledObject().GetComponent<NormalTile>();
        this.CurrentTile.OnActivate();
    }

    public void SetOnPool(GameObject _pool)
    {
        _pool.transform.SetParent(this._poolContainer.transform);
    }

}
