using UnityEngine;

public abstract class _PoolObjectBase : MonoBehaviour
{
    public abstract void OnActivate();
    public abstract void OnUpdate();
    public abstract void OnDeactivate();
}