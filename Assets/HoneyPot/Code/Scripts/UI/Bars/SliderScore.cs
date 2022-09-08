using UnityEngine;
using TMPro;

public class SliderScore : _SliderBase
{
    private void Start() {
        this.SetMaxValue(GameplayManagers.ScoreManager.MaxScore);
    }

    public override void SetMaxValue(float maxValue)
    {
        this._slider.maxValue = maxValue;
        this._slider.value = 0;
    }

    public override void SetValue(float value)
    {
        this._slider.value = value;
    }
}