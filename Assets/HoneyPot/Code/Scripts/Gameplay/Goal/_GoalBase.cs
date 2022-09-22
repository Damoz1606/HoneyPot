using UnityEngine;

public abstract class _GoalBase : MonoBehaviour
{
    [SerializeField] protected float _required = 0;
    public abstract bool IsAchived();
    public abstract void Complete();
    public abstract void DrawHUD();
}