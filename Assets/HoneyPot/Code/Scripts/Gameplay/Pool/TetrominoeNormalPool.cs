using UnityEngine;

public class TetrominoeNormalPool : APoolFactory<TetrominoeNormal>
{
    public override void OnKill(TetrominoeNormal shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override TetrominoeNormal OnSpawn()
    {
        return (this._usePool) ?
        this.Pool.Get() : this.OnCreate();
    }
}