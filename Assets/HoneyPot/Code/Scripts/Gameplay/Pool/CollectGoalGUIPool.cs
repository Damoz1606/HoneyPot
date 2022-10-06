using UnityEngine;

public class CollectGoalGUIPool : APoolFactory<CollectUI>
{
    [SerializeField] private GameObject gridAllocation;

    public override void OnReleased(CollectUI shape)
    {
        shape.transform.SetParent(this._poolContainer);
        shape.gameObject.SetActive(false);
    }

    public override void OnGet(CollectUI shape)
    {
        shape.gameObject.SetActive(true);
    }

    public override void OnKill(CollectUI shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override CollectUI OnSpawn()
    {
        CollectUI shape = (this._usePool) ?
        this.Pool.Get() : this.OnCreate();
        shape.transform.SetParent(this.gridAllocation.transform);
        return shape;
    }
}