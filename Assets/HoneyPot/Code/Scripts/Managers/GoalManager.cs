using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private List<ChallengeCollect> _challengeCollect;
    [SerializeField] private ChallengeScore _challengeScore;
    [SerializeField] private ChallengeChannel _challengeChannel;

    private bool _challengeHasBeenCompleted = false;

    private void OnEnable()
    {
        this._challengeChannel.OnChallengeChangeListener += this.CheckCompletedGoals;
    }

    private void OnDisable()
    {
        this._challengeChannel.OnChallengeChangeListener -= this.CheckCompletedGoals;
    }

    private void OnGUI()
    {
        if (this._challengeCollect.Count > 0)
            this._challengeCollect.ForEach(challenge => challenge.DrawHUD());
    }

    public void CheckCompletedGoals()
    {
        if (this._challengeCollect.Count > 0)
        {
            this._challengeHasBeenCompleted = true;
            this._challengeCollect.ForEach(challenge =>
            {
                this._challengeHasBeenCompleted = challenge.IsAchived() && this._challengeHasBeenCompleted;
                if (challenge.IsAchived())
                {
                    challenge.OnComplete();
                    // Destroy(challenge);
                }
            });
        }
        if (this._challengeScore != null)
        {
            if (_challengeScore.IsAchived())
            {
                _challengeScore.OnComplete();
            }
        }
        // this.EndGame();
    }

    private void EndGame()
    {
        if (this._challengeHasBeenCompleted)
            GameplayManagers.GameManager.SetState(GameStates.GAMEOVER);
    }
}
