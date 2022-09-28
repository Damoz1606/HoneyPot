using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChallengeChannel", menuName = "HoneyPot/ChallengeChannel", order = 0)]
public class ChallengeChannel : ScriptableObject
{
    public UnityAction OnChallengeChangeListener;
    public void OnChallengeChangeTrigger()
    {
        this.OnChallengeChangeListener?.Invoke();
    }
}