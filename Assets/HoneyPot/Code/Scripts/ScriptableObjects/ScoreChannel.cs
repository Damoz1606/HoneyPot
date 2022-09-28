using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreChannel", menuName = "HoneyPot/ScoreChannel", order = 0)]
public class ScoreChannel : ScriptableObject
{
    public UnityAction<int> OnScoreIncreaseListener;

    public void OnScoreIncreaseTrigger(int value)
    {
        this.OnScoreIncreaseListener?.Invoke(value);
    }
}