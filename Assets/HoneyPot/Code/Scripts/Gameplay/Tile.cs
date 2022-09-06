using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwipeComponent))]
public class Tile : MonoBehaviour
{
    private SwipeComponent _movementController;
    public SwipeComponent MovementController { get { return this._movementController; } }

    private void Awake()
    {
        this._movementController = this.GetComponent<SwipeComponent>();
    }
}
