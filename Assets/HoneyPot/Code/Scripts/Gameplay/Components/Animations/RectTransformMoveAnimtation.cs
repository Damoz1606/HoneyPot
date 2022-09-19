using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class RectTransformMoveAnimtation : MonoBehaviour
{
    [SerializeField] private SwipeTypes _moveDirection;
    [SerializeField] private float _distance;
    [SerializeField] private float _tweeningTime;
    [SerializeField] private Ease _tweeningEase;

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
        switch (this._moveDirection)
        {
            case SwipeTypes.UP:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosY(this.transform.GetComponent<RectTransform>().anchoredPosition.y + this._distance, this._tweeningTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.DOWN:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosY(this.transform.GetComponent<RectTransform>().anchoredPosition.y - this._distance, this._tweeningTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.LEFT:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosX(this.transform.GetComponent<RectTransform>().anchoredPosition.x - this._distance, this._tweeningTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.RIGHT:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosX(this.transform.GetComponent<RectTransform>().anchoredPosition.x + this._distance, this._tweeningTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            default:
                break;
        }
    }

    private async Task ExitAnimationAsync()
    {
        switch (this._moveDirection)
        {
            case SwipeTypes.UP:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosY(this.transform.GetComponent<RectTransform>().anchoredPosition.y - this._distance, this._tweeningTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.DOWN:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosY(this.transform.GetComponent<RectTransform>().anchoredPosition.y + this._distance, this._tweeningTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.LEFT:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosX(this.transform.GetComponent<RectTransform>().anchoredPosition.x + this._distance, this._tweeningTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            case SwipeTypes.RIGHT:
                await this.transform.GetComponent<RectTransform>().DOAnchorPosX(this.transform.GetComponent<RectTransform>().anchoredPosition.x - this._distance, this._tweeningTime, true)
                .SetEase(this._tweeningEase)
                .SetUpdate(true)
                .Play().AsyncWaitForCompletion();
                break;
            default:
                break;
        }
    }
}