using UnityEngine;

public class CraftGoalGUIPool : APoolFactory<CraftGUI>
{
    [SerializeField] private GameObject gridAllocation;

    public override void OnReleased(CraftGUI shape)
    {
        shape.transform.SetParent(this._poolContainer);
        shape.gameObject.SetActive(false);
    }

    public override void OnGet(CraftGUI shape)
    {
        shape.gameObject.SetActive(true);
    }

    public override void OnKill(CraftGUI shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override CraftGUI OnSpawn()
    {
        CraftGUI shape = (this._usePool) ?
        this.Pool.Get() : this.OnCreate();
        shape.transform.SetParent(this.gridAllocation.transform);
        return shape;
    }
}