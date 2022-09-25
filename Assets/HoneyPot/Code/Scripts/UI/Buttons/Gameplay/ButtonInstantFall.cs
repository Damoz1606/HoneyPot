using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstantFall : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        if (GameplayManagers.InputManager.IsInputActive)
            GameplayManagers.GameManager.CurrentTetrominoe.FallController.InstantFall();
    }
}
