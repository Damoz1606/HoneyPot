using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameOverPopup : _PopupBase
{
    [SerializeField] private BorderUIAnimation _border;
    [SerializeField] private FadeAnimation _modal;
    [SerializeField] private FadeAnimation _uiBackground;
    [SerializeField] private float _animationTime;
    [SerializeField] Star[] _stars;
    public override void OnActivatePopup()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(OnActivatePopupCoroutine());
    }

    public override void OnDeactivatePopup()
    {
        StartCoroutine(OnDeactivatePopupCoroutine());
    }

    private IEnumerator OnActivatePopupCoroutine()
    {
        this._uiBackground.EnterAnimation();
        yield return new WaitForSecondsRealtime(this._animationTime);
        this._border.StartAnimation();
        yield return new WaitForSecondsRealtime(this._animationTime);
        this._modal.EnterAnimation();
        yield return new WaitForSecondsRealtime(this._animationTime);
        this.ActiveStars();
        yield break;
    }

    private IEnumerator OnDeactivatePopupCoroutine()
    {
        this._modal.ExitAnimation();
        yield return new WaitForSecondsRealtime(this._animationTime);
        this._border.EndAnimation();
        yield return new WaitForSecondsRealtime(this._animationTime);
        this._uiBackground.ExitAnimation();
        yield return new WaitForSecondsRealtime(this._animationTime);
        this.gameObject.SetActive(false);
        yield break;
    }

    private void ActiveStars()
    {
        StartCoroutine(ActiveStarsCoroutine());
    }
    private IEnumerator ActiveStarsCoroutine()
    {
        if (GameplayManagers.ScoreManager.ScoreReferences[0] <= GameplayManagers.ScoreManager.CurrentScore)
        {
            _stars[0].UpdateReference();
        }
        if (GameplayManagers.ScoreManager.ScoreReferences[1] <= GameplayManagers.ScoreManager.CurrentScore)
        {
            yield return new WaitForSecondsRealtime(0.25f);
            _stars[1].UpdateReference();
        }
        if (GameplayManagers.ScoreManager.ScoreReferences[2] <= GameplayManagers.ScoreManager.CurrentScore)
        {
            yield return new WaitForSecondsRealtime(0.25f);
            _stars[2].UpdateReference();
        }
        yield break;
    }
}