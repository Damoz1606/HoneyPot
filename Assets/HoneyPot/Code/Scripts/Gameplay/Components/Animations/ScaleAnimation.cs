using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : _AnimationBase
{

    private void Start()
    {
        this.transform.localScale = Vector3.zero;
    }

    public override async void StartAnimation()
    {
        await this.StartAnimationAsync();
    }

    public override async void EndAnimation()
    {
        await this.EndAnimationAsync();
    }

    private async Task StartAnimationAsync()
    {
        await this.transform.GetComponent<RectTransform>().DOScale(Vector3.one, this._delayTime)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
    }

    private async Task EndAnimationAsync()
    {
        await this.transform.GetComponent<RectTransform>().DOScale(Vector3.zero, this._delayTime)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
    }
}
