using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    [SerializeField] private Transform _blockHolder;
    [SerializeField] private bool _isGameActive;
    private bool _hasStarted = false;
    // private Tetromino _currentTetromino;
    private ITetrominoe _currentTetrominoe;
    private _StatesBase _currentState;

    // public Tetromino CurrentTetromino { get { return this._currentTetromino; } set { this._currentTetromino = value; } }
    public ITetrominoe CurrentTetrominoe { get { return this._currentTetrominoe; } set { this._currentTetrominoe = value; } }
    public Transform BlockHolder { get { return this._blockHolder; } }
    public bool IsGameActive { get { return this._isGameActive; } set { this._isGameActive = value; } }
    public bool HasStarted { get => this._hasStarted; set => this._hasStarted = value; }

    private void Awake()
    {
        this.IsGameActive = false;
    }

    void Update()
    {
        if (this._currentState != null)
        {
            this._currentState.OnUpdate();
        }
    }

    public void SetState(GameStates newState)
    {
        if (this._currentState != null)
        {
            this._currentState.OnDeactivate();
        }
        this._currentState = GetComponentInChildren(this.GetState(newState)) as _StatesBase;
        if (this._currentState != null)
        {
            this._currentState.OnActivate();
        }
    }

    private System.Type GetState(GameStates state)
    {
        switch (state)
        {
            case GameStates.GAMEOVER:
                return typeof(GameOverState);
            case GameStates.PAUSE:
                return typeof(PauseState);
            case GameStates.PLAY:
                return typeof(PlayState);
            case GameStates.WINNER:
                return typeof(WinnerState);
            case GameStates.TUTORIAL:
                return typeof(TutorialState);
            default:
                return null;
        }
    }

    public void SetUp()
    {
        this.SetState(GameStates.PLAY);
    }
}
