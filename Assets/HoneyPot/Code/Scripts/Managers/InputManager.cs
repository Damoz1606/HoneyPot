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
    private Transform _selectedTile;

    public bool IsInputActive { set { this._isInputActive = value; } }

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
            this._selectedTile = GameplayManagers.GridManager.GridTile.GetTileAtGridPosition(_startMousePosition);
        }

        if (this._selectedTile != null && Input.GetMouseButtonUp(0))
        {
            if (Time.time - this._buttonDownPhaseStart > this._tapInterval)
            {
                this._endMousePosition = VectorRound.Vector2Round(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                this._currentSwipe = this._endMousePosition - this._startMousePosition;
                this._currentSwipe.Normalize();

                //Left
                if (this._currentSwipe.x < 0 &&
                this._currentSwipe.y > -this._errorSwipeValue &&
                this._currentSwipe.y < this._errorSwipeValue)
                    this._selectedTile.GetComponent<Tile>().MovementController.SwipeHorizontal(false);
                //Right
                else if (this._currentSwipe.x > 0 &&
                this._currentSwipe.y > -this._errorSwipeValue &&
                this._currentSwipe.y < this._errorSwipeValue)
                    this._selectedTile.GetComponent<Tile>().MovementController.SwipeHorizontal(true);
                //Up
                else if (this._currentSwipe.y > 0 &&
                this._currentSwipe.x > -this._errorSwipeValue &&
                this._currentSwipe.x < this._errorSwipeValue)
                    this._selectedTile.GetComponent<Tile>().MovementController.SwipeVertical(true);
                //Down 
                else if (this._currentSwipe.y < 0 &&
                this._currentSwipe.x > -this._errorSwipeValue &&
                this._currentSwipe.x < this._errorSwipeValue)
                    this._selectedTile.GetComponent<Tile>().MovementController.SwipeVertical(false);
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
                    this._selectedTile = GameplayManagers.GridManager.GridTile.GetTileAtGridPosition(_startTouchPosition);
                    break;
                case TouchPhase.Ended:
                    if (this._selectedTile != null && (Time.time - this._touchDownPhaseStart > this._tapInterval))
                    {
                        this._endTouchPosition = VectorRound.Vector2Round(Camera.main.ScreenToWorldPoint(touch.position));
                        this._currentSwipe = this._endTouchPosition - this._startTouchPosition;
                        this._currentSwipe.Normalize();
                        //Left
                        if (this._currentSwipe.x < 0 &&
                        this._currentSwipe.y > -this._errorSwipeValue &&
                        this._currentSwipe.y < this._errorSwipeValue)
                            this._selectedTile.GetComponent<Tile>().MovementController.SwipeHorizontal(false);
                        //Right
                        else if (this._currentSwipe.x > 0 &&
                        this._currentSwipe.y > -this._errorSwipeValue &&
                        this._currentSwipe.y < this._errorSwipeValue)
                            this._selectedTile.GetComponent<Tile>().MovementController.SwipeHorizontal(true);
                        //Up
                        else if (this._currentSwipe.y > 0 &&
                        this._currentSwipe.x > -this._errorSwipeValue &&
                        this._currentSwipe.x < this._errorSwipeValue)
                            this._selectedTile.GetComponent<Tile>().MovementController.SwipeVertical(true);
                        //Down 
                        else if (this._currentSwipe.y < 0 &&
                        this._currentSwipe.x > -this._errorSwipeValue &&
                        this._currentSwipe.x < this._errorSwipeValue)
                            this._selectedTile.GetComponent<Tile>().MovementController.SwipeVertical(false);

                    }
                    break;

            }

        }
    }
    #endregion
}
