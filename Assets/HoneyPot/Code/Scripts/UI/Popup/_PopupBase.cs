using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public abstract class _PopupBase : MonoBehaviour
{
    protected AnimationController _animationController;
    public abstract void OnActivatePopup();
    public abstract void OnDeactivatePopup();

}