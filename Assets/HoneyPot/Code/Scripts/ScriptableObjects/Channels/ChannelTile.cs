using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChannelTile", menuName = "Channel/ChannelTile", order = 0)]
public class ChannelTile : ScriptableObject
{
    public UnityAction ListenHoneypot;
    public UnityAction<IBlock> ListenBomb;
    public UnityAction<Vector3Int> ListenTile;

    public void TriggerHoneypot()
    {
        this.ListenHoneypot?.Invoke();
    }

    public void TriggerBomb(IBlock block)
    {
        this.ListenBomb?.Invoke(block);
    }

    public void TriggerTile(Vector3Int position)
    {
        this.ListenTile?.Invoke(position);
    }
}