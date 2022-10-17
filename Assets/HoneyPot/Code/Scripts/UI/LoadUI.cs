using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : AUIBase
{
    [SerializeField] private List<_AnimationBase> doors = new List<_AnimationBase>();
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private Image _loadingSprite;

    private void Start()
    {
        this.gameObject.SetActive(false);
        doors.ForEach(item => item.gameObject.SetActive(false));
        _loadingText.gameObject.SetActive(false);
        _loadingSprite.gameObject.SetActive(false);
    }

    public override void EndUI()
    {
        doors.ForEach(item => item.gameObject.SetActive(false));
        _loadingText.gameObject.SetActive(false);
        _loadingSprite.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public override void StartUI()
    {
        this.gameObject.SetActive(true);
        doors.ForEach(item => item.gameObject.SetActive(true));
        _loadingText.gameObject.SetActive(true);
        _loadingSprite.gameObject.SetActive(true);
        _loadingText.DOFade(1, 0.5f);
        _loadingSprite.DOFade(1, 0.5f);
    }
}