using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDisable : MonoBehaviour
{
    void Update()
    {
        this.transform.rotation = Quaternion.identity;
    }
}
