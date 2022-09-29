using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreChannel", menuName = "HoneyPot/ScoreChannel", order = 0)]
public class ScoreChannel : ScriptableObject
{
    public UnityAction<int> OnScoreIncreaseListener;
    public UnityAction<int> ListenScore;

    public void OnScoreIncreaseTrigger(int value)
    {
        this.OnScoreIncreaseListener?.Invoke(value);
    }

    public void TriggerScore(int value)
    {
        this.ListenScore?.Invoke(value);
    }
}