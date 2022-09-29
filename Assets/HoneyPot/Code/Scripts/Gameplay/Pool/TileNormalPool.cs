using UnityEngine;

public class TileNormalPool : APoolFactory<TileNormal>
{
    public override void OnKill(TileNormal shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override TileNormal OnSpawn()
    {
        return (this._usePool) ?
        this.Pool.Get() : this.OnCreate();
    }
}