using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeft : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        if (GameplayManagers.InputManager.IsInputActive)
            GameplayManagers.GameManager.CurrentTetrominoe.HorizontalMovement.Move(Vector2.left);
    }
}
