using UnityEngine;

[System.Serializable]
public abstract class _GoalBase : MonoBehaviour
{
    [SerializeField] protected float _required = 0;
    public abstract void SetHUD(_HUDBase hud);
    public abstract bool IsAchived();
    public abstract void Complete();
    public abstract void DrawHUD();
}