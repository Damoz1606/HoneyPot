using UnityEngine;

public class ChallengeScore : MonoBehaviour, IChallenge
{
    [SerializeField] private int _require;
    [SerializeField] private int _current;

    [SerializeField] private ScoreChannel _scoreChannel;
    [SerializeField] private ChallengeChannel _challengeChannel;

    private void OnEnable()
    {
        this._scoreChannel.OnScoreIncreaseListener += this.OnEventTrigger;
    }

    private void OnDisable()
    {
        this._scoreChannel.OnScoreIncreaseListener -= this.OnEventTrigger;
    }

    public void DrawHUD()
    {

    }

    public bool IsAchived()
    {
        return this._require <= this._current;
    }

    public void OnComplete()
    {
        Debug.Log($"Score Complete!!!");
    }

    public void OnEventTrigger(int increase)
    {
        this._current += increase;
        if (this._challengeChannel != null) this._challengeChannel.OnChallengeChangeTrigger();
    }
}