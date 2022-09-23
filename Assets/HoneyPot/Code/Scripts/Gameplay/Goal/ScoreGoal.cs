using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScoreHUD))]
public class ScoreGoal : _GoalBase
{
    private ScoreHUD _hud;
    private void Start()
    {
        this.SetHUD(this.GetComponent<ScoreHUD>());
    }

    public override void Complete()
    {
        _hud.OnDeactiveHUD();
    }

    public override void DrawHUD()
    {
        _hud.OnUpdateHUD();
        // throw new System.NotImplementedException();
    }

    public override bool IsAchived()
    {
        return GameplayManagers.ScoreManager.CurrentScore >= this._required;
    }

    public override void SetHUD(_HUDBase hud)
    {
        this._hud = (ScoreHUD)hud;
        this._hud.Initialize((int)this._required);
    }
}
