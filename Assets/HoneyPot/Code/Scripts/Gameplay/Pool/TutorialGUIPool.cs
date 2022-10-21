using UnityEngine;

public class TutorialGUIPool : APoolFactory<TutorialGUI>
{
    [SerializeField] private GameObject gridAllocation;

    public override void OnReleased(TutorialGUI shape)
    {
        shape.transform.SetParent(this._poolContainer);
        shape.gameObject.SetActive(false);
    }

    public override void OnGet(TutorialGUI shape)
    {
        shape.gameObject.SetActive(true);
    }

    public override void OnKill(TutorialGUI shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override TutorialGUI OnSpawn()
    {
        TutorialGUI shape = (this._usePool) ?
        this.Pool.Get() : this.OnCreate();
        shape.transform.SetParent(this.gridAllocation.transform);
        return shape;
    }
}