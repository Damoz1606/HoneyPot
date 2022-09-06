using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TetrominoSpawnManager))]
[RequireComponent(typeof(TileSpawnManager))]
public class SpawnManager : MonoBehaviour
{
    private TetrominoSpawnManager _tetrominoSpawnManager;
    private TileSpawnManager _tileSpawnManager;

    public TetrominoSpawnManager TetrominoSpawnManager { get { return this._tetrominoSpawnManager; } }
    public TileSpawnManager TileSpawnManager { get { return this._tileSpawnManager; } }

    private void Awake()
    {
        this._tetrominoSpawnManager = GetComponent<TetrominoSpawnManager>();
        this._tileSpawnManager = GetComponent<TileSpawnManager>();
    }
}
