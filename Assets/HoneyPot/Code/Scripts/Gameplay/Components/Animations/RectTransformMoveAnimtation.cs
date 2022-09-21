using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class RectTransformMoveAnimtation : _AnimationBase
{
    [SerializeField] private SwipeTypes _moveDirection;
    [SerializeField] private float _distance;

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
        switch (this._moveDirection)
        {
            case SwipeTypes.UP:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosY(this.transform.GetComponent<RectTransform>().anchoredPosition.y + this._distance, this._delayTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.DOWN:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosY(this.transform.GetComponent<RectTransform>().anchoredPosition.y - this._distance, this._delayTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.LEFT:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosX(this.transform.GetComponent<RectTransform>().anchoredPosition.x - this._distance, this._delayTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.RIGHT:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosX(this.transform.GetComponent<RectTransform>().anchoredPosition.x + this._distance, this._delayTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            default:
                break;
        }
    }

    private async Task EndAnimationAsync()
    {
        switch (this._moveDirection)
        {
            case SwipeTypes.UP:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosY(this.transform.GetComponent<RectTransform>().anchoredPosition.y - this._distance, this._delayTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.DOWN:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosY(this.transform.GetComponent<RectTransform>().anchoredPosition.y + this._distance, this._delayTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.LEFT:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosX(this.transform.GetComponent<RectTransform>().anchoredPosition.x + this._distance, this._delayTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.RIGHT:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosX(this.transform.GetComponent<RectTransform>().anchoredPosition.x - this._distance, this._delayTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            default:
                break;
        }
    }
}