using UnityEngine;

public class TetrominoeSpawner : _SpawnerBase<Tetrominoe>
{
    public override void OnKill(Tetrominoe shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override Tetrominoe OnSpawn()
    {
        if (this._usePool)
        {
            return this.Pool.Get();
        }
        else
        {
            Tetrominoe shape = this.OnCreate();
            shape.OnActivate();
            return shape;
        }
    }

    protected override Tetrominoe OnCreate()
    {
        return Instantiate(this._objectPrefab, Vector3.zero, Quaternion.identity);
    }

    protected override void OnGet(Tetrominoe shape)
    {
        shape.OnActivate();
        shape.gameObject.SetActive(true);
    }

    protected override void OnReleased(Tetrominoe shape)
    {
        shape.OnDeactivate();
        shape.transform.SetParent(this._poolContainer);
        shape.gameObject.SetActive(false);
    }

    protected override void OnRemove(Tetrominoe shape)
    {
        Destroy(shape.gameObject);
    }
}