using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private bool _canDecrease = false;

    public bool CanDecrease { set { this._canDecrease = value; } get { return this._canDecrease; } }

    void Start()
    {
        GameplayManagers.SpawnManager.TileSpawnManager.Spawn(this.transform);
    }
}
