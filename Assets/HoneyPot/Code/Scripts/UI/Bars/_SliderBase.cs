using UnityEngine;
using UnityEngine.UI;

public abstract class _SliderBase : MonoBehaviour
{
    [SerializeField] protected Slider _slider;

    public abstract void SetMaxValue(float maxValue);

    public abstract void SetValue(float value);
}