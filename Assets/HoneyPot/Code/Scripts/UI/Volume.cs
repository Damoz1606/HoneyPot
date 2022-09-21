using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Volume : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private float _increment = 0.1f;
    [SerializeField] private UnityEvent onValueChange;

    private TextMeshProUGUI _labelTMP;
    private Slider _slider;
    private Button _btnMinus;
    private Button _btnPlus;
    private float _value = 0.5f;

    public float Value { get { return this._value; } }

    private void Awake()
    {
        this._slider = this.transform.Find("Slider").GetComponent<Slider>();
        this._labelTMP = this.transform.Find("Label").GetComponent<TextMeshProUGUI>();
        this._btnMinus = this.transform.Find("Minus").GetComponent<Button>();
        this._btnPlus = this.transform.Find("Plus").GetComponent<Button>();
    }

    private void Start()
    {
        this._slider.onValueChanged.AddListener(delegate { OnChangeValue(this._slider.value); });
        this._btnMinus.onClick.AddListener(delegate { ChangeValue(false); });
        this._btnPlus.onClick.AddListener(delegate { ChangeValue(true); });
        this._labelTMP.text = this._label != null ? this._label : "Music";
        this._slider.maxValue = 1;
        this._slider.minValue = 0;
        this._slider.value = this._value;
    }

    private void ChangeValue(bool flag = true)
    {
        this._value += flag ? this._increment : -this._increment;
        if (this._value < 0) this._value = 0;
        else if (this._value > 1) this._value = 1;
        this._slider.value = this._value;
        this.onValueChange.Invoke();
    }

    private void OnChangeValue(float value)
    {
        this._value = value;
        this.onValueChange.Invoke();
    }
}
