using UnityEngine;

public abstract class _HUDBase : MonoBehaviour
{
    public abstract void OnActiveHUD();
    public abstract void OnUpdateHUD();
    public abstract void OnDeactiveHUD();
}