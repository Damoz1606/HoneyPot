using UnityEngine;

public class TileComboPool : APoolFactory<TileCombo>
{
    public override void OnKill(TileCombo shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override TileCombo OnSpawn()
    {
        return (this._usePool) ?
        this.Pool.Get() : this.OnCreate();
    }
}