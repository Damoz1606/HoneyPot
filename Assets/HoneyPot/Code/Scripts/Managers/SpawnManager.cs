using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private AllTetrominoesSpawner _tetrominoeSpawner;
    [SerializeField] private BlockSpawner _blockSpawner;

    [SerializeField] private bool _usePool = false;

    public bool UsePool
    {
        set
        {
            this._usePool = value;
            this._tetrominoeSpawner.UsePool = this._usePool;
            this._blockSpawner.UsePool = this._usePool;
        }
    }

    public BlockSpawner BlockSpawner { get { return this._blockSpawner; } }
    public AllTetrominoesSpawner TetrominoeSpawner { get { return this._tetrominoeSpawner; } }

    private void Awake() => this.UsePool = this._usePool;
}
