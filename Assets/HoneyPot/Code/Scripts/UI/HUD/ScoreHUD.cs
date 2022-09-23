using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreHUD : _HUDBase
{
    [SerializeField] private TextMeshProUGUI _scoreGoal;

    public void Initialize(int required)
    {
        this._scoreGoal.text = required.ToString();
    }

    public override void OnActiveHUD()
    {
        this.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(true);
        this.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
    }

    public override void OnDeactiveHUD()
    {
        this.transform.DOScale(Vector3.one, 1f).SetEase(Ease.Linear).OnComplete(() => this.gameObject.SetActive(false));
    }

    public override void OnUpdateHUD()
    {
        // throw new System.NotImplementedException();
    }
}