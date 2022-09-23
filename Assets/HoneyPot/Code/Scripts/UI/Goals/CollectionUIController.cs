using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUIController : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _completed;
    [SerializeField] private TextMeshProUGUI _required;
    [SerializeField] private TextMeshProUGUI _value;

    public void Initialize(Material material, int required)
    {
        this._image.material = material;
        if (this._required != null) this._required.text = required.ToString();
    }

    public void UpdateHUD(int value)
    {
        if (this._value != null) this._value.text = value.ToString();
    }

    public void Completed()
    {
        this._completed.SetActive(true);
    }
}
