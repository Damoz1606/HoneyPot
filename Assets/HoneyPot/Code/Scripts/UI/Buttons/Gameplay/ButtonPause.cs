using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        GameplayManagers.GameManager.SetState(GameStates.PAUSE);
    }
}
