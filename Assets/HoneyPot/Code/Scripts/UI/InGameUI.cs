using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private SliderScore _sliderScore;

    public void UpdateScoreUI() {
        _sliderScore.SetValue(GameplayManagers.ScoreManager.CurrentScore);
        this._textScore.text = $"{GameplayManagers.ScoreManager.CurrentScore}";
    }
}
