using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChallengeCollectChannel", menuName = "HoneyPot/ChallengeCollectChannel", order = 0)]
public class ChallengeCollectChannel : ScriptableObject
{
    public UnityAction<Tile> OnTileCollectListener;

    public void OnTileCollectTrigger(Tile value)
    {
        this.OnTileCollectListener?.Invoke(value);
    }
}