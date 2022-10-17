using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButtonUIController : MonoBehaviour
{
    [SerializeField] private string _worldID;
    [SerializeField] private string _levelID;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private List<Star> _stars = new List<Star>();
    [SerializeField] private int _starCount;

    public int StarCount { set => this._starCount = value; }


    private void Start()
    {
        _text.text = _levelID;
        if (this._stars.Count < 0) return;
        for (int i = 0; i < this._starCount; i++)
        {
            _stars[i].UpdateReference();
        }
    }
}