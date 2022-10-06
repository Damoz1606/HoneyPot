using UnityEngine;

public class ParticlePool : APoolFactory<Particle>
{
    public override void OnGet(Particle shape)
    {
        shape.gameObject.SetActive(true);
    }

    public override void OnReleased(Particle shape)
    {
        shape.transform.SetParent(this._poolContainer);
        shape.gameObject.SetActive(false);
    }

    public override void OnKill(Particle shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override Particle OnSpawn()
    {
        return (this._usePool) ?
        this.Pool.Get() : this.OnCreate();
    }
}