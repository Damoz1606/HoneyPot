using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwipeComponent))]
[RequireComponent(typeof(DestroyWithParticles))]
[RequireComponent(typeof(PointComponent))]

public class Tile : MonoBehaviour
{
    [SerializeField] private TileTypes _type;
    private SwipeComponent _movementController;
    private DestroyWithParticles _destroyWithParticles;

    public SwipeComponent MovementController { get { return this._movementController; } }
    public TileTypes Type { get { return this._type; } }

    private void Awake()
    {
        this._movementController = this.GetComponent<SwipeComponent>();
        this._destroyWithParticles = this.GetComponent<DestroyWithParticles>();
    }
}
