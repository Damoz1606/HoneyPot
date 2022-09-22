using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(CameraManager))]
[RequireComponent(typeof(ComboManager))]
[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(GridManager))]
[RequireComponent(typeof(HistoryManager))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(ParticlesManager))]
[RequireComponent(typeof(SpawnManager))]
[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(GoalManager))]
[RequireComponent(typeof(UIManager))]
public class GameplayManagers : MonoBehaviour
{
    private static AudioManager _audioManager;
    private static CameraManager _cameraManager;
    private static ComboManager _comboManager;
    private static GameManager _gameManager;
    private static GridManager _gridManager;
    private static HistoryManager _historyManager;
    private static InputManager _inputManager;
    private static ParticlesManager _particlesManager;
    private static SpawnManager _spawnManager;
    private static ScoreManager _scoreManager;
    private static GoalManager _goalManager;
    private static UIManager _uiManager;

    public static AudioManager AudioManager { get { return _audioManager; } }
    public static CameraManager CameraManager { get { return _cameraManager; } }
    public static ComboManager ComboManager { get { return _comboManager; } }
    public static GameManager GameManager { get { return _gameManager; } }
    public static GridManager GridManager { get { return _gridManager; } }
    public static InputManager InputManager { get { return _inputManager; } }
    public static HistoryManager HistoryManager { get { return _historyManager; } }
    public static ParticlesManager ParticlesManager { get { return _particlesManager; } }
    public static SpawnManager SpawnManager { get { return _spawnManager; } }
    public static ScoreManager ScoreManager { get { return _scoreManager; } }
    public static GoalManager GoalManager { get { return _goalManager; } }
    public static UIManager UIManager { get { return _uiManager; } }

    private void Awake()
    {
        _audioManager = GetComponent<AudioManager>();
        _cameraManager = GetComponent<CameraManager>();
        _comboManager = GetComponent<ComboManager>();
        _gameManager = GetComponent<GameManager>();
        _gridManager = GetComponent<GridManager>();
        _historyManager = GetComponent<HistoryManager>();
        _inputManager = GetComponent<InputManager>();
        _particlesManager = GetComponent<ParticlesManager>();
        _spawnManager = GetComponent<SpawnManager>();
        _scoreManager = GetComponent<ScoreManager>();
        _goalManager = GetComponent<GoalManager>();
        _uiManager = GetComponent<UIManager>();
    }
}
