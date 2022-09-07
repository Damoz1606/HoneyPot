using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstantFall : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        GameplayManagers.GameManager.CurrentTetromino.FallController.InstantFall();
    }
}
