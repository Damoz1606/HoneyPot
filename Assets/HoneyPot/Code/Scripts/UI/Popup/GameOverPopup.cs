using System.Collections;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class GameOverPopup : _PopupBase
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        this._animationController = GetComponent<AnimationController>();
    }

    public override void OnActivatePopup()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(OnActivatePopupCoroutine());
        scoreText.text = $"{GameplayManagers.ScoreManager.CurrentScore}";
    }

    public override void OnDeactivatePopup()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(OnDeactivatePopupCoroutine());
    }

    private IEnumerator OnActivatePopupCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        this._animationController.StartAnimation();
        GameplayManagers.AudioManager.PlayUI(GameplayManagers.AudioManager.UIGameOver);
        yield break;
    }

    private IEnumerator OnDeactivatePopupCoroutine()
    {
        GameplayManagers.AudioManager.PlayUI(GameplayManagers.AudioManager.UIGameOver);
        this._animationController.EndAnimation();
        yield return new WaitForSecondsRealtime(0.5f);
        this.gameObject.SetActive(false);
        yield break;
    }
}