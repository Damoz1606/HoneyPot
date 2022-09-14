using UnityEngine;
using TMPro;

public class SliderScore : _SliderBase
{
    [SerializeField] private bool _invertSlider = false;

    private void Start()
    {
        this.SetMaxValue(GameplayManagers.ScoreManager.MaxScore);
    }

    public override void SetMaxValue(float maxValue)
    {
        this._slider.maxValue = maxValue;
        this._slider.value = this._invertSlider ? maxValue : 0;
    }

    public override void SetValue(float value)
    {
        this._slideValue = this._invertSlider ? this._slider.maxValue - value : value;
    }
}