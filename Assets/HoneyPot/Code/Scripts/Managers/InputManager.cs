using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMethods
{
    ComputerInput,
    PhoneInput
}

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputMethods _inputMethod;
    [SerializeField] private bool _isInputActive;
    [SerializeField] private float _tapInterval = 0.5f;
    [SerializeField] private float _errorSwipeValue = 0.5f;
    private Vector2 _currentSwipe;
    private Block _selectedBlock;

    public bool IsInputActive { set { this._isInputActive = value; } get { return this._isInputActive; } }

    private void Update()
    {
        if (this._isInputActive)
        {
            switch (this._inputMethod)
            {
                case InputMethods.ComputerInput:
                    this.KeyboardInput();
                    this.MouseInput();
                    break;
                case InputMethods.PhoneInput:
                    this.TouchInput();
                    break;
            }
        }
    }

    private SwipeTypes GetSwipe()
    {
        if (this._currentSwipe.x > 0 &&
            this._currentSwipe.y > -this._errorSwipeValue &&
            this._currentSwipe.y < this._errorSwipeValue)
            return SwipeTypes.RIGHT;
        else if (this._currentSwipe.x < 0 &&
            this._currentSwipe.y > -this._errorSwipeValue &&
            this._currentSwipe.y < this._errorSwipeValue)
            return SwipeTypes.LEFT;
        else if (this._currentSwipe.y > 0 &&
            this._currentSwipe.x > -this._errorSwipeValue &&
            this._currentSwipe.x < this._errorSwipeValue)
            return SwipeTypes.UP;
        else if (this._currentSwipe.y < 0 &&
                        this._currentSwipe.x > -this._errorSwipeValue &&
                        this._currentSwipe.x < this._errorSwipeValue)
            return SwipeTypes.DOWN;
        else
            return SwipeTypes.DEFAULT;
    }

    private void SwipeEvent()
    {
        Block nextBlock;

        switch (this.GetSwipe())
        {
            case SwipeTypes.UP:
                nextBlock = GameplayManagers.GridManager.Board.GetBlockAt(this._selectedBlock.GetComponent<SwipeComponent>().GetNextVerticalPosition(true));
                break;
            case SwipeTypes.DOWN:
                nextBlock = GameplayManagers.GridManager.Board.GetBlockAt(this._selectedBlock.GetComponent<SwipeComponent>().GetNextVerticalPosition(false));
                break;
            case SwipeTypes.RIGHT:
                nextBlock = GameplayManagers.GridManager.Board.GetBlockAt(this._selectedBlock.GetComponent<SwipeComponent>().GetNextHorizontalPosition(true));
                break;
            case SwipeTypes.LEFT:
                nextBlock = GameplayManagers.GridManager.Board.GetBlockAt(this._selectedBlock.GetComponent<SwipeComponent>().GetNextHorizontalPosition(false));
                break;
            default:
                nextBlock = null;
                break;
        }
        
        if (nextBlock != null) GameplayManagers.GridManager.Board.SwapBlock(_selectedBlock, nextBlock);
    }

    #region ComputerInput
    public void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            GameplayManagers.GameManager.CurrentTetromino.RotationController.Rotate(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.D))
        {
            GameplayManagers.GameManager.CurrentTetromino.FallController.InstantFall();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            GameplayManagers.GameManager.CurrentTetromino.MovementController.Move(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            GameplayManagers.GameManager.CurrentTetromino.MovementController.Move(Vector2.right);
        }
    }

    private float _buttonDownPhaseStart;
    private Vector2 _startMousePosition;
    private Vector2 _endMousePosition;
    public void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this._startMousePosition = VectorRound.Vector2Round(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            this._buttonDownPhaseStart = Time.time;
            this._selectedBlock = GameplayManagers.GridManager.Board.GetBlockAt(_startMousePosition);
        }

        if (this._selectedBlock != null && Input.GetMouseButtonUp(0))
        {
            if (Time.time - this._buttonDownPhaseStart > this._tapInterval)
            {
                this._endMousePosition = VectorRound.Vector2Round(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                this._currentSwipe = this._endMousePosition - this._startMousePosition;
                this._currentSwipe.Normalize();

                this.SwipeEvent();
            }
        }
    }
    #endregion

    #region PhoneInput
    private float _touchDownPhaseStart;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    public void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    this._startTouchPosition = VectorRound.Vector2Round(Camera.main.ScreenToWorldPoint(touch.position));
                    this._touchDownPhaseStart = Time.time;
                    this._selectedBlock = GameplayManagers.GridManager.Board.GetBlockAt(_startTouchPosition);
                    break;
                case TouchPhase.Ended:
                    if (this._selectedBlock != null && (Time.time - this._touchDownPhaseStart > this._tapInterval))
                    {
                        this._endTouchPosition = VectorRound.Vector2Round(Camera.main.ScreenToWorldPoint(touch.position));
                        this._currentSwipe = this._endTouchPosition - this._startTouchPosition;
                        this._currentSwipe.Normalize();

                        this.SwipeEvent();

                    }
                    break;

            }

        }
    }
    #endregion
}
