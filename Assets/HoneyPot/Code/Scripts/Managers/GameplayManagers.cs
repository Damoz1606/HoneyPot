using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(CameraManager))]
[RequireComponent(typeof(ComboManager))]
[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(GridManager))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(ParticlesManager))]
[RequireComponent(typeof(SpawnManager))]
[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(TargetManager))]
[RequireComponent(typeof(UIManager))]
public class GameplayManagers : MonoBehaviour
{
    private static AudioManager _audioManager;
    private static CameraManager _cameraManager;
    private static ComboManager _comboManager;
    private static GameManager _gameManager;
    private static GridManager _gridManager;
    private static InputManager _inputManager;
    private static ParticlesManager _particlesManager;
    private static SpawnManager _spawnManager;
    private static ScoreManager _scoreManager;
    private static TargetManager _targetManager;
    private static UIManager _uiManager;

    public static AudioManager AudioManager { get { return _audioManager; } }
    public static CameraManager CameraManager { get { return _cameraManager; } }
    public static ComboManager ComboManager { get { return _comboManager; } }
    public static GameManager GameManager { get { return _gameManager; } }
    public static GridManager GridManager { get { return _gridManager; } }
    public static InputManager InputManager { get { return _inputManager; } }
    public static ParticlesManager ParticlesManager { get { return _particlesManager; } }
    public static SpawnManager SpawnManager { get { return _spawnManager; } }
    public static ScoreManager ScoreManager { get { return _scoreManager; } }
    public static TargetManager TargetManager { get { return _targetManager; } }
    public static UIManager UIManager { get { return _uiManager; } }

    private void Awake()
    {
        _audioManager = GetComponent<AudioManager>();
        _cameraManager = GetComponent<CameraManager>();
        _comboManager = GetComponent<ComboManager>();
        _gameManager = GetComponent<GameManager>();
        _gridManager = GetComponent<GridManager>();
        _inputManager = GetComponent<InputManager>();
        _particlesManager = GetComponent<ParticlesManager>();
        _spawnManager = GetComponent<SpawnManager>();
        _scoreManager = GetComponent<ScoreManager>();
        _targetManager = GetComponent<TargetManager>();
        _uiManager = GetComponent<UIManager>();
    }
}
