using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private List<_GoalBase> _goals;
    private bool _goalsHasBeenCompleted = false;

    private void OnGUI() => this._goals.ForEach(goal => goal.DrawHUD());

    public void CheckCompletedGoals()
    {
        this._goalsHasBeenCompleted = true;
        this._goals.ForEach(goal =>
        {
            this._goalsHasBeenCompleted = goal.IsAchived() && this._goalsHasBeenCompleted;
            if (goal.IsAchived())
            {
                goal.Complete();
                Destroy(goal);
            }
        });
        this.EndGame();
    }

    private void EndGame()
    {
        if (this._goalsHasBeenCompleted)
        {
            GameplayManagers.GameManager.SetState(GameStates.GAMEOVER);
        }
    }
}
