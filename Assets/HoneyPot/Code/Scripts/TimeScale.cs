using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private float scale = 1;
    void Start()
    {
        Time.timeScale = scale;
    }
}
