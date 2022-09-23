using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreGoalUIController : MonoBehaviour
{
    [SerializeField] private GameObject _completed;
    [SerializeField] private TextMeshProUGUI _required;

    public void Initialize(int required)
    {
        this._required.text = required.ToString();
    }

    public void UpdateHUD(int value)
    {
        
    }

    public void Completed()
    {
        this._completed.SetActive(true);
    }
}
