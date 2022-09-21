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
        this._animationController.StartAnimation();
        GameplayManagers.AudioManager.PlayUI(GameplayManagers.AudioManager.UIPause);
    }

    public override void OnDeactivatePopup()
    {
        this.StartCoroutine(OnDeactivatePopupCoroutine());
        GameplayManagers.AudioManager.PlayUI(GameplayManagers.AudioManager.UIPause);
    }

    private IEnumerator OnDeactivatePopupCoroutine()
    {
        this._animationController.EndAnimation();
        yield return new WaitForSecondsRealtime(0.5f);
        this.gameObject.SetActive(false);
        yield break;
    }
}