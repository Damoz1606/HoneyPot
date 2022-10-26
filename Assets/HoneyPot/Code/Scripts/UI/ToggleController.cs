using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject onActive;
    [SerializeField] private GameObject onDeactive;

    [SerializeField] private string _onActiveText;
    [SerializeField] private string _onDeactiveText;

    private void Start()
    {
        this.ChangeOnActive();
    }

    public void ChangeOnActive()
    {
        if (this.GetComponent<Toggle>().isOn)
        {
            this.onActive.SetActive(true);
            this.onDeactive.SetActive(false);
            this._text.text = this._onActiveText;
        }
        else
        {
            this.onDeactive.SetActive(true);
            this.onActive.SetActive(false);
            this._text.text = this._onDeactiveText;
        }
    }
}
