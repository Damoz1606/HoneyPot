using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRight : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        if (GameplayManagers.InputManager.IsInputActive)
            GameplayManagers.GameManager.CurrentTetrominoe.MovementController.Move(Vector2.right);
    }
}
