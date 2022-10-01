using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "OnCollect", menuName = "Events/OnCollect", order = 0)]
public class OnCollect : ScriptableObject
{
    public UnityAction<ITile> ListenCollection;
    public void OnActivate()
    {
        this.ListenCollection?.Invoke(default);
    }
}