using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithPoints : MonoBehaviour
{
    [SerializeField] private int _points = 100;
    [SerializeField] private int _increment = 1;
    public int Increment { set { this._increment = value; } }

    void OnDestroy()
    {
        GameplayManagers.ScoreManager.OnScore(this._points * this._increment);
    }
}
