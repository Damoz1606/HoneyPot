using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class TimeGoal : _GoalBase
{
    private Timer _timer;

    private void Awake() => this._timer = GetComponent<Timer>();

    private void Start() => this._timer.TargetTime = this._required;

    public override void Complete()
    {

    }

    public override void DrawHUD()
    {
        // throw new System.NotImplementedException();
    }

    public override bool IsAchived()
    {
        return this._required >= 0;
    }
}
