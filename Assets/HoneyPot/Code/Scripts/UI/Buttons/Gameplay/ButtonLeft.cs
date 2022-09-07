using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLeft : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        GameplayManagers.GameManager.CurrentTetromino.MovementController.Move(Vector2.left);
    }
}
