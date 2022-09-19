using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] private float _tweeningTime;
    [SerializeField] private Ease _tweeningEase;

    private void Start()
    {
       /*  Image image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0;
        image.color = tempColor; */
    }

    public async void EnterAnimation()
    {
        await this.EnterAnimationAsync();
    }

    public async void ExitAnimation()
    {
        await this.ExitAnimationAsync();
    }

    private async Task EnterAnimationAsync()
    {
        await this.transform.GetComponent<Image>().DOFade(1f, this._tweeningTime)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
    }

    private async Task ExitAnimationAsync()
    {
        await this.transform.GetComponent<Image>().DOFade(0.01f, this._tweeningTime)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
    }
}
