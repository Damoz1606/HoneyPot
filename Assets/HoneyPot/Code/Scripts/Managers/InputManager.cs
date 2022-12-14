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
        Vector2Int blockPosition;

        switch (this.GetSwipe())
        {
            case SwipeTypes.UP:
                blockPosition = VectorRound.Vector2Round(this._selectedBlock.GetComponent<SwipeComponent>().GetNextVerticalPosition(true));
                nextBlock = GameplayManagers.GridManager.Board.GetBlockAt(blockPosition);
                break;
            case SwipeTypes.DOWN:
                blockPosition = VectorRound.Vector2Round(this._selectedBlock.GetComponent<SwipeComponent>().GetNextVerticalPosition(false));
                nextBlock = GameplayManagers.GridManager.Board.GetBlockAt(blockPosition);
                break;
            case SwipeTypes.RIGHT:
                blockPosition = VectorRound.Vector2Round(this._selectedBlock.GetComponent<SwipeComponent>().GetNextHorizontalPosition(true));
                nextBlock = GameplayManagers.GridManager.Board.GetBlockAt(blockPosition);
                break;
            case SwipeTypes.LEFT:
                blockPosition = VectorRound.Vector2Round(this._selectedBlock.GetComponent<SwipeComponent>().GetNextHorizontalPosition(false));
                nextBlock = GameplayManagers.GridManager.Board.GetBlockAt(blockPosition);
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
            GameplayManagers.GameManager.CurrentTetrominoe.RotationController.Rotate(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.D))
        {
            GameplayManagers.GameManager.CurrentTetrominoe.FallController.InstantFall();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            GameplayManagers.GameManager.CurrentTetrominoe.MovementController.Move(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            GameplayManagers.GameManager.CurrentTetrominoe.MovementController.Move(Vector2.right);
        }
    }

    private float _buttonDownPhaseStart;
    private Vector2 _startMousePosition;
    private Vector2 _endMousePosition;
    public void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this._startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this._buttonDownPhaseStart = Time.time;
            this._selectedBlock = GameplayManagers.GridManager.Board.GetBlockAt(VectorRound.Vector2Round(_startMousePosition));
        }

        if (this._selectedBlock != null && Input.GetMouseButtonUp(0))
        {
            if (Time.time - this._buttonDownPhaseStart > this._tapInterval)
            {
                this._endMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
                    this._startTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    this._touchDownPhaseStart = Time.time;
                    this._selectedBlock = GameplayManagers.GridManager.Board.GetBlockAt(VectorRound.Vector2Round(_startTouchPosition));
                    break;
                case TouchPhase.Ended:
                    if (this._selectedBlock != null && (Time.time - this._touchDownPhaseStart > this._tapInterval))
                    {
                        this._endTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
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
