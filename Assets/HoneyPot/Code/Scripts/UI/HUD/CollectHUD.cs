using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectHUD : MonoBehaviour, IHUD
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _targetValue;
    [SerializeField] private TextMeshProUGUI _value;

    public int Value { set; get; }

    public void Initialize(Material material, int required)
    {
        this._image.material = material;
        this._targetValue.text = required.ToString();
    }

    public void OnActiveHUD()
    {
        this.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(true);
        this.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
    }

    public void OnDeactiveHUD()
    {
        this.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.Linear).OnComplete(() => this.gameObject.SetActive(false));
    }

    public void OnUpdateHUD()
    {
        this._value.text = Value.ToString();
    }
}