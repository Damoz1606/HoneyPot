using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChallengeCollectChannel", menuName = "HoneyPot/ChallengeCollectChannel", order = 0)]
public class ChallengeCollectChannel : ScriptableObject
{
    public UnityAction<ITile> ListenCollectTile;

    public void TriggerCollectTile(ITile value)
    {
        this.ListenCollectTile?.Invoke(value);
    }
}