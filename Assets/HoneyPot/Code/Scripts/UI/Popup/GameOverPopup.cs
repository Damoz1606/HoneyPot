using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameOverPopup : _PopupBase
{
    private void Awake()
    {
        this._animationController = GetComponent<AnimationController>();
    }

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
        yield return new WaitForSecondsRealtime(1f);
        GameplayManagers.AudioManager.PlayPopupOpen();
        this._animationController.StartAnimation();
        yield return new WaitForSecondsRealtime(1.25f);
        this.ActiveStars();
        yield break;
    }

    private IEnumerator OnDeactivatePopupCoroutine()
    {
        GameplayManagers.AudioManager.PlayPopupClose();
        this._animationController.EndAnimation();
        yield return new WaitForSecondsRealtime(0.5f);
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