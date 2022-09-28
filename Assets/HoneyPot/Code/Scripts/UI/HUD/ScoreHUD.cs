using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreHUD : MonoBehaviour, IHUD
{
    [SerializeField] private TextMeshProUGUI _scoreGoal;

    public void Initialize(int required)
    {
        this._scoreGoal.text = required.ToString();
    }

    public void OnActiveHUD()
    {
        this.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(true);
        this.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
    }

    public void OnDeactiveHUD()
    {
        this.transform.DOScale(Vector3.one, 1f).SetEase(Ease.Linear).OnComplete(() => this.gameObject.SetActive(false));
    }

    public void OnUpdateHUD()
    {
        // throw new System.NotImplementedException();
    }
}