using DG.Tweening;
using UnityEngine;

public abstract class _AnimationBase : MonoBehaviour
{
    [SerializeField] protected float _delayTime = 0;
    [SerializeField] protected Ease _tweeningEase = Ease.InSine;
    public abstract void StartAnimation();
    public abstract void EndAnimation();
}