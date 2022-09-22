using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGoal : _GoalBase
{
    public override void Complete()
    {
        
    }

    public override void DrawHUD()
    {
        // throw new System.NotImplementedException();
    }

    public override bool IsAchived()
    {
        return GameplayManagers.ScoreManager.CurrentScore >= this._required;
    }
}
