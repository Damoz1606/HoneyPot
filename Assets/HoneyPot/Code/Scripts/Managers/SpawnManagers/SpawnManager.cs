using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private TetrominoePoolSpawner _tetrominoePoolSpawner;
    [SerializeField] private BlockPoolSpawner _blockPoolSpawner;
    [SerializeField] private NormalTilePoolSpawner _normalTilePoolSpawner;
    [SerializeField] private ComboTilePoolSpawner _comboTilePoolSpawner;

    public TetrominoePoolSpawner TetrominoePoolSpawner { get { return this._tetrominoePoolSpawner; } }
    public BlockPoolSpawner BlockPoolSpawner { get { return this._blockPoolSpawner; } }
    public NormalTilePoolSpawner NormalTilePoolSpawner { get { return this._normalTilePoolSpawner; } }
    public ComboTilePoolSpawner ComboTilePoolSpawner { get { return this._comboTilePoolSpawner; } }
}
