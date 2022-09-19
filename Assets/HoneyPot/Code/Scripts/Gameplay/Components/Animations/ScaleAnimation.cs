using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float _tweeningTime;
    [SerializeField] private Ease _tweeningEase;

    private void Start()
    {
        this.transform.localScale = Vector3.zero;
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
        await this.transform.GetComponent<RectTransform>().DOShakeScale(this._tweeningTime)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
    }

    private async Task ExitAnimationAsync()
    {
        await this.transform.GetComponent<RectTransform>().DOPunchScale(Vector3.zero, this._tweeningTime)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
    }
}
