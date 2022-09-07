using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRotate : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        GameplayManagers.GameManager.CurrentTetromino.RotationController.Rotate(true);
    }
}
