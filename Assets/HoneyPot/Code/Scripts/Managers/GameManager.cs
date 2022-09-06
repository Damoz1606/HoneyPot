using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    GAMEOVER,
    PAUSE,
    PLAY,
    RESUME
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _blockHolder;
    [SerializeField] private Transform _tileHolder;
    [SerializeField] private bool _isGameActive;
    private Tetromino _currentTetromino;
    private _StatesBase _currentState;

    public Tetromino CurrentTetromino { get { return this._currentTetromino; } set { this._currentTetromino = value; } }
    public Transform BlockHolder { get { return this._blockHolder; } }
    public Transform TileHolder { get { return this._tileHolder; } }
    public bool IsGameActive { get { return this._isGameActive; } set { this._isGameActive = value; } }

    private void Awake()
    {
        this.IsGameActive = false;
    }

    void Start()
    {
        this.SetState(GameStates.PLAY);
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
            case GameStates.RESUME:
                return typeof(ResumeState);
            default:
                return null;
        }
    }
}
