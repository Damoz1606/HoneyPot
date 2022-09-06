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

    public void MouseInput()
    {

    }
    #endregion

    #region PhoneInput
    public void TouchInput()
    {

    }
    #endregion
}
