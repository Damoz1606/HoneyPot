using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : _SpawnerBase<Tile>
{
    public override void OnKill(Tile shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnKill(shape);
    }

    public override Tile OnSpawn()
    {
        return (this._usePool) ? this.Pool.Get() : this.OnCreate();
    }

    protected override Tile OnCreate()
    {
        return Instantiate(this._objectPrefab, Vector3.zero, Quaternion.identity);
    }

    protected override void OnGet(Tile shape)
    {
        shape.OnActivate();
        shape.gameObject.SetActive(true);
    }

    protected override void OnReleased(Tile shape)
    {
        shape.OnDeactivate();
        shape.transform.SetParent(this._poolContainer);
        shape.gameObject.SetActive(false);
    }

    protected override void OnRemove(Tile shape)
    {
        Destroy(shape.gameObject);
    }
}
