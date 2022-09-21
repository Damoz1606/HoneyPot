using System.Collections;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class GameOverPopup : _PopupBase
{
    [SerializeField] private Star[] _stars;
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
        StartCoroutine(OnDeactivatePopupCoroutine());
    }

    private IEnumerator OnActivatePopupCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        this._animationController.StartAnimation();
        GameplayManagers.AudioManager.PlayUI(GameplayManagers.AudioManager.UIGameOver);
        yield return new WaitForSecondsRealtime(1.25f);
        this.ActiveStars();
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