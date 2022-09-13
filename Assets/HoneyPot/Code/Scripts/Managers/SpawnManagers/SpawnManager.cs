using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private TetrominoSpawnManager _tetrominoSpawnManager;
    [SerializeField] private TileSpawnManager _tileSpawnManager;

    public TetrominoSpawnManager TetrominoSpawnManager { get { return this._tetrominoSpawnManager; } }
    public TileSpawnManager TileSpawnManager { get { return this._tileSpawnManager; } }
}
