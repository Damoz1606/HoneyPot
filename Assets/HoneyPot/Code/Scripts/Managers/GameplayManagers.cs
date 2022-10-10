using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(CameraManager))]
[RequireComponent(typeof(ChallengeManager))]
[RequireComponent(typeof(ComboManager))]
[RequireComponent(typeof(EventManager))]
[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(GridManager))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(ParticlesManager))]
[RequireComponent(typeof(SpawnManager))]
[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(UIManager))]
public class GameplayManagers : MonoBehaviour, IManager
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
    public static UIManager UIManager { get { return _uiManager; } }

    private void Awake()
    {
        _audioManager = GetComponent<AudioManager>();
        _cameraManager = GetComponent<CameraManager>();
        _comboManager = GetComponent<ComboManager>();
        _gridManager = GetComponent<GridManager>();
        _inputManager = GetComponent<InputManager>();
        _particlesManager = GetComponent<ParticlesManager>();
        _spawnManager = GetComponent<SpawnManager>();
        _scoreManager = GetComponent<ScoreManager>();
        _uiManager = GetComponent<UIManager>();
        _gameManager = GetComponent<GameManager>();


    }

    void Start()
    {
        this.SetUp();
    }

    public void SetUp()
    {
        _audioManager.SetUp();
        _cameraManager.SetUp();
        GetComponent<ChallengeManager>().SetUp();
        _gridManager.SetUp();
        _inputManager.SetUp();
        _particlesManager.SetUp();
        _spawnManager.SetUp();
        _scoreManager.SetUp();
        _uiManager.SetUp();
        _gameManager.SetUp();
    }
}
