using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _doors;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private SliderScore _sliderScore;
    [SerializeField] private Star[] _reference;

    private AnimationController _animationController;
    private bool hasReferences = true;


    private void Awake()
    {
        this._animationController = GetComponent<AnimationController>();
    }

    public void StartAnimation()
    {
        this._doors.SetActive(true);
        this._animationController.StartAnimation();
    }

    public void EndAnimation()
    {
        this._doors.SetActive(true);
        this._animationController.EndAnimation();
    }

    public void UpdateScoreUI()
    {
        _sliderScore.SetValue(GameplayManagers.ScoreManager.CurrentScore);
        this.ActiveReference();
        this._textScore.text = $"{GameplayManagers.ScoreManager.CurrentScore}";
    }

    private void ActiveReference()
    {
        if (GameplayManagers.ScoreManager.ScoreReferences[0] <= GameplayManagers.ScoreManager.CurrentScore && hasReferences)
        {
            _reference[0].UpdateReference();
        }
        if (GameplayManagers.ScoreManager.ScoreReferences[1] <= GameplayManagers.ScoreManager.CurrentScore && hasReferences)
        {
            _reference[1].UpdateReference();
        }
        if (GameplayManagers.ScoreManager.ScoreReferences[2] <= GameplayManagers.ScoreManager.CurrentScore && hasReferences)
        {
            _reference[2].UpdateReference();
            hasReferences = false;
        }
    }
}
