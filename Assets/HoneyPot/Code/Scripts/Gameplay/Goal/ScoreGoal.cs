using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGoal : _GoalBase
{
    [SerializeField] private ScoreGoalUIController _controller;

    private void Start()
    {
        Debug.Log(this._required);
        this._controller.Initialize((int)this._required);
    }

    public override void Complete()
    {
        _controller.Completed();
    }

    public override void DrawHUD()
    {
        _controller.UpdateHUD(0);
        // throw new System.NotImplementedException();
    }

    public override bool IsAchived()
    {
        return GameplayManagers.ScoreManager.CurrentScore >= this._required;
    }
}
