using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonResume : _ButtonEventBase
{
    public override void ButtonEvent()
    {
        GameplayManagers.GameManager.SetState(GameStates.PLAY);
    }
}
