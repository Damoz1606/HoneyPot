using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameOverPopup : _PopupBase
{
    [SerializeField] Star[] _stars;
    public override void OnActivatePopup()
    {
        this.gameObject.SetActive(true);
        this.ActiveStars();
    }

    public override void OnDeactivatePopup()
    {
        this.gameObject.SetActive(false);
    }

    private void ActiveStars()
    {
        StartCoroutine(ActiveStarsCoroutine());
    }
    private IEnumerator ActiveStarsCoroutine()
    {
        if (GameplayManagers.ScoreManager.ScoreReferences[0] <= GameplayManagers.ScoreManager.CurrentScore)
        {
            yield return new WaitForSeconds(1f);
            _stars[0].UpdateReference();
        }
        if (GameplayManagers.ScoreManager.ScoreReferences[1] <= GameplayManagers.ScoreManager.CurrentScore)
        {
            yield return new WaitForSeconds(1f);
            _stars[1].UpdateReference();
        }
        if (GameplayManagers.ScoreManager.ScoreReferences[2] <= GameplayManagers.ScoreManager.CurrentScore)
        {
            yield return new WaitForSeconds(1f);
            _stars[2].UpdateReference();
        }
        yield break;
    }
}