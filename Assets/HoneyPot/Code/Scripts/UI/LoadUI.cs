using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : AUIBase
{
    [SerializeField] private RectTransform _leftDoor;
    [SerializeField] private RectTransform _leftDarkDoor;
    [SerializeField] private RectTransform _rightDoor;
    [SerializeField] private RectTransform _rightDarkDoor;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private Image _loadingSprite;

    private void Start()
    {
        this.gameObject.SetActive(false);
        _leftDoor.gameObject.SetActive(false);
        _rightDoor.gameObject.SetActive(false);
        _leftDarkDoor.gameObject.SetActive(false);
        _rightDarkDoor.gameObject.SetActive(false);
        _loadingText.gameObject.SetActive(false);
        _loadingSprite.gameObject.SetActive(false);
    }

    public override void EndUI()
    {
        _leftDoor.gameObject.SetActive(false);
        _rightDoor.gameObject.SetActive(false);
        _leftDarkDoor.gameObject.SetActive(false);
        _rightDarkDoor.gameObject.SetActive(false);
        _loadingText.gameObject.SetActive(false);
        _loadingSprite.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public override void StartUI()
    {
        this.gameObject.SetActive(true);

        _leftDoor.gameObject.SetActive(true);
        _rightDoor.gameObject.SetActive(true);
        _leftDarkDoor.gameObject.SetActive(true);
        _rightDarkDoor.gameObject.SetActive(true);
        _leftDoor.DOAnchorPosX(_leftDoor.anchoredPosition.x + 1960, 1, true).Play();
        _leftDarkDoor.DOAnchorPosX(_leftDarkDoor.anchoredPosition.x + 1960, 1, true).Play();
        _rightDoor.DOAnchorPosX(_rightDoor.anchoredPosition.x - 1960, 1, true).Play();
        _rightDarkDoor.DOAnchorPosX(_rightDarkDoor.anchoredPosition.x - 1960, 1, true).Play();

        _loadingText.gameObject.SetActive(true);
        _loadingText.DOFade(1, 0.5f);
        _loadingSprite.gameObject.SetActive(true);
        _loadingSprite.DOFade(1, 0.5f);
    }
}