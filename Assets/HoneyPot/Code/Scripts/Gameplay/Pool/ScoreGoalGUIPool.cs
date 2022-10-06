using UnityEngine;

public class ScoreGoalGUIPool : APoolFactory<ScoreGUI>
{
    [SerializeField] private GameObject verticalContainer;

    public override void OnReleased(ScoreGUI shape)
    {
        shape.transform.SetParent(this._poolContainer);
        shape.gameObject.SetActive(false);
    }

    public override void OnGet(ScoreGUI shape)
    {
        shape.gameObject.SetActive(true);
    }

    public override void OnKill(ScoreGUI shape)
    {
        if (this._usePool) this.Pool.Release(shape);
        else this.OnRemove(shape);
    }

    public override ScoreGUI OnSpawn()
    {
        ScoreGUI shape = (this._usePool) ?
        this.Pool.Get() : this.OnCreate();
        shape.transform.SetParent(this.verticalContainer.transform);
        return shape;
    }
}