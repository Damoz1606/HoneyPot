using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotate : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        if (GameplayManagers.InputManager.IsInputActive)
            GameplayManagers.GameManager.CurrentTetromino.RotationController.Rotate(true);
    }
}
