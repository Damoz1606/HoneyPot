using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TileChannel", menuName = "HoneyPot/TileChannel", order = 0)]
public class TileChannel : ScriptableObject
{
    public UnityAction OnDestroyListener;
    public UnityAction OnSwapListener;

    public void OnDestroyTrigger()
    {
        this.OnDestroyListener?.Invoke();
    }
    public void OnSwapTrigger()
    {
        this.OnSwapListener?.Invoke();
    }
}