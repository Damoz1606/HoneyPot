using System.Collections;
using UnityEngine;

public class PausePopup : _PopupBase
{
    [SerializeField] private BorderUIAnimation _border;
    [SerializeField] private FadeAnimation _modal;
    [SerializeField] private FadeAnimation _uiBackground;
    [SerializeField] private float _animationTime;
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
}