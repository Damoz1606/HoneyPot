using UnityEngine;
using UnityEngine.UI;

public abstract class _SliderBase : MonoBehaviour
{
    [SerializeField] protected Slider _slider;
    [SerializeField] private float smoothTime = 100;
    protected float _slideValue = 0;
    private float currentVelocity = 0;

    private void Update() {
        this.SmoothSlide();
    }

    public abstract void SetMaxValue(float maxValue);

    public abstract void SetValue(float value);

    public void SmoothSlide()
    {
        float currentValue = Mathf.SmoothDamp(this._slider.value, _slideValue, ref currentVelocity, smoothTime * Time.deltaTime);
        this._slider.value = currentValue;
    }
}