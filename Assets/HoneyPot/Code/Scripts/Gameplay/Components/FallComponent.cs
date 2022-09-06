using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallComponent : MonoBehaviour
{
    [SerializeField] private float _transitionInterval = 0.8f;
    [SerializeField] private float _fastTransitionInterval = 0;
    private float _lastFall;

    public void FreeFall() {
        if(Time.time - this._lastFall >= this._transitionInterval) {
            this.transform.position += Vector3.down;
            /* if() {

            } else {

            } */

            this._lastFall = Time.time;
        }
    }

    public void InstantFall() {
        this._transitionInterval = this._fastTransitionInterval;
    }
}
