using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(CameraManager))]
[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(GridManager))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(SpawnManager))]
public class GameplayManagers : MonoBehaviour
{
    private static AudioManager _audioManager;
    private static CameraManager _cameraManager;
    private static GameManager _gameManager;
    private static GridManager _gridManager;
    private static InputManager _inputManager;
    private static SpawnManager _spawnManager;

    public static AudioManager AudioManager { get { return _audioManager; } }
    public static CameraManager CameraManager { get { return _cameraManager; } }
    public static GameManager GameManager { get { return _gameManager; } }
    public static GridManager GridManager { get { return _gridManager; } }
    public static InputManager InputManager { get { return _inputManager; } }
    public static SpawnManager SpawnManager { get { return _spawnManager; } }

    private void Awake()
    {
        _audioManager = GetComponent<AudioManager>();
        _cameraManager = GetComponent<CameraManager>();
        _gameManager = GetComponent<GameManager>();
        _gridManager = GetComponent<GridManager>();
        _inputManager = GetComponent<InputManager>();
        _spawnManager = GetComponent<SpawnManager>();
    }
}
