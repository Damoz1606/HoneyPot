using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPoolSpawner : _SpawnerBase
{
    [SerializeField] private GameObject _poolContainer;
    [SerializeField] private Pool _blockPool;
    public Block CurrentBlock { private set; get; }

    public override void Spawn()
    {
        this.CurrentBlock = _blockPool.GetPooledObject().GetComponent<Block>();
        this.CurrentBlock.OnActivate();
    }

    public void SetOnPool(GameObject _pool)
    {
        _pool.transform.SetParent(this._poolContainer.transform);
    }
}
