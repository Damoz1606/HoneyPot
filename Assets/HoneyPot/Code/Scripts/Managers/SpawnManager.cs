using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private TetrominoNormalSpawner _tetrominoeNormalSpawner;
    [SerializeField] private BlockNormalPool _blockNormalSpawner;
    [SerializeField] private bool _usePool = true;

    public bool UsePool
    {
        set
        {
            this._usePool = value;
            this._tetrominoeNormalSpawner.UsePool = this._usePool;
            this._blockNormalSpawner.UsePool = this._usePool;
        }
    }

    public BlockNormalPool BlockNormalSpawner { get { return this._blockNormalSpawner; } }
    public TetrominoNormalSpawner TetrominoeNormalSpawner { get { return this._tetrominoeNormalSpawner; } }

    private void Awake() => this.UsePool = this._usePool;
}
