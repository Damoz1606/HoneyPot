using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _targetTime = 60.0f;

    public float TargetTime { set { this._targetTime = value; } }

    private void Update()
    {
        this._targetTime -= Time.deltaTime;
        if (this._targetTime <= 0.0f) TimeEnded();
    }

    private void TimeEnded() { }
}
