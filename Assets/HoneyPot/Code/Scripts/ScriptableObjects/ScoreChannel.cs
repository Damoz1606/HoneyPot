using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreChannel", menuName = "HoneyPot/ScoreChannel", order = 0)]
public class ScoreChannel : ScriptableObject
{
    public UnityAction<int> ListenScore;

    public void TriggerScore(int value)
    {
        this.ListenScore?.Invoke(value);
    }
}