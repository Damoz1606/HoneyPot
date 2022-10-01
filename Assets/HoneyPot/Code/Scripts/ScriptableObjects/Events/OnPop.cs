using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "OnPop", menuName = "Events/OnPopSingle", order = 0)]
public class OnPop : ScriptableObject
{
    public UnityAction<Vector3Int> ListenPop;

    public void OnActivate()
    {
        this.ListenPop?.Invoke(Vector3Int.zero);
    }
}