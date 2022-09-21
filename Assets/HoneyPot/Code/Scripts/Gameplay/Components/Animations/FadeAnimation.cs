using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : _AnimationBase
{

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
        await this.transform.GetComponent<Image>().DOFade(1f, this._delayTime)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
    }

    private async Task EndAnimationAsync()
    {
        await this.transform.GetComponent<Image>().DOFade(0.01f, this._delayTime)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
    }
}
