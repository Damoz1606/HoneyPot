using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    [SerializeField] private int _frameRate = Constants.FRAME_RATE;
    private void Awake()
    {
        Application.targetFrameRate = this._frameRate;
    }
}
