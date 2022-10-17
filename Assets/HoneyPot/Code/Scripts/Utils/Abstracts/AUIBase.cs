using UnityEngine;

public abstract class AUIBase : MonoBehaviour, IUI
{
    public abstract void EndUI();
    public abstract void StartUI();
}