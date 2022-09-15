using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestroyWithParticles))]
public class Block : MonoBehaviour
{
    private bool _canDecrease = false;
    private DestroyWithParticles _destroyWithParticles;

    public bool CanDecrease { set { this._canDecrease = value; } get { return this._canDecrease; } }
    public DestroyWithParticles DestroyWithParticles { get { return this._destroyWithParticles; } }

    private void Awake()
    {
        this._destroyWithParticles = this.GetComponent<DestroyWithParticles>();
    }

    void Start()
    {
        GameplayManagers.SpawnManager.TileSpawnManager.Spawn(this.transform);
    }
}
