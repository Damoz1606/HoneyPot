using System.Collections;
using UnityEngine;

public class PausePopup : _PopupBase
{
    private void Awake()
    {
        this._animationController = GetComponent<AnimationController>();
    }

    public override void OnActivatePopup()
    {
        this.gameObject.SetActive(true);
        GameplayManagers.AudioManager.PlayPopupOpen();
        this._animationController.StartAnimation();
    }

    public override void OnDeactivatePopup()
    {
        this.StartCoroutine(OnDeactivatePopupCoroutine());
    }

    private IEnumerator OnDeactivatePopupCoroutine()
    {
        GameplayManagers.AudioManager.PlayPopupClose();
        this._animationController.EndAnimation();
        yield return new WaitForSecondsRealtime(0.5f);
        this.gameObject.SetActive(false);
        yield break;
    }
}