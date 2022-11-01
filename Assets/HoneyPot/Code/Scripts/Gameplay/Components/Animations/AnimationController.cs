using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationController : _AnimationBase
{
    [SerializeField] private _AnimationBase[] _animations;
    [SerializeField] private bool _endAnimation = false;

    public override void StartAnimation()
    {
        if (this._endAnimation)
            StartCoroutine(this.EndAnimationCorountine());
        else
            StartCoroutine(this.StartAnimationCorountine());
    }

    public override void EndAnimation()
    {
        if (this._endAnimation)
            StartCoroutine(this.StartAnimationCorountine());
        else
            StartCoroutine(this.EndAnimationCorountine());
    }

    private IEnumerator StartAnimationCorountine()
    {
        foreach (_AnimationBase animation in _animations)
        {
            yield return new WaitForSecondsRealtime(this._delayTime);
            animation.StartAnimation();
        }
    }

    private IEnumerator EndAnimationCorountine()
    {
        foreach (_AnimationBase animation in _animations)
        {
            animation.EndAnimation();
            yield return new WaitForSecondsRealtime(this._delayTime);
        }
    }
}
